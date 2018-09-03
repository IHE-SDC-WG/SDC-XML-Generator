using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Globalization;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
//using 

namespace SDC
{
    public class FormDesignMapper
    {/// <summary>
        /// Initializes a new instance of the <see cref="FormDesign"/> class.
        /// </summary>
        /// <param name="fD"></param>


        public object rowToNodeMapper(DataRow dr, out ItemType qType)
        {
            //TODO: Add similar method to map items in the template header 
            //(see MapTemplate and MapTemplateHeader):

            //!QF, QM, QS, List, ListItem, Section or Note
            qType = new ItemType();

            //!Base item metadata
            String type = (String)(dr["type"]);
            String styleClass = (String)(dr["styleClass"]);
            String shortName = (String)(dr["name"]);

            //!Displayed items: Q, S, N (DI), A (LI)
            Decimal itemCkey = (Decimal)(dr["itemCkey"]);
            String visibleText = (String)(dr["visibleText"]);
            String longText = (String)(dr["longText"]);
            String reportText = (String)(dr["reportText"]);

            //!All Displayed Items
            Boolean enabled = (Boolean)(dr["enabled"]);
            Boolean visible = (Boolean)(dr["visible"]);
            String tooltip = (String)(dr["tooltip"]);
            String popupText = (String)(dr["popupText"]);
            String linkText = (String)(dr["linkText"]);
            String linkText2 = (String)(dr["linkText2"]);
            String Source = (String)(dr["Source"]);  //TODO: not in the Question object //Source is AuthorityID 
            Boolean showInReport = (Boolean)(dr["showInReport"]);
            Boolean mustImplement = (Boolean)(dr["mustImplement"]);
            Int32 sortorder = (Int32)(dr["SortOrder"]);

            //!Response items QF and AF
            String dataType = (String)(dr["ColName"]);
            String answerUnits = (String)(dr["ColName"]);
            String textAfterResponse = (String)(dr["ColName"]);

            //!A, AF
            Boolean sdc = (Boolean)(dr["SelectionDisablesChildren"]); //sdc, SelectionDisablesChildren
            Boolean sds = (Boolean)(dr["SelectionDisablesSiblings"]); //sdc, SelectionDeselectsSiblings
            Boolean omitWhenSelected = (Boolean)(dr["omitWhenSelected"]);

            //!AF only
            Boolean responseRequired = (Boolean)(dr["responseRequired"]);  //not used here

            //!List Items
            String colTextDelimiter = (String)(dr["colTextDelimiter"]);
            Int16 numCols = (Int16)(dr["numCols"]);
            Int16 storedCol = (Int16)(dr["storedCol"]);
            String listHeaderText = (String)(dr["listHeaderText"]);
            Int16 minSelections = (Int16)(dr["minSelections"]);
            Int16 maxSelections = (Int16)(dr["maxSelections"]);

            //!Sections and Questions are repeatable items
            Boolean locked = (Boolean)(dr["ColName"]);
            Int32 minCard = (Int32)(dr["minCard"]);
            Int32 maxCard = (Int32)(dr["maxCard"]);
            Boolean authorityRequired = (Boolean)(dr["AuthorityRequired"]); //ordinarily, this "required" flag only applies to Questions and the Sections (eCC headers) that contain them


            var n = new QuestionItemType();

            //Don't serialize the follwing attributes if they are empty; use if clause to ensure they are not touched if they have default values
            //Need to check if this keep them out of the serialized XML.  If it has no effect, then most of the if clauses can be removed. 
            //This is an attempt to avoid cluttering the XML with default-valued attributes.

            //!Base item metadata
            if (type != String.Empty) n.type = type;
            if (styleClass != String.Empty) n.styleClass = styleClass;
            if (shortName != String.Empty) n.name = shortName;
            //!All Displayed Items
            n.ID = itemCkey.ToString();
            n.title = visibleText;

            if (longText != String.Empty)
            {

            }
            if (reportText != String.Empty)
            {
                //n.reportText = reportText;
            }
            n.enabled = enabled; //show only when false
            n.visible = visible;
            if (tooltip != String.Empty)
            {
                //n.tooltip = tooltip;
            };
            if (popupText != String.Empty)
            {
                //n.popupText = popupText;
            };

            if (linkText != String.Empty)
            {
                //n.linkText = linkText;
            };
            if (linkText2 != String.Empty)
            {
                //n.linkText2 = linkText2;
            };
            //n.Source = Source; 
            //TODO: Why isn't Source in Question in the Schema?  Is it a coding construct?  Should it be treated as OtherText (note)?

            if (!showInReport) n.showInReport = false;
            if (mustImplement == false) n.mustImplement = mustImplement;  //default is true

            //!eCC Special handling Conditional Reporting with "?": Q and S only
            if (n.title.StartsWith("?"))
            {
                n.mustImplement = true;
                n.title.TrimStart('?');
            }
            //!eCC Special handling for Authority Required: Q, A and S only
            if (authorityRequired)
            {
                //n.required = true;
                n.minCard = 1;
                n.mustImplement = true;
            }
            else
            {
                //n.required = false;
                n.minCard = 0;
                n.mustImplement = false;
            }

            if (sortorder >= 0) n.order = sortorder;

            //!Sections and Questions are repeatable items//
            if (locked) n.readOnly = true;  //show only when true
            n.minCard = (UInt16)minCard; //default is 1 (meaning that the Section or Question is required)
            n.maxCard = (UInt16)maxCard;
            //Console.WriteLine( n.title, n.maxCard.ToString(), maxCard.ToString());



            //!Questions: QF, QM or QS
            if (qType == ItemType.QUESTIONFILLIN)
            {
                //n.questionfillin = true;
                if (dataType != string.Empty)
                {
                    //n.datatype = dataType;
                }
                if (answerUnits != string.Empty)
                {
                    //n.answerunits = answerUnits;
                }
                if (textAfterResponse != string.Empty)
                {
                    //n.textAfterResponse = textAfterResponse;
                }
            }


            //!List Wrapper
            if (qType == ItemType.QUESTIONMULTIPLE || qType == ItemType.QUESTIONSINGLE)
            {//add a wrapper element (fixedlistanswer) for all the list items that will be added later
                if (n.Item != null)
                {
                    var listField = new ListFieldType();
                    var list = new ListType();
                    listField.Item = list;

                    //!QM
                    //listField.X_multiSelect = (qType == ItemType.QUESTIONMULTIPLE); //TODO: multiSelect is not needed if maxSelections is specified
                    if (qType == ItemType.QUESTIONMULTIPLE) listField.maxSelections = 0;
                    if (qType == ItemType.QUESTIONSINGLE) listField.maxSelections = 1;
                    if (minSelections > 1) listField.minSelections = (ushort)minSelections;
                    if (maxSelections > 0) listField.maxSelections = (ushort)maxSelections;

                    if (numCols > 0)
                    {
                        listField.storedCol = (storedCol == (byte)0) ? (byte)1 : (byte)storedCol;
                        listField.numCols = (byte)numCols;
                        listField.colTextDelimiter = colTextDelimiter.Trim();
                        if (listHeaderText != String.Empty) listField.ListHeaderText.val = listHeaderText;
                    }
                    listField.name = "";
                    listField.type = "";
                    listField.styleClass = "";

                    list.name = "";
                    list.type = "";
                    list.styleClass = "";

                    n.Item = listField;
                }



                //!QM
                //if (qType == ItemType.QUESTIONMULTIPLE)
                //{
                //    list.allowmultipleselection = (qType == ItemType.QUESTIONMULTIPLE); //just trying out a Boolean trick :-)
                //    if (minSelections > 1) f.minSelections = (UInt16)minSelections;
                //    if (maxSelections > 0) f.maxSelections = (UInt16)maxSelections;
                //}

                //n.Items = n.Items.Add(f);

                //!List Items Only
                //TODO:  Move to method for ListItems
                var qLF = (ListFieldType)n.Item;
                var qL = (ListType)qLF.Item;
                var qLI = new ListItemType();
                qL.Items.Add(qLI);



                qLI.title = visibleText;

                qLI.name = shortName;
                qLI.type = type;
                qLI.styleClass = styleClass;
                qLI.order = sortorder;
                qLI.enabled = enabled;
                qLI.visible = visible;
                qLI.selected = locked;


                qLI.selectionDeselectsSiblings = sdc;
                qLI.selectionDeselectsSiblings = sds;



                //n.selectiondisableschildren = sdc;
                //n.selectiondeselectssiblings = sds;
                //n.omitWhenSelected = omitWhenSelected;

                //!+QF; AF is similar, but should be handled seperately

                var RF = new ResponseFieldType();
                n.ResponseField_Item = RF;  //can assign to ListField too



                if (qType == ItemType.ANSWERFILLIN || qType == ItemType.QUESTIONFILLIN)
                {
                    if (dataType != string.Empty)
                    {
                        //var dt = new DataTypes_DEType();

                        //qLI.ListItemResponseField  = RF;

                        if (answerUnits != string.Empty)
                        {
                            RF.ResponseUnits = new UnitsType();
                            RF.ResponseUnits.val = answerUnits;
                        }

                    }

                    //!+!eCC Special handling for QF; handle AF seperately
                    if (n.title.ToLower().Contains("specify") ||
                        n.title.ToLower().Contains("explain") ||
                        n.title.ToLower().Contains("at least")
                        )
                    {
                        //!liRF.responseRequired = true;  //for AF
                        //n.mustImplement = true;
                        if (minCard == 0) n.minCard = 1; //for QF
                    }
                    else
                    { //n.responseRequired = false; 
                    }

                    return n;

                }
                //var liRF=R;
                //var liRF = new ListItemBaseTypeListItemResponseField();

                //var sdcRF = (ResponseFieldType)RF;  //Response field on a List Item
                var liRF = (ListItemResponseFieldType)(ResponseFieldType)RF;  //Response field on a List Item

                if (qType == ItemType.ANSWERFILLIN)
                {
                    if (textAfterResponse != String.Empty) RF.TextAfterResponse.val = textAfterResponse;
                    liRF.responseRequired = responseRequired;
                }

            }
            return n;

        }

        internal void AddTemplateMetadataToForm(BaseType n, DisplayedType parentNode)
        {
            //Uses CTV data
        }
    }

    public class SDCTreeBuilder: ITreeBuilder
    {
        public DataRow dr = DataRepo.dr;
        public SDCTreeBuilder()
        {    }

        public SectionItemType AddSectionToFormDesign(FormDesignType fd)
        {
            var s = new SectionItemType();
            AddRepeatingTypeItems(s);
            return s;
        }

        #region Base Types
        //!Base type functions
        public RepeatingType AddRepeatingTypeItems(RepeatingType rt)
        {
            rt.maxCard = (ushort)dr["maxCard"];
            rt.minCard = (ushort)dr["minCard"];

            AddDisplayedTypeItems((RepeatingType)rt);
            return (RepeatingType)rt;
        }
        public DisplayedType AddDisplayedTypeItems(DisplayedType dt)
        {
            dt.enabled = (bool)dr["enabled"];
            dt.visible = (bool)dr["visible"];
            dt.title = (string)dr["VisibleText"];
            dt.mustImplement = (bool)dr["mustImplement"];
            dt.showInReport = (bool)dr["showInReport"];

            AddIdentifiedTypeItems((DisplayedType)dt);

            //Each row can have multiple OtherText, Blobs, Contacts, Codes, Links
            //Small numbers of these can travel in the same record, but the structured will be relatively simple.
            //An ORM model will be more capable, enabling any number of each of the above items
            //at any level of complexity.
            //This model does not yet cover extensions, which can occur anywhere
            //Rules not covered here yet - may be out of scope for pilot
            //need style lists


            //adjust for multiple OT: ShortNames, Reporting Text, instructions, footnotes, TNM short forms
            AddOtherText(dt);
            //adjust for multiple Blobs
            AddBlob(dt);
            //adjust for multiple contacts - FIGO and AJCC?
            AddContact(dt);
            //adjust for multiple codes
            AddCodedValue(dt);  //TNM short forms?
            //adjust for multiple link
            AddLink(dt);

            return (DisplayedType)dt;
            //addDisplayedTypeToChildItems(rt);
            //Add @ordered
        }
        public IdentifiedExtensionType AddIdentifiedTypeItems(IdentifiedExtensionType iet)
        {
            iet.baseURI = (string)dr["baseURI"];
            iet.ID = (string)dr["ChecklistTemplateItemCKey"];

            AddExtensionBaseTypeItems((IdentifiedExtensionType)iet);
            return (IdentifiedExtensionType)iet;
        }
        public ExtensionBaseType AddExtensionBaseTypeItems(ExtensionBaseType ebt)
        {
            AddComment(ebt);
            AddExtension(ebt);
            AddBaseTypeItems((ExtensionBaseType)ebt);
            return (ExtensionBaseType)ebt;
        }
        public BaseType AddBaseTypeItems(BaseType bt)
        {
            bt.name       = (string)dr["shortName"];
            bt.type       = (string)dr["type"];
            bt.styleClass = (string)dr["styleClass"];
            bt.order      = (decimal)dr["sortorder"];
            return (BaseType)bt;
        }
        public CommentType AddComment(ExtensionBaseType ebt)
        {
            var c = new CommentType();

            if (ebt.Comment == null) ebt.Comment = new List<CommentType>();
            ebt.Comment.Add(c);
            c.val = (string)dr["Comment"];
            //AddBaseTypeItems(ebt);
            return c;
        }
        public ExtensionType AddExtension(ExtensionBaseType ebt)
        {
            var e = new ExtensionType();

            if (ebt.Extension == null) ebt.Extension = new List<ExtensionType>();
            ebt.Extension.Add(e);
            e.Any = new List<XmlElement>();
            e.AnyAttr = new List<XmlAttribute>();
            //AddBaseTypeItems(ebt);
            //TODO: XElement
            //var any = new System.Xml.XmlElement();
            //e.Any.Add(any);
            //Add extension XML here with xs:Any and specified other namespace
            return e;
        }
        #endregion
        #region Data Types
        public HTML_Stype AddHTML(RichTextType rtf)
        {
            //throw new NotImplementedException();

            var html        = new HTML_Stype();
            rtf.HTML        = html;
            html.name       = "";
            html.type       = "";
            html.styleClass = "";

            html.Any        = new List<XmlElement>();

            var xhtml       = StringToXMLElement((string)dr["html"]);
            html.Any.Add(xhtml);
            //TODO: Check XHTML builder here: 
            //https://gist.github.com/rarous/3150395, 
            //http://www.authorcode.com/code-snippet-converting-xmlelement-to-xelement-and-xelement-to-xmlelement-in-vb-net/
            //https://msdn.microsoft.com/en-us/library/system.xml.linq.loadoptions%28v=vs.110%29.aspx
            return html;

        }

        public XmlElement StringToXMLElement(string rawXML)
        {
            var xe = XElement.Parse(rawXML, LoadOptions.PreserveWhitespace);
            var doc = new XmlDocument();

            var xmlReader = xe.CreateReader();
            doc.Load(xmlReader);
            xmlReader.Dispose();

            return doc.DocumentElement;
        }
        public WebServiceType AddWebService(LookupEndPointType lep)
        {
            throw new NotImplementedException();
        }
        public DataTypes_SType AddDataTypesS(CodingType coding)
        {
            throw new NotImplementedException();
        }

        public DataTypes_DEType AddDataTypesDE(ResponseFieldType RF)
        {
            var resp = new DataTypes_DEType();

            //var dt = new DataTypes_DEType
            //resp.DataTypeDE_Item= new  

            RF.Response = new DataTypes_DEType();
            AddBaseTypeItems(RF.Response.Item);

            var dataType = new ItemChoiceType();
            RF.Response.ItemElementName = dataType;

            string itemDataType = (string)dr["ItemDataType"];

            switch (itemDataType)
            {
                case "HTML":
                    dataType = ItemChoiceType.HTML;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new HTML_DEtype();
                        dt.Any = new List<XmlElement>();
                        dt.Any.Add((XmlElement)dr["xml"]);  //TODO:
                        dt.AnyAttr = new List<XmlAttribute>();
                        dt.AnyAttr.Add((XmlAttribute)dr["attribute"]);
                        dt.maxLength = (long)dr["maxLength"];
                        dt.minLength = (long)dr["minLength"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "XML":
                    dataType = ItemChoiceType.XML;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new XML_DEtype();
                        dt.Any = new List<XmlElement>();
                        dt.Any.Add((XmlElement)dr["xml"]);  //TODO: 
                        //dt.AnyAttr = new List<System.Xml.XmlAttribute>();
                        dt.maxLength = (long)dr["maxLength"];
                        dt.minLength = (long)dr["minLength"];
                        //dt.@namespace = (string)dr["namespace"];
                        dt.schema = (string)dr["schema"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "anyType":
                    dataType = ItemChoiceType.anyType;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new anyType_DEtype();
                        dt.Any = new List<XmlElement>();
                        dt.Any.Add((XmlElement)dr["xml"]);  //TODO: 
                        dt.AnyAttr = new List<XmlAttribute>();
                        dt.AnyAttr.Add((XmlAttribute)dr["attribute"]);
                        dt.maxLength = (long)dr["maxLength"];
                        dt.minLength = (long)dr["minLength"];
                        dt.@namespace = (string)dr["namespace"];
                        dt.schema = (string)dr["schema"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "anyURI":
                    dataType = ItemChoiceType.anyURI;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new anyURI_DEtype();
                        dt.val = (string)dr["val"];
                        dt.length = (long)dr["length"];
                        dt.maxLength = (long)dr["maxLength"];
                        dt.minLength = (long)dr["minLength"];
                        dt.pattern = (string)dr["pattern"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "base64Binary":
                    dataType = ItemChoiceType.base64Binary;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new base64Binary_DEtype();
                        dt.val = (byte[])dr["val"];
                        dt.valBase64 = (string)dr["val_string"];
                        dt.length = (long)dr["length"];
                        dt.maxLength = (long)dr["maxLength"];
                        dt.minLength = (long)dr["minLength"];
                        dt.mimeType = (string)dr["mimeType"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "boolean":
                    dataType = ItemChoiceType.boolean;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new boolean_DEtype();
                        dt.val = (bool)dr["val"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "byte":
                    dataType = ItemChoiceType.@byte;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new byte_DEtype();
                        dt.val = (sbyte)dr["val"];
                        dt.minExclusive = (sbyte)dr["minExclusive"];
                        dt.minInclusive = (sbyte)dr["minInclusive"];
                        dt.maxExclusive = (sbyte)dr["maxExclusive"];
                        dt.maxInclusive = (sbyte)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.totalDigits = (byte)dr["totalDigits"]; ;

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "date":
                    dataType = ItemChoiceType.date;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new date_DEtype();
                        dt.val = (DateTime)dr["val"];
                        dt.minExclusive = (DateTime)dr["minExclusive"];
                        dt.minInclusive = (DateTime)dr["minInclusive"];
                        dt.maxExclusive = (DateTime)dr["maxExclusive"];
                        dt.maxInclusive = (DateTime)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "dateTime":
                    dataType = ItemChoiceType.dateTime;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new dateTime_DEtype();
                        dt.val = (DateTime)dr["val"];
                        dt.minExclusive = (DateTime)dr["minExclusive"];
                        dt.minInclusive = (DateTime)dr["minInclusive"];
                        dt.maxExclusive = (DateTime)dr["maxExclusive"];
                        dt.maxInclusive = (DateTime)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "dateTimeStamp":
                    dataType = ItemChoiceType.dateTimeStamp;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new dateTimeStamp_DEtype();
                        dt.val = (DateTime)dr["val"];
                        dt.minExclusive = (DateTime)dr["minExclusive"];
                        dt.minInclusive = (DateTime)dr["minInclusive"];
                        dt.maxExclusive = (DateTime)dr["maxExclusive"];
                        dt.maxInclusive = (DateTime)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "decimal":
                    dataType = ItemChoiceType.@decimal;
                    dataType = ItemChoiceType.@float;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new decimal_DEtype();
                        dt.val = (decimal)dr["val"];
                        dt.minExclusive = (decimal)dr["minExclusive"];
                        dt.minInclusive = (decimal)dr["minInclusive"];
                        dt.maxExclusive = (decimal)dr["maxExclusive"];
                        dt.maxInclusive = (decimal)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.fractionDigits = (byte)dr["fractionDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "double":
                    dataType = ItemChoiceType.@double;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new double_DEtype();
                        dt.val = (double)dr["val"];
                        dt.minExclusive = (double)dr["minExclusive"];
                        dt.minInclusive = (double)dr["minInclusive"];
                        dt.maxExclusive = (double)dr["maxExclusive"];
                        dt.maxInclusive = (double)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.fractionDigits = (byte)dr["fractionDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "duration":
                    dataType = ItemChoiceType.duration;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new duration_DEtype();
                        dt.val = (System.TimeSpan)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (string)dr["minExclusive"];
                        dt.minInclusive = (string)dr["minInclusive"];
                        dt.maxExclusive = (string)dr["maxExclusive"];
                        dt.maxInclusive = (string)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "float":
                    dataType = ItemChoiceType.@float;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new float_DEtype();
                        dt.val = (float)dr["val"];
                        dt.minExclusive = (float)dr["minExclusive"];
                        dt.minInclusive = (float)dr["minInclusive"];
                        dt.maxExclusive = (float)dr["maxExclusive"];
                        dt.maxInclusive = (float)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.fractionDigits = (byte)dr["fractionDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gDay":
                    dataType = ItemChoiceType.gDay;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new gDay_DEtype();
                        dt.val = (DateTime)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (DateTime)dr["minExclusive"];
                        dt.minInclusive = (DateTime)dr["minInclusive"];
                        dt.maxExclusive = (DateTime)dr["maxExclusive"];
                        dt.maxInclusive = (DateTime)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gMonth":
                    dataType = ItemChoiceType.gMonth;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new gMonth_DEtype();
                        dt.val = (DateTime)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (DateTime)dr["minExclusive"];
                        dt.minInclusive = (DateTime)dr["minInclusive"];
                        dt.maxExclusive = (DateTime)dr["maxExclusive"];
                        dt.maxInclusive = (DateTime)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gMonthDay":
                    dataType = ItemChoiceType.gMonthDay;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new gMonthDay_DEtype();
                        dt.val = (DateTime)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (DateTime)dr["minExclusive"];
                        dt.minInclusive = (DateTime)dr["minInclusive"];
                        dt.maxExclusive = (DateTime)dr["maxExclusive"];
                        dt.maxInclusive = (DateTime)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gYear":
                    dataType = ItemChoiceType.gYear;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new gYear_DEtype();
                        dt.val = (DateTime)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (DateTime)dr["minExclusive"];
                        dt.minInclusive = (DateTime)dr["minInclusive"];
                        dt.maxExclusive = (DateTime)dr["maxExclusive"];
                        dt.maxInclusive = (DateTime)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gYearMonth":
                    dataType = ItemChoiceType.gYearMonth;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new gYearMonth_DEtype();
                        dt.val = (DateTime)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (DateTime)dr["minExclusive"];
                        dt.minInclusive = (DateTime)dr["minInclusive"];
                        dt.maxExclusive = (DateTime)dr["maxExclusive"];
                        dt.maxInclusive = (DateTime)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "hexBinary":
                    dataType = ItemChoiceType.hexBinary;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new hexBinary_DEtype();
                        dt.val = (byte[])dr["val"];
                        dt.valHex = (string)dr["val_string"];
                        dt.length = (long)dr["length"];
                        dt.maxLength = (long)dr["maxLength"];
                        dt.mimeType = (string)dr["mimeType"];
                        dt.minLength = (long)dr["minLength"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "int":
                    dataType = ItemChoiceType.@int;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new int_DEtype();
                        dt.val = (int)dr["val"];
                        dt.minExclusive = (int)dr["minExclusive"];
                        dt.minInclusive = (int)dr["minInclusive"];
                        dt.maxExclusive = (int)dr["maxExclusive"];
                        dt.maxInclusive = (int)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "integer":
                    dataType = ItemChoiceType.integer;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new integer_DEtype();
                        dt.val = (System.Nullable<decimal>)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (System.Nullable<decimal>)dr["minExclusive"];
                        dt.minInclusive = (System.Nullable<decimal>)dr["minInclusive"];
                        dt.maxExclusive = (System.Nullable<decimal>)dr["maxExclusive"];
                        dt.maxInclusive = (System.Nullable<decimal>)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "long":
                    dataType = ItemChoiceType.@long;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new long_DEtype();
                        dt.val = (long)dr["val"];
                        dt.minExclusive = (long)dr["minExclusive"];
                        dt.minInclusive = (long)dr["minInclusive"];
                        dt.maxExclusive = (long)dr["maxExclusive"];
                        dt.maxInclusive = (long)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "negativeInteger":
                    dataType = ItemChoiceType.negativeInteger;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new negativeInteger_DEtype();
                        dt.val = (System.Nullable<decimal>)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (System.Nullable<decimal>)dr["minExclusive"];
                        dt.minInclusive = (System.Nullable<decimal>)dr["minInclusive"];
                        dt.maxExclusive = (System.Nullable<decimal>)dr["maxExclusive"];
                        dt.maxInclusive = (System.Nullable<decimal>)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "nonNegativeInteger":
                    dataType = ItemChoiceType.nonNegativeInteger;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new nonNegativeInteger_DEtype();
                        dt.val = (System.Nullable<decimal>)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (System.Nullable<decimal>)dr["minExclusive"];
                        dt.minInclusive = (System.Nullable<decimal>)dr["minInclusive"];
                        dt.maxExclusive = (System.Nullable<decimal>)dr["maxExclusive"];
                        dt.maxInclusive = (System.Nullable<decimal>)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "nonPositiveInteger":
                    dataType = ItemChoiceType.nonPositiveInteger;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new nonPositiveInteger_DEtype();
                        dt.val = (System.Nullable<decimal>)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (System.Nullable<decimal>)dr["minExclusive"];
                        dt.minInclusive = (System.Nullable<decimal>)dr["minInclusive"];
                        dt.maxExclusive = (System.Nullable<decimal>)dr["maxExclusive"];
                        dt.maxInclusive = (System.Nullable<decimal>)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "positiveInteger":
                    dataType = ItemChoiceType.positiveInteger;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new positiveInteger_DEtype();
                        dt.val = (System.Nullable<decimal>)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (System.Nullable<decimal>)dr["minExclusive"];
                        dt.minInclusive = (System.Nullable<decimal>)dr["minInclusive"];
                        dt.maxExclusive = (System.Nullable<decimal>)dr["maxExclusive"];
                        dt.maxInclusive = (System.Nullable<decimal>)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "@short":
                    dataType = ItemChoiceType.@short;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new short_DEtype();
                        dt.val = (short)dr["val"];
                        dt.minExclusive = (short)dr["minExclusive"];
                        dt.minInclusive = (short)dr["minInclusive"];
                        dt.maxExclusive = (short)dr["maxExclusive"];
                        dt.maxInclusive = (short)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "string":
                    dataType = ItemChoiceType.@string;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new @string_DEtype();
                        dt.val = (string)dr["val"];
                        dt.maxLength = (long)dr["maxLength"];
                        dt.minLength = (long)dr["minLength"];
                        dt.pattern = (string)dr["pattern"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "time":
                    dataType = ItemChoiceType.time;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new time_DEtype();
                        dt.val = (DateTime)dr["val"];
                        dt.minExclusive = (DateTime)dr["minExclusive"];
                        dt.minInclusive = (DateTime)dr["minInclusive"];
                        dt.maxExclusive = (DateTime)dr["maxExclusive"];
                        dt.maxInclusive = (DateTime)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedByte":
                    dataType = ItemChoiceType.unsignedByte;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new unsignedByte_DEtype();
                        dt.val = (byte)dr["val"];
                        dt.minExclusive = (byte)dr["minExclusive"];
                        dt.minInclusive = (byte)dr["minInclusive"];
                        dt.maxExclusive = (byte)dr["maxExclusive"];
                        dt.maxInclusive = (byte)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedInt":
                    dataType = ItemChoiceType.unsignedInt;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new unsignedInt_DEtype();
                        dt.val = (uint)dr["val"];
                        dt.minExclusive = (uint)dr["minExclusive"];
                        dt.minInclusive = (uint)dr["minInclusive"];
                        dt.maxExclusive = (uint)dr["maxExclusive"];
                        dt.maxInclusive = (uint)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedLong":
                    dataType = ItemChoiceType.unsignedLong;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new unsignedLong_DEtype();
                        dt.val = (ulong)dr["val"];
                        dt.minExclusive = (ulong)dr["minExclusive"];
                        dt.minInclusive = (ulong)dr["minInclusive"];
                        dt.maxExclusive = (ulong)dr["maxExclusive"];
                        dt.maxInclusive = (ulong)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedShort":
                    dataType = ItemChoiceType.unsignedShort;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new unsignedShort_DEtype();
                        dt.val = (ushort)dr["val"];
                        dt.minExclusive = (ushort)dr["minExclusive"];
                        dt.minInclusive = (ushort)dr["minInclusive"];
                        dt.maxExclusive = (ushort)dr["maxExclusive"];
                        dt.maxInclusive = (ushort)dr["maxInclusive"];
                        dt.totalDigits = (byte)dr["totalDigits"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "yearMonthDuration":
                    dataType = ItemChoiceType.yearMonthDuration;
                    for (int i = 0; i < 1; i++)
                    {
                        var dt = new yearMonthDuration_DEtype();
                        dt.val = (TimeSpan)dr["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (TimeSpan)dr["minExclusive"];
                        dt.minInclusive = (TimeSpan)dr["minInclusive"];
                        dt.maxExclusive = (TimeSpan)dr["maxExclusive"];
                        dt.maxInclusive = (TimeSpan)dr["maxInclusive"];
                        dt.mask = (string)dr["mask"];
                        dt.quantEnum = AssignQuantifier(dr);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                default:
                    dataType = ItemChoiceType.@string;
                    {
                        var dt = new @string_DEtype();
                        dt.val = (string)dr["val"];
                        dt.maxLength = (long)dr["maxLength"];
                        dt.minLength = (long)dr["minLength"];
                        dt.pattern = (string)dr["pattern"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
            }

            //RF.Response.DataTypeDE_Item = value;
            return (DataTypes_DEType)RF.Response;

        }

        internal dtQuantEnum AssignQuantifier(DataRow dr)
        {
            var dtQE = new dtQuantEnum();
            var q = (string)dr["quantifier"];

            switch (q)
            {
                case "EQ":
                    dtQE = dtQuantEnum.EQ;
                    break;
                case "GT":
                    dtQE = dtQuantEnum.GT;
                    break;
                case "GTE":
                    dtQE = dtQuantEnum.GTE;
                    break;
                case "LT":
                    dtQE = dtQuantEnum.LT;
                    break;
                case "LTE":
                    dtQE = dtQuantEnum.LTE;
                    break;
                case "APPROX":
                    dtQE = dtQuantEnum.APPROX;
                    break;
                case "":
                    dtQE = dtQuantEnum.EQ;
                    break;
                case null:
                    dtQE = dtQuantEnum.EQ;
                    break;
                default:
                    dtQE = dtQuantEnum.EQ;
                    break;
            }
            return dtQE;
        }


        public DataTypes_DEType AddDataTypesDE(ListFieldType lft, QuestionItemBaseTypeResponseTypeEnum dataType)
        {   //DefaultListItemDataType

            throw new NotImplementedException();
        }



        #endregion



        #region Contact

        #region Add Contacts


        /// <summary>
        /// Add Contact to the Contacts type inside FileType
        /// </summary>
        /// <param name="cts"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public ContactType AddContact(FileType ft)
        {
            ContactsType c;
            if (ft.Contacts == null)
            {
                c = AddContactsListToFileType(ft);
            }
            else
            {
                c = ft.Contacts;
            }
            var ct = new ContactType();
            c.Contact.Add(ct);
            //TODO: Need to be able to add multiple people/orgs by reading the data source or ORM
            var p = AddPerson(ct);
            var org = AddOrganization(ct);

            return ct;
        }


        public ContactType AddContact(DisplayedType dt)
        {
            if (dt.Contact == null) dt.Contact = new List<ContactType>();
            var ct = new ContactType();
            dt.Contact.Add(ct);
            return ct;
        }


        /// <summary>
        /// Add a Contacts grouper to FileType; Contacts contains a List<ContactType/>
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public ContactsType AddContactsListToFileType(FileType ft)
        {
            if (ft.Contacts == null) ft.Contacts = new ContactsType();
            //List<ContactsType>();
            return ft.Contacts; //returns a .NET List<ContactType>

        }



        #endregion

        #region Address
        public AddressType AddAddress(PersonType pt)
        {
            throw new NotImplementedException();
        }
        public AddressType AddAddress(OrganizationType ot)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Person

        public PersonType AddPerson(ContactType contact)
        {
            if (contact == null) {contact = new ContactType();}

            var pt = new PersonType();
            contact.Item=pt;
            AddPersonItems(pt);

            return pt;
        }

        public PersonType AddPerson(DisplayedType dt)
        {
            List<ContactType> contactList;
            if (dt.Contact == null)
            {
                contactList = new List<ContactType>();
                dt.Contact = contactList;
            }
            else
            {contactList = dt.Contact;}
            var newContact = new ContactType();

            //var pt = new PersonType();
            //newContact.Item = pt;
            //contactList.Add(newContact);

            //AddPersonItems(pt);

            return AddPerson(newContact);
        }

        public PersonType AddContactPerson(OrganizationType ot)
        {
            if (ot.ContactPerson == null) ot.ContactPerson = new List<PersonType>();

            var pt = new PersonType();
            AddPersonItems(pt);
            ot.ContactPerson.Add(pt);
            return pt;
        }


        public PersonType AddPersonItems(PersonType pt)
        {
            pt.PersonName = new NameType();//TODO: Need separate method(s) for this
            //pt.Alias = new NameType();
            pt.PersonName.FirstName.val = (string)dr["FirstName"];
            pt.PersonName.LastName.val = (string)dr["LastName"];

            pt.Email = new List<EmailType>();//TODO: Need separate method(s) for this
            var email = new EmailType();//TODO: Need separate method(s) for this
            pt.Email.Add(email);

            pt.Phone = new List<PhoneType>();//TODO: Need separate method(s) for this

            pt.Job = new List<JobType>();//TODO: Need separate method(s) for this

            pt.Role = new string_Stype();

            pt.StreetAddress = new List<AddressType>();//TODO: Need separate method(s) for this

            pt.Identifier = new List<IdentifierType>();
            pt.Usage = new string_Stype();

            pt.WebURL = new List<anyURI_Stype>();//TODO: Need separate method(s) for this 

            return pt;
        }
        public NameType AddPersonName(PersonType pt)
        { throw new NotImplementedException(); }
        public JobType AddJob(PersonType pt)
        { throw new NotImplementedException(); }
        public EmailType AddEmail(OrganizationType ot)
        { throw new NotImplementedException(); }
        public EmailType AddEmail(PersonType pt)
        { throw new NotImplementedException(); }
        public PhoneType AddPhone(OrganizationType ot)
        { throw new NotImplementedException(); }
        public IdentifierType AddIdentifier(OrganizationType ot)
        { throw new NotImplementedException(); }
        public anyURI_Stype AddWebURL(OrganizationType ot)
        { throw new NotImplementedException(); }
        #endregion

        #region Organization

        public OrganizationType AddOrganization(ContactType contact)
        {
            var ot = new OrganizationType();
            contact.Item = ot;

            AddOrganizationItems(ot);
            return ot;
        }

        public OrganizationType AddOrganization(JobType job)
        {
            var ot = new OrganizationType();
            job.Organization = ot;
            AddOrganizationItems(ot);
            return ot;
        }
        public OrganizationType AddOrganizationItems(OrganizationType ot)
        {
            ot.OrgName = new string_Stype();//TODO: Need separate method(s) for this
            ot.OrgName.val = (string)dr["OrgName"];
            var p = AddContactPerson(ot);  //This function will add all the person details to teh Person list
            ot.Email = new List<EmailType>();//TODO: Need separate method(s) for this
            var email = new EmailType();//TODO: Need separate method(s) for this
            ot.Email.Add(email);
            ot.Phone = new List<PhoneType>();//TODO: Need separate method(s) for this
            var ph1 = new PhoneType();
            ot.Phone.Add(ph1);
            //var pt = new string_Stype();
            ph1.PhoneType1.val = (string)dr["PhoneType1"];

            var pn = new PhoneNumberType();
            ph1.PhoneNumber = pn;
            pn.val = (string)dr["PhoneNumber1"];
            //ot.Department = new string_Stype();
            ot.Department.val = (string)dr["Department1"];
            ot.Role = new List<string_Stype>();
            var r = new string_Stype();
            ot.Role.Add(r);
            r.val = (string)dr["OrgRole1"];
            var address1 = AddAddress(ot);
            address1.AddressLine = new List<string_Stype>();
            var al1 = new string_Stype();
            address1.AddressLine.Add(al1);
            ot.Identifier = new List<IdentifierType>();
            var id1 = new IdentifierType();
            ot.Identifier.Add(id1);
            id1.val = (string)dr["OrgID1"];
            //ot.Usage = new string_Stype();
            ot.Usage.val = (string)dr["Usage"];
            ot.WebURL = new List<anyURI_Stype>();//TODO: Need separate method(s) for this\
            var webURL = new anyURI_Stype();
            ot.WebURL.Add(webURL);
            webURL.val = (string)dr["WebURL"];
            return ot;
        }


        #endregion



        #endregion
        #region Common Types
        ////!Add Common Elements to Tree
        //private SectionItemType AddSection(SectionItemType sParent)
        //{
        //    var sNew = new SectionItemType();
        //    var childItemsList = AddChildItems(sParent);
        //    childItemsList.ListOfItems.Add(sNew);
        //    AddRepeatingTypeItems(sNew);
        //    //add ResponseReportingAttributes (for SubmitForm)
        //    return sNew;
        //}
        //private SectionItemType AddSection(QuestionItemType qParent)
        //{
        //    var sNew = new SectionItemType();
        //    var childItemsList = AddChildItems(qParent);
        //    childItemsList.ListOfItems.Add(sNew);
        //    AddRepeatingTypeItems(sNew);
        //    //add ResponseReportingAttributes (for SubmitForm)
        //    return sNew;
        //}
        //private SectionItemType AddSection(ListItemType liParent)
        //{
        //    var sNew = new SectionItemType();
        //    var childItemsList = AddChildItems(liParent);
        //    childItemsList.ListOfItems.Add(sNew);
        //    AddRepeatingTypeItems(sNew);
        //    //add ResponseReportingAttributes (for SubmitForm)
        //    return sNew;
        //}

        //private QuestionItemType AddQuestion(SectionItemType sParent)
        //{
        //    var qNew = new QuestionItemType();
        //    qNew.readOnly = (bool)dr["readonly"];
        //    var childItemsList = AddChildItems(sParent);
        //    childItemsList.ListOfItems.Add(qNew);

        //    AddRepeatingTypeItems(qNew);
        //    //If QuestionType = Qenum.QF vs QM/QS
        //    AddListFieldToQuestion(qNew);
        //    //else Qenum.QF
        //    AddResponseField(qNew);
        //    return qNew;
        //}
        //private QuestionItemType AddQuestion(ListItemType liParent)
        //{
        //    var qNew = new QuestionItemType();
        //    qNew.readOnly = (bool)dr["readonly"];
        //    var childItemsList = AddChildItems(liParent);
        //    childItemsList.ListOfItems.Add(qNew);

        //    AddRepeatingTypeItems(qNew);
        //    //If QuestionType = Qenum.QF vs QM/QS
        //    AddListFieldToQuestion(qNew);
        //    //else Qenum.QF
        //    AddResponseField(qNew);
        //    return qNew;
        //}
        //private QuestionItemType AddQuestion(QuestionItemType qParent)
        //{
        //    var qNew = new QuestionItemType();
        //    qNew.readOnly = (bool)dr["readonly"];
        //    var childItemsList = AddChildItems(qParent);
        //    childItemsList.ListOfItems.Add(qNew);

        //    AddRepeatingTypeItems(qNew);
        //    //If QuestionType = Qenum.QF vs QM/QS
        //    AddListFieldToQuestion(qNew);
        //    //else Qenum.QF
        //    AddResponseField(qNew);
        //    return qNew;
        //}
        //private DisplayedType AddDisplayedItem(QuestionItemType sParent)
        //{
        //    var dtNew = new DisplayedType();
        //    var childItemsList = AddChildItems(sParent);
        //    childItemsList.ListOfItems.Add(dtNew);
        //    AddDisplayedTypeItems(dtNew);
        //    return dtNew;
        //}


        //#region InjectedForms
        ////!+InjectedForms
        //internal InjectFormType AddInjectedFormItems(InjectFormType injectedForm)
        //{
        //    //TODO:...need overloaded methods so we can add Injected forms to childItems from Sections, questions, ListItems...
        //    //var injForm = new InjectFormType();
        //    //childItems.ListOfItems.Add(injForm);  //TODO:need to first instantiate the List!

        //    //iform.injectionID = a0;  needs to be string format??
        //    injectedForm.packageID = (string)dr["packageID"];
        //    injectedForm.rootItemID = (string)dr["rootItemID"];
        //    injectedForm.baseURI = (string)dr["baseURI"];
        //    injectedForm.packageID = (string)dr["packageID"];
        //    injectedForm.ID = (string)dr["ID"];


        //    //response properties
        //    injectedForm.formInstanceURI = (string)dr["formInstanceURI"];
        //    injectedForm.formInstanceVersionURI = (string)dr["formInstanceVersionURI"];
        //    injectedForm.injectionID = (string)dr["injectionID"];
        //    injectedForm.formInstanceURI = (string)dr["baseURI"];


        //    AddExtensionBaseTypeItems(injectedForm);

        //    return injectedForm;
        //}

        //internal InjectFormType AddInjectedForm(QuestionItemType q)
        //{
        //    var injForm = new InjectFormType();
        //    var childItems = AddChildItems(q);
        //    childItems.ListOfItems.Add(injForm);  //TODO:need to first instantiate the List!
        //    AddInjectedFormItems(injForm);

        //    return injForm;
        //}
        //internal InjectFormType AddInjectedForm(SectionItemType s)
        //{
        //    var injForm = new InjectFormType();
        //    var childItems = AddChildItems(s);
        //    childItems.ListOfItems.Add(injForm);  //TODO:need to first instantiate the List!
        //    AddInjectedFormItems(injForm);

        //    return injForm;
        //}
        #endregion

        #region ChildItems

        #region Generics

        public DisplayedType AddDisplayedItem<T>(T T_Parent) where T : DisplayedType, IChildItems, new()
        {
            var dNew = new DisplayedType();

            var childItemsList = AddChildItems(T_Parent);
            childItemsList.ListOfItems.Add(dNew);
            AddDisplayedTypeItems(dNew);
            return dNew;
        }
        public SectionItemType AddSection<T>(T T_Parent) where T : DisplayedType, IChildItems, new()
        {
            var sNew = new SectionItemType();
            var childItemsList = AddChildItems(T_Parent);
            childItemsList.ListOfItems.Add(sNew);
            AddRepeatingTypeItems(sNew);
            //add ResponseReportingAttributes (for SubmitForm)
            return sNew;
        }
        public QuestionItemType AddQuestion<T>(T T_Parent, ItemType qType) where T : DisplayedType, IChildItems, new()
        {
            var qNew = new QuestionItemType();
            qNew.readOnly = (bool)dr["readonly"];
            var childItemsList = AddChildItems(T_Parent);
            childItemsList.ListOfItems.Add(qNew);

            switch (qType)
            {
                case ItemType.QUESTIONSINGLE:
                    AddListFieldToQuestion(qNew);
                    break;
                case ItemType.QUESTIONMULTIPLE:
                    AddListFieldToQuestion(qNew);
                    break;
                case ItemType.QUESTIONFILLIN:
                    AddResponseField(qNew);
                    break;
                case ItemType.QUESTION_LOOKUP:
                    AddListFieldToQuestion(qNew);
                    break;
                default:
                    break;
            }
            AddRepeatingTypeItems(qNew);

            return qNew;
        }
        public InjectFormType AddInjectedForm<T>(T T_Parent) where T : DisplayedType, IChildItems, new()
        {
            var childItems = AddChildItems(T_Parent);
            var injForm = new InjectFormType();
            childItems.ListOfItems.Add(injForm);  //TODO:need to first instantiate the List!
            //reeBuilder.AddInjectedFormItems(injForm);

            //iform.injectionID = a0;  needs to be string format??
            injForm.packageID = (string)dr["packageID"];
            injForm.rootItemID = (string)dr["rootItemID"];
            injForm.baseURI = (string)dr["baseURI"];
            injForm.packageID = (string)dr["packageID"];
            injForm.ID = (string)dr["ID"];


            //response properties
            injForm.formInstanceURI = (string)dr["formInstanceURI"];
            injForm.formInstanceVersionURI = (string)dr["formInstanceVersionURI"];
            injForm.injectionID = (string)dr["injectionID"];
            injForm.formInstanceURI = (string)dr["baseURI"];


            AddExtensionBaseTypeItems(injForm);


            return injForm;
        }

        public ButtonItemType AddButtonAction<T>(T T_Parent) where T : DisplayedType, IChildItems, new()
        {
            var btnNew = new ButtonItemType();
            var childItemsList = AddChildItems(T_Parent);
            childItemsList.ListOfItems.Add(btnNew);
            AddDisplayedTypeItems(btnNew);
            // TODO: Add reeBuilder.AddButtonActionTypeItems(btnNew);
            return btnNew;
        }
        public ChildItemsType AddChildItems<T>(T T_Parent) where T : DisplayedType, IChildItems, new()
        {
            ChildItemsType childItems;
            if (T_Parent == null)
            { childItems = new ChildItemsType(); }
            else { childItems = T_Parent.ChildItems_Item; }

            T_Parent.ChildItems_Item = childItems;
            childItems.ListOfItems = new List<IdentifiedExtensionType>();
            return childItems;
        }
        #endregion
        #endregion


        #region Displayed Type
        public RichTextType AddOtherText(DisplayedType dt)
        {
            var richText = new RichTextType();
            
            if (dt.OtherText == null) dt.OtherText = new List<RichTextType>();
            dt.OtherText.Add(richText);

            var html = AddHTML(richText);

            return richText;
        }
        public LinkType AddLink(DisplayedType dt)
        {
            var link = new LinkType();

            if (dt.Link == null) dt.Link = new List<LinkType>();
            dt.Link.Add(link);

            var rtf = new RichTextType();
            link.LinkText = rtf;

            rtf.val = (string)dr["LinkText"];
            var html = AddHTML(rtf);  //check this

            link.LinkURI = new anyURI_Stype();
            link.LinkURI.val = (string)dr["LinkURI"];

            var desc = new RichTextType();
            if (link.Description == null) link.Description = new List<RichTextType>();
            link.Description.Add(desc);
            desc.val = (string)dr["LinkDescText"];
            //AddHTML(desc).Any.Add(HTML);
            //Fill the description text here

            //....

            AddExtensionBaseTypeItems(dt);

            return link;

            //LinkText: HTML Type
            //LinkURI: URI Type: ExtensionBase Type
            //Description: HTML Type
        }
        public BlobType AddBlob(DisplayedType dt)
        {
            var blob = new BlobType();
            dt.BlobContent.Add(blob);

            var rtf = new RichTextType();
            rtf.val = (string)dr["BlobText"];
            var html = AddHTML(rtf);

            blob.Description = new List<RichTextType>();
            blob.Description.Add(rtf);
            blob.Description.Add(rtf);

            var bUri = new anyURI_Stype();
            bUri.val = "https://www.cap.org/ecc/sdc/image1234.jpg";
            blob.Item = bUri;
            var bin = new base64Binary_DEtype();
            bin.valBase64 = "SGVsbG8=";
            //TODO: error in code generator for base64Binary_Stype - val should be string datatype, not a byte array.


            return blob;
            //Description: RichTextType
            //Hash
            //BobURI
            //BinaryMediaBase63
        }


        public IfThenType AddOnEvent(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public IfThenType AddOnEnter(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public OnEventType AddOnExit(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        //private IfThenType AddOnEnter(DisplayedType dt)
        //{
        //    throw new NotImplementedException();
        //}
        public WatchedPropertyType AddActivateIf(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public WatchedPropertyType AddDeActivateIf(DisplayedType dt)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Codes
        //!+CodedValues


        public UnitsType AddUnits(CodingType cf)
        {
            throw new NotImplementedException();
            var u = new UnitsType(); //AddUnits<CodingType>(new CodingType());
            cf.Units = u;
            return u;
        }
        public CodeMatchType AddCodeMatch(CodingType cf)
        {
            throw new NotImplementedException();
            var cm = new CodeMatchType();
            cf.CodeMatch = cm;
            return cm;
        }
        public RichTextType AddCodeText(CodingType cf)
        {
            throw new NotImplementedException();
            var u = new RichTextType();
            return u;
        }

        public CodeSystemType AddCodeSystem(CodingType code)
        {
            var cs = new CodeSystemType();
            code.CodeSystem = cs;
            AddCodeSystemItems(cs);
            return cs;
        }

        public CodeSystemType AddCodeSystem(ListFieldType lf)
        {
            var cs = new CodeSystemType();
            lf.DefaultCodeSystem = cs;
            AddCodeSystemItems(cs);
            return cs;
        }
        public CodeSystemType AddCodeSystemItems(CodeSystemType cs)
        {
            cs.CodeSystemName.val = (string)dr["CodeSystemName"];
            cs.CodeSystemURI.val = (string)dr["CodeSystemURI"];
            cs.OID.val = (string)dr["CodeSystemOID"];
            cs.ReleaseDate.val = (DateTime)dr["CodeSystemReleaseDate"];
            cs.Version.val = (string)dr["CodeSystemVersion"];

            AddExtensionBaseTypeItems(cs);
            return (CodeSystemType)cs;
        }


        public CodingType AddCodedValue(DisplayedType dt)
        {
            var coding = new CodingType();

            var codingList = AddCodingList(dt);
            codingList.Add(coding);

            coding.CodeMatch = (CodeMatchType)dr["CodeMatch"];  //this will need work for enums
            coding.Code.val = (string)dr["Code"];
            var richText = new RichTextType();
            richText.val = (string)dr["CodeText"];
            AddHTML(richText);  //create AddHTML method to RichTextType partial class

            coding.CodeText = richText;

            AddCodeSystem(coding);

            return coding;
        }

        /// <summary>
        /// Handles Response derived from a LookupEndpoint
        /// </summary>
        /// <param name="lep">LookupEndPointType</param>
        /// <param name="dr">DataRow</param>
        /// <returns></returns>
        public CodingType AddCodedValue(LookupEndPointType lep)
        {
            var coding = new CodingType();

            var codingList = AddCodingList(lep);
            codingList.Add(coding);

            coding.CodeMatch = (CodeMatchType)dr["CodeMatch"];  //this will need work for enums
            coding.Code.val = (string)dr["Code"];
            var richText = new RichTextType();
            richText.val = (string)dr["CodeText"];
            AddHTML(richText);  //create AddHTML method to RichTextType partial class

            coding.CodeText = richText;

            AddCodeSystem(coding);

            return coding;
        }

        public List<CodingType> AddCodingList(DisplayedType dt)
        {
            List<CodingType> lct;

            if (dt.CodedValue == null)
            {
                lct = new List<CodingType>();
                dt.CodedValue = lct;
            }
            else { lct = dt.CodedValue; }

            return lct;
        }

        public List<CodingType> AddCodingList(LookupEndPointType ep)
        {
            List<CodingType> lct;

            if (ep.ResponseValue == null)
            {
                lct = new List<CodingType>();
                ep.ResponseValue = lct;
            }
            else { lct = ep.ResponseValue; }

            return lct;
        }


        #endregion
        #region IfThen
        public void AddActionToThenType(ThenType thenType)
        {
            AddExtensionBaseTypeItems(thenType);
            //Add @onlyIf
            //Add PropertyAction
            //Add If LIstItemStatus
            //Add IfPredicate
            //Add IfGroup
        }


        #endregion

        #region QAS


        #region List
        //!+List
        public ListFieldType AddListFieldToQuestion(QuestionItemType q)
        {
            var listField = new ListFieldType();
            q.ListField_Item = listField;

            return listField;
        }
        public ListType AddListToListField(ListFieldType listField)
        {

            ListType list;  //this is not the .NET List class; It's an answer list
            //List<DisplayedType> listDT;

            if (listField.List_Item == null)
            {
                list = new ListType();
                listField.List_Item = list;
            }
            else
            {
                list = listField.List_Item;
            }


            //The "list" item contains a list<DisplayedType>, to which the list items are added.
            if (list.DisplayedItem_List == null)
            {
                //listDT = new List<DisplayedType>();
                //list.DisplayedItem_List = listDT;
                list.DisplayedItem_List = new List<DisplayedType>();
            }


            return (ListType)list;
        }
        public void AddListItemProperties(ListItemType li)
        {
            //throw new NotImplementedException();
            li.title = (string)dr["VisibleText"];
            li.omitWhenSelected = (bool)dr["omitWhenSelected"];
            li.selected = (bool)dr["selected"];
            li.selectionDeselectsSiblings = (bool)dr["selectionDeselectsSiblings"];
            li.selectionDisablesChildren = (bool)dr["selectionDisablesChildren"];
            AddDisplayedTypeItems(li);
            AddListItemResponseField(li);
            //li.OnDeselect;
            //li.OnSelect;
            //li.DeselectIf;
            //li.SelectIf;
            //li.ActivateIf;
            //li.DeActivateIf;



        }
        public DisplayedType AddListMemberToQuestion(QuestionItemType qNode)
        {
            var listField = AddListFieldToQuestion(qNode);
            var list = AddListToListField(qNode.ListField_Item);
            //var itemList = list.DisplayedItem_List;

            ListItemType li;
            DisplayedType di;

            //!+Choose one of the following as the first Item in LIst
            var type = (int)(dr["AnswerItemTypeKey"]);
            if (type == 1)
            {
                li = AddListItemToList(list);
                return (DisplayedType)(DisplayedType)(ListItemType)li;
            }
            else if (type == 2)
            {
                di = AddDisplayedItemToList(list);
                return di;
            }
            else return null;

        }
        public ListItemType AddListItemToList(ListType list)
        {            //test
            //var lstIfThen = new List<IfThenType>();
            //lstIfThen.Add(new IfThenType());

            //var li = new ListItemType();
            //li.OnDeselect = lstIfThen;


            var li = new ListItemType();
            list.DisplayedItem_List.Add(li);
            AddListItemProperties(li);

            AddListItemResponseField(li);
            //addDisplayedTypeToListItem(li);
            return li;
        }
        public DisplayedType AddDisplayedItemToList(ListType list)
        {
            var di = new DisplayedType();
            list.DisplayedItem_List.Add(di);
            AddDisplayedTypeItems(di);

            return di;
        }
        #endregion


        #region Response
        //!+Create Response Items
        public ResponseFieldType AddResponseField(QuestionItemType q)
        {  //Add Response, TextAfterResponse (RichTextType), ReponseUnits, SetValueExpression
            var rf = new ResponseFieldType();
            rf.ResponseUnits.val = (string)dr["AnswerUnits"];
            rf.TextAfterResponse.val = (string)dr["TextAfterResponse"];
            q.ResponseField_Item = rf;

            AddResponseItems(rf);
            AddExtensionBaseTypeItems(rf);


            return rf;
        }

        public ListItemResponseFieldType AddListItemResponseField(ListItemType li)
        {  //Add Response, TextAfterResponse (RichTextType), ReponseUnits, SetValueExpression
            var liRF = new ListItemResponseFieldType();
            liRF.ResponseUnits.val = (string)dr["AnswerUnits"];
            liRF.TextAfterResponse.val = (string)dr["TextAfterResponse"];
            liRF.responseRequired = (bool)dr["responseRequired"];
            li.ListItemResponseField = liRF;

            AddResponseItems(liRF);
            AddExtensionBaseTypeItems(liRF);


            return liRF;
        }

        public ResponseFieldType AddResponseItems(ResponseFieldType rf)
        {
            var stringDE = new string_DEtype();
            rf.Response.Item = stringDE;
            rf.Response.ItemElementName = ItemChoiceType.@string; //why is this needed?
            return (ResponseFieldType)rf;

        }

        public BaseType AddDataTypeToResponseType(ResponseFieldType dd)
        {
           throw new NotImplementedException();
        }
        public void AddResponseHistoryToQuestion(QuestionItemType q)
        {
            AddReplacedResponseToResponseHistory(q.ResponseHistory);
            throw new NotImplementedException();
        }
        public void AddReplacedResponseToResponseHistory(List<ResponseChangeType> respChange)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LookupEndpoint
        //!Add LookupEndpoint to Question w ListField
        public LookupEndPointType AddEndpointToQuestion(DisplayedType nodeToAdd, DisplayedType parentNode)
        {
            //Add ResponseComment, Editor, DateTime, Response, SelectedItems
            return new LookupEndPointType();
        }
        #endregion

        #endregion
        #region Rules
        //!+Rules
        public RulesType AddRuleToDisplayedType(DisplayedType parent)
        {
            //FormAction
            //PropertyAction, 
            //If ListItemStatus, 
            //If Predicate, 
            //IfGroup
            return new RulesType();
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Converts the string expression of an enum value to the desired type. Example: var qType= reeBuilder.ConvertStringToEnum&lt;ItemType&gt;("answer");
        /// </summary>
        /// <typeparam name="Tenum">The enum type that the inputString will be converted into.</typeparam>
        /// <param name="inputString">The string that must represent one of the Tenum enumerated values; not case sensitive</param>
        /// <returns></returns>
        public Tenum ConvertStringToEnum<Tenum>(string inputString) where Tenum : struct
        {
            //T newEnum = (T)Enum.Parse(typeof(T), inputString, true);

            Tenum newEnum;
            if (Enum.TryParse<Tenum>(inputString, true, out newEnum))
            {
                return newEnum;
            }
            else
            {

                //throw new Exception("Failure to create enum");
            }
            return newEnum;
        }
        #endregion

        #region Actions

        public ActSendMessageType AddActSendMessage(ThenType tt)
        {
            var asmt = new ActSendMessageType();
            if (tt.Items != null) tt.Items = new List<SDC.BaseType>();
            tt.Items.Add(asmt);
            var html = new SDC.HTML_DEtype();
            asmt.HTML = html;
            //var DataRepo.dr = DataRepo.dr;
            asmt.val = (string)DataRepo.dr["ActSendMessage"];
            if (html.Any == null) html.Any = new List<XmlElement>();
            html.Any.Add(StringToXMLElement((string)DataRepo.dr["ActSendMessageHTML"]));
            return asmt;

        }
        #endregion
        #region Resources

        
        public UnitsType AddUnits<T>(T t) where T:ExtensionBaseType
        {
            throw new NotImplementedException();
            var u = new UnitsType();
            u.val = (string)dr["units"];
            return u;
        }
        public UnitsType AddUnits(ResponseFieldType rf)
        {
            throw new NotImplementedException();
            var u = new UnitsType();
            return u;
        }
        private RichTextType AddRichText<T>(T t, string strHTML = "", string val = "") where T:BaseType
        {
            throw new NotImplementedException();
            var rtf = new RichTextType();
            var html = AddHTML(rtf);

            return rtf;
        }

        #endregion

    }

}



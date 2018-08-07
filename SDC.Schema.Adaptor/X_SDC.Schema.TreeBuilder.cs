using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using My = Microsoft.VisualBasic;
using SDC.DAL.DataSets;
//using 

namespace SDC
{

    public abstract class SDCTreeBuilder : ITreeBuilder
    {

        protected DataTable dtFormDesign { get; set; }
        protected DataTable dtHeaderDesign { get; set; }
        protected DataRow drFormDesign { get; set; }

        //!+raise event when form is created, so it can be serialized and/or transformed
        public decimal FormDesignID { get; set; } //CTV_Ckey
        public string XsltFileName { get; set; }
        private SectionItemType _Header;
        private SectionItemType _Body;
        private SectionItemType _Footer;

        public FormDesignType FormDesign { get; set; }
        protected SectionItemType Header { get { return _Header; } }
        protected SectionItemType Body { get { return _Body; } }
        protected SectionItemType Footer { get { return _Footer; } }

        public SDCTreeBuilder() //treeBuilder is the class that subclasses this abstract class
        //!+ We can initialize dtFormDesign here - can pass initializer into the constructor, also can pass in FormDesignID
        {
            //Can (partially) initialize FormDesign data source here, using overloaded constructor in subclass
            //Initialize FormDesign, FormDesignID, xslt

        }

        #region  Template Builder
        protected abstract ItemType GetRowType(string strRowType);
        /// <summary>
        /// //make sure FormDesign has injected and primed ITreeBuilder and IFormDesignDataSet objects
        /// </summary>
        /// <returns></returns>
        public virtual FormDesignType CreateFormDesignTree()
        {
            //var fd = new FormDesignType();

            var rowType = new ItemType();
            
            //rowType = ItemType.HEADER;
            //rowType = ConvertStringToEnum<ItemType>(drFormDesign["ItemType"].ToString());

            //string fdID = CTVcKey;
            IdentifiedExtensionType newNode;
            IdentifiedExtensionType parentNode;
            IdentifiedExtensionType prevSibNode;

            FormDesign.ID = FormDesignID.ToString();
            FormDesign.baseURI = "www.cap.org/eCC/SDC/IHE";

            //Add a Body node, whether we have an explict node for it or not.
            //If we find one later, we can overwrite teh ID and other metadata
            parentNode = FormDesign.AddBody();
            newNode = FormDesign.Body;

            FormDesign.Body.ID = FormDesignID + "_DefaultBody_" + DateTime.Now.ToString();

            //move to next row
            //get row type
            //cast parentNode to row type
            //cast newNode to RowType
            //cast nextNode to Row type
            //Add newNode to appropriate parent node - either previous sib or parent

            for (int i = 0; i < dtFormDesign.Rows.Count - 1; i++)
            {
                rowType = GetRowType(drFormDesign["ItemType"].ToString());
                //rowType = ConvertStringToEnum<ItemType>(drFormDesign["ItemType"].ToString());

                if (i == 0) //Look for Body section.  If not present, add it here.
                {
                    if ((string)drFormDesign["ItemType"] != ItemType.Section.ToString() ||
                        !((string)drFormDesign["type"]).ToLower().Contains("body"))
                    {


                    }
                }
                switch (rowType)
                {
                    case ItemType.Template:
                        break;
                    case ItemType.ListItem:
                        break;
                    case ItemType.ListItemFill:
                        break;
                    case ItemType.QuestionSingle:
                        break;
                    case ItemType.QuestionMultiple:
                        break;
                    case ItemType.QuestionFill:
                        break;
                    case ItemType.QuestionLookup:
                        break;
                    case ItemType.Section:
                        if (rowType == ItemType.Section)
                        {
                            if (newNode.type == "Header" && FormDesign.Header == null)
                            { FormDesign.AddHeader(); }
                            if (newNode.type == "Footer" && FormDesign.Footer == null)
                            { FormDesign.AddFooter(); }
                            if (newNode.type == "Body")  //Body was added at top of proc.  If it is present explicitly, then fill it with metadata
                            { FormDesign.Body.Fill_SectionBaseType(); }
                        }
                        newNode = ((IChildItems)parentNode).AddSection();
                        var section = (SectionItemType)newNode;
                        //section.ParentRecordNode = "1";
                        //get row type
                        break;
                    case ItemType.Note:
                        break;
                    case ItemType.FixedListNote:
                        break;
                    case ItemType.Rule:
                        break;
                    case ItemType.InjectedTemplate:
                        break;
                }
            }
            return FormDesign;
        }

        public virtual void AddTemplateMetadataToForm(IdentifiedExtensionType n, DisplayedType parentNode)
        {   //Uses CTV data
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets template by Ckey
        /// </summary>
        /// <param name="formDesignID">Ckey of template to get</param>
        /// <param name="BESTfilename">The preferred file name.  Use USERfilename in preference to the Database supplied filename</param>
        /// <param name="USERfilename">The preferred filename, if available</param>
        /// <value>retirns FormDesign XML or "" if an error occurs</value>
        /// <returns>Template XML</returns>
        public virtual String GetTemplateByCkey(String formDesignID, out String BESTfilename, String USERfilename)
        {
            Decimal decFormDesignID = 0;
            if (formDesignID.Trim() != "" && Decimal.TryParse(formDesignID, out decFormDesignID))
            {
                if (decFormDesignID > 0)
                {
                    FormDesignID = decFormDesignID;
                    if (USERfilename == "")
                    { USERfilename = "_file_" + DateTime.Now.Ticks.ToString().Trim(); }   //create a dummy filename and insert it into the dataset, so it's also added to the XML 

                    BESTfilename = USERfilename;
                    this.CreateFormDesignTree(); //make sure FormDesign has an injected ITreeBuilder, and an injected IFormDesignDataSet 

                    return SerializeFormDesignTree();
                }
            }
            BESTfilename = "error";
            return String.Empty;
        }

        /// <summary>
        /// Creates template with information from data tables 
        /// </summary>
        /// <param name="templateVersion">Data table with information about the template version</param>
        /// <param name="templateItems">Data table with information about the template items</param>
        /// <param name="templateProcedures">Data table with information about the template procedures</param>
        /// <returns>Template Xml</returns>
        public virtual String SerializeFormDesignTree()
        //SerializeProcessFDxml 
        {
            if (FormDesign != null)
            {
                //var ns = new XmlSerializerNamespaces();
                //rlm: next line directs the format of the serialized XML, according to the schema-generated template
                //var serializer = new XmlSerializer(typeof(fd));
                //var writer = new StringWriterEncoding(Encoding.UTF8);
                //serializer.Serialize(writer, cl, ns);
                //String templateXml = writer.ToString();

                String formDesignXml = FormDesign.Serialize();
                if (this.XsltFileName != String.Empty)
                {
                    //rlm: add xsl transform directive; 
                    var replaceTxt = "encoding=\"UTF-8\"?>";
                    formDesignXml = formDesignXml.Replace(replaceTxt,
                        string.Format(replaceTxt +
                        "/r/n<?xml-stylesheet type=\"text/xsl\" href=\"{0}\" ?>/r/n",
                        XsltFileName));
                }

                // entity strings being passed thru by the serializer
                // specifically apos and quot, need to be fixed
                formDesignXml = formDesignXml.Replace("&amp;quot", "&quot");
                formDesignXml = formDesignXml.Replace("&amp;apos", "&apos");
                return formDesignXml;
            }
            return String.Empty;
        }



        #endregion
        #region  FormDesign Creation

        public SectionBaseType AddSectionItems(SectionBaseType s)
        {
            s.ordered = (bool)drFormDesign["ordered"];
            AddRepeatingTypeItems(s);
            return s;
        }

        #endregion


        #region Base Types
        //!Base type functions
        public virtual RepeatingType AddRepeatingTypeItems(RepeatingType rt)
        {
            //var dr = this.drFormDesign();

            rt.maxCard = (ushort)drFormDesign["maxCard"];  //TODO: replace with real data
            rt.minCard = (ushort)drFormDesign["minCard"];  //TODO: replace with real data

            AddDisplayedTypeItems((RepeatingType)rt);
            return (RepeatingType)rt;
        }
        public virtual DisplayedType AddDisplayedTypeItems(DisplayedType dt)
        {
            //var dr = this.drFormDesign();

            dt.enabled = (bool)drFormDesign["enabled"];  //TODO: replace with real data
            dt.visible = (bool)drFormDesign["visible"];  //TODO: replace with real data
            dt.title = (string)drFormDesign["VisibleText"];  //TODO: replace with real data
            dt.mustImplement = (bool)drFormDesign["mustImplement"];  //TODO: replace with real data
            dt.showInReport = (bool)drFormDesign["showInReport"];  //TODO: replace with real data

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
        public virtual IdentifiedExtensionType AddIdentifiedTypeItems(IdentifiedExtensionType iet)
        {
            //var dr = this.drFormDesign();

            iet.baseURI = (string)drFormDesign["baseURI"];  //TODO: replace with real data
            iet.ID = (string)drFormDesign["ChecklistTemplateItemCKey"];  //TODO: replace with real data
            iet.ParentID = (string)drFormDesign["ParentItemCKey"];  //TODO: replace with real data
            //iet.ParentRecordNode = ParentNodes.Find();


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
        public virtual IdentifiedExtensionType AddBaseTypeItems(IdentifiedExtensionType bt)
        {
            //var dr = this.drFormDesign();

            bt.ParentID = (string)drFormDesign["ParentItemCkey"];  //TODO: replace with real data
            bt.name = (string)drFormDesign["shortName"];   //TODO: replace with real data// could move this to IdentifiedExtensionType
            bt.type = (string)drFormDesign["type"];  //TODO: replace with real data
            bt.styleClass = (string)drFormDesign["styleClass"];  //TODO: replace with real data
            bt.order = (decimal)drFormDesign["sortorder"];  //TODO: replace with real data
            return (IdentifiedExtensionType)bt;
        }
        public virtual CommentType AddComment(ExtensionBaseType ebt)
        {
            //var dr = this.drFormDesign();
            var c = new CommentType();

            if (ebt.Comment == null) ebt.Comment = new List<CommentType>();
            ebt.Comment.Add(c);
            c.val = (string)drFormDesign["Comment"];  //TODO: replace with real data
            //AddBaseTypeItems(ebt);
            return c;
        }
        public virtual ExtensionType AddExtension(ExtensionBaseType ebt)
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
        public virtual HTML_Stype AddHTML(RichTextType rtf)
        {
            //throw new NotImplementedException();
            //var dr = this.drFormDesign();

            var html = new HTML_Stype();
            rtf.HTML = html;
            html.name = "";  //TODO: replace with real data
            html.type = "";  //TODO: replace with real data
            html.styleClass = "";  //TODO: replace with real data

            html.Any = new List<XmlElement>();

            var xhtml = StringToXMLElement((string)drFormDesign["html"]);  //TODO: replace with real data
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
        public virtual WebServiceType AddWebService(LookupEndPointType lep)
        {
            throw new NotImplementedException();
        }
        public virtual DataTypes_SType AddDataTypesS(CodingType coding)
        {
            throw new NotImplementedException();
        }

        public virtual DataTypes_DEType AddDataTypesDE(ResponseFieldType RF)
        {
            var resp = new DataTypes_DEType();

            //var dt = new DataTypes_DEType
            //resp.DataTypeDE_Item= new  

            RF.Response = new DataTypes_DEType();
            AddBaseTypeItems(RF.Response.Item);

            var dataType = new ItemChoiceType();
            RF.Response.ItemElementName = dataType;
            //var dr = this.drFormDesign();

            string itemDataType = (string)drFormDesign["ItemDataType"];  //TODO: replace with real data

            switch (itemDataType)
            {
                case "HTML":
                    dataType = ItemChoiceType.HTML;
                    if (true)
                    {
                        var dt = new HTML_DEtype();
                        dt.Any = new List<XmlElement>();
                        dt.Any.Add((XmlElement)drFormDesign["xml"]);  //TODO: replace with real data
                        dt.AnyAttr = new List<XmlAttribute>();
                        dt.AnyAttr.Add((XmlAttribute)drFormDesign["attribute"]);  //TODO: replace with real data
                        dt.maxLength = (long)drFormDesign["maxLength"];  //TODO: replace with real data
                        dt.minLength = (long)drFormDesign["minLength"];  //TODO: replace with real data

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "XML":
                    dataType = ItemChoiceType.XML;
                    if (true)
                    {
                        var dt = new XML_DEtype();
                        dt.Any = new List<XmlElement>();
                        dt.Any.Add((XmlElement)drFormDesign["xml"]);  //TODO:   //TODO: replace with real data
                        //dt.AnyAttr = new List<System.Xml.XmlAttribute>();
                        dt.maxLength = (long)drFormDesign["maxLength"];  //TODO: replace with real data
                        dt.minLength = (long)drFormDesign["minLength"];  //TODO: replace with real data
                        //dt.@namespace = (string)dr["namespace"];
                        dt.schema = (string)drFormDesign["schema"];  //TODO: replace with real data

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "anyType":
                    dataType = ItemChoiceType.anyType;
                    if (true)
                    {
                        var dt = new anyType_DEtype();
                        dt.Any = new List<XmlElement>();
                        dt.Any.Add((XmlElement)drFormDesign["xml"]);  //TODO: 
                        dt.AnyAttr = new List<XmlAttribute>();
                        dt.AnyAttr.Add((XmlAttribute)drFormDesign["attribute"]);
                        dt.maxLength = (long)drFormDesign["maxLength"];
                        dt.minLength = (long)drFormDesign["minLength"];
                        dt.@namespace = (string)drFormDesign["namespace"];
                        dt.schema = (string)drFormDesign["schema"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "anyURI":
                    dataType = ItemChoiceType.anyURI;
                    if (true)
                    {
                        var dt = new anyURI_DEtype();
                        dt.val = (string)drFormDesign["val"];
                        dt.length = (long)drFormDesign["length"];
                        dt.maxLength = (long)drFormDesign["maxLength"];
                        dt.minLength = (long)drFormDesign["minLength"];
                        dt.pattern = (string)drFormDesign["pattern"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "base64Binary":
                    dataType = ItemChoiceType.base64Binary;
                    if (true)
                    {
                        var dt = new base64Binary_DEtype();
                        dt.val = (byte[])drFormDesign["val"];
                        dt.valBase64 = (string)drFormDesign["val_string"];
                        dt.length = (long)drFormDesign["length"];
                        dt.maxLength = (long)drFormDesign["maxLength"];
                        dt.minLength = (long)drFormDesign["minLength"];
                        dt.mimeType = (string)drFormDesign["mimeType"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "boolean":
                    dataType = ItemChoiceType.boolean;
                    if (true)
                    {
                        var dt = new boolean_DEtype();
                        dt.val = (bool)drFormDesign["val"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "byte":
                    dataType = ItemChoiceType.@byte;
                    if (true)
                    {
                        var dt = new byte_DEtype();
                        dt.val = (sbyte)drFormDesign["val"];
                        dt.minExclusive = (sbyte)drFormDesign["minExclusive"];
                        dt.minInclusive = (sbyte)drFormDesign["minInclusive"];
                        dt.maxExclusive = (sbyte)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (sbyte)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"]; ;

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "date":
                    dataType = ItemChoiceType.date;
                    if (true)
                    {
                        var dt = new date_DEtype();
                        dt.val = (DateTime)drFormDesign["val"];
                        dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["minInclusive"];
                        dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "dateTime":
                    dataType = ItemChoiceType.dateTime;
                    if (true)
                    {
                        var dt = new dateTime_DEtype();
                        dt.val = (DateTime)drFormDesign["val"];
                        dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["minInclusive"];
                        dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "dateTimeStamp":
                    dataType = ItemChoiceType.dateTimeStamp;
                    if (true)
                    {
                        var dt = new dateTimeStamp_DEtype();
                        dt.val = (DateTime)drFormDesign["val"];
                        dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["minInclusive"];
                        dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "decimal":
                    dataType = ItemChoiceType.@decimal;
                    dataType = ItemChoiceType.@float;
                    if (true)
                    {
                        var dt = new decimal_DEtype();
                        dt.val = (decimal)drFormDesign["val"];
                        dt.minExclusive = (decimal)drFormDesign["minExclusive"];
                        dt.minInclusive = (decimal)drFormDesign["minInclusive"];
                        dt.maxExclusive = (decimal)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (decimal)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.fractionDigits = (byte)drFormDesign["fractionDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "double":
                    dataType = ItemChoiceType.@double;
                    if (true)
                    {
                        var dt = new double_DEtype();
                        dt.val = (double)drFormDesign["val"];
                        dt.minExclusive = (double)drFormDesign["minExclusive"];
                        dt.minInclusive = (double)drFormDesign["minInclusive"];
                        dt.maxExclusive = (double)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (double)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.fractionDigits = (byte)drFormDesign["fractionDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "duration":
                    dataType = ItemChoiceType.duration;
                    if (true)
                    {
                        var dt = new duration_DEtype();
                        dt.val = (System.TimeSpan)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (string)drFormDesign["minExclusive"];
                        dt.minInclusive = (string)drFormDesign["minInclusive"];
                        dt.maxExclusive = (string)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (string)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "float":
                    dataType = ItemChoiceType.@float;
                    if (true)
                    {
                        var dt = new float_DEtype();
                        dt.val = (float)drFormDesign["val"];
                        dt.minExclusive = (float)drFormDesign["minExclusive"];
                        dt.minInclusive = (float)drFormDesign["minInclusive"];
                        dt.maxExclusive = (float)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (float)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.fractionDigits = (byte)drFormDesign["fractionDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gDay":
                    dataType = ItemChoiceType.gDay;
                    if (true)
                    {
                        var dt = new gDay_DEtype();
                        dt.val = (DateTime)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["minInclusive"];
                        dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gMonth":
                    dataType = ItemChoiceType.gMonth;
                    if (true)
                    {
                        var dt = new gMonth_DEtype();
                        dt.val = (DateTime)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["minInclusive"];
                        dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gMonthDay":
                    dataType = ItemChoiceType.gMonthDay;
                    if (true)
                    {
                        var dt = new gMonthDay_DEtype();
                        dt.val = (DateTime)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["minInclusive"];
                        dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gYear":
                    dataType = ItemChoiceType.gYear;
                    if (true)
                    {
                        var dt = new gYear_DEtype();
                        dt.val = (DateTime)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["minInclusive"];
                        dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gYearMonth":
                    dataType = ItemChoiceType.gYearMonth;
                    if (true)
                    {
                        var dt = new gYearMonth_DEtype();
                        dt.val = (DateTime)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["minInclusive"];
                        dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "hexBinary":
                    dataType = ItemChoiceType.hexBinary;
                    if (true)
                    {
                        var dt = new hexBinary_DEtype();
                        dt.val = (byte[])drFormDesign["val"];
                        dt.valHex = (string)drFormDesign["val_string"];
                        dt.length = (long)drFormDesign["length"];
                        dt.maxLength = (long)drFormDesign["maxLength"];
                        dt.mimeType = (string)drFormDesign["mimeType"];
                        dt.minLength = (long)drFormDesign["minLength"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "int":
                    dataType = ItemChoiceType.@int;
                    if (true)
                    {
                        var dt = new int_DEtype();
                        dt.val = (int)drFormDesign["val"];
                        dt.minExclusive = (int)drFormDesign["minExclusive"];
                        dt.minInclusive = (int)drFormDesign["minInclusive"];
                        dt.maxExclusive = (int)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (int)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "integer":
                    dataType = ItemChoiceType.integer;
                    if (true)
                    {
                        var dt = new integer_DEtype();
                        dt.val = (System.Nullable<decimal>)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (System.Nullable<decimal>)drFormDesign["minExclusive"];
                        dt.minInclusive = (System.Nullable<decimal>)drFormDesign["minInclusive"];
                        dt.maxExclusive = (System.Nullable<decimal>)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (System.Nullable<decimal>)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "long":
                    dataType = ItemChoiceType.@long;
                    if (true)
                    {
                        var dt = new long_DEtype();
                        dt.val = (long)drFormDesign["val"];
                        dt.minExclusive = (long)drFormDesign["minExclusive"];
                        dt.minInclusive = (long)drFormDesign["minInclusive"];
                        dt.maxExclusive = (long)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (long)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "negativeInteger":
                    dataType = ItemChoiceType.negativeInteger;
                    if (true)
                    {
                        var dt = new negativeInteger_DEtype();
                        dt.val = (System.Nullable<decimal>)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (System.Nullable<decimal>)drFormDesign["minExclusive"];
                        dt.minInclusive = (System.Nullable<decimal>)drFormDesign["minInclusive"];
                        dt.maxExclusive = (System.Nullable<decimal>)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (System.Nullable<decimal>)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "nonNegativeInteger":
                    dataType = ItemChoiceType.nonNegativeInteger;
                    if (true)
                    {
                        var dt = new nonNegativeInteger_DEtype();
                        dt.val = (System.Nullable<decimal>)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (System.Nullable<decimal>)drFormDesign["minExclusive"];
                        dt.minInclusive = (System.Nullable<decimal>)drFormDesign["minInclusive"];
                        dt.maxExclusive = (System.Nullable<decimal>)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (System.Nullable<decimal>)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "nonPositiveInteger":
                    dataType = ItemChoiceType.nonPositiveInteger;
                    if (true)
                    {
                        var dt = new nonPositiveInteger_DEtype();
                        dt.val = (System.Nullable<decimal>)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (System.Nullable<decimal>)drFormDesign["minExclusive"];
                        dt.minInclusive = (System.Nullable<decimal>)drFormDesign["minInclusive"];
                        dt.maxExclusive = (System.Nullable<decimal>)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (System.Nullable<decimal>)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "positiveInteger":
                    dataType = ItemChoiceType.positiveInteger;
                    if (true)
                    {
                        var dt = new positiveInteger_DEtype();
                        dt.val = (System.Nullable<decimal>)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (System.Nullable<decimal>)drFormDesign["minExclusive"];
                        dt.minInclusive = (System.Nullable<decimal>)drFormDesign["minInclusive"];
                        dt.maxExclusive = (System.Nullable<decimal>)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (System.Nullable<decimal>)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "@short":
                    dataType = ItemChoiceType.@short;
                    if (true)
                    {
                        var dt = new short_DEtype();
                        dt.val = (short)drFormDesign["val"];
                        dt.minExclusive = (short)drFormDesign["minExclusive"];
                        dt.minInclusive = (short)drFormDesign["minInclusive"];
                        dt.maxExclusive = (short)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (short)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "string":
                    dataType = ItemChoiceType.@string;
                    if (true)
                    {
                        var dt = new @string_DEtype();
                        dt.val = (string)drFormDesign["val"];
                        dt.maxLength = (long)drFormDesign["maxLength"];
                        dt.minLength = (long)drFormDesign["minLength"];
                        dt.pattern = (string)drFormDesign["pattern"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "time":
                    dataType = ItemChoiceType.time;
                    if (true)
                    {
                        var dt = new time_DEtype();
                        dt.val = (DateTime)drFormDesign["val"];
                        dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["minInclusive"];
                        dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedByte":
                    dataType = ItemChoiceType.unsignedByte;
                    if (true)
                    {
                        var dt = new unsignedByte_DEtype();
                        dt.val = (byte)drFormDesign["val"];
                        dt.minExclusive = (byte)drFormDesign["minExclusive"];
                        dt.minInclusive = (byte)drFormDesign["minInclusive"];
                        dt.maxExclusive = (byte)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (byte)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedInt":
                    dataType = ItemChoiceType.unsignedInt;
                    if (true)
                    {
                        var dt = new unsignedInt_DEtype();
                        dt.val = (uint)drFormDesign["val"];
                        dt.minExclusive = (uint)drFormDesign["minExclusive"];
                        dt.minInclusive = (uint)drFormDesign["minInclusive"];
                        dt.maxExclusive = (uint)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (uint)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedLong":
                    dataType = ItemChoiceType.unsignedLong;
                    if (true)
                    {
                        var dt = new unsignedLong_DEtype();
                        dt.val = (ulong)drFormDesign["val"];
                        dt.minExclusive = (ulong)drFormDesign["minExclusive"];
                        dt.minInclusive = (ulong)drFormDesign["minInclusive"];
                        dt.maxExclusive = (ulong)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (ulong)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedShort":
                    dataType = ItemChoiceType.unsignedShort;
                    if (true)
                    {
                        var dt = new unsignedShort_DEtype();
                        dt.val = (ushort)drFormDesign["val"];
                        dt.minExclusive = (ushort)drFormDesign["minExclusive"];
                        dt.minInclusive = (ushort)drFormDesign["minInclusive"];
                        dt.maxExclusive = (ushort)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (ushort)drFormDesign["maxInclusive"];
                        dt.totalDigits = (byte)drFormDesign["totalDigits"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "yearMonthDuration":
                    dataType = ItemChoiceType.yearMonthDuration;
                    if (true)
                    {
                        var dt = new yearMonthDuration_DEtype();
                        dt.val = (TimeSpan)drFormDesign["val"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.minExclusive = (TimeSpan)drFormDesign["minExclusive"];
                        dt.minInclusive = (TimeSpan)drFormDesign["minInclusive"];
                        dt.maxExclusive = (TimeSpan)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (TimeSpan)drFormDesign["maxInclusive"];
                        dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier(drFormDesign);

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                default:
                    dataType = ItemChoiceType.@string;
                    {
                        var dt = new @string_DEtype();
                        dt.val = (string)drFormDesign["val"];
                        dt.maxLength = (long)drFormDesign["maxLength"];
                        dt.minLength = (long)drFormDesign["minLength"];
                        dt.pattern = (string)drFormDesign["pattern"];

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


        public virtual DataTypes_DEType AddDataTypesDE(ListFieldType lft, QuestionItemBaseTypeResponseTypeEnum dataType)
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
        public virtual AddressType AddAddress(PersonType pt)
        {
            throw new NotImplementedException();
        }
        public virtual AddressType AddAddress(OrganizationType ot)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Person

        public PersonType AddPerson(ContactType contact)
        {
            if (contact == null) { contact = new ContactType(); }

            var pt = new PersonType();
            contact.Item = pt;
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
            { contactList = dt.Contact; }
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


        public virtual PersonType AddPersonItems(PersonType pt)
        {
            //var dr = this.drFormDesign();
            pt.PersonName = new NameType();//TODO: Need separate method(s) for this
            //pt.Alias = new NameType();
            pt.PersonName.FirstName.val = (string)drFormDesign["FirstName"];  //TODO: replace with real data
            pt.PersonName.LastName.val = (string)drFormDesign["LastName"];  //TODO: replace with real data

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
        public virtual NameType AddPersonName(PersonType pt)
        { throw new NotImplementedException(); }
        public virtual JobType AddJob(PersonType pt)
        { throw new NotImplementedException(); }
        public virtual EmailType AddEmail(OrganizationType ot)
        { throw new NotImplementedException(); }
        public virtual EmailType AddEmail(PersonType pt)
        { throw new NotImplementedException(); }
        public virtual PhoneType AddPhone(OrganizationType ot)
        { throw new NotImplementedException(); }
        public virtual PhoneType AddPhone(PersonType ot)
        { throw new NotImplementedException(); }
        public virtual IdentifierType AddIdentifier(OrganizationType ot)
        { throw new NotImplementedException(); }
        public virtual anyURI_Stype AddWebURL(OrganizationType ot)
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
        public virtual OrganizationType AddOrganizationItems(OrganizationType ot)
        {
            //var dr = this.drFormDesign();

            ot.OrgName = new string_Stype();//TODO: Need separate method(s) for this
            ot.OrgName.val = (string)drFormDesign["OrgName"];
            var p = AddContactPerson(ot);  //This function will add all the person details to teh Person list
            ot.Email = new List<EmailType>();//TODO: Need separate method(s) for this
            var email = new EmailType();//TODO: Need separate method(s) for this
            ot.Email.Add(email);
            ot.Phone = new List<PhoneType>();//TODO: Need separate method(s) for this
            var ph1 = new PhoneType();
            ot.Phone.Add(ph1);
            //var pt = new string_Stype();
            ph1.PhoneType1.val = (string)drFormDesign["PhoneType1"];

            var pn = new PhoneNumberType();
            ph1.PhoneNumber = pn;
            pn.val = (string)drFormDesign["PhoneNumber1"];
            //ot.Department = new string_Stype();
            ot.Department.val = (string)drFormDesign["Department1"];
            ot.Role = new List<string_Stype>();
            var r = new string_Stype();
            ot.Role.Add(r);
            r.val = (string)drFormDesign["OrgRole1"];
            var address1 = AddAddress(ot);
            address1.AddressLine = new List<string_Stype>();
            var al1 = new string_Stype();
            address1.AddressLine.Add(al1);
            ot.Identifier = new List<IdentifierType>();
            var id1 = new IdentifierType();
            ot.Identifier.Add(id1);
            id1.val = (string)drFormDesign["OrgID1"];
            //ot.Usage = new string_Stype();
            ot.Usage.val = (string)drFormDesign["Usage"];
            ot.WebURL = new List<anyURI_Stype>();//TODO: Need separate method(s) for this\
            var webURL = new anyURI_Stype();
            ot.WebURL.Add(webURL);
            webURL.val = (string)drFormDesign["WebURL"];
            return ot;
        }


        #endregion



        #endregion
        #region Common Types



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
        public virtual QuestionItemType AddQuestion<T>(T T_Parent, ItemType qType) where T : DisplayedType, IChildItems, new()
        {
            //var dr = this.drFormDesign();

            var qNew = new QuestionItemType();
            qNew.readOnly = (bool)drFormDesign["readonly"];  //TODO: replace with real data
            var childItemsList = AddChildItems(T_Parent);
            childItemsList.ListOfItems.Add(qNew);

            switch (qType)
            {
                case ItemType.QuestionSingle:
                    AddListFieldToQuestion(qNew);
                    break;
                case ItemType.QuestionMultiple:
                    AddListFieldToQuestion(qNew);
                    break;
                case ItemType.QuestionFill:
                    AddResponseField(qNew);
                    break;
                case ItemType.QuestionLookup:
                    AddListFieldToQuestion(qNew);
                    break;
                default:
                    break;
            }
            AddRepeatingTypeItems(qNew);

            return qNew;
        }
        public virtual InjectFormType AddInjectedForm<T>(T T_Parent) where T : DisplayedType, IChildItems, new()
        {
            //var dr = this.drFormDesign();

            var childItems = AddChildItems(T_Parent);
            var injForm = new InjectFormType();
            childItems.ListOfItems.Add(injForm);  //TODO:need to first instantiate the List!
            //reeBuilder.AddInjectedFormItems(injForm);

            //iform.injectionID = a0;  needs to be string format??
            injForm.packageID = (string)drFormDesign["packageID"];  //TODO: replace with real data
            injForm.rootItemID = (string)drFormDesign["rootItemID"];  //TODO: replace with real data
            injForm.baseURI = (string)drFormDesign["baseURI"];  //TODO: replace with real data
            injForm.packageID = (string)drFormDesign["packageID"];  //TODO: replace with real data
            injForm.ID = (string)drFormDesign["ID"];  //TODO: replace with real data


            //response properties
            injForm.formInstanceURI = (string)drFormDesign["formInstanceURI"];  //TODO: replace with real data
            injForm.formInstanceVersionURI = (string)drFormDesign["formInstanceVersionURI"];  //TODO: replace with real data
            injForm.injectionID = (string)drFormDesign["injectionID"];  //TODO: replace with real data
            injForm.formInstanceURI = (string)drFormDesign["baseURI"];  //TODO: replace with real data


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
        public virtual LinkType AddLink(DisplayedType dt)
        {
            //var dr = this.drFormDesign();

            var link = new LinkType();

            if (dt.Link == null) dt.Link = new List<LinkType>();
            dt.Link.Add(link);

            var rtf = new RichTextType();
            link.LinkText = rtf;

            rtf.val = (string)drFormDesign["LinkText"];  //TODO: replace with real data
            var html = AddHTML(rtf);  //check this

            link.LinkURI = new anyURI_Stype();
            link.LinkURI.val = (string)drFormDesign["LinkURI"];  //TODO: replace with real data

            var desc = new RichTextType();
            if (link.Description == null) link.Description = new List<RichTextType>();
            link.Description.Add(desc);
            desc.val = (string)drFormDesign["LinkDescText"];  //TODO: replace with real data
            //AddHTML(desc).Any.Add(HTML);
            //Fill the description text here

            //....

            AddExtensionBaseTypeItems(dt);

            return link;

            //LinkText: HTML Type
            //LinkURI: URI Type: ExtensionBase Type
            //Description: HTML Type
        }
        public virtual BlobType AddBlob(DisplayedType dt)
        {
            //var dr = this.drFormDesign();

            var blob = new BlobType();
            dt.BlobContent.Add(blob);

            var rtf = new RichTextType();
            rtf.val = (string)drFormDesign["BlobText"];  //TODO: replace with real data
            var html = AddHTML(rtf);

            blob.Description = new List<RichTextType>();
            blob.Description.Add(rtf);
            blob.Description.Add(rtf);

            var bUri = new anyURI_Stype();
            bUri.val = "https://www.cap.org/ecc/sdc/image1234.jpg";  //TODO: replace with real data
            blob.Item = bUri;
            var bin = new base64Binary_DEtype();
            bin.valBase64 = "SGVsbG8=";  //TODO: replace with real data
            //TODO: error in code generator for base64Binary_Stype - val should be string datatype, not a byte array.


            return blob;
            //Description: RichTextType
            //Hash
            //BobURI
            //BinaryMediaBase63
        }


        public virtual IfThenType AddOnEvent(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public virtual IfThenType AddOnEnter(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public virtual OnEventType AddOnExit(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public virtual WatchedPropertyType AddActivateIf(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public virtual WatchedPropertyType AddDeActivateIf(DisplayedType dt)
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
        public virtual CodeSystemType AddCodeSystemItems(CodeSystemType cs)
        {
            //var dr = this.drFormDesign();

            cs.CodeSystemName.val = (string)drFormDesign["CodeSystemName"];  //TODO: replace with real data
            cs.CodeSystemURI.val = (string)drFormDesign["CodeSystemURI"];  //TODO: replace with real data
            cs.OID.val = (string)drFormDesign["CodeSystemOID"];  //TODO: replace with real data
            cs.ReleaseDate.val = (DateTime)drFormDesign["CodeSystemReleaseDate"];  //TODO: replace with real data
            cs.Version.val = (string)drFormDesign["CodeSystemVersion"];  //TODO: replace with real data

            AddExtensionBaseTypeItems(cs);
            return (CodeSystemType)cs;
        }


        public virtual CodingType AddCodedValue(DisplayedType dt)
        {
            //var dr = this.drFormDesign();

            var coding = new CodingType();

            var codingList = AddCodingList(dt);
            codingList.Add(coding);

            coding.CodeMatch = (CodeMatchType)drFormDesign["CodeMatch"];  //this will need work for enums
            coding.Code.val = (string)drFormDesign["Code"];
            var richText = new RichTextType();
            richText.val = (string)drFormDesign["CodeText"];
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
        public virtual CodingType AddCodedValue(LookupEndPointType lep)
        {
            //var dr = this.drFormDesign();

            var coding = new CodingType();

            var codingList = AddCodingList(lep);
            codingList.Add(coding);

            coding.CodeMatch = (CodeMatchType)drFormDesign["CodeMatch"];  //this will need work for enums
            coding.Code.val = (string)drFormDesign["Code"];
            var richText = new RichTextType();
            richText.val = (string)drFormDesign["CodeText"];
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
        public virtual void AddListItemProperties(ListItemType li)
        {
            li.title = "title";//TODO: replace with real data
            li.omitWhenSelected = true;//TODO: replace with real data
            li.selected = true;//TODO: replace with real data
            li.selectionDeselectsSiblings = true;//TODO: replace with real data
            li.selectionDisablesChildren = true;//TODO: replace with real data

            AddDisplayedTypeItems(li);
            AddListItemResponseField(li);
            //li.OnDeselect;
            //li.OnSelect;
            //li.DeselectIf;
            //li.SelectIf;
            //li.ActivateIf;
            //li.DeActivateIf;



        }
        public virtual DisplayedType AddListMemberToQuestion(QuestionItemType qNode)
        {
            //var dr = this.drFormDesign();

            var listField = AddListFieldToQuestion(qNode);
            var list = AddListToListField(qNode.ListField_Item);
            //var itemList = list.DisplayedItem_List;

            ListItemType li;
            DisplayedType di;

            //!+Choose one of the following as the first Item in LIst
            var type = 1; //TODO: replace with real data
            if (type == 1)
            {
                li = AddListItemToList(list);
                return li;
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
        public virtual ResponseFieldType AddResponseField(QuestionItemType q)
        {
            //Add Response, TextAfterResponse (RichTextType), ReponseUnits, SetValueExpression
            //var dr = this.drFormDesign();

            var rf = new ResponseFieldType();
            rf.ResponseUnits.val = "ResponseUnits";//TODO: replace with real data
            rf.TextAfterResponse.val = (string)drFormDesign["TextAfterResponse"];//TODO: replace with real data
            q.ResponseField_Item = rf;

            AddResponseItems(rf);
            AddExtensionBaseTypeItems(rf);


            return rf;
        }

        public virtual ListItemResponseFieldType AddListItemResponseField(ListItemType li)
        {
            //Add Response, TextAfterResponse (RichTextType), ReponseUnits, SetValueExpression
            //var dr = this.drFormDesign();

            var liRF = new ListItemResponseFieldType();
            liRF.ResponseUnits.val = "ResponseUnits";//TODO: replace with real data
            liRF.TextAfterResponse.val = "TextAfterResponse";//TODO: replace with real data
            liRF.responseRequired = true;//TODO: replace with real data
            li.ListItemResponseField = liRF;

            AddResponseItems(liRF);
            AddExtensionBaseTypeItems(liRF);


            return liRF;
        }

        public ResponseFieldType AddResponseItems(ResponseFieldType rf)
        {
            var stringDE = new string_DEtype();
            rf.Response.Item = stringDE;
            rf.Response.ItemElementName = ItemChoiceType.@string; //TODO: replace with real data
            return (ResponseFieldType)rf;

        }

        public virtual IdentifiedExtensionType AddDataTypeToResponseType(ResponseFieldType dd)
        {
            throw new NotImplementedException();
        }
        public virtual void AddResponseHistoryToQuestion(QuestionItemType q)
        {
            AddReplacedResponseToResponseHistory(q.ResponseHistory);
            throw new NotImplementedException();
        }
        public virtual void AddReplacedResponseToResponseHistory(List<ResponseChangeType> respChange)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LookupEndpoint
        //!Add LookupEndpoint to Question w ListField
        public virtual LookupEndPointType AddEndpointToQuestion(DisplayedType nodeToAdd, DisplayedType parentNode)
        {
            //Add ResponseComment, Editor, DateTime, Response, SelectedItems
            return new LookupEndPointType();
        }
        #endregion

        #endregion
        #region Rules
        //!+Rules
        public virtual RulesType AddRuleToDisplayedType(DisplayedType parent)
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
            { //throw new Exception("Failure to create enum");
            }
            return newEnum;
        }


        #endregion

        #region Actions

        public virtual ActSendMessageType AddActSendMessage(ThenType tt)
        {
            var asmt = new ActSendMessageType();
            if (tt.Items != null) tt.Items = new List<SDC.IdentifiedExtensionType>();
            tt.Items.Add(asmt);
            var html = new SDC.HTML_DEtype();
            asmt.HTML = html;
            //var DataRepo.dr = DataRepo.dr;
            asmt.val = (string)drFormDesign["ActSendMessage"];
            if (html.Any == null) html.Any = new List<XmlElement>();
            html.Any.Add(StringToXMLElement((string)drFormDesign["ActSendMessageHTML"]));
            return asmt;

        }
        #endregion
        #region Resources


        public virtual UnitsType AddUnits<T>(T t) where T : ExtensionBaseType
        {

            throw new NotImplementedException();
            //var dr = this.drFormDesign();
            var u = new UnitsType();
            u.val = "units";//TODO: replace with real data
            return u;
        }
        public UnitsType AddUnits(ResponseFieldType rf)
        {
            throw new NotImplementedException();
            var u = new UnitsType();
            return u;
        }
        private RichTextType AddRichText<T>(T t, string strHTML = "", string val = "") where T : IdentifiedExtensionType
        {
            throw new NotImplementedException();
            var rtf = new RichTextType();
            var html = AddHTML(rtf);

            return rtf;
        }

        #endregion

    }

}



using System;
using System.Data;
using System.Collections.Generic;
using SDC.DAL.DataSets;
using System.Xml;

namespace SDC
{
    /// <summary>
    /// Implementation of abstract class SDCTreeBuilder.  
    /// Can add custom functions for tooltips, HTML titles, pop-up help or links, etc
    /// tooltip/description, Note HTML/txt, Note Links, Note, ICD-O3, SNOMED, ICD-10, CPT
    /// rules
    /// </summary>
    public class SDCTreeBuilderEcc : SDCTreeBuilder
    {
        public SDCTreeBuilderEcc(string formDesignID, IFormDesignDataSets dataSets, string xsltPath = "")
        {
            var fd = new FormDesignType(); //start the form!
            fd.InjectTreeBuilder = this;   //Inject this tree builder into the form; 
            //this adds custom tree builder functions to appropriate form objects
            FormDesign = fd;               //The tree builder needs a copy of the form  to act upon

            decimal decVal;
            Decimal.TryParse(formDesignID, out decVal);
            FormDesignID = decVal;

            XsltFileName = xsltPath;

            dtHeaderDesign = dataSets.dtGetFormDesignMetadata(FormDesignID);
            dtFormDesign = dataSets.dtGetFormDesign(FormDesignID);

            CreateFormDesignTree();        //Add branches to the form tree
        }

        #region Template Builder

        protected override ItemType GetRowType(string strRowType)
        {
            switch (strRowType.ToUpper())
            {
                case "CHECKLIST":
                    return ItemType.Template;
                case "ANSWER":
                    return ItemType.ListItem;
                case "ANSWERFILLIN":
                    return ItemType.ListItemFill;
                case "QUESTIONSINGLE":
                    return ItemType.QuestionSingle;
                case "QUESTIONMULTIPLE":
                    return ItemType.QuestionMultiple;
                case "QUESTIONFILLIN":
                    return ItemType.QuestionFill;
                case "QUESTION_LOOKUP":
                    return ItemType.QuestionLookup;
                case "HEADER":
                    return ItemType.Section;
                case "NOTE":
                    return ItemType.Note;
                case "FIXEDLISTNOTE":
                    return ItemType.FixedListNote;
                default:
                    return ItemType.Note;
            }

        }

        /// <summary>
        /// Gets template by Ckey
        /// </summary>
        /// <param name="formDesignID">Ckey of template to get</param>
        /// <param name="BESTfilename">The preferred file name.  Use USERfilename in preference to the Database supplied filename</param>
        /// <param name="USERfilename">The preferred filename, if available</param>
        /// <returns>Template XML</returns>
        public override String GetTemplateByCkey(String formDesignID, out String BESTfilename, String USERfilename = "")
        {
            Decimal CTV_Ckey = 0;
            if (formDesignID.Trim() != "" && Decimal.TryParse(formDesignID, out CTV_Ckey))
            {
                if (CTV_Ckey > 0)
                {
                    DataRow row = dtHeaderDesign.Rows[0];

                    //Decide if we should use the database supplied filename, or preferably, a user-supplied filename
                    if (USERfilename != string.Empty)
                    { row["CurrentFileName"] = USERfilename.Trim(); }
                    else if ((String)row["CurrentFileName"] == String.Empty)
                    { USERfilename = "_file_" + DateTime.Now.Ticks.ToString().Trim(); }   //create a dummy filename and insert it into the dataset, so it's also added to the XML 

                    BESTfilename = (String)row["CurrentFileName"];

                    this.CreateFormDesignTree();  //!+This line needs work

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
        public override String SerializeFormDesignTree()
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
                        string.Format(
                        replaceTxt + Environment.NewLine +
                        "<?xml-stylesheet type=\"text/xsl\" href=\"{0}\" ?>/r/n" +
                        "<!--(c) 2007-2016 College of American Pathologists.  All rights reserved.  " +
                        "License required for use.-->" + Environment.NewLine,
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
        public override void AddTemplateMetadataToForm(IdentifiedExtensionType n, DisplayedType parentNode)
        {   //Uses CTV data
            throw new NotImplementedException();
        }

        #endregion

        #region Base Types
        public override RepeatingType AddRepeatingTypeItems(RepeatingType rt)
        {
            //var dr = this.drFormDesign();

            rt.maxCard = (ushort)drFormDesign["maxCard"];
            rt.minCard = (ushort)drFormDesign["minCard"];

            AddDisplayedTypeItems((RepeatingType)rt);
            return (RepeatingType)rt;
        }
        public override DisplayedType AddDisplayedTypeItems(DisplayedType dt)
        {
            //var dr = this.drFormDesign();

            dt.enabled = (bool)drFormDesign["enabled"];
            dt.visible = (bool)drFormDesign["visible"];
            dt.title = (string)drFormDesign["VisibleText"];
            dt.mustImplement = (bool)drFormDesign["mustImplement"];
            dt.showInReport = (bool)drFormDesign["showInReport"];

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
        public override IdentifiedExtensionType AddIdentifiedTypeItems(IdentifiedExtensionType iet)
        {
            //var dr = this.drFormDesign();

            iet.baseURI = (string)drFormDesign["baseURI"];
            iet.ID = (string)drFormDesign["ChecklistTemplateItemCKey"];
            iet.ParentID = (string)drFormDesign["ParentItemCKey"];
            //iet.ParentRecordNode = ParentNodes.Find();


            AddExtensionBaseTypeItems((IdentifiedExtensionType)iet);
            return (IdentifiedExtensionType)iet;
        }
        public override IdentifiedExtensionType AddBaseTypeItems(IdentifiedExtensionType bt)
        {
            //var dr = this.drFormDesign();

            bt.ParentID = (string)drFormDesign["ParentItemCkey"];
            bt.name = (string)drFormDesign["shortName"]; // could move this to IdentifiedExtensionType
            bt.type = (string)drFormDesign["type"];
            bt.styleClass = (string)drFormDesign["styleClass"];
            bt.order = (decimal)drFormDesign["sortorder"];
            return (IdentifiedExtensionType)bt;
        }
        public override CommentType AddComment(ExtensionBaseType ebt)
        {
            //var dr = this.drFormDesign();
            var c = new CommentType();

            if (ebt.Comment == null) ebt.Comment = new List<CommentType>();
            ebt.Comment.Add(c);
            c.val = (string)drFormDesign["Comment"];
            //AddBaseTypeItems(ebt);
            return c;
        }
        public override ExtensionType AddExtension(ExtensionBaseType ebt)
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
        public override HTML_Stype AddHTML(RichTextType rtf)
        {
            //throw new NotImplementedException();
            //var dr = this.drFormDesign();

            var html = new HTML_Stype();
            rtf.HTML = html;
            html.name = "";
            html.type = "";
            html.styleClass = "";

            html.Any = new List<XmlElement>();

            var xhtml = StringToXMLElement((string)drFormDesign["html"]);
            html.Any.Add(xhtml);
            //TODO: Check XHTML builder here: 
            //https://gist.github.com/rarous/3150395, 
            //http://www.authorcode.com/code-snippet-converting-xmlelement-to-xelement-and-xelement-to-xmlelement-in-vb-net/
            //https://msdn.microsoft.com/en-us/library/system.xml.linq.loadoptions%28v=vs.110%29.aspx
            return html;

        }
        public override WebServiceType AddWebService(LookupEndPointType lep)
        {
            throw new NotImplementedException();
        }

        public override DataTypes_SType AddDataTypesS(CodingType coding)
        {
            throw new NotImplementedException();
        }

        public override DataTypes_DEType AddDataTypesDE(ResponseFieldType RF)
        {
            var resp = new DataTypes_DEType();

            //var dt = new DataTypes_DEType
            //resp.DataTypeDE_Item= new  

            RF.Response = new DataTypes_DEType();
            AddBaseTypeItems(RF.Response.Item);

            var dataType = new ItemChoiceType();
            RF.Response.ItemElementName = dataType;
            //var dr = this.drFormDesign();

            string itemDataType = (string)drFormDesign["ItemDataType"];

            switch (itemDataType)
            {
                case "HTML":
                    dataType = ItemChoiceType.HTML;
                    if (true)
                    {
                        var dt = new HTML_DEtype();
                        dt.Any = new List<XmlElement>();
                        dt.Any.Add((XmlElement)drFormDesign["xml"]);  //TODO:
                        dt.AnyAttr = new List<XmlAttribute>();
                        dt.AnyAttr.Add((XmlAttribute)drFormDesign["attribute"]);
                        dt.maxLength = (long)drFormDesign["maxLength"];
                        dt.minLength = (long)drFormDesign["minLength"];

                        RF.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "XML":
                    dataType = ItemChoiceType.XML;
                    if (true)
                    {
                        var dt = new XML_DEtype();
                        dt.Any = new List<XmlElement>();
                        dt.Any.Add((XmlElement)drFormDesign["xml"]);  //TODO: 
                        //dt.AnyAttr = new List<System.Xml.XmlAttribute>();
                        dt.maxLength = (long)drFormDesign["maxLength"];
                        dt.minLength = (long)drFormDesign["minLength"];
                        //dt.@namespace = (string)dr["namespace"];
                        dt.schema = (string)drFormDesign["schema"];

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
        public override DataTypes_DEType AddDataTypesDE(ListFieldType lft, QuestionItemBaseTypeResponseTypeEnum dataType)
        {   //DefaultListItemDataType

            throw new NotImplementedException();
        }
        #endregion

        #region Contacts

        public override AddressType AddAddress(PersonType pt)
        {
            throw new NotImplementedException();
        }
        public override AddressType AddAddress(OrganizationType ot)
        {
            throw new NotImplementedException();
        }
        public override PersonType AddPersonItems(PersonType pt)
        {
            //var dr = this.drFormDesign();
            pt.PersonName = new NameType();//TODO: Need separate method(s) for this
            //pt.Alias = new NameType();
            pt.PersonName.FirstName.val = (string)drFormDesign["FirstName"];
            pt.PersonName.LastName.val = (string)drFormDesign["LastName"];

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
        public override NameType AddPersonName(PersonType pt)
        { throw new NotImplementedException(); }
        public override JobType AddJob(PersonType pt)
        { throw new NotImplementedException(); }
        public override EmailType AddEmail(OrganizationType ot)
        { throw new NotImplementedException(); }
        public override EmailType AddEmail(PersonType pt)
        { throw new NotImplementedException(); }
        public override PhoneType AddPhone(OrganizationType ot)
        { throw new NotImplementedException(); }
        public override PhoneType AddPhone(PersonType ot)
        { throw new NotImplementedException(); }
        public override IdentifierType AddIdentifier(OrganizationType ot)
        { throw new NotImplementedException(); }
        public override anyURI_Stype AddWebURL(OrganizationType ot)
        { throw new NotImplementedException(); }

        public override OrganizationType AddOrganizationItems(OrganizationType ot)
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

        #region ChildItems

        public override QuestionItemType AddQuestion<T>(T T_Parent, ItemType qType) //where T : DisplayedType, IChildItems, new()
        {
            //var dr = this.drFormDesign();

            var qNew = new QuestionItemType();
            qNew.readOnly = (bool)drFormDesign["readonly"];
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
        public override InjectFormType AddInjectedForm<T>(T T_Parent) // where T : DisplayedType, IChildItems, new()
        {
            //var dr = this.drFormDesign();

            var childItems = AddChildItems(T_Parent);
            var injForm = new InjectFormType();
            childItems.ListOfItems.Add(injForm);  //TODO:need to first instantiate the List!
            //reeBuilder.AddInjectedFormItems(injForm);

            //iform.injectionID = a0;  needs to be string format??
            injForm.packageID = (string)drFormDesign["packageID"];
            injForm.rootItemID = (string)drFormDesign["rootItemID"];
            injForm.baseURI = (string)drFormDesign["baseURI"];
            injForm.packageID = (string)drFormDesign["packageID"];
            injForm.ID = (string)drFormDesign["ID"];


            //response properties
            injForm.formInstanceURI = (string)drFormDesign["formInstanceURI"];
            injForm.formInstanceVersionURI = (string)drFormDesign["formInstanceVersionURI"];
            injForm.injectionID = (string)drFormDesign["injectionID"];
            injForm.formInstanceURI = (string)drFormDesign["baseURI"];


            AddExtensionBaseTypeItems(injForm);


            return injForm;
        }

        #endregion

        #region Displayed Type

        public override LinkType AddLink(DisplayedType dt)
        {
            //var dr = this.drFormDesign();

            var link = new LinkType();

            if (dt.Link == null) dt.Link = new List<LinkType>();
            dt.Link.Add(link);

            var rtf = new RichTextType();
            link.LinkText = rtf;

            rtf.val = (string)drFormDesign["LinkText"];
            var html = AddHTML(rtf);  //check this

            link.LinkURI = new anyURI_Stype();
            link.LinkURI.val = (string)drFormDesign["LinkURI"];

            var desc = new RichTextType();
            if (link.Description == null) link.Description = new List<RichTextType>();
            link.Description.Add(desc);
            desc.val = (string)drFormDesign["LinkDescText"];
            //AddHTML(desc).Any.Add(HTML);
            //Fill the description text here

            //....

            AddExtensionBaseTypeItems(dt);

            return link;

            //LinkText: HTML Type
            //LinkURI: URI Type: ExtensionBase Type
            //Description: HTML Type
        }
        public override BlobType AddBlob(DisplayedType dt)
        {
            //var dr = this.drFormDesign();

            var blob = new BlobType();
            dt.BlobContent.Add(blob);

            var rtf = new RichTextType();
            rtf.val = (string)drFormDesign["BlobText"];
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

        #region DisplayedType Events
        public override IfThenType AddOnEvent(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public override IfThenType AddOnEnter(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public override OnEventType AddOnExit(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public override WatchedPropertyType AddActivateIf(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        public override WatchedPropertyType AddDeActivateIf(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region Coding

        public override CodeSystemType AddCodeSystemItems(CodeSystemType cs)
        {
            //var dr = this.drFormDesign();

            cs.CodeSystemName.val = (string)drFormDesign["CodeSystemName"];
            cs.CodeSystemURI.val = (string)drFormDesign["CodeSystemURI"];
            cs.OID.val = (string)drFormDesign["CodeSystemOID"];
            cs.ReleaseDate.val = (DateTime)drFormDesign["CodeSystemReleaseDate"];
            cs.Version.val = (string)drFormDesign["CodeSystemVersion"];

            AddExtensionBaseTypeItems(cs);
            return (CodeSystemType)cs;
        }
        public override CodingType AddCodedValue(DisplayedType dt)
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
        public override CodingType AddCodedValue(LookupEndPointType lep)
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

        #endregion

        #region QAS


        #region List
        //!+List
        public override void AddListItemProperties(ListItemType li)
        {
            //var dr = this.drFormDesign();

            //throw new NotImplementedException();
            li.title = (string)drFormDesign["VisibleText"];
            li.omitWhenSelected = (bool)drFormDesign["omitWhenSelected"];
            li.selected = (bool)drFormDesign["selected"];
            li.selectionDeselectsSiblings = (bool)drFormDesign["selectionDeselectsSiblings"];
            li.selectionDisablesChildren = (bool)drFormDesign["selectionDisablesChildren"];
            AddDisplayedTypeItems(li);
            AddListItemResponseField(li);
            //li.OnDeselect;
            //li.OnSelect;
            //li.DeselectIf;
            //li.SelectIf;
            //li.ActivateIf;
            //li.DeActivateIf;
        }
        public override DisplayedType AddListMemberToQuestion(QuestionItemType qNode)
        {
            //var dr = this.drFormDesign();

            var listField = AddListFieldToQuestion(qNode);
            var list = AddListToListField(qNode.ListField_Item);
            //var itemList = list.DisplayedItem_List;

            ListItemType li;
            DisplayedType di;

            //!+Choose one of the following as the first Item in LIst
            var type = (int)(drFormDesign["AnswerItemTypeKey"]);
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
        #endregion


        #region Response
        //!+Create Response Items
        public override ResponseFieldType AddResponseField(QuestionItemType q)
        {
            //Add Response, TextAfterResponse (RichTextType), ReponseUnits, SetValueExpression
            //var dr = this.drFormDesign();

            var rf = new ResponseFieldType();
            rf.ResponseUnits.val = (string)drFormDesign["AnswerUnits"];
            rf.TextAfterResponse.val = (string)drFormDesign["TextAfterResponse"];
            q.ResponseField_Item = rf;

            AddResponseItems(rf);
            AddExtensionBaseTypeItems(rf);


            return rf;
        }

        public override ListItemResponseFieldType AddListItemResponseField(ListItemType li)
        {
            //Add Response, TextAfterResponse (RichTextType), ReponseUnits, SetValueExpression
            //var dr = this.drFormDesign();

            var liRF = new ListItemResponseFieldType();
            liRF.ResponseUnits.val = (string)drFormDesign["AnswerUnits"];
            liRF.TextAfterResponse.val = (string)drFormDesign["TextAfterResponse"];
            liRF.responseRequired = (bool)drFormDesign["responseRequired"];
            li.ListItemResponseField = liRF;

            AddResponseItems(liRF);
            AddExtensionBaseTypeItems(liRF);


            return liRF;
        }

        public override IdentifiedExtensionType AddDataTypeToResponseType(ResponseFieldType dd)
        {
            throw new NotImplementedException();
        }
        public override void AddResponseHistoryToQuestion(QuestionItemType q)
        {
            AddReplacedResponseToResponseHistory(q.ResponseHistory);
            throw new NotImplementedException();
        }
        public override void AddReplacedResponseToResponseHistory(List<ResponseChangeType> respChange)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LookupEndpoint
        //!Add LookupEndpoint to Question w ListField
        public override LookupEndPointType AddEndpointToQuestion(DisplayedType nodeToAdd, DisplayedType parentNode)
        {
            //Add ResponseComment, Editor, DateTime, Response, SelectedItems
            return new LookupEndPointType();
        }
        #endregion

        #endregion

        #region Rules
        //!+Rules
        public override RulesType AddRuleToDisplayedType(DisplayedType parent)
        {
            //FormAction
            //PropertyAction, 
            //If ListItemStatus, 
            //If Predicate, 
            //IfGroup
            return new RulesType();
        }
        #endregion

        #region Actions

        public override ActSendMessageType AddActSendMessage(ThenType tt)
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

        public override UnitsType AddUnits<T>(T t) //where T : ExtensionBaseType
        {

            throw new NotImplementedException();
            //var dr = this.drFormDesign();
            var u = new UnitsType();
            u.val = (string)drFormDesign["units"];
            return u;
        }


        #endregion
        #region Custom Objects
        private void AddTooltip(DisplayedType q)
        {
            throw new NotImplementedException();
        }

        private RichTextType AddNote(DisplayedType q)
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}

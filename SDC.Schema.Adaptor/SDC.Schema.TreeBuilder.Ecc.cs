using SDC.DAL.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
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

        #region Rules

        //!+Rules
        protected override RulesType AddRuleToDisplayedType(DisplayedType parent)
        {
            //FormAction
            //PropertyAction,
            //If ListItemStatus,
            //If Predicate,
            //IfGroup
            return new RulesType(parent);
        }

        #endregion

        #region Ctor

        public SDCTreeBuilderEcc(string CTV_Ckey, IFormDesignDataSets dataSets, string xsltPath = "")
        {
            this.Order = 0;
            this.XsltFileName = xsltPath;

            decimal decCTV_Ckey;
            Decimal.TryParse(CTV_Ckey, out decCTV_Ckey);

            //First, set up the data for the form.
            this.dtHeaderDesign = dataSets.dtGetFormDesignMetadata(decCTV_Ckey);
            this.dtFormDesign = dataSets.dtGetFormDesign(decCTV_Ckey);

            //TODO: could use a dtFooter/Body/Footer instead: modify IFormDesignDataSets

            AddFillFormDesignProperties();
            //CreateHeader();
            CreateFormDesignTree();
            CreateFooter();
        }

        private void AddFillFormDesignProperties()
        {
            DataRow dr = dtHeaderDesign.Rows[0];
            {
                string shortName = dr["ShortName"].ToString().Replace(" ", "");  //used for creating filenames etc..; sample: “Adrenal.Res”; spaces are removed  
                string releaseVersionSuffix = dr["ReleaseVersionSuffix"].ToString();  //e.g., CTP1, RC2, REL; UNK if a value is missing
                string title = dr["OfficialName"].ToString();
                string lineage = dr["ChecklistTemplateVersionCkey"].ToString().Replace(".1000043", "");//remove the eCC namespace suffix ".1000043"
                string version = (dr["VersionID"].ToString()).Replace(".1000043", "") + "." + releaseVersionSuffix;//remove the eCC namespace suffix ".1000043"
                string id = lineage + "_" + version + "_sdcFDF";  //FDF = "Form Design File".  This distinguishes the CTV_Ckey (Forms) from CTI_ Ckeys (Items).
                                                               //id - has format like: 129.3.001.001.CTP1 – note the 3 components, and the “.” separator
                string required = dr["Restrictions"].ToString().Contains("optional") ? "false" : "true"; //determines if accreditation applies
                string AJCCversion = dr["AJCC_UICC_Version"].ToString();
                string FIGOversion = dr["FIGO_Version"].ToString();

                var fd = new FormDesignType(this, null, false, id); //create the form with the new id. Inject "this" SDCTreeBuilderEcc object into the form;

                this.FormDesign = fd;  //This eCC-specific tree builder needs a copy of the new form object.
                                       //Note that we have a 2-way reference here:  fd holds a copy of "this" (the eCC tree buiilder), and "this" holds a copy of fd.
                                       //This is a bidirectional dependency injection, allowing fd to call eCC-specific tree builder functions, 
                                       //and "this" (The eCC tree builder) to assemble the eCC tree with fd components, and fill it with eCC content.
                

                //Some basic fd properties
                fd.baseURI = "https://www.cap.org/eCC/SDC.3.1" + "/" + shortName;  //uses SDC Schema version (3.1) is “SDC.3.1”
                fd.lineage = lineage;
                fd.version = version;
                //fullURI - format like: https://www.cap.org/eCC/SDC.3.11/AdrenalRes/129.3.000.001.CTP1            
                fd.fullURI = fd.baseURI + "/" + fd.ID;  //Note that “/” is used for URI used instead of “_”
                fd.filename = id + ".xml";  // "SDC.3.11_FDF_" + shortName + "_" + fd.ID; //format like: SDC.3.11_FDF_Adrenal.Res_129.3.001.001.CTP1
                fd.formTitle = title;


                //+PROPERTIES

                //Copyright notice:
                var yr = DateTime.Now.Year.ToString();
                CreateStaticProperty(fd,
                    "(c) " + yr + " College of American Pathologists.  All rights reserved.  License required for use.",  //title
                    "CAPeCC_static_text", "copyright", "Copyright", //type, style, Propertyname
                    false, "The displayed copyright year represents the year that this XML file was generated", //comment
                    null, null);

                //CAPeCC_CAP_Protocol poperties
                CreateStaticProperty(fd, dr["GenericHeaderText"].ToString(), "CAPeCC_static_text", "", "GenericHeaderText", false, string.Empty, null, string.Empty, "GenericHeaderText"); //, "CAPeCC_CAP_Protocol");
                CreateStaticProperty(fd, dr["Category"].ToString(), "CAPeCC_meta", string.Empty, "Category", false, string.Empty, null, string.Empty, "Category"); //, "CAPeCC_CAP_Protocol");
                CreateStaticProperty(fd, dr["OfficialName"].ToString(), "CAPeCC_meta", string.Empty, "OfficialName", false, string.Empty, null, string.Empty, "OfficialName");  //, "CAPeCC_CAP_Protocol");
                CreateStaticProperty(fd, dr["CAPProtocolVersion"].ToString(), "CAPeCC_meta", string.Empty, "CAPProtocolVersion", false, string.Empty, null, string.Empty, "CAPProtocolVersion");  //, "CAPeCC_CAP_Protocol");
                //CreateStaticProperty(fd, dr["SDCSchemaVersion"].ToString(), "CAPeCC_meta", string.Empty, "SDCSchemaVersion", false, string.Empty, null, string.Empty, "SDCSchemaVersion");  //, "CAPeCC_CAP_Protocol");

                CreateStaticProperty(fd, dr["Restrictions"].ToString(), "CAPeCC_meta", string.Empty, "Restrictions", false, string.Empty, null, string.Empty, "Restrictions");  //, "CAPeCC_CAP_Protocol");
                CreateStaticProperty(fd, required, "CAPeCC_meta", string.Empty, "CAP_Required", false, string.Empty, null, string.Empty, string.Empty);  //, "CAPeCC_CAP_Protocol");
                //need CAP Protocol version #
                //need SDC Schema version?

                //Dates
                //Populate database for Effective Date
                CreateStaticProperty(fd, MapDBNullToDateTime(dr["EffectiveDate"]).ToString(), "CAPeCC_meta dt.dateTime", "", "AccreditationDate", false, string.Empty, null, string.Empty, "AccreditationDate"); //, "CAPeCC_Dates");
                CreateStaticProperty(fd, MapDBNullToDateTime(dr["WebPostingDate"]).ToString(), "CAPeCC_meta dt.dateTime", "", "WebPostingDate", false, string.Empty, null, string.Empty, "WebPostingDate"); //, "CAPeCC_Dates");

                //CAPeCC_Data_Sources
                CreateStaticProperty(fd, shortName, "CAPeCC_meta", string.Empty, "ShortName", false, string.Empty, null, string.Empty, "ShortName"); //, "CAPeCC_Data_Sources");
                CreateStaticProperty(fd, releaseVersionSuffix, "CAPeCC_meta", string.Empty, "ApprovalStatus", false, string.Empty, null, string.Empty, "ApprovalStatus"); //, "CAPeCC_Data_Sources");
                if (!string.IsNullOrEmpty(AJCCversion)) CreateStaticProperty(fd, AJCCversion, "CAPeCC_meta", "", "AJCC_Version", false, string.Empty, null, string.Empty, "AJCC_Version"); //, "CAPeCC_Data_Sources");
                if (!string.IsNullOrEmpty(FIGOversion)) CreateStaticProperty(fd, FIGOversion, "CAPeCC_meta", "", "FIGO_Version", false, string.Empty, null, string.Empty, "FIGO_Version"); //, "CAPeCC_Data_Sources");

            }

        }




        #endregion


        #region Actions

        public override ActSendMessageType AddFillActSendMessage(ThenType tt, Boolean fillData = true)
        {
            var asmt = new ActSendMessageType(tt);
            if (tt.Items != null) tt.Items = new SDC.ExtensionBaseType[25]; //xsd2code generated an array instead of a list here.  
                                                                            //It's not clear if sizing the array at 25 will generate an error for the null entries ????  
                                                                            //It will probably work, but it's not clear.
                                                                            //TODO: Consider a function to auto-resize the array 
                                                                            //TODO: Follow up bug report with xsd2code - this should be a List, not an array
            tt.Items[tt.Items.Length] = asmt;

            var p = new PropertyType(asmt);
            var html = new SDC.HTML_Stype(asmt);
            p.TypedValue.Item = html;

            asmt.Property.Add(p);
            //var DataRepo.dr = DataRepo.dr;

            if (html.Any == null) html.Any = new List<XmlElement>();

            if (fillData)
            {
                asmt.val = (string)drFormDesign["ActSendMessage"];
                html.Any.Add(StringToXMLElement((string)drFormDesign["ActSendMessageHTML"]));
            }
            return asmt;

        }
        public override ActActionType AddAction(ThenType tt, Boolean fillData = true)
        { return new ActActionType(tt); }
        //public override ActSetPropertyType AddSetProperty(ThenType tt, Boolean fillData = true)
        //{ return new ActSetPropertyType(tt) ; }
        public override ActAddCodeType AddAddCode(ThenType tt, Boolean fillData = true)
        { return new ActAddCodeType(tt); }
        //public override ActSetValueType AddSetValue(ThenType tt, Boolean fillData = true)
        //{ return new ActSetValueType(tt); }
        public override ActInjectType AddInject(ThenType tt, Boolean fillData = true)
        { return new ActInjectType(tt); }
        public override ActShowMessageType AddShowMessage(ThenType tt, Boolean fillData = true)
        { return new ActShowMessageType(tt); }
        //public override ExpressionType AddRunCommand(ThenType tt, Boolean fillData = true)
        //{ return new ExpressionType(tt); }
        public override FuncType AddShowURL(ThenType tt, Boolean fillData = true)
        { return new FuncType(tt); }
        public override ActShowFormType AddShowForm(ThenType tt, Boolean fillData = true)
        { return new ActShowFormType(tt); }
        public override ActSaveResponsesType AddSave(ThenType tt, Boolean fillData = true)
        { return new ActSaveResponsesType(tt); }
        public override ActSendReportType AddShowReport(ThenType tt, Boolean fillData = true)
        { return new ActSendReportType(tt); }
        public override ActSendMessageType AddSendMessage(ThenType tt, Boolean fillData = true)
        { return new ActSendMessageType(tt); }
        public override ActValidateFormType AddValidateForm(ThenType tt, Boolean fillData = true)
        { return new ActValidateFormType(tt); }
        public override IfThenType AddIfThen(ThenType tt, Boolean fillData = true)
        { return new IfThenType(tt); }
        public override ItemNameType AddCallIfThen(ThenType tt, Boolean fillData = true)
        { return new ItemNameType(tt); }



        #endregion

        #region Base Types

        protected override BaseType FillBaseTypeItemData(BaseType bt)
        {
            bt.type = (string)drFormDesign["type"];
            bt.styleClass = (string)drFormDesign["styleClass"];
            bt.name = CreateName(bt);
            //bt.order = bt.ObjectID;  //removed 3/25/2018; added to BaseType constructor so that all subclasses output @order 


            //the items below would be better placed under FillIdentifiedItemTypeData
            //bt.ParentIETypeID = drFormDesign["ParentItemCkey"].ToString();

            var rowType = GetEccRowTypeEnum((int)drFormDesign["ItemTypekey"]);
            if (rowType == ItemTypeEnum.DisplayedItem && (drFormDesign["HasChildren"].ToString().Equals("true")))
                rowType = ItemTypeEnum.Section;  //DisplayedType Items (Notes) cannot have child nodes.  If we find one, convert it to a section
            //bt.styleClass = "noteStyle";
            bt.NodeType = rowType;

            return bt;
        }

        /// <summary>
        /// If an Identified ancestor (the closest ancestor with an IdentifiedExtensionType) has a name,
        /// then we can it's name and ID to name non-repeating child elements such as ResponseField
        /// 
        /// </summary>
        /// <param name="prefix">prefix iindicating the type of object to name. The default is "BT" for BaseType</param>
        /// <param name="bt">an object of type BaseType</param>
        /// <returns></returns>

        protected string CreateName(BaseType bt)
        {
            if (!bt.GetType().IsSubclassOf(typeof(DisplayedType)))
            {
                //HACK: Assign names for testing; names need to be fixed strings assigned in the database.
                IdentifiedExtensionType iet = bt.ParentIETypeObject;
                string shortText = iet.name;
                string prefix = "BT";

                //Console.Write(bt.GetType().ToString());

                if (!string.IsNullOrWhiteSpace(shortText))
                {
                    switch (bt.GetType().ToString())
                    {
                        case "SDC.ResponseFieldType":
                            prefix = "rf_";
                            break;
                        case "SDC.ResponseType":
                            prefix = "rsp_";
                            break;
                        case "SDC.PropertyType":
                            prefix = "p_";
                            break;

                        case "SDC.ListItemResponseFieldType":
                            prefix = "lirf_";
                            break;
                        case "SDC.ListFieldType":
                            prefix = "lf_";
                            break;
                        case "SDC.string_DEtype":
                            prefix = "str_";
                            break;
                        case "SDC.integer_DEtype":
                            prefix = "igr_";
                            break;
                        case "SDC.int_DEtype":
                            prefix = "int_";
                            break;
                        case "SDC.long_DEtype":
                            prefix = "lng_";
                            break;
                        case "SDC.decimal_DEtype":
                            prefix = "dec_";
                            break;
                        case "SDC.CodedValueType":
                            prefix = "cod_";
                            break;
                        case "SDC.ContactType":
                            prefix = "con_";
                            break;
                        case "SDC.LinkType":
                            prefix = "lnk_";
                            break;



                        case "":
                        default:
                            prefix = "_" + bt.GetType().ToString();
                            break;

                    }
                    string ancestorID = Decimal.Truncate(Convert.ToDecimal(iet.ID)).ToString();  //could throw errors
                    string objectCtr = bt.ObjectID.ToString();
                    return prefix + shortText + "_" + objectCtr + '_' + ancestorID;
                }
                else { }
            }
            return bt.name;
        }

        protected override CommentType FillCommentData(BaseType bt, string comment)
        {
            //TODO:  support Comment in database and DataTable

            CommentType ct = null;
            if (!string.IsNullOrWhiteSpace(comment))
            {
                ct = new CommentType(bt);
                ct.val = comment;
            }
            return ct;
        }

        public override DisplayedType FillDisplayedTypeItemData(DisplayedType dt)
        {
            dt.enabled = (bool)drFormDesign["enabled"];
            dt.visible = (bool)drFormDesign["visible"];
            dt.title = (string)drFormDesign["VisibleText"];
            dt.mustImplement = (bool)drFormDesign["mustImplement"];
            //ToDo: Fix mismatch between bool database value and int enum DisplayedTypeShowInReport
            //dt.showInReport = (DisplayedTypeShowInReport)drFormDesign["showInReport"];

            var repText = drFormDesign["reportText"].ToString();
            if (!string.IsNullOrWhiteSpace(repText)) AddPropertyReportText(dt);

            return dt;
            //addDisplayedTypeToChildItems(rt);
            //Add @ordered
        }

        public override IdentifiedExtensionType FillIdentifiedTypeItemData(IdentifiedExtensionType iet)
        {
            //iet.baseURI = (string)drFormDesign["baseURI"];
            //TODO: support baseURI

            iet.ID = drFormDesign["ChecklistTemplateItemCKey"].ToString();
            return iet;

        }

        public override RepeatingType FillRepeatingTypeItemData(RepeatingType rt)
        {
            rt.maxCard = 1;
            rt.minCard = 1;

            ushort usResult;
            if (ushort.TryParse(drFormDesign["maxCard"].ToString(), out usResult))
                rt.maxCard = usResult;
            if (ushort.TryParse(drFormDesign["minCard"].ToString(), out usResult))
                rt.minCard = usResult;

            //!eCC Special handling Conditional Reporting with "?"
            if (rt.title.StartsWith("?"))
            {
                rt.mustImplement = true;
                rt.title.TrimStart('?');
            }

            //!eCC Special handling for Authority Required
            var authorityRequired = (bool)drFormDesign["authorityRequired"];
            if (authorityRequired)
            {
                //n.required = true;
                if (rt.minCard == 0) rt.minCard = 1;
                rt.mustImplement = true;
            }
            else
                //n.required = false;
                if (rt.minCard > 0) rt.minCard = 0;

            return rt;
        }

        #endregion

        #region ChildItems

        #region Generics

        public override InjectFormType AddInjectedForm<T>(T T_Parent, Boolean fillData = true, string id = null)
        {
            var childItems = AddChildItemsNode(T_Parent);//TODO:need to first instantiate the List
            var injForm = new InjectFormType(childItems);
            AddExtensionBaseTypeItems(injForm);

            childItems.ListOfItems.Add(injForm);
            //reeBuilder.AddInjectedFormItems(injForm);

            if (fillData)
            {
                //iform.injectionID = a0;  needs to be string format??
                injForm.packageID = (string)drFormDesign["packageID"];
                injForm.rootItemID = (string)drFormDesign["rootItemID"];
                injForm.baseURI = (string)drFormDesign["baseURI"];
                injForm.packageID = (string)drFormDesign["packageID"];
                injForm.ID = (string)drFormDesign["ID"];


                //response properties
                injForm.packageID = (string)drFormDesign["packageID"];
                injForm.rootItemID = (string)drFormDesign["rootItemID"];
                injForm.injectionID = (string)drFormDesign["injectionID"];

            }



            return injForm;
        }

        #endregion

        public override ButtonItemType FillButton(ButtonItemType button)
        { throw new NotImplementedException(); }

        public override InjectFormType FillInjectedForm(InjectFormType injForm)
        {
            //iform.injectionID = a0;  needs to be string format??
            injForm.packageID = (string)drFormDesign["packageID"];  //TODO: replace with real data
            injForm.rootItemID = (string)drFormDesign["rootItemID"];  //TODO: replace with real data
            injForm.baseURI = (string)drFormDesign["baseURI"];  //TODO: replace with real data
            injForm.ID = (string)drFormDesign["ID"];  //TODO: replace with real data


            //response properties
            //injForm.packageID = (string)drFormDesign["packageID"];  //TODO: replace with real data
            //injForm.rootItemID = (string)drFormDesign["rootItemID"];  //TODO: replace with real data

            return injForm;
        }

        protected override SectionItemType FillSection(SectionItemType s)
        {
            FillSectionBase(s);
            //add ResponseReportingAttributes (for SubmitForm)
            return s;
        }
        public override SectionBaseType FillSectionBase(SectionBaseType s)
        {
            s.ordered = (bool)drFormDesign["ordered"];
            return s;
        }

        #endregion

        #region Coding

        public override CodingType AddCodedValue(DisplayedType dt, Boolean fillData = true)
        {
            var coding = new CodingType(dt);

            var codingList = AddCodingList(dt);
            codingList.Add(coding);
            var richText = new RichTextType(coding);
            AddFillHTML(richText, fillData);  //create AddHTML method to RichTextType partial class

            if (fillData)
            {
                coding.CodeMatch = (CodeMatchType)drFormDesign["CodeMatch"];  //this will need work for enums
                coding.Code.val = (string)drFormDesign["Code"];
                richText.val = (string)drFormDesign["CodeText"];
            }
            coding.CodeText = richText;

            AddFillCodeSystem(coding, fillData);

            return coding;
        }

        /// <summary>
        /// Handles Response derived from a LookupEndpoint
        /// </summary>
        /// <param name="lep">LookupEndPointType</param>
        /// <param name="dr">DataRow</param>
        /// <returns></returns>
        public override CodingType AddCodedValue(LookupEndPointType lep, Boolean fillData = true)
        {

            var coding = new CodingType(lep);

            var codingList = AddCodingList(lep);
            codingList.Add(coding);

            var richText = new RichTextType(coding);
            AddFillHTML(richText, fillData);  //create AddHTML method to RichTextType partial class
            coding.CodeText = richText;

            if (fillData)
            {
                coding.CodeMatch = (CodeMatchType)drFormDesign["CodeMatch"];  //this will need work for enums
                coding.Code.val = (string)drFormDesign["Code"];
                richText.val = (string)drFormDesign["CodeText"];
            }

            AddFillCodeSystem(coding, fillData);

            return coding;
        }

        public override CodeSystemType FillCodeSystemItems(CodeSystemType cs)
        {
            cs.CodeSystemName.val = (string)drFormDesign["CodeSystemName"];
            cs.CodeSystemURI.val = (string)drFormDesign["CodeSystemURI"];
            cs.OID.val = (string)drFormDesign["CodeSystemOID"];
            cs.ReleaseDate.val = (DateTime)drFormDesign["CodeSystemReleaseDate"];
            cs.Version.val = (string)drFormDesign["CodeSystemVersion"];


            return cs;
        }

        protected override CodingType FillCodedValue(DisplayedType dt)
        { throw new NotImplementedException(); }

        #endregion

        #region Contacts

        public override AddressType AddFillAddress(OrganizationType ot, Boolean fillData = true)
        { throw new NotImplementedException(); }

        public override AddressType AddFillAddress(PersonType pt, Boolean fillData = true)
        { throw new NotImplementedException(); }

        public override OrganizationType AddFillOrganizationItems(OrganizationType ot, Boolean fillData = true)
        {
            ot.OrgName = new string_Stype(ot);
            ot.Usage = new string_Stype(ot);
            ot.Department = new string_Stype(ot);

            var person = AddContactPerson(ot);  //This function will add all the person details to the Person list

            ot.Email = new List<EmailType>();
            var email = new EmailType(ot);
            ot.Email.Add(email);


            var ph1 = new PhoneType(ot);
            var pn = new PhoneNumberType(ph1);
            ph1.PhoneNumber = pn;
            ot.Phone = new List<PhoneType>();
            ot.Phone.Add(ph1);

            ot.Role = new List<string_Stype>();
            var role = new string_Stype(ot);
            ot.Role.Add(role);

            var address1 = AddFillAddress(ot);
            address1.AddressLine = new List<string_Stype>();
            var addrLine1 = new string_Stype(ot);
            address1.AddressLine.Add(addrLine1);
            ot.Identifier = new List<IdentifierType>();


            var id1 = new IdentifierType(ot);
            ot.Identifier.Add(id1);

            ot.WebURL = new List<anyURI_Stype>();
            var webURL = new anyURI_Stype(ot);
            ot.WebURL.Add(webURL);

            if (fillData)
            {
                ot.OrgName.val = (string)drFormDesign["OrgName"];
                ph1.PhoneType1.val = (string)drFormDesign["PhoneType1"];
                pn.val = (string)drFormDesign["PhoneNumber1"];
                ot.Department.val = (string)drFormDesign["Department1"];
                role.val = (string)drFormDesign["OrgRole1"];
                id1.val = (string)drFormDesign["OrgID1"];
                ot.Usage.val = (string)drFormDesign["Usage"];
                webURL.val = (string)drFormDesign["WebURL"];
            }

            return ot;
        }

        protected override AddressType FillAddress(AddressType at)
        { throw new NotImplementedException(); }

        protected override ContactType FillContact(ContactType ct)
        { throw new NotImplementedException(); }

        protected override string FillPersonAddress1(string ot)
        { throw new NotImplementedException(); }

        protected override string FillPersonAddress2(string ot)
        { throw new NotImplementedException(); }

        protected override string FillPersonAddress3(string ot)
        { throw new NotImplementedException(); }

        protected override string FillPersonAddress4(string ot)
        { throw new NotImplementedException(); }

        protected override EmailType FillPersonEmail(EmailType ot)
        { throw new NotImplementedException(); }

        protected override IdentifierType FillPersonIdentifier(IdentifierType ot)
        { throw new NotImplementedException(); }

        protected override JobType FillPersonJob(JobType pt)
        { throw new NotImplementedException(); }

        //public override NameType AddPersonName(PersonType pt)
        //{ throw new NotImplementedException(); }
        //public override JobType AddJob(PersonType pt)
        //{ throw new NotImplementedException(); }
        //public override EmailType AddEmail(OrganizationType ot)
        //{ throw new NotImplementedException(); }
        //public override EmailType AddEmail(PersonType pt)
        //{ throw new NotImplementedException(); }
        //public override PhoneType AddPhone(OrganizationType ot)
        //{ throw new NotImplementedException(); }
        //public override PhoneType AddPhone(PersonType ot)
        //{ throw new NotImplementedException(); }
        //public override IdentifierType AddIdentifier(OrganizationType ot)
        //{ throw new NotImplementedException(); }
        //public override anyURI_Stype AddWebURL(OrganizationType ot)
        //{ throw new NotImplementedException(); }

        protected override NameType FillPersonName(PersonType pt)
        { throw new NotImplementedException(); }

        protected override PhoneType FillPersonPhone(PhoneType ot)
        { throw new NotImplementedException(); }

        protected override string_Stype FillPersonUsage(string_Stype ut)
        { throw new NotImplementedException(); }

        protected override string FillPersonWebURL(string ot)
        { throw new NotImplementedException(); }

        #endregion

        #region Custom eCC Methods

        private void CreateHeader()
        {

            var h = AddHeader(false);

            foreach (DataRow dr in dtHeaderDesign.Rows)
            {

                //this section is no longer used
                //Copyright notice:
                var yr = DateTime.Now.Year.ToString();
                CreateStaticProperty(h,
                    "(c) " + yr + " College of American Pathologists.  All rights reserved.  License required for use.",  //title
                    "meta", "copyright", "Copyright", //type, style, name
                    true, "The displayed copyright year represents the year that this XML file was generated", //comment
                    null, null);

                CreateStaticProperty(h, "CAP approved", string.Empty, "float-right");

                var CTV_Ckey = dr["ChecklistTemplateVersionCkey"].ToString();
                var title = dr["OfficialName"].ToString();
                string required = dr["Restrictions"].ToString().Contains("optional") ? "false" : "true"; //rlm 2015_12_11 changed to better reflect optionality

                int pos = title.IndexOf(":");
                if (pos > 0)
                    h.title = title.Substring(0, pos);
                //truncate at first colon}
                else
                    h.title = title;

                h.styleClass = "left";
                h.ID = "Header" + "." + CTV_Ckey;

                //CreateStaticOtherText(h, "", "", "", ""); //parent, title, type, style, name,       comment, XmlElement xml, xhtml
                CreateStaticProperty(h, dr["GenericHeaderText"].ToString(), "meta", "left", "GenericHeaderText", false, string.Empty, null, string.Empty, "GenericHeaderText", "eCC_CAP_Protocol");
                CreateStaticProperty(h, dr["OfficialName"].ToString(), "meta", string.Empty, "OfficialName", false, string.Empty, null, string.Empty, "OfficialName", "eCC_CAP_Protocol");
                CreateStaticProperty(h, dr["Category"].ToString(), "meta", string.Empty, "Category", false, string.Empty, null, string.Empty, "Category", "eCC_CAP_Protocol");
                CreateStaticProperty(h, dr["Restrictions"].ToString(), "meta", string.Empty, "Restrictions", false, string.Empty, null, string.Empty, "Restrictions", "eCC_CAP_Protocol");
                CreateStaticProperty(h, required, "meta", string.Empty, "Required", false, string.Empty, null, string.Empty, string.Empty, "eCC_CAP_Protocol");
                CreateStaticProperty(h, dr["ChecklistTemplateVersionCkey"].ToString(), "meta", string.Empty, "CTV_Ckey", false, string.Empty, null, string.Empty, "CTV_Ckey", "eCC_");
                CreateStaticProperty(h, dr["ChecklistCKey"].ToString(), "meta", string.Empty, "ChecklistCKey", false, string.Empty, null, string.Empty, "ChecklistCKey", "eCC_");
                CreateStaticProperty(h, dr["VersionID"].ToString(), "meta", string.Empty, "VersionID", false, string.Empty, null, string.Empty, "VersionID", "eCC_");
                CreateStaticProperty(h, dr["CurrentFileName"].ToString(), "meta", string.Empty, "CurrentFileName", false, string.Empty, null, string.Empty, "CurrentFileName", "eCC_");
                CreateStaticProperty(h, dr["ApprovalStatus"].ToString(), "meta", string.Empty, "ApprovalStatus", false, string.Empty, null, string.Empty, "ApprovalStatus", "eCC_");
                //CreateStaticOtherText(h, MapDBNullToDateTime(dr["RevisionDate"]).ToString(), "meta dt.dateTime", "right", "RevisionDate");
                CreateStaticProperty(h, MapDBNullToDateTime(dr["EffectiveDate"]).ToString(), "meta dt.dateTime", "right", "EffectiveDate", false, string.Empty, null, string.Empty, "EffectiveDate", "eCC_Dates");
                //CreateStaticOtherText(h, MapDBNullToDateTime(dr["RetireDate"]).ToString(), "meta dt.dateTime", "right", "RetireDate");
                CreateStaticProperty(h, MapDBNullToDateTime(dr["WebPostingDate"]).ToString(), "meta dt.dateTime", "right", "WebPostingDate", false, string.Empty, null, string.Empty, "WebPostingDate", "eCC_Dates");
                CreateStaticProperty(h, dr["AJCC_UICC_Version"].ToString(), "meta", "right", "AJCC_UICC_Version", false, string.Empty, null, string.Empty, "AJCC_UICC_Version", "eCC_Data_Sources");
                CreateStaticProperty(h, dr["CS_Version"].ToString(), "meta", "right", "CS_Version", false, string.Empty, null, string.Empty, "CS_Version", "eCC_Data_Sources");
                //CreateStaticProperty(h, dr["FIGO_Version"].ToString(), "meta", "right", "FIGO_Version");
                CreateStaticProperty(h, dr["FIGO_Version"].ToString(), "meta", "right", "FIGO_Version", false, string.Empty, null, string.Empty, "FIGO_Version", "eCC_Data_Sources");

                //CreateStaticQF(h, "GenericHeaderText" + "." + CTV_Ckey, ItemChoiceType.@string, "Generic Header", dr["GenericHeaderText"].ToString(), "GenericHeaderText", "meta", "left", true, true);  //e.g., Cancer Biomarker Reporting Template
                //CreateStaticQF(h, "OfficialName" + "." + CTV_Ckey, ItemChoiceType.@string, "", dr["OfficialName"].ToString(), "OfficialName", "meta", "left", true, true);
                //CreateStaticQF(h, "Category" + "." + CTV_Ckey, ItemChoiceType.@string, "", dr["Category"].ToString(), "Category", "meta", "left", true, true); //e.g., Endocrine

                //CreateStaticQF(h, "Required" + "." + CTV_Ckey, ItemChoiceType.boolean, "Required", required, "Required", "meta", "left", true, true);

                //CreateStaticQF(h, "CTV_Ckey" + "." + CTV_Ckey, ItemChoiceType.@string, "CTV Ckey", dr["ChecklistTemplateVersionCkey"].ToString(), "ChecklistTemplateVersionCkey", "meta", "left", true, true);
                //CreateStaticQF(h, "ChecklistCKey" + "." + CTV_Ckey, ItemChoiceType.@string, "Checklist CKey", dr["ChecklistCKey"].ToString(), "ChecklistCKey", "meta", "left", true, true);
                //CreateStaticQF(h, "VersionID" + "." + CTV_Ckey, ItemChoiceType.@string, "Version ID", dr["VersionID"].ToString(), "VersionID", "meta", "left", true, true);
                //CreateStaticQF(h, "CurrentFileName" + "." + CTV_Ckey, ItemChoiceType.@string, "FileName", dr["CurrentFileName"].ToString(), "CurrentFileName", "meta", "left", true, true);
                //CreateStaticQF(h, "ApprovalStatus" + "." + CTV_Ckey, ItemChoiceType.@string, "Approval Status", dr["ApprovalStatus"].ToString(), "ApprovalStatus", "meta", "left", true, false);

                //CreateStaticQF(h, "RevisionDate" + "." + CTV_Ckey, ItemChoiceType.@dateTime, "Revision Date", MapDBNullToDateTime(dr["RevisionDate"]), "RevisionDate", "meta", "left", true, false);
                //CreateStaticQF(h, "EffectiveDate" + "." + CTV_Ckey, ItemChoiceType.@dateTime, "Effective Date", MapDBNullToDateTime(dr["EffectiveDate"]), "EffectiveDate", "meta", "left", true, true);
                //CreateStaticQF(h, "RetireDate" + "." + CTV_Ckey, ItemChoiceType.@dateTime, "Retire Date", MapDBNullToDateTime(dr["RetireDate"]), "RetireDate", "meta", "left", true, false);
                //CreateStaticQF(h, "WebPostingDate" + "." + CTV_Ckey, ItemChoiceType.@dateTime, "Web Posting Date", MapDBNullToDateTime(dr["WebPostingDate"]), "WebPostingDate", "meta", "right", true, true);

                //CreateStaticQF(h, "Restrictions" + "." + CTV_Ckey, ItemChoiceType.@string, "Restrictions", dr["Restrictions"].ToString(), "Restrictions", "meta", "left", true, true);
                //CreateStaticQF(h, "AJCC_UICC_Version" + "." + CTV_Ckey, ItemChoiceType.@string, "AJCC-UICC Version", dr["AJCC_UICC_Version"].ToString(), "AJCC_UICC_Version", "meta", "right", true, true);
                //CreateStaticQF(h, "CS_Version" + "." + CTV_Ckey, ItemChoiceType.@string, "CS Version", dr["CS_Version"].ToString(), "CS_Version", "meta", "right", true, true);
                //CreateStaticQF(h, "FIGO_Version" + "." + CTV_Ckey, ItemChoiceType.@string, "FIGO Version", dr["FIGO_Version"].ToString(), "FIGO_Version", "meta", "right", true, true);


                //                CreateStaticOtherText(h, "Surgical Pathology Cancer Case Summary (Checklist)", "", "header_text", "header_text", true, "", null,
                //                                @"
                //            <div 
                //xmlns=""http://www.w3.org/1999/xhtml""
                //xsi:schemaLocation=""http://www.w3.org/1999/xhtml xhtml.xsd""
                //class=""header_text""
                //style=""border-bottom:1px solid black;padding-bottom:0px;text-align:left;"">
                //Surgical Pathology Cancer Case Summary (Checklist)
                //</div>
                //"                                );


            }


        }

        /// <summary>
        /// Create Question-Fillin (QF).  This is a Question Item that takes a Repsonse Field.  In most cases, this method is used to create a read-only field in a form.
        ///
        /// </summary>
        /// <param name="dataType">DataTypes_DEType</param>
        /// <param name="response"></param>
        /// <param name="type"></param>
        /// <param name="style"></param>
        /// <param name="readOnly"></param>
        private QuestionItemType CreateStaticQF<T>(
            T parent,
            string ID,
            ItemChoiceType dataType,
            string title = "",
            object response = null,
            string name = "",
            string type = "",
            string style = "",
            Boolean isReadOnly = true,
            Boolean isVisible = true) where T : BaseType, IParent, new()
        {
            if (response != null || (dataType == ItemChoiceType.@string && response.ToString().Length == 0)) //do not serialize empty objects
            {
                var q = parent.AddQuestion(QuestionEnum.QuestionFill, false);
                q.ID = ID;
                q.title = title;
                q.readOnly = isReadOnly;
                q.visible = isVisible;
                q.NodeType = ItemTypeEnum.QuestionFill;
                AddDataTypesDE(q.ResponseField_Item, response, dataType);
                CreateStaticBaseItems(q, type, style, name);
                //q.RegisterParents(parent);

                return q;
            }
            return null;
            //TODO: ResponseField needs AddDataType and AddFillDataType method
        }

        private QuestionItemType CreateStaticQS<T>(
            T parent,
            ItemChoiceType dataType,
            string title = "",
            object response = null,
            string name = "",
            string type = "",
            string style = "",
            Boolean isReadOnly = true,
            Boolean isVisible = true) where T : BaseType, IParent, new()
        { throw new NotImplementedException(); }

        private QuestionItemType CreateStaticQM<T>(
            T parent,
            ItemChoiceType dataType,
            string title = "",
            object response = null,
            string name = "",
            string type = "",
            string style = "",
            Boolean isReadOnly = true,
            Boolean isVisible = true) where T : BaseType, IParent, new()
        { throw new NotImplementedException(); }

        private PropertyType CreateStaticProperty(
            IdentifiedExtensionType parent,
            string val,
            string type,
            string style,
            string propertyName = "",
            Boolean visible = true,
            string comment = "",
            XmlElement xml = null,
            string xhtml = "",
            string name = "",
            string propertyClass = "",
            bool fillData = false
            )
        {

            var prop = AddProperty(parent, false);
            prop.val = val;
            prop.type = type;
            //prop.name = name;
            prop.styleClass = style;
            prop.order = prop.ObjectID;
            prop.propName = propertyName;
            prop.propClass = propertyClass;
            //p.RegisterParents(parent);

            //if (comment !="") AddRichText<DisplayedType>(parent, xhtml, comment);

            //var p = new RichTextType();
            //CreateStaticComment(p, comment);
            //CreateStaticExtension(p, new XmlElement());
            //AddFillHTML(p, false, xhtml);

            return prop;
        }

        private CodingType CreateStaticCoding(ExtensionBaseType parent)
        {
            var ct = new CodingType(parent);

            return ct;
        }

        private void CreateFooter()
        {
            var f = AddFooter(false);
            foreach (DataRow dr in dtHeaderDesign.Rows)
            {
                //Copyright notice:
                var yr = DateTime.Now.Year.ToString();
                CreateStaticProperty(f,
                    "(c) " + yr + " College of American Pathologists.  All rights reserved.  License required for use.",  //title
                    "meta", "copyright", "CopyrightFooter", //type, style, name
                    true, "The displayed copyright year represents the year that this XML file was generated", //comment
                    null, null);
                //f.styleClass = "left";
                f.ID = "Footer" + "." + FormDesign.ID;
            }
        }

        private BaseType CreateStaticBaseItems(BaseType parent, string type, string style, string name)
        {
            //var p = new RichTextType();
            //var p = AddFillOtherText(FormDesign.Header, false);
            parent.type = type;
            parent.styleClass = style;
            parent.name = name;

            return parent;
        }


        private ExtensionType CreateStaticExtension(ExtensionBaseType parent, XmlElement xml)
        {
            if (xml == null) return null;

            var ext = AddFillExtension(parent, false);
            ext.Any.Add(xml);

            return ext;
        }
        private CommentType CreateStaticComment(ExtensionBaseType parent, string comment)
        {
            var ct = parent.AddFillComment(false);
            ct.val = comment;
            //AddRichText<ExtensionBaseType>(parent, comment);
            return ct;
        }

        public override ExtensionType AddFillExtension(ExtensionBaseType parent, Boolean fillData = true, string inXML = "")
        {
            if (fillData == false && (string.IsNullOrWhiteSpace(inXML))) return null;
            //See AddFillHTML for code sample
            ExtensionType ext = null;

            if (fillData || inXML != string.Empty)
                try
                {
                    if (fillData)
                    {
                        return null; //TODO: Fix for known extensions
                        ext = new ExtensionType(parent);
                        var s = drFormDesign["XMLextension"].ToString();

                        if (!string.IsNullOrEmpty(s))
                        {
                            if (parent.Extension == null) parent.Extension = new List<ExtensionType>();
                            parent.Extension.Add(ext);
                            //ext.RegisterParents(parent);

                            ext.Any.Add(StringToXMLElement(drFormDesign["XMLextension"].ToString()));
                            return ext;
                        }
                    }
                    else if (inXML != string.Empty)
                    {
                        ext = new ExtensionType(parent);
                        if (parent.Extension == null) parent.Extension = new List<ExtensionType>();
                        parent.Extension.Add(ext);
                        //ext.RegisterParents(parent);

                        ext.Any.Add(StringToXMLElement(inXML));
                        return ext;
                    }
                    return ext;
                }
                catch
                { MessageBox.Show("Extensions are not fully implemented yet"); }
            return null;
        }


        #endregion

        #region Data Types

        protected override DataTypes_DEType AddFillDataTypesDE(ListFieldType lft, SectionBaseTypeResponseTypeEnum dataType)
        {   //DefaultListItemDataType
            throw new NotImplementedException();
        }

        protected override DataTypes_DEType AddFillDataTypesDE(ResponseFieldType rfParent)
        {
            rfParent.Response = new DataTypes_DEType(rfParent);

            var dataTypeEnum = new ItemChoiceType();
            //RF.Response.ItemElementName = dataType;
            string itemDataType = drFormDesign["DataType"].ToString();

            switch (itemDataType)
            {
                case "HTML":
                    dataTypeEnum = ItemChoiceType.HTML;
                    if (true)
                    {
                        var dt = new HTML_DEtype(rfParent.Response);
                        dt.Any = new List<XmlElement>();
                        //TODO: dt.Any.Add((XmlElement)drFormDesign["xml"]);  //TODO: replace with real data
                        dt.AnyAttr = new List<XmlAttribute>();
                        //TODO: dt.AnyAttr.Add((XmlAttribute)drFormDesign["attribute"]);  //TODO: replace with real data
                        dt.maxLength = (long)drFormDesign["AnswerMaxChars"];  //TODO: use AnswerMaxChars
                        //TODO: dt.minLength = (long)drFormDesign["minLength"];  //TODO: Missing

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "XML":
                    dataTypeEnum = ItemChoiceType.XML;
                    if (true)
                    {
                        var dt = new XML_DEtype(rfParent.Response);
                        dt.Any = new List<XmlElement>();
                        //TODO: dt.Any.Add((XmlElement)drFormDesign["xml"]);  //TODO: missing
                        //dt.AnyAttr = new List<System.Xml.XmlAttribute>();
                        dt.maxLength = (long)drFormDesign["AnswerMaxChars"];  //TODO: replace with real data
                        //TODO: dt.minLength = (long)drFormDesign["minLength"];  //TODO: replace with real data
                        //TODO: dt.@namespace = (string)dr["namespace"];
                        //TODO: dt.schema = (string)drFormDesign["schema"];  //TODO: missing

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "anyType":
                    dataTypeEnum = ItemChoiceType.anyType;
                    if (true)
                    {
                        var dt = new anyType_DEtype(rfParent.Response);
                        dt.Any = new List<XmlElement>();
                        //TODO: dt.Any.Add((XmlElement)drFormDesign["xml"]);  //TODO: missing
                        dt.AnyAttr = new List<XmlAttribute>();
                        //TODO: dt.AnyAttr.Add((XmlAttribute)drFormDesign["attribute"]);  //TODO: missing
                        dt.maxLength = (long)drFormDesign["AnswerMaxChars"];
                        //TODO: dt.minLength = (long)drFormDesign["minLength"];  //TODO: missing
                        //TODO: dt.@namespace = (string)drFormDesign["namespace"];  //TODO: missing
                        //TODO: dt.schema = (string)drFormDesign["schema"];  //TODO: missing

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "anyURI":
                    dataTypeEnum = ItemChoiceType.anyURI;
                    if (true)
                    {
                        var dt = new anyURI_DEtype(rfParent.Response);
                        dt.val = (string)drFormDesign["DefaultValue"];
                        //TODO: dt.length = (long)drFormDesign["length"];  //TODO: missing - calculated value
                        dt.maxLength = (long)drFormDesign["AnswerMaxChars"];
                        //TODO: dt.minLength = (long)drFormDesign["minLength"];
                        //TODO: dt.pattern = (string)drFormDesign["pattern"];  //TODO: missing

                        rfParent.Response.DataTypeDE_Item = dt;
                        //dt.RegisterParents(rfParent.Response);
                    }
                    break;
                case "base64Binary":
                    dataTypeEnum = ItemChoiceType.base64Binary;
                    if (true)
                    {
                        var dt = new base64Binary_DEtype(rfParent.Response);
                        dt.val = (byte[])drFormDesign["DefaultValue"];
                        //TODO: dt.valBase64 = (string)drFormDesign["val_string"];
                        //TODO: dt.length = (long)drFormDesign["length"];  //TODO: missing
                        dt.maxLength = (long)drFormDesign["AnswerMaxChars"];
                        //TODO: dt.minLength = (long)drFormDesign["minLength"];  //TODO: missing
                        //TODO: dt.mimeType = (string)drFormDesign["mimeType"];  //TODO: missing

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "boolean":
                case "Boolean":
                    dataTypeEnum = ItemChoiceType.boolean;
                    if (true)
                    {
                        var dt = new boolean_DEtype(rfParent.Response);
                        dt.val = (bool)drFormDesign["DefaultValue"];

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "byte":
                    dataTypeEnum = ItemChoiceType.@byte;
                    if (true)
                    {
                        var dt = new byte_DEtype(rfParent.Response);
                        dt.val = (sbyte)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (sbyte)drFormDesign["minExclusive"];//TODO: missing
                        dt.minInclusive = (sbyte)drFormDesign["AnswerMinValue"];//TODO: missing
                        //TODO: dt.maxExclusive = (sbyte)drFormDesign["maxExclusive"];//TODO: missing
                        dt.maxInclusive = (sbyte)drFormDesign["AnswerMaxValue"];//TODO: missing
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        //TODO: dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);//TODO: missing

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "date":
                case "Date":
                    dataTypeEnum = ItemChoiceType.date;
                    if (true)
                    {
                        var dt = new date_DEtype(rfParent.Response);
                        dt.val = (DateTime)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "dateTime":
                case "DateTime":
                    dataTypeEnum = ItemChoiceType.@dateTime;
                    if (true)
                    {
                        var dt = new dateTime_DEtype(rfParent.Response);
                        dt.val = (DateTime)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "dateTimeStamp":
                    dataTypeEnum = ItemChoiceType.@dateTimeStamp;
                    if (true)
                    {
                        var dt = new dateTimeStamp_DEtype(rfParent.Response);
                        dt.val = (DateTime)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "decimal":
                case "Decimal":
                    dataTypeEnum = ItemChoiceType.@decimal;
                    //TODO: dataType = ItemChoiceType.@float;
                    if (true)
                    {
                        var dt = new decimal_DEtype(rfParent.Response);
                        //dt.val = drFormDesign["DefaultValue"] as decimal?;
                        //SDCHelpers.NZ(drFormDesign["DefaultValue"], dt.val);
                        if (drFormDesign["DefaultValue"].ToString() != "") dt.val = (decimal)drFormDesign["DefaultValue"];
                        
                        //TODO: dt.minExclusive = (decimal)drFormDesign["minExclusive"];
                        if (drFormDesign["AnswerMinValue"] != null) dt.minInclusive = (decimal)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (decimal)drFormDesign["maxExclusive"];
                        //SDCHelpers.NZ(drFormDesign["AnswerMaxValue"], dt.maxInclusive);
                        if (drFormDesign["AnswerMaxValue"] != null) dt.maxInclusive = (decimal)drFormDesign["AnswerMaxValue"];
                        //SDCHelpers.NZ(drFormDesign["AnswerMaxChars"], dt.totalDigits);
                        if (drFormDesign["AnswerMaxChars"] != null) dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);//AnswerMaxChars
                        //SDCHelpers.NZ(drFormDesign["AnswerMaxDecimals"], dt.fractionDigits);

                        //TODO:  Need to truncate the fractional digits  according to the fractionDigits number of digits.  This requires converting to string-based properties instead of decimal
                        //see the Integer code for an example
                        if (drFormDesign["AnswerMaxDecimals"] != null) dt.fractionDigits = Convert.ToByte(drFormDesign["AnswerMaxDecimals"]);//AnswerMaxDecimals
                        //TODO: dt.mask = (string)drFormDesign["mask"];//TODO: missing
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "double":
                case "Double":
                    dataTypeEnum = ItemChoiceType.@double;
                    if (true)
                    {
                        var dt = new double_DEtype(rfParent.Response);
                        if (drFormDesign["DefaultValue"] != null) dt.val = (double)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (double)drFormDesign["minExclusive"];
                        dt.minInclusive = (double)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (double)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (double)drFormDesign["AnswerMaxValue"];
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        dt.fractionDigits = Convert.ToByte(drFormDesign["AnswerMaxDecimals"]);//AnswerMaxDecimals
                        //TODO: dt.mask = (string)drFormDesign["mask"];//TODO: missing
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "duration":
                    dataTypeEnum = ItemChoiceType.duration;
                    if (true)
                    {
                        var dt = new duration_DEtype(rfParent.Response);
                        dt.val = (string)drFormDesign["DefaultValue"];   //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (string)drFormDesign["minExclusive"];
                        dt.minInclusive = (string)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (string)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (string)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "float":
                    dataTypeEnum = ItemChoiceType.@float;
                    if (true)
                    {
                        var dt = new float_DEtype(rfParent.Response);
                        dt.val = (float)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (float)drFormDesign["minExclusive"];
                        dt.minInclusive = (float)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (float)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (float)drFormDesign["AnswerMaxValue"];
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        dt.fractionDigits = Convert.ToByte(drFormDesign["AnswerMaxDecimals"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gDay":
                    dataTypeEnum = ItemChoiceType.gDay;
                    if (true)
                    {
                        var dt = new gDay_DEtype(rfParent.Response);
                        dt.val = (string)drFormDesign["DefaultValue"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (string)drFormDesign["minExclusive"];
                        dt.minInclusive = (string)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (string)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (string)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gMonth":
                    dataTypeEnum = ItemChoiceType.gMonth;
                    if (true)
                    {
                        var dt = new gMonth_DEtype(rfParent.Response);
                        dt.val = (string)drFormDesign["DefaultValue"];  //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (string)drFormDesign["minExclusive"];
                        dt.minInclusive = (string)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (string)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (string)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gMonthDay":
                    dataTypeEnum = ItemChoiceType.gMonthDay;
                    if (true)
                    {
                        var dt = new gMonthDay_DEtype(rfParent.Response);
                        dt.val = (string)drFormDesign["DefaultValue"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (string)drFormDesign["minExclusive"];
                        dt.minInclusive = (string)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (string)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (string)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gYear":
                    dataTypeEnum = ItemChoiceType.gYear;
                    if (true)
                    {
                        var dt = new gYear_DEtype(rfParent.Response);
                        dt.val = (string)drFormDesign["DefaultValue"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (string)drFormDesign["minExclusive"];
                        dt.minInclusive = (string)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (string)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (string)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "gYearMonth":
                    dataTypeEnum = ItemChoiceType.gYearMonth;
                    if (true)
                    {
                        var dt = new gYearMonth_DEtype(rfParent.Response);
                        dt.val = (string)drFormDesign["DefaultValue"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (string)drFormDesign["minExclusive"];
                        dt.minInclusive = (string)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (string)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (string)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "hexBinary":
                    dataTypeEnum = ItemChoiceType.hexBinary;
                    if (true)
                    {
                        var dt = new hexBinary_DEtype(rfParent.Response);
                        dt.val = (byte[])drFormDesign["DefaultValue"];
                        //TODO: dt.valHex = (string)drFormDesign["val_string"];//TODO: missing
                        dt.length = (long)drFormDesign["length"];
                        dt.maxLength = (long)drFormDesign["AnswerMaxChars"];//AnswerMaxChars
                        //TODO: dt.mimeType = (string)drFormDesign["mimeType"];//TODO: missing
                        dt.minLength = (long)drFormDesign["minLength"];

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "int":
                case "Int":
                    dataTypeEnum = ItemChoiceType.@int;
                    if (true)
                    {
                        var dt = new int_DEtype(rfParent.Response);
                        dt.val = (int)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (int)drFormDesign["minExclusive"];
                        dt.minInclusive = (int)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (int)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (int)drFormDesign["AnswerMaxValue"];
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "integer":
                case "Integer":

                    dataTypeEnum = ItemChoiceType.integer;
                    if (true)
                    {
                        //TODO:  bug in xsdCode++ - uses wrong data type - uses string because there is no integer (truncated decimal) format in .NET
                        //However, we test to ensure that the database value is a long- it would be better to test against a truncated decimal
                        var dt = new integer_DEtype(rfParent.Response);
                        if (drFormDesign["DefaultValue"] is decimal) dt.val = decimal.Truncate((decimal)drFormDesign["DefaultValue"]).ToString();  //dt.valDec = (decimal)drFormDesign["DefaultValue"];
                        //dt.val= Convert.ToDecimal(drFormDesign["DefaultValue"]);   //TODO:  bug in xsdCode++ - wrong data type

                        //if (drFormDesign["minExclusive"] is long) dt.minInclusive = (string)drFormDesign["minExclusive"];  //not in table
                        //if (drFormDesign["maxExclusive"] is long) dt.minInclusive = (string)drFormDesign["maxExclusive"];  //not in table


                        if (drFormDesign["AnswerMinValue"] is decimal) dt.minInclusive = decimal.Truncate((decimal)drFormDesign["AnswerMinValue"]).ToString();
                        if (drFormDesign["AnswerMaxValue"] is decimal) dt.maxInclusive = decimal.Truncate((decimal)drFormDesign["AnswerMaxValue"]).ToString();

                        if (drFormDesign["AnswerMaxChars"] is byte) dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "long":
                case "Long":
                    dataTypeEnum = ItemChoiceType.@long;
                    if (true)
                    {
                        var dt = new long_DEtype(rfParent.Response);
                        dt.val = (long)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (long)drFormDesign["minExclusive"];
                        dt.minInclusive = (long)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (long)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (long)drFormDesign["AnswerMaxValue"];
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "negativeInteger":
                    dataTypeEnum = ItemChoiceType.negativeInteger;
                    if (true)
                    {
                        var dt = new negativeInteger_DEtype(rfParent.Response);
                        dt.val = drFormDesign["DefaultValue"].ToString(); ;  //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (System.Nullable<decimal>)drFormDesign["minExclusive"];
                        dt.minInclusive = drFormDesign["AnswerMinValue"].ToString();
                        //TODO: dt.maxExclusive = (System.Nullable<decimal>)drFormDesign["maxExclusive"];
                        dt.maxInclusive = drFormDesign["AnswerMaxValue"].ToString();
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "nonNegativeInteger":
                    dataTypeEnum = ItemChoiceType.nonNegativeInteger;
                    if (true)
                    {
                        var dt = new nonNegativeInteger_DEtype(rfParent.Response);
                        dt.val = drFormDesign["DefaultValue"].ToString();   //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (System.Nullable<decimal>)drFormDesign["minExclusive"];
                        dt.minInclusive = drFormDesign["AnswerMinValue"].ToString();
                        //TODO: dt.maxExclusive = (System.Nullable<decimal>)drFormDesign["maxExclusive"];
                        dt.maxInclusive = drFormDesign["AnswerMaxValue"].ToString();
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "nonPositiveInteger":
                    dataTypeEnum = ItemChoiceType.nonPositiveInteger;
                    if (true)
                    {
                        var dt = new nonPositiveInteger_DEtype(rfParent.Response);
                        dt.val = drFormDesign["DefaultValue"].ToString(); ;  //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (System.Nullable<decimal>)drFormDesign["minExclusive"];
                        dt.minInclusive = drFormDesign["AnswerMinValue"].ToString();
                        //TODO: dt.maxExclusive = (System.Nullable<decimal>)drFormDesign["maxExclusive"];
                        dt.maxInclusive = drFormDesign["AnswerMaxValue"].ToString();
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "positiveInteger":
                    dataTypeEnum = ItemChoiceType.positiveInteger;
                    if (true)
                    {
                        var dt = new positiveInteger_DEtype(rfParent.Response);
                        dt.val = drFormDesign["DefaultValue"].ToString(); ;  //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (System.Nullable<decimal>)drFormDesign["minExclusive"];
                        dt.minInclusive = drFormDesign["AnswerMinValue"].ToString();
                        //TODO: dt.maxExclusive = (System.Nullable<decimal>)drFormDesign["maxExclusive"];
                        dt.maxInclusive = drFormDesign["AnswerMaxValue"].ToString();
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "@short":
                    dataTypeEnum = ItemChoiceType.@short;
                    if (true)
                    {
                        var dt = new short_DEtype(rfParent.Response);
                        dt.val = (short)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (short)drFormDesign["minExclusive"];
                        dt.minInclusive = (short)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (short)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (short)drFormDesign["AnswerMaxValue"];
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "string":
                case "String":
                    dataTypeEnum = ItemChoiceType.@string;
                    if (true)
                    {
                        var dt = new @string_DEtype(rfParent.Response);
                        dt.val = (string)drFormDesign["DefaultValue"];
                        dt.maxLength = Convert.ToInt64((drFormDesign["AnswerMaxChars"] is DBNull) ? 80L : drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.minLength = (long)drFormDesign["minLength"];
                        //TODO: dt.pattern = (string)drFormDesign["pattern"];

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "time":
                case "Time":
                    dataTypeEnum = ItemChoiceType.time;
                    if (true)
                    {
                        var dt = new time_DEtype(rfParent.Response);
                        dt.val = (DateTime)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (DateTime)drFormDesign["minExclusive"];
                        dt.minInclusive = (DateTime)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (DateTime)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (DateTime)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedByte":
                    dataTypeEnum = ItemChoiceType.unsignedByte;
                    if (true)
                    {
                        var dt = new unsignedByte_DEtype(rfParent.Response);
                        dt.val = (byte)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (byte)drFormDesign["minExclusive"];
                        dt.minInclusive = (byte)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (byte)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (byte)drFormDesign["AnswerMaxValue"];
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedInt":
                    dataTypeEnum = ItemChoiceType.unsignedInt;
                    if (true)
                    {
                        var dt = new unsignedInt_DEtype(rfParent.Response);
                        dt.val = (uint)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (uint)drFormDesign["minExclusive"];
                        dt.minInclusive = (uint)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (uint)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (uint)drFormDesign["AnswerMaxValue"];
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedLong":
                    dataTypeEnum = ItemChoiceType.unsignedLong;
                    if (true)
                    {
                        var dt = new unsignedLong_DEtype(rfParent.Response);
                        dt.val = (ulong)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (ulong)drFormDesign["minExclusive"];
                        dt.minInclusive = (ulong)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (ulong)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (ulong)drFormDesign["AnswerMaxValue"];
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "unsignedShort":
                    dataTypeEnum = ItemChoiceType.unsignedShort;
                    if (true)
                    {
                        var dt = new unsignedShort_DEtype(rfParent.Response);
                        dt.val = (ushort)drFormDesign["DefaultValue"];
                        //TODO: dt.minExclusive = (ushort)drFormDesign["minExclusive"];
                        dt.minInclusive = (ushort)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (ushort)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (ushort)drFormDesign["AnswerMaxValue"];
                        dt.totalDigits = Convert.ToByte(drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case "yearMonthDuration":
                    dataTypeEnum = ItemChoiceType.yearMonthDuration;
                    if (true)
                    {
                        var dt = new yearMonthDuration_DEtype(rfParent.Response);
                        dt.val = (String)drFormDesign["DefaultValue"]; ;  //TODO:  bug in xsdCode++ - wrong data type
                        //TODO: dt.minExclusive = (String)drFormDesign["minExclusive"];
                        dt.minInclusive = (String)drFormDesign["AnswerMinValue"];
                        //TODO: dt.maxExclusive = (String)drFormDesign["maxExclusive"];
                        dt.maxInclusive = (String)drFormDesign["AnswerMaxValue"];
                        //TODO: dt.mask = (string)drFormDesign["mask"];
                        dt.quantEnum = AssignQuantifier();

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                default:
                    dataTypeEnum = ItemChoiceType.@string;
                    {
                        var dt = new @string_DEtype(rfParent.Response);
                        dt.val = (string)drFormDesign["DefaultValue"];
                        dt.maxLength = Convert.ToInt64((drFormDesign["AnswerMaxChars"] is DBNull) ? 80L : drFormDesign["AnswerMaxChars"]);
                        //TODO: dt.minLength = (long)drFormDesign["minLength"];
                        //TODO: dt.pattern = (string)drFormDesign["pattern"];

                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
            }

            rfParent.Response.ItemElementName = dataTypeEnum;
            FillBaseTypeItem(rfParent.Response.DataTypeDE_Item);
            return rfParent.Response;

        }


        protected override DataTypes_SType AddFillDataTypesS(CodingType coding)
        { throw new NotImplementedException(); }

        public override HTML_Stype AddFillHTML(RichTextType rt, Boolean fillData = true, string InXhtml = "")
        {
            HTML_Stype html = null;

            try
            {
                html = new HTML_Stype(rt);
                rt.RichText = html;
                html.Any = new List<XmlElement>();

                if (fillData)
                {
                    //html.name = "";
                    //html.type = "";
                    //html.styleClass = "";
                    var s = drFormDesign["html"].ToString();
                    if (s != null && s != string.Empty)
                        html.Any.Add(StringToXMLElement(s));
                }
                else if (InXhtml != string.Empty)
                    html.Any.Add(StringToXMLElement(InXhtml));
                return html;
            }
            catch
            {
                MessageBox.Show("AddFillHTML is not implemented");
            }
            return html;

            //TODO: Check XHTML builder here:
            //https://gist.github.com/rarous/3150395,
            //http://www.authorcode.com/code-snippet-converting-xmlelement-to-xelement-and-xelement-to-xmlelement-in-vb-net/
            //https://msdn.microsoft.com/en-us/library/system.xml.linq.loadoptions%28v=vs.110%29.aspx
            return html;

        }

        protected override FuncType AddFillWebService(LookupEndPointType lep, Boolean fillData = true)
        { throw new NotImplementedException(); }

        protected override string FillQuantifier()
        {
            string q;
            try
            {
                q = (string)drFormDesign["quantifier"];
            }
            catch (Exception ex)
            {
                q = "EQ";
            }

            return q;
        }

        protected override FuncType FillWebService(FuncType wst)
        { throw new NotImplementedException(); }

        #endregion

        #region Displayed Type

        #region DisplayedType Events

        public override WatchedPropertyType AddActivateIf(DisplayedType dt, Boolean fillData = true)
        { throw new NotImplementedException(); }

        public override WatchedPropertyType AddDeActivateIf(DisplayedType dt, Boolean fillData = true)
        { throw new NotImplementedException(); }

        public override IfThenType AddOnEnter(DisplayedType dt, Boolean fillData = true)
        { throw new NotImplementedException(); }

        public override IfThenType AddOnEvent(DisplayedType dt, Boolean fillData = true)
        { throw new NotImplementedException(); }

        public override OnEventType AddOnExit(DisplayedType dt, Boolean fillData = true)
        { throw new NotImplementedException(); }

        protected override WatchedPropertyType FillActivateIf(WatchedPropertyType oe)
        { throw new NotImplementedException(); }

        protected override WatchedPropertyType FillDeActivateIf(WatchedPropertyType oe)
        { throw new NotImplementedException(); }

        protected override IfThenType FillOnEnter(IfThenType oe)
        { throw new NotImplementedException(); }

        protected override IfThenType FillOnEvent(IfThenType oe)
        { throw new NotImplementedException(); }

        protected override OnEventType FillOnExit(OnEventType oe)
        { throw new NotImplementedException(); }

        #endregion

        #region OtherText

        protected PropertyType AddPropertyAltText(DisplayedType parent)
        {
            var p = AddProperty(parent, false);
            p.propName = "altText";
            p.val = drFormDesign["LongText"].ToString();
            return p;
        }

        protected PropertyType AddPropertyDescription(DisplayedType parent)
        {
            var p = AddProperty(parent, false);
            p.type = "description";
            p.val = drFormDesign["Description"].ToString();
            return p;
        }

        protected PropertyType AddPropertyInstruction(DisplayedType parent)
        {
            var p = AddProperty(parent, false);
            p.type = "instruction";
            p.val = drFormDesign["Instruction"].ToString();
            p.order = p.ObjectID;
            return p;
        }

        protected PropertyType AddPropertyPopupText(DisplayedType parent)
        {
            var p = AddProperty(parent, false);
            p.type = "popUpText";
            p.val = drFormDesign["PopUpText"].ToString();
            p.order = p.ObjectID;
            return p;
        }

        protected PropertyType AddPropertyReportText(DisplayedType parent)
        {
            var p = AddProperty(parent, false);
            p.val = drFormDesign["ReportText"].ToString();
            if (p.val == "''") p.val = "{no text}";
            if (p.val.StartsWith("]")) p.val = p.val.Remove(1, 1);  //the leading "]" is a flag for QA queries to ignore this text because it was customized by a modeler.
            p.propName = "reportText";
            p.order = p.ObjectID;
            return p;
        }

        protected PropertyType AddPropertyShortText(DisplayedType parent)
        {
            var p = AddProperty(parent, false);
            p.type = "reportTextShort";
            p.val = drFormDesign["ShortText"].ToString();
            p.order = p.ObjectID;
            return p;
        }

        protected PropertyType AddPropertySpanishText(DisplayedType parent)
        {
            var p = AddProperty(parent, false);
            p.type = "spanishText";
            p.val = drFormDesign["panishText"].ToString();
            p.order = p.ObjectID;
            return p;
        }

        protected PropertyType AddPropertyTooltip(DisplayedType parent)
        {
            var p = AddProperty(parent, false);
            p.type = "tooltip";
            p.val = drFormDesign["ControlTip"].ToString();
            p.order = p.ObjectID;
            return p;
        }

        public override PropertyType FillProperty(PropertyType pt, Boolean fillData = true)
        {
            pt.name = CreateName(pt);
            return pt;
        }

        #endregion

        public override BlobType FillBlob(BlobType blob)
        {
            var rtf = new RichTextType(blob);
            rtf.val = (string)drFormDesign["BlobText"];

            if (blob.Description == null) blob.Description = new List<RichTextType>();
            blob.Description.Add(rtf);

            var html = AddFillHTML(rtf);

            var bUri = new anyURI_Stype(blob);
            bUri.val = "https://www.cap.org/ecc/sdc/image1234.jpg";
            blob.Item = bUri;
            blob.order = blob.ObjectID;

            var bin = new base64Binary_DEtype(blob);
            bin.valBase64 = "SGVsbG8=";
            blob.Item = bin;
            //TODO: Note: in code generator for base64Binary_Stype - val is not the string datatype it is a byte array.
            //Description: RichTextType
            //Hash
            //BobURI
            //BinaryMediaBase63

            return blob;
        }

        public override LinkType FillLinkText(LinkType link)
        {
            //link.LinkText = new RichTextType(link);
            link.LinkText.val = (string)drFormDesign["LinkText"];

            var html = AddFillHTML(link.LinkText);  //check this

            link.LinkURI = new anyURI_Stype(link);
            link.LinkURI.val = (string)drFormDesign["LinkURI"];

            var lt = new RichTextType(link);
            if (link.LinkText == null) link.LinkText = lt;

            lt.order = lt.ObjectID;

            lt.val = (string)drFormDesign["LinkDescText"];
            //AddHTML(desc).Any.Add(HTML);
            //Fill the description text here

            //LinkText: HTML Type
            //LinkURI: URI Type: ExtensionBase Type
            //Description: HTML Type
            //....
            return link;
        }

        #endregion


        #region  Organization

        protected override string FillOrgAddress1(string ot)
        { throw new NotImplementedException(); }

        protected override string FillOrgAddress2(string ot)
        { throw new NotImplementedException(); }

        protected override string FillOrgAddress3(string ot)
        { throw new NotImplementedException(); }

        protected override string FillOrgAddress4(string ot)
        { throw new NotImplementedException(); }

        protected override string FillOrgDept(string ot)
        { throw new NotImplementedException(); }

        protected override EmailType FillOrgEmail(EmailType et)
        { throw new NotImplementedException(); }

        protected override IdentifierType FillOrgIdentifier(IdentifierType ot)
        { throw new NotImplementedException(); }

        protected override NameType FillOrgName(PersonType pt)
        { throw new NotImplementedException(); }

        protected override string_Stype FillOrgName(string_Stype ot)
        { throw new NotImplementedException(); }

        protected override PhoneType FillOrgPhone(PhoneType ot)
        { throw new NotImplementedException(); }

        protected override string FillOrgRole(string rt)
        { throw new NotImplementedException(); }

        protected override string FillOrgUsage(string ut)
        { throw new NotImplementedException(); }

        protected override anyURI_Stype FillOrgWebURL(anyURI_Stype wt)
        { throw new NotImplementedException(); }

        #endregion

        #region QAS

        #region Question

        public override QuestionItemBaseType FillQuestionItemBase(QuestionItemBaseType q)
        {
            q.readOnly = (bool)drFormDesign["Locked"];
            return q;
        }
        #endregion

        #region List

        protected override DisplayedType FillDisplayedTypeListItemData(DisplayedType di, string title, QuestionItemType qNode)
        {
            //TODO: remove "title" from parameter list ?
            //TODO: multImplement kludge    Also, could move this to base class bc there is no datarow access here.
            if (qNode.mustImplement == true)
                di.mustImplement = true;
            else
                di.mustImplement = false;





            //TODO: Why are we looking at @name here???  Why are we changing @name???  removed 3/25/2018
            //Probably need to delete this block

            //var n = di.name;
            //var t = di.title;
            //if (!string.IsNullOrWhiteSpace(t) &&
            //    !string.IsNullOrWhiteSpace(n) &&
            //    t.Substring(1, 1) == "+")
            //    di.name = n.Substring(1, n.Length - 1);
            ////di.name = n.Substring(2, n.Length);




            //handle "optional answers" inside a required question
            var t = di.title;
            if (!string.IsNullOrWhiteSpace(t) &&
                t.Substring(0, 1) == "+")
                di.mustImplement = false;
            return di;
        }

        public override ListItemBaseType FillListItemBase(ListItemBaseType li)  //, QuestionItemType qNode)
        {
            li.title = (string)drFormDesign["VisibleText"];
            li.omitWhenSelected = (bool)drFormDesign["omitWhenSelected"];
            li.selected = (bool)drFormDesign["locked"]; //TODO: Add "selected" to database and SQL
            li.selectionDeselectsSiblings = (bool)drFormDesign["selectionDeselectsSiblings"];
            li.selectionDisablesChildren = (bool)drFormDesign["selectionDisablesChildren"];
            li.name = (string)drFormDesign["ShortName"];

            //For eCC, we need a better way to handle "optional" answers on required questions"
            //need to set mustImplement to true for all ListItems
            //var dt = (DisplayedType)li.GetParentIETypeNode;

            //TODO: mustImplement kludge  //removed 3/25/2018
            //var qNode = (QuestionItemType)li.GetParentIETypeNode;

            //if (qNode.mustImplement == true)
            //    li.mustImplement = true;
            //else
            //    li.mustImplement = false;






            //TODO: Why are we looking at @name here???  Why are we changing @name???  removed 3/25/2018
            //Probably need to delete this block

            //var n = li.name;
            //var t = li.title;
            //if (!string.IsNullOrWhiteSpace(t) &&
            //    !string.IsNullOrWhiteSpace(n) &&
            //    t.Substring(0, 1) == "+")
            //    li.name = n.Substring(1, n.Length-1);
            //    //li.name = n.Substring(2, n.Length);





            //handle "optional answers" inside a required question
            var t = li.title;

            if (!string.IsNullOrWhiteSpace(t) &&
                t.Substring(0, 1) == "+")
                li.mustImplement = false;


            //!eCC Special handling Conditional Reporting with "?"
            if (li.title.StartsWith("?"))
            {
                //li.mustImplement = true;
                li.title.TrimStart('?');
                li.omitWhenSelected = true;
            }

            var liType = (int)drFormDesign["ItemTypeKey"];
            if (liType == 20) //Answer Fill-in
                AddListItemResponseField(li);
            //li.OnDeselect;
            //li.OnSelect;
            //li.DeselectIf;
            //li.SelectIf;
            //li.ActivateIf;
            //li.DeActivateIf;
            return li;
        }

        public override ListFieldType FillListField(ListFieldType lf)
        {
            lf.ordered = (bool)drFormDesign["ordered"];
            return lf;
        }

        #endregion

        #region LookupEndpoint

        public override LookupEndPointType FillLookupEndpoint(LookupEndPointType lep)
        {
            //TODO: Needs work to add more metadata
            lep.type = string.Empty;
            lep.styleClass = string.Empty;
            lep.name = string.Empty;
            lep.includesHeaderRow = false;

            lep.Security = new RichTextType(lep);
            lep.Security.val = string.Empty;


            //if (lep.Parameter == null) lep.Parameter = new List<GetParameterFromPropertyType>();
            if (lep.Items == null) lep.Items = new List<ExtensionBaseType>();
            var p = new ParameterItemType111();
            p.paramName = string.Empty;
            p.sourceItemName = "";
            p.SourceItemAttribute = "val";
            lep.Items.Add(p);
            lep.Function.val = drFormDesign["LookupEndpoint"].ToString();

            //AddParameterToLookupEndpoint(LookupEndPointType lep)

            if (lep.ResponseValue == null) lep.ResponseValue = new List<CodingType>();
            //AddResponseValueToLookupEndpoint(LookupEndPointType lep)

            return lep;
        }

        #endregion

        #region Response

        //protected override ReplacedResponseType FillReplacedResponse(ReplacedResponseType replResp)
        //{
        //    MessageBox.Show("FillReplacedResponse not implemented yet");
        //    return replResp;
        //}

        //protected override ResponseChangeType FillResponseChange(ResponseChangeType respChange)
        //{
        //    MessageBox.Show("FillResponseChange not implemented yet");
        //    return respChange;
        //}

        public override ListItemResponseFieldType FillListItemResponseField(ListItemResponseFieldType liRF)
        {
            FillResponseField(liRF);
            liRF.responseRequired = (bool)drFormDesign["responseRequired"];


            //FillListItemResponseField(liRF);
            AddFillResponseUnits(liRF);

            var li = (ListItemType)liRF.ParentNode;

            //Special eCC rule
            if (li.title.ToLower().Contains("specify") ||
                li.title.ToLower().Contains("explain") ||
                li.title.ToLower().Contains("at least")
                )
                liRF.responseRequired = true;

            return liRF;
        }

        //!+Create Response Items

        public override ResponseFieldType FillResponseField(ResponseFieldType rf)
        {   //TODO: Add Response, TextAfterResponse (RichTextType), ReponseUnits, SetValueExpression

            string textAfterResp;
            try
            {
                //this field will not be present in the current datarow when we are filling the form header with read-only data
                textAfterResp = (string)drFormDesign["TextAfterConcept"];
            }
            catch (Exception ex)
            {
                textAfterResp = string.Empty;
            }

            if (textAfterResp != string.Empty)
            {
                rf.TextAfterResponse = new RichTextType(rf); //TextAfterResponse type must be initialized
                rf.TextAfterResponse.val = textAfterResp; //TODO: edit SQL and database to read "TextAfterResponse"
            }

            return rf;
        }



        protected override UnitsType AddFillResponseUnits(ResponseFieldType rf, Boolean fillData = true)
        {
            UnitsType u = null;


            if (fillData)
            {
                string respUnits = (string)drFormDesign["AnswerUnits"];

                if (respUnits != string.Empty)
                { AddUnits(rf, fillData); }
            }
            return u;
        }

        public override UnitsType FillUnits(UnitsType ut)
        {
            ut.unitSystem = "UCOM";
            ut.val = drFormDesign["AnswerUnits"].ToString();
            return ut;
        }
        #endregion

        #endregion

        #region Resources


        /// <summary>
        /// Maps a DateTime value
        /// </summary>
        /// <param name="value">value to be mapped</param>
        /// <returns>return datetime if value is not DBNull or else returns null</returns>
        public static DateTime? MapDBNullToDateTime(object value)
        {
            if (value == DBNull.Value)
                return null;

            try
            {
                var dateTime = Convert.ToDateTime(value);
                return dateTime;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static ItemChoiceType ConvertEccDataTypeToSDC(string EccDataType)
        {  //TODO: add date and duration types to eCC and here
            ItemChoiceType sdcDataType = new ItemChoiceType();
            switch (EccDataType.ToLower())
            {
                case "gid":
                case "guid":
                case "oid":
                case "string255":
                case "string":
                    sdcDataType = ItemChoiceType.@string;
                    break;
                case "memo":
                    sdcDataType = ItemChoiceType.@string;
                    break;
                case "decimal":
                case "ckey":
                    sdcDataType = ItemChoiceType.@decimal;
                    break;
                case "float":
                case "real":
                    sdcDataType = ItemChoiceType.@float;
                    break;
                case "integer":
                    sdcDataType = ItemChoiceType.integer;
                    break;
                case "short":
                    sdcDataType = ItemChoiceType.@short;
                    break;
                case "long":
                    sdcDataType = ItemChoiceType.@long;
                    break;
                case "byte":
                    sdcDataType = ItemChoiceType.@byte;
                    break;
                case "bit":
                case "boolean":
                    sdcDataType = ItemChoiceType.boolean;
                    break;
                case "image":
                case "binary":
                case "base64binary":
                    sdcDataType = ItemChoiceType.base64Binary;
                    break;
                default:
                    sdcDataType = ItemChoiceType.@string;
                    break;
            }
            return sdcDataType;
        }

        #endregion

        //protected override UnitsType FillUnits(UnitsType ut)
        //{ throw new NotImplementedException(); }

        #region Template Builder

        public static ItemTypeEnum GetEccRowTypeEnum(Int32 rowType)
        {
            /*
                 --List of in- use eCC ItemTypeKeys
                --Key	TypeName
                --4		Q Question - Single-Select (Combo/Option)
                --6		A Answer
                --12	Note
                --17	QQ Question - Fill-in
                --20	AA Answer - Fill-in
                --23	QQQ Question - Multi-Select Checkbox Label
                --24	H Section Header
                --26	ComboBoxNote
             */
            switch (rowType)
            {
                case 1: //Rich Text Node or CHECKLIST
                    return ItemTypeEnum.FormDesign;
                case 2: //Page Header - not coded
                    return ItemTypeEnum.Header;
                case 3: //Page Footer - not coded
                    return ItemTypeEnum.Footer;
                case 4: //QUESTIONSINGLE
                    return ItemTypeEnum.QuestionSingle;
                case 6: //ANSWER
                    return ItemTypeEnum.ListItem;
                case 12: //NOTE
                    return ItemTypeEnum.DisplayedItem;
                case 15: //Section Header - not coded
                    return ItemTypeEnum.Section; //TODO: Check this, probably intended as sub-section
                case 17: //QUESTIONFILLIN
                    return ItemTypeEnum.QuestionFill;
                case 20: //ANSWERFILLIN
                    return ItemTypeEnum.ListItemFillin;
                case 23: //QUESTIONMULTIPLE
                    return ItemTypeEnum.QuestionMultiple;
                case 24: //HEADER
                    return ItemTypeEnum.Section;  // Main eCC Section "header"
                case 25: //Top Level Header (Body)
                    return ItemTypeEnum.Body; //TODO: Check this
                case 26: //FIXEDLISTNOTE (combo box note)
                    return ItemTypeEnum.ListNote;
                default:
                    return ItemTypeEnum.None;
            }

        }

        public override QuestionEnum GetEccQuestionTypeEnum(string strRowType)
        {
            switch (strRowType.ToUpper())
            {
                case "QUESTIONSINGLE":
                    return QuestionEnum.QuestionSingle;
                case "QUESTIONMULTIPLE":
                    return QuestionEnum.QuestionMultiple;
                case "QUESTIONFILLIN":
                    return QuestionEnum.QuestionFill;
                case "QUESTION_LOOKUP":
                    return QuestionEnum.QuestionLookup;
                default:
                    return new QuestionEnum();
            }

        }

        public override ItemTypeEnum GetEccRowTypeEnum(string strRowType)
        //TODO: Delete this method in abstract class ???
        {
            switch (strRowType.ToUpper())
            {
                case "CHECKLIST":
                    return ItemTypeEnum.FormDesign;
                case "ANSWER":
                    return ItemTypeEnum.ListItem;
                case "ANSWERFILLIN":
                    return ItemTypeEnum.ListItem;
                case "QUESTIONSINGLE":
                    return ItemTypeEnum.QuestionGroup;
                case "QUESTIONMULTIPLE":
                    return ItemTypeEnum.QuestionGroup;
                case "QUESTIONFILLIN":
                    return ItemTypeEnum.QuestionGroup;
                case "QUESTION_LOOKUP":
                    return ItemTypeEnum.QuestionGroup;
                case "HEADER":
                    return ItemTypeEnum.Section;
                case "NOTE":
                    return ItemTypeEnum.DisplayedItem;
                case "FIXEDLISTNOTE":
                    return ItemTypeEnum.DisplayedItem;
                default:
                    return ItemTypeEnum.DisplayedItem;
            }

        }

        internal override SectionEnum GetSectionType(string strSectionType)
        {
            var se = new SectionEnum();

            if (strSectionType != string.Empty)
            {

                char[] delims = { ' ' };
                var splits = strSectionType.ToLower().Split(delims);

                foreach (string item in splits)
                { //first matching string wins...
                    if (item.StartsWith("sdcbody")) { se = SectionEnum.Body; return se; }
                    if (item.StartsWith("sdcheader")) { se = SectionEnum.Header; return se; }
                    if (item.StartsWith("sdcfooter")) { se = SectionEnum.Footer; return se; }
                }
            }
            return se;
        }

        //protected internal override SectionItemType AddHeader(SectionItemType header)
        //{   //Uses CTV data
        //    throw new NotImplementedException();
        //}

        //protected internal override SectionItemType AddFooter(SectionItemType footer)
        //{   //Uses CTV data
        //    throw new NotImplementedException();
        //}

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
            if (formDesignID.Trim() != string.Empty && Decimal.TryParse(formDesignID, out CTV_Ckey))
                if (CTV_Ckey > 0)
                {
                    DataRow row = dtHeaderDesign.Rows[0];

                    //Decide if we should use the database supplied filename, or preferably, a user-supplied filename
                    if (USERfilename != string.Empty)
                        row["CurrentFileName"] = USERfilename.Trim();
                    else if ((String)row["CurrentFileName"] == String.Empty)
                        USERfilename = "_file_" + DateTime.Now.Ticks.ToString().Trim();
                    BESTfilename = (String)row["CurrentFileName"];

                    this.CreateFormDesignTree();  //Call virtual functions?

                    return SerializeFormDesignTree();
                }
            BESTfilename = "error";
            return String.Empty;
        }

        internal override void InitRow(out ItemTypeEnum rowType, out string parentID, out string type)
        {
            //questionType = GetQuestionType(drFormDesign["ItemTypeKey"].ToString());
            rowType = GetEccRowTypeEnum((int)drFormDesign["ItemTypeKey"]);

            if (rowType == ItemTypeEnum.DisplayedItem && (drFormDesign["HasChildren"].ToString().Equals("true")))
                rowType = ItemTypeEnum.Section;  //DisplayedType Items (Notes) cannot have child nodes.  If we find one, convert to a section
            parentID = drFormDesign["ParentItemCkey"].ToString();
            type = drFormDesign["type"].ToString();
        }

        #endregion

    }
}



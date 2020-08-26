using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace SDC.Schema2
{
    class SDCTreeBuilder : ITreeBuilder
    {

        #region  Local Fields
        protected internal decimal Order { get; set; }

        //!raise event when form is created, so it can be serialized and/or transformed
        //public decimal FormDesignID { get; set; } //CTV_Ckey
        public string XsltFileName { get; set; }
        public ITopNode TopNode { get; set; }
        //public FormDesignType FormDesign { get; set; }
        //public DemogFormDesignType DemogFormDesign { get; set; }
        //public PackageItemType Package { get; set; }
        //public PackageListType PackageList { get; set; }
        //public DataElementType DataElement { get; set; }

        #endregion

        #region ..ctor
        public SDCTreeBuilder()
        {
            Order = 0;
        }
        #endregion
        #region ITreeBuilder

        
        PredGuardType ITreeBuilder.AddActivateIf(DisplayedType dt)
        {
            throw new NotImplementedException();
        }

        BlobType ITreeBuilder.AddBlob(DisplayedType dtParent, int insertPosition)
        {
            var blob = new BlobType(dtParent);
            if (dtParent.BlobContent == null) dtParent.BlobContent = new List<BlobType>();
            var count = dtParent.BlobContent.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            dtParent.BlobContent.Insert(insertPosition, blob);
            return blob;
        }

        SectionItemType ITreeBuilder.AddBody(FormDesignType fd)
        {
            if (fd.Body == null)
            {
                fd.Body = new SectionItemType(fd, fd.ID + "_Body");  //Set a default ID, in case the database template does not have a body
                fd.Body.name = "Body";
            }
            return fd.Body;
        }

        ButtonItemType ITreeBuilder.AddButtonAction<T>(T T_Parent, string id, int insertPosition)
        {
            //AddChildItem<SectionItemType, SectionItemType>(T_Parent as SectionItemType, id, insertPosition);
            var childItems = AddChildItemsNode(T_Parent);
            var btnNew = new ButtonItemType(childItems, id);
            var count = childItems.ChildItemsList.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            childItems.ChildItemsList.Insert(insertPosition, btnNew);

            // TODO: Add TreeBuilder.AddButtonActionTypeItems(btnNew);
            return btnNew;
        }


        //C AddChildItem<P, C>(P parent, string childID, int insertPosition) where P : IdentifiedExtensionType, IParent where C : IdentifiedExtensionType, new()
        //{
        //    var childNew = new C();  //would need public parameterless consructors for this to work
        //    parent.TopNode.ParentNodes.Add(childNew.ObjectID, parent);
        //    childNew.ID = childID;
        //    var childItems = AddChildItemsNode(parent);
        //    var count = childItems.ChildItemsList.Count;
        //    if (insertPosition < 0 || insertPosition > count) insertPosition = count;
        //    childItems.ChildItemsList.Insert(insertPosition, childNew);

        //    // TODO: Add TreeBuilder.AddButtonActionTypeItems(btnNew);
        //    return childNew;

        //}

        CodingType ITreeBuilder.AddCodedValue(DisplayedType dt, int insertPosition)
        {
            throw new NotImplementedException();
        }

        CodingType ITreeBuilder.AddCodedValue(LookupEndPointType lep, int insertPosition)
        {
            throw new NotImplementedException();
        }
        #region Comment
            CommentType ITreeBuilder.AddComment(ExtensionBaseType ebtParent, int insertPosition)
            {
                if (ebtParent.Comment == null) ebtParent.Comment = new List<CommentType>();
                CommentType ct = null;
                var count = ebtParent.Comment.Count;
                if (insertPosition < 0 || insertPosition > count) insertPosition = count;
                ebtParent.Comment.Insert(insertPosition, ct );  //return new empty Comment object for caller to fill
                return ct;
            }
            bool ITreeBuilder.RemoveComment(CommentType comment)
            {
                var ebt = (ExtensionBaseType)(comment.ParentNode);
                return ebt.Comment.Remove(comment);
            }
            bool ITreeBuilder.MoveComment(CommentType comment, ExtensionBaseType ebtTarget, int newListIndex)
            {
                if (comment == null) return false;

                var ebt = (ExtensionBaseType)(comment.ParentNode);  //get the list that comment is attached to          
                if (ebtTarget == null) ebtTarget = ebt;  //attach to the original parent
                bool b = ebt.Comment.Remove(comment);
                var count = ebt.Comment.Count;
                if (newListIndex < 0 || newListIndex > count) newListIndex = count;
                if (b) ebtTarget.Comment.Insert(newListIndex, comment);
                if (ebtTarget.Comment[newListIndex] == comment) return true; //success
                return false;
            }

        #endregion

        #region Contact
        ContactType ITreeBuilder.AddContact(DisplayedType dtParent, int insertPosition)
            {
                if (dtParent.Contact == null) dtParent.Contact = new List<ContactType>();
                var ct = new ContactType(dtParent);
                var count = dtParent.Contact.Count;
                if (insertPosition < 0 || insertPosition > count) insertPosition = count;
                dtParent.Contact.Insert(insertPosition, ct);
                return ct;
            }

            ContactType ITreeBuilder.AddContact(FileType ftParent, int insertPosition)
            {
                ContactsType c;
                if (ftParent.Contacts == null)
                    c = AddContactsListToFileType(ftParent);
                else
                    c = ftParent.Contacts;
                var ct = new ContactType(c);
                var count = c.Contact.Count;
                if (insertPosition < 0 || insertPosition > count) insertPosition = count;
                c.Contact.Insert(insertPosition, ct);
                //TODO: Need to be able to add multiple people/orgs by reading the data source or ORM
                var p = ((ITreeBuilder)this).AddPerson(ct);
                var org = ((ITreeBuilder)this).AddOrganization(ct);

                return ct;
            }
            private ContactsType AddContactsListToFileType(FileType ftParent)
            {
                if (ftParent.Contacts == null)
                    ftParent.Contacts = new ContactsType(ftParent);

                return ftParent.Contacts; //returns a .NET List<ContactType>

            }
        #endregion



        PredGuardType ITreeBuilder.AddDeActivateIf(DisplayedType dt)
        {
            throw new NotImplementedException();
        }

        DisplayedType ITreeBuilder.AddDisplayedItem<T>(T T_Parent, string id, int insertPosition)
        {
            var childItemsList = AddChildItemsNode(T_Parent);
            var dNew = new DisplayedType(childItemsList, id);  //!+Test this
            var count = childItemsList.ChildItemsList.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            childItemsList.ChildItemsList.Insert(insertPosition, dNew);
            return dNew;
        }

        #region Extension
        ExtensionType ITreeBuilder.AddExtension(ExtensionBaseType ebt, int insertPosition)
        {
            var e = new ExtensionType(ebt);
            if (ebt.Extension == null) ebt.Extension = new List<ExtensionType>();
            var count = ebt.Extension.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            ebt.Extension.Insert(insertPosition, e);
            return e;
        }

        bool ITreeBuilder.RemoveExtension(ExtensionType extension)
        {
            var ebt = (ExtensionBaseType)(extension.ParentNode);
            return ebt.Extension.Remove(extension);
        }
        bool ITreeBuilder.MoveExtension(ExtensionType extension, ExtensionBaseType ebtTarget, int newListIndex)
        {
            if (extension == null) return false;

            var ebt = (ExtensionBaseType)(extension.ParentNode);  //get the list that comment is attached to          
            if (ebtTarget == null) ebtTarget = ebt;  //attach to the original parent
            bool b = ebt.Extension.Remove(extension);
            if (b) ebtTarget.Extension.Insert(newListIndex, extension);
            var count = ebtTarget.Extension.Count;
            if (newListIndex < 0 || newListIndex > count) newListIndex = count;
            if (ebtTarget.Extension[newListIndex] == extension) return true; //success
            return false;
        }
        #endregion

        HTML_Stype ITreeBuilder.AddHTML(RichTextType rt)
        {
            HTML_Stype html = null;

            html = new HTML_Stype(rt);
            rt.RichText = html;
            html.Any = new List<XmlElement>();

            return html;

            //TODO: Check XHTML builder here:
            //https://gist.github.com/rarous/3150395,
            //http://www.authorcode.com/code-snippet-converting-xmlelement-to-xelement-and-xelement-to-xmlelement-in-vb-net/
            //https://msdn.microsoft.com/en-us/library/system.xml.linq.loadoptions%28v=vs.110%29.aspx

        }

        OrganizationType ITreeBuilder.AddOrganization(ContactType contactParent)
        {
            var ot = new OrganizationType(contactParent);
            contactParent.Organization = ot;

            return ot;
        }

        OrganizationType ITreeBuilder.AddOrganization(JobType jobParent)
        {
            var ot = new OrganizationType(jobParent);
            jobParent.Organization = ot;

            return ot;
        }

        OrganizationType ITreeBuilder.AddOrganizationItems(OrganizationType ot)
        {
            throw new NotImplementedException();
        }

        SectionItemType ITreeBuilder.AddFooter(FormDesignType fd)
        {
            if (fd.Footer == null)
            {
                fd.Footer = new SectionItemType(fd, fd.ID + "_Footer");  //Set a default ID, in case the database template does not have a body
                fd.Footer.name = "Footer";
            }
            return fd.Footer;
        }

        SectionItemType ITreeBuilder.AddHeader(FormDesignType fd)
        {
            if (fd.Header == null)
            {
                fd.Header = new SectionItemType(fd, fd.ID + "_Header");  //Set a default ID, in case the database template does not have a body
                fd.Header.name = "Header";
            }
            return fd.Header;
        }

        InjectFormType ITreeBuilder.AddInjectedForm<T>(T T_Parent, string id, int insertPosition)
        {
            var childItems = AddChildItemsNode(T_Parent);
            var injForm = new InjectFormType(childItems, id);
            var count = childItems.ChildItemsList.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            childItems.ChildItemsList.Insert(insertPosition, injForm);

            return injForm;
        }

        LinkType ITreeBuilder.AddLink(DisplayedType dtParent, int insertPosition)
        {
            var link = new LinkType(dtParent);

            if (dtParent.Link == null) dtParent.Link = new List<LinkType>();
            var count = dtParent.Link.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            dtParent.Link.Insert(insertPosition, link);
            link.order = link.ObjectID;

            var rtf = new RichTextType(link);
            link.LinkText = rtf;

            return link;
        }

        ListItemResponseFieldType ITreeBuilder.AddListItemResponseField(ListItemBaseType liParent)
        {
            var liRF = new ListItemResponseFieldType(liParent);
            liParent.ListItemResponseField = liRF;

            return liRF;
        }

        EventType ITreeBuilder.AddOnEnterEvent(DisplayedType dt)
        {
            throw new NotImplementedException();
        }

        OnEventType ITreeBuilder.AddOnEventEvent(DisplayedType dt)
        {
            throw new NotImplementedException();
        }

        EventType ITreeBuilder.AddOnExitEvent(DisplayedType dt)
        {
            throw new NotImplementedException();
        }
        #region Property
        PropertyType ITreeBuilder.AddProperty(ExtensionBaseType dtParent, int insertPosition)
        {
            var prop = new PropertyType(dtParent);
            if (dtParent.Property == null) dtParent.Property = new List<PropertyType>();
            var count = dtParent.Property.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            dtParent.Property.Insert(insertPosition, prop);

            return prop;
        }
        bool ITreeBuilder.RemoveProperty(PropertyType property)
        {
            var ebt = (ExtensionBaseType)(property.ParentNode);
            return ebt.Property.Remove(property);
        }
        bool ITreeBuilder.MoveProperty(PropertyType property, ExtensionBaseType ebtTarget, int newListIndex)
        {
            if (property == null) return false;

            var ebt = (ExtensionBaseType)(property.ParentNode);  //get the list that comment is attached to          
            if (ebtTarget == null) ebtTarget = ebt;  //attach to the original parent
            bool b = ebt.Property.Remove(property);
            var count = ebt.Property.Count;
            if (newListIndex < 0 || newListIndex > count) newListIndex = count;
            if (b) ebtTarget.Property.Insert(newListIndex, property);
            if (ebtTarget.Property[newListIndex] == property) return true; //success
            return false;
        }


        #endregion


        QuestionItemType ITreeBuilder.AddQuestion<T>(T T_Parent, QuestionEnum qType, string id, int insertPosition)
        {
            var childItemsList = AddChildItemsNode(T_Parent);
            var qNew = new QuestionItemType(childItemsList, id);
            ListFieldType lf;
            var count = childItemsList.ChildItemsList.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            childItemsList.ChildItemsList.Insert(insertPosition, qNew);

            switch (qType)
            {
                case QuestionEnum.QuestionSingle:
                    AddListToListField(AddListFieldToQuestion(qNew));
                    break;
                case QuestionEnum.QuestionMultiple:
                    AddListToListField(AddListFieldToQuestion(qNew));
                    qNew.ListField_Item.maxSelections = 0;
                    break;
                case QuestionEnum.QuestionFill:
                    AddQuestionResponseField(qNew);
                    break;
                case QuestionEnum.QuestionLookupSingle:
                    lf = AddListFieldToQuestion(qNew);
                    AddEndpointToListField(lf);
                    break;
                case QuestionEnum.QuestionLookupMultiple:
                    lf = AddListFieldToQuestion(qNew);
                    AddEndpointToListField(lf);
                    break;
                default:
                    throw new NotSupportedException($"{qType} is not supported");                    
            }

            return qNew;
        }


        SectionItemType ITreeBuilder.AddSection<T>(T T_Parent, string id, int insertPosition)
        {
            var childItemsList = AddChildItemsNode(T_Parent);
            var sNew = new SectionItemType(childItemsList, id);
            var count = childItemsList.ChildItemsList.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            childItemsList.ChildItemsList.Insert(insertPosition, sNew);

            return sNew;
        }

        UnitsType ITreeBuilder.AddUnits(ResponseFieldType rfParent)
        {
            UnitsType u = new UnitsType(rfParent);
            rfParent.ResponseUnits = u;
            return u;
        }
        UnitsType ITreeBuilder.AddUnits(CodingType ctParent)
        {
            UnitsType u = new UnitsType(ctParent);
            ctParent.Units = u;
            return u;
        }
        string ITreeBuilder.CreateName(BaseType bt)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Person
        PersonType ITreeBuilder.AddPerson(ContactType contactParent)
        {

            var newPerson = new PersonType(contactParent);
            contactParent.Person = newPerson;

            AddPersonItems(newPerson);  //AddFillPersonItems?

            return newPerson;
        }

        PersonType ITreeBuilder.AddPerson(DisplayedType dtParent, int insertPosition)
        {
            List<ContactType> contactList;
            if (dtParent.Contact == null)
            {
                contactList = new List<ContactType>();
                dtParent.Contact = contactList;
            }
            else
                contactList = dtParent.Contact;
            var newContact = new ContactType(dtParent); //newContact will contain a person child
            var count = contactList.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            contactList.Insert(insertPosition, newContact);

            var newPerson = ((ITreeBuilder)this).AddPerson(newContact);

            return newPerson;
        }

        PersonType ITreeBuilder.AddContactPerson(OrganizationType otParent, int insertPosition)
        {
            List<PersonType> contactPersonList;
            if (otParent.ContactPerson == null)
            {
                contactPersonList = new List<PersonType>();
                otParent.ContactPerson = contactPersonList;
            }
            else
                contactPersonList = otParent.ContactPerson;

            var newPerson = new PersonType(otParent);
            AddPersonItems(newPerson);

            var count = contactPersonList.Count;
            if (insertPosition < 0 || insertPosition > count) insertPosition = count;
            contactPersonList.Insert(insertPosition, newPerson);

            return newPerson;
        }


        protected virtual PersonType AddPersonItems(PersonType pt)  //AddFillPersonItems, make this abstract and move to subclass?
        {
            pt.PersonName = new NameType(pt);//TODO: Need separate method(s) for this
            //pt.Alias = new NameType();
            //pt.PersonName.FirstName.val = (string)drFormDesign["FirstName"];  //TODO: replace with real data
            //pt.PersonName.LastName.val = (string)drFormDesign["LastName"];  //TODO: replace with real data

            pt.Email = new List<EmailType>();//TODO: Need separate method(s) for this
            var email = new EmailType(pt);//TODO: Need separate method(s) for this
            pt.Email.Add(email);

            pt.Phone = new List<PhoneType>();//TODO: Need separate method(s) for this
            pt.Job = new List<JobType>();//TODO: Need separate method(s) for this

            pt.Role = new string_Stype(pt, "Role");

            pt.StreetAddress = new List<AddressType>();//TODO: Need separate method(s) for this
            pt.Identifier = new List<IdentifierType>();

            pt.Usage = new string_Stype(pt, "Usage");

            pt.WebURL = new List<anyURI_Stype>();//TODO: Need separate method(s) for this

            return pt;
        }

        #endregion


        #region  Actions

           ActActionType ITreeBuilder.AddActAction(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActActionType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        RuleSelectMatchingListItemsType ITreeBuilder.AddActSelectMatchingListItems(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new RuleSelectMatchingListItemsType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActAddCodeType ITreeBuilder.AddActAddCode(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActAddCodeType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActInjectType ITreeBuilder.AddActInject(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActInjectType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        CallFuncActionType ITreeBuilder.AddActShowURL(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new CallFuncActionType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActSaveResponsesType ITreeBuilder.AddActSaveResponses(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActSaveResponsesType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActSendReportType ITreeBuilder.AddActSendReport(ActionsType at, int insertPosition )
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActSendReportType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActSendMessageType ITreeBuilder.AddActSendMessage(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActSendMessageType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActSetAttributeType ITreeBuilder.AddActSetAttributeValue(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActSetAttributeType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActSetAttrValueScriptType ITreeBuilder.AddActSetAttributeValueScript(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActSetAttrValueScriptType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActSetBoolAttributeValueCodeType ITreeBuilder.AddActSetBoolAttributeValueCode(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActSetBoolAttributeValueCodeType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActShowFormType ITreeBuilder.AddActShowForm(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActShowFormType(at);
            at.Items[insertPosition] = act;
            return act;
        }
         ActShowMessageType ITreeBuilder.AddActShowMessage(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActShowMessageType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActShowReportType ITreeBuilder.AddActShowReport(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActShowReportType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActPreviewReportType ITreeBuilder.AddActPreviewReport(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActPreviewReportType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ActValidateFormType ITreeBuilder.AddActValidateForm(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ActValidateFormType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        ScriptCodeAnyType ITreeBuilder.AddActRunCode(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new ScriptCodeAnyType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        CallFuncActionType ITreeBuilder.AddActCallFunctionction(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new CallFuncActionType(at);
            at.Items[insertPosition] = act;
            return act;
        }
        PredActionType ITreeBuilder.AddActConditionalGroupAction(ActionsType at, int insertPosition)
        {
            var a = at.Items;
            if (a == null) a = new ExtensionBaseType[10];
            if (insertPosition > a.Length - 1) Array.Resize(ref a, a.Length + 10);
            var act = new PredActionType(at);
            at.Items[insertPosition] = act;
            return act;
        }



        #endregion

        #region Rules
        protected virtual RulesType AddRulesTypeToDisplayedType(DisplayedType parent)
        {
            //FormAction
            //PropertyAction,
            //If ListItemStatus,
            //If Predicate,
            //IfGroup
            return new RulesType(parent);
        }
        //RuleAutoSelectType
        //RuleSelectionTestType
        //RuleIllegalSelectionSetsType
        //SelectionSetBoolType
        //RulesCollectionType
        //RuleReferenceType
        #endregion

        #region Data Helpers
        protected virtual DataTypes_DEType AddDataTypesDE(
          ResponseFieldType rfParent,          
          ItemChoiceType dataTypeEnum = ItemChoiceType.@string,
          dtQuantEnum quantifierEnum = dtQuantEnum.EQ,
          object value=null)
        {
            rfParent.Response = new DataTypes_DEType(rfParent);

            switch (dataTypeEnum)
            {
                case ItemChoiceType.HTML:
                    {
                        var dt = new HTML_DEtype(rfParent.Response);
                        dt.Any = new List<XmlElement>();
                        dt.AnyAttr = new List<XmlAttribute>();
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.XML: //TODO: Need to be able to add custom attributes to first wrapper element - see anyType; in fact, do we even need XML as a separate type?
                    {
                        var dt = new XML_DEtype(rfParent.Response);
                        dt.Any = new List<XmlElement>();
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.anyType:
                    {
                        var dt = new anyType_DEtype(rfParent.Response);
                        dt.Any = new List<XmlElement>();
                        dt.AnyAttr = new List<XmlAttribute>();
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.anyURI:
                    {
                        var dt = new anyURI_DEtype(rfParent.Response);
                        dt.val = (string)value;
                    }
                    break;
                case ItemChoiceType.base64Binary:
                    {
                        var dt = new base64Binary_DEtype(rfParent.Response);
                        dt.val = (byte[])value;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.boolean:
                    {
                        var dt = new boolean_DEtype(rfParent.Response);
                        dt.val = (bool)value;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.@byte:
                    {
                        var dt = new byte_DEtype(rfParent.Response);
                        dt.val = (sbyte)value;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.date:
                    {
                        var dt = new date_DEtype(rfParent.Response);
                        dt.val = (DateTime)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.@dateTime: //TODO: added the "@" symbol to dateTime and dateTimeStamp here, in AddFillDataTypesDE, and in the 2 ItemChoiceType files.  Also fixed bug in the DateTypes_DEtype with the wrong ItemTypeNames from xsd2code - on dateTime and dateTimeStamp.
                    {
                        var dt = new dateTime_DEtype(rfParent.Response);
                        if (value != null)  //TODO: value testing may be needed for the other dateTime and duration types in this method, and also in AddFillDataTypesDE
                        {
                            var test = value as DateTime?;
                            if (test != null)
                                dt.val = (DateTime)test;
                            else
                                try
                                {
                                    var sTest = DateTime.Parse(value.ToString());
                                    dt.val = sTest;
                                }
                                catch (Exception ex)
                                { }
                        }
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.@dateTimeStamp:
                    {
                        var dt = new dateTimeStamp_DEtype(rfParent.Response);
                        dt.val = (DateTime)value;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.@decimal:
                    {
                        var dt = new decimal_DEtype(rfParent.Response);
                        dt.val = (decimal)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.@double:
                    {
                        var dt = new double_DEtype(rfParent.Response);
                        dt.val = (double)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.duration:
                    {
                        var dt = new duration_DEtype(rfParent.Response);
                        dt.val = (string)value;   //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                    }
                    break;
                case ItemChoiceType.@float:
                    {
                        var dt = new float_DEtype(rfParent.Response);
                        dt.val = (float)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.gDay:
                    {
                        var dt = new gDay_DEtype(rfParent.Response);
                        dt.val = (string)value; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.gMonth:
                    {
                        var dt = new gMonth_DEtype(rfParent.Response);
                        dt.val = (string)value; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.gMonthDay:
                    {
                        var dt = new gMonthDay_DEtype(rfParent.Response);
                        dt.val = (string)value; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.gYear:
                    {
                        var dt = new gYear_DEtype(rfParent.Response);
                        dt.val = (string)value; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.gYearMonth:
                    {
                        var dt = new gYearMonth_DEtype(rfParent.Response);
                        dt.val = (string)value; //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.hexBinary:
                    {
                        var dt = new hexBinary_DEtype(rfParent.Response);
                        dt.val = (byte[])value;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.@int:
                    {
                        var dt = new int_DEtype(rfParent.Response);
                        dt.val = (int)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.integer:
                    {
                        var dt = new integer_DEtype(rfParent.Response);
                        dt.val = (string)value; //(string)value; ;  //TODO:  bug in xsdCode++ - wrong data type - uses string because there is no integer (truncated decimal) format in .NET
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.@long:
                    {
                        var dt = new long_DEtype(rfParent.Response);
                        dt.val = (long)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.negativeInteger:
                    {
                        var dt = new negativeInteger_DEtype(rfParent.Response);
                        dt.val = (string)value;  // drFormDesign["DefaultValue"].ToString(); ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.nonNegativeInteger:
                    {
                        var dt = new nonNegativeInteger_DEtype(rfParent.Response);
                        dt.val = (string)value; //dt.val = drFormDesign["DefaultValue"].ToString(); ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.nonPositiveInteger:
                    {
                        var dt = new nonPositiveInteger_DEtype(rfParent.Response);
                        dt.val = (string)value; //dt.val = drFormDesign["DefaultValue"].ToString(); ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.positiveInteger:
                    {
                        var dt = new positiveInteger_DEtype(rfParent.Response);
                        dt.val = (string)value; //dt.val = drFormDesign["DefaultValue"].ToString(); ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.@short:
                    {
                        var dt = new short_DEtype(rfParent.Response);
                        dt.val = (short)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.@string:
                    {
                        var dt = new @string_DEtype(rfParent.Response);
                        dt.val = (string)value;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.time:
                    {
                        var dt = new time_DEtype(rfParent.Response);
                        dt.val = (DateTime)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.unsignedByte:
                    {
                        var dt = new unsignedByte_DEtype(rfParent.Response);
                        dt.val = (byte)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.unsignedInt:
                    {
                        var dt = new unsignedInt_DEtype(rfParent.Response);
                        dt.val = (uint)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.unsignedLong:
                    {
                        var dt = new unsignedLong_DEtype(rfParent.Response);
                        dt.val = (ulong)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.unsignedShort:
                    {
                        var dt = new unsignedShort_DEtype(rfParent.Response);
                        dt.val = (ushort)value;
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                case ItemChoiceType.yearMonthDuration:
                    {
                        var dt = new yearMonthDuration_DEtype(rfParent.Response);
                        dt.val = (string)value; ;  //TODO:  bug in xsdCode++ - wrong data type
                        dt.quantEnum = quantifierEnum;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
                default:
                    {
                        var dt = new @string_DEtype(rfParent.Response);
                        dt.val = (string)value;
                        rfParent.Response.DataTypeDE_Item = dt;
                    }
                    break;
            }

            rfParent.Response.ItemElementName = (ItemChoiceType2)dataTypeEnum;
            return rfParent.Response;

        }
        #endregion
        #region Helpers


        protected virtual dtQuantEnum AssignQuantifier(string quantifier)
    {
        var dtQE = new dtQuantEnum();

        switch (quantifier)
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
        protected virtual XmlElement StringToXMLElement(string rawXML)
        {
            var xe = XElement.Parse(rawXML, LoadOptions.PreserveWhitespace);
            var doc = new XmlDocument();

            var xmlReader = xe.CreateReader();
            doc.Load(xmlReader);
            xmlReader.Dispose();

            return doc.DocumentElement;
        }
        protected virtual ChildItemsType AddChildItemsNode<T>(T T_Parent) where T :BaseType, IParent //, new()
        {
            ChildItemsType childItems = null;  //this class contains an "Items" list
            if (T_Parent == null)
                throw new ArgumentNullException("The T_Parent object was null");
                //return childItems; 
            else if (T_Parent.ChildItemsNode == null)
            {
                childItems = new ChildItemsType(T_Parent);
                T_Parent.ChildItemsNode = childItems;  //This may be null for the Header, Body and Footer  - need to check this
            }
            else //(T_Parent.ChildItemsNode != null)
                childItems = T_Parent.ChildItemsNode;

            if (childItems.ChildItemsList == null)
                childItems.ChildItemsList = new List<IdentifiedExtensionType>();

            return childItems;
        }
        public int GetListIndex<T>(List<T> list, T node) where T:notnull, BaseType  //TODO: could make this an interface feature of all list children
        {
            int i = 0;
            foreach (T n in list)
            {
                if (n == node) return i;
                i++;
            }
            return -1;
        }
        public bool IsItemChangeAllowed<S, T>(S source, T target)
            where S : notnull, IdentifiedExtensionType
            where T : IdentifiedExtensionType
        {
            ChildItemsType ci;
            switch (source)
            {
                case SectionItemType _:
                case QuestionItemType _:
                case ListItemType _:
                    ci = (source as ChildItemsType);
                    switch (target)
                    {
                        case SectionItemType _:
                        case QuestionItemType _:
                        case ListItemType _:
                            return true;
                        case ButtonItemType _:
                        case DisplayedType _:                        
                            if (ci is null) return true;
                            if (ci.ChildItemsList is null) return true;
                            if (ci.ChildItemsList.Count == 0) return true;
                            return false;
                        case InjectFormType j:
                            return false;
                        default: return false;
                    }
                case ButtonItemType b:
                    break;
                case DisplayedType d:
                    break;
                case InjectFormType j:
                    return false;

                    break;
                default:
                    break;
            }


        return false;
        }


        #endregion
        #region IChildItemMember
        bool ITreeBuilder.Remove<T>(T source)
        {
            var ci = ((ChildItemsType)source.ParentNode).Items;
                return ci.Remove(source);
        }
        bool ITreeBuilder.IsMoveAllowedToChild<S, T>(S source, T target, out string error) 

        {
            var errorSource = "";
            var errorTarget = "";
            error = "";
            bool sourceOK = false;
            bool targetOK = false;

            if (source is null) {error = "source is null"; return false; }
            if (target is null) {error = "target is null"; return false; }
            if (target is ButtonItemType) { error = "ButtonItemType is not allowed as a target"; return false; }
            if (target is InjectFormType) { error = "InjectFormType is not allowed as a target"; return false; }
            if (((ITreeBuilder)this).IsDisplayedItem(target))  { error = "DisplayedItem is not allowed as a target"; return false; }

            if (source is ListItemType && !(target is QuestionItemType) && !(target is ListItemType)) { error = "A ListItem can only be moved into a Question List"; return false; };

            //special case to allow LI to drop on a Q and be added to the Q's List, rather than under ChildItem (which would be illegal)
            if (source is ListItemType && 
                target is QuestionItemType &&
                !((target as QuestionItemType).GetQuestionSubtype == QuestionEnum.QuestionSingle) &&
                !((target as QuestionItemType).GetQuestionSubtype == QuestionEnum.QuestionMultiple))
            { error = "A Question target must be a QuestionSingle or QuestionMultiple"; return false; }


            if (source is DisplayedType || source is InjectFormType) sourceOK = true;
            if (target is QuestionItemType || target is SectionItemType || target is ListItemType) targetOK = true;

            if(!sourceOK ||!targetOK)
            { if (!sourceOK) errorSource = "Illegal source object";
              if (!targetOK) errorTarget = "Illegal target object";
                if (errorTarget.Length > 0) errorTarget += " and ";
                error = errorSource + errorTarget;
            }


            return sourceOK & targetOK;
        }
        bool ITreeBuilder.IsDisplayedItem(BaseType target)
        {
            if (target is DisplayedType && !(target is QuestionItemType || target is ListItemType || target is SectionItemType || target is ButtonItemType))
                return true;
            return false;
        }        
        bool ITreeBuilder.MoveAfterSib<S, T>(S source, T target, int newListIndex, bool moveAbove)
        {
            throw new Exception(String.Format("Not Implemented" ));
        }
        bool ITreeBuilder.MoveAsChild<S, T>(S source, T target, int newListIndex)
        {
            if (source is null) return false;
            if (source.ParentNode is null) return false;
            if (source is ListItemType && !(target is QuestionItemType)) return false;  //ListItem can only be moved to a question.

            List<BaseType> sourceList;

            switch (source)  //get the sourceList from the parent node
            {
                case QuestionItemType _:
                case SectionItemType _:
                case InjectFormType _:
                case ButtonItemType _:
                    sourceList = (source.ParentNode as ChildItemsType)?.Items.ToList<BaseType>();
                    //sourceList = (source.ParentNode as ChildItemsType).Items.Cast<BaseType>().ToList(); //alternate method
                    break;
                case ListItemType _:
                    sourceList = (source.ParentNode as ListType)?.Items.ToList<BaseType>();
                    break;
                case DisplayedType _:
                    sourceList = (source.ParentNode as ChildItemsType)?.Items.ToList<BaseType>();
                    if (sourceList is null)
                        sourceList = (source.ParentNode as ListType)?.Items.ToList<BaseType>();
                    else return false;
                    break;
                default:
                    return false; //error in source type
            }

            if (sourceList is null) return false;

            List<BaseType> targetList = null;

            if (target != null)
            {
                switch (target)  //get the targetList from the child node
                {
                    case QuestionItemType q:
                        //This is an exception - if we drop a source LI on a QS/QM, we will want to add it ant the end of the Q's List object
                        if (source is ListItemType)
                        {
                            if (q.GetQuestionSubtype != QuestionEnum.QuestionSingle &&
                                q.GetQuestionSubtype != QuestionEnum.QuestionMultiple &&
                                q.GetQuestionSubtype != QuestionEnum.QuestionRaw) return false;  //QR, and QL cannot have child LI nodes
                            if (q.ListField_Item is null)  //create new targetList
                            {
                                targetList = AddListToListField(AddListFieldToQuestion(q)).Items.ToList<BaseType>();
                                if (targetList is null) return false;
                                break;
                            }
                            targetList = q.ListField_Item.List.Items.ToList<BaseType>();
                        }
                        else //use the ChildItems node instead as the targetList
                        {
                            AddChildItemsNode(q);
                            targetList = q.ChildItemsNode.Items.ToList<BaseType>();
                        }
                        break;
                    case SectionItemType s:
                        AddChildItemsNode(s);
                        targetList = s.ChildItemsNode.Items.ToList<BaseType>();
                        break;
                    case ListItemType l:
                        AddChildItemsNode(l);
                        targetList = l.ChildItemsNode.Items.ToList<BaseType>();
                        break;
                    default:
                        return false; //error in source type
                }
            }
            else targetList = sourceList;
            if (targetList is null) return false;


            var count = targetList.Count;
            if (newListIndex < 0 || newListIndex > count) newListIndex = count; //add to end  of list

            var indexSource = GetListIndex(sourceList, source);  //save the original source index in case we need to replace the source node back to it's origin
            bool b = sourceList.Remove(source); if (!b) return false;
            targetList.Insert(newListIndex, source);
            if (targetList[newListIndex] == source) return true; //check for success

            //Error - the source item is now disconnected from the list.  Lets add it back to the end of the list.
            sourceList.Insert(indexSource, source); //put source back where it came from; the move failed
            return false;
        }
        #endregion
        #region IQuestionListMember
        bool ITreeBuilder.IsMoveAllowedToList<S, T>(S source, T target, out string error)
        {
            error = "";

            if (source is null) { error = "source is null"; return false; }
            if (target is null) { error = "target is null"; return false; }
            if (source is SectionItemType) return false; //S is illegal in the list
            if (target is SectionItemType) return false; //S is illegal in the list
            if (source is ButtonItemType) return false; //B is illegal in the list
            if (target is ButtonItemType) return false; //B is illegal in the list

            if (source is QuestionItemType) return false; //Q is illegal in the list


            if (!(source is ListItemType) && !(source is DisplayedType)) { error = "The source must be a ListItem or DisplayedItem"; return false; };
            if (!(target is ListItemType) && !(target is QuestionItemType)) { error = "The target must be a ListItem or Question"; return false; };

            if (target is QuestionItemType &&
                !((target as QuestionItemType).GetQuestionSubtype == QuestionEnum.QuestionSingle) &&
                !((target as QuestionItemType).GetQuestionSubtype == QuestionEnum.QuestionMultiple))
            { error = "A Question target must be a QuestionSingle or QuestionMultiple"; return false; }

            return true;

        }
        bool ITreeBuilder.MoveInList(DisplayedType source, QuestionItemType target)
        {
            if (source is null) return false;
            if (source is RepeatingType) return false; //S, Q are illegal in the list
            if (source is ButtonItemType) return false; //B is illegal in the list

            if (target is null) target = source.ParentNode?.ParentNode?.ParentNode as QuestionItemType; //LI-->List-->ListField-->Q  - see if we can capture a Question from the source node
            if (target is null) return false;

            List<BaseType> sourceList = (source.ParentNode as ListType)?.Items?.ToList<BaseType>();//guess that the sourrce node is inside a List
            if (sourceList is null) sourceList = (source.ParentNode as ChildItemsType)?.ChildItemsList?.ToList<BaseType>();//try again - guess that source node is inside a ChildItems node
            if (sourceList is null) return false;//both guesses failed - this is probably a disconnected node, and we can't work with that.

            if (target.ListField_Item is null) AddListToListField(AddListFieldToQuestion(target));  //make sure there is a ist instantiated on this target Question; if not, then create it.          
            var targetList = target.ListField_Item.List.Items;
            if (targetList is null) return false;  //unkown problem getting Question-->ListField-->List

            var indexSource = GetListIndex(sourceList, source);
            int index = targetList.Count;
            sourceList.Remove(source);
            targetList.Insert(index, source);
            if (targetList[index] == source)
                return true;

            //Error - the source item is now disconnected from the list.  Lets add it back to the end of the list.
            sourceList.Insert(indexSource, source); //put source back where it came from; the move failed
            return false;

        }
        bool ITreeBuilder.MoveInList(DisplayedType source, DisplayedType target, bool moveAbove) //target must be a LI or DI (not a RepeatingType)
        {
            //this function allows dropping items inside a QS o QM list to rearrange the list
            //prevent illegal operations
            if (source is null) return false;
            if (source is RepeatingType) return false; //S, Q (RepeatingTypes) are illegal in the Q's list
            if (target is RepeatingType) return false; //S, Q (RepeatingTypes) are illegal in the Q's list
            if (source is ButtonItemType) return false; //B is illegal in the Q's list
            if (target is ButtonItemType) return false; //B is illegal in the Q's list

            List<BaseType> sourceList = (source.ParentNode?.ParentNode as ListType)?.Items?.ToList<BaseType>();
            if (sourceList is null) sourceList = (source.ParentNode as ChildItemsType)?.ChildItemsList?.ToList<BaseType>();
            if (sourceList is null) return false;

            var qTarget = target.ParentNode?.ParentNode?.ParentNode as QuestionItemType;
            if (qTarget is null) return false;  //we did not get a Q object, so we are not moving  a node into a Q List

            var targetList = qTarget.ListField_Item?.List?.Items;


            if (targetList is null) return false;

            var indexSource = GetListIndex(sourceList, source);
            sourceList.Remove(source);

            //Determine where to insert the node in the list, based on the location of the existing Lis
            var index = GetListIndex(targetList, target);
            if (index < 0) index = targetList.Count; //target node not found in list, so insert source at the end of the list; this should never execute
            if (moveAbove) index--;

            targetList.Insert(index, source);
            if (targetList[index] == source)
                return true;

            //Error - the source item is now disconnected from the list.  Lets add it back to the end of the list.
            sourceList.Insert(indexSource, source); //put source back where it came from; the move failed
            return false;
        }
        #endregion
        #region QAS
        protected virtual ListType AddListToListField(ListFieldType listFieldParent)
        {
            ListType list;  //this is not the .NET List class; It's an answer list
            if (listFieldParent.List == null)
            {
                list = new ListType(listFieldParent);
                listFieldParent.List = list;
            }
            else list = listFieldParent.List;

            //The "list" item contains a list<DisplayedType>, to which the ListItems and ListNotes (DisplayedItems) are added.
            if (list.QuestionListMembers == null)

                list.QuestionListMembers = new List<DisplayedType>();

            return list;
        }
        protected virtual ListFieldType AddListFieldToQuestion(QuestionItemType qParent)
        {
            if (qParent.ListField_Item == null)
            {
                var listField = new ListFieldType(qParent);
                qParent.ListField_Item = listField;
            }

            return qParent.ListField_Item;
        }
        ListItemType ITreeBuilder.AddListItemToQuestion(QuestionItemType qParent, string id, int insertPosition)
        {
            return ((ITreeBuilder)this).AddListItemToList(qParent, id, insertPosition);
        }
        ListItemType ITreeBuilder.AddListItemResponseToQuestion(QuestionItemType qParent, string id, int insertPosition)
        {
            var li = ((ITreeBuilder)this).AddListItemToQuestion(qParent, id, insertPosition);
            ((ITreeBuilder)this).AddListItemResponseField(li);
            return li;
        }
        ListItemType ITreeBuilder.AddListItemToList(QuestionItemType qParent, string id, int insertPosition)
        {   //Check for QS/QM first!
            if (qParent.GetQuestionSubtype == QuestionEnum.QuestionMultiple ||
                qParent.GetQuestionSubtype == QuestionEnum.QuestionSingle)
            {
                ListType list = qParent.ListField_Item.List;  
                ListItemType li = new ListItemType(list, id);
                var count = list.QuestionListMembers.Count;
                if (insertPosition < 0 || insertPosition > count) insertPosition = count;
                list.QuestionListMembers.Insert(insertPosition, li);
                return li;
            }
            else throw new InvalidOperationException("Can only add ListItem to QuestionSingle or QuestionMultiple");

        }
        public virtual ResponseFieldType AddQuestionResponseField(QuestionItemType qParent)
        {
            var rf = new ResponseFieldType(qParent);
            qParent.ResponseField_Item = rf;

            return rf;
        }
        public virtual LookupEndPointType AddEndpointToListField(ListFieldType listFieldParent)
        {
            if (listFieldParent.List == null)
            {
                var lep = new LookupEndPointType(listFieldParent);
                listFieldParent.LookupEndpoint = lep;
                return lep;
            }
            else throw new InvalidOperationException("Can only add LookupEndpoint to ListField if List object is not present");
        }
        #endregion


    }



}

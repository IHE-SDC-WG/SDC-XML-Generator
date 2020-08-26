using System;
using System.IO;

namespace SDC.Schema2
{
    /// <summary>
    /// Top-level public methods used to build SDC tree in SDC.Schema.PartialClasses
    /// </summary>
    public interface ITreeBuilder {

        #region Package

        #endregion

        #region DataElement

        #endregion

        #region Demog Form

        #endregion

        #region Map

        #endregion





        #region New Form
        //FormDesignType CreateForm(bool addHeader, bool addFooter, string formID, string lineage, string version, string fullURI);
        //FormDesignType CreateFormFromTemplatePath(string path, string formID, string lineage, string version, string fullURI);
        //FormDesignType CreateFormFromTemplateXML(string xml, string formID, string lineage, string version, string fullURI);
        //bool RemoveFormFromPackage(RetrieveFormPackageType pkg, FormDesignType form);
        #endregion

        #region Base Types
        //ExtensionBaseType AddExtensionBaseTypeItems(ExtensionBaseType ebt);

        #region Comment
        CommentType AddComment(ExtensionBaseType ebtParent, int insertPosition = -1);
        public bool RemoveComment(CommentType comment);
        public bool MoveComment(CommentType comment, ExtensionBaseType ebtTarget, int newListIndex);
        #endregion



        #region Extension Type

        ExtensionType AddExtension(ExtensionBaseType ebtParent, int insertPosition = -1);
        public bool RemoveExtension(ExtensionType extension);
        public bool MoveExtension(ExtensionType extension, ExtensionBaseType ebtTarget, int newListIndex);
        #endregion

        #region Property
        PropertyType AddProperty(ExtensionBaseType dtParent, int insertPosition = -1);
        public bool RemoveProperty(PropertyType property);
        public bool MoveProperty(PropertyType property, ExtensionBaseType ebtTarget, int newListIndex);

        #endregion

        #endregion

        #region DisplayedType helpers

        LinkType AddLink(DisplayedType dtParent, int insertPosition = -1);
        BlobType AddBlob(DisplayedType dtParent, int insertPosition = -1);
        //bool RemoveLink(DisplayedType dtParent, int removePosition = -1);
        //bool RemoveBlob(DisplayedType dtParent, int removePosition = -1);
        //allow move to new parents?  Moving allowed from ebt to ebt
        //void MoveAllowedEBT(string movingItemObjectID, string targetItemObjectID, int insertPosition);
        #endregion

        #region FormDesign Main Items

        #region Header, Body, Footer
        SectionItemType AddHeader(FormDesignType fd);
        SectionItemType AddBody(FormDesignType fd);
        SectionItemType AddFooter(FormDesignType fd);
        //bool RemoveHeader(FormDesignType fd);
        //bool RemoveFooter(FormDesignType fd);

        #endregion

        #region Other Main Items

        SectionItemType AddSection<T>(T T_Parent, string id = "", int insertPosition = -1) where T : BaseType, IParent; //, new();
        DisplayedType AddDisplayedItem<T>(T T_Parent, string id = "", int insertPosition = -1) where T : BaseType, IParent; //, new();
        ButtonItemType AddButtonAction<T>(T T_Parent, string id = "", int insertPosition = -1) where T : BaseType, IParent; //, new();
        InjectFormType AddInjectedForm<T>(T T_Parent, string id = "", int insertPosition = -1) where T : BaseType, IParent; //, new();
        //C AddChildItem<P, C>(P parent, string childID = "", int insertPosition = -1) 
        //    where P : IdentifiedExtensionType, IParent
        //    where C : IdentifiedExtensionType, IChildItem, new();
        //bool RemoveChildItem<C>(C childItem) where C: IChildItem;
        //bool MoveChildItem<P, C>(C childItem, int newPosition, P targetParent) where P: IParent where C: IChildItem ; 

        //allow move to new parents?  The above check will also work for iet-derived parent classes lile DIs
        #endregion
        #region IChildItemMember
        bool Remove<T>(T source) where T :notnull, IdentifiedExtensionType, IChildItemMember;
        bool MoveAsChild<S, T>(S source, T target, int newListIndex)
            where S : notnull, IdentifiedExtensionType    //, IChildItemMember
            where T : DisplayedType, IParent;
        bool IsMoveAllowedToChild<S, T>(S source, T target, out string error)
            where S : notnull, IdentifiedExtensionType
            where T: notnull, IdentifiedExtensionType;
        bool MoveAfterSib<S, T>(S source, T target, int newListIndex, bool moveBefore = false)
            where S : notnull, IdentifiedExtensionType
            where T : notnull, IdentifiedExtensionType;
        bool IsDisplayedItem(BaseType target);

        #endregion
        #region IQuestionListMember
        bool IsMoveAllowedToList<S,T>(S source, T target, out string error)
            where S : notnull, DisplayedType
            where T: notnull, DisplayedType;
        bool MoveInList(DisplayedType source, QuestionItemType target);
        bool MoveInList(DisplayedType source, DisplayedType target, bool moveAbove = false);
        #endregion
        #region QAS
        QuestionItemType AddQuestion<T>(T T_Parent, QuestionEnum qType, string id = "", int insertPosition = -1) where T : BaseType, IParent; //, new();
        ListItemResponseFieldType AddListItemResponseField(ListItemBaseType liParent);
        ListItemType AddListItemToQuestion(QuestionItemType qParent, string id = "", int insertPosition = -1);
        ListItemType AddListItemToList(QuestionItemType qParent, string id = "", int insertPosition = -1);
        ListItemType AddListItemResponseToQuestion(QuestionItemType qParent, string id = "", int listIndex = -1);
        UnitsType AddUnits(ResponseFieldType rfParent);
        //LookupEndPointType AddLookupEndpoint(ListFieldType lfParent);


        #endregion
        #endregion

        #region Coding
        CodingType AddCodedValue(DisplayedType dt, int insertPosition = -1);
        CodingType AddCodedValue(LookupEndPointType lep, int insertPosition = -1);
        UnitsType AddUnits(CodingType ctParent);
        #endregion

        #region Events
        PredGuardType AddActivateIf(DisplayedType dt);
        PredGuardType AddDeActivateIf(DisplayedType dt);
        EventType AddOnEnterEvent(DisplayedType dt);
        OnEventType AddOnEventEvent(DisplayedType dt);
        EventType AddOnExitEvent(DisplayedType dt);
        #endregion

        #region Contacts

        ContactType AddContact(DisplayedType dtParent, int insertPosition = -1);
        ContactType AddContact(FileType ftParent, int insertPosition = -1);
        OrganizationType AddOrganization(ContactType contactParent);
        OrganizationType AddOrganization(JobType jobParent);
        OrganizationType AddOrganizationItems(OrganizationType ot);
        PersonType AddPerson(ContactType contact);
        PersonType AddPerson(DisplayedType dt, int insertPosition = -1);
        PersonType AddContactPerson(OrganizationType otParent, int insertPosition = -1);
        #endregion
        #region  Actions
        public ActActionType AddActAction(ActionsType at, int insertPosition = -1);
        public RuleSelectMatchingListItemsType AddActSelectMatchingListItems(ActionsType at, int insertPosition = -1);
        //public abstract ActSetPropertyType AddSetProperty(ActionsType at);
        public ActAddCodeType AddActAddCode(ActionsType at, int insertPosition = -1);
        //public abstract ActSetValueType AddSetValue(ActionsType at);
        public ActInjectType AddActInject(ActionsType at, int insertPosition = -1);
        public CallFuncActionType AddActShowURL(ActionsType at, int insertPosition = -1);
        public ActSaveResponsesType AddActSaveResponses(ActionsType at, int insertPosition = -1);
        public ActSendReportType AddActSendReport(ActionsType at, int insertPosition = -1);
        public ActSendMessageType AddActSendMessage(ActionsType at, int insertPosition = -1);
        public ActSetAttributeType AddActSetAttributeValue(ActionsType at, int insertPosition = -1);
        public ActSetAttrValueScriptType AddActSetAttributeValueScript(ActionsType at, int insertPosition = -1);
        public ActSetBoolAttributeValueCodeType AddActSetBoolAttributeValueCode(ActionsType at, int insertPosition = -1);
        public ActShowFormType AddActShowForm(ActionsType at, int insertPosition = -1);
        public ActShowMessageType AddActShowMessage(ActionsType at, int insertPosition = -1);
        public ActShowReportType AddActShowReport(ActionsType at, int insertPosition = -1);
        public ActPreviewReportType AddActPreviewReport(ActionsType at, int insertPosition = -1);
        public ActValidateFormType AddActValidateForm(ActionsType at, int insertPosition = -1);
        public ScriptCodeAnyType AddActRunCode(ActionsType at, int insertPosition = -1);
        public CallFuncActionType AddActCallFunctionction(ActionsType at, int insertPosition = -1);
        public PredActionType AddActConditionalGroupAction(ActionsType at, int insertPosition = -1);

        #endregion


        #region Resources
        HTML_Stype AddHTML(RichTextType rt);
        string CreateName(BaseType bt);
        #endregion

    }
}

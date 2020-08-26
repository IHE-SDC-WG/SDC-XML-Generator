using System;
using System.IO;

namespace SDC.Schema2
{
    /// <summary>
    /// Top-level public methods used to build SDC tree in SDC.Schema.PartialClasses
    /// </summary>
    public interface ITreeBuilderRemote    {
        #region Package
        #endregion
        #region Map
        #endregion
        #region DataElement
        #endregion
        #region DemogForm                
        #endregion

        #region New Form
        FormDesignType CreateForm(bool addHeader, bool addFooter, string formID, string lineage, string version, string fullURI, out string result, out bool success);
        FormDesignType CreateFormFromTemplatePath(string path, string formID, string lineage, string version, string fullURI, out string result, out bool success);
        FormDesignType CreateFormFromTemplateXML(string xml, string formID, string lineage, string version, string fullURI, out string result, out bool success);
        FormDesignType RemoveFormFromPackage(string formID, out string result, out bool success);
        #endregion

        #region Base Types
        //ExtensionBaseType AddExtensionBaseTypeItems(ExtensionBaseType ebt);

        #region ExtensionBaseType
        #endregion
        #endregion

        #region DisplayedType helpers

        LinkType AddLink(string parentID, int insertPosition, out string result, out bool success, out string name);
        BlobType AddBlob(string parentID, int insertPosition, out string result, out bool success, out string name);
        void RemoveLink(string parentID, int removePosition, out string result, out bool success);
        void RemoveBlob(string parentID, int removePosition, out string result, out bool success);
        //allow move to new parents?  Moving allowed from ebt to ebt
        void MoveAllowedEBT(string movingItemObjectID, string targetItemObjectID, int insertPosition);
        #endregion

        #region FormDesign Main Items

        #region Header, Body, Footer
        SectionItemType AddHeader(string formID, string id, out bool success);
        SectionItemType AddBody(string formID, string id, out bool success);
        SectionItemType AddFooter(string formID, string id, out bool success);
        void RemoveHeader(string formID, string id, out string result, out bool success);
        void RemoveFooter(string formID, string id, out string result, out bool success);
        #endregion

        #region Other Main Items

        SectionItemType AddSection(string parentID, string id, int insertPosition, out string result, out bool success);
        DisplayedType AddDisplayedItem(string parentID, string id, int insertPosition, out string result, out bool success);
        ButtonItemType AddButtonAction(string parentID, string id, int insertPosition, out string result, out bool success);
        InjectFormType AddInjectedForm(string parentID, string id, int insertPosition, out string result, out bool success);
        bool RemoveSection(string id);
        bool RemoveDisplayedItem(string id);
        bool RemoveButtonAction(string id);
        bool RemoveInjectedForm(string id);
        bool MoveSection(string id, int newPosition, string newParentID = "");
        bool MoveDisplayedItem(string id, int newPosition, string newParentID = "");
        bool MoveButtonAction(string id, int newPosition, string newParentID = "");
        bool MoveInjectedForm<T>(string id, int newPosition, string newParentID = "");
        bool MoveAllowed(string movingItemID, string targetItemID, int insertPosition = -1);  //moving allowed from IParent to IParent only
        //allow move to new parents?  The above check will also work for iet-derived parent classes lile DIs
        #endregion

        #region QAS
        QuestionItemType AddQuestion(string parentID, string id, int insertPosition, out string result, out bool success);
        ListItemResponseFieldType AddListItemResponseField(string questionID, out string result, out bool success, out string name);
        ListItemType AddListItemToQuestion(string questionID, string id, int listIndex, out string result, out bool success);
        ListItemType AddListItemResponseToQuestion(string questionID, string id, int listIndex, out string result, out bool success);
        UnitsType AddUnits(string questionID, out string result, out bool success, out string name);
        LookupEndPointType AddLookupEndpoint(string questionID, out string result, out bool success, out string name);
        //Remove Q, LI
        //Move Q, LI


        #endregion
        #endregion
        #region ExensionBase
        CommentType AddComment(string parentID, int insertPosition, out string result, out bool success, out string name);
        ExtensionType AddExtension(string parentID, int insertPosition, out string result, out bool success, out string name);
        PropertyType AddProperty(string parentID, int insertPosition, out string result, out bool success, out string name);
        bool RemoveComment(string parentID, int removePosition, out string result, out bool success);
        bool RemoveExtension(string parentID, int removePosition, out string result, out bool success);
        bool RemoveProperty(string parentID, int removePosition, out string result, out bool success);
        //allow move to new parents?

        //FillProperty
        //FillComment
        //FillExtension
        public bool RemoveComment(string commentName, out string result, out bool success);
        public bool RemoveExtension(string extensionName, out string result, out bool success);
        public bool RemoveProperty(string propertyName, out string result, out bool success);

        public bool MoveComment(string parentID, int currentIndex, string targetParentID, int insertIndex, out string result, out bool success); //if targetParent == "", we assume the target has not changed
        public bool MoveExtension(string parentID, int currentIndex, string targetParentID, int insertIndex, out string result, out bool success);
        public bool MoveProperty(string parentID, int currentIndex, string targetParentID, int insertIndex, out string result, out bool success);
        public bool MoveComment(string commentName, string targetParentName, int insertIndex, out string result, out bool success); 
        public bool MoveExtension(string extensionName, string targetParentName, int insertIndex, out string result, out bool success);
        public bool MoveProperty(string propertyName, string targetParentName, int insertIndex, out string result, out bool success);

        public bool FillComment(string commentName, string val, out string result, out bool success);
        public bool FillExtension(string extensionName, string val, out string result, out bool success);
        public bool FillProperty(string propertyName, string val, string propName, string propClass, out string result, out bool success);





        public bool FillBase(string name, string type, string styleClass, out string result, out bool success);

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

        #region ID Resources

        //string GetParentObjectIDFromName(string nodeName);
        string GetParentIDFromName(string nodeName);
        string GetParentNameFromName(string nodeName);

        //string GetParentObjectIDFromID(string nodeID);
        string GetParentIDFromID(string nodeID);  //returns IET
        string GetParentNameFromID(string nodeID);

        //string GetObjectIDFromID(string nodeID);
        string GetNameFromID(string nodeID);  //returns IET
        //string GetObjectIDFromName(string nodeName);
        string GetIDFromName(string nodeName);

        #endregion

    }
}

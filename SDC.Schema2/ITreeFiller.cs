using System;

namespace SDC.Schema2
{
    /// <summary>
    /// Top-level public methods used to build SDC tree in SDC.Schema.PartialClasses
    /// </summary>
    public interface ITreeFiller
    {

        #region Base Types
        BaseType FillBaseTypeItem(BaseType bt, Boolean fillData = true);
        ExtensionBaseType AddExtensionBaseTypeItems(ExtensionBaseType ebt, Boolean fillData = true);
        IdentifiedExtensionType FillIdentifiedTypeItems(IdentifiedExtensionType iet, Boolean fillData = true);
        RepeatingType FillRepeatingTypeItemData(RepeatingType rt);

        #region Extension Type
        CommentType AddFillComment(ExtensionBaseType ebtParent, Boolean fillData = true, string comment = "");
        ExtensionType AddFillExtension(ExtensionBaseType ebt, Boolean fillData = true, string InXML = "");
        #endregion
        #endregion

        #region DisplayedType helpers
        PropertyType AddProperty(IdentifiedExtensionType dtParent, Boolean fillData = true);
        PropertyType FillProperty(PropertyType ot, Boolean fillData = true);
        LinkType FillLinkText(LinkType lt);
        BlobType FillBlob(BlobType bt);

        LinkType AddLink(DisplayedType dtParent, Boolean fillData = true);
        BlobType AddBlob(DisplayedType dtParent, Boolean fillData = true);
        #endregion

        #region FormDesign Main Items

        #region Header, Body, Footer
        SectionItemType AddHeader(Boolean fillData = false, string id = null);
        SectionItemType AddBody(Boolean fillData = false, string id = null);
        SectionItemType AddFooter(Boolean fillData = false, string id = null);
        #endregion

        #region Other Main Items

        SectionItemType AddSection<T>(T T_Parent, Boolean fillData = true, string id = null) where T : BaseType, IParent; //, new();
        SectionBaseType FillSectionBase(SectionBaseType s);
        InjectFormType FillInjectedForm(InjectFormType injForm);

        DisplayedType AddDisplayedItem<T>(T T_Parent, Boolean fillData = true, string id = null) where T : BaseType, IParent; //, new();
        DisplayedType FillDisplayedTypeItems(DisplayedType dt, Boolean fillData = true);
        RepeatingType FillRepeatingTypeItems(RepeatingType rt, Boolean fillData = true);
        //DisplayedType AddDisplayedItemToList(ListType list);

        ButtonItemType AddButtonAction<T>(T T_Parent, Boolean fillData = true, string id = null) where T : BaseType, IParent; //, new();
        InjectFormType AddInjectedForm<T>(T T_Parent, Boolean fillData = true, string id = null) where T : BaseType, IParent; //, new();

        #endregion

        #region QAS
        QuestionItemType AddQuestion<T>(T T_Parent, QuestionEnum qType, Boolean fillData = true, string id = null) where T : BaseType, IParent; //, new();
        QuestionItemBaseType FillQuestionItemBase(QuestionItemBaseType q);

        ListFieldType FillListField(ListFieldType lf);

        ListItemBaseType FillListItemBase(ListItemBaseType li);
        ListItemResponseFieldType AddListItemResponseField(ListItemBaseType li, Boolean fillData = true);
        ListItemResponseFieldType FillListItemResponseField(ListItemResponseFieldType lirf);

        //ResponseFieldType AddQuestionResponseField(QuestionItemType qParent, Boolean fillData = true);
        ResponseFieldType FillResponseField(ResponseFieldType rf);
        //DataTypes_DEType AddFillDataTypesDE(ResponseFieldType rfParent);
        //DataTypes_DEType AddFillDataTypesDE(ListFieldType lftParent, SectionBaseTypeResponseTypeEnum dataType);

        LookupEndPointType FillLookupEndpoint(LookupEndPointType lep);

        UnitsType AddUnits(ResponseFieldType rf, bool fillData = true);
        UnitsType FillUnits(UnitsType ut);

        #endregion
        #endregion

        #region Coding
        CodingType AddCodedValue(DisplayedType dt, Boolean fillData = true);
        CodingType AddCodedValue(LookupEndPointType lep, Boolean fillData = true);
        CodeSystemType FillCodeSystemItems(CodeSystemType cs);
        #endregion

        #region Events
        PredGuardType AddActivateIf(DisplayedType dt, Boolean fillData = true);
        PredGuardType AddDeActivateIf(DisplayedType dt, Boolean fillData = true);
        EventType AddOnEnter(DisplayedType dt, Boolean fillData = true);
        OnEventType AddOnEvent(DisplayedType dt, Boolean fillData = true);
        EventType AddOnExit(DisplayedType dt, Boolean fillData = true);
        #endregion

        #region Contacts

        ContactType AddContact(DisplayedType dt, Boolean fillData = true);
        ContactType AddContact(FileType ft, Boolean fillData = true);
        OrganizationType AddFillOrganization(ContactType contact, Boolean fillData = true);
        OrganizationType AddFillOrganization(JobType job, Boolean fillData = true);
        OrganizationType AddFillOrganizationItems(OrganizationType ot, Boolean fillData = true);
        PersonType AddPerson(ContactType contact);
        PersonType AddPerson(DisplayedType dt);
        #endregion

        #region Resources
        HTML_Stype AddFillHTML(RichTextType rt, Boolean fillData = true, string InXhtml = "");
        string CreateName(BaseType bt);

        String SerializeFormDesignTree();

        #endregion

    }
}

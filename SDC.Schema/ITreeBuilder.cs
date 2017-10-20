using System;

namespace SDC
{
    /// <summary>
    /// Top-level public methods used to build SDC tree in SDC.Schema.PartialClasses
    /// </summary>
    public interface ITreeBuilder
    {

        #region Base Types
        BaseType AddFillBaseTypeItems(BaseType bt, Boolean fillData = true);
        ExtensionBaseType AddFillExtensionBaseTypeItems(ExtensionBaseType ebt, Boolean fillData = true);
        IdentifiedExtensionType AddFillIdentifiedTypeItems(IdentifiedExtensionType iet, Boolean fillData = true);
        RepeatingType FillRepeating(RepeatingType rt);

        #region Extension Type
        CommentType AddFillComment(ExtensionBaseType ebtParent, Boolean fillData = true, string comment = "");
        ExtensionType AddFillExtension(ExtensionBaseType ebt, Boolean fillData = true, string InXML = "");
        #endregion
        #endregion

        #region DisplayedType helpers
        PropertyType AddFillProperty(IdentifiedExtensionType dtParent, Boolean fillData = true);
        LinkType AddFillLink(DisplayedType dtParent, Boolean fillData = true);
        BlobType AddFillBlob(DisplayedType dtParent, Boolean fillData = true);
        #endregion

        #region FormDesign Main Items

        #region Header, Body, Footer
        SectionItemType AddFillHeader(Boolean fillData = false);
        SectionItemType AddFillBody(Boolean fillData = false);
        SectionItemType AddFillFooter(Boolean fillData = false);
        #endregion

        #region Other Main Items

        SectionItemType AddFillSection<T>(T T_Parent, Boolean fillData = true) where T : BaseType, IParent, new();
        SectionBaseType FillSectionBase(SectionBaseType s);

        DisplayedType AddFillDisplayedItem<T>(T T_Parent, Boolean fillData = true) where T : BaseType, IParent, new();
        DisplayedType AddFillDisplayedTypeItems(DisplayedType dt, Boolean fillData = true);
        //DisplayedType AddDisplayedItemToList(ListType list);

        ButtonItemType AddFillButtonAction<T>(T T_Parent, Boolean fillData = true) where T : BaseType, IParent, new();
        InjectFormType AddFillInjectedForm<T>(T T_Parent, Boolean fillData = true) where T : BaseType, IParent, new();
        #endregion

        #region QAS
        QuestionItemType AddFillQuestion<T>(T T_Parent, QuestionEnum qType, Boolean fillData = true) where T : BaseType, IParent, new();
        QuestionItemBaseType FillQuestionItemBase(QuestionItemBaseType q);

        ListFieldType FillListField(ListFieldType lf);

        ListItemBaseType FillListItemBase(ListItemBaseType li);
        ListItemResponseFieldType AddFillListItemResponseField(ListItemBaseType li, Boolean fillData = true);
        ListItemResponseFieldType FillListItemResponseField(ListItemResponseFieldType lirf);

        LookupEndPointType FillLookupEndpoint(LookupEndPointType lep);

        UnitsType FillUnits(UnitsType ut);
        #endregion
        #endregion

        #region Coding
        CodingType AddFillCodedValue(DisplayedType dt, Boolean fillData = true);
        CodingType AddFillCodedValue(LookupEndPointType lep, Boolean fillData = true);
        #endregion

        #region Events
        WatchedPropertyType AddFillActivateIf(DisplayedType dt, Boolean fillData = true);
        WatchedPropertyType AddFillDeActivateIf(DisplayedType dt, Boolean fillData = true);
        IfThenType AddFillOnEnter(DisplayedType dt, Boolean fillData = true);
        IfThenType AddFillOnEvent(DisplayedType dt, Boolean fillData = true);
        OnEventType AddFillOnExit(DisplayedType dt, Boolean fillData = true);
        #endregion

        #region Contacts

        ContactType AddFillContact(DisplayedType dt, Boolean fillData = true);
        ContactType AddFillContact(FileType ft, Boolean fillData = true);
        OrganizationType AddFillOrganization(ContactType contact, Boolean fillData = true);
        OrganizationType AddFillOrganization(JobType job, Boolean fillData = true);
        OrganizationType AddFillOrganizationItems(OrganizationType ot, Boolean fillData = true);
        PersonType AddPerson(ContactType contact);
        PersonType AddPerson(DisplayedType dt);
        #endregion

        #region Resources
        HTML_Stype AddFillHTML(RichTextType rt, Boolean fillData = true, string InXhtml = "");
        String SerializeFormDesignTree();

        #endregion

        #region Utilities
        //Tenum ConvertStringToEnum<Tenum>(string inputString) where Tenum : struct;

        #endregion
    }
}

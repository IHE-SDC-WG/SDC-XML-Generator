using System;
using System.Linq;

namespace SDC
{
    /// <summary>
    /// Database item types
    /// </summary>
    public enum ItemTypeEccEnum
    {
        Template,
        ListItem,
        ListItemFill,
        QuestionSingle,
        QuestionMultiple,
        QuestionFill,
        QuestionLookup,
        Section,
        Note,
        FixedListNote,
        Rule,
        InjectedTemplate,
        Button
    }

    /// <summary>
    /// These are database representations of form items that need to be converted to SDC types
    /// </summary>
    [Flags]
    public enum ItemTypeEnum
    {   //HEX enum values let us test  for membership of multiple enum types
        //and let us easily define group types
        None = 0x0,

        DisplayedItem = 0x2,

        //Question types
        QuestionSingle = 0x4,
        QuestionMultiple = 0x8,
        QuestionFill = 0x10,
        QuestionLookup = 0x20,
        QuestionGroup = QuestionSingle | QuestionMultiple | QuestionFill | QuestionLookup,

        Rule = 0x40,
        Button = 0x80,
        InjectForm = 0x100,

        ListItem = 0x200,
        ListNote = 0x400,
        ListItemFillin = 0x800,
        ListItemGroup = ListItem | ListNote,

        //Section types
        Header = 0x100000, //100 thousand
        Body = 0x200000,
        Footer = 0x400000,
        Section = 0x800000,
        SectionGroup = Section | Header | Body | Footer,

        //Form types
        FormDesign = 0x1000000, //1 million
        InjectedTemplate = 0x2000000,
        FormGroup = FormDesign | InjectedTemplate,

        _IdentifiedExtensionType = 0x10000000, //10 million
        _BaseType = 0x20000000,
        _BaseTypeGroup = _IdentifiedExtensionType | _BaseType,


    }


    public enum RuleEnum
    { }

    [Flags]
    public enum HelperTypes //TODO: Clean this up
    {
        Tooltip = 0x1,
        PopupNote = 0x2,
        Description = 0X4,

        OtherText = Tooltip | PopupNote | Note | ReportNote | ReportText | Description,

        CodedValue = 0x10,
        Note = 0x20,
        ReportNote = 0x40,
        ReportText = 0x80,
        Link = 0x100,

        Image = 0x200,
        Video = 0x400,
        Blob = 0x800,
        Dicom = 0x1000,
        BlobGroup = Image | Video | Blob | Dicom,
    }

    [Flags]
    public enum QuestionEnum
    {
        QuestionSingle = ItemTypeEnum.QuestionSingle,
        QuestionMultiple = ItemTypeEnum.QuestionMultiple,
        QuestionFill = ItemTypeEnum.QuestionFill,
        QuestionLookup = ItemTypeEnum.QuestionLookup,
        QuestionGroup = ItemTypeEnum.QuestionGroup
    }

    [Flags]
    public enum SectionEnum
    {
        Header = ItemTypeEnum.Header,
        Body = ItemTypeEnum.Body,
        Footer = ItemTypeEnum.Footer,
        Section = ItemTypeEnum.Section,
        SectionGroup = ItemTypeEnum.SectionGroup
    }

    public enum SDCtypes
    {
        AcceptabilityType,
        ActInjectType,
        ActSaveResponsesType,
        ActSendMessageType,
        ActSendReportType,
        ActSetValueType,
        ActShowFormType,
        ActShowMessageType,
        ActShowReportType,
        ActValidateFormType,
        AddressType,
        anyType_DEtype,
        anyURI_DEtype,
        anyURI_Stype,
        ApprovalType,
        AreaCodeType,
        AssociatedFilesType,
        base64Binary_DEtype,
        base64Binary_Stype,
        BaseType,
        BlobType,
        boolean_DEtype,
        boolean_Stype,
        ButtonItemType,
        byte_DEtype,
        byte_Stype,
        ChangedFieldType,
        ChangedListItemType,
        ChangedSelectedItemsType,
        ChangeLogType,
        ChangeTrackingType,
        ChangeType,
        ChildItemsType,
        CodeMatchType,
        CodeSystemType,
        CodingType,
        CommentType,
        ComplianceRuleType,
        ContactsType,
        ContactType,
        CountryCodeType,
        DataTypes_DEType,
        DataTypes_SType,
        date_DEtype,
        date_Stype,
        dateTime_DEtype,
        dateTime_Stype,
        dateTimeStamp_DEtype,
        dateTimeStamp_Stype,
        dayTimeDuration_DEtype,
        dayTimeDuration_Stype,
        decimal_DEtype,
        decimal_Stype,
        DestinationType,
        DisplayedType,
        double_DEtype,
        double_Stype,
        duration_DEtype,
        duration_Stype,
        EmailAddressType,
        EmailType,
        ExclusiveItemPairsType,
        ExpressionType,
        ExtensionBaseType,
        ExtensionType,
        FileDatesType,
        FileHashType,
        FileType,
        FileUsageType,
        float_DEtype,
        float_Stype,
        FormDesignType,
        gDay_DEtype,
        gDay_Stype,
        GetCodeType,
        GetPropertyValuesType,
        gMonth_DEtype,
        gMonth_Stype,
        gMonthDay_DEtype,
        gMonthDay_Stype,
        gYear_DEtype,
        gYear_Stype,
        gYearMonth_DEtype,
        gYearMonth_Stype,
        HashType,
        hexBinary_DEtype,
        hexBinary_Stype,
        HTML_DEtype,
        HTML_Stype,
        IdentifiedExtensionType,
        IdentifierType,
        IfBoolCompareType,
        IfThenType,
        IfType,
        InjectFormType,
        int_DEtype,
        int_Stype,
        integer_DEtype,
        integer_Stype,
        ItemNameType,
        JobType,
        LanguageCodeISO6393_Type,
        LanguageType,
        LinkType,
        ListFieldType,
        ListItemBaseType,
        ListItemResponseFieldType,
        ListItemType,
        ListType,
        long_DEtype,
        long_Stype,
        LookupEndPointType,
        MaxExclusiveType,
        MaxInclusiveType,
        MinExclusiveType,
        MinInclusiveType,
        NameType,
        negativeInteger_DEtype,
        negativeInteger_Stype,
        nonNegativeInteger_DEtype,
        nonNegativeInteger_Stype,
        nonPositiveInteger_DEtype,
        nonPositiveInteger_Stype,
        OnEventType,
        OrganizationType,
        ParameterType,
        PersonType,
        PhoneNumberType,
        PhoneType,
        positiveInteger_DEtype,
        positiveInteger_Stype,
        PredicateBetweenType,
        PredicateCompareType,
        PredicateInListType,
        PredicateType,
        ProvenanceType,
        QuestionItemBaseType,
        QuestionItemType,
        RepeatingType,
        ReplacedIDsType,
        ReplacedResponseType,
        ResponseChangeType,
        ResponseFieldType,
        RichTextType,
        RulesType,
        ScriptCodeType,
        SectionBaseType,
        SectionItemType,
        SetPropertyType,
        short_DEtype,
        short_Stype,
        string_DEtype,
        string_Stype,
        SubmissionRuleType,
        TargetItemIDType,
        TargetItemNameType,
        TargetItemXPathType,
        ThenType,
        time_DEtype,
        time_Stype,
        UnitsType,
        unsignedByte_DEtype,
        unsignedByte_Stype,
        unsignedInt_DEtype,
        unsignedInt_Stype,
        unsignedLong_DEtype,
        unsignedLong_Stype,
        unsignedShort_DEtype,
        unsignedShort_Stype,
        VersionType,
        WatchedPropertyType,
        WebServiceType,
        XML_DEtype,
        XML_Stype,
        yearMonthDuration_DEtype,
        yearMonthDuration_Stype,



    }
}
// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 4.4.0.7
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
namespace SDC.Schema
{
using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Collections.Generic;

[System.Xml.Serialization.XmlIncludeAttribute(typeof(BasePackageType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SimpleSdcRetrieveFormPackageType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataStoreType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataSourceType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(TemplateTargetType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ItemMapType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(MappingType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RegisteredItemStateType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RegisteredItemType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(InterfaceType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RegistrySummaryType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RegistryType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(TemplateAdminType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataTypesDateTime_SType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataTypesDateTime_DEType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataTypesNumeric_SType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataTypesNumeric_DEType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActValidateFormType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActPreviewReportType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActShowReportType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActShowMessageType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActShowFormType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActSendReportType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActSaveResponsesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RuleAutoSelectType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RuleAutoActivationType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RulesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ListType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ListFieldType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ScriptCodeAnyType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActSetAttrValueType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataTypes_DEType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ResponseFieldType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ListItemResponseFieldType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ChildItemsType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ItemNameType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActAddCodeType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ItemNameAttributeType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActSetItemAttributeType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActActionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActionsType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(FuncBoolBaseType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ConditionalGroupActionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EventType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(OnEventType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ConditionsSubActionsType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GuardType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AlternativesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(GetItemAttribValuesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(MultiSelectionSetBoolType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RuleSingleSelectionSetsType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RuleSelectionTestType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ScriptCodeBoolType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActSetValueBoolType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CallBoolFuncType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(IdentifiedExtensionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(FormDesignType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(InjectFormType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActInjectType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DisplayedType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ButtonItemType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ListItemBaseType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ListItemType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RepeatingType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(QuestionItemBaseType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(QuestionItemType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SectionBaseType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SectionItemType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataElementType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LinkType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ChangeType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ComplianceRuleType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DestinationType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmissionRuleType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProvenanceType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AssociatedFilesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LanguageType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ApprovalType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(FileDatesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(FileUsageType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ContactsType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ChangedFieldType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ChangeLogType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(VersionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(FileType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(IdentifierType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PhoneType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EmailType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AddressType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(OrganizationType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(JobType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(NameType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PersonType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ContactType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CodeSystemType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CodingType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ParameterItemType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CallFuncType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LookupEndPointType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PhoneNumberType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EmailAddressType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RichTextType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActSendMessageType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BlobType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(DataTypes_SType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ParameterValueType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PropertyType1))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PropertyType))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public abstract partial class ExtensionBaseType : BaseType
{
    
    private static XmlSerializer serializer;
    
        [System.Xml.Serialization.XmlElementAttribute("Comment", Order=0)]
        public virtual List<CommentType> Comment { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("Extension", Order=1)]
        public virtual List<ExtensionType> Extension { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("Property", Order=2)]
        public virtual List<PropertyType> Property { get; set; }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(ExtensionBaseType));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether Comment should be serialized
    /// </summary>
    public virtual bool ShouldSerializeComment()
    {
        return Comment != null && Comment.Count > 0;
    }
    
    /// <summary>
    /// Test whether Extension should be serialized
    /// </summary>
    public virtual bool ShouldSerializeExtension()
    {
        return Extension != null && Extension.Count > 0;
    }
    
    /// <summary>
    /// Test whether Property should be serialized
    /// </summary>
    public virtual bool ShouldSerializeProperty()
    {
        return Property != null && Property.Count > 0;
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current ExtensionBaseType object into an XML string
    /// </summary>
    /// <returns>string XML value</returns>
    public virtual string Serialize(System.Text.Encoding encoding)
    {
        System.IO.StreamReader streamReader = null;
        System.IO.MemoryStream memoryStream = null;
        try
        {
            memoryStream = new System.IO.MemoryStream();
            System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
            xmlWriterSettings.Encoding = encoding;
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.IndentChars = " ";
            System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
            Serializer.Serialize(xmlWriter, this);
            memoryStream.Seek(0, SeekOrigin.Begin);
            streamReader = new System.IO.StreamReader(memoryStream, encoding);
            return streamReader.ReadToEnd();
        }
        finally
        {
            if ((streamReader != null))
            {
                streamReader.Dispose();
            }
            if ((memoryStream != null))
            {
                memoryStream.Dispose();
            }
        }
    }
    
    public virtual string Serialize()
    {
        return Serialize(System.Text.Encoding.UTF8);
    }
    
    /// <summary>
    /// Deserializes workflow markup into an ExtensionBaseType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output ExtensionBaseType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out ExtensionBaseType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(ExtensionBaseType);
        try
        {
            obj = Deserialize(input);
            return true;
        }
        catch (System.Exception ex)
        {
            exception = ex;
            return false;
        }
    }
    
    public static bool Deserialize(string input, out ExtensionBaseType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static ExtensionBaseType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((ExtensionBaseType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static ExtensionBaseType Deserialize(System.IO.Stream s)
    {
        return ((ExtensionBaseType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current ExtensionBaseType object into file
    /// </summary>
    /// <param name="fileName">full path of outupt xml file</param>
    /// <param name="exception">output Exception value if failed</param>
    /// <returns>true if can serialize and save into file; otherwise, false</returns>
    public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
    {
        exception = null;
        try
        {
            SaveToFile(fileName, encoding);
            return true;
        }
        catch (System.Exception e)
        {
            exception = e;
            return false;
        }
    }
    
    public virtual bool SaveToFile(string fileName, out System.Exception exception)
    {
        return SaveToFile(fileName, System.Text.Encoding.UTF8, out exception);
    }
    
    public virtual void SaveToFile(string fileName)
    {
        SaveToFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
    {
        System.IO.StreamWriter streamWriter = null;
        try
        {
            string xmlString = Serialize(encoding);
            streamWriter = new System.IO.StreamWriter(fileName, false, encoding);
            streamWriter.WriteLine(xmlString);
            streamWriter.Close();
        }
        finally
        {
            if ((streamWriter != null))
            {
                streamWriter.Dispose();
            }
        }
    }
    
    /// <summary>
    /// Deserializes xml markup from file into an ExtensionBaseType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output ExtensionBaseType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ExtensionBaseType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(ExtensionBaseType);
        try
        {
            obj = LoadFromFile(fileName, encoding);
            return true;
        }
        catch (System.Exception ex)
        {
            exception = ex;
            return false;
        }
    }
    
    public static bool LoadFromFile(string fileName, out ExtensionBaseType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out ExtensionBaseType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static ExtensionBaseType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static ExtensionBaseType LoadFromFile(string fileName, System.Text.Encoding encoding)
    {
        System.IO.FileStream file = null;
        System.IO.StreamReader sr = null;
        try
        {
            file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
            sr = new System.IO.StreamReader(file, encoding);
            string xmlString = sr.ReadToEnd();
            sr.Close();
            file.Close();
            return Deserialize(xmlString);
        }
        finally
        {
            if ((file != null))
            {
                file.Dispose();
            }
            if ((sr != null))
            {
                sr.Dispose();
            }
        }
    }
}
}
#pragma warning restore

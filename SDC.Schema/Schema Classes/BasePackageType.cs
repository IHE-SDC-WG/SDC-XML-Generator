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

[System.Xml.Serialization.XmlIncludeAttribute(typeof(SimpleSdcRetrieveFormPackageType))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class BasePackageType : ExtensionBaseType
{
    
    private bool _shouldSerializeresponseStatusEnum;
    
    private bool _shouldSerializeresponseTypeEnum;
    
    private static XmlSerializer serializer;
    
    /// <summary>
    /// Admin contains information about a package, including a description of the package contents and purpose (PackageDescription), information about the registry that contains the package file (RegistryData), and information about the package file characteristics (TemplateFile).
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public virtual TemplateAdminType Admin { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public virtual string packageID { get; set; }
    /// <summary>
    /// NEW
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string title { get; set; }
    /// <summary>
    /// If the ID does not use the default base URI (namespace), then the local baseURI goes here. Note that all IDs must be unique within a form, even if they do not have the same baseURI.
    /// 
    /// Ideally, the baseURI + ID should combine to form a *globally* unique identifier, that uniquely identifies an item in a particular form.  The same baseURI and ID may be reused in derived or versioned forms, as long as the context stays the same, and any affected data elements remain unchanged in context and semantics.  Following this approach is likely to simplify analytics based on form content.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public virtual string baseURI { get; set; }
    /// <summary>
    /// NEW: filename to use when the current FormDesign instance is saved as a file.
    /// For forms containing responses, the filename may include the formInstanceVersionURI,
    /// but the naming convention may be use-case-specific.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string filename { get; set; }
    /// <summary>
    /// NEW: URI used to identify the form that that this FormDesign is based upon.  In most cases, this should be a standard form that is modified and/or extended by the current FormDesign.
    /// 
    /// The current template reuses the basedOn IDs whenever the question/answer/semantic context is identical to the original.
    /// 
    /// 5/11/17:  Relying on data element mapping may be a better and more flexible approach than @basedOnURI. In this way, forms could compare data elements to determine if they contain semantic matches, and this is supported better with a more robust code map section.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public virtual string basedOnURI { get; set; }
    /// <summary>
    /// NEW: A text identifier that is used to group multiple versions of a single form.  The lineage is constant for all versions of a single kind of form.
    /// 
    /// When appended to @baseURI, it can be used to retrieve all versions of one particular form.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string lineage { get; set; }
    /// <summary>
    /// NEW: @version contains the version text for the current form.  It is designed to be used in conjuction with @baseURI and @lineage.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string version { get; set; }
    /// <summary>
    /// NEW: The full URI that uniquely identifies the current form.   It is created by concatenating @baseURI + lineage + version.  Each of the componenets is separated by a single forward slash.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public virtual string fullURI { get; set; }
    /// <summary>
    /// NEW: The full URI used to identify the form that is the immediate previous version of the current FormDesign
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public virtual string prevVersionURI { get; set; }
    /// <summary>
    /// NEW: Unique URI used to identify a unique instance of a form.  Used for tracking form responses across time and across multiple episodes of editing by end-users.  This URI does not change for each edit session of a form instance.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public virtual string formInstanceURI { get; set; }
    /// <summary>
    /// NEW: Unique URI used to identify a unique instance of a form's saved responses.  It is used for tracking form responses across time and across multiple episodes of editing by end-users.  This URI must change for each edit/save session of a form instance.  It may be e.g., a new GUID, or a repeat of the formInstanceID followed by a version number.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public virtual string formInstanceVersionURI { get; set; }
    /// <summary>
    /// NEW: Unique URI used to identify the immediate previous instance of a form containing responses.  This is the @formInstanceVersionURI that represents the instance of the form that the user opened up before beginning a new cycle of edit/save.  This attribute is used for tracking form responses across time and across multiple episodes of editing by end-users.  This URI must change for each edit session of a form instance.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public virtual string formPreviousInstanceVersionURI { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual SectionBaseTypeResponseTypeEnum responseTypeEnum { get; set; }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool responseTypeEnumSpecified { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual SectionBaseTypeResponseStatusEnum responseStatusEnum { get; set; }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool responseStatusEnumSpecified { get; set; }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(BasePackageType));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether responseTypeEnum should be serialized
    /// </summary>
    public virtual bool ShouldSerializeresponseTypeEnum()
    {
        if (_shouldSerializeresponseTypeEnum)
        {
            return true;
        }
        return (responseTypeEnum != default(SectionBaseTypeResponseTypeEnum));
    }
    
    /// <summary>
    /// Test whether responseStatusEnum should be serialized
    /// </summary>
    public virtual bool ShouldSerializeresponseStatusEnum()
    {
        if (_shouldSerializeresponseStatusEnum)
        {
            return true;
        }
        return (responseStatusEnum != default(SectionBaseTypeResponseStatusEnum));
    }
    
    /// <summary>
    /// Test whether Admin should be serialized
    /// </summary>
    public virtual bool ShouldSerializeAdmin()
    {
        return (Admin != null);
    }
    
    /// <summary>
    /// Test whether packageID should be serialized
    /// </summary>
    public virtual bool ShouldSerializepackageID()
    {
        return !string.IsNullOrEmpty(packageID);
    }
    
    /// <summary>
    /// Test whether title should be serialized
    /// </summary>
    public virtual bool ShouldSerializetitle()
    {
        return !string.IsNullOrEmpty(title);
    }
    
    /// <summary>
    /// Test whether baseURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializebaseURI()
    {
        return !string.IsNullOrEmpty(baseURI);
    }
    
    /// <summary>
    /// Test whether filename should be serialized
    /// </summary>
    public virtual bool ShouldSerializefilename()
    {
        return !string.IsNullOrEmpty(filename);
    }
    
    /// <summary>
    /// Test whether basedOnURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializebasedOnURI()
    {
        return !string.IsNullOrEmpty(basedOnURI);
    }
    
    /// <summary>
    /// Test whether lineage should be serialized
    /// </summary>
    public virtual bool ShouldSerializelineage()
    {
        return !string.IsNullOrEmpty(lineage);
    }
    
    /// <summary>
    /// Test whether version should be serialized
    /// </summary>
    public virtual bool ShouldSerializeversion()
    {
        return !string.IsNullOrEmpty(version);
    }
    
    /// <summary>
    /// Test whether fullURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializefullURI()
    {
        return !string.IsNullOrEmpty(fullURI);
    }
    
    /// <summary>
    /// Test whether prevVersionURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeprevVersionURI()
    {
        return !string.IsNullOrEmpty(prevVersionURI);
    }
    
    /// <summary>
    /// Test whether formInstanceURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeformInstanceURI()
    {
        return !string.IsNullOrEmpty(formInstanceURI);
    }
    
    /// <summary>
    /// Test whether formInstanceVersionURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeformInstanceVersionURI()
    {
        return !string.IsNullOrEmpty(formInstanceVersionURI);
    }
    
    /// <summary>
    /// Test whether formPreviousInstanceVersionURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeformPreviousInstanceVersionURI()
    {
        return !string.IsNullOrEmpty(formPreviousInstanceVersionURI);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current BasePackageType object into an XML string
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
    /// Deserializes workflow markup into an BasePackageType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output BasePackageType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out BasePackageType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(BasePackageType);
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
    
    public static bool Deserialize(string input, out BasePackageType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static BasePackageType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((BasePackageType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static BasePackageType Deserialize(System.IO.Stream s)
    {
        return ((BasePackageType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current BasePackageType object into file
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
    /// Deserializes xml markup from file into an BasePackageType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output BasePackageType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out BasePackageType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(BasePackageType);
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
    
    public static bool LoadFromFile(string fileName, out BasePackageType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out BasePackageType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static BasePackageType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static BasePackageType LoadFromFile(string fileName, System.Text.Encoding encoding)
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

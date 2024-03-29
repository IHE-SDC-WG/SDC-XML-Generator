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

/// <summary>
/// Information about a file, usually thought of as a binary byte stream
/// stored on disk. A FileType can also represent a "virtual" file, such as an XML
/// module in a larger XML document. Such a virtual file could theoretically be stored
/// as a byte stream, as an independant file on disk or as a database record or set of
/// records, even if this byte stream is never actually persisted as an independant disk
/// file.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class FileType : ExtensionBaseType
{
    
    private static XmlSerializer serializer;
    
    /// <summary>
    /// Internal/local File ID, not necessarily in the format
    /// of the FileURI used for all SDC FormDesign items.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public virtual string_Stype FileID { get; set; }
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public virtual anyURI_Stype FileURI { get; set; }
    /// <summary>
    /// Text to be displayed that encapulates the file
    /// contents. This may be the same as the internal Title of the
    /// file.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public virtual string_Stype DisplayName { get; set; }
    /// <summary>
    /// Official title of the file.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public virtual string_Stype Title { get; set; }
    /// <summary>
    /// File version
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public virtual VersionType Version { get; set; }
    /// <summary>
    /// The name of the file as saved on disk or other persistant storage.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public virtual string_Stype FileName { get; set; }
    /// <summary>
    /// The file type extension that describes the file's
    /// internal format. This is usually the 3-4 character text that appears
    /// after the last period in the file name, e.g., txt, docx,
    /// etc.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public virtual string_Stype FileExtension { get; set; }
    /// <summary>
    /// A short description of the class of file, such as "FormDesign XML"
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public virtual string_Stype FileClass { get; set; }
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public virtual positiveInteger_Stype FileSizeKB { get; set; }
    /// <summary>
    /// Any additional information about the template or file. The type of information should be specified in the @type
    /// attribute.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Description", Order=9)]
        public virtual List<string_Stype> Description { get; set; }
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public virtual string_Stype Copyright { get; set; }
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public virtual string_Stype TermsofUse { get; set; }
    /// <summary>
    /// Guidance for when this file should be used, and when it should not be used.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public virtual FileUsageType Usage { get; set; }
    /// <summary>
    /// Various dates associated with the file release, versioning and usage.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public virtual FileDatesType Dates { get; set; }
    /// <summary>
    /// People and Organizations associated with the file.
    /// Specify the type of Contact in the @type attribute. Examples of
    /// @type include Curator, Author, and Authority.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        public virtual ContactsType Contacts { get; set; }
    /// <summary>
    /// Documentation of review and acceptance of the file for production usage.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Approval", Order=15)]
        public virtual List<ApprovalType> Approval { get; set; }
        [System.Xml.Serialization.XmlElementAttribute(Order=16)]
        public virtual FileHashType FileHash { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("Language", Order=17)]
        public virtual List<LanguageType> Language { get; set; }
    /// <summary>
    /// Link to any associated files, such as schemas, reference documents, manuals, etc.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=18)]
        public virtual AssociatedFilesType AssociatedFiles { get; set; }
        [System.Xml.Serialization.XmlElementAttribute(Order=19)]
        public virtual ProvenanceType Provenance { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("DefaultSubmissionRule", Order=20)]
        public virtual List<SubmissionRuleType> DefaultSubmissionRule { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("DefaultComplianceRule", Order=21)]
        public virtual List<ComplianceRuleType> DefaultComplianceRule { get; set; }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(FileType));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether Description should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDescription()
    {
        return Description != null && Description.Count > 0;
    }
    
    /// <summary>
    /// Test whether Approval should be serialized
    /// </summary>
    public virtual bool ShouldSerializeApproval()
    {
        return Approval != null && Approval.Count > 0;
    }
    
    /// <summary>
    /// Test whether Language should be serialized
    /// </summary>
    public virtual bool ShouldSerializeLanguage()
    {
        return Language != null && Language.Count > 0;
    }
    
    /// <summary>
    /// Test whether DefaultSubmissionRule should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDefaultSubmissionRule()
    {
        return DefaultSubmissionRule != null && DefaultSubmissionRule.Count > 0;
    }
    
    /// <summary>
    /// Test whether DefaultComplianceRule should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDefaultComplianceRule()
    {
        return DefaultComplianceRule != null && DefaultComplianceRule.Count > 0;
    }
    
    /// <summary>
    /// Test whether FileID should be serialized
    /// </summary>
    public virtual bool ShouldSerializeFileID()
    {
        return (FileID != null);
    }
    
    /// <summary>
    /// Test whether FileURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeFileURI()
    {
        return (FileURI != null);
    }
    
    /// <summary>
    /// Test whether DisplayName should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDisplayName()
    {
        return (DisplayName != null);
    }
    
    /// <summary>
    /// Test whether Title should be serialized
    /// </summary>
    public virtual bool ShouldSerializeTitle()
    {
        return (Title != null);
    }
    
    /// <summary>
    /// Test whether Version should be serialized
    /// </summary>
    public virtual bool ShouldSerializeVersion()
    {
        return (Version != null);
    }
    
    /// <summary>
    /// Test whether FileName should be serialized
    /// </summary>
    public virtual bool ShouldSerializeFileName()
    {
        return (FileName != null);
    }
    
    /// <summary>
    /// Test whether FileExtension should be serialized
    /// </summary>
    public virtual bool ShouldSerializeFileExtension()
    {
        return (FileExtension != null);
    }
    
    /// <summary>
    /// Test whether FileClass should be serialized
    /// </summary>
    public virtual bool ShouldSerializeFileClass()
    {
        return (FileClass != null);
    }
    
    /// <summary>
    /// Test whether FileSizeKB should be serialized
    /// </summary>
    public virtual bool ShouldSerializeFileSizeKB()
    {
        return (FileSizeKB != null);
    }
    
    /// <summary>
    /// Test whether Copyright should be serialized
    /// </summary>
    public virtual bool ShouldSerializeCopyright()
    {
        return (Copyright != null);
    }
    
    /// <summary>
    /// Test whether TermsofUse should be serialized
    /// </summary>
    public virtual bool ShouldSerializeTermsofUse()
    {
        return (TermsofUse != null);
    }
    
    /// <summary>
    /// Test whether Usage should be serialized
    /// </summary>
    public virtual bool ShouldSerializeUsage()
    {
        return (Usage != null);
    }
    
    /// <summary>
    /// Test whether Dates should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDates()
    {
        return (Dates != null);
    }
    
    /// <summary>
    /// Test whether Contacts should be serialized
    /// </summary>
    public virtual bool ShouldSerializeContacts()
    {
        return (Contacts != null);
    }
    
    /// <summary>
    /// Test whether FileHash should be serialized
    /// </summary>
    public virtual bool ShouldSerializeFileHash()
    {
        return (FileHash != null);
    }
    
    /// <summary>
    /// Test whether AssociatedFiles should be serialized
    /// </summary>
    public virtual bool ShouldSerializeAssociatedFiles()
    {
        return (AssociatedFiles != null);
    }
    
    /// <summary>
    /// Test whether Provenance should be serialized
    /// </summary>
    public virtual bool ShouldSerializeProvenance()
    {
        return (Provenance != null);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current FileType object into an XML string
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
    /// Deserializes workflow markup into an FileType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output FileType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out FileType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(FileType);
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
    
    public static bool Deserialize(string input, out FileType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static FileType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((FileType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static FileType Deserialize(System.IO.Stream s)
    {
        return ((FileType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current FileType object into file
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
    /// Deserializes xml markup from file into an FileType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output FileType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out FileType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(FileType);
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
    
    public static bool LoadFromFile(string fileName, out FileType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out FileType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static FileType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static FileType LoadFromFile(string fileName, System.Text.Encoding encoding)
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

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
/// Contains information about a registered package, including a description of the package contents and purpose (PackageDescription), information about the registry that contains the package XML (RegistryData), and information about the package file characteristics (TemplateFile).
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
[System.Xml.Serialization.XmlRootAttribute("TemplateAdmin", Namespace="urn:ihe:qrph:sdc:2016", IsNullable=false)]
public partial class TemplateAdminType : ExtensionBaseType
{
    
    private static XmlSerializer serializer;
    
    /// <summary>
    /// Description of the XML package contents and the purpose for the contained XML templates.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("PackageDescription", Order=0)]
        public virtual List<RichTextType> PackageDescription { get; set; }
    /// <summary>
    /// Information about the registry that contains the XML template, and registration status of the XML template. (The XML template may contain a package of sub-templates.  In this case, the the RegistryData refers primarily to the package, not the sub-templates.)
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public virtual RegistryType RegistryData { get; set; }
    /// <summary>
    /// Information about the  XML template's file characteristics (The XML template may contain a package of sub-templates.  In this case, the the RegistryData refers primarily to the entire package, not the sub-templates.)
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public virtual FileType TemplateFile { get; set; }
    /// <summary>
    /// NEW
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("DigitalSignature", Order=3)]
        public virtual List<TemplateAdminTypeDigitalSignature> DigitalSignature { get; set; }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(TemplateAdminType));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether PackageDescription should be serialized
    /// </summary>
    public virtual bool ShouldSerializePackageDescription()
    {
        return PackageDescription != null && PackageDescription.Count > 0;
    }
    
    /// <summary>
    /// Test whether DigitalSignature should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDigitalSignature()
    {
        return DigitalSignature != null && DigitalSignature.Count > 0;
    }
    
    /// <summary>
    /// Test whether RegistryData should be serialized
    /// </summary>
    public virtual bool ShouldSerializeRegistryData()
    {
        return (RegistryData != null);
    }
    
    /// <summary>
    /// Test whether TemplateFile should be serialized
    /// </summary>
    public virtual bool ShouldSerializeTemplateFile()
    {
        return (TemplateFile != null);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current TemplateAdminType object into an XML string
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
    /// Deserializes workflow markup into an TemplateAdminType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output TemplateAdminType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out TemplateAdminType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(TemplateAdminType);
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
    
    public static bool Deserialize(string input, out TemplateAdminType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static TemplateAdminType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((TemplateAdminType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static TemplateAdminType Deserialize(System.IO.Stream s)
    {
        return ((TemplateAdminType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current TemplateAdminType object into file
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
    /// Deserializes xml markup from file into an TemplateAdminType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output TemplateAdminType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out TemplateAdminType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(TemplateAdminType);
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
    
    public static bool LoadFromFile(string fileName, out TemplateAdminType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out TemplateAdminType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static TemplateAdminType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static TemplateAdminType LoadFromFile(string fileName, System.Text.Encoding encoding)
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

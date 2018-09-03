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
/// A model structure for a Person object.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class PersonType : ExtensionBaseType
{
    
    private static XmlSerializer serializer;
    
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public virtual NameType PersonName { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("AliasName", Order=1)]
        public virtual List<NameType> AliasName { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("Job", Order=2)]
        public virtual List<JobType> Job { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("StreetAddress", Order=3)]
        public virtual List<AddressType> StreetAddress { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("Email", Order=4)]
        public virtual List<EmailType> Email { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("Phone", Order=5)]
        public virtual List<PhoneType> Phone { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("WebURL", Order=6)]
        public virtual List<anyURI_Stype> WebURL { get; set; }
    /// <summary>
    /// Role of the person, e.g., creator, copyright holder, accreditor, certifier, curator, etc.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public virtual string_Stype Role { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("Identifier", Order=8)]
        public virtual List<IdentifierType> Identifier { get; set; }
    /// <summary>
    /// When this person should be contacted.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public virtual string_Stype Usage { get; set; }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(PersonType));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether AliasName should be serialized
    /// </summary>
    public virtual bool ShouldSerializeAliasName()
    {
        return AliasName != null && AliasName.Count > 0;
    }
    
    /// <summary>
    /// Test whether Job should be serialized
    /// </summary>
    public virtual bool ShouldSerializeJob()
    {
        return Job != null && Job.Count > 0;
    }
    
    /// <summary>
    /// Test whether StreetAddress should be serialized
    /// </summary>
    public virtual bool ShouldSerializeStreetAddress()
    {
        return StreetAddress != null && StreetAddress.Count > 0;
    }
    
    /// <summary>
    /// Test whether Email should be serialized
    /// </summary>
    public virtual bool ShouldSerializeEmail()
    {
        return Email != null && Email.Count > 0;
    }
    
    /// <summary>
    /// Test whether Phone should be serialized
    /// </summary>
    public virtual bool ShouldSerializePhone()
    {
        return Phone != null && Phone.Count > 0;
    }
    
    /// <summary>
    /// Test whether WebURL should be serialized
    /// </summary>
    public virtual bool ShouldSerializeWebURL()
    {
        return WebURL != null && WebURL.Count > 0;
    }
    
    /// <summary>
    /// Test whether Identifier should be serialized
    /// </summary>
    public virtual bool ShouldSerializeIdentifier()
    {
        return Identifier != null && Identifier.Count > 0;
    }
    
    /// <summary>
    /// Test whether PersonName should be serialized
    /// </summary>
    public virtual bool ShouldSerializePersonName()
    {
        return (PersonName != null);
    }
    
    /// <summary>
    /// Test whether Role should be serialized
    /// </summary>
    public virtual bool ShouldSerializeRole()
    {
        return (Role != null);
    }
    
    /// <summary>
    /// Test whether Usage should be serialized
    /// </summary>
    public virtual bool ShouldSerializeUsage()
    {
        return (Usage != null);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current PersonType object into an XML string
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
    /// Deserializes workflow markup into an PersonType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output PersonType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out PersonType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(PersonType);
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
    
    public static bool Deserialize(string input, out PersonType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static PersonType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((PersonType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static PersonType Deserialize(System.IO.Stream s)
    {
        return ((PersonType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current PersonType object into file
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
    /// Deserializes xml markup from file into an PersonType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output PersonType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out PersonType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(PersonType);
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
    
    public static bool LoadFromFile(string fileName, out PersonType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out PersonType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static PersonType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static PersonType LoadFromFile(string fileName, System.Text.Encoding encoding)
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

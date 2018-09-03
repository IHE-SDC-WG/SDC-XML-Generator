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

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class nonPositiveInteger_DEtype : nonPositiveInteger_Stype
{
    
    private bool _shouldSerializeallowAPPROX;
    
    private bool _shouldSerializeallowLTE;
    
    private bool _shouldSerializeallowLT;
    
    private bool _shouldSerializeallowGTE;
    
    private bool _shouldSerializeallowGT;
    
    private static XmlSerializer serializer;
    
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonPositiveInteger")]
        public virtual string minInclusive { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonPositiveInteger")]
        public virtual string maxInclusive { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonPositiveInteger")]
        public virtual string minExclusive { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonPositiveInteger")]
        public virtual string maxExclusive { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual byte totalDigits { get; set; }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool totalDigitsSpecified { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string mask { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public virtual bool allowGT { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public virtual bool allowGTE { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public virtual bool allowLT { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public virtual bool allowLTE { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public virtual bool allowAPPROX { get; set; }
    
    ///// <summary>
    ///// nonPositiveInteger_DEtype class constructor
    ///// </summary>
    //public nonPositiveInteger_DEtype()
    //{
    //    this.allowGT = false;
    //    this.allowGTE = false;
    //    this.allowLT = false;
    //    this.allowLTE = false;
    //    this.allowAPPROX = false;
    //}
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(nonPositiveInteger_DEtype));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether allowGT should be serialized
    /// </summary>
    public virtual bool ShouldSerializeallowGT()
    {
        if (_shouldSerializeallowGT)
        {
            return true;
        }
        return (allowGT != default(bool));
    }
    
    /// <summary>
    /// Test whether allowGTE should be serialized
    /// </summary>
    public virtual bool ShouldSerializeallowGTE()
    {
        if (_shouldSerializeallowGTE)
        {
            return true;
        }
        return (allowGTE != default(bool));
    }
    
    /// <summary>
    /// Test whether allowLT should be serialized
    /// </summary>
    public virtual bool ShouldSerializeallowLT()
    {
        if (_shouldSerializeallowLT)
        {
            return true;
        }
        return (allowLT != default(bool));
    }
    
    /// <summary>
    /// Test whether allowLTE should be serialized
    /// </summary>
    public virtual bool ShouldSerializeallowLTE()
    {
        if (_shouldSerializeallowLTE)
        {
            return true;
        }
        return (allowLTE != default(bool));
    }
    
    /// <summary>
    /// Test whether allowAPPROX should be serialized
    /// </summary>
    public virtual bool ShouldSerializeallowAPPROX()
    {
        if (_shouldSerializeallowAPPROX)
        {
            return true;
        }
        return (allowAPPROX != default(bool));
    }
    
    /// <summary>
    /// Test whether minInclusive should be serialized
    /// </summary>
    public virtual bool ShouldSerializeminInclusive()
    {
        return !string.IsNullOrEmpty(minInclusive);
    }
    
    /// <summary>
    /// Test whether maxInclusive should be serialized
    /// </summary>
    public virtual bool ShouldSerializemaxInclusive()
    {
        return !string.IsNullOrEmpty(maxInclusive);
    }
    
    /// <summary>
    /// Test whether minExclusive should be serialized
    /// </summary>
    public virtual bool ShouldSerializeminExclusive()
    {
        return !string.IsNullOrEmpty(minExclusive);
    }
    
    /// <summary>
    /// Test whether maxExclusive should be serialized
    /// </summary>
    public virtual bool ShouldSerializemaxExclusive()
    {
        return !string.IsNullOrEmpty(maxExclusive);
    }
    
    /// <summary>
    /// Test whether mask should be serialized
    /// </summary>
    public virtual bool ShouldSerializemask()
    {
        return !string.IsNullOrEmpty(mask);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current nonPositiveInteger_DEtype object into an XML string
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
    /// Deserializes workflow markup into an nonPositiveInteger_DEtype object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output nonPositiveInteger_DEtype object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out nonPositiveInteger_DEtype obj, out System.Exception exception)
    {
        exception = null;
        obj = default(nonPositiveInteger_DEtype);
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
    
    public static bool Deserialize(string input, out nonPositiveInteger_DEtype obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static nonPositiveInteger_DEtype Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((nonPositiveInteger_DEtype)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static nonPositiveInteger_DEtype Deserialize(System.IO.Stream s)
    {
        return ((nonPositiveInteger_DEtype)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current nonPositiveInteger_DEtype object into file
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
    /// Deserializes xml markup from file into an nonPositiveInteger_DEtype object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output nonPositiveInteger_DEtype object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out nonPositiveInteger_DEtype obj, out System.Exception exception)
    {
        exception = null;
        obj = default(nonPositiveInteger_DEtype);
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
    
    public static bool LoadFromFile(string fileName, out nonPositiveInteger_DEtype obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out nonPositiveInteger_DEtype obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static nonPositiveInteger_DEtype LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static nonPositiveInteger_DEtype LoadFromFile(string fileName, System.Text.Encoding encoding)
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

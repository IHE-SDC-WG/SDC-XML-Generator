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
/// Function or web service that returns a string value.
/// </summary>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LookupEndPointType))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class CallFuncType : ExtensionBaseType
{
    
    private bool _shouldSerializereturnList;
    
    private static XmlSerializer serializer;
    
    /// <summary>
    /// Name of the function or URI of the web service. The Function may use the parameter list and transmit the item name, property and value for each parameter.
    /// 
    /// The function must understand the parameters and return an appropriate response of the correct data type.  Guidelines for URI construction syntax will be defined external to this Schema, and may be use-case and implementation-specific.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public virtual anyURI_Stype Function { get; set; }
    /// <summary>
    /// Information about securly accessing the web service.  More detailed service patterns may be required.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public virtual RichTextType Security { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("ParameterRef", typeof(ParameterItemType), Order=2)]
        [System.Xml.Serialization.XmlElementAttribute("ParameterValue", typeof(ParameterValueType), Order=2)]
        public virtual List<ExtensionBaseType> Items { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public virtual List<string> dataTypeListAll { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public virtual bool returnList { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string objectTypeName { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string objectFormat { get; set; }
    
    ///// <summary>
    ///// CallFuncType class constructor
    ///// </summary>
    //public CallFuncType()
    //{
    //    this.returnList = false;
    //}
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(CallFuncType));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether Items should be serialized
    /// </summary>
    public virtual bool ShouldSerializeItems()
    {
        return Items != null && Items.Count > 0;
    }
    
    /// <summary>
    /// Test whether dataTypeListAll should be serialized
    /// </summary>
    public virtual bool ShouldSerializedataTypeListAll()
    {
        return dataTypeListAll != null && dataTypeListAll.Count > 0;
    }
    
    /// <summary>
    /// Test whether returnList should be serialized
    /// </summary>
    public virtual bool ShouldSerializereturnList()
    {
        if (_shouldSerializereturnList)
        {
            return true;
        }
        return (returnList != default(bool));
    }
    
    /// <summary>
    /// Test whether Function should be serialized
    /// </summary>
    public virtual bool ShouldSerializeFunction()
    {
        return (Function != null);
    }
    
    /// <summary>
    /// Test whether Security should be serialized
    /// </summary>
    public virtual bool ShouldSerializeSecurity()
    {
        return (Security != null);
    }
    
    /// <summary>
    /// Test whether objectTypeName should be serialized
    /// </summary>
    public virtual bool ShouldSerializeobjectTypeName()
    {
        return !string.IsNullOrEmpty(objectTypeName);
    }
    
    /// <summary>
    /// Test whether objectFormat should be serialized
    /// </summary>
    public virtual bool ShouldSerializeobjectFormat()
    {
        return !string.IsNullOrEmpty(objectFormat);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current CallFuncType object into an XML string
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
    /// Deserializes workflow markup into an CallFuncType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output CallFuncType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out CallFuncType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(CallFuncType);
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
    
    public static bool Deserialize(string input, out CallFuncType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static CallFuncType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((CallFuncType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static CallFuncType Deserialize(System.IO.Stream s)
    {
        return ((CallFuncType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current CallFuncType object into file
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
    /// Deserializes xml markup from file into an CallFuncType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output CallFuncType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out CallFuncType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(CallFuncType);
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
    
    public static bool LoadFromFile(string fileName, out CallFuncType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out CallFuncType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static CallFuncType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static CallFuncType LoadFromFile(string fileName, System.Text.Encoding encoding)
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
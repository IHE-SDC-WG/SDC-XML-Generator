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
/// Programming code or pseudocode that describes a calculation.  THe code returns a value of the data type required by the parent Response field.  To assist with enabling the code in the form, the referenced form items and properties should be referenced by @name under the parameters elemeent.  It is possible to add mulitple calculation expressions to produce equivalent results using different programming languages and URIs.  The @ type attribute may be used to distinguish between them.  An Extension may be used instead of or along with an Expression and Parameters list.  Expressions may populate Responses that are set to @readOnly = "true" to ensure that all responses are calculated and not latered by the user.  Alternatively, the user may change a value created by (or instead of) the Expression.
/// </summary>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActSetAttrValueType))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class ScriptCodeAnyType : ExtensionBaseType
{
    
    private bool _shouldSerializereturnList;
    
    private static XmlSerializer serializer;
    
    /// <summary>
    /// Parameters are named FormDesign items which have property values that need to be supplied to a scripted function or a web service URI.
    /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Parameter", Order=0)]
        public virtual List<ParameterItemType> Parameter { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public virtual List<string> dataTypeListAll { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public virtual bool returnList { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string objectTypeName { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string objectFormat { get; set; }
    /// <summary>
    /// Programming language.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string language { get; set; }
    /// <summary>
    /// Script contents.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string code { get; set; }
    
    /// <summary>
    /// ScriptCodeAnyType class constructor
    /// </summary>
    public ScriptCodeAnyType()
    {
        this.returnList = false;
    }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(ScriptCodeAnyType));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether Parameter should be serialized
    /// </summary>
    public virtual bool ShouldSerializeParameter()
    {
        return Parameter != null && Parameter.Count > 0;
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
    
    /// <summary>
    /// Test whether language should be serialized
    /// </summary>
    public virtual bool ShouldSerializelanguage()
    {
        return !string.IsNullOrEmpty(language);
    }
    
    /// <summary>
    /// Test whether code should be serialized
    /// </summary>
    public virtual bool ShouldSerializecode()
    {
        return !string.IsNullOrEmpty(code);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current ScriptCodeAnyType object into an XML string
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
    /// Deserializes workflow markup into an ScriptCodeAnyType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output ScriptCodeAnyType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out ScriptCodeAnyType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(ScriptCodeAnyType);
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
    
    public static bool Deserialize(string input, out ScriptCodeAnyType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static ScriptCodeAnyType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((ScriptCodeAnyType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static ScriptCodeAnyType Deserialize(System.IO.Stream s)
    {
        return ((ScriptCodeAnyType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current ScriptCodeAnyType object into file
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
    /// Deserializes xml markup from file into an ScriptCodeAnyType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output ScriptCodeAnyType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ScriptCodeAnyType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(ScriptCodeAnyType);
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
    
    public static bool LoadFromFile(string fileName, out ScriptCodeAnyType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out ScriptCodeAnyType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static ScriptCodeAnyType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static ScriptCodeAnyType LoadFromFile(string fileName, System.Text.Encoding encoding)
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
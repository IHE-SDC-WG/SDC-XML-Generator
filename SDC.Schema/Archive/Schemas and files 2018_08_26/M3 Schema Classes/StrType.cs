// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 4.4.0.7
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
namespace SDC
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
/// List of string values or references to string values inside a string list.  Allows constraints on the values in the list. Each value in the list must be validated per the constraints in the list attributes.
/// 
/// Precede named references with "$" to distinguish them from literal strings.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public abstract partial class StrType : ExtensionBaseType
{
    
    #region Private fields
    private bool _shouldSerializemaxLength;
    
    private bool _shouldSerializeminLength;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _val;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private System.Nullable<DataTypeString_StypeEnum> _dataTypeString;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private System.Nullable<long> _minLength;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private System.Nullable<long> _maxLength;
    
    private static XmlSerializer serializer;
    #endregion
    
    /// <summary>
    /// StrType class constructor
    /// </summary>
    public StrType()
    {
        this._dataTypeString = DataTypeString_StypeEnum.@string;
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string val
    {
        get
        {
            return this._val;
        }
        set
        {
            this._val = value;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
    public virtual DataTypeString_StypeEnum dataTypeString
    {
        get
        {
            if (this._dataTypeString.HasValue)
            {
                return this._dataTypeString.Value;
            }
            else
            {
                return default(DataTypeString_StypeEnum);
            }
        }
        set
        {
            this._dataTypeString = value;
        }
    }
    
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual bool dataTypeStringSpecified
    {
        get
        {
            return this._dataTypeString.HasValue;
        }
        set
        {
            if (value==false)
            {
                this._dataTypeString = null;
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual long minLength
    {
        get
        {
            if (this._minLength.HasValue)
            {
                return this._minLength.Value;
            }
            else
            {
                return default(long);
            }
        }
        set
        {
            this._minLength = value;
            _shouldSerializeminLength = true;
        }
    }
    
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual bool minLengthSpecified
    {
        get
        {
            return this._minLength.HasValue;
        }
        set
        {
            if (value==false)
            {
                this._minLength = null;
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual long maxLength
    {
        get
        {
            if (this._maxLength.HasValue)
            {
                return this._maxLength.Value;
            }
            else
            {
                return default(long);
            }
        }
        set
        {
            this._maxLength = value;
            _shouldSerializemaxLength = true;
        }
    }
    
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual bool maxLengthSpecified
    {
        get
        {
            return this._maxLength.HasValue;
        }
        set
        {
            if (value==false)
            {
                this._maxLength = null;
            }
        }
    }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(StrType));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether minLength should be serialized
    /// </summary>
    public virtual bool ShouldSerializeminLength()
    {
        if (_shouldSerializeminLength)
        {
            return true;
        }
        return (_minLength != default(long));
    }
    
    /// <summary>
    /// Test whether maxLength should be serialized
    /// </summary>
    public virtual bool ShouldSerializemaxLength()
    {
        if (_shouldSerializemaxLength)
        {
            return true;
        }
        return (_maxLength != default(long));
    }
    
    /// <summary>
    /// Test whether val should be serialized
    /// </summary>
    public virtual bool ShouldSerializeval()
    {
        return !string.IsNullOrEmpty(val);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current StrType object into an XML string
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
    /// Deserializes workflow markup into an StrType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output StrType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out StrType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(StrType);
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
    
    public static bool Deserialize(string input, out StrType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static StrType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((StrType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static StrType Deserialize(System.IO.Stream s)
    {
        return ((StrType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current StrType object into file
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
    /// Deserializes xml markup from file into an StrType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output StrType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out StrType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(StrType);
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
    
    public static bool LoadFromFile(string fileName, out StrType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out StrType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static StrType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static StrType LoadFromFile(string fileName, System.Text.Encoding encoding)
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

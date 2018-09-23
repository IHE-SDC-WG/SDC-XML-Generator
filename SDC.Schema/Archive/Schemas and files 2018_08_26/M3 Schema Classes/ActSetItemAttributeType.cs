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
/// This type is used to act upon the value of common item attributes.  If an "act" attribute (a type with the "act" prefix) has no value assigned, it is ignored. If it has a value, then that attribute on the target item(s) assume(s) that stated value when an attached Boolean condition evaluates to true.  The attached condition may be an "If" statement or any expression that evaluates to a Boolean value.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class ActSetItemAttributeType : ExtensionBaseType
{
    
    #region Private fields
    private bool _shouldSerializeactReadOnly;
    
    private bool _shouldSerializeactDeleteResponse;
    
    private bool _shouldSerializeactSelect;
    
    private bool _shouldSerializeactActivate;
    
    private bool _shouldSerializeactEnable;
    
    private bool _shouldSerializeactVisible;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _targetNames;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private System.Nullable<bool> _actVisible;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private System.Nullable<bool> _actEnable;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _actMinCard;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _actMaxCard;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private System.Nullable<bool> _actActivate;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private System.Nullable<bool> _actSelect;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private System.Nullable<bool> _actDeleteResponse;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private System.Nullable<bool> _actReadOnly;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _actType;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _actStyleClass;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _actSetTitleText;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private byte[] _actSetBase64HTML;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _actSetCode;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _actSetCodeSystem;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _actSetVal;
    
    private static XmlSerializer serializer;
    #endregion
    
    /// <summary>
    /// The names of the items affected by property actions
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
    public virtual string targetNames
    {
        get
        {
            return this._targetNames;
        }
        set
        {
            this._targetNames = value;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actVisible
    {
        get
        {
            if (this._actVisible.HasValue)
            {
                return this._actVisible.Value;
            }
            else
            {
                return default(bool);
            }
        }
        set
        {
            this._actVisible = value;
            _shouldSerializeactVisible = true;
        }
    }
    
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual bool actVisibleSpecified
    {
        get
        {
            return this._actVisible.HasValue;
        }
        set
        {
            if (value==false)
            {
                this._actVisible = null;
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actEnable
    {
        get
        {
            if (this._actEnable.HasValue)
            {
                return this._actEnable.Value;
            }
            else
            {
                return default(bool);
            }
        }
        set
        {
            this._actEnable = value;
            _shouldSerializeactEnable = true;
        }
    }
    
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual bool actEnableSpecified
    {
        get
        {
            return this._actEnable.HasValue;
        }
        set
        {
            if (value==false)
            {
                this._actEnable = null;
            }
        }
    }
    
    /// <summary>
    /// Controls requirement to answer the question and the minimum number of repeats.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
    public virtual string actMinCard
    {
        get
        {
            return this._actMinCard;
        }
        set
        {
            this._actMinCard = value;
        }
    }
    
    /// <summary>
    /// Controls requirement to answer the question
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
    public virtual string actMaxCard
    {
        get
        {
            return this._actMaxCard;
        }
        set
        {
            this._actMaxCard = value;
        }
    }
    
    /// <summary>
    /// Toggle visible and enabled together.  Setting this to false will de-activate all descendents but will not change their enabled or visible properties.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actActivate
    {
        get
        {
            if (this._actActivate.HasValue)
            {
                return this._actActivate.Value;
            }
            else
            {
                return default(bool);
            }
        }
        set
        {
            this._actActivate = value;
            _shouldSerializeactActivate = true;
        }
    }
    
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual bool actActivateSpecified
    {
        get
        {
            return this._actActivate.HasValue;
        }
        set
        {
            if (value==false)
            {
                this._actActivate = null;
            }
        }
    }
    
    /// <summary>
    /// Toggle selection of a List Item; not applicable to other items.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actSelect
    {
        get
        {
            if (this._actSelect.HasValue)
            {
                return this._actSelect.Value;
            }
            else
            {
                return default(bool);
            }
        }
        set
        {
            this._actSelect = value;
            _shouldSerializeactSelect = true;
        }
    }
    
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual bool actSelectSpecified
    {
        get
        {
            return this._actSelect.HasValue;
        }
        set
        {
            if (value==false)
            {
                this._actSelect = null;
            }
        }
    }
    
    /// <summary>
    /// Delete any response in a Response field on a question or ListItem.  Not applicable to other item types.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actDeleteResponse
    {
        get
        {
            if (this._actDeleteResponse.HasValue)
            {
                return this._actDeleteResponse.Value;
            }
            else
            {
                return default(bool);
            }
        }
        set
        {
            this._actDeleteResponse = value;
            _shouldSerializeactDeleteResponse = true;
        }
    }
    
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual bool actDeleteResponseSpecified
    {
        get
        {
            return this._actDeleteResponse.HasValue;
        }
        set
        {
            if (value==false)
            {
                this._actDeleteResponse = null;
            }
        }
    }
    
    /// <summary>
    /// Delete any response in a Response field on a question or ListItem.  Not applicable to other item types.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actReadOnly
    {
        get
        {
            if (this._actReadOnly.HasValue)
            {
                return this._actReadOnly.Value;
            }
            else
            {
                return default(bool);
            }
        }
        set
        {
            this._actReadOnly = value;
            _shouldSerializeactReadOnly = true;
        }
    }
    
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual bool actReadOnlySpecified
    {
        get
        {
            return this._actReadOnly.HasValue;
        }
        set
        {
            if (value==false)
            {
                this._actReadOnly = null;
            }
        }
    }
    
    /// <summary>
    /// Set the @type attribute value
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
    public virtual string actType
    {
        get
        {
            return this._actType;
        }
        set
        {
            this._actType = value;
        }
    }
    
    /// <summary>
    /// Set the @styleClass attribute value
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
    public virtual string actStyleClass
    {
        get
        {
            return this._actStyleClass;
        }
        set
        {
            this._actStyleClass = value;
        }
    }
    
    /// <summary>
    /// Set the @title text on an item.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetTitleText
    {
        get
        {
            return this._actSetTitleText;
        }
        set
        {
            this._actSetTitleText = value;
        }
    }
    
    /// <summary>
    /// Set HTML as base-64-encoded binary
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="base64Binary")]
    public virtual byte[] actSetBase64HTML
    {
        get
        {
            return this._actSetBase64HTML;
        }
        set
        {
            this._actSetBase64HTML = value;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetCode
    {
        get
        {
            return this._actSetCode;
        }
        set
        {
            this._actSetCode = value;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetCodeSystem
    {
        get
        {
            return this._actSetCodeSystem;
        }
        set
        {
            this._actSetCodeSystem = value;
        }
    }
    
    /// <summary>
    /// Set a @val attribute.  The correct data type must be used if applicable.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetVal
    {
        get
        {
            return this._actSetVal;
        }
        set
        {
            this._actSetVal = value;
        }
    }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(ActSetItemAttributeType));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether actVisible should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactVisible()
    {
        if (_shouldSerializeactVisible)
        {
            return true;
        }
        return (_actVisible != default(bool));
    }
    
    /// <summary>
    /// Test whether actEnable should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactEnable()
    {
        if (_shouldSerializeactEnable)
        {
            return true;
        }
        return (_actEnable != default(bool));
    }
    
    /// <summary>
    /// Test whether actActivate should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactActivate()
    {
        if (_shouldSerializeactActivate)
        {
            return true;
        }
        return (_actActivate != default(bool));
    }
    
    /// <summary>
    /// Test whether actSelect should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSelect()
    {
        if (_shouldSerializeactSelect)
        {
            return true;
        }
        return (_actSelect != default(bool));
    }
    
    /// <summary>
    /// Test whether actDeleteResponse should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactDeleteResponse()
    {
        if (_shouldSerializeactDeleteResponse)
        {
            return true;
        }
        return (_actDeleteResponse != default(bool));
    }
    
    /// <summary>
    /// Test whether actReadOnly should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactReadOnly()
    {
        if (_shouldSerializeactReadOnly)
        {
            return true;
        }
        return (_actReadOnly != default(bool));
    }
    
    /// <summary>
    /// Test whether targetNames should be serialized
    /// </summary>
    public virtual bool ShouldSerializetargetNames()
    {
        return !string.IsNullOrEmpty(targetNames);
    }
    
    /// <summary>
    /// Test whether actMinCard should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactMinCard()
    {
        return !string.IsNullOrEmpty(actMinCard);
    }
    
    /// <summary>
    /// Test whether actMaxCard should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactMaxCard()
    {
        return !string.IsNullOrEmpty(actMaxCard);
    }
    
    /// <summary>
    /// Test whether actType should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactType()
    {
        return !string.IsNullOrEmpty(actType);
    }
    
    /// <summary>
    /// Test whether actStyleClass should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactStyleClass()
    {
        return !string.IsNullOrEmpty(actStyleClass);
    }
    
    /// <summary>
    /// Test whether actSetTitleText should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetTitleText()
    {
        return !string.IsNullOrEmpty(actSetTitleText);
    }
    
    /// <summary>
    /// Test whether actSetCode should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetCode()
    {
        return !string.IsNullOrEmpty(actSetCode);
    }
    
    /// <summary>
    /// Test whether actSetCodeSystem should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetCodeSystem()
    {
        return !string.IsNullOrEmpty(actSetCodeSystem);
    }
    
    /// <summary>
    /// Test whether actSetVal should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetVal()
    {
        return !string.IsNullOrEmpty(actSetVal);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current ActSetItemAttributeType object into an XML string
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
    /// Deserializes workflow markup into an ActSetItemAttributeType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output ActSetItemAttributeType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out ActSetItemAttributeType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(ActSetItemAttributeType);
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
    
    public static bool Deserialize(string input, out ActSetItemAttributeType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static ActSetItemAttributeType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((ActSetItemAttributeType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static ActSetItemAttributeType Deserialize(System.IO.Stream s)
    {
        return ((ActSetItemAttributeType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current ActSetItemAttributeType object into file
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
    /// Deserializes xml markup from file into an ActSetItemAttributeType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output ActSetItemAttributeType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ActSetItemAttributeType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(ActSetItemAttributeType);
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
    
    public static bool LoadFromFile(string fileName, out ActSetItemAttributeType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out ActSetItemAttributeType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static ActSetItemAttributeType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static ActSetItemAttributeType LoadFromFile(string fileName, System.Text.Encoding encoding)
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
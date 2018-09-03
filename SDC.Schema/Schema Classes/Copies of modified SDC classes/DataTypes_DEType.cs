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
/// MOVED from SDCDataTypes:
/// SDC datatypes for Data Entry (DE), based mostly on W3C specifications. Uses baseAttributes  and Extension capability to enhance the list of Data Types. Includes additonal metadata to specify data input restrictions for data entry forms, and to aid in validation of IHE RFD SubmitForm responses in XML instance documents.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class DataTypes_DEType : ExtensionBaseType
{
    
    private bool _shouldSerializeItemElementName;
    
    private static XmlSerializer serializer;
    
        [System.Xml.Serialization.XmlElementAttribute("HTML", typeof(HTML_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("XML", typeof(XML_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("anyType", typeof(anyType_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("anyURI", typeof(anyURI_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("base64Binary", typeof(base64Binary_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("boolean", typeof(boolean_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("byte", typeof(byte_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("date", typeof(date_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("dateTime", typeof(dateTimeStamp_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("dateTimeStamp", typeof(dateTimeStamp_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("decimal", typeof(decimal_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("double", typeof(double_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("duration", typeof(duration_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("float", typeof(float_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("gDay", typeof(gDay_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("gMonth", typeof(gMonth_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("gMonthDay", typeof(gMonthDay_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("gYear", typeof(gYear_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("gYearMonth", typeof(gMonth_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("hexBinary", typeof(hexBinary_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("int", typeof(int_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("integer", typeof(integer_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("long", typeof(long_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("negativeInteger", typeof(negativeInteger_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("nonNegativeInteger", typeof(nonNegativeInteger_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("nonPositiveInteger", typeof(nonPositiveInteger_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("positiveInteger", typeof(positiveInteger_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("short", typeof(short_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("string", typeof(string_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("time", typeof(time_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("unsignedByte", typeof(unsignedByte_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("unsignedInt", typeof(unsignedInt_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("unsignedLong", typeof(unsignedLong_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("unsignedShort", typeof(unsignedShort_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("yearMonthDuration", typeof(yearMonthDuration_DEtype), IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public virtual BaseType Item { get; set; }
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual ItemChoiceType1 ItemElementName { get; set; }
        //public virtual ItemChoiceType ItemElementName { get; set; }

        private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(DataTypes_DEType));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether ItemElementName should be serialized
    /// </summary>
    public virtual bool ShouldSerializeItemElementName()
    {
        if (_shouldSerializeItemElementName)
        {
            return true;
        }
            //return (ItemElementName != default(ItemChoiceType1));
            return (ItemElementName != default(ItemChoiceType1));

        }

        /// <summary>
        /// Test whether Item should be serialized
        /// </summary>
        public virtual bool ShouldSerializeItem()
    {
        return (Item != null);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current DataTypes_DEType object into an XML string
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
    /// Deserializes workflow markup into an DataTypes_DEType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output DataTypes_DEType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out DataTypes_DEType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(DataTypes_DEType);
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
    
    public static bool Deserialize(string input, out DataTypes_DEType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static DataTypes_DEType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((DataTypes_DEType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static DataTypes_DEType Deserialize(System.IO.Stream s)
    {
        return ((DataTypes_DEType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current DataTypes_DEType object into file
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
    /// Deserializes xml markup from file into an DataTypes_DEType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output DataTypes_DEType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out DataTypes_DEType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(DataTypes_DEType);
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
    
    public static bool LoadFromFile(string fileName, out DataTypes_DEType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out DataTypes_DEType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static DataTypes_DEType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static DataTypes_DEType LoadFromFile(string fileName, System.Text.Encoding encoding)
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

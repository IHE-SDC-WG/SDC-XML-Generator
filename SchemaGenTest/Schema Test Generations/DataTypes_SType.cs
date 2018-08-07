// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 4.2.0.15
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
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Xml;

/// <summary>
/// SDC datatypes in Simple (S) format, based mostly on W3C specifications.
/// Uses baseAttributes and Extension capability to enhance the list of Data Types.
/// **CHECK for ERRORS and completeness**
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1067.0")]
[Serializable]
[DebuggerStepThrough]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://healthIT.gov/sdc")]
[DataContractAttribute(Name="DataTypes_SType", Namespace="http://healthIT.gov/sdc")]
public partial class DataTypes_SType : ExtensionBaseType
{
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private BaseType _item;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private ItemChoiceType1 _itemElementName;
    
    private static XmlSerializer serializer;
    
    private ObjectChangeTracker changeTrackerField;
    
    [XmlElementAttribute("HTML", typeof(HTML_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("XML", typeof(XML_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("anyType", typeof(anyType_DEtype), IsNullable=true, Order=0)]
    [XmlElementAttribute("anyURI", typeof(anyURI_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("base64Binary", typeof(base64Binary_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("boolean", typeof(boolean_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("byte", typeof(byte_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("date", typeof(date_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("dateTime", typeof(dateTimeStamp_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("dateTimeStamp", typeof(dateTimeStamp_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("decimal", typeof(decimal_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("double", typeof(double_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("duration", typeof(duration_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("float", typeof(float_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("gDay", typeof(gDay_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("gMonth", typeof(gMonth_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("gMonthDay", typeof(gMonthDay_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("gYear", typeof(gYear_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("gYearMonth", typeof(gMonth_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("hexBinary", typeof(hexBinary_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("int", typeof(int_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("integer", typeof(integer_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("long", typeof(long_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("negativeInteger", typeof(negativeInteger_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("nonNegativeInteger", typeof(nonNegativeInteger_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("nonPositiveInteger", typeof(nonPositiveInteger_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("positiveInteger", typeof(positiveInteger_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("short", typeof(short_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("string", typeof(string_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("time", typeof(time_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("unsignedByte", typeof(unsignedByte_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("unsignedInt", typeof(unsignedInt_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("unsignedLong", typeof(unsignedLong_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("unsignedShort", typeof(unsignedShort_Stype), IsNullable=true, Order=0)]
    [XmlElementAttribute("yearMonthDuration", typeof(yearMonthDuration_Stype), IsNullable=true, Order=0)]
    [XmlChoiceIdentifierAttribute("ItemElementName")]
    [DataMemberAttribute(Name="HTML", Order=0)]
    public virtual BaseType Item
    {
        get
        {
            return _item;
        }
        set
        {
            if ((_item == value))
            {
                return;
            }
            if (((_item == null) 
                        || (_item.Equals(value) != true)))
            {
                _item = value;
                OnPropertyChanged("Item", value);
            }
        }
    }
    
    [XmlElementAttribute(Order=1)]
    [XmlIgnore]
    [DataMemberAttribute(Order=1)]
    public virtual ItemChoiceType1 ItemElementName
    {
        get
        {
            return _itemElementName;
        }
        set
        {
            if ((_itemElementName.Equals(value) != true))
            {
                _itemElementName = value;
                OnPropertyChanged("ItemElementName", value);
            }
        }
    }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(DataTypes_SType));
            }
            return serializer;
        }
    }
    
    [XmlIgnore()]
    public ObjectChangeTracker ChangeTracker
    {
        get
        {
            if ((changeTrackerField == null))
            {
                changeTrackerField = new ObjectChangeTracker(this);
            }
            return changeTrackerField;
        }
    }
    
    /// <summary>
    /// Test whether Item should be serialized
    /// </summary>
    public virtual bool ShouldSerializeItem()
    {
        return (_item != null);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current DataTypes_SType object into an XML string
    /// </summary>
    /// <returns>string XML value</returns>
    public virtual string Serialize()
    {
        StreamReader streamReader = null;
        MemoryStream memoryStream = null;
        try
        {
            memoryStream = new MemoryStream();
            System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
            Serializer.Serialize(xmlWriter, this);
            memoryStream.Seek(0, SeekOrigin.Begin);
            streamReader = new StreamReader(memoryStream);
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
    
    /// <summary>
    /// Deserializes workflow markup into an DataTypes_SType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output DataTypes_SType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out DataTypes_SType obj, out Exception exception)
    {
        exception = null;
        obj = default(DataTypes_SType);
        try
        {
            obj = Deserialize(input);
            return true;
        }
        catch (Exception ex)
        {
            exception = ex;
            return false;
        }
    }
    
    public static bool Deserialize(string input, out DataTypes_SType obj)
    {
        Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static DataTypes_SType Deserialize(string input)
    {
        StringReader stringReader = null;
        try
        {
            stringReader = new StringReader(input);
            return ((DataTypes_SType)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static DataTypes_SType Deserialize(Stream s)
    {
        return ((DataTypes_SType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current DataTypes_SType object into file
    /// </summary>
    /// <param name="fileName">full path of outupt xml file</param>
    /// <param name="exception">output Exception value if failed</param>
    /// <returns>true if can serialize and save into file; otherwise, false</returns>
    public virtual bool SaveToFile(string fileName, out Exception exception)
    {
        exception = null;
        try
        {
            SaveToFile(fileName);
            return true;
        }
        catch (Exception e)
        {
            exception = e;
            return false;
        }
    }
    
    public virtual void SaveToFile(string fileName)
    {
        StreamWriter streamWriter = null;
        try
        {
            string xmlString = Serialize();
            FileInfo xmlFile = new FileInfo(fileName);
            streamWriter = xmlFile.CreateText();
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
    /// Deserializes xml markup from file into an DataTypes_SType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output DataTypes_SType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, out DataTypes_SType obj, out Exception exception)
    {
        exception = null;
        obj = default(DataTypes_SType);
        try
        {
            obj = LoadFromFile(fileName);
            return true;
        }
        catch (Exception ex)
        {
            exception = ex;
            return false;
        }
    }
    
    public static bool LoadFromFile(string fileName, out DataTypes_SType obj)
    {
        Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public new static DataTypes_SType LoadFromFile(string fileName)
    {
        FileStream file = null;
        StreamReader sr = null;
        try
        {
            file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            sr = new StreamReader(file);
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
    
    #region Clone method
    /// <summary>
    /// Create a clone of this DataTypes_SType object
    /// </summary>
    public virtual DataTypes_SType Clone()
    {
        return ((DataTypes_SType)(MemberwiseClone()));
    }
    #endregion
}
}
#pragma warning restore
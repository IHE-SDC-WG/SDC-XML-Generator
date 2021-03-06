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
/// This type represents any type of coding, terminology, classification, keyword, or local value system that may be applied to any displayable item in a
/// FormDesign template.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1067.0")]
[Serializable]
[DebuggerStepThrough]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://healthIT.gov/sdc")]
[DataContractAttribute(Name="CodingType", Namespace="http://healthIT.gov/sdc")]
public partial class CodingType : ExtensionBaseType
{
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string_Stype _code;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private RichTextType _codeText;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private CodeMatchType _codeMatch;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private CodeSystemType _codeSystem;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private DataTypes_SType _dataType;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private UnitsType _units;
    
    private static XmlSerializer serializer;
    
    private ObjectChangeTracker changeTrackerField;
    
    /// <summary>
    /// A standard code, or a local value from a custom coding system, that can be used to consistently identify, or provide a
    /// standard value for, the coded item.
    /// </summary>
    [XmlElementAttribute(IsNullable=true, Order=0)]
    [DataMemberAttribute(Order=0)]
    public virtual string_Stype Code
    {
        get
        {
            return _code;
        }
        set
        {
            if ((_code == value))
            {
                return;
            }
            if (((_code == null) 
                        || (_code.Equals(value) != true)))
            {
                _code = value;
                OnPropertyChanged("Code", value);
            }
        }
    }
    
    /// <summary>
    /// The human readable text that accompanies the assigned code and represents the code's precise meaning (semantics) or
    /// usage.
    /// </summary>
    [XmlElementAttribute(Order=1)]
    [DataMemberAttribute(Order=1)]
    public virtual RichTextType CodeText
    {
        get
        {
            return _codeText;
        }
        set
        {
            if ((_codeText == value))
            {
                return;
            }
            if (((_codeText == null) 
                        || (_codeText.Equals(value) != true)))
            {
                _codeText = value;
                OnPropertyChanged("CodeText", value);
            }
        }
    }
    
    /// <summary>
    /// Degree of match between the mapped item and the assigned code - @codeMatchType holds an entry from an enumerated
    /// list of match types.
    /// </summary>
    [XmlElementAttribute(Order=2)]
    [DataMemberAttribute(Order=2)]
    public virtual CodeMatchType CodeMatch
    {
        get
        {
            return _codeMatch;
        }
        set
        {
            if ((_codeMatch == value))
            {
                return;
            }
            if (((_codeMatch == null) 
                        || (_codeMatch.Equals(value) != true)))
            {
                _codeMatch = value;
                OnPropertyChanged("CodeMatch", value);
            }
        }
    }
    
    [XmlElementAttribute(Order=3)]
    [DataMemberAttribute(Order=3)]
    public virtual CodeSystemType CodeSystem
    {
        get
        {
            return _codeSystem;
        }
        set
        {
            if ((_codeSystem == value))
            {
                return;
            }
            if (((_codeSystem == null) 
                        || (_codeSystem.Equals(value) != true)))
            {
                _codeSystem = value;
                OnPropertyChanged("CodeSystem", value);
            }
        }
    }
    
    /// <summary>
    /// Data type enumeration derived from W3C XML Schema. If
    /// the code is derived from a local value system (e.g., numbered answer
    /// choices such as clock positions, tumor grades, or clinical scoring
    /// systems), then the data type of the local value may be specified
    /// here. This may be important if the code value will need to me
    /// manipulated mathematically.
    /// </summary>
    [XmlElementAttribute(Order=4)]
    [DataMemberAttribute(Order=4)]
    public virtual DataTypes_SType DataType
    {
        get
        {
            return _dataType;
        }
        set
        {
            if ((_dataType == value))
            {
                return;
            }
            if (((_dataType == null) 
                        || (_dataType.Equals(value) != true)))
            {
                _dataType = value;
                OnPropertyChanged("DataType", value);
            }
        }
    }
    
    [XmlElementAttribute(Order=5)]
    [DataMemberAttribute(Order=5)]
    public virtual UnitsType Units
    {
        get
        {
            return _units;
        }
        set
        {
            if ((_units == value))
            {
                return;
            }
            if (((_units == null) 
                        || (_units.Equals(value) != true)))
            {
                _units = value;
                OnPropertyChanged("Units", value);
            }
        }
    }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(CodingType));
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
    /// Test whether Code should be serialized
    /// </summary>
    public virtual bool ShouldSerializeCode()
    {
        return (_code != null);
    }
    
    /// <summary>
    /// Test whether CodeText should be serialized
    /// </summary>
    public virtual bool ShouldSerializeCodeText()
    {
        return (_codeText != null);
    }
    
    /// <summary>
    /// Test whether CodeMatch should be serialized
    /// </summary>
    public virtual bool ShouldSerializeCodeMatch()
    {
        return (_codeMatch != null);
    }
    
    /// <summary>
    /// Test whether CodeSystem should be serialized
    /// </summary>
    public virtual bool ShouldSerializeCodeSystem()
    {
        return (_codeSystem != null);
    }
    
    /// <summary>
    /// Test whether DataType should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDataType()
    {
        return (_dataType != null);
    }
    
    /// <summary>
    /// Test whether Units should be serialized
    /// </summary>
    public virtual bool ShouldSerializeUnits()
    {
        return (_units != null);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current CodingType object into an XML string
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
    /// Deserializes workflow markup into an CodingType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output CodingType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out CodingType obj, out Exception exception)
    {
        exception = null;
        obj = default(CodingType);
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
    
    public static bool Deserialize(string input, out CodingType obj)
    {
        Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static CodingType Deserialize(string input)
    {
        StringReader stringReader = null;
        try
        {
            stringReader = new StringReader(input);
            return ((CodingType)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static CodingType Deserialize(Stream s)
    {
        return ((CodingType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current CodingType object into file
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
    /// Deserializes xml markup from file into an CodingType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output CodingType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, out CodingType obj, out Exception exception)
    {
        exception = null;
        obj = default(CodingType);
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
    
    public static bool LoadFromFile(string fileName, out CodingType obj)
    {
        Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public new static CodingType LoadFromFile(string fileName)
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
    /// Create a clone of this CodingType object
    /// </summary>
    public virtual CodingType Clone()
    {
        return ((CodingType)(MemberwiseClone()));
    }
    #endregion
}
}
#pragma warning restore

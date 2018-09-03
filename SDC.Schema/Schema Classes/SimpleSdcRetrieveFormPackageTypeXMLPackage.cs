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
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:ihe:qrph:sdc:2016")]
public partial class SimpleSdcRetrieveFormPackageTypeXMLPackage
{
    
    private static XmlSerializer serializer;
    
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public virtual FormDesignType DemogFormDesign { get; set; }
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public virtual FormDesignType FormDesign { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("DataElement", Order=2)]
        public virtual List<DataElementType> DataElement { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("MapTemplate", Order=3)]
        public virtual List<MappingType> MapTemplate { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("ReportDesignTemplate", Order=4)]
        public virtual List<SimpleSdcRetrieveFormPackageTypeXMLPackageReportDesignTemplate> ReportDesignTemplate { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("HelperFile", Order=5)]
        public virtual List<SimpleSdcRetrieveFormPackageTypeXMLPackageHelperFile> HelperFile { get; set; }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(SimpleSdcRetrieveFormPackageTypeXMLPackage));
            }
            return serializer;
        }
    }
    
    /// <summary>
    /// Test whether DataElement should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDataElement()
    {
        return DataElement != null && DataElement.Count > 0;
    }
    
    /// <summary>
    /// Test whether MapTemplate should be serialized
    /// </summary>
    public virtual bool ShouldSerializeMapTemplate()
    {
        return MapTemplate != null && MapTemplate.Count > 0;
    }
    
    /// <summary>
    /// Test whether ReportDesignTemplate should be serialized
    /// </summary>
    public virtual bool ShouldSerializeReportDesignTemplate()
    {
        return ReportDesignTemplate != null && ReportDesignTemplate.Count > 0;
    }
    
    /// <summary>
    /// Test whether HelperFile should be serialized
    /// </summary>
    public virtual bool ShouldSerializeHelperFile()
    {
        return HelperFile != null && HelperFile.Count > 0;
    }
    
    /// <summary>
    /// Test whether DemogFormDesign should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDemogFormDesign()
    {
        return (DemogFormDesign != null);
    }
    
    /// <summary>
    /// Test whether FormDesign should be serialized
    /// </summary>
    public virtual bool ShouldSerializeFormDesign()
    {
        return (FormDesign != null);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current SimpleSdcRetrieveFormPackageTypeXMLPackage object into an XML string
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
    /// Deserializes workflow markup into an SimpleSdcRetrieveFormPackageTypeXMLPackage object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output SimpleSdcRetrieveFormPackageTypeXMLPackage object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out SimpleSdcRetrieveFormPackageTypeXMLPackage obj, out System.Exception exception)
    {
        exception = null;
        obj = default(SimpleSdcRetrieveFormPackageTypeXMLPackage);
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
    
    public static bool Deserialize(string input, out SimpleSdcRetrieveFormPackageTypeXMLPackage obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public static SimpleSdcRetrieveFormPackageTypeXMLPackage Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((SimpleSdcRetrieveFormPackageTypeXMLPackage)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static SimpleSdcRetrieveFormPackageTypeXMLPackage Deserialize(System.IO.Stream s)
    {
        return ((SimpleSdcRetrieveFormPackageTypeXMLPackage)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current SimpleSdcRetrieveFormPackageTypeXMLPackage object into file
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
    /// Deserializes xml markup from file into an SimpleSdcRetrieveFormPackageTypeXMLPackage object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output SimpleSdcRetrieveFormPackageTypeXMLPackage object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SimpleSdcRetrieveFormPackageTypeXMLPackage obj, out System.Exception exception)
    {
        exception = null;
        obj = default(SimpleSdcRetrieveFormPackageTypeXMLPackage);
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
    
    public static bool LoadFromFile(string fileName, out SimpleSdcRetrieveFormPackageTypeXMLPackage obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out SimpleSdcRetrieveFormPackageTypeXMLPackage obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static SimpleSdcRetrieveFormPackageTypeXMLPackage LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public static SimpleSdcRetrieveFormPackageTypeXMLPackage LoadFromFile(string fileName, System.Text.Encoding encoding)
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

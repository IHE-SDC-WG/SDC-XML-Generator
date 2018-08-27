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
using System.Xml;
using System.Collections.Generic;

/// <summary>
/// This Rule sets the activation status of Items based on the selection status of other ListItems.
/// 
/// This declarative rule determines (guards) when target Items should be activated or deactivated.  The rule may optionally activate/deactivate multiple target items with a single rule. The target item(s) to activate/deactivate are listed in targetNameList.
/// 
/// In the simplest case, this rule operates as follows:  A list of ListItems is provided (selectedItemWatchList).  If all the items in the list are selected (or unselected - see below) as specified in the selectedItemWatchList list, then the guard evaluates to true, and the targetNameList items are activated/deactivated.
/// 
/// In some cases, we may wish to watch unselected items in the selectedItemWatchList.  This is indicated by prefixing the name of the watched item with a minus sign/dash ("-").
/// 
/// In some cases, we may wish to deactivate items in the targetNameSelectList list when the selectedItemWatchList evaluated to true.  In this case, the target item is prefixed with a dash ("-").
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class RuleAutoActivationType : ExtensionBaseType
{
    
    private static XmlSerializer serializer;
    
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
        public string selectedItemSet { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool onlyIf { get; set; }
    /// <summary>
    /// This list contains the @names of Identified Items that will be automatically activated or deactivated when the @selectedItemSet evaluates to true.
    /// 
    /// If a @name is prefixed with a hyphen (-), then the item will be deactivated when @selectedItemSet evaluates to true.  If @not = true, then the Boolean rule evaluation is negated, and thus the rule works in reverse.
    /// 
    /// If @onlyIf is true, then the above rule is reversed when @selectedItemSet evaluates to false.  In other words, named items will be deactivated, and hyphen-prefixed items will be activated when @selectedItemSet is false.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NCName")]
        public string targetNameActivationList { get; set; }
    /// <summary>
    /// Make target items visible when activated and vice versa.  Default = false.  All descendants are affected in the same way.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(toggleType.@true)]
        public toggleType setVisibility { get; set; }
    /// <summary>
    /// Make target items enabled when activated and vice versa.  Default = true.  All descendants are affected in the same way.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(toggleType.@true)]
        public toggleType setEnabled { get; set; }
    /// <summary>
    /// Expand target items when activated and collapse item when deactivated.  Default = false.  All descendants are affected in the same way.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(toggleType.@true)]
        public toggleType setExpanded { get; set; }
    /// <summary>
    /// Delete all user selections, responses and comments when the item is deactivated.  Applies to all descendant items as well.  User should be warned before deleting anything, with an option to preserve the responses in the disabled items.  Disabled item responses should not be saved with the form data.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool removeResponsesWhenDeactivated { get; set; }
    
    /// <summary>
    /// RuleAutoActivationType class constructor
    /// </summary>
    public RuleAutoActivationType()
    {
        this.onlyIf = false;
        this.setVisibility = toggleType.@true;
        this.setEnabled = toggleType.@true;
        this.setExpanded = toggleType.@true;
        this.removeResponsesWhenDeactivated = false;
    }
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(RuleAutoActivationType));
            }
            return serializer;
        }
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current RuleAutoActivationType object into an XML string
    /// </summary>
    /// <returns>string XML value</returns>
    public virtual string Serialize()
    {
        System.IO.StreamReader streamReader = null;
        System.IO.MemoryStream memoryStream = null;
        try
        {
            memoryStream = new System.IO.MemoryStream();
            System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
            xmlWriterSettings.NewLineOnAttributes = true;
            System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
            Serializer.Serialize(xmlWriter, this);
            memoryStream.Seek(0, SeekOrigin.Begin);
            streamReader = new System.IO.StreamReader(memoryStream);
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
    /// Deserializes workflow markup into an RuleAutoActivationType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output RuleAutoActivationType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out RuleAutoActivationType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(RuleAutoActivationType);
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
    
    public static bool Deserialize(string input, out RuleAutoActivationType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static RuleAutoActivationType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((RuleAutoActivationType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static RuleAutoActivationType Deserialize(System.IO.Stream s)
    {
        return ((RuleAutoActivationType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current RuleAutoActivationType object into file
    /// </summary>
    /// <param name="fileName">full path of outupt xml file</param>
    /// <param name="exception">output Exception value if failed</param>
    /// <returns>true if can serialize and save into file; otherwise, false</returns>
    public virtual bool SaveToFile(string fileName, out System.Exception exception)
    {
        exception = null;
        try
        {
            SaveToFile(fileName);
            return true;
        }
        catch (System.Exception e)
        {
            exception = e;
            return false;
        }
    }
    
    public virtual void SaveToFile(string fileName)
    {
        System.IO.StreamWriter streamWriter = null;
        try
        {
            string xmlString = Serialize();
            System.IO.FileInfo xmlFile = new System.IO.FileInfo(fileName);
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
    /// Deserializes xml markup from file into an RuleAutoActivationType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output RuleAutoActivationType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, out RuleAutoActivationType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(RuleAutoActivationType);
        try
        {
            obj = LoadFromFile(fileName);
            return true;
        }
        catch (System.Exception ex)
        {
            exception = ex;
            return false;
        }
    }
    
    public static bool LoadFromFile(string fileName, out RuleAutoActivationType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public new static RuleAutoActivationType LoadFromFile(string fileName)
    {
        System.IO.FileStream file = null;
        System.IO.StreamReader sr = null;
        try
        {
            file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
            sr = new System.IO.StreamReader(file);
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

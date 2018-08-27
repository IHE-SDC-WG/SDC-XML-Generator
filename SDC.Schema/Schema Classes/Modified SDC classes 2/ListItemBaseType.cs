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
/// This base element is the foundation for ListItems, but does not include the MainItems sub-group under each ListItem.
/// </summary>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ListItemType))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public abstract partial class ListItemBaseType : DisplayedType
{
    
    private static XmlSerializer serializer;
    
    /// <summary>
    /// A place to enter values (of any data type) that are directly associated with, and attached to, a selected answer choice.
    /// </summary>
        public ListItemResponseFieldType ListItemResponseField { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("OnSelect")]
        public List<EventType> OnSelect { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("OnDeselect")]
        public List<EventType> OnDeselect { get; set; }
        public GuardType SelectIf { get; set; }
        public GuardType DeselectIf { get; set; }
    /// <summary>
    /// Represents the default value of the ListItem in the FormDesign template, or the user's response in selecting one or more ListItems.  If @multiSelect='false' on ListField then only one item may be selected.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool selected { get; set; }
    /// <summary>
    /// If set to true, then selecting this ListItem must deactivate all descendant parts of the form, and ignore any user-entered values in the deactivated part.  Deselecting the ListItem should reactivate the descendant items in their state at the time the items were deactivated.
    /// 
    /// If items are disabled, then any data stored in the disabled questions should be removed.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool selectionDisablesChildren { get; set; }
    /// <summary>
    /// NEW
    /// Selecting the current ListItem will enable the named items in this attribute's content.
    /// 
    /// Prefixing any named with a hyphen (-) will reverse the above behaviour (i.e., the named items will be disabled).
    /// 
    /// Unselecting the ListItem will reverse this behaviour.
    /// Prefixing the name with a tilde (~) will supress this reversal behavior.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
        public string selectionActivatesItems { get; set; }
    /// <summary>
    /// NEW
    /// Selecting the current ListItem will select the named ListItems in this attribute's content.
    /// 
    /// Prefixing any named with a hyphen (-) will reverse the above behaviour.
    /// 
    /// Unselecting the ListItem will reverse this behaviour.
    /// Prefixing the name with a tilde (~) will suppress this reversal behavior.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
        public string selectionSelectsListItems { get; set; }
    /// <summary>
    /// If the ancestor ListField has @multiselect = 'true', then selecting this ListItem should de-select all other ListItem (sibling) nodes except the current one.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool selectionDeselectsSiblings { get; set; }
    /// <summary>
    /// NEW:
    /// 
    /// If @omitWhenSelected is set to true, then the question and its response(s) should not be present in a typical  report derived from this template.  This attribute is usually set to true when the answer choice is used to control form behavior (e.g., skip logic), or when the question provides unhelpful "negative" information about actions that did not occur or were not performed, or things that were not observed or could not be assessed.  If @omitWhenSelectedset is false (default) then the question and its response(s) should appear in the report.  Added 11/29/15 to replace @reportAction
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool omitWhenSelected { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        [System.ComponentModel.DefaultValueAttribute("1")]
        public string repeat { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string instanceGUID { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string parentGUID { get; set; }
    /// <summary>
    /// A typed value (e.g., an integer) that is uniquely associated with a ListItem.  An example is the integer 10 for a ListItem with title that reads "10 o'clock".  Typically these values are set to be used in calculations or other algorithms.  In general, they can be treated something like a user-entered response on a the ListItemResponseField of a selected ListItem.
    /// 
    /// This field should not be used for terminologies or local codes.  The CodedValue type should be used for these kinds of metadata.  This field should also not be used other properties such as translations, usage, etc.
    /// 
    /// The data type shoudl be specified in @AssociatedValueType
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string associatedValue { get; set; }
    /// <summary>
    /// The data type of @AssociatedValue.  Default is string.
    /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string associatedValueType { get; set; }
    
    ///// <summary>
    ///// ListItemBaseType class constructor
    ///// </summary>
    //public ListItemBaseType()
    //{
    //    this.selected = false;
    //    this.selectionDisablesChildren = false;
    //    this.selectionDeselectsSiblings = false;
    //    this.omitWhenSelected = false;
    //    this.repeat = "1";
    //}
    
    private static XmlSerializer Serializer
    {
        get
        {
            if ((serializer == null))
            {
                serializer = new XmlSerializerFactory().CreateSerializer(typeof(ListItemBaseType));
            }
            return serializer;
        }
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current ListItemBaseType object into an XML string
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
    /// Deserializes workflow markup into an ListItemBaseType object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output ListItemBaseType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out ListItemBaseType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(ListItemBaseType);
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
    
    public static bool Deserialize(string input, out ListItemBaseType obj)
    {
        System.Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public new static ListItemBaseType Deserialize(string input)
    {
        System.IO.StringReader stringReader = null;
        try
        {
            stringReader = new System.IO.StringReader(input);
            return ((ListItemBaseType)(Serializer.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static ListItemBaseType Deserialize(System.IO.Stream s)
    {
        return ((ListItemBaseType)(Serializer.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current ListItemBaseType object into file
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
    /// Deserializes xml markup from file into an ListItemBaseType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output ListItemBaseType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, out ListItemBaseType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(ListItemBaseType);
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
    
    public static bool LoadFromFile(string fileName, out ListItemBaseType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public new static ListItemBaseType LoadFromFile(string fileName)
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

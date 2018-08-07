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
/// This base element is the foundation for ListItems, but does not include the MainItems sub-group under each ListItem.
/// </summary>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ListItemType))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2053.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class ListItemBaseType : DisplayedType
{
    
    #region Private fields
    private bool _shouldSerializeomitWhenSelected;
    
    private bool _shouldSerializeselectionDeselectsSiblings;
    
    private bool _shouldSerializeselectionDisablesChildren;
    
    private bool _shouldSerializeselected;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private ListItemResponseFieldType _listItemResponseField;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private List<EventType> _onSelect;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private List<EventType> _onDeselect;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private WatchedPropertyType _selectIf;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private WatchedPropertyType _deselectIf;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private bool _selected;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private bool _selectionDisablesChildren;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _selectionActivatesItems;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _selectionSelectsListItems;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private bool _selectionDeselectsSiblings;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _x_selectionDisablesItems;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _x_SelectionDeselectsListItems;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private bool _omitWhenSelected;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _repeat;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _instanceGUID;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _parentGUID;
    
    private static XmlSerializer serializer;
    #endregion
    
    /// <summary>
    /// ListItemBaseType class constructor
    /// </summary>
    //public ListItemBaseType()
    //{
    //    this._selected = false;
    //    this._selectionDisablesChildren = false;
    //    this._selectionDeselectsSiblings = false;
    //    this._omitWhenSelected = false;
    //    this._repeat = "1";
    //}
    
    /// <summary>
    /// A place to enter values (of any data type) that are directly associated with, and attached to, a selected answer choice.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public virtual ListItemResponseFieldType ListItemResponseField
    {
        get
        {
            return this._listItemResponseField;
        }
        set
        {
            this._listItemResponseField = value;
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute("OnSelect", Order=1)]
    public virtual List<EventType> OnSelect
    {
        get
        {
            return this._onSelect;
        }
        set
        {
            this._onSelect = value;
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute("OnDeselect", Order=2)]
    public virtual List<EventType> OnDeselect
    {
        get
        {
            return this._onDeselect;
        }
        set
        {
            this._onDeselect = value;
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute(Order=3)]
    public virtual WatchedPropertyType SelectIf
    {
        get
        {
            return this._selectIf;
        }
        set
        {
            this._selectIf = value;
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute(Order=4)]
    public virtual WatchedPropertyType DeselectIf
    {
        get
        {
            return this._deselectIf;
        }
        set
        {
            this._deselectIf = value;
        }
    }
    
    /// <summary>
    /// Represents the default value of the ListItem in the FormDesign template, or the user's response in selecting one or more ListItems.  If @multiSelect='false' on ListField then only one item may be selected.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(false)]
    public virtual bool selected
    {
        get
        {
            return this._selected;
        }
        set
        {
            this._selected = value;
            _shouldSerializeselected = true;
        }
    }
    
    /// <summary>
    /// If set to true, then selecting this ListItem must deactivate all descendant parts of the form, and ignore any user-entered values in the deactivated part.  Deselecting the ListItem should reactivate the descendant items in their state at the time the items were deactivated.
    /// 
    /// If items are disabled, then any data stored in the disabled questions should be removed.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(false)]
    public virtual bool selectionDisablesChildren
    {
        get
        {
            return this._selectionDisablesChildren;
        }
        set
        {
            this._selectionDisablesChildren = value;
            _shouldSerializeselectionDisablesChildren = true;
        }
    }
    
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
    public virtual string selectionActivatesItems
    {
        get
        {
            return this._selectionActivatesItems;
        }
        set
        {
            this._selectionActivatesItems = value;
        }
    }
    
    /// <summary>
    /// NEW
    /// Selecting the current ListItem will select the named ListItems in this attribute's content.
    /// 
    /// Prefixing any named with a hyphen (-) will reverse the above behaviour.
    /// 
    /// Unselecting the ListItem will reverse this behaviour.
    /// Prefixing the name with a tilde (~) will supress this reversal behavior.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
    public virtual string selectionSelectsListItems
    {
        get
        {
            return this._selectionSelectsListItems;
        }
        set
        {
            this._selectionSelectsListItems = value;
        }
    }
    
    /// <summary>
    /// If the ancestor ListField has @multiselect = 'true', then selecting this ListItem should de-select all other ListItem (sibling) nodes except the current one.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(false)]
    public virtual bool selectionDeselectsSiblings
    {
        get
        {
            return this._selectionDeselectsSiblings;
        }
        set
        {
            this._selectionDeselectsSiblings = value;
            _shouldSerializeselectionDeselectsSiblings = true;
        }
    }
    
    /// <summary>
    /// NEW
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
    public virtual string X_selectionDisablesItems
    {
        get
        {
            return this._x_selectionDisablesItems;
        }
        set
        {
            this._x_selectionDisablesItems = value;
        }
    }
    
    /// <summary>
    /// NEW:
    /// Selecting the current ListItem will cause the named items in the list (or any other list) to be de-selected.  All named items must be prefixed with the $ symbol, and must point to ListItems.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
    public virtual string X_SelectionDeselectsListItems
    {
        get
        {
            return this._x_SelectionDeselectsListItems;
        }
        set
        {
            this._x_SelectionDeselectsListItems = value;
        }
    }
    
    /// <summary>
    /// NEW:
    /// 
    /// If @omitWhenSelected is set to true, then the question and its response(s) should not be present in a typical  report derived from this template.  This attribute is usually set to true when the answer choice is used to control form behavior (e.g., skip logic), or when the question provides unhelpful "negative" information about actions that did not occur or were not performed, or things that were not observed or could not be assessed.  If @omitWhenSelectedset is false (default) then the question and its response(s) should appear in the report.  Added 11/29/15 to replace @reportAction
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(false)]
    public virtual bool omitWhenSelected
    {
        get
        {
            return this._omitWhenSelected;
        }
        set
        {
            this._omitWhenSelected = value;
            _shouldSerializeomitWhenSelected = true;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
    [System.ComponentModel.DefaultValueAttribute("1")]
    public virtual string repeat
    {
        get
        {
            return this._repeat;
        }
        set
        {
            this._repeat = value;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string instanceGUID
    {
        get
        {
            return this._instanceGUID;
        }
        set
        {
            this._instanceGUID = value;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string parentGUID
    {
        get
        {
            return this._parentGUID;
        }
        set
        {
            this._parentGUID = value;
        }
    }
    
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
    
    /// <summary>
    /// Test whether OnSelect should be serialized
    /// </summary>
    public virtual bool ShouldSerializeOnSelect()
    {
        return OnSelect != null && OnSelect.Count > 0;
    }
    
    /// <summary>
    /// Test whether OnDeselect should be serialized
    /// </summary>
    public virtual bool ShouldSerializeOnDeselect()
    {
        return OnDeselect != null && OnDeselect.Count > 0;
    }
    
    /// <summary>
    /// Test whether selected should be serialized
    /// </summary>
    public virtual bool ShouldSerializeselected()
    {
        if (_shouldSerializeselected)
        {
            return true;
        }
        return (_selected != default(bool));
    }
    
    /// <summary>
    /// Test whether selectionDisablesChildren should be serialized
    /// </summary>
    public virtual bool ShouldSerializeselectionDisablesChildren()
    {
        if (_shouldSerializeselectionDisablesChildren)
        {
            return true;
        }
        return (_selectionDisablesChildren != default(bool));
    }
    
    /// <summary>
    /// Test whether selectionDeselectsSiblings should be serialized
    /// </summary>
    public virtual bool ShouldSerializeselectionDeselectsSiblings()
    {
        if (_shouldSerializeselectionDeselectsSiblings)
        {
            return true;
        }
        return (_selectionDeselectsSiblings != default(bool));
    }
    
    /// <summary>
    /// Test whether omitWhenSelected should be serialized
    /// </summary>
    public virtual bool ShouldSerializeomitWhenSelected()
    {
        if (_shouldSerializeomitWhenSelected)
        {
            return true;
        }
        return (_omitWhenSelected != default(bool));
    }
    
    /// <summary>
    /// Test whether ListItemResponseField should be serialized
    /// </summary>
    public virtual bool ShouldSerializeListItemResponseField()
    {
        return (_listItemResponseField != null);
    }
    
    /// <summary>
    /// Test whether SelectIf should be serialized
    /// </summary>
    public virtual bool ShouldSerializeSelectIf()
    {
        return (_selectIf != null);
    }
    
    /// <summary>
    /// Test whether DeselectIf should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDeselectIf()
    {
        return (_deselectIf != null);
    }
    
    /// <summary>
    /// Test whether selectionActivatesItems should be serialized
    /// </summary>
    public virtual bool ShouldSerializeselectionActivatesItems()
    {
        return !string.IsNullOrEmpty(selectionActivatesItems);
    }
    
    /// <summary>
    /// Test whether selectionSelectsListItems should be serialized
    /// </summary>
    public virtual bool ShouldSerializeselectionSelectsListItems()
    {
        return !string.IsNullOrEmpty(selectionSelectsListItems);
    }
    
    /// <summary>
    /// Test whether X_selectionDisablesItems should be serialized
    /// </summary>
    public virtual bool ShouldSerializeX_selectionDisablesItems()
    {
        return !string.IsNullOrEmpty(X_selectionDisablesItems);
    }
    
    /// <summary>
    /// Test whether X_SelectionDeselectsListItems should be serialized
    /// </summary>
    public virtual bool ShouldSerializeX_SelectionDeselectsListItems()
    {
        return !string.IsNullOrEmpty(X_SelectionDeselectsListItems);
    }
    
    /// <summary>
    /// Test whether repeat should be serialized
    /// </summary>
    public virtual bool ShouldSerializerepeat()
    {
        return !string.IsNullOrEmpty(repeat);
    }
    
    /// <summary>
    /// Test whether instanceGUID should be serialized
    /// </summary>
    public virtual bool ShouldSerializeinstanceGUID()
    {
        return !string.IsNullOrEmpty(instanceGUID);
    }
    
    /// <summary>
    /// Test whether parentGUID should be serialized
    /// </summary>
    public virtual bool ShouldSerializeparentGUID()
    {
        return !string.IsNullOrEmpty(parentGUID);
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current ListItemBaseType object into an XML string
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
    /// Deserializes xml markup from file into an ListItemBaseType object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output ListItemBaseType object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ListItemBaseType obj, out System.Exception exception)
    {
        exception = null;
        obj = default(ListItemBaseType);
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
    
    public static bool LoadFromFile(string fileName, out ListItemBaseType obj, out System.Exception exception)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8, out obj, out exception);
    }
    
    public static bool LoadFromFile(string fileName, out ListItemBaseType obj)
    {
        System.Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static ListItemBaseType LoadFromFile(string fileName)
    {
        return LoadFromFile(fileName, System.Text.Encoding.UTF8);
    }
    
    public new static ListItemBaseType LoadFromFile(string fileName, System.Text.Encoding encoding)
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
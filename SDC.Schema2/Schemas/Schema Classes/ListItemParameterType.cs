// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 5.1.87.0. www.xsd2code.com
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
namespace SDC.Schema2
{
using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

/// <summary>
/// Parameters are named, uniquely identifiable, instances of form attributes (e.g., @selected).  They are fed into expressions, which are then used as part of a rule within the form.  Parameters can also be fed into URI expressions used inside a Lookup Endpoint, i.e., URIs that call web services to supply list items (e.g., a list of SNOMED-coded items) to a question.
/// 
/// Parameters using this construct are derived from other locations in the XML instance document, including user-entered Response values.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class ListItemParameterType : ExtensionBaseType
{
    
    #region Private fields
    private string _dataType;
    
    private string _paramName;
    
    private string _sourceQuestionName;
    
    private string _listItemAttribute;
    #endregion
    
    /// <summary>
    /// ListItemParameterType class constructor
    /// </summary>
    public ListItemParameterType()
    {
        this._dataType = "string";
        this._listItemAttribute = "associatedValue";
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute("string")]
    public virtual string dataType
    {
        get
        {
            return this._dataType;
        }
        set
        {
            if ((this._dataType == value))
            {
                return;
            }
            if (((this._dataType == null) 
                        || (_dataType.Equals(value) != true)))
            {
                this._dataType = value;
                this.OnPropertyChanged("dataType", value);
            }
        }
    }
    
    /// <summary>
    /// A locally useful name that describes the parameter
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NCName")]
    public virtual string paramName
    {
        get
        {
            return this._paramName;
        }
        set
        {
            if ((this._paramName == value))
            {
                return;
            }
            if (((this._paramName == null) 
                        || (_paramName.Equals(value) != true)))
            {
                this._paramName = value;
                this.OnPropertyChanged("paramName", value);
            }
        }
    }
    
    /// <summary>
    /// The @name of a Question in the current form.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NCName")]
    public virtual string sourceQuestionName
    {
        get
        {
            return this._sourceQuestionName;
        }
        set
        {
            if ((this._sourceQuestionName == value))
            {
                return;
            }
            if (((this._sourceQuestionName == null) 
                        || (_sourceQuestionName.Equals(value) != true)))
            {
                this._sourceQuestionName = value;
                this.OnPropertyChanged("sourceQuestionName", value);
            }
        }
    }
    
    /// <summary>
    /// The name of any XML attribute on a ListItem.  The property value is the parameter's value.  If the Question is multi-select, a list of attribute values is returned.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NCName")]
    [System.ComponentModel.DefaultValueAttribute("associatedValue")]
    public virtual string listItemAttribute
    {
        get
        {
            return this._listItemAttribute;
        }
        set
        {
            if ((this._listItemAttribute == value))
            {
                return;
            }
            if (((this._listItemAttribute == null) 
                        || (_listItemAttribute.Equals(value) != true)))
            {
                this._listItemAttribute = value;
                this.OnPropertyChanged("listItemAttribute", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether dataType should be serialized
    /// </summary>
    public virtual bool ShouldSerializedataType()
    {
        return !string.IsNullOrEmpty(dataType);
    }
    
    /// <summary>
    /// Test whether paramName should be serialized
    /// </summary>
    public virtual bool ShouldSerializeparamName()
    {
        return !string.IsNullOrEmpty(paramName);
    }
    
    /// <summary>
    /// Test whether sourceQuestionName should be serialized
    /// </summary>
    public virtual bool ShouldSerializesourceQuestionName()
    {
        return !string.IsNullOrEmpty(sourceQuestionName);
    }
    
    /// <summary>
    /// Test whether listItemAttribute should be serialized
    /// </summary>
    public virtual bool ShouldSerializelistItemAttribute()
    {
        return !string.IsNullOrEmpty(listItemAttribute);
    }
}
}
#pragma warning restore

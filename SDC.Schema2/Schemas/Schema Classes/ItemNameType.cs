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
/// Moved from SDCFormDesign
/// The type is a standard way to point to a named item anywhere in a FormDesign template.  A named item is any element that has the @name attribute set to a unique value.
/// </summary>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ActAddCodeType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ItemNameAttributeType))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class ItemNameType : ExtensionBaseType
{
    
    #region Private fields
    private string _itemName;
    #endregion
    
    /// <summary>
    /// The @name attribute of the referenced element.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NCName")]
    public virtual string itemName
    {
        get
        {
            return this._itemName;
        }
        set
        {
            if ((this._itemName == value))
            {
                return;
            }
            if (((this._itemName == null) 
                        || (_itemName.Equals(value) != true)))
            {
                this._itemName = value;
                this.OnPropertyChanged("itemName", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether itemName should be serialized
    /// </summary>
    public virtual bool ShouldSerializeitemName()
    {
        return !string.IsNullOrEmpty(itemName);
    }
}
}
#pragma warning restore

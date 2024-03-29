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

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class ChangedFieldType : ExtensionBaseType
{
    
    #region Private fields
    private BaseType _item;
    #endregion
    
    [System.Xml.Serialization.XmlElementAttribute("TargetItemID", typeof(TargetItemIDType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("TargetItemName", typeof(TargetItemNameType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("TargetItemXPath", typeof(TargetItemXPathType), Order=0)]
    public virtual BaseType Item
    {
        get
        {
            return this._item;
        }
        set
        {
            if ((this._item == value))
            {
                return;
            }
            if (((this._item == null) 
                        || (_item.Equals(value) != true)))
            {
                this._item = value;
                this.OnPropertyChanged("Item", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether Item should be serialized
    /// </summary>
    public virtual bool ShouldSerializeItem()
    {
        return (_item != null);
    }
}
}
#pragma warning restore

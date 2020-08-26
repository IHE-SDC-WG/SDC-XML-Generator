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
[System.Xml.Serialization.XmlRootAttribute("SDCPackageList", Namespace="urn:ihe:qrph:sdc:2016", IsNullable=false)]
public partial class PackageListType : ExtensionBaseType
{
    
    #region Private fields
    private List<PackageItemType> _packageItem;
    
    private HTMLPackageType _hTML;
    #endregion
    
    [System.Xml.Serialization.XmlElementAttribute("PackageItem", Order=0)]
    public virtual List<PackageItemType> PackageItem
    {
        get
        {
            return this._packageItem;
        }
        set
        {
            if ((this._packageItem == value))
            {
                return;
            }
            if (((this._packageItem == null) 
                        || (_packageItem.Equals(value) != true)))
            {
                this._packageItem = value;
                this.OnPropertyChanged("PackageItem", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public virtual HTMLPackageType HTML
    {
        get
        {
            return this._hTML;
        }
        set
        {
            if ((this._hTML == value))
            {
                return;
            }
            if (((this._hTML == null) 
                        || (_hTML.Equals(value) != true)))
            {
                this._hTML = value;
                this.OnPropertyChanged("HTML", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether PackageItem should be serialized
    /// </summary>
    public virtual bool ShouldSerializePackageItem()
    {
        return PackageItem != null && PackageItem.Count > 0;
    }
    
    /// <summary>
    /// Test whether HTML should be serialized
    /// </summary>
    public virtual bool ShouldSerializeHTML()
    {
        return (_hTML != null);
    }
}
}
#pragma warning restore

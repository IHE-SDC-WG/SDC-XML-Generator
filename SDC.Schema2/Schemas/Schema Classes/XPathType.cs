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
public partial class XPathType : string_Stype
{
    
    #region Private fields
    private string _version;
    #endregion
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string version
    {
        get
        {
            return this._version;
        }
        set
        {
            if ((this._version == value))
            {
                return;
            }
            if (((this._version == null) 
                        || (_version.Equals(value) != true)))
            {
                this._version = value;
                this.OnPropertyChanged("version", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether version should be serialized
    /// </summary>
    public virtual bool ShouldSerializeversion()
    {
        return !string.IsNullOrEmpty(version);
    }
}
}
#pragma warning restore

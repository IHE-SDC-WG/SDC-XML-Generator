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
public partial class FileHashType : string_Stype
{
    
    #region Private fields
    private string _hashType;
    #endregion
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string hashType
    {
        get
        {
            return this._hashType;
        }
        set
        {
            if ((this._hashType == value))
            {
                return;
            }
            if (((this._hashType == null) 
                        || (_hashType.Equals(value) != true)))
            {
                this._hashType = value;
                this.OnPropertyChanged("hashType", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether hashType should be serialized
    /// </summary>
    public virtual bool ShouldSerializehashType()
    {
        return !string.IsNullOrEmpty(hashType);
    }
}
}
#pragma warning restore

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
/// Function or web service that returns a string value.
/// </summary>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CallFuncActionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LookupEndPointType))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public abstract partial class CallFuncType : CallFuncBaseType
{
    
    #region Private fields
    private string _dataType;
    #endregion
    
    ///// <summary>
    ///// CallFuncType class constructor
    ///// </summary>
    //public CallFuncType()
    //{
    //    this._dataType = "string";
    //}
    
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
    /// Test whether dataType should be serialized
    /// </summary>
    public virtual bool ShouldSerializedataType()
    {
        return !string.IsNullOrEmpty(dataType);
    }
}
}
#pragma warning restore

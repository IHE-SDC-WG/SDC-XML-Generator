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
public partial class hexBinary_DEtype : hexBinary_Stype
{
    
    #region Private fields
    private bool _shouldSerializemaxLength;
    
    private bool _shouldSerializeminLength;
    
    private string _description;
    
    private long _minLength;
    
    private long _maxLength;
    
    private string _mask;
    #endregion
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string description
    {
        get
        {
            return this._description;
        }
        set
        {
            if ((this._description == value))
            {
                return;
            }
            if (((this._description == null) 
                        || (_description.Equals(value) != true)))
            {
                this._description = value;
                this.OnPropertyChanged("description", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual long minLength
    {
        get
        {
            return this._minLength;
        }
        set
        {
            if ((_minLength.Equals(value) != true))
            {
                this._minLength = value;
                this.OnPropertyChanged("minLength", value);
            }
            _shouldSerializeminLength = true;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual long maxLength
    {
        get
        {
            return this._maxLength;
        }
        set
        {
            if ((_maxLength.Equals(value) != true))
            {
                this._maxLength = value;
                this.OnPropertyChanged("maxLength", value);
            }
            _shouldSerializemaxLength = true;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string mask
    {
        get
        {
            return this._mask;
        }
        set
        {
            if ((this._mask == value))
            {
                return;
            }
            if (((this._mask == null) 
                        || (_mask.Equals(value) != true)))
            {
                this._mask = value;
                this.OnPropertyChanged("mask", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether minLength should be serialized
    /// </summary>
    public virtual bool ShouldSerializeminLength()
    {
        if (_shouldSerializeminLength)
        {
            return true;
        }
        return (_minLength != default(long));
    }
    
    /// <summary>
    /// Test whether maxLength should be serialized
    /// </summary>
    public virtual bool ShouldSerializemaxLength()
    {
        if (_shouldSerializemaxLength)
        {
            return true;
        }
        return (_maxLength != default(long));
    }
    
    /// <summary>
    /// Test whether description should be serialized
    /// </summary>
    public virtual bool ShouldSerializedescription()
    {
        return !string.IsNullOrEmpty(description);
    }
    
    /// <summary>
    /// Test whether mask should be serialized
    /// </summary>
    public virtual bool ShouldSerializemask()
    {
        return !string.IsNullOrEmpty(mask);
    }
}
}
#pragma warning restore

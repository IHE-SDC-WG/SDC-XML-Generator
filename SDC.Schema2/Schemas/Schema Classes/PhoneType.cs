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
/// A structure for recording telephone numbers.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class PhoneType : ExtensionBaseType
{
    
    #region Private fields
    private string_Stype _phoneType1;
    
    private CountryCodeType _countryCode;
    
    private AreaCodeType _areaCode;
    
    private PhoneNumberType _phoneNumber;
    
    private string_Stype _phoneExtension;
    
    private string_Stype _usage;
    #endregion
    
    [System.Xml.Serialization.XmlElementAttribute("PhoneType", Order=0)]
    public virtual string_Stype PhoneType1
    {
        get
        {
            return this._phoneType1;
        }
        set
        {
            if ((this._phoneType1 == value))
            {
                return;
            }
            if (((this._phoneType1 == null) 
                        || (_phoneType1.Equals(value) != true)))
            {
                this._phoneType1 = value;
                this.OnPropertyChanged("PhoneType1", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public virtual CountryCodeType CountryCode
    {
        get
        {
            return this._countryCode;
        }
        set
        {
            if ((this._countryCode == value))
            {
                return;
            }
            if (((this._countryCode == null) 
                        || (_countryCode.Equals(value) != true)))
            {
                this._countryCode = value;
                this.OnPropertyChanged("CountryCode", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public virtual AreaCodeType AreaCode
    {
        get
        {
            return this._areaCode;
        }
        set
        {
            if ((this._areaCode == value))
            {
                return;
            }
            if (((this._areaCode == null) 
                        || (_areaCode.Equals(value) != true)))
            {
                this._areaCode = value;
                this.OnPropertyChanged("AreaCode", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute(Order=3)]
    public virtual PhoneNumberType PhoneNumber
    {
        get
        {
            return this._phoneNumber;
        }
        set
        {
            if ((this._phoneNumber == value))
            {
                return;
            }
            if (((this._phoneNumber == null) 
                        || (_phoneNumber.Equals(value) != true)))
            {
                this._phoneNumber = value;
                this.OnPropertyChanged("PhoneNumber", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute(Order=4)]
    public virtual string_Stype PhoneExtension
    {
        get
        {
            return this._phoneExtension;
        }
        set
        {
            if ((this._phoneExtension == value))
            {
                return;
            }
            if (((this._phoneExtension == null) 
                        || (_phoneExtension.Equals(value) != true)))
            {
                this._phoneExtension = value;
                this.OnPropertyChanged("PhoneExtension", value);
            }
        }
    }
    
    /// <summary>
    /// When this phone number should be used
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=5)]
    public virtual string_Stype Usage
    {
        get
        {
            return this._usage;
        }
        set
        {
            if ((this._usage == value))
            {
                return;
            }
            if (((this._usage == null) 
                        || (_usage.Equals(value) != true)))
            {
                this._usage = value;
                this.OnPropertyChanged("Usage", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether PhoneType1 should be serialized
    /// </summary>
    public virtual bool ShouldSerializePhoneType1()
    {
        return (_phoneType1 != null);
    }
    
    /// <summary>
    /// Test whether CountryCode should be serialized
    /// </summary>
    public virtual bool ShouldSerializeCountryCode()
    {
        return (_countryCode != null);
    }
    
    /// <summary>
    /// Test whether AreaCode should be serialized
    /// </summary>
    public virtual bool ShouldSerializeAreaCode()
    {
        return (_areaCode != null);
    }
    
    /// <summary>
    /// Test whether PhoneNumber should be serialized
    /// </summary>
    public virtual bool ShouldSerializePhoneNumber()
    {
        return (_phoneNumber != null);
    }
    
    /// <summary>
    /// Test whether PhoneExtension should be serialized
    /// </summary>
    public virtual bool ShouldSerializePhoneExtension()
    {
        return (_phoneExtension != null);
    }
    
    /// <summary>
    /// Test whether Usage should be serialized
    /// </summary>
    public virtual bool ShouldSerializeUsage()
    {
        return (_usage != null);
    }
}
}
#pragma warning restore
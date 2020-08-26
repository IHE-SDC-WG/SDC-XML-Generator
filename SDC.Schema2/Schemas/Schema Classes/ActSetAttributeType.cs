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
/// This type is used to act upon the value of common item attributes. If
/// an "act" attribute (a type with the "act" prefix) has no value assigned, it is
/// ignored. If it has a value, then that attribute on the target item(s) assume(s) that
/// stated value when an attached Boolean condition evaluates to true. The attached
/// condition may be an "If" statement or any expression that evaluates to a Boolean
/// value.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class ActSetAttributeType : ExtensionBaseType
{
    
    #region Private fields
    private bool _shouldSerializeactReadOnly;
    
    private bool _shouldSerializeactDeleteResponse;
    
    private bool _shouldSerializeactSelect;
    
    private bool _shouldSerializeactActivate;
    
    private bool _shouldSerializeactEnable;
    
    private bool _shouldSerializeactVisible;
    
    private string _targetNames;
    
    private bool _actVisible;
    
    private bool _actEnable;
    
    private string _actMinCard;
    
    private string _actMaxCard;
    
    private bool _actActivate;
    
    private bool _actSelect;
    
    private bool _actDeleteResponse;
    
    private bool _actReadOnly;
    
    private string _actType;
    
    private string _actStyleClass;
    
    private string _actSetTitleText;
    
    private byte[] _actSetBase64HTML;
    
    private string _actSetCode;
    
    private string _actSetCodeSystem;
    
    private string _actSetVal;
    
    private string _actSetAssociatedValue;
    
    private string _actSetValFromRef;
    
    private string _actSetAssociatedValueFromRef;
    #endregion
    
    /// <summary>
    /// The names of the items affected by property
    /// actions
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
    public virtual string targetNames
    {
        get
        {
            return this._targetNames;
        }
        set
        {
            if ((this._targetNames == value))
            {
                return;
            }
            if (((this._targetNames == null) 
                        || (_targetNames.Equals(value) != true)))
            {
                this._targetNames = value;
                this.OnPropertyChanged("targetNames", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actVisible
    {
        get
        {
            return this._actVisible;
        }
        set
        {
            if ((_actVisible.Equals(value) != true))
            {
                this._actVisible = value;
                this.OnPropertyChanged("actVisible", value);
            }
            _shouldSerializeactVisible = true;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actEnable
    {
        get
        {
            return this._actEnable;
        }
        set
        {
            if ((_actEnable.Equals(value) != true))
            {
                this._actEnable = value;
                this.OnPropertyChanged("actEnable", value);
            }
            _shouldSerializeactEnable = true;
        }
    }
    
    /// <summary>
    /// Controls requirement to answer the question and the
    /// minimum number of repeats.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
    public virtual string actMinCard
    {
        get
        {
            return this._actMinCard;
        }
        set
        {
            if ((this._actMinCard == value))
            {
                return;
            }
            if (((this._actMinCard == null) 
                        || (_actMinCard.Equals(value) != true)))
            {
                this._actMinCard = value;
                this.OnPropertyChanged("actMinCard", value);
            }
        }
    }
    
    /// <summary>
    /// Controls requirement to answer the
    /// question
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
    public virtual string actMaxCard
    {
        get
        {
            return this._actMaxCard;
        }
        set
        {
            if ((this._actMaxCard == value))
            {
                return;
            }
            if (((this._actMaxCard == null) 
                        || (_actMaxCard.Equals(value) != true)))
            {
                this._actMaxCard = value;
                this.OnPropertyChanged("actMaxCard", value);
            }
        }
    }
    
    /// <summary>
    /// Toggle visible and enabled together. Setting this to false
    /// will de-activate all descendents but will not change their enabled or
    /// visible properties.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actActivate
    {
        get
        {
            return this._actActivate;
        }
        set
        {
            if ((_actActivate.Equals(value) != true))
            {
                this._actActivate = value;
                this.OnPropertyChanged("actActivate", value);
            }
            _shouldSerializeactActivate = true;
        }
    }
    
    /// <summary>
    /// Toggle selection of a List Item; not applicable to other
    /// items.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actSelect
    {
        get
        {
            return this._actSelect;
        }
        set
        {
            if ((_actSelect.Equals(value) != true))
            {
                this._actSelect = value;
                this.OnPropertyChanged("actSelect", value);
            }
            _shouldSerializeactSelect = true;
        }
    }
    
    /// <summary>
    /// Delete any response in a Response field on a question or
    /// ListItem. Not applicable to other item types.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actDeleteResponse
    {
        get
        {
            return this._actDeleteResponse;
        }
        set
        {
            if ((_actDeleteResponse.Equals(value) != true))
            {
                this._actDeleteResponse = value;
                this.OnPropertyChanged("actDeleteResponse", value);
            }
            _shouldSerializeactDeleteResponse = true;
        }
    }
    
    /// <summary>
    /// Delete any response in a Response field on a question or
    /// ListItem. Not applicable to other item types.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool actReadOnly
    {
        get
        {
            return this._actReadOnly;
        }
        set
        {
            if ((_actReadOnly.Equals(value) != true))
            {
                this._actReadOnly = value;
                this.OnPropertyChanged("actReadOnly", value);
            }
            _shouldSerializeactReadOnly = true;
        }
    }
    
    /// <summary>
    /// Set the @type attribute value
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
    public virtual string actType
    {
        get
        {
            return this._actType;
        }
        set
        {
            if ((this._actType == value))
            {
                return;
            }
            if (((this._actType == null) 
                        || (_actType.Equals(value) != true)))
            {
                this._actType = value;
                this.OnPropertyChanged("actType", value);
            }
        }
    }
    
    /// <summary>
    /// Set the @styleClass attribute value
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
    public virtual string actStyleClass
    {
        get
        {
            return this._actStyleClass;
        }
        set
        {
            if ((this._actStyleClass == value))
            {
                return;
            }
            if (((this._actStyleClass == null) 
                        || (_actStyleClass.Equals(value) != true)))
            {
                this._actStyleClass = value;
                this.OnPropertyChanged("actStyleClass", value);
            }
        }
    }
    
    /// <summary>
    /// Set the @title text on an item.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetTitleText
    {
        get
        {
            return this._actSetTitleText;
        }
        set
        {
            if ((this._actSetTitleText == value))
            {
                return;
            }
            if (((this._actSetTitleText == null) 
                        || (_actSetTitleText.Equals(value) != true)))
            {
                this._actSetTitleText = value;
                this.OnPropertyChanged("actSetTitleText", value);
            }
        }
    }
    
    /// <summary>
    /// Set HTML as base-64-encoded binary
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="base64Binary")]
    public virtual byte[] actSetBase64HTML
    {
        get
        {
            return this._actSetBase64HTML;
        }
        set
        {
            if ((this._actSetBase64HTML == value))
            {
                return;
            }
            if (((this._actSetBase64HTML == null) 
                        || (_actSetBase64HTML.Equals(value) != true)))
            {
                this._actSetBase64HTML = value;
                this.OnPropertyChanged("actSetBase64HTML", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetCode
    {
        get
        {
            return this._actSetCode;
        }
        set
        {
            if ((this._actSetCode == value))
            {
                return;
            }
            if (((this._actSetCode == null) 
                        || (_actSetCode.Equals(value) != true)))
            {
                this._actSetCode = value;
                this.OnPropertyChanged("actSetCode", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetCodeSystem
    {
        get
        {
            return this._actSetCodeSystem;
        }
        set
        {
            if ((this._actSetCodeSystem == value))
            {
                return;
            }
            if (((this._actSetCodeSystem == null) 
                        || (_actSetCodeSystem.Equals(value) != true)))
            {
                this._actSetCodeSystem = value;
                this.OnPropertyChanged("actSetCodeSystem", value);
            }
        }
    }
    
    /// <summary>
    /// Set a @val attribute with a supplied value. The correct
    /// data type must be used if applicable.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetVal
    {
        get
        {
            return this._actSetVal;
        }
        set
        {
            if ((this._actSetVal == value))
            {
                return;
            }
            if (((this._actSetVal == null) 
                        || (_actSetVal.Equals(value) != true)))
            {
                this._actSetVal = value;
                this.OnPropertyChanged("actSetVal", value);
            }
        }
    }
    
    /// <summary>
    /// Set @associatedValue attribute of a ListItem with a
    /// supplied value. The correct data type must be used if
    /// applicable.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetAssociatedValue
    {
        get
        {
            return this._actSetAssociatedValue;
        }
        set
        {
            if ((this._actSetAssociatedValue == value))
            {
                return;
            }
            if (((this._actSetAssociatedValue == null) 
                        || (_actSetAssociatedValue.Equals(value) != true)))
            {
                this._actSetAssociatedValue = value;
                this.OnPropertyChanged("actSetAssociatedValue", value);
            }
        }
    }
    
    /// <summary>
    /// Set a @val attribute. The correct data type must be used
    /// if applicable. Supply the @name of an element that has a non-null @val
    /// value of the correct datatype. Null values are
    /// ignored.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetValFromRef
    {
        get
        {
            return this._actSetValFromRef;
        }
        set
        {
            if ((this._actSetValFromRef == value))
            {
                return;
            }
            if (((this._actSetValFromRef == null) 
                        || (_actSetValFromRef.Equals(value) != true)))
            {
                this._actSetValFromRef = value;
                this.OnPropertyChanged("actSetValFromRef", value);
            }
        }
    }
    
    /// <summary>
    /// Set @associatedValue attribute of a ListItem. The correct
    /// data type must be used if applicable. Supply the @name of an element
    /// that has a non-null @val value of the correct datatype. Null values are
    /// ignored.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string actSetAssociatedValueFromRef
    {
        get
        {
            return this._actSetAssociatedValueFromRef;
        }
        set
        {
            if ((this._actSetAssociatedValueFromRef == value))
            {
                return;
            }
            if (((this._actSetAssociatedValueFromRef == null) 
                        || (_actSetAssociatedValueFromRef.Equals(value) != true)))
            {
                this._actSetAssociatedValueFromRef = value;
                this.OnPropertyChanged("actSetAssociatedValueFromRef", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether actVisible should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactVisible()
    {
        if (_shouldSerializeactVisible)
        {
            return true;
        }
        return (_actVisible != default(bool));
    }
    
    /// <summary>
    /// Test whether actEnable should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactEnable()
    {
        if (_shouldSerializeactEnable)
        {
            return true;
        }
        return (_actEnable != default(bool));
    }
    
    /// <summary>
    /// Test whether actActivate should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactActivate()
    {
        if (_shouldSerializeactActivate)
        {
            return true;
        }
        return (_actActivate != default(bool));
    }
    
    /// <summary>
    /// Test whether actSelect should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSelect()
    {
        if (_shouldSerializeactSelect)
        {
            return true;
        }
        return (_actSelect != default(bool));
    }
    
    /// <summary>
    /// Test whether actDeleteResponse should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactDeleteResponse()
    {
        if (_shouldSerializeactDeleteResponse)
        {
            return true;
        }
        return (_actDeleteResponse != default(bool));
    }
    
    /// <summary>
    /// Test whether actReadOnly should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactReadOnly()
    {
        if (_shouldSerializeactReadOnly)
        {
            return true;
        }
        return (_actReadOnly != default(bool));
    }
    
    /// <summary>
    /// Test whether targetNames should be serialized
    /// </summary>
    public virtual bool ShouldSerializetargetNames()
    {
        return !string.IsNullOrEmpty(targetNames);
    }
    
    /// <summary>
    /// Test whether actMinCard should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactMinCard()
    {
        return !string.IsNullOrEmpty(actMinCard);
    }
    
    /// <summary>
    /// Test whether actMaxCard should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactMaxCard()
    {
        return !string.IsNullOrEmpty(actMaxCard);
    }
    
    /// <summary>
    /// Test whether actType should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactType()
    {
        return !string.IsNullOrEmpty(actType);
    }
    
    /// <summary>
    /// Test whether actStyleClass should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactStyleClass()
    {
        return !string.IsNullOrEmpty(actStyleClass);
    }
    
    /// <summary>
    /// Test whether actSetTitleText should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetTitleText()
    {
        return !string.IsNullOrEmpty(actSetTitleText);
    }
    
    /// <summary>
    /// Test whether actSetCode should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetCode()
    {
        return !string.IsNullOrEmpty(actSetCode);
    }
    
    /// <summary>
    /// Test whether actSetCodeSystem should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetCodeSystem()
    {
        return !string.IsNullOrEmpty(actSetCodeSystem);
    }
    
    /// <summary>
    /// Test whether actSetVal should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetVal()
    {
        return !string.IsNullOrEmpty(actSetVal);
    }
    
    /// <summary>
    /// Test whether actSetAssociatedValue should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetAssociatedValue()
    {
        return !string.IsNullOrEmpty(actSetAssociatedValue);
    }
    
    /// <summary>
    /// Test whether actSetValFromRef should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetValFromRef()
    {
        return !string.IsNullOrEmpty(actSetValFromRef);
    }
    
    /// <summary>
    /// Test whether actSetAssociatedValueFromRef should be serialized
    /// </summary>
    public virtual bool ShouldSerializeactSetAssociatedValueFromRef()
    {
        return !string.IsNullOrEmpty(actSetAssociatedValueFromRef);
    }
}
}
#pragma warning restore

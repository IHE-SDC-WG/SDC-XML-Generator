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
/// Function or web service that returns a Boolean value.  Items that inherit from this class must test the result for being a Boolean true/false value or null.
/// </summary>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PredAlternativesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PredGuardType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PredMultiSelectionSetBoolType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(MultiSelectionsActionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PredEvalAttribValuesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttributeEvalActionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PredActionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EventType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(OnEventType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PredSingleSelectionSetsType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SelectionSetsActionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PredSelectionTestType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(SelectionTestActionType))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public abstract partial class FuncBoolBaseType : ExtensionBaseType
{
    
    #region Private fields
    private bool _shouldSerializereturnVal;
    
    private bool _shouldSerializeallowNull;
    
    private bool _allowNull;
    
    private string _validationMessage;
    
    private bool _returnVal;
    #endregion
    
    ///// <summary>
    ///// FuncBoolBaseType class constructor
    ///// </summary>
    //public FuncBoolBaseType()
    //{
    //    this._allowNull = true;
    //}
    
    /// <summary>
    /// True means that null values are allowed in @returnVal.  This corresponds to an empty string in @val or a missing @returnVal attribute.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(true)]
    public virtual bool allowNull
    {
        get
        {
            return this._allowNull;
        }
        set
        {
            if ((_allowNull.Equals(value) != true))
            {
                this._allowNull = value;
                this.OnPropertyChanged("allowNull", value);
            }
            _shouldSerializeallowNull = true;
        }
    }
    
    /// <summary>
    /// Optional message that appears when the rule evaluates to true
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string validationMessage
    {
        get
        {
            return this._validationMessage;
        }
        set
        {
            if ((this._validationMessage == value))
            {
                return;
            }
            if (((this._validationMessage == null) 
                        || (_validationMessage.Equals(value) != true)))
            {
                this._validationMessage = value;
                this.OnPropertyChanged("validationMessage", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool returnVal
    {
        get
        {
            return this._returnVal;
        }
        set
        {
            if ((_returnVal.Equals(value) != true))
            {
                this._returnVal = value;
                this.OnPropertyChanged("returnVal", value);
            }
            _shouldSerializereturnVal = true;
        }
    }
    
    /// <summary>
    /// Test whether allowNull should be serialized
    /// </summary>
    public virtual bool ShouldSerializeallowNull()
    {
        if (_shouldSerializeallowNull)
        {
            return true;
        }
        return (_allowNull != default(bool));
    }
    
    /// <summary>
    /// Test whether returnVal should be serialized
    /// </summary>
    public virtual bool ShouldSerializereturnVal()
    {
        if (_shouldSerializereturnVal)
        {
            return true;
        }
        return (_returnVal != default(bool));
    }
    
    /// <summary>
    /// Test whether validationMessage should be serialized
    /// </summary>
    public virtual bool ShouldSerializevalidationMessage()
    {
        return !string.IsNullOrEmpty(validationMessage);
    }
}
}
#pragma warning restore

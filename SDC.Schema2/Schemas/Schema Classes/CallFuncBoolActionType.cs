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
/// Function or web service that returns a string
/// value.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class CallFuncBoolActionType : CallFuncBoolType
{
    
    #region Private fields
    private ExtensionBaseType[] _items1;
    
    private Items1ChoiceType[] _items1ElementName;
    #endregion
    
    [System.Xml.Serialization.XmlElementAttribute("Actions", typeof(ActionsType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ConditionalActions", typeof(PredActionType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("Else", typeof(PredActionType), Order=0)]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("Items1ElementName")]
    public virtual ExtensionBaseType[] Items1
    {
        get
        {
            return this._items1;
        }
        set
        {
            if ((this._items1 == value))
            {
                return;
            }
            if (((this._items1 == null) 
                        || (_items1.Equals(value) != true)))
            {
                this._items1 = value;
                this.OnPropertyChanged("Items1", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute("Items1ElementName", Order=1)]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual Items1ChoiceType[] Items1ElementName
    {
        get
        {
            return this._items1ElementName;
        }
        set
        {
            if ((this._items1ElementName == value))
            {
                return;
            }
            if (((this._items1ElementName == null) 
                        || (_items1ElementName.Equals(value) != true)))
            {
                this._items1ElementName = value;
                this.OnPropertyChanged("Items1ElementName", value);
            }
        }
    }
}
}
#pragma warning restore
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
public partial class SelectionTestActionType : PredSelectionTestType
{
    
    #region Private fields
    private ActionsType _actions;
    
    private List<PredActionType> _else;
    #endregion
    
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public virtual ActionsType Actions
    {
        get
        {
            return this._actions;
        }
        set
        {
            if ((this._actions == value))
            {
                return;
            }
            if (((this._actions == null) 
                        || (_actions.Equals(value) != true)))
            {
                this._actions = value;
                this.OnPropertyChanged("Actions", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute("Else", Order=1)]
    public virtual List<PredActionType> Else
    {
        get
        {
            return this._else;
        }
        set
        {
            if ((this._else == value))
            {
                return;
            }
            if (((this._else == null) 
                        || (_else.Equals(value) != true)))
            {
                this._else = value;
                this.OnPropertyChanged("Else", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether Else should be serialized
    /// </summary>
    public virtual bool ShouldSerializeElse()
    {
        return Else != null && Else.Count > 0;
    }
    
    /// <summary>
    /// Test whether Actions should be serialized
    /// </summary>
    public virtual bool ShouldSerializeActions()
    {
        return (_actions != null);
    }
}
}
#pragma warning restore

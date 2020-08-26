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
public partial class PredGuardType : FuncBoolBaseType
{
    
    #region Private fields
    private bool _shouldSerializeboolOp;
    
    private bool _shouldSerializenot;
    
    private List<ExtensionBaseType> _items;
    
    private bool _not;
    
    private PredEvalAttribValuesTypeBoolOp _boolOp;
    #endregion
    
    ///// <summary>
    ///// PredGuardType class constructor
    ///// </summary>
    //public PredGuardType()
    //{
    //    this._not = false;
    //    this._boolOp = PredEvalAttribValuesTypeBoolOp.AND;
    //}
    
    [System.Xml.Serialization.XmlElementAttribute("AttributeEval", typeof(PredEvalAttribValuesType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("CallBoolFunc", typeof(CallFuncBoolType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("Group", typeof(PredGuardType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ItemAlternatives", typeof(PredAlternativesType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("MultiSelections", typeof(PredMultiSelectionSetBoolType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ScriptBoolFunc", typeof(ScriptCodeBoolType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("SelectionSets", typeof(PredGuardTypeSelectionSets), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("SelectionTest", typeof(PredSelectionTestType), Order=0)]
    public virtual List<ExtensionBaseType> Items
    {
        get
        {
            return this._items;
        }
        set
        {
            if ((this._items == value))
            {
                return;
            }
            if (((this._items == null) 
                        || (_items.Equals(value) != true)))
            {
                this._items = value;
                this.OnPropertyChanged("Items", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(false)]
    public virtual bool not
    {
        get
        {
            return this._not;
        }
        set
        {
            if ((_not.Equals(value) != true))
            {
                this._not = value;
                this.OnPropertyChanged("not", value);
            }
            _shouldSerializenot = true;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(PredEvalAttribValuesTypeBoolOp.AND)]
    public virtual PredEvalAttribValuesTypeBoolOp boolOp
    {
        get
        {
            return this._boolOp;
        }
        set
        {
            if ((_boolOp.Equals(value) != true))
            {
                this._boolOp = value;
                this.OnPropertyChanged("boolOp", value);
            }
            _shouldSerializeboolOp = true;
        }
    }
    
    /// <summary>
    /// Test whether Items should be serialized
    /// </summary>
    public virtual bool ShouldSerializeItems()
    {
        return Items != null && Items.Count > 0;
    }
    
    /// <summary>
    /// Test whether not should be serialized
    /// </summary>
    public virtual bool ShouldSerializenot()
    {
        if (_shouldSerializenot)
        {
            return true;
        }
        return (_not != default(bool));
    }
    
    /// <summary>
    /// Test whether boolOp should be serialized
    /// </summary>
    public virtual bool ShouldSerializeboolOp()
    {
        if (_shouldSerializeboolOp)
        {
            return true;
        }
        return (_boolOp != default(PredEvalAttribValuesTypeBoolOp));
    }
}
}
#pragma warning restore

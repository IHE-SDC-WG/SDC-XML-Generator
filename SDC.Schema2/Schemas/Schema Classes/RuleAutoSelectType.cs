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
/// This Rule selects/unselects ListItems based on the selected status of
/// other ListItems. This declarative rule determines (guards) when target ListItems
/// should be selected or unselected. The guard rule may optionally select/unselect
/// multiple target ListItems with a single rule. The target ListItem(s) to
/// select/unselect are listed in targetNameSelectList. In the simplest case, this rule
/// operates as follows: A list of ListItems is provided (selectedItemWatchList). If all
/// the items in the list are selected (or unselected - see below) as specified in the
/// selectedItemWatchList list, then the guard evaluates to true, and the
/// targetNameSelectList items are selected. In some cases, we may wish to watch
/// unselected items in the selectedItemWatchList. This is indicated by prefixing the
/// name of the watched item with a minus sign/dash ("-"). In some cases, we may wish to
/// unselect items in the targetNameSelectList list when the selectedItemWatchList
/// evaluated to true. In this case, the target item is prefixed with a dash ("-").
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class RuleAutoSelectType : ExtensionBaseType
{
    
    #region Private fields
    private bool _shouldSerializeonlyIf;
    
    private string _selectedItemSet;
    
    private bool _onlyIf;
    
    private string _targetNameSelectList;
    #endregion
    
    /// <summary>
    /// RuleAutoSelectType class constructor
    /// </summary>
    public RuleAutoSelectType()
    {
        this._onlyIf = false;
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NMTOKENS")]
    public virtual string selectedItemSet
    {
        get
        {
            return this._selectedItemSet;
        }
        set
        {
            if ((this._selectedItemSet == value))
            {
                return;
            }
            if (((this._selectedItemSet == null) 
                        || (_selectedItemSet.Equals(value) != true)))
            {
                this._selectedItemSet = value;
                this.OnPropertyChanged("selectedItemSet", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(false)]
    public virtual bool onlyIf
    {
        get
        {
            return this._onlyIf;
        }
        set
        {
            if ((_onlyIf.Equals(value) != true))
            {
                this._onlyIf = value;
                this.OnPropertyChanged("onlyIf", value);
            }
            _shouldSerializeonlyIf = true;
        }
    }
    
    /// <summary>
    /// This list contains the @names of ListItems that will be
    /// automatically selected or deselected when the @selectedItemSet evaluates
    /// to true. If a @name is prefixed with a hyphen (-), then the item will be
    /// deselected when @selectedItemSet evaluates to true. If @not = true, then
    /// the Boolean rule evaluation is negated, and thus the rule works in
    /// reverse. If @onlyIf is true, then the above rule is reversed when
    /// @selectedItemSet evaluates to false. In other words, named items will be
    /// deselected, and hyphen-prefixed items will be selected when
    /// @selectedItemSet is false.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="NCName")]
    public virtual string targetNameSelectList
    {
        get
        {
            return this._targetNameSelectList;
        }
        set
        {
            if ((this._targetNameSelectList == value))
            {
                return;
            }
            if (((this._targetNameSelectList == null) 
                        || (_targetNameSelectList.Equals(value) != true)))
            {
                this._targetNameSelectList = value;
                this.OnPropertyChanged("targetNameSelectList", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether onlyIf should be serialized
    /// </summary>
    public virtual bool ShouldSerializeonlyIf()
    {
        if (_shouldSerializeonlyIf)
        {
            return true;
        }
        return (_onlyIf != default(bool));
    }
    
    /// <summary>
    /// Test whether selectedItemSet should be serialized
    /// </summary>
    public virtual bool ShouldSerializeselectedItemSet()
    {
        return !string.IsNullOrEmpty(selectedItemSet);
    }
    
    /// <summary>
    /// Test whether targetNameSelectList should be serialized
    /// </summary>
    public virtual bool ShouldSerializetargetNameSelectList()
    {
        return !string.IsNullOrEmpty(targetNameSelectList);
    }
}
}
#pragma warning restore

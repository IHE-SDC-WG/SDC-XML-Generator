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
/// Rules are procedures that affect form behaviour. They are usually
/// triggered by changes to the form by a form user. They may also be triggered by other
/// rules or changes to form metadata (e.g., change to @visible). SDC decarative form
/// rules consist of several types: Assertions are "predicate" expressions that evaulate
/// to true or false, based upon the state of answers/responses or metadata within a
/// form. An asserion is equivalent to the "If" part of an If-Then block. The
/// implementation framework must assign a fucntion to handle any assertion that
/// evaluates to False. Assertions are no handled by the form itself. If-Then-Else
/// blocks run a "Then" action if the "If block evaluated to true, and an "Else" block
/// if the "If" block evaluates to false. An AutoSelection rule automatically selects an
/// answer choice (ListItem) when another answer (or set of answers) is selected or
/// unselected. IllegalSelectionTest evaluates to true if a disallowed set of answer
/// choices is selected at the same time. The implementation should take an action
/// (alert user, cancel inout, etc) when a true result occurs. IllegalSelection Set
/// restricts combinations of co-selected ListItems (answers). In other words, only zero
/// or one item in this list may be selected at a time.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class RulesType : ExtensionBaseType
{
    
    #region Private fields
    private List<ExtensionBaseType> _items;
    #endregion
    
    [System.Xml.Serialization.XmlElementAttribute("AutoActivation", typeof(RuleAutoActivateType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("AutoSelection", typeof(RuleAutoSelectType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ConditionalActions", typeof(PredActionType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ExternalRule", typeof(CallFuncActionType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ScriptedRule", typeof(ScriptCodeAnyType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("SelectMatchingListItems", typeof(RuleSelectMatchingListItemsType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("Validation", typeof(ValidationType), Order=0)]
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
    
    /// <summary>
    /// Test whether Items should be serialized
    /// </summary>
    public virtual bool ShouldSerializeItems()
    {
        return Items != null && Items.Count > 0;
    }
}
}
#pragma warning restore

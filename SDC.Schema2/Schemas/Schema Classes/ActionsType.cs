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
public partial class ActionsType : ExtensionBaseType
{
    
    #region Private fields
    private ExtensionBaseType[] _items;
    
    private ItemsChoiceType1[] _itemsElementName;
    #endregion
    
    [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActActionType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("AddCode", typeof(ActAddCodeType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("CallFunction", typeof(CallFuncActionType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ConditionalGroupAction", typeof(PredActionType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("Inject", typeof(ActInjectType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("PreviewReport", typeof(ActPreviewReportType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("RunCode", typeof(ScriptCodeAnyType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("Save", typeof(ActSaveResponsesType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("SelectMatchingListItems", typeof(RuleSelectMatchingListItemsType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("SendMessage111", typeof(ActSendMessageType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("SendReport", typeof(ActSendReportType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("SetAttributeValue", typeof(ActSetAttributeType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("SetAttributeValueScript", typeof(ActSetAttrValueScriptType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("SetBoolAttributeValueCode", typeof(ActSetBoolAttributeValueCodeType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ShowForm", typeof(ActShowFormType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ShowMessage", typeof(ActShowMessageType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ShowReport", typeof(ActShowReportType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ShowURL", typeof(CallFuncActionType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("ValidateForm", typeof(ActValidateFormType), Order=0)]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public virtual ExtensionBaseType[] Items
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
    
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order=1)]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public virtual ItemsChoiceType1[] ItemsElementName
    {
        get
        {
            return this._itemsElementName;
        }
        set
        {
            if ((this._itemsElementName == value))
            {
                return;
            }
            if (((this._itemsElementName == null) 
                        || (_itemsElementName.Equals(value) != true)))
            {
                this._itemsElementName = value;
                this.OnPropertyChanged("ItemsElementName", value);
            }
        }
    }
}
}
#pragma warning restore
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
public partial class TemplateTargetType : ExtensionBaseType
{
    
    #region Private fields
    private anyURI_Stype _targetItemID;
    
    private RichTextType _targetDisplayText;
    #endregion
    
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public virtual anyURI_Stype TargetItemID
    {
        get
        {
            return this._targetItemID;
        }
        set
        {
            if ((this._targetItemID == value))
            {
                return;
            }
            if (((this._targetItemID == null) 
                        || (_targetItemID.Equals(value) != true)))
            {
                this._targetItemID = value;
                this.OnPropertyChanged("TargetItemID", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public virtual RichTextType TargetDisplayText
    {
        get
        {
            return this._targetDisplayText;
        }
        set
        {
            if ((this._targetDisplayText == value))
            {
                return;
            }
            if (((this._targetDisplayText == null) 
                        || (_targetDisplayText.Equals(value) != true)))
            {
                this._targetDisplayText = value;
                this.OnPropertyChanged("TargetDisplayText", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether TargetItemID should be serialized
    /// </summary>
    public virtual bool ShouldSerializeTargetItemID()
    {
        return (_targetItemID != null);
    }
    
    /// <summary>
    /// Test whether TargetDisplayText should be serialized
    /// </summary>
    public virtual bool ShouldSerializeTargetDisplayText()
    {
        return (_targetDisplayText != null);
    }
}
}
#pragma warning restore

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
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:ihe:qrph:sdc:2016")]
public partial class ValidationTypeSelectionTest : PredSelectionTestType
{
    
    #region Private fields
    private bool _shouldSerializenot;
    
    private bool _not;
    #endregion
    
    /// <summary>
    /// ValidationTypeSelectionTest class constructor
    /// </summary>
    public ValidationTypeSelectionTest()
    {
        this._not = false;
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
}
}
#pragma warning restore

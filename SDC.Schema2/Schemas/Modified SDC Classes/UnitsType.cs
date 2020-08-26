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
/// UnitsType represents the measurement standard and its abbreviated notation for quantifiable objects, e.g., miles, km, mm, cm, etc. The default system for standard notations is UCUM.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class UnitsType : string_Stype
{
    
    #region Private fields
    private string _unitSystem;
    #endregion
    
    ///// <summary>
    ///// UnitsType class constructor
    ///// </summary>
    //public UnitsType()
    //{
    //    this._unitSystem = "UCUM";
    //}
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute("UCUM")]
    public virtual string unitSystem
    {
        get
        {
            return this._unitSystem;
        }
        set
        {
            if ((this._unitSystem == value))
            {
                return;
            }
            if (((this._unitSystem == null) 
                        || (_unitSystem.Equals(value) != true)))
            {
                this._unitSystem = value;
                this.OnPropertyChanged("unitSystem", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether unitSystem should be serialized
    /// </summary>
    public virtual bool ShouldSerializeunitSystem()
    {
        return !string.IsNullOrEmpty(unitSystem);
    }
}
}
#pragma warning restore

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
public partial class FileUsageType : ExtensionBaseType
{
    
    #region Private fields
    private List<CodingType> _included;
    
    private List<CodingType> _excluded;
    
    private List<string_Stype> _description;
    #endregion
    
    /// <summary>
    /// Reasons to use the file
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute("Included", Order=0)]
    public virtual List<CodingType> Included
    {
        get
        {
            return this._included;
        }
        set
        {
            if ((this._included == value))
            {
                return;
            }
            if (((this._included == null) 
                        || (_included.Equals(value) != true)))
            {
                this._included = value;
                this.OnPropertyChanged("Included", value);
            }
        }
    }
    
    /// <summary>
    /// Reasons to not use the file
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute("Excluded", Order=1)]
    public virtual List<CodingType> Excluded
    {
        get
        {
            return this._excluded;
        }
        set
        {
            if ((this._excluded == value))
            {
                return;
            }
            if (((this._excluded == null) 
                        || (_excluded.Equals(value) != true)))
            {
                this._excluded = value;
                this.OnPropertyChanged("Excluded", value);
            }
        }
    }
    
    /// <summary>
    /// Non-coded text describing usage criteria.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute("Description", Order=2)]
    public virtual List<string_Stype> Description
    {
        get
        {
            return this._description;
        }
        set
        {
            if ((this._description == value))
            {
                return;
            }
            if (((this._description == null) 
                        || (_description.Equals(value) != true)))
            {
                this._description = value;
                this.OnPropertyChanged("Description", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether Included should be serialized
    /// </summary>
    public virtual bool ShouldSerializeIncluded()
    {
        return Included != null && Included.Count > 0;
    }
    
    /// <summary>
    /// Test whether Excluded should be serialized
    /// </summary>
    public virtual bool ShouldSerializeExcluded()
    {
        return Excluded != null && Excluded.Count > 0;
    }
    
    /// <summary>
    /// Test whether Description should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDescription()
    {
        return Description != null && Description.Count > 0;
    }
}
}
#pragma warning restore

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
public partial class SQL_Type : string_Stype
{
    
    #region Private fields
    private string _sqlDialect;
    
    private string _sqlDialectVersion;
    #endregion
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string sqlDialect
    {
        get
        {
            return this._sqlDialect;
        }
        set
        {
            if ((this._sqlDialect == value))
            {
                return;
            }
            if (((this._sqlDialect == null) 
                        || (_sqlDialect.Equals(value) != true)))
            {
                this._sqlDialect = value;
                this.OnPropertyChanged("sqlDialect", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string sqlDialectVersion
    {
        get
        {
            return this._sqlDialectVersion;
        }
        set
        {
            if ((this._sqlDialectVersion == value))
            {
                return;
            }
            if (((this._sqlDialectVersion == null) 
                        || (_sqlDialectVersion.Equals(value) != true)))
            {
                this._sqlDialectVersion = value;
                this.OnPropertyChanged("sqlDialectVersion", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether sqlDialect should be serialized
    /// </summary>
    public virtual bool ShouldSerializesqlDialect()
    {
        return !string.IsNullOrEmpty(sqlDialect);
    }
    
    /// <summary>
    /// Test whether sqlDialectVersion should be serialized
    /// </summary>
    public virtual bool ShouldSerializesqlDialectVersion()
    {
        return !string.IsNullOrEmpty(sqlDialectVersion);
    }
}
}
#pragma warning restore

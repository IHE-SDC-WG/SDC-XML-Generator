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
public partial class ItemMapType : ExtensionBaseType
{
    
    #region Private fields
    private TemplateTargetType _templateTarget;
    
    private DataSourceType _dataSource;
    
    private List<RichTextType> _mapComment;
    #endregion
    
    /// <summary>
    /// Target item in a FormDesignTemplate.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public virtual TemplateTargetType TemplateTarget
    {
        get
        {
            return this._templateTarget;
        }
        set
        {
            if ((this._templateTarget == value))
            {
                return;
            }
            if (((this._templateTarget == null) 
                        || (_templateTarget.Equals(value) != true)))
            {
                this._templateTarget = value;
                this.OnPropertyChanged("TemplateTarget", value);
            }
        }
    }
    
    /// <summary>
    /// The DataSource is an object that maps to a target item in a FormDesignTemplate.  DataSources objects can include terminology codes, local values, XML-based document entries, database records, RDF store triples, etc.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public virtual DataSourceType DataSource
    {
        get
        {
            return this._dataSource;
        }
        set
        {
            if ((this._dataSource == value))
            {
                return;
            }
            if (((this._dataSource == null) 
                        || (_dataSource.Equals(value) != true)))
            {
                this._dataSource = value;
                this.OnPropertyChanged("DataSource", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute("MapComment", Order=2)]
    public virtual List<RichTextType> MapComment
    {
        get
        {
            return this._mapComment;
        }
        set
        {
            if ((this._mapComment == value))
            {
                return;
            }
            if (((this._mapComment == null) 
                        || (_mapComment.Equals(value) != true)))
            {
                this._mapComment = value;
                this.OnPropertyChanged("MapComment", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether MapComment should be serialized
    /// </summary>
    public virtual bool ShouldSerializeMapComment()
    {
        return MapComment != null && MapComment.Count > 0;
    }
    
    /// <summary>
    /// Test whether TemplateTarget should be serialized
    /// </summary>
    public virtual bool ShouldSerializeTemplateTarget()
    {
        return (_templateTarget != null);
    }
    
    /// <summary>
    /// Test whether DataSource should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDataSource()
    {
        return (_dataSource != null);
    }
}
}
#pragma warning restore

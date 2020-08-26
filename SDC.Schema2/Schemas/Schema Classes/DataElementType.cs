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
[System.Xml.Serialization.XmlRootAttribute("DataElement", Namespace="urn:ihe:qrph:sdc:2016", IsNullable=false)]
public partial class DataElementType : IdentifiedExtensionType
{
    
    #region Private fields
    private List<IdentifiedExtensionType> _items;
    
    private string _lineage;
    
    private string _version;
    
    private string _versionPrev;
    
    private string _fullURI;
    
    private string _basedOnURI;
    
    private string _filename;
    #endregion
    
    [System.Xml.Serialization.XmlElementAttribute("ButtonAction", typeof(ButtonItemType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("DisplayedItem", typeof(DisplayedType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("InjectForm", typeof(InjectFormType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("Question", typeof(QuestionItemType), Order=0)]
    [System.Xml.Serialization.XmlElementAttribute("Section", typeof(SectionItemType), Order=0)]
    public virtual List<IdentifiedExtensionType> Items
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
    /// NEW: A text identifier that is used to group multiple
    /// versions of a single DE. The lineage is constant for all versions of a
    /// single kind of DE. When appended to @baseURI, it can be used to retrieve
    /// all versions of one particular DE.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string lineage
    {
        get
        {
            return this._lineage;
        }
        set
        {
            if ((this._lineage == value))
            {
                return;
            }
            if (((this._lineage == null) 
                        || (_lineage.Equals(value) != true)))
            {
                this._lineage = value;
                this.OnPropertyChanged("lineage", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: @version contains the version text for the current
    /// DE. It is designed to be used in conjuction with @baseURI and
    /// @lineage.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string version
    {
        get
        {
            return this._version;
        }
        set
        {
            if ((this._version == value))
            {
                return;
            }
            if (((this._version == null) 
                        || (_version.Equals(value) != true)))
            {
                this._version = value;
                this.OnPropertyChanged("version", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: Identify the immediate previous version of the
    /// current DE. The format is the same as version. The primary role of this
    /// optional attribute is to allow automated comparisons between a current
    /// DE and the immediate previous DE version. This is often helpful when
    /// deciding whether to adopt a newer version of a DE.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string versionPrev
    {
        get
        {
            return this._versionPrev;
        }
        set
        {
            if ((this._versionPrev == value))
            {
                return;
            }
            if (((this._versionPrev == null) 
                        || (_versionPrev.Equals(value) != true)))
            {
                this._versionPrev = value;
                this.OnPropertyChanged("versionPrev", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: The full URI that uniquely identifies the current
    /// DE.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
    public virtual string fullURI
    {
        get
        {
            return this._fullURI;
        }
        set
        {
            if ((this._fullURI == value))
            {
                return;
            }
            if (((this._fullURI == null) 
                        || (_fullURI.Equals(value) != true)))
            {
                this._fullURI = value;
                this.OnPropertyChanged("fullURI", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: URI used to identify the DE that that this DE is
    /// based upon. In most cases, this should be a standard form that is
    /// modified and/or extended by the current DE. The current template reuses
    /// the basedOn IDs whenever the question/answer/semantic context is
    /// identical to the original.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
    public virtual string basedOnURI
    {
        get
        {
            return this._basedOnURI;
        }
        set
        {
            if ((this._basedOnURI == value))
            {
                return;
            }
            if (((this._basedOnURI == null) 
                        || (_basedOnURI.Equals(value) != true)))
            {
                this._basedOnURI = value;
                this.OnPropertyChanged("basedOnURI", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: filename to use when the current FormDesign instance
    /// is saved as a file. For forms containing responses, the filename may
    /// include the formInstanceVersionURI, but the naming convention may be
    /// use-case-specific.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string filename
    {
        get
        {
            return this._filename;
        }
        set
        {
            if ((this._filename == value))
            {
                return;
            }
            if (((this._filename == null) 
                        || (_filename.Equals(value) != true)))
            {
                this._filename = value;
                this.OnPropertyChanged("filename", value);
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
    
    /// <summary>
    /// Test whether lineage should be serialized
    /// </summary>
    public virtual bool ShouldSerializelineage()
    {
        return !string.IsNullOrEmpty(lineage);
    }
    
    /// <summary>
    /// Test whether version should be serialized
    /// </summary>
    public virtual bool ShouldSerializeversion()
    {
        return !string.IsNullOrEmpty(version);
    }
    
    /// <summary>
    /// Test whether versionPrev should be serialized
    /// </summary>
    public virtual bool ShouldSerializeversionPrev()
    {
        return !string.IsNullOrEmpty(versionPrev);
    }
    
    /// <summary>
    /// Test whether fullURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializefullURI()
    {
        return !string.IsNullOrEmpty(fullURI);
    }
    
    /// <summary>
    /// Test whether basedOnURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializebasedOnURI()
    {
        return !string.IsNullOrEmpty(basedOnURI);
    }
    
    /// <summary>
    /// Test whether filename should be serialized
    /// </summary>
    public virtual bool ShouldSerializefilename()
    {
        return !string.IsNullOrEmpty(filename);
    }
}
}
#pragma warning restore

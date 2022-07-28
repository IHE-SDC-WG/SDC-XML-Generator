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
/// A generic structure for recording file version metadata.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class VersionType : ExtensionBaseType
{
    
    #region Private fields
    private FileType _versioningReference;
    
    private RichTextType _versionComments;
    
    private VersionTypeChanges _changes;
    
    private string _fullVersion;
    
    private string _versionRegExPattern;
    
    private string _versionLevel1;
    
    private string _versionLevel2;
    
    private string _versionLevel3;
    
    private string _versionLevel4;
    
    private string _versionLevel5;
    #endregion
    
    /// <summary>
    /// Information about the document that describes the versioning methodology nomenclature.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public virtual FileType VersioningReference
    {
        get
        {
            return this._versioningReference;
        }
        set
        {
            if ((this._versioningReference == value))
            {
                return;
            }
            if (((this._versioningReference == null) 
                        || (_versioningReference.Equals(value) != true)))
            {
                this._versioningReference = value;
                this.OnPropertyChanged("VersioningReference", value);
            }
        }
    }
    
    /// <summary>
    /// Comments about the changes in this version
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public virtual RichTextType VersionComments
    {
        get
        {
            return this._versionComments;
        }
        set
        {
            if ((this._versionComments == value))
            {
                return;
            }
            if (((this._versionComments == null) 
                        || (_versionComments.Equals(value) != true)))
            {
                this._versionComments = value;
                this.OnPropertyChanged("VersionComments", value);
            }
        }
    }
    
    /// <summary>
    /// Itemized list of changes in the new version
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public virtual VersionTypeChanges Changes
    {
        get
        {
            return this._changes;
        }
        set
        {
            if ((this._changes == value))
            {
                return;
            }
            if (((this._changes == null) 
                        || (_changes.Equals(value) != true)))
            {
                this._changes = value;
                this.OnPropertyChanged("Changes", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string fullVersion
    {
        get
        {
            return this._fullVersion;
        }
        set
        {
            if ((this._fullVersion == value))
            {
                return;
            }
            if (((this._fullVersion == null) 
                        || (_fullVersion.Equals(value) != true)))
            {
                this._fullVersion = value;
                this.OnPropertyChanged("fullVersion", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string versionRegExPattern
    {
        get
        {
            return this._versionRegExPattern;
        }
        set
        {
            if ((this._versionRegExPattern == value))
            {
                return;
            }
            if (((this._versionRegExPattern == null) 
                        || (_versionRegExPattern.Equals(value) != true)))
            {
                this._versionRegExPattern = value;
                this.OnPropertyChanged("versionRegExPattern", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute("versionLevel.1")]
    public virtual string versionLevel1
    {
        get
        {
            return this._versionLevel1;
        }
        set
        {
            if ((this._versionLevel1 == value))
            {
                return;
            }
            if (((this._versionLevel1 == null) 
                        || (_versionLevel1.Equals(value) != true)))
            {
                this._versionLevel1 = value;
                this.OnPropertyChanged("versionLevel1", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute("versionLevel.2")]
    public virtual string versionLevel2
    {
        get
        {
            return this._versionLevel2;
        }
        set
        {
            if ((this._versionLevel2 == value))
            {
                return;
            }
            if (((this._versionLevel2 == null) 
                        || (_versionLevel2.Equals(value) != true)))
            {
                this._versionLevel2 = value;
                this.OnPropertyChanged("versionLevel2", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute("versionLevel.3")]
    public virtual string versionLevel3
    {
        get
        {
            return this._versionLevel3;
        }
        set
        {
            if ((this._versionLevel3 == value))
            {
                return;
            }
            if (((this._versionLevel3 == null) 
                        || (_versionLevel3.Equals(value) != true)))
            {
                this._versionLevel3 = value;
                this.OnPropertyChanged("versionLevel3", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute("versionLevel.4")]
    public virtual string versionLevel4
    {
        get
        {
            return this._versionLevel4;
        }
        set
        {
            if ((this._versionLevel4 == value))
            {
                return;
            }
            if (((this._versionLevel4 == null) 
                        || (_versionLevel4.Equals(value) != true)))
            {
                this._versionLevel4 = value;
                this.OnPropertyChanged("versionLevel4", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute("versionLevel.5")]
    public virtual string versionLevel5
    {
        get
        {
            return this._versionLevel5;
        }
        set
        {
            if ((this._versionLevel5 == value))
            {
                return;
            }
            if (((this._versionLevel5 == null) 
                        || (_versionLevel5.Equals(value) != true)))
            {
                this._versionLevel5 = value;
                this.OnPropertyChanged("versionLevel5", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether VersioningReference should be serialized
    /// </summary>
    public virtual bool ShouldSerializeVersioningReference()
    {
        return (_versioningReference != null);
    }
    
    /// <summary>
    /// Test whether VersionComments should be serialized
    /// </summary>
    public virtual bool ShouldSerializeVersionComments()
    {
        return (_versionComments != null);
    }
    
    /// <summary>
    /// Test whether Changes should be serialized
    /// </summary>
    public virtual bool ShouldSerializeChanges()
    {
        return (_changes != null);
    }
    
    /// <summary>
    /// Test whether fullVersion should be serialized
    /// </summary>
    public virtual bool ShouldSerializefullVersion()
    {
        return !string.IsNullOrEmpty(fullVersion);
    }
    
    /// <summary>
    /// Test whether versionRegExPattern should be serialized
    /// </summary>
    public virtual bool ShouldSerializeversionRegExPattern()
    {
        return !string.IsNullOrEmpty(versionRegExPattern);
    }
    
    /// <summary>
    /// Test whether versionLevel1 should be serialized
    /// </summary>
    public virtual bool ShouldSerializeversionLevel1()
    {
        return !string.IsNullOrEmpty(versionLevel1);
    }
    
    /// <summary>
    /// Test whether versionLevel2 should be serialized
    /// </summary>
    public virtual bool ShouldSerializeversionLevel2()
    {
        return !string.IsNullOrEmpty(versionLevel2);
    }
    
    /// <summary>
    /// Test whether versionLevel3 should be serialized
    /// </summary>
    public virtual bool ShouldSerializeversionLevel3()
    {
        return !string.IsNullOrEmpty(versionLevel3);
    }
    
    /// <summary>
    /// Test whether versionLevel4 should be serialized
    /// </summary>
    public virtual bool ShouldSerializeversionLevel4()
    {
        return !string.IsNullOrEmpty(versionLevel4);
    }
    
    /// <summary>
    /// Test whether versionLevel5 should be serialized
    /// </summary>
    public virtual bool ShouldSerializeversionLevel5()
    {
        return !string.IsNullOrEmpty(versionLevel5);
    }
}
}
#pragma warning restore
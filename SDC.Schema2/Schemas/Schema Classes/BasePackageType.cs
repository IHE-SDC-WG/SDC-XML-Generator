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

[System.Xml.Serialization.XmlIncludeAttribute(typeof(RetrieveFormPackageType))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class BasePackageType : ExtensionBaseType
{
    
    #region Private fields
    private bool _shouldSerializeX_completionStatus;
    
    private bool _shouldSerializeX_approvalStatus;
    
    private bool _shouldSerializenewData;
    
    private bool _shouldSerializechangedData;
    
    private bool _shouldSerializeinstanceVersionPrev;
    
    private bool _shouldSerializeinstanceVersion;
    
    private TemplateAdminType _admin;
    
    private string _packageID;
    
    private string _pkgTitle;
    
    private string _baseURI;
    
    private string _filename;
    
    private string _basedOnURI;
    
    private string _lineage;
    
    private string _version;
    
    private string _fullURI;
    
    private string _instanceID;
    
    private System.DateTime _instanceVersion;
    
    private string _instanceVersionURI;
    
    private System.DateTime _instanceVersionPrev;
    
    private string _x_pkgPreviousInstanceVersionURI;
    
    private string _x_prevVersionURI;
    
    private string _x_pkgInstanceURI;
    
    private string _x_pkgInstanceVersionURI;
    
    private BasePackageTypeX_approvalStatus _x_approvalStatus;
    
    private BasePackageTypeX_completionStatus _x_completionStatus;
    
    private bool _changedData;
    
    private bool _newData;
    #endregion
    
    /// <summary>
    /// Admin contains information about a package, including a description of the package contents and purpose (PackageDescription), information about the registry that contains the package file (RegistryData), and information about the package file characteristics (TemplateFile).
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public virtual TemplateAdminType Admin
    {
        get
        {
            return this._admin;
        }
        set
        {
            if ((this._admin == value))
            {
                return;
            }
            if (((this._admin == null) 
                        || (_admin.Equals(value) != true)))
            {
                this._admin = value;
                this.OnPropertyChanged("Admin", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
    public virtual string packageID
    {
        get
        {
            return this._packageID;
        }
        set
        {
            if ((this._packageID == value))
            {
                return;
            }
            if (((this._packageID == null) 
                        || (_packageID.Equals(value) != true)))
            {
                this._packageID = value;
                this.OnPropertyChanged("packageID", value);
            }
        }
    }
    
    /// <summary>
    /// NEW Feb 2019
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string pkgTitle
    {
        get
        {
            return this._pkgTitle;
        }
        set
        {
            if ((this._pkgTitle == value))
            {
                return;
            }
            if (((this._pkgTitle == null) 
                        || (_pkgTitle.Equals(value) != true)))
            {
                this._pkgTitle = value;
                this.OnPropertyChanged("pkgTitle", value);
            }
        }
    }
    
    /// <summary>
    /// @baseURI is required in the SDCPackage element but is optional elsewhere.  It identifies the organization that is responsible for designing and maintaining the Package contents.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
    public virtual string baseURI
    {
        get
        {
            return this._baseURI;
        }
        set
        {
            if ((this._baseURI == value))
            {
                return;
            }
            if (((this._baseURI == null) 
                        || (_baseURI.Equals(value) != true)))
            {
                this._baseURI = value;
                this.OnPropertyChanged("baseURI", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: filename to use when the current package instance is saved as a file.
    /// For package containing responses, the filename may include the pkgInstanceVersionURI,
    /// but the naming convention may be use-case-specific.
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
    /// NEW: URI used to identify the package that that this package is based upon.  In most cases, this should be a standard package that is modified and/or extended by the current package.
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
    /// NEW: A text identifier that is used to group multiple versions of a single package.  The lineage is constant for all versions of a single kind of package.
    /// 
    /// When appended to @baseURI, it can be used to retrieve all versions of one particular package.
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
    /// NEW: @version contains the version text for the current package.  It is designed to be used in conjunction with @baseURI and @lineage.
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
    /// NEW: The full URI that uniquely identifies the current package.
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
    /// NEW: Unique string used to identify a unique instance of a form. Used for tracking form responses across time and across multiple episodes of editing by end-users.  This string does not change for each edit session of a package instance.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual string instanceID
    {
        get
        {
            return this._instanceID;
        }
        set
        {
            if ((this._instanceID == value))
            {
                return;
            }
            if (((this._instanceID == null) 
                        || (_instanceID.Equals(value) != true)))
            {
                this._instanceID = value;
                this.OnPropertyChanged("instanceID", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: Timestamp used to identify a unique instance of a package.  Used for tracking form responses across time and across multiple episodes of editing by end-users.  This field must change for each edit session of a form instance.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual System.DateTime instanceVersion
    {
        get
        {
            return this._instanceVersion;
        }
        set
        {
            if ((_instanceVersion.Equals(value) != true))
            {
                this._instanceVersion = value;
                this.OnPropertyChanged("instanceVersion", value);
            }
            _shouldSerializeinstanceVersion = true;
        }
    }
    
    /// <summary>
    /// NEW: Globally-unique URI used to identify a unique instance of a Pkg with saved FDF-R responses.  It is used for tracking Pkg responses across time and across multiple episodes of editing by end-users.  The instanceVersionURI must change for each edit/save session of a Pkg instance (defined by instanceVersion).
    /// 
    /// The instanceVersionURI should be formatted similarly to the fullURI but must include values for instanceID and instanceVersion.  The instanceVersion value is the release date/time for the new version, in W3C datetime format.
    /// 
    /// An example instanceVersionURI is:
    /// instanceVersionURI="_baseURI=cap.org&_lineage=Lung.Bmk.227&_version=1.001.011.RC1 &_instanceID=Abc1dee2fg987&_instanceVersion=2019-07-16T19:20:30+01:00&_docType=sdcFDFR "
    /// 
    /// It is possible to create a shorter URI without the _baseURI, _lineage and _version parameters, as long as the URI is able to globally and uniquely identify and retrieve the instance and version of the Pkg that was transmitted:
    /// instanceVersionURI="_instanceID=Abc1dee2fg987&_instanceVersion=2019-07-16T19:20:30+01:00&_docType=sdcFDFR"
    /// 
    /// Note that the FR webservice endpoint URI is not provided in the instanceVersionURI.  The FR endpoint and its security settings may be found in the SDC Package that contains the FDF-R, at SDCPackage/SubmissionRule.  An FR may also be provided in a custom FDF Property if desired.
    /// 
    /// The docType for instanceVersionURI is sdcFDFR for a single FDF-R transaction.  The docType for for a Pkg with multiple FDF-R and/or other content is sdcPkg.  The specific order of components shown in the URI examples is not required, but the component order shown above is suggested for consistency and readability.
    /// 
    /// The instanceVersionURI is not required, and is not allowed in an FDF.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
    public virtual string instanceVersionURI
    {
        get
        {
            return this._instanceVersionURI;
        }
        set
        {
            if ((this._instanceVersionURI == value))
            {
                return;
            }
            if (((this._instanceVersionURI == null) 
                        || (_instanceVersionURI.Equals(value) != true)))
            {
                this._instanceVersionURI = value;
                this.OnPropertyChanged("instanceVersionURI", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: Unique dateTime used to identify the immediate previous instance of a package.  Used for tracking form responses across time and across multiple episodes of editing by end-users.  This field must change for each edit session of a form instance.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual System.DateTime instanceVersionPrev
    {
        get
        {
            return this._instanceVersionPrev;
        }
        set
        {
            if ((_instanceVersionPrev.Equals(value) != true))
            {
                this._instanceVersionPrev = value;
                this.OnPropertyChanged("instanceVersionPrev", value);
            }
            _shouldSerializeinstanceVersionPrev = true;
        }
    }
    
    /// <summary>
    /// NEW: Unique URI used to identify the immediate previous instance of a package containing responses.  This is the @pkgnstanceVersionURI that represents the instance of the package that the user opened up before beginning a new cycle of edit/save.  This attribute is used for tracking package responses across time and across multiple episodes of editing by end-users.  This URI must change for each edit session of a package instance.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
    public virtual string X_pkgPreviousInstanceVersionURI
    {
        get
        {
            return this._x_pkgPreviousInstanceVersionURI;
        }
        set
        {
            if ((this._x_pkgPreviousInstanceVersionURI == value))
            {
                return;
            }
            if (((this._x_pkgPreviousInstanceVersionURI == null) 
                        || (_x_pkgPreviousInstanceVersionURI.Equals(value) != true)))
            {
                this._x_pkgPreviousInstanceVersionURI = value;
                this.OnPropertyChanged("X_pkgPreviousInstanceVersionURI", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: The full URI used to identify the package that is the immediate previous version of the current package
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
    public virtual string X_prevVersionURI
    {
        get
        {
            return this._x_prevVersionURI;
        }
        set
        {
            if ((this._x_prevVersionURI == value))
            {
                return;
            }
            if (((this._x_prevVersionURI == null) 
                        || (_x_prevVersionURI.Equals(value) != true)))
            {
                this._x_prevVersionURI = value;
                this.OnPropertyChanged("X_prevVersionURI", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: Unique URI used to identify a unique instance of a package.  Used for tracking form responses across time and across multiple episodes of editing by end-users.  This URI does not change for each edit session of a package instance.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
    public virtual string X_pkgInstanceURI
    {
        get
        {
            return this._x_pkgInstanceURI;
        }
        set
        {
            if ((this._x_pkgInstanceURI == value))
            {
                return;
            }
            if (((this._x_pkgInstanceURI == null) 
                        || (_x_pkgInstanceURI.Equals(value) != true)))
            {
                this._x_pkgInstanceURI = value;
                this.OnPropertyChanged("X_pkgInstanceURI", value);
            }
        }
    }
    
    /// <summary>
    /// NEW: Unique URI used to identify a unique instance of a package's saved responses.  It is used for tracking package responses across time and across multiple episodes of editing by end-users.  This URI must change for each edit/save session of a package instance.  It may be e.g., a new GUID, or a repeat of the pkgInstanceID followed by a version number.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
    public virtual string X_pkgInstanceVersionURI
    {
        get
        {
            return this._x_pkgInstanceVersionURI;
        }
        set
        {
            if ((this._x_pkgInstanceVersionURI == value))
            {
                return;
            }
            if (((this._x_pkgInstanceVersionURI == null) 
                        || (_x_pkgInstanceVersionURI.Equals(value) != true)))
            {
                this._x_pkgInstanceVersionURI = value;
                this.OnPropertyChanged("X_pkgInstanceVersionURI", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual BasePackageTypeX_approvalStatus X_approvalStatus
    {
        get
        {
            return this._x_approvalStatus;
        }
        set
        {
            if ((_x_approvalStatus.Equals(value) != true))
            {
                this._x_approvalStatus = value;
                this.OnPropertyChanged("X_approvalStatus", value);
            }
            _shouldSerializeX_approvalStatus = true;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual BasePackageTypeX_completionStatus X_completionStatus
    {
        get
        {
            return this._x_completionStatus;
        }
        set
        {
            if ((_x_completionStatus.Equals(value) != true))
            {
                this._x_completionStatus = value;
                this.OnPropertyChanged("X_completionStatus", value);
            }
            _shouldSerializeX_completionStatus = true;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool changedData
    {
        get
        {
            return this._changedData;
        }
        set
        {
            if ((_changedData.Equals(value) != true))
            {
                this._changedData = value;
                this.OnPropertyChanged("changedData", value);
            }
            _shouldSerializechangedData = true;
        }
    }
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual bool newData
    {
        get
        {
            return this._newData;
        }
        set
        {
            if ((_newData.Equals(value) != true))
            {
                this._newData = value;
                this.OnPropertyChanged("newData", value);
            }
            _shouldSerializenewData = true;
        }
    }
    
    /// <summary>
    /// Test whether instanceVersion should be serialized
    /// </summary>
    public virtual bool ShouldSerializeinstanceVersion()
    {
        if (_shouldSerializeinstanceVersion)
        {
            return true;
        }
        return (_instanceVersion != default(System.DateTime));
    }
    
    /// <summary>
    /// Test whether instanceVersionPrev should be serialized
    /// </summary>
    public virtual bool ShouldSerializeinstanceVersionPrev()
    {
        if (_shouldSerializeinstanceVersionPrev)
        {
            return true;
        }
        return (_instanceVersionPrev != default(System.DateTime));
    }
    
    /// <summary>
    /// Test whether changedData should be serialized
    /// </summary>
    public virtual bool ShouldSerializechangedData()
    {
        if (_shouldSerializechangedData)
        {
            return true;
        }
        return (_changedData != default(bool));
    }
    
    /// <summary>
    /// Test whether newData should be serialized
    /// </summary>
    public virtual bool ShouldSerializenewData()
    {
        if (_shouldSerializenewData)
        {
            return true;
        }
        return (_newData != default(bool));
    }
    
    /// <summary>
    /// Test whether X_approvalStatus should be serialized
    /// </summary>
    public virtual bool ShouldSerializeX_approvalStatus()
    {
        if (_shouldSerializeX_approvalStatus)
        {
            return true;
        }
        return (_x_approvalStatus != default(BasePackageTypeX_approvalStatus));
    }
    
    /// <summary>
    /// Test whether X_completionStatus should be serialized
    /// </summary>
    public virtual bool ShouldSerializeX_completionStatus()
    {
        if (_shouldSerializeX_completionStatus)
        {
            return true;
        }
        return (_x_completionStatus != default(BasePackageTypeX_completionStatus));
    }
    
    /// <summary>
    /// Test whether Admin should be serialized
    /// </summary>
    public virtual bool ShouldSerializeAdmin()
    {
        return (_admin != null);
    }
    
    /// <summary>
    /// Test whether packageID should be serialized
    /// </summary>
    public virtual bool ShouldSerializepackageID()
    {
        return !string.IsNullOrEmpty(packageID);
    }
    
    /// <summary>
    /// Test whether pkgTitle should be serialized
    /// </summary>
    public virtual bool ShouldSerializepkgTitle()
    {
        return !string.IsNullOrEmpty(pkgTitle);
    }
    
    /// <summary>
    /// Test whether baseURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializebaseURI()
    {
        return !string.IsNullOrEmpty(baseURI);
    }
    
    /// <summary>
    /// Test whether filename should be serialized
    /// </summary>
    public virtual bool ShouldSerializefilename()
    {
        return !string.IsNullOrEmpty(filename);
    }
    
    /// <summary>
    /// Test whether basedOnURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializebasedOnURI()
    {
        return !string.IsNullOrEmpty(basedOnURI);
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
    /// Test whether fullURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializefullURI()
    {
        return !string.IsNullOrEmpty(fullURI);
    }
    
    /// <summary>
    /// Test whether instanceID should be serialized
    /// </summary>
    public virtual bool ShouldSerializeinstanceID()
    {
        return !string.IsNullOrEmpty(instanceID);
    }
    
    /// <summary>
    /// Test whether instanceVersionURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeinstanceVersionURI()
    {
        return !string.IsNullOrEmpty(instanceVersionURI);
    }
    
    /// <summary>
    /// Test whether X_pkgPreviousInstanceVersionURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeX_pkgPreviousInstanceVersionURI()
    {
        return !string.IsNullOrEmpty(X_pkgPreviousInstanceVersionURI);
    }
    
    /// <summary>
    /// Test whether X_prevVersionURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeX_prevVersionURI()
    {
        return !string.IsNullOrEmpty(X_prevVersionURI);
    }
    
    /// <summary>
    /// Test whether X_pkgInstanceURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeX_pkgInstanceURI()
    {
        return !string.IsNullOrEmpty(X_pkgInstanceURI);
    }
    
    /// <summary>
    /// Test whether X_pkgInstanceVersionURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeX_pkgInstanceVersionURI()
    {
        return !string.IsNullOrEmpty(X_pkgInstanceVersionURI);
    }
}
}
#pragma warning restore

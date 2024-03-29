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
/// This type represents information about the coding system used in CodingType. It may refer to any type of coding, terminology, classification, keyword, or local value system.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class CodeSystemType : ExtensionBaseType
{
    
    #region Private fields
    private string_Stype _codeSystemName;
    
    private dateTime_Stype _releaseDate;
    
    private string_Stype _version;
    
    private string_Stype _oID;
    
    private anyURI_Stype _codeSystemURI;
    #endregion
    
    /// <summary>
    /// The name of the coding system, as recommended by the
    /// coding system curators, or as recommended by the agency that creates
    /// standards for the code map in use.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public virtual string_Stype CodeSystemName
    {
        get
        {
            return this._codeSystemName;
        }
        set
        {
            if ((this._codeSystemName == value))
            {
                return;
            }
            if (((this._codeSystemName == null) 
                        || (_codeSystemName.Equals(value) != true)))
            {
                this._codeSystemName = value;
                this.OnPropertyChanged("CodeSystemName", value);
            }
        }
    }
    
    /// <summary>
    /// The day that the selected version of the coding system was released for general use by the coding system curators.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public virtual dateTime_Stype ReleaseDate
    {
        get
        {
            return this._releaseDate;
        }
        set
        {
            if ((this._releaseDate == value))
            {
                return;
            }
            if (((this._releaseDate == null) 
                        || (_releaseDate.Equals(value) != true)))
            {
                this._releaseDate = value;
                this.OnPropertyChanged("ReleaseDate", value);
            }
        }
    }
    
    /// <summary>
    /// Version of the coding system, using the version format defined by the coding system
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public virtual string_Stype Version
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
                this.OnPropertyChanged("Version", value);
            }
        }
    }
    
    /// <summary>
    /// The ISO object identifier (OID) for the coding system, as found at the HL7 OID Registry: https://www.hl7.org/oid/index.cfm
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=3)]
    public virtual string_Stype OID
    {
        get
        {
            return this._oID;
        }
        set
        {
            if ((this._oID == value))
            {
                return;
            }
            if (((this._oID == null) 
                        || (_oID.Equals(value) != true)))
            {
                this._oID = value;
                this.OnPropertyChanged("OID", value);
            }
        }
    }
    
    /// <summary>
    /// Web resource that uniquely identifies the coding system
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=4)]
    public virtual anyURI_Stype CodeSystemURI
    {
        get
        {
            return this._codeSystemURI;
        }
        set
        {
            if ((this._codeSystemURI == value))
            {
                return;
            }
            if (((this._codeSystemURI == null) 
                        || (_codeSystemURI.Equals(value) != true)))
            {
                this._codeSystemURI = value;
                this.OnPropertyChanged("CodeSystemURI", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether CodeSystemName should be serialized
    /// </summary>
    public virtual bool ShouldSerializeCodeSystemName()
    {
        return (_codeSystemName != null);
    }
    
    /// <summary>
    /// Test whether ReleaseDate should be serialized
    /// </summary>
    public virtual bool ShouldSerializeReleaseDate()
    {
        return (_releaseDate != null);
    }
    
    /// <summary>
    /// Test whether Version should be serialized
    /// </summary>
    public virtual bool ShouldSerializeVersion()
    {
        return (_version != null);
    }
    
    /// <summary>
    /// Test whether OID should be serialized
    /// </summary>
    public virtual bool ShouldSerializeOID()
    {
        return (_oID != null);
    }
    
    /// <summary>
    /// Test whether CodeSystemURI should be serialized
    /// </summary>
    public virtual bool ShouldSerializeCodeSystemURI()
    {
        return (_codeSystemURI != null);
    }
}
}
#pragma warning restore

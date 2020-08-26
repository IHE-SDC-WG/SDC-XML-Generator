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
/// Contains information about a registered package, including a description of the package contents and purpose (PackageDescription), information about the registry that contains the package XML (RegistryData), and information about the package file characteristics (TemplateFile).
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
[System.Xml.Serialization.XmlRootAttribute("TemplateAdmin", Namespace="urn:ihe:qrph:sdc:2016", IsNullable=false)]
public partial class TemplateAdminType : ExtensionBaseType
{
    
    #region Private fields
    private List<RichTextType> _packageDescription;
    
    private RegistryType _registryData;
    
    private FileType _templateFile;
    
    private List<TemplateAdminTypeDigitalSignature> _digitalSignature;
    #endregion
    
    /// <summary>
    /// Description of the XML package contents and the purpose for the contained XML templates.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute("PackageDescription", Order=0)]
    public virtual List<RichTextType> PackageDescription
    {
        get
        {
            return this._packageDescription;
        }
        set
        {
            if ((this._packageDescription == value))
            {
                return;
            }
            if (((this._packageDescription == null) 
                        || (_packageDescription.Equals(value) != true)))
            {
                this._packageDescription = value;
                this.OnPropertyChanged("PackageDescription", value);
            }
        }
    }
    
    /// <summary>
    /// Information about the registry that contains the XML template, and registration status of the XML template. (The XML template may contain a package of sub-templates.  In this case, the the RegistryData refers primarily to the package, not the sub-templates.)
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public virtual RegistryType RegistryData
    {
        get
        {
            return this._registryData;
        }
        set
        {
            if ((this._registryData == value))
            {
                return;
            }
            if (((this._registryData == null) 
                        || (_registryData.Equals(value) != true)))
            {
                this._registryData = value;
                this.OnPropertyChanged("RegistryData", value);
            }
        }
    }
    
    /// <summary>
    /// Information about the  XML template's file characteristics (The XML template may contain a package of sub-templates.  In this case, the the RegistryData refers primarily to the entire package, not the sub-templates.)
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public virtual FileType TemplateFile
    {
        get
        {
            return this._templateFile;
        }
        set
        {
            if ((this._templateFile == value))
            {
                return;
            }
            if (((this._templateFile == null) 
                        || (_templateFile.Equals(value) != true)))
            {
                this._templateFile = value;
                this.OnPropertyChanged("TemplateFile", value);
            }
        }
    }
    
    /// <summary>
    /// NEW
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute("DigitalSignature", Order=3)]
    public virtual List<TemplateAdminTypeDigitalSignature> DigitalSignature
    {
        get
        {
            return this._digitalSignature;
        }
        set
        {
            if ((this._digitalSignature == value))
            {
                return;
            }
            if (((this._digitalSignature == null) 
                        || (_digitalSignature.Equals(value) != true)))
            {
                this._digitalSignature = value;
                this.OnPropertyChanged("DigitalSignature", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether PackageDescription should be serialized
    /// </summary>
    public virtual bool ShouldSerializePackageDescription()
    {
        return PackageDescription != null && PackageDescription.Count > 0;
    }
    
    /// <summary>
    /// Test whether DigitalSignature should be serialized
    /// </summary>
    public virtual bool ShouldSerializeDigitalSignature()
    {
        return DigitalSignature != null && DigitalSignature.Count > 0;
    }
    
    /// <summary>
    /// Test whether RegistryData should be serialized
    /// </summary>
    public virtual bool ShouldSerializeRegistryData()
    {
        return (_registryData != null);
    }
    
    /// <summary>
    /// Test whether TemplateFile should be serialized
    /// </summary>
    public virtual bool ShouldSerializeTemplateFile()
    {
        return (_templateFile != null);
    }
}
}
#pragma warning restore

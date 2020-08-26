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
/// This type provides data about the current and original registries that host the template or package, and also provides information about the status of the template/package within the current registry.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class RegistryType : ExtensionBaseType
{
    
    #region Private fields
    private RegistrySummaryType _originalRegistry;
    
    private RegistrySummaryType _currentRegistry;
    
    private RegisteredItemType _registrationStatus;
    #endregion
    
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public virtual RegistrySummaryType OriginalRegistry
    {
        get
        {
            return this._originalRegistry;
        }
        set
        {
            if ((this._originalRegistry == value))
            {
                return;
            }
            if (((this._originalRegistry == null) 
                        || (_originalRegistry.Equals(value) != true)))
            {
                this._originalRegistry = value;
                this.OnPropertyChanged("OriginalRegistry", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public virtual RegistrySummaryType CurrentRegistry
    {
        get
        {
            return this._currentRegistry;
        }
        set
        {
            if ((this._currentRegistry == value))
            {
                return;
            }
            if (((this._currentRegistry == null) 
                        || (_currentRegistry.Equals(value) != true)))
            {
                this._currentRegistry = value;
                this.OnPropertyChanged("CurrentRegistry", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public virtual RegisteredItemType RegistrationStatus
    {
        get
        {
            return this._registrationStatus;
        }
        set
        {
            if ((this._registrationStatus == value))
            {
                return;
            }
            if (((this._registrationStatus == null) 
                        || (_registrationStatus.Equals(value) != true)))
            {
                this._registrationStatus = value;
                this.OnPropertyChanged("RegistrationStatus", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether OriginalRegistry should be serialized
    /// </summary>
    public virtual bool ShouldSerializeOriginalRegistry()
    {
        return (_originalRegistry != null);
    }
    
    /// <summary>
    /// Test whether CurrentRegistry should be serialized
    /// </summary>
    public virtual bool ShouldSerializeCurrentRegistry()
    {
        return (_currentRegistry != null);
    }
    
    /// <summary>
    /// Test whether RegistrationStatus should be serialized
    /// </summary>
    public virtual bool ShouldSerializeRegistrationStatus()
    {
        return (_registrationStatus != null);
    }
}
}
#pragma warning restore

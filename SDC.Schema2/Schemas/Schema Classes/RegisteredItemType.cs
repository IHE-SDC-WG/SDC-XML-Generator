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
/// This type provides a structure to record information about a file, template or package stored in a registry.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:ihe:qrph:sdc:2016")]
public partial class RegisteredItemType : ExtensionBaseType
{
    
    #region Private fields
    private List<RichTextType> _registeredItemDescription;
    
    private RegisteredItemStateType _state;
    
    private List<ContactType> _contact;
    
    private List<FileType> _referenceDocument;
    #endregion
    
    /// <summary>
    /// Description of the Registered Item
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute("RegisteredItemDescription", Order=0)]
    public virtual List<RichTextType> RegisteredItemDescription
    {
        get
        {
            return this._registeredItemDescription;
        }
        set
        {
            if ((this._registeredItemDescription == value))
            {
                return;
            }
            if (((this._registeredItemDescription == null) 
                        || (_registeredItemDescription.Equals(value) != true)))
            {
                this._registeredItemDescription = value;
                this.OnPropertyChanged("RegisteredItemDescription", value);
            }
        }
    }
    
    /// <summary>
    /// Status of the Registered Item
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public virtual RegisteredItemStateType State
    {
        get
        {
            return this._state;
        }
        set
        {
            if ((this._state == value))
            {
                return;
            }
            if (((this._state == null) 
                        || (_state.Equals(value) != true)))
            {
                this._state = value;
                this.OnPropertyChanged("State", value);
            }
        }
    }
    
    /// <summary>
    /// Person(s) and Organization(s) to contact regarding the Registration Status of the Registered Item
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute("Contact", Order=2)]
    public virtual List<ContactType> Contact
    {
        get
        {
            return this._contact;
        }
        set
        {
            if ((this._contact == value))
            {
                return;
            }
            if (((this._contact == null) 
                        || (_contact.Equals(value) != true)))
            {
                this._contact = value;
                this.OnPropertyChanged("Contact", value);
            }
        }
    }
    
    [System.Xml.Serialization.XmlElementAttribute("ReferenceDocument", Order=3)]
    public virtual List<FileType> ReferenceDocument
    {
        get
        {
            return this._referenceDocument;
        }
        set
        {
            if ((this._referenceDocument == value))
            {
                return;
            }
            if (((this._referenceDocument == null) 
                        || (_referenceDocument.Equals(value) != true)))
            {
                this._referenceDocument = value;
                this.OnPropertyChanged("ReferenceDocument", value);
            }
        }
    }
    
    /// <summary>
    /// Test whether RegisteredItemDescription should be serialized
    /// </summary>
    public virtual bool ShouldSerializeRegisteredItemDescription()
    {
        return RegisteredItemDescription != null && RegisteredItemDescription.Count > 0;
    }
    
    /// <summary>
    /// Test whether Contact should be serialized
    /// </summary>
    public virtual bool ShouldSerializeContact()
    {
        return Contact != null && Contact.Count > 0;
    }
    
    /// <summary>
    /// Test whether ReferenceDocument should be serialized
    /// </summary>
    public virtual bool ShouldSerializeReferenceDocument()
    {
        return ReferenceDocument != null && ReferenceDocument.Count > 0;
    }
    
    /// <summary>
    /// Test whether State should be serialized
    /// </summary>
    public virtual bool ShouldSerializeState()
    {
        return (_state != null);
    }
}
}
#pragma warning restore

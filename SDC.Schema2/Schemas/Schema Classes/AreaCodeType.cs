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
public partial class AreaCodeType : BaseType
{
    
    #region Private fields
    private bool _shouldSerializeval;
    
    private ushort _val;
    #endregion
    
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public virtual ushort val
    {
        get
        {
            return this._val;
        }
        set
        {
            if ((_val.Equals(value) != true))
            {
                this._val = value;
                this.OnPropertyChanged("val", value);
            }
            _shouldSerializeval = true;
        }
    }
    
    /// <summary>
    /// Test whether val should be serialized
    /// </summary>
    public virtual bool ShouldSerializeval()
    {
        if (_shouldSerializeval)
        {
            return true;
        }
        return (_val != default(ushort));
    }
}
}
#pragma warning restore

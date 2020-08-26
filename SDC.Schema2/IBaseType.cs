using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace SDC.Schema2

{
    public interface IBaseType
    {
        #region Public Members
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public bool AutoNameFlag { get; set; }
        ///// <summary>
        ///// Field to hold the ordinal position of an object (XML element) under an IdentifiedExtensionType (IET)-derived object.
        ///// This number is used for creating the name attribute suffix.
        ///// </summary>
        //[System.Xml.Serialization.XmlIgnore]
        //[JsonIgnore]
        //public decimal SubIETcounter { get; }

        /// <summary>
        /// The root text ("shortName") used to construct the name property.  The code may add a prefix and/or suffix to BaseName
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public string BaseName { get; set; }

        /// <summary>
        /// The name of XML element that is output from this class instance.
        /// Some SDC types are used in conjunction with multiple element names.  
        /// The auto-generated classes do not provide a way to determine the element name form the class instance.
        /// This property allows the code whichj creates each object to specify the element names that it is adding 
        /// as each object is creeated in code.  Although it may be possible to achieve this effect by reflection of 
        /// attributes, this ElementName approach provides more flexibility and is probably more efficient.
        /// ElementName will be most useful for auto-generating @name attributes for some elements.
        /// In many cases, ElementName will be assigned through class constructors, but it can also be assigned 
        /// through this property after the object is instantiated
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public string ElementName { get; set; }

        /// <summary>
        /// The prefix used 
        /// in the @name attribute that is output from this class instance
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public string ElementPrefix { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public int ObjectID { get; }
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public Guid ObjectGUID { get; }
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public ItemTypeEnum NodeType { get; }
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public Boolean IsLeafNode { get; }

        //[System.Xml.Serialization.XmlIgnore]
        //[JsonIgnore]
        //public string ParentIETypeID { get => ParentIETypeNode?.ID; }
        /// <summary>
        /// Returns the ID of the parent object (representing the parent XML element)
        /// This is the ObjectID, which is a sequentially assigned integer value.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public int ParentID
        {
            get
            {
                if (ParentNode != null)
                { return ParentNode.ObjectID; }
                else return -1;
            }
        }
        ///// <summary>
        ///// Returns the ID property of the closest ancestor of type DisplayedType.  
        ///// For eCC, this is the Parent node's ID, which is derived from  the parent node's CTI_Ckey, a.k.a. ParentItemCkey.
        ///// </summary>
        //[System.Xml.Serialization.XmlIgnore]
        //[JsonIgnore]
        //public IdentifiedExtensionType ParentIETypeNode { get; }
        /// <summary>
        /// Retrieve the BaseType object that is the immediate parent of the current object in the object tree
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public BaseType ParentNode { get; }
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public ITopNode TopNode { get; }
        public void SetNames(string elementName = "", string elementPrefix = "", string baseName = "");


        #endregion

    }
}

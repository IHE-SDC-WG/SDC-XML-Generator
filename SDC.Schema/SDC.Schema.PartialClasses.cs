
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
//using SDC.Schema;
//using SDC;
//using SDC.Schema.Adapter;

//using SDC.DAL.DataModel;

//!Handling Item and Items generic types derived from the xsd2Code++ code generator
namespace SDC.Schema
{
    public partial class FormDesignType
    {
        protected FormDesignType():base() { 
        
        }
        public FormDesignType(ITreeBuilder treeBuilder, BaseType parentNode = null, bool fillData = false, string id = null)
        : base(parentNode, fillData, id)
        //TODO: add ID, lineage, baseURI, version, etc to this constructor? (only ID is required)
        {
            sdcTreeBuilder = treeBuilder;
            IdentExtNodes = new Dictionary<String, IdentifiedExtensionType>();  //reset this static IdentExtNodes Dictionary of IET IDs for the current form

            //if (fillData) FillBaseTypeItem();  //this must be run after sdcTreeBuilder is assigned, and all sdcTreeBuilder data objects are initialized.
        }

        public SectionItemType AddBody(bool fillData = false, string id = "")
        {
            return sdcTreeBuilder.AddBody(fillData, id);
        }
        public SectionItemType AddFooter(bool fillData = false, string id = "")
        {
            return sdcTreeBuilder.AddFooter(fillData, id);
        }

        public SectionItemType AddHeader(bool fillData = false, string id = "")
        {
            return sdcTreeBuilder.AddHeader(fillData, id);
        }
        public RulesType AddRules()
        {
            //TODO: AddRules
            //var r = new RulesType();
            //this.Rules = r;
            //return r;
            return null;
        }

        [System.Xml.Serialization.XmlIgnore]
        public int MaxObjectID { get; internal set; }  //save the highest object counter value for the current FormDesign tree

        public static FormDesignType DeserializeSdcFromPath(string sdcPath)
        {
            string sdcXml = System.IO.File.ReadAllText(sdcPath);  // System.Text.Encoding.UTF8);
            return DeserializeSdcFromXml(sdcXml);
        }
        public static FormDesignType DeserializeSdcFromXml(string sdcXML)
        {
            return InitializeNodesFromSdcXml(sdcXML);
        }

        #region Dictionaries
        /// <summary>
        /// Dictionary.  Given an Node ID (int), returns the Node's object reference.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<int, BaseType> Nodes = new Dictionary<int, BaseType>();
        /// <summary>
        /// Dictionary.  Given a NodeID, return the *parent* Node's object reference
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<int, BaseType> ParentNodes = new Dictionary<int, BaseType>();
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<string, IdentifiedExtensionType> IdentifiedTypes;
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<string, SectionItemType> Sections;
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<string, QuestionItemType> Questions;
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<string, ListItemType> ListItemsAll;
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<string, ListItemResponseFieldType> ListItemResponses;
        //public static Dictionary<string, ResponseFieldType> Responses;
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<string, InjectFormType> InjectedItems;
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<string, DisplayedType> DisplayedItems;
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<string, ButtonItemType> Buttons;
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public Dictionary<string, BaseType> NamedNodes;
        #endregion

    }

    #region  Actions
    public partial class ActSendMessageType
    {
        protected ActSendMessageType() { }
        public ActSendMessageType(ActionsType parentNode) : base(parentNode) { }

        /// <summary>
        /// List<BaseType> accepts: EmailAddressType, PhoneNumberType, WebServiceType
        /// </summary>
        internal List<ExtensionBaseType> Email_Phone_WebSvc_List
        {
            get { return this.Items; }
            set { this.Items = value; }
        }
    }

    public partial class ActSendReportType
    {
        protected ActSendReportType() { }
        public ActSendReportType(ActionsType parentNode) : base(parentNode) { }

        internal List<ExtensionBaseType> Email_Phone_WebSvc_List
        {
            get { return this.Items; }
            set { this.Items = value; }
        }
    }

    //public partial class ActSetAttrValueType
    //{
    //    protected ActSetAttrValueType() { }
    //    public ActSetAttrValueType(ActionsType parentNode) : base(parentNode) { }

    //    internal GetCodeType Code_Item
    //    {
    //        get { return (GetCodeType)this.Item; }
    //        set { this.Item = (SDC.ExtensionBaseType)value; }
    //    }

    //    internal ExpressionType Expression_Item
    //    {
    //        get { return (ExpressionType)this.Item; }
    //        set { this.Item = (SDC.ExtensionBaseType)value; }
    //    }

    //    internal ItemNameType ResponseValue_Item
    //    {
    //        get { return (ItemNameType)this.Item; }
    //        set { this.Item = (SDC.ExtensionBaseType)value; }
    //    }

    //    internal DataTypes_SType ValueType_Item
    //    {
    //        get { return (DataTypes_SType)this.Item; }
    //        set { this.Item = (SDC.ExtensionBaseType)value; }
    //    }
    //}

    public partial class ActShowFormType
    {
        protected ActShowFormType() { }
        public ActShowFormType(ActionsType parentNode) : base(parentNode) { }
    }

    public partial class ActShowMessageType
    {
        protected ActShowMessageType() { }
        public ActShowMessageType(ActionsType parentNode) : base(parentNode) { }
    }

    public partial class ActShowReportType
    {
        protected ActShowReportType() { }
        public ActShowReportType(ActionsType parentNode) : base(parentNode) { }
    }

    public partial class ActValidateFormType
    {
        protected ActValidateFormType() { }
        public ActValidateFormType(ActionsType parentNode) : base(parentNode)
        {
            this.validateDataTypes = false;
            this.validateRules = false;
            this.validateCompleteness = false;
        }

        //!+Replaced in original class: protected ActValidateFormType() { }
        public ActValidateFormType Fill_ActValidateFormType()
        { return null; }
    }

    //public partial class ActSetPropertyType
    //{
    //    protected ActSetPropertyType() { }
    //    public ActSetPropertyType(ActionsType parentNode) : base(parentNode) { }

    //}

    public partial class ActActionType
    {
        protected ActActionType() { }
        public ActActionType(ActionsType parentNode) : base(parentNode) { }
    }
    ////add creation proc for each Then subtype

    public partial class ActInjectType
    {
        protected ActInjectType() { }
        public ActInjectType(ActionsType parentNode) : base(parentNode) { }
    }

    public partial class ActSaveResponsesType
    {
        protected ActSaveResponsesType() { }
        public ActSaveResponsesType(ActionsType parentNode) : base(parentNode) { }
    }

    public partial class ActAddCodeType
    {
        protected ActAddCodeType() { }
        public ActAddCodeType(ActionsType parentNode) : base(parentNode) { }
    }

    #endregion

    #region Main Types
    public partial class ButtonItemType
    {
        protected ButtonItemType() { }
        public ButtonItemType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "ButtonAction";
            ElementPrefix = "B";
            SetNames(elementName, elementPrefix);
            //if (fillData) sdcTreeBuilder.FillButton(this);
        }

    }

    public partial class InjectFormType
    {
        protected InjectFormType() { }
        public InjectFormType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "InjectForm";
            ElementPrefix = "Inj";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillInjectedForm(this);
        }

    }

    public partial class SectionBaseType
    {
        protected SectionBaseType() { }
        internal SectionBaseType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData, id)
        {
            this.ordered = true;
            ElementName = "Section";
            ElementPrefix = "S";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillSectionBase(this);
        }

        //!+Replaced in original class: protected SectionBaseType() { }
        //public void FillSectionBaseType()
        //{ sdcTreeBuilder.FillSectionBase(this); }
    }

    public partial class SectionItemType : IParent
    {
        public SectionItemType() { }
        public SectionItemType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData, id)
        { }


        #region IParent Implementation
        [System.Xml.Serialization.XmlIgnore]
        public ChildItemsType ChildItemsNode
        {
            get { return this.Item; }
            set { this.Item = value; }
        }
        public SectionItemType AddSection(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddSection<SectionItemType>(this, fillData, id); }
        public QuestionItemType AddQuestion(QuestionEnum qType, Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddQuestion<SectionItemType>(this, qType, fillData, id); }
        public InjectFormType AddInjectedForm(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddInjectedForm<SectionItemType>(this, fillData, id); }
        public ButtonItemType AddButtonAction(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddButtonAction<SectionItemType>(this, fillData, id); }
        public DisplayedType AddDisplayedItem(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddDisplayedItem<SectionItemType>(this, fillData, id); }
        #endregion
    }



    #region QAS

    #region Question
    public partial class QuestionItemType : IParent
    {
        public QuestionItemType() { }  //need public parameterless constructor to support generics
        public QuestionItemType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData, id)
        {
            this.readOnly = false;
            ElementName = "Question";
            ElementPrefix = "Q";
            SetNames(elementName, elementPrefix);
            if (fillData) FillQuestionItemBase();

        }

        #region IChildItems
        [System.Xml.Serialization.XmlIgnore]
        public ChildItemsType ChildItemsNode
        {
            get { return this.Item1; }
            set { this.Item1 = value; }
        }
        public SectionItemType AddSection(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddSection<QuestionItemType>(this, fillData, id); }
        public QuestionItemType AddQuestion(QuestionEnum qType, Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddQuestion<QuestionItemType>(this, qType, fillData, id); }
        public InjectFormType AddInjectedForm(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddInjectedForm<QuestionItemType>(this, fillData, id); }
        public ButtonItemType AddButtonAction(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddButtonAction<QuestionItemType>(this, fillData, id); }
        public DisplayedType AddDisplayedItem(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddDisplayedItem<QuestionItemType>(this, fillData, id); }
        #endregion

    }

    public partial class QuestionItemBaseType
    {
        protected QuestionItemBaseType() { }
        public QuestionItemBaseType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData, id)
        { }
        ////!+Replaced in original class: protected QuestionItemBaseType() { }

        public QuestionItemBaseType FillQuestionItemBase()
        { return sdcTreeBuilder.FillQuestionItemBase(this); }

        [System.Xml.Serialization.XmlIgnore]
        public ListFieldType ListField_Item
        {
            get { return (ListFieldType)this.Item; }
            set { this.Item = value; }
        }

        [System.Xml.Serialization.XmlIgnore]
        public QuestionEnum QuestionType { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public ResponseFieldType ResponseField_Item
        {
            get { return (ResponseFieldType)this.Item; }
            set { this.Item = value; }
        }

        //AddListField
        //AddList or AddLookupField or AddResponseField
        //AddListItem

    }
    #endregion

    #region QAS ListItems and Lookups


    public partial class ListType
    {
        protected ListType() { }
        public ListType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "lst";
            SetNames(elementName, elementPrefix);
        }

        /// <summary>
        /// Replaces Items; ListItem or DisplayedItem
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public List<DisplayedType> DisplayedItem_List
        {
            get { return this.Items; }
            set { this.Items = value; }
        }
    }

    public partial class ListFieldType
    {
        protected ListFieldType() { }
        public ListFieldType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.colTextDelimiter = "|";
            this.numCols = ((byte)(1));
            this.storedCol = ((byte)(1));
            this.minSelections = ((ushort)(1));
            this.maxSelections = ((ushort)(1));
            this.ordered = true;


            ElementPrefix = "lf";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillListField(this);
        }

        [System.Xml.Serialization.XmlIgnore]
        public ListType List_Item
        {
            get { return (ListType)this.Item; }
            set { this.Item = value; }
        }
        /// <summary>
        /// Replaces Item
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public LookupEndPointType LookupEndpoint_Item
        {
            get { return (LookupEndPointType)this.Item; }
            set { this.Item = value; }
        }

    }

    public partial class ListItemType : IParent
    {
        protected ListItemType() { }  //!+Replaced in original class: need PUBLIC parameterless constructor to support generics
        public ListItemType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData, id)
        {
            this.selected = false;
            this.selectionDisablesChildren = false;
            this.selectionDeselectsSiblings = false;
            this.omitWhenSelected = false;
            this.repeat = "1";
            ElementPrefix = "LI";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillListItemBase(this);
        }

        public ListItemResponseFieldType AddListItemResponseField(ListItemBaseType li)
        {
            return sdcTreeBuilder.AddListItemResponseField(this);
        }


        #region IChildItems
        /// <summary>
        /// The ChildItems node replaces "Item" (MainNodesType), and may contain:
        ///"ButtonAction", typeof(ButtonItemType),
        ///"DisplayedItem", typeof(DisplayedType),
        ///"InjectForm", typeof(InjectFormType),
        ///"Question", typeof(QuestionItemType),
        ///"Section", typeof(SectionItemType),
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public ChildItemsType ChildItemsNode
        {
            get { return this.Item; }
            set { this.Item = value; }
        }
        public SectionItemType AddSection(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddSection<ListItemType>(this, fillData, id); }
        public QuestionItemType AddQuestion(QuestionEnum qType, Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddQuestion<ListItemType>(this, qType, fillData, id); }
        public InjectFormType AddInjectedForm(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddInjectedForm<ListItemType>(this, fillData, id); }
        public ButtonItemType AddButtonAction(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddButtonAction<ListItemType>(this, fillData, id); }
        public DisplayedType AddDisplayedItem(Boolean fillData = true, string id = null)
        { return sdcTreeBuilder.AddDisplayedItem<ListItemType>(this, fillData, id); }
        #endregion


    }

    public partial class ListItemBaseType
    {
        protected ListItemBaseType() { }
        public ListItemBaseType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData, id)
        { }
        //!+Replaced in original class: protected  ListItemBaseType (){}

        //public ListItemBaseType FillListItemBase()
        //{ return sdcTreeBuilder.FillListItemBase(this); }

        [NonSerialized]
        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, ListItemType> ListItems;

    }

    public partial class LookupEndPointType  //TODO: fix base class in Schema update
    {
        protected LookupEndPointType() { }
        public LookupEndPointType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base()
        {
            this.includesHeaderRow = false;
            ElementPrefix = "LEP";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillLookupEndpoint(this);
        }



        //!+Replaced in original class: protected LookupEndPointType() { }

        //public LookupEndPointType FillLookupEndPoint()
        //{ return sdcTreeBuilder.FillLookupEndpoint(this); }
    }

    #endregion

    #region Responses

    public partial class ListItemResponseFieldType
    {
        protected ListItemResponseFieldType() { }
        public ListItemResponseFieldType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.responseRequired = false;
            //if (fillData) AddFillDataTypesDE(parentNode);

            ElementPrefix = "lirf";
            SetNames(elementName, elementPrefix); //this was already called by the superType ResponseField.
            if (fillData) sdcTreeBuilder.FillListItemResponseField(this);
        }
    }

    public partial class ResponseFieldType
    {
        protected ResponseFieldType() { }
        public ResponseFieldType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "rf";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillResponseField(this);
        }
    }

    public partial class UnitsType
    {
        protected UnitsType() { }
        public UnitsType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            unitSystem = "UCUM";
            if (fillData) sdcTreeBuilder.FillUnits(this);
            ElementPrefix = "un";
            SetNames(elementName, elementPrefix);
        }
    }

    #endregion

    #endregion


    #endregion

    #region Base Types
    public partial class BaseType
    {

        #region  Local Members

        /// <summary>
        /// sdcTreeBuilder is an object created and held by the top level FormDesign node, 
        /// but referenced throughout the FormDesign object tree through the BaseType class
        /// </summary>
        protected ITreeBuilder sdcTreeBuilder;
        private string _elementName = "";
        private string _elementPrefix = "";

        /// <summary>
        /// Static counter that resets with each new instance of an IdentifiedExtensionType (IET).
        /// Maintains the sequence of all elements nested under an IET-derived element.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        private static int IETresetCounter { get; set; }
        /// <summary>
        /// Field to hold the ordinal position of an object (XML element) under an IdentifiedExtensionType (IET)-derived object.
        /// This number is used for creating the name attribute suffix.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public decimal SubIETcounter { get; private set; }

        /// <summary>
        /// The root text ("shortName") used to construct the name property.  The code may add a prefix and/or suffix to BaseName
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string BaseName { get; set; } = "";


        #endregion


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
        public string ElementName
        {
            get
            {
                if (_elementName.Length == 0)
                {//assign default ElementName from the type.  Strip off sufixes that are not used in the actual XML element tag.
                    _elementName = GetType().ToString()
                        .Replace("SDC.", string.Empty)
                        .Replace("Type", string.Empty)
                        .Replace("_Stype", string.Empty)
                        .Replace("_DEtype", string.Empty);
                }
                return _elementName;
            }
            set
            {
                _elementName = value;
            }
        }

        /// <summary>
        /// The prefix used 
        /// in the @name attribute that is output from this class instance
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string ElementPrefix
        {
            get
            { //assign default prefix from the ElementName
                if (_elementPrefix.Length == 0)
                {
                    _elementPrefix = ElementName;
                    //make sure first letter is lower case for non-IET types:
                    if (!(GetType().IsSubclassOf(typeof(IdentifiedExtensionType)))) _elementPrefix = _elementPrefix.Substring(0, 1).ToLower() + _elementPrefix.Substring(1);
                }
                return _elementPrefix;
            }
            set { _elementPrefix = value; }
        }
        protected BaseType()
        {
            //Debug.WriteLine($"A node has entered the BaseType default ctor. Item type is {this.GetType()}");

            ObjectGUID = Guid.NewGuid();
            IsLeafNode = true;

            //RegisterParent(parentNode);  //We have to obtain the parent node from the XMLDocument tree


            if (GetType().IsSubclassOf(typeof(IdentifiedExtensionType))) IETresetCounter = 0;
            else IETresetCounter++;
            SubIETcounter = IETresetCounter;


            if (FormDesign is null)
            {//this is a top level node, so it MUST be of type FormDesignType
                if (this.GetType() == typeof(FormDesignType))
                {   FormDesign = (FormDesignType)this;  }
                else
                {   throw new InvalidOperationException("The top level node must be a FormDesignType"); }
            }

            ObjectID = FormDesign.MaxObjectID++;
            orderSpecified = true;  //force output of order attribute
            order = ObjectID;
            FormDesign.Nodes.Add(ObjectID, this);

            //Debug.WriteLine($"The node with ObjectID: {this.ObjectID} has entered the BaseType ctor. Item type is {this.GetType()}.  "
            //    + $"The parent ObjectID is {this.ParentObjID.ToString()}");

        }

        protected BaseType(BaseType parentNode, bool fillData = true) //: this()
        {
            ObjectGUID = Guid.NewGuid();
            IsLeafNode = true;

            if (GetType().IsSubclassOf(typeof(IdentifiedExtensionType))) IETresetCounter = 0;
            else IETresetCounter++;
            SubIETcounter = IETresetCounter;

            if (parentNode is null)
            {//this is a top level node, and it MUST be of type FormDesignType
                if (this.GetType() == typeof(FormDesignType))
                { FormDesign = (FormDesignType)this; }  //TopNode.FormDesign;
                else
                { throw new InvalidOperationException("The top level node must be FormDesignType"); }
                }
            else
            {   //a parent node is present, and it must NOT be of type FormDesign
                if (this.GetType() == typeof(FormDesignType))
                {   throw new InvalidOperationException("Only the top level node can be FormDesignType");}

                sdcTreeBuilder = FormDesign.sdcTreeBuilder;
            }

            
            ObjectID = FormDesign.MaxObjectID++;
            FormDesign.Nodes.Add(ObjectID, this);
            orderSpecified = true;  //force output of order attribute
            order = ObjectID;  //added 3/25/2018; this outputs the @order attribute for every element

            RegisterParent(parentNode);

            Debug.WriteLine($"The node with ObjectID: {this.ObjectID} has entered the BaseType ctor. Item type is {this.GetType()}.  " +
                $"The parent ObjectID is {this.ParentObjID.ToString()}, ParentIETypeID is: {this.ParentIETypeID}");


            if (fillData) FillBaseTypeItem();
        }

        protected internal static FormDesignType InitializeNodesFromSdcXmlPath(string path)
        {
            string sdcXml = System.IO.File.ReadAllText(path);  // System.Text.Encoding.UTF8);
            return InitializeNodesFromSdcXml(sdcXml);

        }

        protected internal static FormDesignType InitializeNodesFromSdcXml(string sdcXml)
        {
            //string sdcXml = System.IO.File.ReadAllText(path);  // System.Text.Encoding.UTF8);
            FormDesignType FD = FormDesignType.Deserialize(sdcXml);

            //read as XMLDocument to walk tree
            var x = new System.Xml.XmlDocument();
            x.LoadXml(sdcXml);
            XmlNodeList xmlNodeList = x.SelectNodes("//*");

            var dX_FD = new Dictionary<int, int>(); //the index is iXmlNode, value is FD ObjectID
            int iXmlNode = 0;
            XmlNode xmlNode;

            foreach (BaseType bt in FD.Nodes.Values)
            {                //As we interate through the nodes, we will need code to skip over any non-element node (using i2), 
                //and still stay in sync with FD (using iFD). For now, we assume that every nodeList node is an element.
                //https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnodetype?view=netframework-4.8
                //https://docs.microsoft.com/en-us/dotnet/standard/data/xml/types-of-xml-nodes
                xmlNode = xmlNodeList[iXmlNode];
                while (xmlNode.NodeType.ToString() != "Element")
                {
                    iXmlNode++;
                    xmlNode = xmlNodeList[iXmlNode];
                }
                //Create a new attribute node to hold the node's index in xmlNodeList
                XmlAttribute a = x.CreateAttribute("index");
                a.Value = iXmlNode.ToString();
                var e = (XmlElement)xmlNode;
                e.SetAttributeNode(a);

                //Create  dictionary to track the matched indexes of the XML and FD node collections
                dX_FD[iXmlNode] = bt.ObjectID;
                //Debug.Print("iXmlNode: " + iXmlNode + ", ObjectID: " + bt.ObjectID);

                //Search for parents:
                int parIndexXml;
                int parObjectID;
                bool parExists;
                BaseType btPar;
                XmlNode parNode;
                parIndexXml = -1;
                parObjectID = -1;
                parExists = false;
                btPar = null;

                parNode = xmlNode.ParentNode;
                parExists = int.TryParse(parNode?.Attributes?.GetNamedItem("index")?.Value, out parIndexXml);//The index of the parent XML node
                if (parExists)
                {
                    parExists = dX_FD.TryGetValue(parIndexXml, out parObjectID);// find the matching parent FD node Object ID
                    if (parExists) { parExists = FD.Nodes.TryGetValue(parObjectID, out btPar); } //Find the parent node in FD
                    if (parExists)
                    {
                        bt.IsLeafNode = true;
                        bt.RegisterParent(btPar);
                        Debug.WriteLine($"The node with ObjectID: {bt.ObjectID} is leaving InitializeNodesFromSdcXml. Item type is {bt.GetType()}.  " +
                                    $"The parent ObjectID is {bt.ParentObjID}, ParentIETypeID is: {bt.ParentIETypeID}");
                    }
                    else { throw new KeyNotFoundException("No parent object was returned rom the FormDesign tree"); }
                }
                else
                {
                    bt.IsLeafNode = false;
                    Debug.WriteLine($"The node with ObjectID: {bt.ObjectID} is leaving InitializeNodesFromSdcXml. Item type is {bt.GetType()}.  " +
                                    $", No Parent object exists");
                }

                iXmlNode++;
            }
            return FD;

        }

        ~BaseType() //destructor
        {
            //FormDesign = null;
            sdcTreeBuilder = null;
            //prevent orphan nodes:
            //TODO: delete all child nodes here - lower descendants will delete their own child nodes
            //TODO: Remove this node from all FormDesign dictionaries
            //TODO: Reset IsLeafNode to false for the parent of this node
            //TODO: Remove references from FormDesign Dictionaries
        }
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public readonly int ObjectID; //{ get { return _ObjectID; } }

        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public readonly Guid ObjectGUID;
        private BaseType _ParentNode;
        [System.Xml.Serialization.XmlIgnore]
        public ItemTypeEnum NodeType { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public Boolean IsLeafNode { get; private set; }
        //Properties to mark changed nodes for serialization to database etc.
        [System.Xml.Serialization.XmlIgnore]
        public Boolean Added { get; private set; }
        [System.Xml.Serialization.XmlIgnore]
        public Boolean Changed { get; private set; }
        [System.Xml.Serialization.XmlIgnore]
        public Boolean Deleted { get; private set; }
        [System.Xml.Serialization.XmlIgnore]
        public DateTime UpdatedDateTime { get; private set; }


        public void SetNames(string elementName = "", string elementPrefix = "", string baseName = "")
        {
            if (elementName.Length > 0)
                ElementName = elementName;
            //else if (ElementName.Length == 0) ElementName = GetType().ToString().Replace("Type", "").Replace("type", ""); //assign default ElementName from the type.

            if (elementPrefix.Length > 0)
                ElementPrefix = elementPrefix;

            if (baseName.Length > 0)
                BaseName = baseName;
            //else if (ElementPrefix.Length == 0) ElementPrefix = ElementName;

            //if (name.Length == 0)
            name = sdcTreeBuilder.CreateName(this);



            Debug.WriteLine($"Type: {GetType()} ElementName: {ElementName} Prefix:{ElementPrefix} name: {name}");
        }

        /// <summary>
        /// Returns the ID property of the closest ancestor of type DisplayedType.  
        /// For eCC, this is the Parent node's ID, which is derived from  the parent node's CTI_Ckey, a.k.a. ParentItemCkey.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string ParentIETypeID { get => ParentIETypeObject?.ID; }

        /// <summary>
        /// Returns the ID of the parent object (representing the parent XML element)
        /// This is the ObjectID, which is a sequentially assigned integer value.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public int ParentObjID
        {
            get
            {
                if (ParentNode != null)
                { return ParentNode.ObjectID; }
                else return -1;
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        public IdentifiedExtensionType ParentIETypeObject { get; private set; }
        public virtual void X_AddFill(SDCtypes sdcType = SDCtypes.BaseType, Boolean fillData = true)
        {

            switch (sdcType)
            {

                case SDCtypes.AcceptabilityType:  //just an example
                    break;
            }
        }
        public virtual void X_AddFill(string typeName = nameof(BaseType), Boolean fillData = true)
        {
            var sdcType = new SDCtypes();
            try
            {
                sdcType = SDCHelpers.ConvertStringToEnum<SDCtypes>(typeName);
                //AddFill(sdcType, fillData);
                return;
            }
            catch (Exception ex)
            {
                sdcType = SDCtypes.BaseType;
            }
            //AddFill(sdcType, fillData);
        }
        public BaseType FillBaseTypeItem()
        {
            //this null case happens when FormDesign is first being created, 
            //and the BaseType constructor runs before the FormDesign constructor.
            //In this special case, this function must be called directly from the FormDesign constructor, 
            //after sdcTreeBuilder has been created
            if (sdcTreeBuilder == null) return null;
            return sdcTreeBuilder.FillBaseTypeItem(this);
        }

        /// <summary>
        /// Retrieve the BaseType object that is the immediate parent of the current object in the object tree
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public BaseType ParentNode
        {
            get
            {
                if (!(_ParentNode is null)) return _ParentNode;  //this works for objects that were created with the parentNode constructor
                                                                 //BaseType outParentNode;
                if (this.GetType() != typeof(FormDesignType)) //TODO: remove this reflection condition and test for errors
                {
                    Debug.WriteLine(GetType().ToString());
                    FormDesign.ParentNodes.TryGetValue(this.ObjectID, out BaseType outParentNode);
                    _ParentNode = outParentNode;
                }
                else { _ParentNode = null; }
                return _ParentNode;


            }
            internal set
            {
                _ParentNode = value;
            }
        }
        private static FormDesignType formDesign;
        [System.Xml.Serialization.XmlIgnore]
        public static FormDesignType FormDesign
        {
            get  {return formDesign;}
            private set  {
                if (formDesign is null)
                {  formDesign = value; }
                else {throw new Exception("FormDesign has already been assigned"); }
            }
        }

        private void RegisterParent<T>(T inParentNode) where T : BaseType
        {
            try
            {
                if (inParentNode != null)
                {   //Register parent node
                    ParentNode = inParentNode;
                    FormDesign.ParentNodes.Add(ObjectID, inParentNode);
                    inParentNode.IsLeafNode = false; //the parent node has a child node, so it can't be a leaf node
                    //Register IdentifiedExtensionType parent node
                    BaseType par = ParentNode;
                    while (par != null) //walk up the parent tree until we find the first IdentifiedExtensionType object
                    {
                        if (par.GetType().IsSubclassOf(typeof(IdentifiedExtensionType)))
                        {
                            ParentIETypeObject = par as IdentifiedExtensionType;
                            return;
                        }
                        par = par.ParentNode;
                    }
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message + "/n  ObjectID:" + this.ObjectID.ToString()); }
        }
    }

    public partial class ExtensionBaseType
    {
        protected ExtensionBaseType() { }
        public ExtensionBaseType(BaseType parentNode, bool fillData = true) : base(parentNode, fillData)
        {
            if (fillData) AddFillExtensionBaseType(fillData);
        }

        public CommentType AddFillComment(Boolean fillData = true) { return sdcTreeBuilder.AddFillComment(this, fillData); }
        public ExtensionType AddFillExtension(Boolean fillData = true) { return sdcTreeBuilder.AddFillExtension(this, fillData); }

        public ExtensionBaseType AddFillExtensionBaseType(Boolean fillData = true) { return sdcTreeBuilder.AddExtensionBaseTypeItems(this); }
    }

    public partial class ExtensionType
    {
        protected ExtensionType() { }
        public ExtensionType(BaseType parentNode, bool fillData = true) : base(parentNode, fillData) { } // sdcTreeBuilder.FillExtensionTypeItems(this); }
    }

    public partial class IdentifiedExtensionType
    {

        /// <summary>
        /// Given an Item Node's URI, returns the Item's object reference as IdentifiedExtensionType.
        /// </summary>
        [NonSerialized]
        public static Dictionary<String, IdentifiedExtensionType> IdentExtNodes = new Dictionary<String, IdentifiedExtensionType>();

        protected IdentifiedExtensionType() { }
        protected IdentifiedExtensionType(BaseType parentNode, bool fillData = true, string id = null) : base(parentNode, fillData)
        {
            if (fillData) sdcTreeBuilder.FillIdentifiedTypeItems(this);

            AddToIdentExtNodes(id);
        }

        private void AddToIdentExtNodes(string id = null)
        {

            if (string.IsNullOrWhiteSpace(this.ID))
            {
                if (!string.IsNullOrWhiteSpace(id))
                    this.ID = id;
                else if (this.ObjectGUID != null)
                    this.ID = this.ObjectGUID.ToString();
            }

            if (!string.IsNullOrWhiteSpace(this.ID))
            {
                try
                {
                    IdentExtNodes.Add(this.ID, this);
                    Debug.WriteLine($"The node with ObjectID: {this.ObjectID} and ID: {this.ID} WAS added to the IdentExtNodes dictionary. Item type is {this.GetType()}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}. The node with ObjectID: {this.ObjectID} and ID: {this.ID} was NOT added to the IdentExtNodes dictionary.  Item type is {this.GetType()}");
                }
            }
            else
            { Debug.WriteLine($"The node with ObjectID: {this.ObjectID} and ID: {this.ID} was NOT added to the IdentExtNodes dictionary.  Item type is {this.GetType()}"); }
        }

    }

    public partial class RepeatingType //this is an SDC abstract class
    {
        protected RepeatingType() { }
        protected RepeatingType(BaseType parentNode, bool fillData = true, string id = null) : base(parentNode, fillData, id)
        {
            this.minCard = ((ushort)(1));
            this.maxCard = ((ushort)(1));
            this.repeat = "1";
            sdcTreeBuilder.FillRepeatingTypeItems(this, fillData);
        }
        //!+Replaced in original class: protected RepeatingType() { }

    }

    public partial class ChildItemsType
    {
        protected ChildItemsType() { }
        public ChildItemsType(BaseType parentNode, string elementName = "", string elementPrefix = "") : base(parentNode)
        {
            ElementPrefix = "ch";
            SetNames(elementName, elementPrefix);
        }

        [System.Xml.Serialization.XmlIgnore]
        public List<IdentifiedExtensionType> ListOfItems
        {
            get { return this.Items; }
            set { this.Items = value; }
        }
    }



    #endregion

    #region DisplayedType and Helpers

    public partial class DisplayedType : IDisplayedType
    {
        protected DisplayedType() { }
        public DisplayedType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData, id)
        {
            this.enabled = true;
            this.visible = true;
            this.mustImplement = true;
            this.showInReport = DisplayedTypeShowInReport.True;
            ElementName = "DisplayedItem";
            ElementPrefix = "DI";
            SetNames(elementName, elementPrefix);
            sdcTreeBuilder.FillDisplayedTypeItems(this, fillData);
        }

        #region IDisplayedType
        public PropertyType AddProperty(Boolean fillData = true)
        { return sdcTreeBuilder.AddProperty(this, fillData); }
        public LinkType AddLink(Boolean fillData = true)
        { return sdcTreeBuilder.AddLink(this, fillData); }
        public BlobType AddBlob(Boolean fillData = true)
        { return sdcTreeBuilder.AddBlob(this, fillData); }
        public ContactType AddContact(Boolean fillData = true)
        { return sdcTreeBuilder.AddContact(this, fillData); }
        public CodingType AddCoding(Boolean fillData = true)
        { return sdcTreeBuilder.AddCodedValue(this, fillData); }

        #region DisplayedType Events
        public OnEventType AddOnEvent(Boolean fillData = true)
        { return sdcTreeBuilder.AddOnEvent(this, fillData); }
        public EventType AddOnEnter(Boolean fillData = true)
        { return sdcTreeBuilder.AddOnEnter(this, fillData); }
        public EventType AddOnExit(Boolean fillData = true)
        { return sdcTreeBuilder.AddOnExit(this, fillData); }
        public GuardType AddActivateIf(Boolean fillData = true)
        { return sdcTreeBuilder.AddActivateIf(this, fillData); }
        public GuardType AddDeActivateIf(Boolean fillData = true)
        { return sdcTreeBuilder.AddDeActivateIf(this, fillData); }
        #endregion


        #endregion
    }


    #region DisplayedType Helper Classes

    public partial class BlobType
    {
        protected BlobType() { }
        public BlobType(DisplayedType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "Blob";
            ElementPrefix = "blob";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillBlob(this);
        }
    }

    public partial class PropertyType
    {
        protected PropertyType() { }
        public PropertyType(ExtensionBaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "Property";
            ElementPrefix = "p";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillProperty(this);
        }

        protected HTML_Stype AddHTML()
        {
            var rtf = new RichTextType(this);
            var h = sdcTreeBuilder.AddFillHTML(rtf);
            return h;
        }
    }

    public partial class LinkType
    {
        protected LinkType() { }
        public LinkType(DisplayedType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "Link";
            ElementPrefix = "link";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillLinkText(this);
        }
    }

    #region Coding


    public partial class CodingType
    {
        protected CodingType() { }
        public CodingType(ExtensionBaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "CodedValed";
            ElementPrefix = "cval";
            SetNames(elementName, elementPrefix);

        }
    }

    public partial class CodeMatchType
    {
        protected CodeMatchType() { }
        public CodeMatchType(CodingType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "CodeMatch";
            ElementPrefix = "cmat";
            SetNames(elementName, elementPrefix);
            this.codeMatchEnum = CodeMatchTypeCodeMatchEnum.ExactCodeMatch;

        }
        //!+Replaced in original class: protected CodeMatchType() { }
    }

    public partial class CodeSystemType
    {
        protected CodeSystemType() { }
        public CodeSystemType(ExtensionBaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "CodeSystem";
            ElementPrefix = "csys";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillCodeSystemItems(this);
        }
    }

    #endregion

    #endregion


    #endregion


    #region DataTypes
    public partial class DataTypes_DEType
    {
        protected DataTypes_DEType() { }
        public DataTypes_DEType(ResponseFieldType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "Response"; //response element
            ElementPrefix = "rsp";  //response element            
            SetNames(elementName, elementPrefix);
            //if (fillData) sdcTreeBuilder.AddFillDataTypesDE(parentNode);
        }

        /// <summary>
        /// any *_DEType data type
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public BaseType DataTypeDE_Item
        {
            get { return this.Item; }
            set { this.Item = value; }
        }
    }

    public partial class anyType_DEtype
    {
        protected anyType_DEtype() { }
        public anyType_DEtype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "any";
            SetNames(elementName, elementPrefix);
        }
    }


    public partial class DataTypes_SType
    {
        protected DataTypes_SType() { }
        public DataTypes_SType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "DataTypes";
            SetNames(elementName, elementPrefix);
        }

        /// <summary>
        /// any *_SType data type
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public BaseType DataTypeS_Item
        {
            get { return this.Item; }
            set { this.Item = value; }
        }
    }

    public partial class anyURI_DEtype
    {
        protected anyURI_DEtype() { }
        public anyURI_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "uri";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class anyURI_Stype
    {
        protected anyURI_Stype() { }
        public anyURI_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "uri";
            SetNames(elementName, elementPrefix);

        }
    }

    public partial class base64Binary_DEtype
    {
        protected base64Binary_DEtype() { }
        public base64Binary_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            //ElementPrefix = "b64";
            //SetNames(elementName, elementPrefix);

        }
    }

    public partial class base64Binary_Stype
    {
        protected base64Binary_Stype() { }
        public base64Binary_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "b64";
            SetNames(elementName, elementPrefix);

        }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "string")] //changed to string
        public string valBase64 { get; set; }
    }

    public partial class boolean_DEtype
    {
        protected boolean_DEtype() { }
        public boolean_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            //ElementPrefix = "bool";
            //SetNames(elementName, elementPrefix);

        }
    }

    public partial class boolean_Stype
    {
        protected boolean_Stype() { }
        public boolean_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "bool";
            SetNames(elementName, elementPrefix);

        }
    }

    public partial class byte_DEtype
    {
        protected byte_DEtype() { }
        public byte_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "byte";
            //SetNames(elementName, elementPrefix);

        }
    }

    public partial class byte_Stype
    {
        protected byte_Stype() { }
        public byte_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "byte";
            SetNames(elementName, elementPrefix);
            //this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class date_DEtype
    {
        protected date_DEtype() { }
        public date_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "date";
            //SetNames(elementName, elementPrefix);

        }
    }

    public partial class date_Stype
    {
        protected date_Stype() { }
        public date_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "date";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class dateTime_DEtype
    {
        protected dateTime_DEtype() { }
        public dateTime_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "dt";
            //SetNames(elementName, elementPrefix);

        }
    }

    public partial class dateTime_Stype
    {
        protected dateTime_Stype() { }
        public dateTime_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "dt";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class dateTimeStamp_DEtype
    {
        protected dateTimeStamp_DEtype() { }
        public dateTimeStamp_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            //ElementPrefix = "dts";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class dateTimeStamp_Stype
    {
        protected dateTimeStamp_Stype() { }
        public dateTimeStamp_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "dts";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class dayTimeDuration_DEtype
    {
        protected dayTimeDuration_DEtype() { }
        public dayTimeDuration_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "dtdur";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class dayTimeDuration_Stype
    {
        protected dayTimeDuration_Stype() { }
        public dayTimeDuration_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "dtdur";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class decimal_DEtype
    {
        protected decimal_DEtype() { }
        public decimal_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "dec";
            //SetNames(elementName, elementPrefix);

        }
    }

    public partial class decimal_Stype
    {
        protected decimal_Stype() { }
        public decimal_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "dec";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class double_DEtype
    {
        protected double_DEtype() { }
        public double_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "dbl";
            //SetNames(elementName, elementPrefix);

        }
    }

    public partial class double_Stype
    {
        protected double_Stype() { }
        public double_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "dbl";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class duration_DEtype
    {
        protected duration_DEtype() { }
        public duration_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "dur";
            //SetNames(elementName, elementPrefix);

        }
    }

    public partial class duration_Stype
    {
        protected duration_Stype() { }
        public duration_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class float_DEtype
    {
        protected float_DEtype() { }
        public float_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "flt";
            //SetNames(elementName, elementPrefix);

        }
    }

    public partial class float_Stype
    {
        protected float_Stype() { }
        public float_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "flt";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class gDay_DEtype
    {
        protected gDay_DEtype() { }
        public gDay_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "day";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class gDay_Stype
    {
        protected gDay_Stype() { }
        public gDay_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "day";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class gMonth_DEtype
    {
        protected gMonth_DEtype() { }
        public gMonth_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "mon";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class gMonth_Stype
    {
        protected gMonth_Stype() { }
        public gMonth_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "mon";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class gMonthDay_DEtype
    {
        protected gMonthDay_DEtype() { }
        public gMonthDay_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "mday";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class gMonthDay_Stype
    {
        protected gMonthDay_Stype() { }
        public gMonthDay_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "mday";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class gYear_DEtype
    {
        protected gYear_DEtype() { }
        public gYear_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "y";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class gYear_Stype
    {
        protected gYear_Stype() { }
        public gYear_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "y";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class gYearMonth_DEtype
    {
        protected gYearMonth_DEtype() { }
        public gYearMonth_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "ym";
            //SetNames(elementName, elementPrefix);
        }
    }
    public partial class gYearMonth_Stype
    {
        protected gYearMonth_Stype() { }
        public gYearMonth_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "ym";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class hexBinary_DEtype
    {
        protected hexBinary_DEtype() { }
        public hexBinary_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            //ElementPrefix = "hexb";
            //SetNames(elementName, elementPrefix);
        }

    }

    public partial class hexBinary_Stype
    {
        string _hexBinaryStringVal;

        protected hexBinary_Stype() { }
        public hexBinary_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "hexb";
            SetNames(elementName, elementPrefix);
        }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "string")] //changed to string
        public string valHex
        {
            get { return _hexBinaryStringVal; }
            set { _hexBinaryStringVal = value; }
        }
    }

    public partial class HTML_DEtype
    {
        protected HTML_DEtype()
        { this.Any = new List<System.Xml.XmlElement>(); }
        public HTML_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            //ElementPrefix = "html";
            //SetNames(elementName, elementPrefix);
            //this.Any = new List<System.Xml.XmlElement>();
        }
    }

    public partial class HTML_Stype
    {
        protected HTML_Stype()
        { this.Any = new List<System.Xml.XmlElement>(); }
        public HTML_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "html";
            SetNames(elementName, elementPrefix);
            //this.Any = new List<System.Xml.XmlElement>();
        }

    }


    public partial class int_DEtype
    {
        protected int_DEtype() { }
        public int_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "int";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class int_Stype
    {
        protected int_Stype() { }
        public int_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "int";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected int_Stype() { }
    }

    public partial class integer_DEtype
    {
        protected integer_DEtype() { }
        public integer_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "intr";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class integer_Stype
    {
        protected integer_Stype() { }
        public integer_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "intr";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected integer_Stype() { }


        /// <summary>
        /// Added to support proper decimal set/get; 
        /// Decimal is the best data type to match W3C integer.
        /// This will not work with XML serializer, bc decimal types always serialize trailing zeros,
        /// and trailing zeros are not allowed with integer types
        /// TODO: Need to truncate (or possibly round) any digits after the decimal point in the setter/getter
        /// For positive/negative etc integers, need to check the sign and throw error if incorrect.
        /// May want to throw errors for for min/max allowed values also - not sure about this)
        /// May need to import System.Numerics.dll to use BigInteger
        /// </summary>
        /// 
        [System.Xml.Serialization.XmlIgnore]
        public virtual decimal? valDec
        //rlm 2/11/18 probably don't want to use this
        {
            get
            {
                if (val != null && val.Length > 0)
                { return Convert.ToDecimal(this.val); }
                return null;
            }
            set
            {
                if (value != null)
                { this.val = Math.Truncate(value.Value).ToString(); }
                else this.val = null;

            }
        }

    }

    public partial class long_DEtype
    {
        protected long_DEtype() { }
        public long_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "lng";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class long_Stype
    {
        protected long_Stype() { }
        public long_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "lng";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class negativeInteger_DEtype
    {
        protected negativeInteger_DEtype() { }
        public negativeInteger_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "nint";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class negativeInteger_Stype
    {
        protected negativeInteger_Stype() { }
        public negativeInteger_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "nint";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class nonNegativeInteger_DEtype
    {
        protected nonNegativeInteger_DEtype() { }
        public nonNegativeInteger_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "nnint";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class nonNegativeInteger_Stype
    {
        protected nonNegativeInteger_Stype() { }
        public nonNegativeInteger_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "nnint";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class nonPositiveInteger_DEtype
    {
        protected nonPositiveInteger_DEtype() { }
        public nonPositiveInteger_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "npint";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class nonPositiveInteger_Stype
    {
        protected nonPositiveInteger_Stype() { }
        public nonPositiveInteger_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "npint";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class positiveInteger_DEtype
    {
        protected positiveInteger_DEtype() { }
        public positiveInteger_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "pint";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class positiveInteger_Stype
    {
        protected positiveInteger_Stype() { }
        public positiveInteger_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "pint";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class short_DEtype
    {
        protected short_DEtype() { }
        public short_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "sh";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class short_Stype
    {
        protected short_Stype() { }
        public short_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "sh";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class string_DEtype
    {
        protected string_DEtype() { }
        public string_DEtype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            //ElementPrefix = "str";
            //SetNames(elementName, elementPrefix);
        } //{if (elementName.Length > 0) ElementName = elementName; }
    }

    public partial class string_Stype
    {
        protected string_Stype() { }
        public string_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "str";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class time_DEtype
    {
        protected time_DEtype() { }
        public time_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "tim";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class time_Stype
    {
        protected time_Stype() { }
        public time_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "tim";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected time_Stype() { }
    }

    public partial class unsignedByte_DEtype
    {
        protected unsignedByte_DEtype() { }
        public unsignedByte_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "ubyte";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class unsignedByte_Stype
    {
        protected unsignedByte_Stype() { }
        public unsignedByte_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "ubyte";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected unsignedByte_Stype() { }
    }

    public partial class unsignedInt_DEtype
    {
        protected unsignedInt_DEtype() { }
        public unsignedInt_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "unint";
            //SetNames(elementName, elementPrefix);
        }

    }

    public partial class unsignedInt_Stype
    {
        protected unsignedInt_Stype() { }
        public unsignedInt_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "uint";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected unsignedInt_Stype() { }
    }

    public partial class unsignedLong_DEtype
    {
        protected unsignedLong_DEtype() { }
        public unsignedLong_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "ulng";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class unsignedLong_Stype
    {
        protected unsignedLong_Stype() { }
        public unsignedLong_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {

            ElementPrefix = "ulng";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected unsignedLong_Stype() { }
    }

    public partial class unsignedShort_DEtype
    {
        protected unsignedShort_DEtype() { }
        public unsignedShort_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;

            //ElementPrefix = "ush";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class unsignedShort_Stype
    {
        protected unsignedShort_Stype() { }
        public unsignedShort_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {

            ElementPrefix = "ush";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected unsignedShort_Stype() { }
    }

    public partial class XML_DEtype
    {
        protected XML_DEtype()
        { }//this.Any = new List<XmlElement>(); }
        public XML_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {

            //ElementPrefix = "xml";
            //SetNames(elementName, elementPrefix);
            //this.Any = new List<XmlElement>();
        }
    }

    public partial class XML_Stype
    {
        protected XML_Stype()
        { this.Any = new List<XmlElement>(); }
        public XML_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {

            ElementPrefix = "xml";
            SetNames(elementName, elementPrefix);
            this.Any = new List<XmlElement>();
        }
    }

    public partial class yearMonthDuration_DEtype
    {
        protected yearMonthDuration_DEtype() { }
        public yearMonthDuration_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.allowGT = false;
            this.allowGTE = false;
            this.allowLT = false;
            this.allowLTE = false;
            this.allowAPPROX = false;
            //ElementPrefix = "ymd";
            //SetNames(elementName, elementPrefix);
        }
    }

    public partial class yearMonthDuration_Stype
    {
        protected yearMonthDuration_Stype() { }
        public yearMonthDuration_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {

            ElementPrefix = "ymd";
            SetNames(elementName, elementPrefix);
            this.quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected yearMonthDuration_Stype() { }
    }
    #endregion

    #region Rules



    public partial class ItemNameType
    {
        protected ItemNameType() { }
        public ItemNameType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "itnm";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class NameType
    {
        protected NameType() { }
        public NameType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "nm";
            SetNames(elementName, elementPrefix);
        }
    }


    public partial class TargetItemIDType
    {
        protected TargetItemIDType() { }
        public TargetItemIDType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "tiid";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class TargetItemNameType
    {
        protected TargetItemNameType() { }
        public TargetItemNameType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "tinm";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class TargetItemXPathType
    {
        protected TargetItemXPathType() { }
        public TargetItemXPathType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "tixp";
            SetNames(elementName, elementPrefix);
        } //{if (elementName.Length > 0) ElementName = elementName; }
    }

    public partial class ParameterItemType : ExtensionBaseType
    {
        protected ParameterItemType() { }
        public ParameterItemType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.SourceItemAttribute = "val";
        }

    }

    public partial class CallFuncType : ExtensionBaseType
    {
        protected CallFuncType() { }
        public CallFuncType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.returnList = false;
        }

    }


    #endregion

    #region Events
    public partial class OnEventType
    {
        protected OnEventType() { }
        public OnEventType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "onev";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class RulesType
    {
        protected RulesType() { }
        public RulesType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "rul";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class EventType : ConditionalGroupActionType
    {
        protected EventType() { }
        public EventType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "evnt";
            SetNames(elementName, elementPrefix);
        }
    }


    public partial class ConditionalGroupActionType : FuncBoolBaseType
    {
        protected ConditionalGroupActionType() { }
        public ConditionalGroupActionType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "cga";
            SetNames(elementName, elementPrefix);
        }

    }

    public partial class FuncBoolBaseType : ExtensionBaseType
    {
        protected FuncBoolBaseType() { }
        public FuncBoolBaseType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "fbb";
            SetNames(elementName, elementPrefix);
        }

    }


    #endregion

    #region Contacts

    public partial class ContactType
    {
        protected ContactType() { }
        public ContactType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "cont";
            SetNames(elementName, elementPrefix);
        }

        public PersonType AddPerson()
        {
            return sdcTreeBuilder.AddPerson(this);
        }
        public OrganizationType AddOrganization()
        {
            return sdcTreeBuilder.AddFillOrganization(this);
        }

    }

    public partial class OrganizationType
    {
        protected OrganizationType() { }
        public OrganizationType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "org";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class PersonType
    {
        protected PersonType() { }
        public PersonType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "pers";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class AddressType
    {
        protected AddressType() { }
        public AddressType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "adrs";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class AreaCodeType
    {
        protected AreaCodeType() { }
        public AreaCodeType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "arcd";
            SetNames(elementName, elementPrefix);
        }
    }
    #endregion

    #region Resources
    public partial class RichTextType
    {
        protected RichTextType() { }
        public RichTextType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "rtt";
            SetNames(elementName, elementPrefix);
        }

        protected HTML_Stype AddHTML()
        {
            var h = sdcTreeBuilder.AddFillHTML(this);
            return h;
        }
    }

    public partial class CommentType
    {
        protected CommentType() { }
        public CommentType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {

            this.ElementPrefix = "cmt";
            SetNames(elementName, elementPrefix);

        }
    }

    #endregion

    #region Classes that need ctor parameters


    #region RequestForm (Package)
    public partial class ComplianceRuleType
    {
        protected ComplianceRuleType() { }
        public ComplianceRuleType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "cmpr";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class SubmissionRuleType
    {
        protected SubmissionRuleType() { }
        public SubmissionRuleType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "subr";
            SetNames(elementName, elementPrefix);
        }
    }


    public partial class HashType
    {
        protected HashType() { }
        public HashType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "hsh";
            SetNames(elementName, elementPrefix);
        }
    }


    public partial class IdentifierType
    {
        protected IdentifierType() { }
        public IdentifierType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "idn";
            SetNames(elementName, elementPrefix);

        }
    }

    public partial class LanguageCodeISO6393_Type
    {
        protected LanguageCodeISO6393_Type() { }
        public LanguageCodeISO6393_Type(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "lngc";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class LanguageType
    {
        protected LanguageType() { }
        public LanguageType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "lang";
            SetNames(elementName, elementPrefix);

        }
    }

    public partial class ProvenanceType
    {
        protected ProvenanceType() { }
        public ProvenanceType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "prv";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class ReplacedIDsType
    {
        protected ReplacedIDsType() { }
        public ReplacedIDsType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "rid";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class VersionType
    {
        protected VersionType() { }
        public VersionType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "ver";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class VersionTypeChanges
    {
        protected VersionTypeChanges() { }
        public VersionTypeChanges(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "vch";
            SetNames(elementName, elementPrefix);
        }
    }


    #endregion

    #region Contacts classes

    public partial class ContactsType
    {
        protected ContactsType() { }
        public ContactsType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "cont";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class CountryCodeType
    {
        protected CountryCodeType() { }
        public CountryCodeType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "cycd";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class DestinationType
    {
        protected DestinationType() { }
        public DestinationType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "dest";
            SetNames(elementName, elementPrefix);

        }
    }


    public partial class PhoneNumberType
    {
        protected PhoneNumberType() { }
        public PhoneNumberType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "phn";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class PhoneType
    {
        protected PhoneType() { }
        public PhoneType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "PhoneType";
            ElementPrefix = "pht";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class JobType
    {
        protected JobType() { }
        public JobType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "job";
            SetNames(elementName, elementPrefix);
        }
    }
    #endregion

    #region  Email
    public partial class EmailAddressType
    {
        protected EmailAddressType() { }
        public EmailAddressType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "emad";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class EmailType
    {
        protected EmailType() { }
        public EmailType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "eml";
            SetNames(elementName, elementPrefix);
            //this.Usage = new string_Stype();
            //this.EmailClass = new string_Stype();
            //this.EmailAddress = new EmailAddressType();
        }
    }

    #endregion

    #region Files


    public partial class ApprovalType
    {
        protected ApprovalType() { }
        public ApprovalType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "appr";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class AssociatedFilesType
    {
        protected AssociatedFilesType() { }
        public AssociatedFilesType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "asfils";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class AcceptabilityType
    {
        protected AcceptabilityType() { }
        public AcceptabilityType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "acc";
            SetNames(elementName, elementPrefix);
        }
    }


    public partial class FileDatesType
    {
        protected FileDatesType() { }
        public FileDatesType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "fildts";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class FileHashType
    {
        protected FileHashType() { }
        public FileHashType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "filhsh";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class FileType
    {
        protected FileType() { }
        public FileType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "file";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class FileUsageType
    {
        protected FileUsageType() { }
        public FileUsageType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "filus";
            SetNames(elementName, elementPrefix);
        }
    }

    #endregion

    #endregion


}


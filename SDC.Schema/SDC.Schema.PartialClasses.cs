
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

//using SDC.DAL.DataModel;

//!Handling Item and Items generic types derived from the xsd2Code++ code generator
namespace SDC
{
    public partial class FormDesignType
    {
        protected FormDesignType() { }
        public FormDesignType(ITreeBuilder treeBuilder, BaseType parentNode = null, bool fillData = false, string id = null)
        : base(parentNode, fillData, id)
        //TODO: add lineage, baseURI, version to this constructor
        {
            sdcTreeBuilder = treeBuilder;
            //FormDesignTreeBuilder = treeBuilder;
            //TODO: move treebuilder reference out of BaseClass to FormDesign, and remove static modifier
            //if (fillData) FillBaseTypeItem();  //this must be run after sdcTreeBuilder is assigned, and all sdcTreeBuilder data objects are initialized.
        }

        public SectionItemType AddBody(bool fillData = false, string id = null)
        {
            return sdcTreeBuilder.AddBody(fillData, id);
        }
        public SectionItemType AddFooter(bool fillData = false, string id = null)
        {
            return sdcTreeBuilder.AddFooter(fillData, id);
        }

        public SectionItemType AddHeader(bool fillData = false, string id = null)
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
        //internal readonly ITreeBuilder FormDesignTreeBuilder;
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
        public ActSendMessageType(ThenType parentNode) : base(parentNode) { }
        protected ActSendMessageType() { }

        /// <summary>
        /// List<BaseType> accepts: EmailAddressType, PhoneNumberType, WebServiceType
        /// </summary>
        internal List<SDC.ExtensionBaseType> Email_Phone_WebSvc_List
        {
            get { return this.Items; }
            set { this.Items = value; }
        }
    }

    public partial class ActSendReportType
    {
        protected ActSendReportType() { }
        public ActSendReportType(ThenType parentNode) : base(parentNode) { }

        internal List<ExtensionBaseType> Email_Phone_WebSvc_List
        {
            get { return this.Items; }
            set { this.Items = value; }
        }
    }

    //public partial class ActSetValueType
    //{
    //    public ActSetValueType(ThenType parentNode) : base(parentNode) { }
    //    protected ActSetValueType() { }

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
        public ActShowFormType(ThenType parentNode) : base(parentNode) { }
    }

    public partial class ActShowMessageType
    {
        protected ActShowMessageType() { }
        public ActShowMessageType(ThenType parentNode) : base(parentNode) { }
    }

    public partial class ActShowReportType
    {
        protected ActShowReportType() { }
        public ActShowReportType(ThenType parentNode) : base(parentNode) { }
    }

    public partial class ActValidateFormType
    {
        protected ActValidateFormType() { }
        public ActValidateFormType(ThenType parentNode) : base(parentNode)
        {
            this._validateDataTypes = false;
            this._validateRules = false;
        }

        //!+Replaced in original class: protected ActValidateFormType() { }
        public ActValidateFormType Fill_ActValidateFormType()
        { return null; }
    }

    //public partial class ActSetPropertyType
    //{
    //    public ActSetPropertyType(ThenType parentNode) : base(parentNode) { }
    //    protected ActSetPropertyType() { }
    //}
    public partial class ActActionType
    {
        protected ActActionType() { }
        public ActActionType(ThenType parentNode) : base(parentNode) { }
    }
    //add creation proc for each Then subtype

    public partial class ActInjectType
    {
        protected ActInjectType() { }
        public ActInjectType(ThenType parentNode) : base(parentNode) { }
    }

    public partial class ActSaveResponsesType
    {
        protected ActSaveResponsesType() { }
        public ActSaveResponsesType(ThenType parentNode) : base(parentNode) { }
    }

    public partial class ActAddCodeType
    {
        protected ActAddCodeType() { }
        public ActAddCodeType(ThenType parentNode) : base(parentNode) { }
    }

    #endregion

    #region Main Types
    public partial class ButtonItemType
    {
        public ButtonItemType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "ButtonAction";
            ElementPrefix = "B";
            SetNames(elementName, elementPrefix);
            //if (fillData) sdcTreeBuilder.FillButton(this);
        }
        protected ButtonItemType() { }
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
        internal SectionBaseType() { }
        internal SectionBaseType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData, id)
        {
            this._ordered = true;
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
        {

        }


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
        public QuestionItemType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData, id)
        {


        }
        public QuestionItemType() { }  //need public parameterless constructor to support generics

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
        {
            this._readOnly = false;
            ElementName = "Question";
            ElementPrefix = "Q";
            SetNames(elementName, elementPrefix);
            if (fillData) FillQuestionItemBase();
        }
        //!+Replaced in original class: protected QuestionItemBaseType() { }

        public QuestionItemBaseType FillQuestionItemBase()
        { return sdcTreeBuilder.FillQuestionItemBase(this); }

        [System.Xml.Serialization.XmlIgnore]
        public ListFieldType ListField_Item
        {
            get { return (ListFieldType)this._item; }
            set { this._item = value; }
        }

        [System.Xml.Serialization.XmlIgnore]
        public QuestionEnum QuestionType { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public ResponseFieldType ResponseField_Item
        {
            get { return (ResponseFieldType)this._item; }
            set { this._item = value; }
        }

        //AddListField
        //AddList or AddLookupField or AddResponseField
        //AddListItem

    }
    #endregion

    #region QAS ListItems and Lookups


    public partial class ListType
    {
        public ListType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "lst";
            SetNames(elementName, elementPrefix);
        }
        protected ListType() { }

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
            this._colTextDelimiter = "|";
            this._numCols = ((byte)(1));
            this._storedCol = ((byte)(1));
            this._minSelections = ((ushort)(1));
            this._maxSelections = ((ushort)(1));
            this._ordered = true;

            ElementPrefix = "lf";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillListField(this);
        }

        //!+Replaced in original class: protected  ListFieldType (){}
        //public ListFieldType FillListFieldType()
        //{ return sdcTreeBuilder.FillListField(this); }

        ///// <summary>
        ///// ListType to group ListItems
        ///// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public ListType List_Item
        {
            get { return (ListType)this._item; }
            set { this._item = value; }
        }
        /// <summary>
        /// Replaces Item
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public LookupEndPointType LookupEndpoint_Item
        {
            get { return (LookupEndPointType)this._item; }
            set { this._item = value; }
        }

    }

    public partial class ListItemType : IParent
    {
        public ListItemType() { }  //!+Replaced in original class: need public parameterless constructor to support generics
        public ListItemType(BaseType parentNode, bool fillData = true, string id = null, string elementName = "", string elementPrefix = "") : base(parentNode, fillData, id)
        {

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
        {
            this._selected = false;
            this._selectionDisablesChildren = false;
            this._selectionDeselectsSiblings = false;
            this._omitWhenSelected = false;
            this._repeat = "1";
            ElementPrefix = "LI";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillListItemBase(this);
        }
        //!+Replaced in original class: protected  ListItemBaseType (){}

        //public ListItemBaseType FillListItemBase()
        //{ return sdcTreeBuilder.FillListItemBase(this); }

        [NonSerialized]
        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, ListItemType> ListItems;

    }



    //public partial class FuncType
    //{
    //    public FuncType(BaseType parentNode, bool fillData = true) : base(parentNode) { }
    //    public FuncType() { }
    //}



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

    //public partial class WebServiceType
    //{
    //    public WebServiceType(BaseType parentNode) : base(parentNode) { }
    //    protected WebServiceType() { }

    //}

    //public partial class GetParameterFromPropertyType
    //{
    //    public GetParameterFromPropertyType(BaseType parentNode) : base(parentNode)
    //    { }
    //    protected GetParameterFromPropertyType() { }

    //}

    #endregion

    #region Responses

    public partial class ListItemResponseFieldType
    {
        public ListItemResponseFieldType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this._responseRequired = false;
            //if (fillData) AddFillDataTypesDE(parentNode);

            ElementPrefix = "lirf";
            SetNames(elementName, elementPrefix); //this was already called by the superType ResponseField.
            if (fillData) sdcTreeBuilder.FillListItemResponseField(this);
        }
        protected ListItemResponseFieldType() { }
        //!Replaced in original class: protected  ListItemResponseFieldType (){}

        //public ListItemResponseFieldType FillListItemResponseFieldType()
        //{ return sdcTreeBuilder.FillListItemResponseField(this); }

        //public ResponseFieldType GetResponseField()
        //{ return (ResponseFieldType)this; }

    }


    //public partial class ReplacedResponseType
    //{

    //    /// <summary>
    //    /// "Response", typeof(DataTypes_SType), IsNullable = true,
    //    /// </summary>
    //    internal DataTypes_SType Response_Item
    //    {
    //        get { return (DataTypes_SType)this._item; }
    //        set { this._item = value; }
    //    }
    //    public ReplacedResponseType(BaseType parentNode) : base(parentNode) { }
    //    protected ReplacedResponseType() { }

    //    // Replaces Item:
    //    //"Response", typeof(DataTypes_SType), IsNullable = true,
    //    //"SelectedItems", typeof(ChangedSelectedItemsType), IsNullable = true

    //    /// <summary>
    //    ///"SelectedItems", typeof(ChangedSelectedItemsType), IsNullable = true
    //    /// </summary>
    //    internal ChangedSelectedItemsType SelectedItems_Item
    //    {
    //        get { return (ChangedSelectedItemsType)this._item; }
    //        set { this._item = value; }
    //    }
    //}

    //public partial class ChangedFieldType
    //{
    //    //public ChangedFieldType(BaseType parentNode) : base(parentNode) { }

    //    /// <summary>
    //    /// TargetItemID, TargetItemIDType
    //    /// </summary>
    //    internal TargetItemIDType TargetItemID_Item
    //    {
    //        get { return (TargetItemIDType)this._item; }
    //        set { this._item = value; }
    //    }


    //    /// <summary>
    //    /// TargetItemName, TargetItemNameType
    //    /// </summary>
    //    internal TargetItemNameType TargetItemName_Item
    //    {
    //        get { return (TargetItemNameType)this._item; }
    //        set { this._item = value; }
    //    }

    //    /// <summary>
    //    /// TargetItemXPath, TargetItemXPathType
    //    /// </summary>
    //    internal TargetItemXPathType TargetItemXPath_Item
    //    {
    //        get { return (TargetItemXPathType)this._item; }
    //        set { this._item = value; }
    //    }
    //}

    public partial class ResponseFieldType
    {
        protected ResponseFieldType() { }
        public ResponseFieldType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            //if (fillData) sdcTreeBuilder.AddFillDataTypesDE(this);

            ElementPrefix = "rf";
            SetNames(elementName, elementPrefix);
            if (fillData) sdcTreeBuilder.FillResponseField(this);
            //sdcTreeBuilder.AddFillResponseUnits(this, fillData);
        }
    }

    public partial class UnitsType
    {
        protected UnitsType() { }
        public UnitsType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this._unitSystem = "UCUM";
            if (fillData) sdcTreeBuilder.FillUnits(this);
            ElementPrefix = "un";
            SetNames(elementName, elementPrefix);

        }
        //!+Replaced in original class: protected UnitsType() { }

        //public UnitsType FillUnitsType()
        //{ return sdcTreeBuilder.FillUnits(this); }
    }

    #endregion

    #endregion


    #endregion

    #region Base Types
    public partial class BaseType
    {

        #region  Local Members
        
        /// <summary>
        /// sdcTreeBuilder is an object instance created and held by the top level FormDesign node, 
        /// but referenced throughout the FormDesign object tree through the BaseType objects
        /// </summary>
        protected ITreeBuilder sdcTreeBuilder;
        private string _elementName = "";
        private string _elementPrefix = "";

        /// <summary>
        /// Static counter that resets with each new instance of an IdentifiedExtensionType (IET).
        /// Maintains the sequence of all elements under an IET.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        private static int IETresetCounter { get; set; }
        /// <summary>
        /// Field to hold the ordinal position of an object (XML element) under an IdentifiedExtensionType (IET).
        /// This number is used for creating the name attribute.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public decimal SubIETcounter { get; private set; }

        /// <summary>
        /// Used to construct the name property
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
        /// through this property directly after the object is instantiated
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
                    _elementPrefix = ElementName;                //make sure EventHandlerfirst letter is lower case for non-IET types.
                    if (!(GetType().IsSubclassOf(typeof(IdentifiedExtensionType)))) _elementPrefix = _elementPrefix.Substring(0, 1).ToLower() + _elementPrefix.Substring(1);
                }
                return _elementPrefix;
            }
            set { _elementPrefix = value; }
        }

        protected BaseType(BaseType parentNode, bool fillData = true) //: this()
        {
            ObjectGUID = Guid.NewGuid();
            IsLeafNode = true;
            ParentNode = parentNode;
            //BaseName = "";

            if (GetType().IsSubclassOf(typeof(IdentifiedExtensionType))) IETresetCounter = 0;
            else IETresetCounter++;
            SubIETcounter = IETresetCounter;

            //ElementName = GetType().ToString().Replace("Type", string.Empty).Replace("type", string.Empty); //assign default ElementName from the type.
            //ElementPrefix = ElementName; //default name prefix - better than nothing, but not great

            if (parentNode is null)
            {//this is a top level node, and it MUST be of type FormDesignType
                if (this.GetType() == typeof(FormDesignType))
                {
                    FormDesign = (FormDesignType)this;   //TopNode.FormDesign;
                }
                else
                { throw new InvalidOperationException("The top level node must be FormDesignType"); }
            }
            else
            {//a parent node is present, and it must NOT be of type FormDesign
                if (this.GetType() == typeof(FormDesignType))
                { throw new InvalidOperationException("Only the top level node can be FormDesignType"); }
                FormDesign = ParentNode.FormDesign;
                sdcTreeBuilder = FormDesign.sdcTreeBuilder;
                ParentNode.IsLeafNode = false;
            }

            ObjectID = FormDesign.MaxObjectID++;
            order = ObjectID;  //added 3/25/2018; this outputs the @order attribute for every element
            RegisterParent(parentNode);


            FormDesign.Nodes.Add(ObjectID, this);

            Debug.WriteLine($"The node with ObjectID: {this.ObjectID} has entered the BaseType ctor. Item type is {this.GetType()}.  " +
                $"The parent ObjectID is {this.ParentObjID.ToString()}, ParentIETypeID is: {this.ParentIETypeID}");


            if (fillData) FillBaseTypeItem();
        }

        protected BaseType()
        {
            Debug.WriteLine($"A node has entered the BaseType default ctor. Item type is {this.GetType()}");
            throw new System.InvalidOperationException("BaseType objects must use the constructor with a parentNode parameter");
        }
        ~BaseType() //destructor
        {
            FormDesign = null;
            sdcTreeBuilder = null;
            //prevent orphan nodes:
            //TODO: delete all child nodes here - lower descendants will delete their own child nodes
            //TODO: Remove this node from all FormDesign dictionaries
            //TODO: Reset IsLeafNode to false for the parent of this node
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



        public void SetNames(string elementName = "", string elementPrefix = "", string baseName="")
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
            //var type = new SDCtypes();
            //type = SDCHelpers.ConvertStringToEnum<SDCtypes>(typeName);

            switch (sdcType)
            {

                case SDCtypes.AcceptabilityType:
                    break;
                case SDCtypes.ActInjectType:
                    break;
                case SDCtypes.ActSaveResponsesType:
                    break;
                case SDCtypes.ActSendMessageType:
                    break;
                case SDCtypes.ActSendReportType:
                    break;
                case SDCtypes.ActSetValueType:
                    break;
                case SDCtypes.ActShowFormType:
                    break;
                case SDCtypes.ActShowMessageType:
                    break;
                case SDCtypes.ActShowReportType:
                    break;
                case SDCtypes.ActValidateFormType:
                    break;
                case SDCtypes.AddressType:
                    break;
                case SDCtypes.anyType_DEtype:
                    break;
                case SDCtypes.anyURI_DEtype:
                    break;
                case SDCtypes.anyURI_Stype:
                    break;
                case SDCtypes.ApprovalType:
                    break;
                case SDCtypes.AreaCodeType:
                    break;
                case SDCtypes.AssociatedFilesType:
                    break;
                case SDCtypes.base64Binary_DEtype:
                    break;
                case SDCtypes.base64Binary_Stype:
                    break;
                case SDCtypes.BlobType:
                    break;
                case SDCtypes.boolean_DEtype:
                    break;
                case SDCtypes.boolean_Stype:
                    break;
                case SDCtypes.ButtonItemType:
                    break;
                case SDCtypes.byte_DEtype:
                    break;
                case SDCtypes.byte_Stype:
                    break;
                case SDCtypes.ChangedFieldType:
                    break;
                case SDCtypes.ChangedListItemType:
                    break;
                case SDCtypes.ChangedSelectedItemsType:
                    break;
                case SDCtypes.ChangeLogType:
                    break;
                case SDCtypes.ChangeTrackingType:
                    break;
                case SDCtypes.ChangeType:
                    break;
                case SDCtypes.ChildItemsType:
                    break;
                case SDCtypes.CodeMatchType:
                    break;
                case SDCtypes.CodeSystemType:
                    break;
                case SDCtypes.CodingType:
                    break;
                case SDCtypes.CommentType:
                    break;
                case SDCtypes.ComplianceRuleType:
                    break;
                case SDCtypes.ContactsType:
                    break;
                case SDCtypes.ContactType:
                    break;
                case SDCtypes.CountryCodeType:
                    break;
                case SDCtypes.DataTypes_DEType:
                    break;
                case SDCtypes.DataTypes_SType:
                    break;
                case SDCtypes.date_DEtype:
                    break;
                case SDCtypes.date_Stype:
                    break;
                case SDCtypes.dateTime_DEtype:
                    break;
                case SDCtypes.dateTime_Stype:
                    break;
                case SDCtypes.dateTimeStamp_DEtype:
                    break;
                case SDCtypes.dateTimeStamp_Stype:
                    break;
                case SDCtypes.dayTimeDuration_DEtype:
                    break;
                case SDCtypes.dayTimeDuration_Stype:
                    break;
                case SDCtypes.decimal_DEtype:
                    break;
                case SDCtypes.decimal_Stype:
                    break;
                case SDCtypes.DestinationType:
                    break;
                case SDCtypes.DisplayedType:
                    break;
                case SDCtypes.double_DEtype:
                    break;
                case SDCtypes.double_Stype:
                    break;
                case SDCtypes.duration_DEtype:
                    break;
                case SDCtypes.duration_Stype:
                    break;
                case SDCtypes.EmailAddressType:
                    break;
                case SDCtypes.EmailType:
                    break;
                case SDCtypes.ExclusiveItemPairsType:
                    break;
                case SDCtypes.ExpressionType:
                    break;
                case SDCtypes.ExtensionBaseType:
                    break;
                case SDCtypes.ExtensionType:
                    break;
                case SDCtypes.FileDatesType:
                    break;
                case SDCtypes.FileHashType:
                    break;
                case SDCtypes.FileType:
                    break;
                case SDCtypes.FileUsageType:
                    break;
                case SDCtypes.float_DEtype:
                    break;
                case SDCtypes.float_Stype:
                    break;
                case SDCtypes.FormDesignType:
                    break;
                case SDCtypes.gDay_DEtype:
                    break;
                case SDCtypes.gDay_Stype:
                    break;
                case SDCtypes.GetCodeType:
                    break;
                case SDCtypes.GetPropertyValuesType:
                    break;
                case SDCtypes.gMonth_DEtype:
                    break;
                case SDCtypes.gMonth_Stype:
                    break;
                case SDCtypes.gMonthDay_DEtype:
                    break;
                case SDCtypes.gMonthDay_Stype:
                    break;
                case SDCtypes.gYear_DEtype:
                    break;
                case SDCtypes.gYear_Stype:
                    break;
                case SDCtypes.gYearMonth_DEtype:
                    break;
                case SDCtypes.gYearMonth_Stype:
                    break;
                case SDCtypes.HashType:
                    break;
                case SDCtypes.hexBinary_DEtype:
                    break;
                case SDCtypes.hexBinary_Stype:
                    break;
                case SDCtypes.HTML_DEtype:
                    break;
                case SDCtypes.HTML_Stype:
                    break;
                case SDCtypes.IdentifiedExtensionType:
                    break;
                case SDCtypes.IdentifierType:
                    break;
                case SDCtypes.IfBoolCompareType:
                    break;
                case SDCtypes.IfThenType:
                    break;
                case SDCtypes.IfType:
                    break;
                case SDCtypes.InjectFormType:
                    break;
                case SDCtypes.int_DEtype:
                    break;
                case SDCtypes.int_Stype:
                    break;
                case SDCtypes.integer_DEtype:
                    break;
                case SDCtypes.integer_Stype:
                    break;
                case SDCtypes.ItemNameType:
                    break;
                case SDCtypes.JobType:
                    break;
                case SDCtypes.LanguageCodeISO6393_Type:
                    break;
                case SDCtypes.LanguageType:
                    break;
                case SDCtypes.LinkType:
                    break;
                case SDCtypes.ListFieldType:
                    break;
                case SDCtypes.ListItemBaseType:
                    break;
                case SDCtypes.ListItemResponseFieldType:
                    break;
                case SDCtypes.ListItemType:
                    break;
                case SDCtypes.ListType:
                    break;
                case SDCtypes.long_DEtype:
                    break;
                case SDCtypes.long_Stype:
                    break;
                case SDCtypes.LookupEndPointType:
                    break;
                case SDCtypes.MaxExclusiveType:
                    break;
                case SDCtypes.MaxInclusiveType:
                    break;
                case SDCtypes.MinExclusiveType:
                    break;
                case SDCtypes.MinInclusiveType:
                    break;
                case SDCtypes.NameType:
                    break;
                case SDCtypes.negativeInteger_DEtype:
                    break;
                case SDCtypes.negativeInteger_Stype:
                    break;
                case SDCtypes.nonNegativeInteger_DEtype:
                    break;
                case SDCtypes.nonNegativeInteger_Stype:
                    break;
                case SDCtypes.nonPositiveInteger_DEtype:
                    break;
                case SDCtypes.nonPositiveInteger_Stype:
                    break;
                case SDCtypes.OnEventType:
                    break;
                case SDCtypes.OrganizationType:
                    break;
                case SDCtypes.ParameterType:
                    break;
                case SDCtypes.PersonType:
                    break;
                case SDCtypes.PhoneNumberType:
                    break;
                case SDCtypes.PhoneType:
                    break;
                case SDCtypes.positiveInteger_DEtype:
                    break;
                case SDCtypes.positiveInteger_Stype:
                    break;
                case SDCtypes.PredicateBetweenType:
                    break;
                case SDCtypes.PredicateCompareType:
                    break;
                case SDCtypes.PredicateInListType:
                    break;
                case SDCtypes.PredicateType:
                    break;
                case SDCtypes.ProvenanceType:
                    break;
                case SDCtypes.QuestionItemBaseType:
                    break;
                case SDCtypes.QuestionItemType:
                    break;
                case SDCtypes.RepeatingType:
                    break;
                case SDCtypes.ReplacedIDsType:
                    break;
                case SDCtypes.ReplacedResponseType:
                    break;
                case SDCtypes.ResponseChangeType:
                    break;
                case SDCtypes.ResponseFieldType:
                    break;
                case SDCtypes.RichTextType:
                    break;
                case SDCtypes.RulesType:
                    break;
                case SDCtypes.ScriptCodeType:
                    break;
                case SDCtypes.SectionBaseType:
                    break;
                case SDCtypes.SectionItemType:
                    break;
                case SDCtypes.SetPropertyType:
                    break;
                case SDCtypes.short_DEtype:
                    break;
                case SDCtypes.short_Stype:
                    break;
                case SDCtypes.string_DEtype:
                    break;
                case SDCtypes.string_Stype:
                    break;
                case SDCtypes.SubmissionRuleType:
                    break;
                case SDCtypes.TargetItemIDType:
                    break;
                case SDCtypes.TargetItemNameType:
                    break;
                case SDCtypes.TargetItemXPathType:
                    break;
                case SDCtypes.ThenType:
                    break;
                case SDCtypes.time_DEtype:
                    break;
                case SDCtypes.time_Stype:
                    break;
                case SDCtypes.UnitsType:
                    break;
                case SDCtypes.unsignedByte_DEtype:
                    break;
                case SDCtypes.unsignedByte_Stype:
                    break;
                case SDCtypes.unsignedInt_DEtype:
                    break;
                case SDCtypes.unsignedInt_Stype:
                    break;
                case SDCtypes.unsignedLong_DEtype:
                    break;
                case SDCtypes.unsignedLong_Stype:
                    break;
                case SDCtypes.unsignedShort_DEtype:
                    break;
                case SDCtypes.unsignedShort_Stype:
                    break;
                case SDCtypes.VersionType:
                    break;
                case SDCtypes.WatchedPropertyType:
                    break;
                case SDCtypes.WebServiceType:
                    break;
                case SDCtypes.XML_DEtype:
                    break;
                case SDCtypes.XML_Stype:
                    break;
                case SDCtypes.yearMonthDuration_DEtype:
                    break;
                case SDCtypes.yearMonthDuration_Stype:
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
            private set
            {
                _ParentNode = value;
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        public FormDesignType FormDesign { get; private set; }

        private void RegisterParent<T>(T inParentNode) where T : BaseType
        {
            try
            {
                if (inParentNode != null)
                {   //Register parent node
                    //ParentNode = inParentNode;
                    FormDesign.ParentNodes.Add(ObjectID, inParentNode);

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
        protected ExtensionBaseType(BaseType parentNode, bool fillData = true) : base(parentNode, fillData)
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

        //public IdentifiedExtensionType AddFillIdentifiedExtensionType(Boolean fillData = true)
        ////TODO: This function is not used
        //{
        //    //this.LastIdentifiedTypeID = this.ID;  //this allows sub-objects to access the parent identified item (type iet)
        //    base.LastDisplayedTypeObject = this;  //this allows sub-objects to access the parent identified item by the auto-assigned sequential number (ObjectID)

        //    var iet = sdcTreeBuilder.FillIdentifiedTypeItems(this, fillData);
        //    return iet;
        //}

    }

    public partial class RepeatingType //this is an SDC abstract class
    {
        protected RepeatingType() { }

        protected RepeatingType(BaseType parentNode, bool fillData = true, string id = null) : base(parentNode, fillData, id)
        {
            this._minCard = ((ushort)(1));
            this._maxCard = ((ushort)(1));
            this._repeat = "1";
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
            get { return this._items; }
            set { this._items = value; }
        }
    }



    #endregion

    #region DisplayedType and Helpers

    public partial class DisplayedType : IDisplayedType
    {
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

        protected DisplayedType() { }
        //!+Replaced in original class: protected DisplayedType() { }

        //internal DisplayedType FillDisplayedType(Boolean fillData = true)
        //{ return sdcTreeBuilder.FillDisplayedTypeItems(this, fillData); }

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
        public IfThenType AddOnEvent(Boolean fillData = true)
        { return sdcTreeBuilder.AddOnEvent(this, fillData); }
        public IfThenType AddOnEnter(Boolean fillData = true)
        { return sdcTreeBuilder.AddOnEnter(this, fillData); }
        public OnEventType AddOnExit(Boolean fillData = true)
        { return sdcTreeBuilder.AddOnExit(this, fillData); }
        public WatchedPropertyType AddActivateIf(Boolean fillData = true)
        { return sdcTreeBuilder.AddActivateIf(this, fillData); }
        public WatchedPropertyType AddDeActivateIf(Boolean fillData = true)
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
            var rtf = new SDC.RichTextType(this);
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
            this._codeMatchEnum = CodeMatchTypeCodeMatchEnum.ExactCodeMatch;
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
    #region Numeric metadata attributes


    //public partial class MaxExclusiveType
    //{
    //    public MaxExclusiveType(BaseType parentNode) : base(parentNode) { }
    //    protected MaxExclusiveType() { }
    //}

    //public partial class MaxInclusiveType
    //{
    //    public MaxInclusiveType(BaseType parentNode) : base(parentNode) { }
    //    protected MaxInclusiveType() { }
    //}

    //public partial class MinExclusiveType
    //{
    //    public MinExclusiveType(BaseType parentNode) : base(parentNode) { }
    //    protected MinExclusiveType() { }
    //}

    //public partial class MinInclusiveType
    //{
    //    public MinInclusiveType(BaseType parentNode) : base(parentNode) { }
    //    protected MinInclusiveType() { }
    //}
    #endregion
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
            get { return this._item; }
            set { this._item = value; }
        }
    }

    public partial class anyType_DEtype
    {
        public anyType_DEtype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "any";
            SetNames(elementName, elementPrefix);
        }
        protected anyType_DEtype() { }
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
            get { return this._item; }
            set { this._item = value; }
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
        string _base64StringVal;

        protected base64Binary_Stype() { }
        public base64Binary_Stype(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "b64";
            SetNames(elementName, elementPrefix);

        }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "string")] //changed to string
        public string valBase64
        {
            get { return _base64StringVal; }
            set { _base64StringVal = value; }
        }
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
            //this._quantEnum = dtQuantEnum.EQ;
        }
    }

    public partial class date_DEtype
    {
        protected date_DEtype() { }
        public date_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected date_Stype() { }
    }

    public partial class dateTime_DEtype
    {
        protected dateTime_DEtype() { }
        public dateTime_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected dateTime_Stype() { }
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected dayTimeDuration_Stype() { }
    }

    public partial class decimal_DEtype
    {
        protected decimal_DEtype() { }
        public decimal_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected decimal_Stype() { }
    }

    public partial class double_DEtype
    {
        protected double_DEtype() { }
        public double_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected double_Stype() { }
    }

    public partial class duration_DEtype
    {
        public duration_DEtype() { }
        public duration_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected duration_Stype() { }
    }

    public partial class float_DEtype
    {
        protected float_DEtype() { }
        public float_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected float_Stype() { }
    }

    public partial class gDay_DEtype
    {
        protected gDay_DEtype() { }
        public gDay_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
        //!+Replaced in original class: protected gDay_Stype() { }
    }

    public partial class gMonth_DEtype
    {
        protected gMonth_DEtype() { }
        public gMonth_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected gMonth_Stype() { }
    }

    public partial class gMonthDay_DEtype
    {
        protected gMonthDay_DEtype() { }
        public gMonthDay_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected gMonthDay_Stype() { }
    }

    public partial class gYear_DEtype
    {
        protected gYear_DEtype() { }
        public gYear_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected gYear_Stype() { }
    }

    public partial class gYearMonth_DEtype
    {
        protected gYearMonth_DEtype() { }
        public gYearMonth_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected gYearMonth_Stype() { }
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
            this.Any = new List<System.Xml.XmlElement>();
        }

    }


    public partial class int_DEtype
    {
        protected int_DEtype() { }
        public int_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected int_Stype() { }
    }

    public partial class integer_DEtype
    {
        protected integer_DEtype() { }
        public integer_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
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
                if (_val != null && _val.Length > 0)
                { return Convert.ToDecimal(this._val); }
                return null;
            }
            set
            {
                if (value != null)
                { this._val = Math.Truncate(value.Value).ToString(); }
                else this._val = null;

            }
        }






    }

    public partial class long_DEtype
    {
        protected long_DEtype() { }
        public long_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected long_Stype() { }
    }

    public partial class negativeInteger_DEtype
    {
        protected negativeInteger_DEtype() { }
        public negativeInteger_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected negativeInteger_Stype() { }
    }

    public partial class nonNegativeInteger_DEtype
    {
        protected nonNegativeInteger_DEtype() { }
        public nonNegativeInteger_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected nonNegativeInteger_Stype() { }
    }

    public partial class nonPositiveInteger_DEtype
    {
        protected nonPositiveInteger_DEtype() { }
        public nonPositiveInteger_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected nonPositiveInteger_Stype() { }
    }

    public partial class positiveInteger_DEtype
    {
        protected positiveInteger_DEtype() { }
        public positiveInteger_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected positiveInteger_Stype() { }
    }

    public partial class short_DEtype
    {
        protected short_DEtype() { }
        public short_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected short_Stype() { }
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected time_Stype() { }
    }

    public partial class unsignedByte_DEtype
    {
        protected unsignedByte_DEtype() { }
        public unsignedByte_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected unsignedByte_Stype() { }
    }

    public partial class unsignedInt_DEtype
    {
        protected unsignedInt_DEtype() { }
        public unsignedInt_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected unsignedInt_Stype() { }
    }

    public partial class unsignedLong_DEtype
    {
        protected unsignedLong_DEtype() { }
        public unsignedLong_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected unsignedLong_Stype() { }
    }

    public partial class unsignedShort_DEtype
    {
        protected unsignedShort_DEtype() { }
        public unsignedShort_DEtype(DataTypes_DEType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {

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
            this._quantEnum = dtQuantEnum.EQ;
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
            this._quantEnum = dtQuantEnum.EQ;
        }
        //!+Replaced in original class: protected yearMonthDuration_Stype() { }
    }
    #endregion

    #region Rules
    //public partial class ExpressionType
    //{
    //    public ExpressionType(BaseType parentNode) : base(parentNode) { }
    //    protected ExpressionType() { }

    //    /// <summary>
    //    /// Replaces Items List<ExtensionBaseType>: Reference,    ScriptCode,     WebService
    //    /// </summary>
    //    internal virtual List<ExtensionBaseType> Ref_Script_WebSvc_List
    //    {
    //        get { return this._items; }
    //        set { this._items = value; }
    //    }
    //}

    //public partial class GetCodeType
    //{
    //    public GetCodeType(BaseType parentNode) : base(parentNode) { }
    //    protected GetCodeType() { }
    //}

    //public partial class GetPropertyValuesType
    //{
    //    public GetPropertyValuesType(BaseType parentNode) : base(parentNode)
    //    {
    //        this._not = false;
    //        this._boolOp = GetPropertyValuesTypeBoolOp.AND;
    //    }
    //    protected GetPropertyValuesType() { }
    //    //!+Replaced in original class: protected GetPropertyValuesType() { }
    //}

    //public partial class IfBoolCompareType
    //{
    //    public IfBoolCompareType(BaseType parentNode) : base(parentNode)
    //    {
    //        this._not = false;
    //        this.boolOp = GetPropertyValuesTypeBoolOp.AND;
    //    }
    //    protected IfBoolCompareType() { }
    //    //!+Replaced in original class: protected IfBoolCompareType() { }
    //    public IfBoolCompareType Fill_IfBoolCompareType(BaseType parentNode)
    //    { return null; }
    //}

    //public partial class IfType
    //{
    //    public IfType(BaseType parentNode, bool fillData = true) : base(parentNode, fillData) { }
    //    protected IfType() { }

    //    /// <summary>
    //    /// Replaces Items: BoolCompare (If blocks)
    //    /// </summary>
    //    //internal List<IfBoolCompareType> BoolCompare_ItemList
    //    //{
    //    //    get { return this._items; }
    //    //    set { this._items = value; }
    //    //}

    //    /// <summary>
    //    /// ExclusiveSelectedItems (ExclusiveItemPairsType)
    //    /// </summary>
    //    //internal SelectionSetBoolType SelectionDependencies_Item
    //    //{
    //    //    get { return (SelectionSetBoolType)this._item; }
    //    //    set { this._item = value; }
    //    //}

    //    /// <summary>
    //    /// Predicate (PredicateType)
    //    /// </summary>
    //    //internal PredicateType Predicate_Item
    //    //{
    //    //    get { return (PredicateType)this._item; }
    //    //    set { this._item = value; }
    //    //}

    //    /// <summary>
    //    /// PropertyValues (GetPropertyValuesType)
    //    /// </summary>
    //    //internal GetPropertyValuesType PropertyValues_Item
    //    //{
    //    //    get { return (GetPropertyValuesType)this._item; }
    //    //    set { this._item = value; }
    //    //}
    //}

    //public partial class IfThenType
    //{
    //    protected IfThenType() { }
    //    public IfThenType(BaseType parentNode) : base(parentNode) { }
    //}


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


    //public partial class PredicateInListType
    //{
    //    public PredicateInListType(BaseType parentNode) : base(parentNode) { }
    //    protected PredicateInListType() { }
    //}


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
    //public partial class ExclusiveItemPairsType
    //{
    //    public ExclusiveItemPairsType(BaseType parentNode) : base(parentNode)
    //    {
    //        this._not = false;
    //    }
    //    //protected ExclusiveItemPairsType () { }
    //}

    //public partial class RuleIllegalSelectionSetsType
    //{
    //    public RuleIllegalSelectionSetsType(BaseType parentNode) : base(parentNode)
    //    {
    //        this._not = false;
    //    }
    //    protected RuleIllegalSelectionSetsType() { }
    //    //!+Replaced in original class: protected RuleIllegalSelectionSetsType () { }
    //}

    #endregion

    #region Events
    public partial class OnEventType
    {
        public OnEventType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "onev";
            SetNames(elementName, elementPrefix);
        }
        protected OnEventType() { }
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

    //public partial class ScriptCodeAnyType
    //{
    //    protected ScriptCodeAnyType() { }
    //        public ScriptCodeAnyType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
    //        {
    //            SetNames(elementName, elementPrefix);
    //        }
    //    }









    //public partial class ReturnType
    //{
    //    protected ReturnType() { }
    //    public ReturnType(BaseType parentNode, bool fillData = true) : base(parentNode, fillData) { }
    //}

    //public partial class ReturnBaseType
    //{
    //    protected ReturnBaseType() { }
    //    public ReturnBaseType(BaseType parentNode, bool fillData = true) : base(parentNode, fillData) { }
    //}









    public partial class EventType : ExtensionBaseType
    {
        protected EventType() { }
        public EventType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "evnt";
            SetNames(elementName, elementPrefix);
        }
    }

    //public partial class ThenType
    //{
    //    protected ThenType() { }
    //    public ThenType(BaseType parentNode, bool fillData = true) : base(parentNode, fillData) { }

    //    /// <summary>
    //    /// Replaces Items List<BaseType>:
    //    ///"Action", typeof(ExtensionType),
    //    ///"CallIfThen", typeof(ItemNameType),
    //    ///"IfThen", typeof(IfThenType),
    //    ///"Inject", typeof(ActInjectType),
    //    ///"RunCommand", typeof(ExpressionType),
    //    ///"Save", typeof(ActSaveResponsesType),
    //    ///"SetProperty", typeof(SetPropertyType),
    //    ///"SetValue", typeof(ActSetValueType),
    //    ///"ShowForm", typeof(ActShowFormType),
    //    ///"ShowMessage", typeof(ActShowMessageType),
    //    ///"ShowReport", typeof(ActShowReportType)
    //    ///"ShowURL", typeof(WebServiceType),
    //    ///"_SendMessage", typeof(ActSendMessageType),
    //    ///"_SendReport", typeof(ActSendReportType),
    //    ///"_ValidateForm", typeof(ActValidateFormType),
    //    /// </summary>
    //    internal ExtensionBaseType[] ThenItems_List  //This was changed from List<ExtensionBaseType>, and this may cause trouble since I usuallly work with lists, not arrays.
    //    {
    //        get { return this.Items; }
    //        set { this.Items = value; }
    //    }
    //}





    //public partial class WatchedPropertyType
    //{
    //    protected WatchedPropertyType() { }
    //        public WatchedPropertyType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
    //        {
    //            SetNames(elementName, elementPrefix);
    //            this._onlyIf = false;
    //        }

    //}


    #region Predicates
    //public partial class PredicateBetweenType
    //{
    //    public PredicateBetweenType(BaseType parentNode) : base(parentNode) { }
    //    public PredicateBetweenType() { }

    //    /// <summary>
    //    /// MinExclusive
    //    /// </summary>
    //    internal MinExclusiveType MinExclusive_Item
    //    {
    //        get { return (MinExclusiveType)this._item; }
    //        set { this._item = value; }
    //    }

    //    /// <summary>
    //    ///  MinInclusive
    //    /// </summary>
    //    internal MinInclusiveType MinInclusive_Item
    //    {
    //        get { return (MinInclusiveType)this._item; }
    //        set { this._item = value; }
    //    }

    //    /// <summary>
    //    /// MaxExclusive
    //    /// </summary>
    //    internal MaxExclusiveType MaxExclusive_Item
    //    {
    //        get { return (MaxExclusiveType)this._item1; }
    //        set { this._item1 = value; }
    //    }

    //    /// <summary>
    //    /// MaxInclusive
    //    /// </summary>
    //    internal MaxInclusiveType Max_Inclusive_Item
    //    {
    //        get { return (MaxInclusiveType)this._item1; }
    //        set { this._item1 = value; }
    //    }

    //}

    //public partial class PredicateCompareType
    //{
    //    public PredicateCompareType(BaseType parentNode) : base(parentNode) { }
    //    public PredicateCompareType() { }

    //    //"RHS_Expression", typeof(ExpressionType)
    //    //"RHS_Extension", typeof(ExtensionBaseType)
    //    //"RHS_GetCode", typeof(GetCodeType)
    //    //"RHS_GetResponse", typeof(ItemNameType)
    //    //"RHS_Value", typeof(DataTypes_SType)

    //    /// <summary>
    //    /// "RHS_Expression", typeof(ExpressionType)
    //    /// </summary>
    //    internal ExpressionType RHS_Expression_Item
    //    {
    //        get { return (ExpressionType)this._item; }
    //        set { this._item = value; }
    //    }
    //    /// <summary>
    //    /// "RHS_Extension", typeof(ExtensionBaseType)
    //    /// </summary>
    //    internal ExtensionBaseType RHS_Extension_Item
    //    {
    //        get { return (ExtensionBaseType)this._item; }
    //        set { this._item = value; }
    //    }
    //    /// <summary>
    //    /// "RHS_GetCode", typeof(GetCodeType)
    //    /// </summary>
    //    internal GetCodeType RHS_GetCode_Item
    //    {
    //        get { return (GetCodeType)this._item; }
    //        set { this._item = value; }
    //    }
    //    /// <summary>
    //    /// "RHS_GetResponse", typeof(ItemNameType)
    //    /// </summary>
    //    internal ItemNameType RHS_GetResponse_Item
    //    {
    //        get { return (ItemNameType)this._item; }
    //        set { this._item = value; }
    //    }
    //    /// <summary>
    //    /// "RHS_Value", typeof(DataTypes_SType)
    //    /// </summary>
    //    internal DataTypes_SType RHS_Value_Item
    //    {
    //        get { return (DataTypes_SType)this._item; }
    //        set { this._item = value; }
    //    }


    //}





    //public partial class ReturnBoolType
    //{

    //    public ReturnBoolType(BaseType parentNode, bool fillData = true) : base(parentNode, fillData) { }

    //    protected ReturnBoolType()
    //    {

    //    }
    //}





    //public partial class PredicateType
    //{
    //    protected PredicateType() { }
    //    public PredicateType(BaseType parentNode, bool fillData = true) : base(parentNode, fillData)
    //    {
    //        this._not = false;
    //        this._boolOp = BoolListTypeBoolOp.AND;
    //    }


    //}
    #endregion

    #endregion

    #region Contacts

    public partial class ContactType
    {
        public ContactType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "cont";
            SetNames(elementName, elementPrefix);
        }
        protected ContactType() { }

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
        public RichTextType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "rtt";
            SetNames(elementName, elementPrefix);
        }
        protected RichTextType() { }

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
        public ComplianceRuleType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "cmpr";
            SetNames(elementName, elementPrefix);
        }
        protected ComplianceRuleType() { }
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

        public ContactsType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            this.ElementPrefix = "cont";
            SetNames(elementName, elementPrefix);
        }
        protected ContactsType() { }
    }

    public partial class CountryCodeType
    {
        protected CountryCodeType() { }
        public CountryCodeType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "cocd";
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
            ElementPrefix = "pn";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class PhoneType
    {
        protected PhoneType() { }
        public PhoneType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementName = "PhoneType";
            ElementPrefix = "phn";
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
        public EmailAddressType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "emad";
            SetNames(elementName, elementPrefix);
        }
        protected EmailAddressType() { }
    }

    public partial class EmailType
    {
        protected EmailType() { }
        public EmailType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "eml";
            SetNames(elementName, elementPrefix);
        }
    }

    #endregion

    #region Files


    public partial class ApprovalType
    {
        public ApprovalType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "appr";
            SetNames(elementName, elementPrefix);
        }
        protected ApprovalType() { }
    }

    public partial class AssociatedFilesType
    {
        protected AssociatedFilesType() { }
        public AssociatedFilesType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "asfi";
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
            ElementPrefix = "fid";
            SetNames(elementName, elementPrefix);
        }
    }

    public partial class FileHashType
    {
        protected FileHashType() { }
        public FileHashType(BaseType parentNode, bool fillData = true, string elementName = "", string elementPrefix = "") : base(parentNode, fillData)
        {
            ElementPrefix = "fihsh";
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
            ElementPrefix = "fius";
            SetNames(elementName, elementPrefix);
        }
    }

    #endregion

    #endregion


}


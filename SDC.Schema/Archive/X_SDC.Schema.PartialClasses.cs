
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using System.Threading;
//using SDC.DAL.DataModel;



//!Handling Item and Items generic types derived from the xsd2Code++ code generator
namespace SDC
{

    public partial class FormDesignType
    {
        public ITreeBuilder InjectTreeBuilder
        {
            get { return sdcTreeBuilder; } 
            set { sdcTreeBuilder = value; } 
        }

        public SectionItemType AddHeader()
        {
            var s = sdcTreeBuilder.AddSectionToFormDesign(this);
            this.Header = s;
            return s;
        }
        public SectionItemType AddBody()
        {
            var s = new SectionItemType();
            this.Body = s;
            return s;
        }
        public SectionItemType AddFooter()
        {
            var s = new SectionItemType();
            this.Footer = s;
            return s;
        }
        public RulesType AddRules()
        {
            var r = new RulesType();
            this.Rules = r;
            return r;
        }
    }

    #region  Actions
    public partial class ActSendMessageType
    {
        /// <summary>
        /// List<BaseType> accepts: EmailAddressType, PhoneNumberType, WebServiceType
        /// </summary>
        internal List<SDC.BaseType> Email_Phone_WebSvc_List
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

    }

    public partial class ActSendReportType
    {

        internal List<BaseType> Email_Phone_WebSvc_List
        {
            get { return this.Items; }
            set { this.Items = value; }
        }
    }

    public partial class ActSetValueType
    {
        internal GetCodeType Code_Item
        {
            get { return (GetCodeType)this.Item; }
            set { this.Item = (SDC.BaseType)value; }
        }

        internal ExpressionType Expression_Item
        {
            get { return (ExpressionType)this.Item; }
            set { this.Item = (SDC.BaseType)value; }
        }

        internal ItemNameType ResponseValue_Item
        {
            get { return (ItemNameType)this.Item; }
            set { this.Item = (SDC.BaseType)value; }
        }

        internal DataTypes_SType ValueType_Item
        {
            get { return (DataTypes_SType)this.Item; }
            set { this.Item = (SDC.BaseType)value; }
        }
    }

    public partial class ActShowFormType
    {
        public ActShowFormType() { }
    }

    public partial class ActShowMessageType
    {
        public ActShowMessageType() { }
    }

    public partial class ActShowReportType
    {
        public ActShowReportType() { }
    }

    public partial class ActValidateFormType
    {
        public void Fill_ActValidateFormType()
        { }
    }
    #endregion

    public partial class AddressType
    {
        public AddressType() { }
    }

    public partial class base64Binary_Stype
    {
        string _base64StringVal;

        public base64Binary_Stype() { }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "string")] //changed to string
        public string valBase64
        {
            get { return _base64StringVal; }
            set { _base64StringVal = value; }
        }
    }


    public partial class base64Binary_DEtype
    {
        public base64Binary_DEtype() { }

    }

    public partial class BaseType
    {
        public BaseType() { }

        public BaseType FillBaseType()
        { return sdcTreeBuilder.AddBaseTypeItems(this); }

        public string ParentID { get; set; }
        internal static ITreeBuilder sdcTreeBuilder;

        /// <summary>
        /// Dictionary that hold IChildItems nodes (Sections, Questions, ListItems) and their IDs
        /// </summary>
        public static Dictionary<String, IChildItems> sdcParentNodes = new Dictionary<String, IChildItems>();
        /// <summary>
        /// Returns the parent node of the input node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IChildItems GetParentObject(BaseType node)
        {   var parentNode = sdcParentNodes[node.ParentID]; //this is the parent of node
            return (IChildItems)parentNode;
        }
        public IChildItems GetParentObject()
        {
            var parentNode = sdcParentNodes[this.ParentID]; //this is the parent of node
            return (IChildItems)parentNode;
        }
    }

    public partial class ButtonItemType
    {
        public ButtonItemType() { }

    }

    public partial class IdentifiedExtensionType
    {
        internal IdentifiedExtensionType() { }

        public IdentifiedExtensionType FillIdentifiedExtensionType()
        { return sdcTreeBuilder.AddIdentifiedTypeItems(this); }

    }

    public partial class ExtensionType
    {
        public ExtensionType() { }

    }

    public partial class ExtensionBaseType
    {
        public ExtensionBaseType() {}

        public ExtensionBaseType FillExtensionBaseType()
        { return sdcTreeBuilder.AddExtensionBaseTypeItems(this); }
        
        public ExtensionType AddExtension()
        { return sdcTreeBuilder.AddExtension(this); }
        public CommentType AddComment()
        { return sdcTreeBuilder.AddComment(this); }
    }

    public partial class ChangedFieldType
    {
        public ChangedFieldType() { }

        /// <summary>
        /// TargetItemID, TargetItemIDType
        /// </summary>
        internal TargetItemIDType TargetItemID_Item
        {
            get { return (TargetItemIDType)this._item; }
            set { this._item = value; }
        }


        /// <summary>
        /// TargetItemName, TargetItemNameType
        /// </summary>
        internal TargetItemNameType TargetItemName_Item
        {
            get { return (TargetItemNameType)this._item; }
            set { this._item = value; }
        }

        /// <summary>
        /// TargetItemXPath, TargetItemXPathType
        /// </summary>
        internal TargetItemXPathType TargetItemXPath_Item
        {
            get { return (TargetItemXPathType)this._item; }
            set { this._item = value; }
        }
    }



    public partial class ChildItemsType
    {
        public ChildItemsType() { }

        public List<IdentifiedExtensionType> ListOfItems
        {
            get { return this._items; }
            set { this._items = value; }
        }
    }

    public partial class CommentType
    {
        public CommentType() { }
    }

    public partial class ContactType
    {
        public ContactType() { }
        
        public PersonType AddPerson()
        {
            return sdcTreeBuilder.AddPerson(this);
        }
        public OrganizationType AddOrganization()
        {
            return sdcTreeBuilder.AddOrganization(this);
        }

    }

    public partial class DataTypes_DEType
    {
        public DataTypes_DEType() { }

        /// <summary>
        /// any *_DEType data type 
        /// </summary>
        public BaseType DataTypeDE_Item
        {
            get { return this._item; }
            set { this._item = value; }
        }
    }

    public partial class DataTypes_SType
    {
        public DataTypes_SType() { }

        /// <summary>
        /// any *_SType data type 
        /// </summary>
        public BaseType DataTypeS_Item
        {
            get { return this._item; }
            set { this._item = value; }
        }
    }

    public partial class DisplayedType : IDisplayedType
    {
        //public DisplayedType():base() { }

        internal DisplayedType FillDisplayedType()
        { return sdcTreeBuilder.AddDisplayedTypeItems(this); }

        #region IDisplayedType
        public RichTextType AddOtherText()
        { return sdcTreeBuilder.AddOtherText(this); }
        public LinkType AddLink()
        { return sdcTreeBuilder.AddLink(this); }
        public BlobType AddBlob()
        { return sdcTreeBuilder.AddBlob(this); }
        public ContactType AddContact()
        { return sdcTreeBuilder.AddContact(this); }
        public CodingType AddCoding()
        { return sdcTreeBuilder.AddCodedValue(this); }

        #region Events
        public IfThenType AddOnEvent()
        { return sdcTreeBuilder.AddOnEvent(this); }
        public IfThenType AddOnEnter()
        { return sdcTreeBuilder.AddOnEnter(this); }
        public OnEventType AddOnExit()
        { return sdcTreeBuilder.AddOnExit(this); }
        public WatchedPropertyType AddActivateIf()
        { return sdcTreeBuilder.AddActivateIf(this); }
        public WatchedPropertyType AddDeActivateIf()
        { return sdcTreeBuilder.AddDeActivateIf(this); }
        #endregion


        #endregion

    }

    public partial class ExpressionType
    {
        public ExpressionType() { }

        /// <summary>
        /// Replaces Items List<BaseType>: Reference,    ScriptCode,     WebService
        /// </summary>
        internal virtual List<BaseType> Ref_Script_WebSvc_List
        {
            get { return this._items; }
            set { this._items = value; }
        }
    }

    public partial class GetCodeType
    {
        public GetCodeType() { }
    }

    public partial class GetPropertyValuesType
    {
        public void Fill_GetPropertyValuesType()
        { }
    }

    public partial class hexBinary_Stype
    {
        string _hexBinaryStringVal;

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "string")] //changed to string
        public string valHex
        {
            get { return _hexBinaryStringVal; }
            set { _hexBinaryStringVal = value; }
        }
    }

    public partial class hexBinary_DEtype
    {

    }

    public partial class HTML_DEtype
    {
        public HTML_DEtype()
            : base()
        {
            this.Any = new List<System.Xml.XmlElement>();
        }
    }

    public partial class HTML_Stype
    {
        public HTML_Stype()
            : base()
        {
            this.Any = new List<System.Xml.XmlElement>();
        }

    }

    public partial class IfBoolCompareType
    {
        public void Fill_IfBoolCompareType()
        { }
    }

    public partial class IfType
    {
        public IfType() { }


        /// <summary>
        /// Predicate (PredicateType)
        /// </summary>
        internal PredicateType Predicate_Item
        {
            get { return (PredicateType)this._item; }
            set { this._item = value; }
        }

        /// <summary>
        /// ExclusiveSelectedItems (ExclusiveItemPairsType)
        /// </summary>
        internal ExclusiveItemPairsType ExclusiveSelectedItems_Item
        {
            get { return (ExclusiveItemPairsType)this._item; }
            set { this._item = value; }
        }

        /// <summary>
        /// PropertyValues (GetPropertyValuesType)
        /// </summary>
        internal GetPropertyValuesType PropertyValues_Item
        {
            get { return (GetPropertyValuesType)this._item; }
            set { this._item = value; }
        }

        /// <summary>
        /// Replaces Items: BoolCompare (If blocks)
        /// </summary>
        internal List<IfBoolCompareType> BoolCompare_ItemList
        {
            get { return this._items; }
            set { this._items = value; }
        }
    }

    public partial class IfThenType
    {
        public IfThenType() { }
    }

    public partial class InjectFormType
    {
        public InjectFormType() { }
    }

    public partial class ItemNameType
    {
        public ItemNameType() { }
    }

    public partial class ListType
    {
        public ListType() { }

        /// <summary>
        /// Replaces Items; ListItem or DisplayedItem
        /// </summary>
        public List<DisplayedType> DisplayedItem_List
        {
            get { return this._items; }
            set { this._items = value; }
        }
    }


    public partial class ListFieldType
    {
        public void Fill_ListFieldType()
        { }

        /// <summary>
        /// ListType to group ListItems
        /// </summary>
        public ListType List_Item
        {
            get { return (ListType)this._item; }
            set { this._item = value; }
        }
        /// <summary>
        /// Replaces Item
        /// </summary>
        public LookupEndPointType LookupEndpoint_Item
        {
            get { return (LookupEndPointType)this._item; }
            set { this._item = value; }
        }

    }

    public partial class ListItemType : IChildItems
    {
        public ListItemType() { }

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
        public ChildItemsType ChildItems_Item
        {
            get { return (ChildItemsType)this.Item; }
            set { this.Item = value; }
        }
        public SectionItemType AddSection()
        { return sdcTreeBuilder.AddSection<ListItemType>(this); }
        public QuestionItemType AddQuestion(ItemType qType)
        { return sdcTreeBuilder.AddQuestion<ListItemType>(this, qType); }
        public InjectFormType AddInjectedForm()
        { return sdcTreeBuilder.AddInjectedForm<ListItemType>(this); }
        public ButtonItemType AddButtonAction()
        { return sdcTreeBuilder.AddButtonAction<ListItemType>(this); }
        public DisplayedType AddDisplayedItem()
        { return sdcTreeBuilder.AddDisplayedItem<ListItemType>(this); }
        #endregion


    }

    public partial class ListItemBaseType
    {
        public void Fill_ListItemBaseType()
        { }
    }

    public partial class ListItemResponseFieldType
    {
        public void Fill_ListItemResponseFieldType()
        { }

        public ResponseFieldType GetResponseField()
        { return (ResponseFieldType)this; }

    }

    public partial class LookupEndPointType
    {
        public void Fill_LookupEndPointType()
        { }
    }

    //MaxExclusiveType
    public partial class MaxExclusiveType
    {
        public MaxExclusiveType() { }
    }

    public partial class MaxInclusiveType
    {
        public MaxInclusiveType() { }
    }

    public partial class MinExclusiveType
    {
        public MinExclusiveType() { }
    }

    public partial class MinInclusiveType
    {
        public MinInclusiveType() { }
    }

    public partial class OnEventType
    {
        public OnEventType() { }
    }

    public partial class OrganizationType
    {
        public OrganizationType() { }
    }

    public partial class ParameterType
    {
        public ParameterType() { }
    }

    public partial class PersonType
    {
        public PersonType() { }
    }

    public partial class PredicateBetweenType
    {
        public PredicateBetweenType() { }

        /// <summary>
        /// MinExclusive
        /// </summary>
        internal MinExclusiveType MinExclusive_Item
        {
            get { return (MinExclusiveType)this._item; }
            set { this._item = value; }
        }

        /// <summary>
        ///  MinInclusive
        /// </summary>
        internal MinInclusiveType MinInclusive_Item
        {
            get { return (MinInclusiveType)this._item; }
            set { this._item = value; }
        }

        /// <summary>
        /// MaxExclusive
        /// </summary>
        internal MaxExclusiveType MaxExclusive_Item
        {
            get { return (MaxExclusiveType)this._item1; }
            set { this._item1 = value; }
        }

        /// <summary>
        /// MaxInclusive
        /// </summary>
        internal MaxInclusiveType Max_Inclusive_Item
        {
            get { return (MaxInclusiveType)this._item1; }
            set { this._item1 = value; }
        }

    }

    public partial class PredicateCompareType
    {
        public PredicateCompareType() { }

        //"RHS_Expression", typeof(ExpressionType)
        //"RHS_Extension", typeof(ExtensionBaseType)
        //"RHS_GetCode", typeof(GetCodeType)
        //"RHS_GetResponse", typeof(ItemNameType)
        //"RHS_Value", typeof(DataTypes_SType)

        /// <summary>
        /// "RHS_Expression", typeof(ExpressionType)
        /// </summary>
        internal ExpressionType RHS_Expression_Item
        {
            get { return (ExpressionType)this._item; }
            set { this._item = value; }
        }
        /// <summary>
        /// "RHS_Extension", typeof(ExtensionBaseType)
        /// </summary>
        internal ExtensionBaseType RHS_Extension_Item
        {
            get { return (ExtensionBaseType)this._item; }
            set { this._item = value; }
        }
        /// <summary>
        /// "RHS_GetCode", typeof(GetCodeType)
        /// </summary>
        internal GetCodeType RHS_GetCode_Item
        {
            get { return (GetCodeType)this._item; }
            set { this._item = value; }
        }
        /// <summary>
        /// "RHS_GetResponse", typeof(ItemNameType)
        /// </summary>
        internal ItemNameType RHS_GetResponse_Item
        {
            get { return (ItemNameType)this._item; }
            set { this._item = value; }
        }
        /// <summary>
        /// "RHS_Value", typeof(DataTypes_SType)
        /// </summary>
        internal DataTypes_SType RHS_Value_Item
        {
            get { return (DataTypes_SType)this._item; }
            set { this._item = value; }
        }


    }

    public partial class PredicateType
    {
        public PredicateType() { }


        //+-------------------Item----------------------------
        //"LHS_Expression", typeof(ExpressionType), 
        //"LHS_Extension", typeof(ExtensionBaseType), 
        //"LHS_GetCode", typeof(GetCodeType), 
        //"LHS_GetResponse", typeof(ItemNameType)  

        /// <summary>
        /// "LHS_Expression", typeof(ExpressionType) 
        /// </summary>
        internal ExpressionType LHS_Expression_Item
        {
            get { return (ExpressionType)this._item; }
            set { this._item = value; }
        }

        /// <summary>
        ///"LHS_Extension", typeof(ExtensionBaseType)
        /// </summary>
        internal ExtensionBaseType LHS_Extension_Item
        {
            get { return (ExtensionBaseType)this._item; }
            set { this._item = value; }
        }

        /// <summary>
        ///"LHS_GetCode", typeof(GetCodeType),        
        /// </summary>
        internal GetCodeType LHS_GetCode_Item
        {
            get { return (GetCodeType)this._item; }
            set { this._item = value; }
        }

        /// <summary>
        /// "LHS_GetResponse", typeof(ItemNameType)        
        /// </summary>
        internal ItemNameType LHS_GetResponse_Item
        {
            get { return (ItemNameType)this._item; }
            set { this._item = value; }
        }

        //+-------------------Item1----------------------------
        /// 
        //"Between", typeof(PredicateBetweenType), 
        //"Compare", typeof(PredicateCompareType), 
        //"InList", typeof(PredicateInListType)  

        /// <summary>
        /// 
        ///"Between", typeof(PredicateBetweenType),        
        /// </summary>
        internal PredicateBetweenType Between_Item
        {
            get { return (PredicateBetweenType)this._item1; }
            set { this._item1 = value; }
        }

        /// <summary>
        ///"Compare", typeof(PredicateCompareType),       
        /// </summary>
        internal PredicateCompareType Compare_Item
        {
            get { return (PredicateCompareType)this._item1; }
            set { this._item1 = value; }
        }

        /// <summary>
        ///"InList", typeof(PredicateInListType)        
        /// </summary>
        internal PredicateInListType InList_Item
        {
            get { return (PredicateInListType)this._item1; }
            set { this._item1 = value; }
        }

    }

    public partial class QuestionItemType : IChildItems
    {
        public QuestionItemType() { }



        #region IChildItems
        public ChildItemsType ChildItems_Item
        {
            get { return (ChildItemsType)this.Item1; }
            set { this.Item1 = value; }
        }
        public SectionItemType AddSection()
        { return sdcTreeBuilder.AddSection<QuestionItemType>(this); }
        public QuestionItemType AddQuestion(ItemType qType)
        { return sdcTreeBuilder.AddQuestion<QuestionItemType>(this, qType); }
        public InjectFormType AddInjectedForm()
        { return sdcTreeBuilder.AddInjectedForm<QuestionItemType>(this); }
        public ButtonItemType AddButtonAction()
        { return sdcTreeBuilder.AddButtonAction<QuestionItemType>(this); }
        public DisplayedType AddDisplayedItem()
        { return sdcTreeBuilder.AddDisplayedItem<QuestionItemType>(this); }
        #endregion

    }

    public partial class QuestionItemBaseType
    {
        public void Fill_QuestionItemBaseType()
        { }

        /// <summary>
        /// ListField (ListFieldType)
        /// </summary>
        public ListFieldType ListField_Item
        {
            get { return (ListFieldType)this._item; }
            set { this._item = value; }
        }
        /// <summary>
        /// ResponseField (ResponseFieldType)
        /// </summary>
        public ResponseFieldType ResponseField_Item
        {
            get { return (ResponseFieldType)this._item; }
            set { this._item = value; }
        }

        //AddListField
        //AddList or AddLookupField or AddResponseField
        //AddListItem

    }

    public partial class RichTextType
    {
        public RichTextType() { }
        internal HTML_Stype AddHTML()
        {
            var h = sdcTreeBuilder.AddHTML(this);
            return (HTML_Stype)h;
        }
    }

    public partial class RepeatingType
    {
        public void Fill_RepeatingType()
        { }

    }

    public partial class ReplacedResponseType
    {
        public ReplacedResponseType() { }

        // Replaces Item:
        //"Response", typeof(DataTypes_SType), IsNullable = true, 
        //"SelectedItems", typeof(ChangedSelectedItemsType), IsNullable = true

        /// <summary>
        ///"SelectedItems", typeof(ChangedSelectedItemsType), IsNullable = true
        /// </summary>
        internal ChangedSelectedItemsType SelectedItems_Item
        {
            get { return (ChangedSelectedItemsType)this._item; }
            set { this._item = value; }
        }

        /// <summary>
        /// "Response", typeof(DataTypes_SType), IsNullable = true, 
        /// </summary>
        internal DataTypes_SType Response_Item
        {
            get { return (DataTypes_SType)this._item; }
            set { this._item = value; }
        }
    }

    public partial class ResponseFieldType
    {
        public ResponseFieldType() { }
    }

    public partial class RulesType
    {
        public RulesType() { }
    }

    public partial class ScriptCodeType
    {
        public ScriptCodeType() { }
    }

    public partial class SectionBaseType
    {
        public void Fill_SectionBaseType()
        { }
    }
    public partial class SectionItemType : IChildItems
    {
        public SectionItemType() { }

        #region IChildItems
        public ChildItemsType ChildItems_Item
        {
            get { return (ChildItemsType)this.Item; }
            set { this.Item = value; }
        }
        public SectionItemType AddSection()
        { return sdcTreeBuilder.AddSection<SectionItemType>(this); }
        public QuestionItemType AddQuestion(ItemType qType)
        { return sdcTreeBuilder.AddQuestion<SectionItemType>(this, qType); }
        public InjectFormType AddInjectedForm()
        { return sdcTreeBuilder.AddInjectedForm<SectionItemType>(this); }
        public ButtonItemType AddButtonAction()
        { return sdcTreeBuilder.AddButtonAction<SectionItemType>(this); }
        public DisplayedType AddDisplayedItem()
        { return sdcTreeBuilder.AddDisplayedItem<SectionItemType>(this); }
        #endregion
    }

    public partial class SetPropertyType
    {
        public SetPropertyType() { }
    }

    public partial class SetResponseValue
    {
        public SetResponseValue() { }
    }

    public partial class ThenType
    {
        public ThenType() { }
        /// <summary>
        /// Replaces Items List<BaseType>: 
        ///"Action", typeof(ExtensionType),  
        ///"CallIfThen", typeof(ItemNameType),  
        ///"IfThen", typeof(IfThenType),  
        ///"Inject", typeof(ActInjectType),  
        ///"RunCommand", typeof(ExpressionType),  
        ///"Save", typeof(ActSaveResponsesType),  
        ///"SetProperty", typeof(SetPropertyType),  
        ///"SetValue", typeof(ActSetValueType),  
        ///"ShowForm", typeof(ActShowFormType),  
        ///"ShowMessage", typeof(ActShowMessageType),  
        ///"ShowReport", typeof(ActShowReportType)
        ///"ShowURL", typeof(WebServiceType),  
        ///"_SendMessage", typeof(ActSendMessageType),  
        ///"_SendReport", typeof(ActSendReportType),  
        ///"_ValidateForm", typeof(ActValidateFormType),  
        /// </summary>
        internal List<SDC.BaseType> ThenItems_List
        {
            get { return this.Items; }
            set { this.Items = value; }
        }

        //add creation proc for each Then subtype
    }

    public partial class UnitsType
    {
        public void Fill_UnitsType()
        { }
    }

    public partial class WatchedPropertyType
    {
        public WatchedPropertyType() { }
    }

    public partial class WebServiceType
    {
        public WebServiceType() { }
    }

    public partial class XML_DEtype
    {
        public XML_DEtype()
        {
            this.Any = new List<XmlElement>();
        }
    }

    public partial class XML_Stype
    {
        public XML_Stype()
        {
            this.Any = new List<XmlElement>();
        }
    }

}


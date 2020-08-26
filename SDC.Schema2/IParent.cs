using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks.Sources;
using Newtonsoft.Json;

//using SDC;
namespace SDC.Schema2
{
    /// <summary>
    /// This interface is applied to the partial classes that must support the ChildItems element.
    /// These are Section, Question and ListItem.  
    /// This interface is require to support generic classes that must handle the creation ofthe 
    /// ChildItems element, which holds List of type IdentifiedItemType
    /// </summary>
    public interface IParent  //implemented by items that can have a ChildItems node.
    {
        ChildItemsType ChildItemsNode { get; set; }
        //C AddChildItem<P, C>(P parent, string childID = "", int insertPosition = -1) where P : IParent where C : IChildItem;
        //bool RemoveChildItem<C>(C childItem) where C : IChildItem;
        //bool MoveChildItem<P, C>(C childItem, int newPosition, P targetParent) where P : IParent where C : IChildItem;

        SectionItemType AddChildSection(string childID = "", int insertPosition = -1);
        QuestionItemType AddChildQuestion(QuestionEnum qType, string childID = "", int insertPosition = -1);
        InjectFormType AddChildInjectedForm(string childID = "", int insertPosition = -1);
        ButtonItemType AddChildButtonAction(string childID = "", int insertPosition = -1);
        DisplayedType AddChildDisplayedItem(string childID = "", int insertPosition = -1);
        //Remove all child nodes 


        //QR AddChildQR(string id = "", int insertPosition = -1);
        //QS AddChildQS(string id = "", int insertPosition = -1);
        //QM AddChildQM(string id = "", int insertPosition = -1);
        //QL AddChildQL(string id = "", int insertPosition = -1);

    }

    public interface IChildItemMember //Marks SectionItemType, QuestionItemType, DisplayedType, ButtonItemType, InjectFormType
    {
        //bool Remove<T>() where T : IdentifiedExtensionType, IUnderChildItem;
        bool Remove();
        bool Move<T>(T target = null, int newListIndex = -1) where T : DisplayedType, IParent;        
        //CanRemove
        //CanMove (target ChildItems)
        //ChildCount

    }

    public interface IQuestionItem: IQuestionList
    {
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        public QuestionEnum GetQuestionSubtype { get; }

        //convert type to QR
        //Convert to S, DI    (must first delete List or ResponseField present)
        //Convert QR to QS    (delete ResponseField, add List, set maxSelections)
        //Convert QR to QM    (delete ResponseField, add List, set maxSelections)
        //LookupEndPointType AddLookupEndpoint(ListFieldType lfParent);  //should be part of AddChildQL code;
        //CanConvert (to Type)

    }
    public interface IQuestionList
    {
        ListItemType AddListItem(string id = "", int insertPosition = -1); //check that no ListItemResponseField object is present
        ListItemType AddListItemResponse(string id = "", int insertPosition = -1); //check that no ListFieldType object is present
        DisplayedType AddDisplayedTypeToList(string id = "", int insertPosition = -1);
    }
    public interface IListField
    {
        public ListType List { get; set; }
        public LookupEndPointType LookupEndpoint { get; set; }

    }
    public interface IQuestionBase
    {
        public ListFieldType ListField_Item { get; set;}
        public ResponseFieldType ResponseField_Item { get; set;}
    }
    public interface IListItem
    {
        public ListItemResponseFieldType AddListItemResponseField(ListItemBaseType li);
        //Convert to DI (Delete LIRF if present)
        //Convert to LIR (Add LIRF)
        //Convert to LI (Delete LIRF if present)
        //Remove  me (Check for descendants?)
        //Move me (Q, List index)

    }
    public interface IExtensionBase
    {
        public CommentType AddComment(int insertPosition = -1);
        public ExtensionType AddExtension(int insertPosition = -1);
        public PropertyType AddProperty(int insertPosition = -1);
       
    }


    public interface IComment
    {
        public bool Remove();
        public bool Move(ExtensionBaseType ebtTarget = null, int newListIndex = -1);
    }
    public interface IExtension
    {
        public bool Remove();
        public bool Move(ExtensionBaseType ebtTarget = null, int newListIndex = -1);
    }
    public interface IProperty
    {
        public bool Remove();
        public bool Move(ExtensionBaseType ebtTarget = null, int newListIndex = -1);
    }

    public interface IResponse //marks LIR and QR
    {
        UnitsType AddUnits(ResponseFieldType rfParent);
        //RichTextType AddTextAfterResponse
    }

    public interface IDisplayedTypeChildMember { } //LinkType, BlobType, ContactType, CodingType, EventType, OnEventType, PredGuardType

    public interface IQuestionListMember //Implemented on ListItem and DisplayedItem
    { }


    public abstract class ParentType : IParent
    {
        public abstract ChildItemsType ChildItemsNode { get; set; }
        public abstract SectionItemType AddChildSection(string id = "", int insertPosition = -1);
        public abstract QuestionItemType AddChildQuestion(QuestionEnum qType, string id = "", int insertPosition = -1);
        public abstract InjectFormType AddChildInjectedForm(string id = "", int insertPosition = -1);
        public abstract ButtonItemType AddChildButtonAction(string id = "", int insertPosition = -1);
        public abstract DisplayedType AddChildDisplayedItem(string id = "", int insertPosition = -1);

        public abstract C AddChildItem<P, C>(P parent, string childID = "", int insertPosition = -1) where P : IParent where C : IChildItemMember;
        public abstract bool RemoveChildItem<C>(C childItem) where C : IChildItemMember;
        public abstract bool MoveChildItem<P, C>(C childItem, int newPosition, P targetParent) where P : IParent where C : IChildItemMember;


    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

//using SDC;
namespace SDC.Schema2
{
    /// <summary>
    /// </summary>
    /// A public/internal interface inherited by all types that sit at the top of the SDC class hierarchy
    /// Used by FormDesignType, DemogFormDesignType, DataElementType, RetrieveFormPackageType, and PackageListType
    /// The interface provides a common way to fill the above object trees using a single set of shared code.
    /// It also provdes a set of consistent, type-specific, public utilities for working with SDC objects
    public interface ITopNode: IBaseType
    {
        /// <summary>
        /// Dictionary.  Given an Node ID (int), returns the node's object reference.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        Dictionary<int, BaseType> Nodes { get; }

        /// <summary>
        /// Dictionary.  Given a NodeID, return the *parent* node's object reference
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        Dictionary<int, BaseType> ParentNodes { get; }
        ///// <summary>
        ///// Dictionary.  Given a NodeID, return the *previous* node's object reference
        ///// </summary>
        //[System.Xml.Serialization.XmlIgnore]
        //Dictionary<int, BaseType> PreviousNodes { get; }

        /// <summary>
        /// Returns SDC XML from the SDC object tree.  THe XML top node is determined by the top-level object tree node:
        /// FormDesignType, DemogFormDesignType, DataElementType, RetrieveFormPackageType, or PackageListType
        /// </summary>
        /// <returns></returns>
        string GetXml();
        /// <summary>
        /// Not yet supported
        /// </summary>
        /// <returns></returns>
        string GetJson();
        /// <summary>
        /// Not yet supported
        /// </summary>
        /// <returns></returns>
        byte[] GetMsgPack();

        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        int GetMaxObjectID { get; }
        [JsonIgnore]
        internal int MaxObjectID{get; set;}
        /// <summary>
        /// Automatically create and assign element names to all SDC elements
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [JsonIgnore]
        bool GlobalAutoNameFlag { get; set; }
        bool EditBegin();
        void EditFinish();
        IdentifiedExtensionType GetNodeFromID(string id) => 
            (IdentifiedExtensionType)Nodes.Values
            .Where(v => v.GetType() == typeof(IdentifiedExtensionType))
            .Where(iet => ((IdentifiedExtensionType)iet).ID == id).First();
        BaseType GetNodeFromName(string name) => 
            Nodes.Values.Where(n => n.name == name).First();
        BaseType GetNodeFromObjectID(int objectID)
        {
            Nodes.TryGetValue(ObjectID, out BaseType n);
            return n;
        }





        #region Utilities
        //Tenum ConvertStringToEnum<Tenum>(string inputString) where Tenum : struct;
        ITopNode ReorderNodes()
        {
            var xml = SDCHelpers.XmlReorder(this.GetXml());
            ITopNode topNode;
            switch (this)
            {
                case DemogFormDesignType _:
                    topNode = SdcSerializer<DemogFormDesignType>.Deserialize(xml);
                    break;
                case FormDesignType _:
                    topNode = SdcSerializer<FormDesignType>.Deserialize(xml);
                    break;
                case DataElementType _:
                    topNode = SdcSerializer<DataElementType>.Deserialize(xml);
                    break;
                case RetrieveFormPackageType _:
                    topNode = SdcSerializer<RetrieveFormPackageType>.Deserialize(xml);
                    break;
                case PackageListType _:
                    topNode = SdcSerializer<PackageListType>.Deserialize(xml);
                    break;
                case MappingType _:
                    topNode = SdcSerializer<MappingType>.Deserialize(xml);
                    break;
                default:
                    throw new InvalidOperationException($"The current object type \"{GetType().Name}\" is not compatable with this method.");
            }
            return topNode;
        }

        bool RemoveNodeTree(ref BaseType topNodeToRemove, out Dictionary<int, BaseType> dict, bool removeNodes = true)
        {
            

            var desc = Descendants(topNodeToRemove);
            dict = new Dictionary<int, BaseType>();
            dict.Add(topNodeToRemove.ObjectID, topNodeToRemove);
            IdentifiedExtensionType iet;
            //iet = topNodeToRemove as IdentifiedExtensionType;
            //if (IsReleased(iet?.ID)) return false;

            foreach (var n in desc)
            {
                if (n.Value is IdentifiedExtensionType)
                {
                    iet = (IdentifiedExtensionType)n;
                    if (!CanRemoveNode(iet.ID)) dict.Add(iet.ObjectID, iet);
                }
            }
            if (dict.Count > 0) return false; //there is block to removing the node tree
            else
            {
                if (removeNodes)
                {
                    foreach (var n in desc)
                    {
                        Nodes.Remove(n.Value.ObjectID);
                        ParentNodes.Remove(n.Value.ObjectID);
                    }
                }
                return true;
            }

        }

        bool CanRemoveNode(string id)
            {  //check LockedItem table in SSP
                return true;  //need real look code here
            }

        bool IsDirectDescendantIET(IdentifiedExtensionType nodeIET, BaseType ChildNode)
        {

            var pIET = ChildNode.ParentIETypeNode;
            if (pIET != null && nodeIET == pIET) return true;
            return false;

        }

        bool IsDescendant(BaseType topNode, BaseType ChildNode)
        {
            while (ParentNode !=null) if (ParentNode == topNode) return true;
                
            return false;
        }

        bool IsChild(BaseType parentNode, BaseType childNode)
        {
            var childDict = Descendants(parentNode);

            childDict.TryGetValue(childNode.ObjectID, out BaseType tryChild);
            if (tryChild != null) return true;

            return getKids(parentNode);

            bool getKids(BaseType parentNode)
            {
                //For each child, get all their children recursively and test for childNode
                var vals = (Dictionary<int, BaseType>)Nodes.Values.Where(n => n.ParentID == parentNode.ParentID);
                foreach (var n in vals)
                {
                    if (n.Value == childNode) return true;
                    getKids(n.Value); //recurse
                }
                return false;
            }
        }

        Dictionary<int, BaseType> Descendants (BaseType topNode)
        {
            var curNode = this;
            var d = new Dictionary<int, BaseType>();
            getKids((BaseType)this);

            void getKids (BaseType node)
            {
                //For each child, get all their children recursively, then add to dictionary
                var vals = (Dictionary<int, BaseType>)Nodes.Values.Where(n => n.ParentID == curNode.ParentID);
                foreach (var n in vals)
                {
                    getKids(n.Value); //recurse
                    d.Add(n.Key, n.Value);
                }
            }

            return d;

        }


        #region GetItems

        IdentifiedExtensionType GetItem(string id)
        {
            IdentifiedExtensionType iet;
            iet = (IdentifiedExtensionType)Nodes.Values.Where(
                t => t.GetType() == typeof(IdentifiedExtensionType)).Where(
                    t => ((IdentifiedExtensionType)t).ID == id).First();
            return iet;
        }
        BaseType GetItemByName(string name)
        {
            BaseType iet;
            iet = (BaseType)Nodes.Values.Where(
                t => t.GetType() == typeof(BaseType)).Where(
                    t => ((BaseType)t).name == name).First();
            return iet;
        }
        QuestionItemType GetQuestion(string id)
        {
            QuestionItemType q;
            q = (QuestionItemType)Nodes.Values.Where(
                t => t.GetType() == typeof(QuestionItemType)).Where(
                    q => ((QuestionItemType)q).ID == id).First();
            return q;
        }
        QuestionItemType GetQuestionByName(string name)
        {
            QuestionItemType q;
            q = (QuestionItemType)Nodes.Values.Where(
                t => t.GetType() == typeof(QuestionItemType)).Where(
                    t => ((QuestionItemType)t).name == name).First();
            return q;
        }
        DisplayedType GetDisplayedType(string id)
        {
            DisplayedType d;
            d = (DisplayedType)Nodes.Values.Where(
                t => t.GetType() == typeof(DisplayedType)).Where(
                    t => ((DisplayedType)t).ID == id).First();
            return d;
        }
        DisplayedType GetDisplayedTypeByName(string name)
        {
            DisplayedType d;
            d = (DisplayedType)Nodes.Values.Where(
                t => t.GetType() == typeof(DisplayedType)).Where(
                    t => ((DisplayedType)t).name == name).First();
            return d;
        }
        SectionItemType GetSection(string id)
        {
            SectionItemType s;
            s = (SectionItemType)Nodes.Values.Where(
                t => t.GetType() == typeof(SectionItemType)).Where(
                    t => ((SectionItemType)t).ID == id).First();
            return s;
        }
        SectionItemType GetSectionByName(string name)
        {
            SectionItemType s;
            s = (SectionItemType)Nodes.Values.Where(
                t => t.GetType() == typeof(SectionItemType)).Where(
                    t => ((SectionItemType)t).name == name).First();
            return s;
        }
        ListItemType GetListItem(string id)
        {
            ListItemType li;
            li = (ListItemType)Nodes.Values.Where(
                t => t.GetType() == typeof(ListItemType)).Where(
                    t => ((ListItemType)t).ID == id).First();
            return li;
        }
        ListItemType GetListItemByName(string name)
        {
            ListItemType li;
            li = (ListItemType)Nodes.Values.Where(
                t => t.GetType() == typeof(ListItemType)).Where(
                    t => ((ListItemType)t).name == name).First();
            return li;
        }

        ButtonItemType GetButton(string id)
        {
            ButtonItemType b;
            b = (ButtonItemType)Nodes.Values.Where(
                t => t.GetType() == typeof(ButtonItemType)).Where(
                    t => ((ButtonItemType)t).ID == id).First();
            return b;
        }
        ButtonItemType GetLButtonByName(string name)
        {
            ButtonItemType b;
            b = (ButtonItemType)Nodes.Values.Where(
                t => t.GetType() == typeof(ButtonItemType)).Where(
                    t => ((ButtonItemType)t).name == name).First();
            return b;
        }
        InjectFormType GetInjectForm(string id)
        {
            InjectFormType inj;
            inj = (InjectFormType)Nodes.Values.Where(
                t => t.GetType() == typeof(InjectFormType)).Where(
                    t => ((InjectFormType)t).ID == id).First();
            return inj;
        }
        InjectFormType GetInjectFormByName(string name)
        {
            InjectFormType inj;
            inj = (InjectFormType)Nodes.Values.Where(
                t => t.GetType() == typeof(InjectFormType)).Where(
                    t => ((InjectFormType)t).name == name).First();
            return inj;
        }
        ResponseFieldType GetResponseFieldByName(string name)
        {
            ResponseFieldType rf;
            rf = (ResponseFieldType)Nodes.Values.Where(
                t => t.GetType() == typeof(ResponseFieldType)).Where(
                    t => ((ResponseFieldType)t).name == name).First();
            return rf;
        }
        PropertyType GetPropertyByName(string name)
        {
            PropertyType p;
            p = (PropertyType)Nodes.Values.Where(
                t => t.GetType() == typeof(PropertyType)).Where(
                    t => ((PropertyType)t).name == name).First();
            return p;
        }
        ExtensionType GetExtensionByName(string name)
        {
            ExtensionType e;
            e = (ExtensionType)Nodes.Values.Where(
                t => t.GetType() == typeof(ExtensionType)).Where(
                    t => ((ExtensionType)t).name == name).First();
            return e;
        }
        CommentType GetCommentByName(string name)
        {
            CommentType c;
            c = (CommentType)Nodes.Values.Where(
                t => t.GetType() == typeof(CommentType)).Where(
                    t => ((CommentType)t).name == name).First();
            return c;
        }
        ContactType GetContactByName(string name)
        {
            ContactType c;
            c = (ContactType)Nodes.Values.Where(
                t => t.GetType() == typeof(CommentType)).Where(
                    t => ((ContactType)t).name == name).First();
            return c;
        }
        LinkType GetLinkByName(string name)
        {
            LinkType l;
            l = (LinkType)Nodes.Values.Where(
                t => t.GetType() == typeof(LinkType)).Where(
                    t => ((LinkType)t).name == name).First();
            return l;
        }
        BlobType GetBlobByName(string name)
        {
            BlobType b;
            b = (BlobType)Nodes.Values.Where(
                t => t.GetType() == typeof(BlobType)).Where(
                    t => ((BlobType)t).name == name).First();
            return b;
        }
        CodingType GetCodedValueByName(string name)
        {
            CodingType c;
            c = (CodingType)Nodes.Values.Where(
                t => t.GetType() == typeof(CodingType)).Where(
                    t => ((CodingType)t).name == name).First();
            return c;
        }
        #endregion

#endregion


    }


}

using System;
using System.Linq;
using System.Xml;

namespace SDC.Schema2
{
    public static class SDCHelpers
    {

        /// <summary>
        /// Converts the string expression of an enum value to the desired type. Example: var qType= reeBuilder.ConvertStringToEnum&lt;ItemType&gt;("answer");
        /// </summary>
        /// <typeparam name="Tenum">The enum type that the inputString will be converted into.</typeparam>
        /// <param name="inputString">The string that must represent one of the Tenum enumerated values; not case sensitive</param>
        /// <returns></returns>
        public static Tenum ConvertStringToEnum<Tenum>(string inputString) where Tenum : struct
        {
            //T newEnum = (T)Enum.Parse(typeof(T), inputString, true);

            Tenum newEnum;
            if (Enum.TryParse<Tenum>(inputString, true, out newEnum))
            {
                return newEnum;
            }
            else
            { //throw new Exception("Failure to create enum");

            }
            return newEnum;
        }
        /// Given a node, returns the parent item's node reference as IdentifiedExtensionType (not the abstract ParentType).
        /// However, the returned node may be cast to ParentType and implements IParent
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IdentifiedExtensionType GetParentIETypeNode(string parentID)
        {
            IdentifiedExtensionType parentItemNode = null;

            if (!string.IsNullOrWhiteSpace(parentID))
                IdentifiedExtensionType.IdentExtNodes.TryGetValue(parentID, out parentItemNode);//this is the parent of node

            return parentItemNode;
        }
        public static void NZ<T>(T nullTestObject, T ObjectToSet)
        {
            if (nullTestObject == null) return;
            if (nullTestObject.GetType() == typeof(string) && nullTestObject.ToString() == "") return;
                ObjectToSet = nullTestObject;
        }

        public static string XmlReorder (string Xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(Xml);
            int j = 0;
            foreach (XmlNode node in doc.SelectNodes("//*"))
            {   //renumber the XML elements in Node order
                if (node.NodeType == XmlNodeType.Element)
                {
                    //if (node.Attributes.GetNamedItem("order") !=null)
                    node.Attributes["order"].Value = j++.ToString();
                }
            }
            return doc.OuterXml;
        }

        public static string XmlFormat (string Xml)
        {
            return System.Xml.Linq.XDocument.Parse(Xml).ToString();  //prettify the minified doc XML 
        }

        /// <summary>
        /// Given a child node, returns the parent item's node reference as IdentifiedExtensionType (not the abstract ParentType).
        /// However, the returned node may be cast to ParentType and implements IParent
        /// </summary>
        /// <param name="childNode"></param>
        /// <returns></returns>
        //public static IdentifiedExtensionType xGetParentIETypeNode(BaseType childNode)
        //{
        //    return childNode.ParentIETypeObject;

        //    //IdentifiedExtensionType parentItemNode = null;

        //    ////if (childNode.ParentIETypeObject != null) { return childNode.ParentIETypeObject; }

        //    //if (childNode.ParentIETypeID != null)
        //    //{
        //    //    if (DisplayedType.IdentExtNodes.TryGetValue(childNode.ParentIETypeID, out parentItemNode))//this is the parent of node
        //    //    { return parentItemNode; }
        //    //}

        //    ////walk up the chain of SDC nodes to find the first ParentType (IParent)
        //    ////this is safer but somewhat slower
        //    //BaseType testBT = childNode;

        //    //while (parentItemNode == null && testBT.ParentNode != null)
        //    //{
        //    //    testBT = testBT.ParentNode;    //GetParentBaseNode(testBT);
        //    //    if (testBT == null) { return null; }  //no more parent nodes

        //    //    parentItemNode = testBT as IdentifiedExtensionType;   //ParentType;  //see if the node can be cast to ParentType
        //    //    if (parentItemNode != null) { return parentItemNode; }
        //    //}
        //    //return null;

        //}


        /// <summary>
        /// Given a ParentObjID (not a ParentIETypeID), returns the parent node reference as BaseType. This "parent" may be any type of XML parent node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        //public static BaseType X_GetParentBaseNode(int parentObjID)
        //{
        //    //BaseType parentNode = null;
        //    parentNode.FormDesign.Nodes.TryGetValue(parentObjID, out BaseType parentNode);
        //    return parentNode;
        //}
        ///// <summary>
        ///// Given a child node, returns the parent node's reference as BaseType.  This "parent" may be any type of XML parent node
        ///// </summary>
        ///// <param name="childNode"></param>
        ///// <returns></returns>
        //public static BaseType X_GetParentBaseNode(BaseType childNode)
        //{
        //    //BaseType parentNode = null;
        //    childNode.FormDesign.Nodes.TryGetValue(childNode.ParentObjID, out BaseType parentNode);
        //    return parentNode;
        //}

    }
}

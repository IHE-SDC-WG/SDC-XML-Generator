using System;
using System.Linq;

namespace SDC
{
    public static class SDCHelpers
    {
        /// Given a node, returns the parent item's node reference as IdentifiedExtensionType (not the abstract ParentType).
        /// However, the returned node may be cast to ParentType and implements IParent
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IdentifiedExtensionType GetParentItem(string parentItemID)
        {
            IdentifiedExtensionType parentItemNode = null;
            IdentifiedExtensionType.IdentExtNodes.TryGetValue(parentItemID, out parentItemNode);//this is the parent of node

            return parentItemNode;
        }
        /// <summary>
        /// Given a child node, returns the parent item's node reference as IdentifiedExtensionType (not the abstract ParentType).
        /// However, the returned node may be cast to ParentType and implements IParent
        /// </summary>
        /// <param name="childNode"></param>
        /// <returns></returns>
        public static IdentifiedExtensionType GetParentItem(BaseType childNode)
        {
            IdentifiedExtensionType parentItemNode = null;
            if (IdentifiedExtensionType.IdentExtNodes.TryGetValue(childNode.ParentItemID, out parentItemNode))//this is the parent of node
            { return parentItemNode; }
            else  //walk up the chain of SDC nodes to find the first ParentType (IParent)
            {
                BaseType testBT = childNode;
                //ParentType parentItem;

                while (parentItemNode == null && testBT.GetParentNode != null)
                {
                    testBT = GetParentBaseNode(testBT);
                    if (testBT == null) { return null; }  //no more parent nodes

                    parentItemNode = testBT as ParentType;  //see if the node can be cast to ParentType
                    if (parentItemNode != null) { return parentItemNode; }
                }
                return parentItemNode;
            }
        }

        /// <summary>
        /// Given a ParentObjID (not a ParentItemID), returns the parent node reference as BaseType. This "parent" may be any type of XML parent node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static BaseType GetParentBaseNode(int parentObjID)
        {
            BaseType parentNode = null;
            BaseType.Nodes.TryGetValue(parentObjID, out parentNode);
            return parentNode;
        }
        /// <summary>
        /// Given a child node, returns the parent node's reference as BaseType.  This "parent" may be any type of XML parent node
        /// </summary>
        /// <param name="childNode"></param>
        /// <returns></returns>
        public static BaseType GetParentBaseNode(BaseType childNode)
        {
            BaseType parentNode = null;
            BaseType.Nodes.TryGetValue(childNode.ParentObjID, out parentNode);
            return parentNode;
        }

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
    }
}

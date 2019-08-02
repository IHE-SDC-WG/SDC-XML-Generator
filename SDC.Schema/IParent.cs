using System;
using System.Linq;

//using SDC;
namespace SDC.Schema
{
    /// <summary>
    /// This interface is applied to the partial classes that must support the ChildItems element.
    /// These are Section, Question and ListItem.  
    /// This interface is require to support generic classes that must handle the creation ofthe 
    /// ChildItems element, which holds List of type IdentifiedItemType
    /// </summary>
    public interface IParent
    {
        ChildItemsType ChildItemsNode { get; set; }
        SectionItemType AddSection(Boolean fillData = true, string id = null);
        QuestionItemType AddQuestion(QuestionEnum qType, Boolean fillData = true, string id = null);
        InjectFormType AddInjectedForm(Boolean fillData = true, string id = null);
        ButtonItemType AddButtonAction(Boolean fillData = true, string id = null);
        DisplayedType AddDisplayedItem(Boolean fillData = true, string id = null);
    }
    public abstract class ParentType : IdentifiedExtensionType, IParent
    {
        public abstract ChildItemsType ChildItemsNode { get; set; }
        public abstract SectionItemType AddSection(Boolean fillData = true, string id = null);
        public abstract QuestionItemType AddQuestion(QuestionEnum qType, Boolean fillData = true, string id = null);
        public abstract InjectFormType AddInjectedForm(Boolean fillData = true, string id = null);
        public abstract ButtonItemType AddButtonAction(Boolean fillData = true, string id = null);
        public abstract DisplayedType AddDisplayedItem(Boolean fillData = true, string id = null);
    }
}

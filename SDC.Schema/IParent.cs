using System;
using System.Linq;

//using SDC;
namespace SDC
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
        SectionItemType AddFillSection(Boolean fillData = true);
        QuestionItemType AddFillQuestion(QuestionEnum qType, Boolean fillData = true);
        InjectFormType AddFillInjectedForm(Boolean fillData = true);
        ButtonItemType AddFillButtonAction(Boolean fillData = true);
        DisplayedType AddFillDisplayedItem(Boolean fillData = true);
    }
    public abstract class ParentType : IdentifiedExtensionType, IParent
    {
        public abstract ChildItemsType ChildItemsNode { get; set; }
        public abstract SectionItemType AddFillSection(Boolean fillData = true);
        public abstract QuestionItemType AddFillQuestion(QuestionEnum qType, Boolean fillData = true);
        public abstract InjectFormType AddFillInjectedForm(Boolean fillData = true);
        public abstract ButtonItemType AddFillButtonAction(Boolean fillData = true);
        public abstract DisplayedType AddFillDisplayedItem(Boolean fillData = true);
    }
}

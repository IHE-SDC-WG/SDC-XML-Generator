using System;
using System.Data;
using System.Collections.Generic;

namespace SDC
{
    public class FormDesignMapper
    {/// <summary>
        /// Initializes a new instance of the <see cref="FormDesign"/> class.
        /// </summary>
        /// <param name="fD"></param>


        public object rowToNodeMapper(DataRow dr, out EccItemType qType)
        {
            //TODO: Add similar method to map items in the template header 
            //(see MapTemplate and MapTemplateHeader):

            //!QF, QM, QS, List, ListItem, Section or Note
            qType = new EccItemType();

            //!Base item metadata
            String type = (String)(dr["type"]);
            String styleClass = (String)(dr["styleClass"]);
            String shortName = (String)(dr["name"]);

            //!Displayed items: Q, S, N (DI), A (LI)
            Decimal itemCkey = (Decimal)(dr["itemCkey"]);
            String visibleText = (String)(dr["visibleText"]);
            String longText = (String)(dr["longText"]);
            String reportText = (String)(dr["reportText"]);

            //!All Displayed Items
            Boolean enabled = (Boolean)(dr["enabled"]);
            Boolean visible = (Boolean)(dr["visible"]);
            String tooltip = (String)(dr["tooltip"]);
            String popupText = (String)(dr["popupText"]);
            String linkText = (String)(dr["linkText"]);
            String linkText2 = (String)(dr["linkText2"]);
            String Source = (String)(dr["Source"]);  //TODO: not in the Question object //Source is AuthorityID 
            Boolean showInReport = (Boolean)(dr["showInReport"]);
            Boolean mustImplement = (Boolean)(dr["mustImplement"]);
            Int32 sortorder = (Int32)(dr["SortOrder"]);

            //!Response items QF and AF
            String dataType = (String)(dr["ColName"]);
            String answerUnits = (String)(dr["ColName"]);
            String textAfterResponse = (String)(dr["ColName"]);

            //!A, AF
            Boolean sdc = (Boolean)(dr["SelectionDisablesChildren"]); //sdc, SelectionDisablesChildren
            Boolean sds = (Boolean)(dr["SelectionDisablesSiblings"]); //sdc, SelectionDeselectsSiblings
            Boolean omitWhenSelected = (Boolean)(dr["omitWhenSelected"]);

            //!AF only
            Boolean responseRequired = (Boolean)(dr["responseRequired"]);  //not used here

            //!List Items
            String colTextDelimiter = (String)(dr["colTextDelimiter"]);
            Int16 numCols = (Int16)(dr["numCols"]);
            Int16 storedCol = (Int16)(dr["storedCol"]);
            String listHeaderText = (String)(dr["listHeaderText"]);
            Int16 minSelections = (Int16)(dr["minSelections"]);
            Int16 maxSelections = (Int16)(dr["maxSelections"]);

            //!Sections and Questions are repeatable items
            Boolean locked = (Boolean)(dr["ColName"]);
            Int32 minCard = (Int32)(dr["minCard"]);
            Int32 maxCard = (Int32)(dr["maxCard"]);
            Boolean authorityRequired = (Boolean)(dr["AuthorityRequired"]); //ordinarily, this "required" flag only applies to Questions and the Sections (eCC headers) that contain them


            var n = new QuestionItemType();

            //Don't serialize the follwing attributes if they are empty; use if clause to ensure they are not touched if they have default values
            //Need to check if this keep them out of the serialized XML.  If it has no effect, then most of the if clauses can be removed. 
            //This is an attempt to avoid cluttering the XML with default-valued attributes.

            //!Base item metadata
            if (type != String.Empty) n.type = type;
            if (styleClass != String.Empty) n.styleClass = styleClass;
            if (shortName != String.Empty) n.name = shortName;
            //!All Displayed Items
            n.ID = itemCkey.ToString();
            n.title = visibleText;

            if (longText != String.Empty)
            {

            }
            if (reportText != String.Empty)
            {
                //n.reportText = reportText;
            }
            n.enabled = enabled; //show only when false
            n.visible = visible;
            if (tooltip != String.Empty)
            {
                //n.tooltip = tooltip;
            };
            if (popupText != String.Empty)
            {
                //n.popupText = popupText;
            };

            if (linkText != String.Empty)
            {
                //n.linkText = linkText;
            };
            if (linkText2 != String.Empty)
            {
                //n.linkText2 = linkText2;
            };
            //n.Source = Source; 
            //TODO: Why isn't Source in Question in the Schema?  Is it a coding construct?  Should it be treated as OtherText (note)?

            if (!showInReport) n.showInReport = false;
            if (mustImplement == false) n.mustImplement = mustImplement;  //default is true

            //!eCC Special handling Conditional Reporting with "?": Q and S only
            if (n.title.StartsWith("?"))
            {
                n.mustImplement = true;
                n.title.TrimStart('?');
            }
            //!eCC Special handling for Authority Required: Q, A and S only
            if (authorityRequired)
            {
                //n.required = true;
                n.minCard = 1;
                n.mustImplement = true;
            }
            else
            {
                //n.required = false;
                n.minCard = 0;
                n.mustImplement = false;
            }

            if (sortorder >= 0) n.order = sortorder;

            //!Sections and Questions are repeatable items//
            if (locked) n.readOnly = true;  //show only when true
            n.minCard = (UInt16)minCard; //default is 1 (meaning that the Section or Question is required)
            n.maxCard = (UInt16)maxCard;
            //Console.WriteLine( n.title, n.maxCard.ToString(), maxCard.ToString());



            //!Questions: QF, QM or QS
            if (qType == EccItemType.QUESTIONFILLIN)
            {
                //n.questionfillin = true;
                if (dataType != string.Empty)
                {
                    //n.datatype = dataType;
                }
                if (answerUnits != string.Empty)
                {
                    //n.answerunits = answerUnits;
                }
                if (textAfterResponse != string.Empty)
                {
                    //n.textAfterResponse = textAfterResponse;
                }
            }


            //!List Wrapper
            if (qType == EccItemType.QUESTIONMULTIPLE || qType == EccItemType.QUESTIONSINGLE)
            {//add a wrapper element (fixedlistanswer) for all the list items that will be added later
                if (n.Item != null)
                {
                    var listField = new ListFieldType();
                    var list = new ListType();
                    listField.Item = list;

                    //!QM
                    //listField.X_multiSelect = (qType == ItemType.QUESTIONMULTIPLE); //TODO: multiSelect is not needed if maxSelections is specified
                    if (qType == EccItemType.QUESTIONMULTIPLE) listField.maxSelections = 0;
                    if (qType == EccItemType.QUESTIONSINGLE) listField.maxSelections = 1;
                    if (minSelections > 1) listField.minSelections = (ushort)minSelections;
                    if (maxSelections > 0) listField.maxSelections = (ushort)maxSelections;

                    if (numCols > 0)
                    {
                        listField.storedCol = (storedCol == (byte)0) ? (byte)1 : (byte)storedCol;
                        listField.numCols = (byte)numCols;
                        listField.colTextDelimiter = colTextDelimiter.Trim();
                        if (listHeaderText != String.Empty) listField.ListHeaderText.val = listHeaderText;
                    }
                    listField.name = "";
                    listField.type = "";
                    listField.styleClass = "";

                    list.name = "";
                    list.type = "";
                    list.styleClass = "";

                    n.Item = listField;
                }



                //!QM
                //if (qType == ItemType.QUESTIONMULTIPLE)
                //{
                //    list.allowmultipleselection = (qType == ItemType.QUESTIONMULTIPLE); //just trying out a Boolean trick :-)
                //    if (minSelections > 1) f.minSelections = (UInt16)minSelections;
                //    if (maxSelections > 0) f.maxSelections = (UInt16)maxSelections;
                //}

                //n.Items = n.Items.Add(f);

                //!List Items Only
                //TODO:  Move to method for ListItems
                var qLF = (ListFieldType)n.Item;
                var qL = (ListType)qLF.Item;
                var qLI = new ListItemType();
                qL.DisplayedItem_List.Add(qLI);



                qLI.title = visibleText;

                qLI.name = shortName;
                qLI.type = type;
                qLI.styleClass = styleClass;
                qLI.order = sortorder;
                qLI.enabled = enabled;
                qLI.visible = visible;
                qLI.selected = locked;


                qLI.selectionDeselectsSiblings = sdc;
                qLI.selectionDeselectsSiblings = sds;



                //n.selectiondisableschildren = sdc;
                //n.selectiondeselectssiblings = sds;
                //n.omitWhenSelected = omitWhenSelected;

                //!+QF; AF is similar, but should be handled seperately

                var RF = new ResponseFieldType();
                n.ResponseField_Item = RF;  //can assign to ListField too



                if (qType == EccItemType.ANSWERFILLIN || qType == EccItemType.QUESTIONFILLIN)
                {
                    if (dataType != string.Empty)
                    {
                        //var dt = new DataTypes_DEType();

                        //qLI.ListItemResponseField  = RF;

                        if (answerUnits != string.Empty)
                        {
                            RF.ResponseUnits = new UnitsType();
                            RF.ResponseUnits.val = answerUnits;
                        }

                    }

                    //!+!eCC Special handling for QF; handle AF seperately
                    if (n.title.ToLower().Contains("specify") ||
                        n.title.ToLower().Contains("explain") ||
                        n.title.ToLower().Contains("at least")
                        )
                    {
                        //!liRF.responseRequired = true;  //for AF
                        //n.mustImplement = true;
                        if (minCard == 0) n.minCard = 1; //for QF
                    }
                    else
                    { //n.responseRequired = false; 
                    }

                    return n;

                }
                //var liRF=R;
                //var liRF = new ListItemBaseTypeListItemResponseField();

                //var sdcRF = (ResponseFieldType)RF;  //Response field on a List Item
                var liRF = (ListItemResponseFieldType)(ResponseFieldType)RF;  //Response field on a List Item

                if (qType == EccItemType.ANSWERFILLIN)
                {
                    if (textAfterResponse != String.Empty) RF.TextAfterResponse.val = textAfterResponse;
                    liRF.responseRequired = responseRequired;
                }

            }
            return n;

        }

    }
}

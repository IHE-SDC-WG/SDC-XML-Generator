using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Capx.Apps.ChecklistTemplateGenerator.DAL;
using Capx.Model.Sr.Template;
using System.Globalization;
namespace Capx.Apps.ChecklistTemplateGenerator
{
    /// <summary>
    /// Template builder implements TemplateBuilderBase to create template xml for the sr-template.xsd schema
    /// </summary>
    public class SrTemplateDataMapper : IDisposable 
    {
        #region Private variables
        private String _templateRepositoryUri = String.Empty;
        private readonly SrTemplateObjectTreeBuilder _srTmpTreeBuilder = new SrTemplateObjectTreeBuilder();
        private const String _xmlHeader = @"<?xml version=""1.0"" encoding=""utf-8""?>";

        private String _xsltFileName = String.Empty;
        private String _defaultTemplateVersion = String.Empty;
        private FormDesignDataSets EccDataSets = new FormDesignDataSets();
        #endregion

        public void Dispose()
        {
            if (EccDataSets != null)
            {
                EccDataSets.Dispose();
                EccDataSets = null;
            }
        }

        /// <summary>
        /// Gets template by Ckey
        /// </summary>
        /// <param name="keyIn">Ckey of template to get</param>
        /// <param name="bestFileName">The preferred filename, passed in from the UI, if available</param>
        /// <returns>Template XML</returns>
        public String CreateOneTemplateByCkey(String keyIn, ref string  bestFileName)
        {
            Decimal CTV_Ckey = 0;


            if (keyIn.Trim() != "" &&
                Decimal.TryParse(keyIn, out CTV_Ckey))
            {
                if (CTV_Ckey > 0)
                {
                    DataTable templateVersion = EccDataSets.dtGetFormDesignMetadata(CTV_Ckey);
                    DataTable templateItems = EccDataSets.dtGetFormDesign(CTV_Ckey);
                    DataTable templateProcedures = EccDataSets.dtGetChecklistVersionProcedures(CTV_Ckey);
                    if (templateVersion.Rows.Count == 0) return "";
                    DataRow row = templateVersion.Rows[0];


                    #region  Create/serialize object tree
                    template templateObjectTree = BuildTemplateObjectTree(templateVersion, templateItems, templateProcedures);

                    
                    #region  Determine Best FileName
                    //Use the database supplied filename if the user-supplied filename is empty
                    //string dbFileName = row["CurrentFileName"].ToString();  //changed 12/31/2017
                    string templateFileName = templateObjectTree.fileName;


                    if (bestFileName != "")
                    { row["CurrentFileName"] = bestFileName.Trim(); }
                    else if (templateFileName == "")  //USERfileName and row's filename are both empty here
                    {   //create a dummy filename and insert it into the row, so it's also added to the XML
                        row["CurrentFileName"] = "_file_" + DateTime.Now.Ticks.ToString().Trim();
                        bestFileName = row["CurrentFileName"].ToString().Trim();  //the new username will now be available to the caller
                    }
                    else { bestFileName = templateFileName; }  //this is the most common case
                    #endregion





                    if (templateObjectTree != null)
                    {
                        var ns = new XmlSerializerNamespaces();
                        //rlm: next line directs the format of the serialized XML, according to the schema-generated template
                        var serializer = new XmlSerializer(typeof(template));
                        var writer = new StringWriterEncoding(Encoding.UTF8);
                        serializer.Serialize(writer, templateObjectTree, ns);
                        String templateXml = writer.ToString();

                        //rlm: add xsl transform directive to xml; 
                        //! \n adds only LF, but we need CR/LF, so I changed "\n" to "\r\n"
                        templateXml = templateXml.Replace(_xmlHeader, _xmlHeader +
                            "\r\n<?xml-stylesheet type=\"text/xsl\" href=\"" + XsltFileName + "\" ?>" +
                            "\r\n<!--(c) 2007-2016 College of American Pathologists.  All rights reserved.  License required for use.-->");
                        //The last line was added 7/23/2013, rlm

                        //  rlm-jmk 2010_5_26: to fix issues with xml apos and quot mangled by the serializer
                        templateXml = templateXml.Replace("&amp;quot", "&quot");
                        templateXml = templateXml.Replace("&amp;apos", "&apos");
                        return templateXml;
                    }
                    return String.Empty;
                    #endregion
                }
            }
            return String.Empty;
        }


        #region Added from TemplateBuilderBase


        /// <summary>
        /// Xsl file name for transform to be included in template xml
        /// </summary>
        /// 
        public String XsltFileName
        {
            get { return _xsltFileName; }
            set { _xsltFileName = value; }
        }


        #endregion
        #region Private Methods

        /// <summary>
        /// Gets (creates) template xml by version ckey
        /// </summary>
        /// <param name="templateVersion">template version data</param>
        /// <param name="templateItems">template item data</param>
        /// <param name="templateProcedures">template procedure data</param>
        /// <returns>template for version ckey</returns>
        private template BuildTemplateObjectTree(DataTable templateVersion, DataTable templateItems, DataTable templateProcedures)
        {
            template aTemplate = null;
            Dictionary<Decimal, object> objectList = new Dictionary<Decimal, object>();
            if (templateVersion.Rows.Count == 0)
            {
                return null;
            }
            aTemplate = MapTemplate(templateVersion.Rows[0]);
            // unmapped procedure until clarifications are completed
            //if (templateProcedures != null)
            //{
            //    foreach (DataRow dr in templateProcedures.Rows)
            //    {
            //        aTemplate.templateheader.procedures = aTemplate.templateheader.procedures.AddItem(MapProcedure(dr));
            //    }
            //}
            ////if (!objectList.ContainsKey(aTemplate.templateid))
            objectList.Add(aTemplate.templateid, aTemplate);
            if (templateItems.Rows.Count == 0)
            {
                return aTemplate;
            }

            foreach (DataRow dr in templateItems.Rows)
            {
                object o;
                ItemType? rowTypePrevious = null;
                var rowType = ItemMapper.MapItemType(Convert.ToInt32(dr["ItemTypeKey"]));
                // fixed to make all notes under question fixed list notes, rlm: in case TE modeler uses a Note instead of ComboNote
                // this is required to fix issues with ordering of all items under a question such as the WilmsTmrResx.xml template
                if (rowType == ItemType.NOTE && Convert.ToInt32(dr["IsParentQuestion"]) == 1)
                {
                    //if (rowTypePrevious==ItemType.ANSWER || rowTypePrevious == ItemType.ANSWERFILLIN || rowTypePrevious == ItemType.FIXEDLISTNOTE || rowTypePrevious==null)
                    rowType = ItemType.FIXEDLISTNOTE;

                }
                switch (rowType)
                {
                    case ItemType.HEADER:
                        headergroup h = MapHeaderGroup(dr);
                        //// if (!objectList.ContainsKey(h.headergroupid)) 
                        objectList.Add(h.headergroupid, h);
                        if (objectList.ContainsKey(Convert.ToDecimal(dr["ParentItemCkey"])))
                        {
                            o = objectList[Convert.ToDecimal(dr["ParentItemCkey"])];
                            _srTmpTreeBuilder.AddHeaderToItem(o, h);
                        }
                        break;
                    case ItemType.NOTE:
                        note n = MapNote(dr);
                        //rlm: If the note is not already in the objectList, then add it
                        //if (!objectList.ContainsKey(n.noteid)) 
                        objectList.Add(n.noteid, n);
                        if (n.noteid== 36816.1000043M)
                        { }
                        //rlm: If the Note's Parent item is already in the objectList, then connect the note to its Parent item
                        //rlm: If the Parent item is not present, then the note is not attached to the Parent
                        if (objectList.ContainsKey(Convert.ToDecimal(dr["ParentItemCkey"])))
                        {
                            o = objectList[Convert.ToDecimal(dr["ParentItemCkey"])];
                            _srTmpTreeBuilder.AddNoteToItem(o, n);
                        }
                        break;
                    case ItemType.FIXEDLISTNOTE:
                        fixedlistnote note = MapFixedListNote(dr);
                        //if (!objectList.ContainsKey(note.noteid)) 
                        objectList.Add(note.noteid, note);
                        if (objectList.ContainsKey(Convert.ToDecimal(dr["ParentItemCkey"])))
                        {
                            o = objectList[Convert.ToDecimal(dr["ParentItemCkey"])];
                            _srTmpTreeBuilder.AddFixedListNoteToItem(o, note);
                        }
                        break;
                    case ItemType.QUESTIONSINGLE:
                        question q = MapQuestion(dr, ItemType.QUESTIONSINGLE);
                        //if (!objectList.ContainsKey(q.questionid)) 
                        objectList.Add(q.questionid, q);
                        if (objectList.ContainsKey(Convert.ToDecimal(dr["ParentItemCkey"])))
                        {
                            o = objectList[Convert.ToDecimal(dr["ParentItemCkey"])];
                            _srTmpTreeBuilder.AddQuestionToItem(o, q);
                        }
                        break;
                    case ItemType.QUESTIONMULTIPLE:
                        question q2 = MapQuestion(dr, ItemType.QUESTIONMULTIPLE);
                        //if (!objectList.ContainsKey(q2.questionid)) 
                        objectList.Add(q2.questionid, q2);
                        if (objectList.ContainsKey(Convert.ToDecimal(dr["ParentItemCkey"])))
                        {
                            o = objectList[Convert.ToDecimal(dr["ParentItemCkey"])];
                            _srTmpTreeBuilder.AddQuestionToItem(o, q2);
                        }
                        break;
                    case ItemType.QUESTIONFILLIN:
                        question q3 = MapQuestion(dr, ItemType.QUESTIONFILLIN);
                        //if (!objectList.ContainsKey(q3.questionid)) 
                        objectList.Add(q3.questionid, q3);
                        if (objectList.ContainsKey(Convert.ToDecimal(dr["ParentItemCkey"])))
                        {
                            o = objectList[Convert.ToDecimal(dr["ParentItemCkey"])];
                            _srTmpTreeBuilder.AddQuestionToItem(o, q3);
                        }
                        break;
                    case ItemType.ANSWER:
                        fixedlistitem item = MapAnswer(dr);
                        //if (!objectList.ContainsKey(item.answerid)) 
                        objectList.Add(item.answerid, item);
                        if (objectList.ContainsKey(Convert.ToDecimal(dr["ParentItemCkey"])))
                        {
                            o = objectList[Convert.ToDecimal(dr["ParentItemCkey"])];
                            _srTmpTreeBuilder.AddAnswerToItem(o, item);
                        }
                        break;
                    case ItemType.ANSWERFILLIN:
                        fixedlistfillinanswer a = MapAnswerFillin(dr);
                        //if (!objectList.ContainsKey(a.answerid)) 
                        objectList.Add(a.answerid, a);
                        if (objectList.ContainsKey(Convert.ToDecimal(dr["ParentItemCkey"])))
                        {
                            o = objectList[Convert.ToDecimal(dr["ParentItemCkey"])];
                            _srTmpTreeBuilder.AddAnswerToItem(o, a);
                        }
                        break;
                }
               //rowTypePrevious = rowType;
            }
            return aTemplate;
        }

        /// <summary>
        /// Creates a note from an item record in TE
        /// </summary>
        /// <remarks>
        /// Mapping info:  
        /// ckey = ChecklistTemplateItemCkey 
        /// text = VisibleText
        /// </remarks>
        /// <param name="dr">item record with note information</param>
        /// <returns>a note class for the specific item</returns>
        private note MapNote(DataRow dr)
        {
            note a = _srTmpTreeBuilder.MapNote(
                //!Base item metadata
                (String)(dr["type"]),         //need standard type lists for Q, H, A, AF, Note, etc
                (String)(dr["styleClass"]),    //
                (String)(dr["ShortName"]),    //@name

                //!All Displayed Items
                (Decimal)(dr["ChecklistTemplateItemCkey"]),
                (String)(dr["VisibleText"]),  //@title or @val
                (String)(dr["longText"]),     //@alt-TextElementEnumerator in eCC, OtherText in SDC
                (String)(dr["ReportText"]),   //OtherText element in SDC

                (Boolean)(dr["enabled"]),
                !(Boolean)(dr["Hidden"]),     //may conflict with styleClass, and may not be used; may be usefu for reporting notes/coments
                (String)(dr["ControlTip"]),   //@toolTip
                (String)(dr["popupText"]),    //@popupText
                (String)(dr["linkText"]),
                (String)(dr["linkText2"]),
                (String)(dr["AuthorityID"]),      //@AuthorityID
                (Boolean)(dr["showInReport"]),    //values are true, false, conditional
                (Boolean)(dr["mustImplement"]),
                (Int32)(dr["SortOrder"]),

                //!Sections, Questions, Notes
                (Boolean)(dr["AuthorityRequired"]) //@required, @minCard, @mustImplemenet
                );
            return a;
        }

        /// <summary>
        /// Creates a fixed list note from an item record in TE
        /// </summary>
        /// <remarks>
        /// Mapping info:  
        /// noteid = ChecklistTemplateItemCkey 
        /// title = VisibleText
        /// </remarks>
        /// <param name="dr">item record with fixed list note information</param>
        /// <returns>a fixed list note class for the specific item</returns>
        private fixedlistnote MapFixedListNote(DataRow dr)
        {
            fixedlistnote a = _srTmpTreeBuilder.MapFixedListNote(
                //!Base item metadata
                (String)(dr["type"]),         //need standard type lists for Q, H, A, AF, Note, etc
                (String)(dr["styleClass"]),    //
                (String)(dr["ShortName"]),    //@name

                //!All Displayed Items
                (Decimal)(dr["ChecklistTemplateItemCkey"]),
                (String)(dr["VisibleText"]),  //@title or @val
                (String)(dr["longText"]),     //@alt-TextElementEnumerator in eCC, OtherText in SDC
                (String)(dr["ReportText"]),   //OtherText element in SDC

                (Boolean)(dr["enabled"]),
                !(Boolean)(dr["Hidden"]),     //may conflict with styleClass, and may not be used; may be usefu for reporting notes/coments
                (String)(dr["ControlTip"]),   //@toolTip
                (String)(dr["popupText"]),    //@popupText
                (String)(dr["linkText"]),
                (String)(dr["linkText2"]),
                (String)(dr["AuthorityID"]),      //@AuthorityID
                (Boolean)(dr["showInReport"]),    //values are true, false, conditional
                (Boolean)(dr["mustImplement"]),
                (Int32)(dr["SortOrder"]),

                //!Sections, Questions, Notes
                (Boolean)(dr["AuthorityRequired"]) //@required, @minCard, @mustImplemenet
                )
                ;

            if (dr["TextCol2"].ToString() != "")
            {
                a.display = new display();
                a.display.property = a.display.property.AddItem(_srTmpTreeBuilder.MapDisplayProperty("ComboCol1", dr["TextCol1"].ToString()));
                a.display.property = a.display.property.AddItem(_srTmpTreeBuilder.MapDisplayProperty("ComboCol2", dr["TextCol2"].ToString()));
                if (dr["TextCol3"].ToString() != "")
                {
                    a.display.property = a.display.property.AddItem(_srTmpTreeBuilder.MapDisplayProperty("ComboCol3", dr["TextCol3"].ToString()));
                }
                if (dr["TextCol4"].ToString() != "")
                {
                    a.display.property = a.display.property.AddItem(_srTmpTreeBuilder.MapDisplayProperty("ComboCol4", dr["TextCol4"].ToString()));
                }
            }
            return a;
        }

        /// <summary>
        /// Creates a procedure from a procedure record
        /// </summary>
        /// <remarks>
        /// Mapping info:  
        /// title = FullySpecifiedName
        /// </remarks>
        /// <param name="dr">procedure record information</param>
        /// <returns>a proceduresProcedure object</returns>
        private proceduresProcedure MapProcedure(DataRow dr)
        {
            proceduresProcedure a = _srTmpTreeBuilder.MapProcedure(dr["FullySpecifiedName"].ToString());
            return a;
        }

        /// <summary>
        /// Creates a headergroup from an item record in TE
        /// </summary>
        /// <remarks>
        /// Mapping info:
        /// headergroupid = ChecklistTemplateItemCkey
        /// title = VisibleText
        /// </remarks>
        /// <param name="dr">item record with header group information</param>
        /// <returns>a headergroup class for the specific item</returns>
        private headergroup MapHeaderGroup(DataRow dr)
        {
            headergroup a = _srTmpTreeBuilder.MapHeaderGroup(
                //!Base item metadata
                (String)(dr["type"]),         //need standard type lists for Q, H, A, AF, Note, etc
                (String)(dr["styleClass"]),    //
                (String)(dr["ShortName"]),    //@name

                //!All Displayed Items
                (Decimal)(dr["ChecklistTemplateItemCkey"]),
                (String)(dr["VisibleText"]),  //@title or @val
                (String)(dr["longText"]),     //@alt-TextElementEnumerator in eCC, OtherText in SDC
                (String)(dr["ReportText"]),   //OtherText element in SDC

                (Boolean)(dr["enabled"]),
                !(Boolean)(dr["Hidden"]),     //may conflict with styleClass, and may not be used; may be usefu for reporting notes/coments
                (String)(dr["ControlTip"]),   //@toolTip
                (String)(dr["popupText"]),    //@popupText
                (String)(dr["linkText"]),
                (String)(dr["linkText2"]),
                (String)(dr["AuthorityID"]),      //@AuthorityID
                (Boolean)(dr["showInReport"]),    //values are true, false, conditional
                (Boolean)(dr["mustImplement"]),
                (Int32)(dr["SortOrder"]),

                //!Sections and Questions are repeatable items
                (Boolean)(dr["Locked"]), //@readOnly in SDC
                (Int32)(dr["minCard"]),  //@minCard
                (Int32)(dr["maxCard"]),  //@maxCard

                //!Sections, Questions, Notes
                (Boolean)(dr["AuthorityRequired"]) //@required, @minCard, @mustImplemenet

                );
            return a;
        }

        /// <summary>
        /// Creates a question from an item record in TE
        /// </summary>
        /// <remarks>
        /// Mapping info:  
        /// questionid = ChecklistTemplateItemCkey 
        /// title = VisibleText
        /// allowmultipleselection = if itemckey is 23 then true else false
        /// </remarks>
        /// <param name="dr">item record with question information</param>
        /// <param name="qType">type of question to be mapped - mult-answer, single-answer, or fillin</param>
        /// <returns>a question class for the specific item</returns>
        /// jmk 20100525 add several items to mapquestion
        /// longText ->  alt, Locked -> Locked
        private question MapQuestion(DataRow dr, ItemType qType)
        {
            question q = _srTmpTreeBuilder.MapQuestion(
                //!Base item metadata
                (String)(dr["type"]),         //need standard type lists for Q, H, A, AF, Note, etc
                (String)(dr["styleClass"]),    //
                (String)(dr["ShortName"]),    //@name

                //!All Displayed Items
                (Decimal)(dr["ChecklistTemplateItemCkey"]),
                (String)(dr["VisibleText"]),  //@title or @val
                (String)(dr["longText"]),     //@alt-TextElementEnumerator in eCC, OtherText in SDC
                (String)(dr["ReportText"]),   //OtherText element in SDC

                (Boolean)(dr["enabled"]),
                !(Boolean)(dr["Hidden"]),     //may conflict with styleClass, and may not be used; may be usefu for reporting notes/coments
                (String)(dr["ControlTip"]),   //@toolTip
                (String)(dr["popupText"]),    //@popupText
                (String)(dr["linkText"]),
                (String)(dr["linkText2"]),
                (String)(dr["AuthorityID"]),      //@AuthorityID
                (Boolean)(dr["showInReport"]),    //values are true, false, conditional
                (Boolean)(dr["mustImplement"]),
                (Int32)(dr["SortOrder"]),

                //!Questions: QF, QM or QS
                qType,

                //!Response items QF and AF
                (String)(dr["DataType"]),
                (String)(dr["AnswerUnits"]),      //@answer-units
                (String)(dr["TextAfterConcept"]), //@textAfterResponse 

                //!AF
                //(Boolean)(dr["responseRequired"]);

                //!List Items
                (String)(dr["colTextDelimiter"]),
                (Int16)(dr["numCols"]),
                (Int16)(dr["storedCol"]),
                (String)(dr["ComboHeaderText"]), //@listHeaderText
                (Int16)(dr["minSelections"]),
                (Int16)(dr["maxSelections"]),

                //!Sections and Questions are repeatable items
                (Boolean)(dr["Locked"]), //@readOnly in SDC
                (Int32)(dr["minCard"]),  //@minCard
                (Int32)(dr["maxCard"]),  //@maxCard

                //!Sections, Questions, Notes
                (Boolean)(dr["AuthorityRequired"]) //@required, @minCard, @mustImplemenet

               );
            //q.authorityrequired = _helper.MapAuthorityRequired(dr["AuthorityID"].ToString()); //removed by rlm 2015_12_10
            return q;
        }

        /// <summary>
        /// Creates a fixedlistitem from an item record in TE
        /// </summary>
        /// <remarks>
        /// Mapping info:
        /// answerid = ChecklistTemplateItemCkey
        /// title = VisibleText
        /// conceptid = ConceptId
        /// </remarks>
        /// <param name="dr">item record with answer information for a selection (item type of 2 or 6)</param>
        /// <returns>an fixedlistitem class for the specific item</returns>
        private fixedlistitem MapAnswer(DataRow dr)
        {
            fixedlistitem a = _srTmpTreeBuilder.MapAnswer(
                //!Base item metadata
                (String)(dr["type"]),         //need standard type lists for Q, H, A, AF, Note, etc
                (String)(dr["styleClass"]),    //
                (String)(dr["ShortName"]),    //@name

                //!All Displayed Items
                (Decimal)(dr["ChecklistTemplateItemCkey"]),
                (String)(dr["VisibleText"]),  //@title or @val
                (String)(dr["longText"]),     //@alt-TextElementEnumerator in eCC, OtherText in SDC
                (String)(dr["ReportText"]),   //OtherText element in SDC

                (Boolean)(dr["enabled"]),
                !(Boolean)(dr["Hidden"]),     //may conflict with styleClass, and may not be used; may be usefu for reporting notes/coments
                (String)(dr["ControlTip"]),   //@toolTip
                (String)(dr["popupText"]),    //@popupText
                (String)(dr["linkText"]),
                (String)(dr["linkText2"]),
                (String)(dr["AuthorityID"]),      //@AuthorityID
                (Boolean)(dr["showInReport"]),    //values are true, false, conditional
                (Boolean)(dr["mustImplement"]),
                (Int32)(dr["SortOrder"]),

                //!List Items (A, AF)
                (Boolean)(dr["SelectionDisablesChildren"]), //sdc
                (Boolean)(dr["SelectionDeselectsSiblings"]), //sds
                (Boolean)(dr["omitWhenSelected"])

                //!AF only
                //(Boolean)dr["responseRequired"];  //not used here
            );
            // populate column data only if necessary, do not want to enlarge the schema
            if (dr["TextCol2"].ToString() != "")
            {
                a.display = new display();
                a.display.property = a.display.property.AddItem(_srTmpTreeBuilder.MapDisplayProperty("ComboCol1", dr["TextCol1"].ToString()));
                a.display.property = a.display.property.AddItem(_srTmpTreeBuilder.MapDisplayProperty("ComboCol2", dr["TextCol2"].ToString()));
                if (dr["TextCol3"].ToString() != "")
                {
                    a.display.property = a.display.property.AddItem(_srTmpTreeBuilder.MapDisplayProperty("ComboCol3", dr["TextCol3"].ToString()));
                }
                if (dr["TextCol4"].ToString() != "")
                {
                    a.display.property = a.display.property.AddItem(_srTmpTreeBuilder.MapDisplayProperty("ComboCol4", dr["TextCol4"].ToString()));
                }
            }
            return a;
        }

        /// <summary>
        /// Creates a fixedlistfillinanswer from an item record in TE
        /// </summary>
        /// <remarks>
        /// Mapping info:
        /// answerid = ChecklistTemplateItemCkey
        /// title = VisibleText
        /// </remarks>
        /// <param name="dr">item record with information for a fillin answer (item type of 20)</param>
        /// <returns>a fixedlistfillinanswer class for the specific item</returns>
        private fixedlistfillinanswer MapAnswerFillin(DataRow dr)
        {
            fixedlistfillinanswer a = _srTmpTreeBuilder.MapAnswerFillin(
                //!Base item metadata
                (String)(dr["type"]),         //need standard type lists for Q, H, A, AF, Note, etc
                (String)(dr["styleClass"]),    //
                (String)(dr["ShortName"]),    //@name

                //!All Displayed Items
                (Decimal)(dr["ChecklistTemplateItemCkey"]),
                (String)(dr["VisibleText"]),  //@title or @val
                (String)(dr["longText"]),     //@alt-TextElementEnumerator in eCC, OtherText in SDC
                (String)(dr["ReportText"]),   //OtherText element in SDC

                (Boolean)(dr["enabled"]),
                !(Boolean)(dr["Hidden"]),     //may conflict with styleClass, and may not be used; may be usefu for reporting notes/coments
                (String)(dr["ControlTip"]),   //@toolTip
                (String)(dr["popupText"]),    //@popupText
                (String)(dr["linkText"]),
                (String)(dr["linkText2"]),
                (String)(dr["AuthorityID"]),      //@AuthorityID
                (Boolean)(dr["showInReport"]),    //values are true, false, conditional
                (Boolean)(dr["mustImplement"]),
                (Int32)(dr["SortOrder"]),

                //!List Items (A, AF)
                (Boolean)(dr["SelectionDisablesChildren"]), //sdc
                (Boolean)(dr["SelectionDeselectsSiblings"]), //sds
                (Boolean)(dr["omitWhenSelected"]),

                //!Response items QF and AF
                (String)(dr["DataType"]),
                (String)(dr["AnswerUnits"]),      //@answer-units
                (String)(dr["TextAfterConcept"]), //@textAfterResponse 

                //!AF only
                (Boolean)dr["responseRequired"]
                );
            return a;
        }
        /// <summary>
        /// Creates a checklist class from an item record in TE
        /// </summary>
        /// <remarks>
        /// Mapping info:
        /// checklistid = ChecklistTemplateItemCkey   
        /// </remarks>
        /// <param name="dr">item record with information for a checklist</param>
        /// <DeprecatedItems>DefaultTemplateVersion is deprecated</DeprecatedItems>
        /// <returns>a checklist class for the specific item</returns>
        private template MapTemplate(DataRow dr)
        {
            Boolean required = dr["Restrictions"].ToString().Contains("optional") ? false : true; //rlm 2015_12_11 changed to better reflect optionality
            template a = _srTmpTreeBuilder.MapTemplate(
                Convert.ToDecimal(dr["ChecklistTemplateVersionCkey"]),
                dr["ChecklistCKey"].ToString(),
                //DefaultTemplateVersion, //rlm: not used
                required,
                dr["VersionID"].ToString(),//templatexmlversion
                dr["CurrentFileName"].ToString(), //rlm: added 2105_12_10
                dr["ReleaseVersionSuffix"].ToString());  
            a.templateheader = MapTemplateHeader(dr);
            return a;
        }

        /// <summary>
        /// Creates a checklist header class from an item record in TE
        /// </summary>
        /// <remarks>
        /// Mapping info:
        /// category = category
        /// restrictions = restrictions
        /// AJCC_UICC version majorversion = AJCC_UICC_Version
        /// FIGO version majorversion = FIGO_Version
        /// CS version majorversion = CS_Version
        /// CS version schema numver = CS_SchemaNum
        /// genericheader = GenericHeaderText
        /// </remarks>
        /// <param name="dr">item record with information for a checklist</param>
        /// <returns>a checklist class for the specific item</returns>
        private templateheader MapTemplateHeader(DataRow dr)
        {
            templateheader a = _srTmpTreeBuilder.MapTemplateHeader(dr["OfficialName"].ToString(), dr["Restrictions"].ToString(), dr["Category"].ToString(), dr["GenericHeaderText"].ToString());
            a.versions = a.versions.AddItem(_srTmpTreeBuilder.MapVersion("AJCC_UICC", dr["AJCC_UICC_Version"].ToString(), "", ""));
            a.versions = a.versions.AddItem(_srTmpTreeBuilder.MapVersion("FIGO", dr["FIGO_Version"].ToString(), "", ""));
            a.versions = a.versions.AddItem(_srTmpTreeBuilder.MapVersion("CS", dr["CS_Version"].ToString(), "", dr["CS_Version"].ToString()));
            a.publication = MapPublication(dr);
            return a;
        }

        /// <summary>
        /// Creates a checklist header publication class from an item record in TE
        /// </summary>
        /// <remarks>
        /// Mapping info:
        /// approvalstatus = ApprovalStatus
        /// webpostingdate = WebPostingDate
        /// revisiondate = RevisionDate
        /// effectivedate = EffectiveDate
        /// retirementdate = RetireDate
        /// </remarks>
        /// <param name="dr">item record with information for a publication</param>
        /// <returns>a publication class for the specific item</returns>
        private templateheaderPublication MapPublication(DataRow dr)
        {
            templateheaderPublication a = _srTmpTreeBuilder.MapPublication(_srTmpTreeBuilder.MapDBNullToDT(dr["WebPostingDate"]),
                _srTmpTreeBuilder.MapDBNullToDT(dr["RevisionDate"]), _srTmpTreeBuilder.MapDBNullToDT(dr["EffectiveDate"]),
                _srTmpTreeBuilder.MapDBNullToDT(dr["RetireDate"]), dr["ApprovalStatus"].ToString());
            return a;
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SDC.DAL.DataSets
{

    /// <summary>
    /// Data access object for checklist template
    /// </summary>
    public class FormDesignDataSets : IFormDesignDataSets
    {
        /// <summary>
        /// Gets all template items for a specific template
        /// </summary>
        /// <param name="versionCkey">version ckey of the specific template</param>
        /// <returns>All template items for the specific template</returns>
        public DataTable dtGetFormDesign(string versionCkey)
        {
            #region SQL for SDC compatability 2015_11_12; RETIRED 8/26/2020
            //            const string getChecklistItem = @"
            //            SELECT  DISTINCT
            //            --Row metadata
            //                    @VersionCkey AS ChecklistTemplateVersionCkey,
            //                    i.ChecklistTemplateItemCkey, 
            //                    COALESCE (i.ParentItemCKey, @VersionCkey) AS ParentItemCkey,
            //                    i.ItemTypeKey,      --determines Q, A, AF, H, N
            //                    HasChildren=(IIF(kids.ParentItemCKey IS NULL, 'false', 'true')),

            //            --Question, Answer, Section, Note
            //                    i.VisibleText,      --@title for Q, A, H, N;  @text for eCC notes; Title @val in SDC
            //                    i.longText,         --@alt-text in eCC, OtherText in SDC            
            //                    i.ReportText,       --@reportText Text to appear on reports; uses 2 single-quotes to indicate that no text should appear in report
            //                    i.Locked,           --@readOnly in SDC
            //                    i.AuthorityRequired AS AuthorityRequired, --to be deprecated; now ecc ""Required eleement and @minCard=1, @mustImplement
            //                    i.SortOrder,        --@sort-order, @order in SDC


            //                    i.Required AS Required, --no longer used

            //                    i.enabled,          --@enabled; allow for to load with sections that are not active until an appropriate item is selected
            //                    i.visible,          --@visible
            //                    i.type,             --@type; useful for note types
            //                    i.styleClass,       --@styleClass in SDC; useful for sections with no borders;
            //                    i.showInReport,     --@showInReport; defaults to true; set to false when items are not to show in reprot

            //                    i.ShortName,        --@name
            //                    i.popupText,        --@popupText
            //                    i.ControlTip,       --@tooltip useful to avoid clutterning screeen with long notes, OtherText in SDC
            //                    i.linkText,         --@linkText link to online notes?
            //                    i.linkText2,        --@linkText2 link to online notes?
            //                    s.Source AS AuthorityID,  --element OtherText in SDC

            //                    CASE
            //                        WHEN COALESCE (p.ItemTypeKey, 0) IN (4, 23)
            //                        THEN 1 --Flag case when parent is QM & QS to handle list notes; 17 (QF) was removed rlm 4/2/2015
            //                        ELSE 0
            //                    END AS IsParentQuestion,

            //            --Q, S
            //                    COALESCE(i.MinRepetitions,1) AS minCard,    --@minCard if 0, the question or section is optional; map to @minCard
            //                    COALESCE(i.MaxRepetitions,1) AS maxCard,    --@maxCard
            //                    i.mustImplement,                            --@mustImplement
            //                    i.ordered,

            //            --Q
            //                    i.colTextDelimiter, --@colTextDelimiter; simpler way to handle multiple columns
            //                    i.numCols,          --@numCols; simpler way to handle multiple columns
            //                    i.storedCol,        --@storedCol; simpler way to handle multiple columns
            //                    i.ComboHeaderText,  --@listHeaderText
            //                    i.minSelections,    --@minSelections; special cases for multi-select questions; min val is 1
            //                    i.maxSelections,    --@maxSelections; special cases for multi-select questions, may be Null

            //                    --Q, To be deprecated
            //                    i.ComboCol1 AS TextCol1,
            //                    i.ComboCol2 AS TextCol2,
            //                    i.ComboCol3 AS TextCol3,
            //                    i.ComboCol4 AS TextCol4,

            //            --Lists




            //            --URI List


            //            --Injected Form


            //            --



            //            --QF, AF (Responses)
            //                    COALESCE (LODT.DataType, '') AS DataType,    --@datatype
            //                    COALESCE (i.AnswerUnits, '') AS AnswerUnits, --@answer-units
            //                    i.TextAfterConcept,                          --@textAfterResponse
            //                    i.DefaultValue,
            //                    i.AnswerMaxChars,
            //                    i.AnswerMaxDecimals,
            //                    i.AnswerMinValue,
            //                    i.AnswerMaxValue,


            //            --AF
            //                    i.responseRequired,                          --@responseRequired; for Answer fill-ins; default to false

            //            --A, AF
            //                    i.SelectionDisablesChildren,                                --@selectionDisablesChildren
            //                    i.SelectionDisablesSiblings AS SelectionDeselectsSiblings,  --@selectionDeselectsSiblings
            //                    i.omitWhenSelected                                          --@omitWhenSelected; Conditional reporting on List Items

            //            FROM    ChecklistTemplateItems
            //                            AS i WITH (NOLOCK)
            //                    LEFT OUTER JOIN  ListOfDataTypes LODT
            //                        ON i.AnswerDataTypeKey = LODT.DataTypeKey
            //                    LEFT OUTER JOIN  ChecklistTemplateItems
            //                        AS p WITH (NOLOCK)
            //                        ON i.ParentItemCKey = p.ChecklistTemplateItemCkey
            //                    LEFT OUTER JOIN  ListOfSources
            //                        AS s WITH (NOLOCK)
            //                        ON i.SourceCKey = s.SourceCkey
            //                    LEFT OUTER JOIN ChecklistTemplateItems AS kids ON
            //                    i.[ChecklistTemplateItemCkey] = kids.ParentItemCkey


            //WHERE     (i.ChecklistTemplateVersionCKey = @VersionCkey)
            //                        AND (i.ItemTypeKey IS NOT NULL)
            //                        AND (i.SkipConcept = 0)
            //            AND i.ChecklistTemplateItemCkey NOT IN
            //            (
            //                SELECT CTI_cKey
            //                FROM dbo.DeprecatedItemsCompleteCTE
            //            )
            //            ORDER BY i.SortOrder
            //";

            #region old SQL, retired June 3, 2016 (rlm)
            //            const string getChecklistItem = @"
            //			SELECT  
            //			--Row metadata
            //					i.ChecklistTemplateItemCkey, 
            //					COALESCE (i.ParentItemCKey, @VersionCkey) AS ParentItemCkey,
            //					i.ItemTypeKey,      --determines Q, A, AF, H, N

            //			--Question, Answer, Section, Note
            //					i.VisibleText,      --@title for Q, A, H, N;  @text for eCC notes; Title @val in SDC
            //					i.longText,         --@alt-text in eCC, OtherText in SDC            
            //					i.ReportText,       --@reportText Text to appear on reports; uses 2 single-quotes to indicate that no text should appear in report
            //					i.Locked,           --@readOnly in SDC
            //					i.AuthorityRequired AS AuthorityRequired, --to be deprecated; now ecc ""Required eleement and @minCard=1, @mustImplement
            //					i.SortOrder,        --@sort-order, @order in SDC


            //					i.Required AS Required, --no longer used

            //					i.enabled,          --@enabled; allow for to load with sections that are not active until an appropriate item is selected
            //					i.visible,          --@visible
            //					i.type,             --@type; useful for note types
            //					i.styleClass,       --@styleClass in SDC; useful for sections with no borders;
            //					i.showInReport,     --@showInReport; defaults to true; set to false when items are not to show in reprot

            //					i.ShortName,        --@name
            //					i.popupText,        --@popupText
            //					i.ControlTip,       --@tooltip useful to avoid clutterning screeen with long notes, OtherText in SDC
            //					i.linkText,         --@linkText link to online notes?
            //					i.linkText2,        --@linkText2 link to online notes?
            //					s.Source AS AuthorityID,  --element OtherText in SDC

            //					CASE
            //						WHEN COALESCE (p.ItemTypeKey, 0) IN (4, 23)
            //						THEN 1 --Flag case when parent is QM & QS to handle list notes; 17 (QF) was removed rlm 4/2/2015
            //						ELSE 0
            //					END AS IsParentQuestion,

            //			--Q, S
            //					COALESCE(i.MinRepetitions,1) AS minCard,    --@minCard if 0, the question or section is optional; map to @minCard
            //					COALESCE(i.MaxRepetitions,1) AS maxCard,    --@maxCard
            //					i.mustImplement,                            --@mustImplement
            //                    i.ordered,

            //			--Q
            //					i.colTextDelimiter, --@colTextDelimiter; simpler way to handle multiple columns
            //					i.numCols,          --@numCols; simpler way to handle multiple columns
            //					i.storedCol,        --@storedCol; simpler way to handle multiple columns
            //					i.ComboHeaderText,  --@listHeaderText
            //					i.minSelections,    --@minSelections; special cases for multi-select questions; min val is 1
            //					i.maxSelections,    --@maxSelections; special cases for multi-select questions, may be Null

            //					--Q, To be deprecated
            //					i.ComboCol1 AS TextCol1,
            //					i.ComboCol2 AS TextCol2,
            //					i.ComboCol3 AS TextCol3,
            //					i.ComboCol4 AS TextCol4,

            //			--Lists




            //			--URI List


            //			--Injected Form


            //			--



            //			--QF, AF (Responses)
            //					COALESCE (LODT.DataType, '') AS DataType,    --@datatype
            //					COALESCE (i.AnswerUnits, '') AS AnswerUnits, --@answer-units
            //					i.TextAfterConcept,                          --@textAfterResponse
            //					i.DefaultValue,
            //                    i.AnswerMaxChars,
            //					i.AnswerMinValue,
            //					i.AnswerMaxValue,

            //
            //			--AF
            //					i.responseRequired,                          --@responseRequired; for Answer fill-ins; default to false

            //			--A, AF
            //					i.SelectionDisablesChildren,                                --@selectionDisablesChildren
            //					i.SelectionDisablesSiblings AS SelectionDeselectsSiblings,  --@selectionDeselectsSiblings
            //					i.omitWhenSelected                                          --@omitWhenSelected; Conditional reporting on List Items

            //			FROM    ChecklistTemplateItems
            //							AS i WITH (NOLOCK)
            //					LEFT OUTER JOIN  ListOfDataTypes LODT
            //						ON i.AnswerDataTypeKey = LODT.DataTypeKey
            //					LEFT OUTER JOIN  ChecklistTemplateItems
            //						AS p WITH (NOLOCK)
            //						ON i.ParentItemCKey = p.ChecklistTemplateItemCkey
            //					LEFT OUTER JOIN  ListOfSources
            //						AS s WITH (NOLOCK)
            //						ON i.SourceCKey = s.SourceCkey


            //WHERE     (i.ChecklistTemplateVersionCKey = @VersionCkey)
            //						AND (i.ItemTypeKey IS NOT NULL)
            //						AND (i.SkipConcept = 0)
            //			ORDER BY i.SortOrder";
            #endregion
            #endregion
            #region SQL for SSP 8/26/2020
            const string getChecklistItem = @"
                    SELECT  DISTINCT
            --Row metadata
                    @TemplateVersionKey AS TemplateVersionKey,
                    i.ChecklistTemplateVersionCkey AS ChecklistTemplateVersionCkey,
			        i.TemplateVersionKey,
                    i.ChecklistTemplateItemCkey, 
			        i.U_ParentItemCKey,
                    COALESCE (i.U_ParentItemCKey, @TemplateVersionkey) AS ParentItemCkey,
                    i.ItemTypeKey,      --determines Q, A, AF, H, N
                    HasChildren=(IIF(kids.U_ParentItemCKey IS NULL, 'false', 'true')),
			        i.PubOption,
            --Question, Answer, Section, Note
                    i.VisibleText,      --@title for Q, A, H, N;  @text for eCC notes; Title @val in SDC
                    i.longText,         --@alt-text in eCC, OtherText in SDC            
                    i.ReportText,       --@reportText Text to appear on reports; uses 2 single-quotes to indicate that no text should appear in report
                    i.Locked,           --@readOnly in SDC
                    i.SortOrder,        --@sort-order, @order in SDC
                    i.enabled,          --@enabled; allow for to load with sections that are not active until an appropriate item is selected
                    i.visible,          --@visible
                    i.type,             --@type; useful for note types
                    i.styleClass,       --@styleClass in SDC; useful for sections with no borders;
                    i.showInReport,     --@showInReport; defaults to true; set to false when items are not to show in reprot
                    i.ShortName,        --@name
                    i.linkText,         --@linkText link to online notes?

                    CASE
                        WHEN COALESCE (p.ItemTypeKey, 0) IN (4, 23)
                        THEN 1 --Flag case when parent is QM & QS to handle list notes; 17 (QF) was removed rlm 4/2/2015
                        ELSE 0
                    END AS IsParentQuestion,
            --Q, S
                    COALESCE(i.MinRepetitions,1) AS minCard,    --@minCard if 0, the question or section is optional; map to @minCard
                    COALESCE(i.MaxRepetitions,1) AS maxCard,    --@maxCard
                    i.mustImplement,                            --@mustImplement
                    i.ordered,
            --Q
                    i.colTextDelimiter, --@colTextDelimiter; simpler way to handle multiple columns
                    i.numCols,          --@numCols; simpler way to handle multiple columns
                    i.storedCol,        --@storedCol; simpler way to handle multiple columns
                    --i.ComboHeaderText,  --@listHeaderText
                    i.minSelections,    --@minSelections; special cases for multi-select questions; min val is 1
                    i.maxSelections,    --@maxSelections; special cases for multi-select questions, may be Null

            --QF, AF (Responses)
                    COALESCE (LODT.DataType, '') AS DataType,    --@datatype
                    COALESCE (i.AnswerUnits, '') AS AnswerUnits, --@answer-units
                    i.TextAfterAnswer,                          --@textAfterResponse
                    i.DefaultValue,
                    i.AnswerMaxChars,
                    i.AnswerMaxDecimals,
                    i.AnswerMinValue,
                    i.AnswerMaxValue,
            --AF
                    i.responseRequired,                          --@responseRequired; for Answer fill-ins; default to false
            --A, AF
                    i.SelectionDisablesChildren,                                --@selectionDisablesChildren
                    i.SelectionDisablesSiblings AS SelectionDeselectsSiblings,  --@selectionDeselectsSiblings
                    i.omitWhenSelected                                          --@omitWhenSelected; Conditional reporting on List Items

        FROM    TemplateVersionItem
                        AS i WITH (NOLOCK)
                LEFT OUTER JOIN  ListDataType LODT
                    ON i.DataTypeKey = LODT.DataTypeKey
                LEFT OUTER JOIN  TemplateVersionItem
                    AS p WITH (NOLOCK)
                    ON i.U_ParentItemCKey = p.ChecklistTemplateItemCkey
                LEFT OUTER JOIN TemplateVersionItem AS kids ON
                i.[ChecklistTemplateItemCkey] = kids.U_ParentItemCkey

        WHERE	i.TemplateVersionKey = @TemplateVersionKey
		        AND (i.ItemTypeKey IS NOT NULL)
		        AND (i.SkipConcept = 0)
		        AND i.DeprecatedFlag = 0  --is this correct?
		        AND i.PubOption > 1
        ORDER BY i.SortOrder
";

            #region old SQL, retired June 3, 2016 (rlm)
            //            const string getChecklistItem = @"
            //			SELECT  
            //			--Row metadata
            //					i.ChecklistTemplateItemCkey, 
            //					COALESCE (i.ParentItemCKey, @VersionCkey) AS ParentItemCkey,
            //					i.ItemTypeKey,      --determines Q, A, AF, H, N

            //			--Question, Answer, Section, Note
            //					i.VisibleText,      --@title for Q, A, H, N;  @text for eCC notes; Title @val in SDC
            //					i.longText,         --@alt-text in eCC, OtherText in SDC            
            //					i.ReportText,       --@reportText Text to appear on reports; uses 2 single-quotes to indicate that no text should appear in report
            //					i.Locked,           --@readOnly in SDC
            //					i.AuthorityRequired AS AuthorityRequired, --to be deprecated; now ecc ""Required eleement and @minCard=1, @mustImplement
            //					i.SortOrder,        --@sort-order, @order in SDC


            //					i.Required AS Required, --no longer used

            //					i.enabled,          --@enabled; allow for to load with sections that are not active until an appropriate item is selected
            //					i.visible,          --@visible
            //					i.type,             --@type; useful for note types
            //					i.styleClass,       --@styleClass in SDC; useful for sections with no borders;
            //					i.showInReport,     --@showInReport; defaults to true; set to false when items are not to show in reprot

            //					i.ShortName,        --@name
            //					i.popupText,        --@popupText
            //					i.ControlTip,       --@tooltip useful to avoid clutterning screeen with long notes, OtherText in SDC
            //					i.linkText,         --@linkText link to online notes?
            //					i.linkText2,        --@linkText2 link to online notes?
            //					s.Source AS AuthorityID,  --element OtherText in SDC

            //					CASE
            //						WHEN COALESCE (p.ItemTypeKey, 0) IN (4, 23)
            //						THEN 1 --Flag case when parent is QM & QS to handle list notes; 17 (QF) was removed rlm 4/2/2015
            //						ELSE 0
            //					END AS IsParentQuestion,

            //			--Q, S
            //					COALESCE(i.MinRepetitions,1) AS minCard,    --@minCard if 0, the question or section is optional; map to @minCard
            //					COALESCE(i.MaxRepetitions,1) AS maxCard,    --@maxCard
            //					i.mustImplement,                            --@mustImplement
            //                    i.ordered,

            //			--Q
            //					i.colTextDelimiter, --@colTextDelimiter; simpler way to handle multiple columns
            //					i.numCols,          --@numCols; simpler way to handle multiple columns
            //					i.storedCol,        --@storedCol; simpler way to handle multiple columns
            //					i.ComboHeaderText,  --@listHeaderText
            //					i.minSelections,    --@minSelections; special cases for multi-select questions; min val is 1
            //					i.maxSelections,    --@maxSelections; special cases for multi-select questions, may be Null

            //					--Q, To be deprecated
            //					i.ComboCol1 AS TextCol1,
            //					i.ComboCol2 AS TextCol2,
            //					i.ComboCol3 AS TextCol3,
            //					i.ComboCol4 AS TextCol4,

            //			--Lists




            //			--URI List


            //			--Injected Form


            //			--



            //			--QF, AF (Responses)
            //					COALESCE (LODT.DataType, '') AS DataType,    --@datatype
            //					COALESCE (i.AnswerUnits, '') AS AnswerUnits, --@answer-units
            //					i.TextAfterConcept,                          --@textAfterResponse
            //					i.DefaultValue,
            //                    i.AnswerMaxChars,
            //					i.AnswerMinValue,
            //					i.AnswerMaxValue,

            //
            //			--AF
            //					i.responseRequired,                          --@responseRequired; for Answer fill-ins; default to false

            //			--A, AF
            //					i.SelectionDisablesChildren,                                --@selectionDisablesChildren
            //					i.SelectionDisablesSiblings AS SelectionDeselectsSiblings,  --@selectionDeselectsSiblings
            //					i.omitWhenSelected                                          --@omitWhenSelected; Conditional reporting on List Items

            //			FROM    ChecklistTemplateItems
            //							AS i WITH (NOLOCK)
            //					LEFT OUTER JOIN  ListOfDataTypes LODT
            //						ON i.AnswerDataTypeKey = LODT.DataTypeKey
            //					LEFT OUTER JOIN  ChecklistTemplateItems
            //						AS p WITH (NOLOCK)
            //						ON i.ParentItemCKey = p.ChecklistTemplateItemCkey
            //					LEFT OUTER JOIN  ListOfSources
            //						AS s WITH (NOLOCK)
            //						ON i.SourceCKey = s.SourceCkey


            //WHERE     (i.ChecklistTemplateVersionCKey = @VersionCkey)
            //						AND (i.ItemTypeKey IS NOT NULL)
            //						AND (i.SkipConcept = 0)
            //			ORDER BY i.SortOrder";
            #endregion
            #endregion

            return CreateDataTable(versionCkey, getChecklistItem);
        }

        /// <summary>
        /// Gets all necessary information of a checklist template version
        /// </summary>
        /// <param name="versionCkey">the specific version ckey of the checklist template version</param>
        /// <returns>Information about a checklist template version</returns>
        public DataTable dtGetFormDesignMetadata(string versionCkey)
        {
            #region SQL for PERC_ECC; RETIRED
            //const string getChecklistVersion = @"SELECT
            //    @VersionCkey AS ChecklistTemplateVersionCkey ,
            //    CTV.VisibleText ,
            //    --CTV.OfficialName AS longText ,
            //    --COALESCE(Chk.FullySpecifiedName , '') AS FSN ,
            //    --COALESCE(Chk.ConceptID , '') AS ConceptId ,
            //    --COALESCE(Chk.LegacyCode , '') AS LegacyCode ,
            //    --COALESCE(Chk.GID , '') AS GID ,
            //    --'' AS LOINC ,
            //    --1 AS ItemTypeKey ,
            //    Chk.ChecklistCKey AS ChecklistCkey ,
            //    Chk.ShortName,
            //    --1 AS SortOrder ,
            //    CTV.VersionID ,
            //    --ExtVer.AJCC_UICC_VersionNum ,
            //    AJCC.AJCC_UICC_Version ,
            //    CTV.Restrictions ,
            //    CONVERT (date, CTV.WebPostingDate) AS WebPostingDate,
            //    CONVERT (date, CTV.RevisionDate) AS RevisionDate,
            //    CONVERT (date, CTV.EffectiveDate) AS EffectiveDate,
            //    CONVERT (date, CTV.RetireDate) AS RetireDate,
            //    CTV.ApprovalStatus ,
            //    CTV.Description ,
            //    COALESCE(Cat.Category , '') AS Category ,
            //    CTV.GenericHeaderText ,
            //    FIGO.FIGO_Version ,
            //    CS.CS_Version ,
            //    --CTV.CS_SchemaNum ,
            //    --Src.Source ,
            //    COALESCE(LORT.ReleaseVersionSuffix, 'UNK') AS ReleaseVersionSuffix ,  --UNK = unknown status
            //    CTV.OfficialName ,
            //    CTV.CurrentFileName,
            //    CTV.CAP_ProtocolName,
            //    CTV.CAP_ProtocolVersion ,
            //    CTV.Lineage
            //FROM
            //    ChecklistTemplateVersions AS CTV WITH ( NOLOCK ) 
            //    LEFT OUTER JOIN Checklists AS Chk WITH ( NOLOCK )
            //    ON  CTV.ChecklistCkey = Chk.ChecklistCKey 
            //    LEFT OUTER JOIN ListOfAJCC_UICC_Versions AS ExtVer WITH ( NOLOCK )
            //    ON  CTV.AJCC_UICC_Version = ExtVer.AJCC_UICC_VersionKey 
            //    LEFT OUTER JOIN ListOfChecklistCategories AS Cat WITH ( NOLOCK )
            //    ON  Chk.CategoryCKey = Cat.CategoryCkey 
            //    LEFT OUTER JOIN ListOfSources AS Src WITH ( NOLOCK )
            //    ON  CTV.ChecklistSourceCKey = Src.SourceCkey
            //    LEFT OUTER JOIN ListofReleaseTypes AS LORT WITH ( NOLOCK )
            //    ON  CTV.ReleaseTypeCKey = LORT.ReleaseTypeCkey

            //    LEFT OUTER JOIN 
            //    (
            //        Select CTV.ChecklistTemplateVersionCKey, EV.ExternalVersion + 'th Edition' AS 'AJCC_UICC_Version'  FROM 
            //        dbo.ChecklistTemplateVersions CTV
            //        INNER JOIN 
            //        dbo.ChecklistVersion_ExternalVersion CVEV ON CTV.ChecklistTemplateVersionCKey = CVEV.ChecklistTemplateVersionCKey
            //        INNER JOIN
            //        dbo.ListOfExternalVersions EV ON EV.ExternalVersionCKey = CVEV.ExternalVersionCKey
            //        WHERE EV.OrganizationName = 'AJCC-UICC'
            //        ) AS AJCC
            //ON AJCC.ChecklistTemplateVersionCKey=CTV.ChecklistTemplateVersionCKey

            //LEFT OUTER JOIN 
            //(
            //    Select CTV.ChecklistTemplateVersionCKey, 'Version ' + EV.ExternalVersion AS 'CS_Version'  FROM 
            //        dbo.ChecklistTemplateVersions CTV
            //    INNER JOIN 
            //        dbo.ChecklistVersion_ExternalVersion CVEV ON CTV.ChecklistTemplateVersionCKey = CVEV.ChecklistTemplateVersionCKey
            //    INNER JOIN 
            //        dbo.ListOfExternalVersions EV ON EV.ExternalVersionCKey = CVEV.ExternalVersionCKey
            //    WHERE EV.OrganizationName = 'Collaborative Staging'
            //    )   AS CS

            //ON CS.ChecklistTemplateVersionCKey=CTV.ChecklistTemplateVersionCKey

            //LEFT OUTER JOIN 
            //        (
            //        Select CTV.ChecklistTemplateVersionCKey, EV.ExternalVersion AS 'Figo_Version'  FROM 
            //        dbo.ChecklistTemplateVersions CTV
            //        INNER JOIN 
            //        dbo.ChecklistVersion_ExternalVersion CVEV ON CTV.ChecklistTemplateVersionCKey = CVEV.ChecklistTemplateVersionCKey
            //        INNER JOIN
            //        dbo.ListOfExternalVersions EV ON EV.ExternalVersionCKey = CVEV.ExternalVersionCKey
            //        WHERE EV.OrganizationName = 'FIGO'
            //        ) AS FIGO
            //ON FIGO.ChecklistTemplateVersionCKey=CTV.ChecklistTemplateVersionCKey

            //WHERE
            //    ( CTV.ChecklistTemplateVersionCkey = @VersionCkey )";
            #endregion
            #region SQL for SSP
            const string getChecklistVersion = @"SELECT
                    PT.CTV_StaticKey ,
				    TV.TemplateVersionKey,
				    PT.Lineage AS ShortName,
                    PT.Lineage,
				    --PT.ShortName,
				    COALESCE(LRS.ReleaseVersionSuffix, 'UNK') AS ReleaseVersionSuffix ,  --UNK = unknown status
				    LRS.ReleaseState,
                    REPLACE(TV.Version,':', '.') AS VersionID,		
                    TV.OfficialNameProperty AS OfficialName,					
                    TV.VisibleTextProperty AS VisibleText,
                    PT.ProtocolTemplateKey AS ChecklistCkey,
                    
                    TV.RestrictionsTextProperty,
                    CONVERT (date, TV.WebPostingDateProperty) AS WebPostingDate,
                    CONVERT (date, TV.RevisionDateProperty) AS RevisionDate,
                    CONVERT (date, TV.AccreditationDeadlineDateProperty) AS EffectiveDate,
                    CONVERT (date, TV.RetireDateProperty) AS RetireDate,
                
                    TV.DescriptionProperty AS Description,
				    Groups.Category,
                    TV.GenericHeaderTextProperty AS GenericHeaderText ,
                    AJCC.AJCC_UICC_Version,
                    FIGO.FIGO_Version ,
                    CS.CS_Version ,
                    TV.L_FilenameLegacyProperty AS  CurrentFileName,
                    P.ProtocolName AS CAP_ProtocolName,
                    PV.ProtocolVersion AS CAP_ProtocolVersion                
				    , PT.Active AS PT_Active
				    , TV.Active AS TV_Active
                FROM
                    TemplateVersion AS TV 
				    JOIN ProtocolTemplate AS PT			ON TV.ProtocolTemplateKey = PT.ProtocolTemplateKey 
				    JOIN ProtocolVersion PV				ON PV.ProtocolVersionKey = TV.ProtocolVersionKey
				    JOIN Protocol P						ON P.ProtocolKey = PV.ProtocolKey
				    LEFT JOIN ProtocolGroupMapping PGM	ON PGM.ProtocolsKey = P.ProtocolKey
                    LEFT JOIN ListProtocolGroup AS LPG	ON PGM.ProtocolGroupKey = LPG.ProtocolGroupKey
                    LEFT JOIN ListReleaseState AS LRS	ON  TV.ReleaseStateKey = LRS.ReleaseStateKey

            LEFT OUTER JOIN 
            (
                Select TV.TemplateVersionKey, LS.Standard + 'th Edition' AS 'AJCC_UICC_Version'  
			    FROM dbo.TemplateVersion TV
                JOIN TemplateVersionStd TVS ON TV.TemplateVersionKey = TVS.TemplateVersionKey
                JOIN ListStandard LS ON LS.StandardKey = TVS.StandardKey
                WHERE LS.OrganizationName = 'AJCC-UICC'
                ) AS AJCC
			    ON AJCC.TemplateVersionKey=TV.TemplateVersionKey

            LEFT JOIN 
		    (
                Select TV.TemplateVersionKey, 'Version ' + LS.Standard AS 'CS_Version'  
			    FROM dbo.TemplateVersion TV
                    JOIN TemplateVersionStd TVS ON TV.TemplateVersionKey = TVS.TemplateVersionKey
                    JOIN ListStandard LS ON LS.StandardKey = TVS.StandardKey
                WHERE LS.OrganizationName = 'Collaborative Staging'
                )   AS CS
			    ON CS.TemplateVersionKey=TV.TemplateVersionKey

            LEFT JOIN 
		    (
                Select TV.TemplateVersionKey, LS.Standard AS 'FIGO_Version'  
			    FROM dbo.TemplateVersion TV
                INNER JOIN TemplateVersionStd TVS ON TV.TemplateVersionKey = TVS.TemplateVersionKey
                INNER JOIN ListStandard LS ON LS.StandardKey = TVS.StandardKey
                WHERE LS.OrganizationName = 'FIGO'
                ) AS FIGO
			    ON FIGO.TemplateVersionKey=TV.TemplateVersionKey
		    LEFT JOIN
		    (	SELECT PGM.ProtocolsKey, STRING_AGG(ProtocolGroup, ', ') WITHIN GROUP (ORDER BY ProtocolGroup ASC) AS Category
			    FROM  ProtocolGroupMapping PGM 
			    JOIN ListProtocolGroup LPG ON LPG.ProtocolGroupKey = PGM.ProtocolGroupKey
			    GROUP BY ProtocolsKey
		    )	AS Groups
		    ON Groups.ProtocolsKey = P.ProtocolKey 

    WHERE 
	    TV.TemplateVersionKey = @TemplateVersionKey 
    ORDER BY PT.Lineage";
            #endregion 


            return CreateDataTable(versionCkey, getChecklistVersion);
        }

        /// <summary>
        /// Gets all procedures for specified checklist template version
        /// </summary>
        /// <param name="versionCkey">version ckey of the checklist template version</param>
        /// <returns>all procedures for a checklist template version</returns>
        public DataTable GetChecklistVersionProcedures(string versionCkey)
        {
            const string getChecklistVersionProcedures = @"
                SELECT ChecklistTemplateVersionCkey, VisibleText, ConceptID, FullySpecifiedName, LegacyCode
                FROM TemplateProcedures (NOLOCK)
                WHERE ChecklistTemplateVersionCkey = @VersionCkey
            ";

            return CreateDataTable(versionCkey, getChecklistVersionProcedures);
        }

        /// <summary>
        /// Gets all the active checklist from the database where filename exist
        /// </summary>
        /// <param name=""></param>
        /// <returns>all the active checklist and filename map to generate xml and html</returns>
        public DataTable dtGetTemplateList(string versionCkey)
        {
            const string getChecklistTemplateMap = @"
                    SELECT RTRIM(CAST(ChecklistTemplateVersionCkey AS CHAR)) ,
                    RTRIM(CurrentFileName)  AS FileNm 
                    FROM ChecklistTemplateVersions CTV
                    WHERE VisibleText LIKE '<*%' 
                    and CurrentFileName is not null       
                    ORDER BY CurrentFileName
            ";

            return CreateDataTable(versionCkey, getChecklistTemplateMap);
        }
        private DataTable CreateDataTable(string TemplateVersionKey, string sql)
        {
            DataTable dt;
            using (var con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = Properties.Settings.Default.SSP_Con;
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        if (int.TryParse(TemplateVersionKey, out int result))
                            cmd.Parameters.Add("@TemplateVersionkey", SqlDbType.Int).Value = result;
                        con.Open();

                        dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                }
                catch
                { //main DB can't be reached, so use local connection string with local SQL Svr
                    //con.ConnectionString = Properties.Settings.Default.SSP_Con_local;
                    //using (var cmd = con.CreateCommand())
                    //{
                    //    cmd.CommandType = CommandType.Text;
                    //    cmd.CommandText = sql;
                    //    cmd.Parameters.Add("@VersionCkey", SqlDbType.Decimal).Value = TemplateVersionKey;
                    //    con.Open();

                    //    dt = new DataTable();
                    //    dt.Load(cmd.ExecuteReader());
                    //    return dt;
                    con.Dispose();
                    return null;
                    //}
                }
            }
        }
    }
}


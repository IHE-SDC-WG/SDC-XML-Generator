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
        public DataTable dtGetFormDesign(Decimal versionCkey)
        {
            #region SQL for SDC compatibility 2015_11_12
            const string getChecklistItem = @"
			SELECT  DISTINCT
			--Row metadata
					i.ChecklistTemplateItemCkey, 
					COALESCE (i.ParentItemCKey, @VersionCkey) AS ParentItemCkey,
					i.ItemTypeKey,      --determines Q, A, AF, H, N
					HasChildren=(IIF(kids.ParentItemCKey IS NULL, 'false', 'true')),

			--Question, Answer, Section, Note
					i.VisibleText,      --@title for Q, A, H, N;  @text for eCC notes; Title @val in SDC
					i.longText,         --@alt-text in eCC, OtherText in SDC            
					i.ReportText,       --@reportText Text to appear on reports; uses 2 single-quotes to indicate that no text should appear in report
					i.Locked,           --@readOnly in SDC
					i.AuthorityRequired AS AuthorityRequired, --to be deprecated; now ecc ""Required eleement and @minCard=1, @mustImplement
					i.SortOrder,        --@sort-order, @order in SDC


					i.Required AS Required, --no longer used

					i.enabled,          --@enabled; allow for to load with sections that are not active until an appropriate item is selected
					i.visible,          --@visible
					i.type,             --@type; useful for note types
					i.styleClass,       --@styleClass in SDC; useful for sections with no borders;
					i.showInReport,     --@showInReport; defaults to true; set to false when items are not to show in reprot

					i.ShortName,        --@name
					i.popupText,        --@popupText
					i.ControlTip,       --@tooltip useful to avoid clutterning screeen with long notes, OtherText in SDC
					i.linkText,         --@linkText link to online notes?
					i.linkText2,        --@linkText2 link to online notes?
					s.Source AS AuthorityID,  --element OtherText in SDC

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
					i.ComboHeaderText,  --@listHeaderText
					i.minSelections,    --@minSelections; special cases for multi-select questions; min val is 1
					i.maxSelections,    --@maxSelections; special cases for multi-select questions, may be Null

					--Q, To be deprecated
					i.ComboCol1 AS TextCol1,
					i.ComboCol2 AS TextCol2,
					i.ComboCol3 AS TextCol3,
					i.ComboCol4 AS TextCol4,

			--Lists




			--URI List


			--Injected Form


			--



			--QF, AF (Responses)
					COALESCE (LODT.DataType, '') AS DataType,    --@datatype
					COALESCE (i.AnswerUnits, '') AS AnswerUnits, --@answer-units
					i.TextAfterConcept,                          --@textAfterResponse
					i.DefaultValue,
                    i.AnswerMaxChars,
					i.AnswerMinValue,
					i.AnswerMaxValue,


			--AF
					i.responseRequired,                          --@responseRequired; for Answer fill-ins; default to false

			--A, AF
					i.SelectionDisablesChildren,                                --@selectionDisablesChildren
					i.SelectionDisablesSiblings AS SelectionDeselectsSiblings,  --@selectionDeselectsSiblings
					i.omitWhenSelected                                          --@omitWhenSelected; Conditional reporting on List Items

			FROM    ChecklistTemplateItems
							AS i WITH (NOLOCK)
					LEFT OUTER JOIN  ListOfDataTypes LODT
						ON i.AnswerDataTypeKey = LODT.DataTypeKey
					LEFT OUTER JOIN  ChecklistTemplateItems
						AS p WITH (NOLOCK)
						ON i.ParentItemCKey = p.ChecklistTemplateItemCkey
					LEFT OUTER JOIN  ListOfSources
						AS s WITH (NOLOCK)
						ON i.SourceCKey = s.SourceCkey
					LEFT OUTER JOIN ChecklistTemplateItems AS kids ON
					i.[ChecklistTemplateItemCkey] = kids.ParentItemCkey


WHERE     (i.ChecklistTemplateVersionCKey = @VersionCkey)
						AND (i.ItemTypeKey IS NOT NULL)
						AND (i.SkipConcept = 0)
            AND i.ChecklistTemplateItemCkey NOT IN
            (
	            SELECT CTI_cKey
	            FROM dbo.DeprecatedItemsCompleteCTE
            )
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
        public DataTable dtGetFormDesignMetadata(Decimal versionCkey)
        {
            #region SQL
            const string getChecklistVersion = @"SELECT
	@VersionCkey AS ChecklistTemplateVersionCkey ,
	CTV.VisibleText ,
	CTV.OfficialName AS longText ,
	COALESCE(Chk.FullySpecifiedName , '') AS FSN ,
	COALESCE(Chk.ConceptID , '') AS ConceptId ,
	COALESCE(Chk.LegacyCode , '') AS LegacyCode ,
	COALESCE(Chk.GID , '') AS GID ,
	'' AS LOINC ,
	1 AS ItemTypeKey ,
	Chk.ChecklistCKey AS ChecklistCkey ,
	1 AS SortOrder ,
	CTV.VersionID ,
	--ExtVer.AJCC_UICC_VersionNum ,
	AJCC.AJCC_UICC_Version ,
	CTV.Restrictions ,
	CTV.WebPostingDate ,
	CTV.RevisionDate ,
	CTV.EffectiveDate ,
	CTV.RetireDate ,
	CTV.ApprovalStatus ,
	CTV.Description ,
	COALESCE(Cat.Category , '') AS Category ,
	CTV.GenericHeaderText ,
	FIGO.FIGO_Version ,
	CS.CS_Version ,
	--CTV.CS_SchemaNum ,
	Src.Source ,
	CTV.OfficialName ,
	CTV.CurrentFileName
FROM
	ChecklistTemplateVersions AS CTV WITH ( NOLOCK ) 
	LEFT OUTER JOIN Checklists AS Chk WITH ( NOLOCK )
	ON  CTV.ChecklistCkey = Chk.ChecklistCKey 
	LEFT OUTER JOIN ListOfAJCC_UICC_Versions AS ExtVer WITH ( NOLOCK )
	ON  CTV.AJCC_UICC_Version = ExtVer.AJCC_UICC_VersionKey 
	LEFT OUTER JOIN ListOfChecklistCategories AS Cat WITH ( NOLOCK )
	ON  Chk.CategoryCKey = Cat.CategoryCkey 
	LEFT OUTER JOIN ListOfSources AS Src WITH ( NOLOCK )
	ON  CTV.ChecklistSourceCKey = Src.SourceCkey
	LEFT OUTER JOIN 
	(
		Select CTV.ChecklistTemplateVersionCKey, EV.ExternalVersion + 'th Edition' AS 'AJCC_UICC_Version'  FROM 
		dbo.ChecklistTemplateVersions CTV
		INNER JOIN 
		dbo.ChecklistVersion_ExternalVersion CVEV ON CTV.ChecklistTemplateVersionCKey = CVEV.ChecklistTemplateVersionCKey
		INNER JOIN
		dbo.ListOfExternalVersions EV ON EV.ExternalVersionCKey = CVEV.ExternalVersionCKey
		WHERE EV.OrganizationName = 'AJCC-UICC'
		) AS AJCC
ON AJCC.ChecklistTemplateVersionCKey=CTV.ChecklistTemplateVersionCKey

LEFT OUTER JOIN 
(
	Select CTV.ChecklistTemplateVersionCKey, 'Version ' + EV.ExternalVersion AS 'CS_Version'  FROM 
		dbo.ChecklistTemplateVersions CTV
	INNER JOIN 
		dbo.ChecklistVersion_ExternalVersion CVEV ON CTV.ChecklistTemplateVersionCKey = CVEV.ChecklistTemplateVersionCKey
	INNER JOIN 
		dbo.ListOfExternalVersions EV ON EV.ExternalVersionCKey = CVEV.ExternalVersionCKey
	WHERE EV.OrganizationName = 'Collaborative Staging'
	)   AS CS

ON CS.ChecklistTemplateVersionCKey=CTV.ChecklistTemplateVersionCKey

LEFT OUTER JOIN 
		(
		Select CTV.ChecklistTemplateVersionCKey, EV.ExternalVersion AS 'Figo_Version'  FROM 
		dbo.ChecklistTemplateVersions CTV
		INNER JOIN 
		dbo.ChecklistVersion_ExternalVersion CVEV ON CTV.ChecklistTemplateVersionCKey = CVEV.ChecklistTemplateVersionCKey
		INNER JOIN
		dbo.ListOfExternalVersions EV ON EV.ExternalVersionCKey = CVEV.ExternalVersionCKey
		WHERE EV.OrganizationName = 'FIGO'
		) AS FIGO
ON FIGO.ChecklistTemplateVersionCKey=CTV.ChecklistTemplateVersionCKey

WHERE
	( CTV.ChecklistTemplateVersionCkey = @VersionCkey )";
            #endregion

            return CreateDataTable(versionCkey, getChecklistVersion);
        }

        /// <summary>
        /// Gets all procedures for specified checklist template version
        /// </summary>
        /// <param name="versionCkey">version ckey of the checklist template version</param>
        /// <returns>all procedures for a checklist template version</returns>
        public DataTable GetChecklistVersionProcedures(Decimal versionCkey)
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
        public DataTable dtGetTemplateList(Decimal versionCkey)
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
        private DataTable CreateDataTable(decimal CTV_Ckey, string sql)
        {
            DataTable dt;
            using (var con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.PERC_eCC_Con;
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@VersionCkey", SqlDbType.Decimal).Value = CTV_Ckey;
                    con.Open();

                    dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
            }
        }

    }





}

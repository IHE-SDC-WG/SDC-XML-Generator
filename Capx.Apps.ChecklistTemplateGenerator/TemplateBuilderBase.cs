using System;
using System.Data;
using System.Collections.Generic;
using Capx.Apps.ChecklistTemplateGenerator.DAL;

namespace Capx.Apps.ChecklistTemplateGenerator
{    
    /// <summary>
    /// Abstract class that implements interface ITemplateBuilder
    /// Used to create template generator for template schema
    /// </summary>
    public abstract class TemplateBuilderBase //: ITemplateBuilder  //
    {
        #region Public Variables

        /// <summary>
        /// Name of the template to get from file
        /// </summary>
        public String TemplateName
        {
            get { return _templateName; }
            set { _templateName = value; }
        }

        /// <summary>
        /// Data repository location
        /// </summary>
        public String TemplateRepositoryUri
        {
            get { return _templateRepositoryUri; }
            set { _templateRepositoryUri = value; }
        }

        /// <summary>
        /// Available templates in database
        /// </summary>
        public String AvailableTemplate
        {
            get { return _availableTemplate; }
            set { _availableTemplate = value; }
        }

        /// <summary>
        /// Xsl file name for transform to be included in template xml
        /// </summary>
        public String XsltFileName
        {
            get { return _xsltFileName; }
            set { _xsltFileName = value; }
        }

        /// <summary>
        /// Default template version 
        /// </summary>
        public String DefaultTemplateVersion
        {
            get { return _defaultTemplateVersion; }
            set { _defaultTemplateVersion = value; }
        }

        /// <summary>
        /// Data access object for template
        /// </summary>
        public ChecklistTemplateXmlDao ChecklistTemplateXmlDao
        {
            get { return _checklistTemplateXmlDao; }
            set { _checklistTemplateXmlDao = value; }
        }

        #endregion

        #region ITemplateBuilder Implementation

        /// <summary>
        /// Gets the template xml by template name
        /// </summary>
        /// <param name="templateName">Name of template to get</param>
        /// <returns>Template XML</returns>
        public String GetTemplate(String templateName)
        {
            if (templateName != String.Empty && templateName.LastIndexOf('.') > 0)
            {
                String ckey = templateName.Substring(0, templateName.LastIndexOf('.'));
                return GetTemplateByCkey(ckey);
            }
            return String.Empty;
        }


        /// <summary>
        /// Gets the default template 
        /// </summary>
        /// <returns>Template XML</returns>
        public String GetTemplate()
        {
            return GetTemplate(_templateName);
        }

        /// <summary>
        /// Gets template by Ckey
        /// </summary>
        /// <param name="CTV_Ckey_in">Ckey of template to get</param>
        /// <returns>Template XML</returns>
        public String GetTemplateByCkey(String CTV_Ckey_in)
        {
            Decimal CTV_Ckey = 0;
            if (CTV_Ckey_in.Trim() != String.Empty && Decimal.TryParse(CTV_Ckey_in, out CTV_Ckey))
            {
                if (CTV_Ckey > 0)
                {
                    DataTable checklistItems = _checklistTemplateXmlDao.GetChecklistItems(CTV_Ckey);
                    DataTable checklistVersion = _checklistTemplateXmlDao.GetChecklistVersion(CTV_Ckey);
                    DataTable checklistVersionProcedure = _checklistTemplateXmlDao.GetChecklistVersionProcedures(CTV_Ckey);
                    return CreateTemplateFromDT(checklistVersion, checklistItems, checklistVersionProcedure);
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// Gets the exceptions (queries) of the repository
        /// </summary>
        /// <param name="type">Type of exceptions to get</param>
        /// <returns>All repository exceptions of specified type</returns>
        public List<TemplateRepositoryException> GetRepositoryException(TRExceptionType type)
        {
            List<TemplateRepositoryException> exc = new List<TemplateRepositoryException>();
            DataTable dt = new DataTable();
            switch (type)
            {
                case TRExceptionType.NOTEPARENT:
                    dt = _checklistTemplateXmlDao.GetNoteParentItems();
                    break;
                case TRExceptionType.NOANSWERQUESTION:
                    dt = _checklistTemplateXmlDao.GetQuestionWithNoAnswerItems();
                    break;
                case TRExceptionType.ANSWERINVALIDPARENT:
                    dt = _checklistTemplateXmlDao.GetAnswerWithNoParentItems();
                    break;
                case TRExceptionType.HEADERINVALIDPARENT:
                    dt = _checklistTemplateXmlDao.GetHeaderGroupWithInvalidParentItems();
                    break;
                case TRExceptionType.NONQUESTIONANSWER:
                    dt = _checklistTemplateXmlDao.GetAnswerWithNonQuestionParentItems();
                    break;
                case TRExceptionType.QUESTIONINVALIDPARENT:
                    dt = _checklistTemplateXmlDao.GetQuestionWithNoParentItems();
                    break;
                case TRExceptionType.TOTALANDREQUIREDQUESTION:
                    dt = _checklistTemplateXmlDao.GetTotalAndRequiredQuestions();
                    break;
            }
            foreach (DataRow dr in dt.Rows)
            {
                TemplateRepositoryException e = new TemplateRepositoryException();
                if (type == TRExceptionType.TOTALANDREQUIREDQUESTION)
                {
                    e.ExceptionType = TemplateRepositoryException.MapDBExceptionType(Convert.ToInt32(dr[0]));
                    e.ItemCkey = Convert.ToDecimal(dr[1]);
                    e.TemplateCkey = Convert.ToDecimal(dr[1]);
                    e.ItemText = String.Format("Total Question: {0}, Required Question: {1}", Convert.ToString(dr[2]), Convert.ToString(dr[3]));
                    e.ConceptId = "";
                    e.TemplateText = Convert.ToString(dr[4]);
                    e.ItemType = ItemType.CHECKLIST;
                }
                else
                {
                    e.ExceptionType = TemplateRepositoryException.MapDBExceptionType(Convert.ToInt32(dr[0]));
                    e.ItemCkey = Convert.ToDecimal(dr[1]);
                    e.TemplateCkey = Convert.ToDecimal(dr[2]);
                    e.ItemText = Convert.ToString(dr[3]);
                    e.ConceptId = Convert.ToString(dr[4]);
                    e.TemplateText = Convert.ToString(dr[5]);
                    e.ItemType = TemplateRepositoryException.MapItemType(Convert.ToInt32(dr[6]));
                }
                exc.Add(e);
            }
            return exc;
        }

        /// <summary>
        /// Gets all repository exceptions at once
        /// </summary>
        /// <returns>All repository exceptions as identified in DAO</returns>
        public List<TemplateRepositoryException> GetRepositoryException()
        {
            Array values = Enum.GetValues(typeof(TRExceptionType));
            List<TemplateRepositoryException> exc = new List<TemplateRepositoryException>();
            for (Int32 i = 0; i < values.Length; i++)
            {
                exc.AddRange(GetRepositoryException((TRExceptionType)values.GetValue(i)));
            }
            return exc;
        }

        /// <summary>
        /// Creates template with information from data tables 
        /// </summary>
        /// <param name="checklistVersion">Data table with information about the template version</param>
        /// <param name="checklistItems">Data table with information about the template items</param>
        /// <returns>Template Xml</returns>
        public abstract String CreateTemplateFromDT(DataTable checklistVersion, DataTable checklistItems);

        /// <summary>
        /// Creates template with information from data tables 
        /// </summary>
        /// <param name="checklistVersion">Data table with information about the template version</param>
        /// <param name="checklistItems">Data table with information about the template items</param>
        /// <param name="checklistVersionProcedure">Data table with information about the template procedures</param>
        /// <returns>Template Xml</returns>
        public abstract String CreateTemplateFromDT(DataTable checklistVersion, DataTable checklistItems, DataTable checklistVersionProcedure);

        /// <summary>
        /// Gets the names and ckeys of all available templates
        /// </summary>
        /// <returns>Dictionary of name/ckey pairs</returns>
        public Dictionary<String, String> GetAvailableTemplates()
        {
            Dictionary<String, String> t = new Dictionary<string, string>();
            DataTable templates = _checklistTemplateXmlDao.GetAvailableTemplates();
            String templateDesc = "";
            foreach (DataRow dr in templates.Rows)
            {
                templateDesc = dr[1].ToString();
                if (Convert.ToInt32(dr[2]) > 50)
                {
                    templateDesc += "...";
                }
                t.Add(dr[0].ToString(), templateDesc);
            }
            return t;
        }

        #endregion

        #region Private Field

        private String _templateName = String.Empty;

        private String _templateRepositoryUri = String.Empty;

        private String _availableTemplate = "";

        private ChecklistTemplateXmlDao _checklistTemplateXmlDao;

        private String _xsltFileName = String.Empty;

        private String _defaultTemplateVersion = String.Empty;

        #endregion
    }
}

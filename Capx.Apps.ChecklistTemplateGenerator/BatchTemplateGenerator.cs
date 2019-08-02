using System;
using System.Diagnostics;
using MyVB = Microsoft.VisualBasic;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace Capx.Apps.ChecklistTemplateGenerator
{
    /// <summary>
    /// Template Generator to generate a batch of checklist templates using the available generators
    /// </summary>
    public class BatchTemplateGenerator //: IBatchTemplateXmlGenerator //Injected by Dependency Injection
    {
        private IDictionary<String, String> _templatesMap;
        private SrTemplateDataMapper _TemplateDataMapper;
        private String _templateGeneratorPath;


        public BatchTemplateGenerator()
        {
            //_templateGeneratorPath = Properties.Settings.Default.FilePath;
        }
        /// <summary>
        /// Template dictionary to map all the checklist template version keys for generation
        /// </summary>
        public IDictionary<String, String> TemplatesMap
        {
            get { return _templatesMap; }
            set { _templatesMap = value; }
        }

        /// <summary>
        /// Template builder for building the templates.  This can be any class that implements the IChecklistTemplateGenerator interface
        /// </summary>
        public SrTemplateDataMapper TemplateDataMapper
        {
            get { return _TemplateDataMapper; }
            set { _TemplateDataMapper = value; }
        }

        /// <summary>
        /// Directory file path to put generated checklist template xml files
        /// </summary>
        public String TemplateGeneratorPath
        {
            get { return _templateGeneratorPath; }
            set { _templateGeneratorPath = value; }
        }

        /// <summary>
        /// Generates all of the templates listed in the TemplatesMap dictionary
        /// </summary>
        public String Generate()
        {
            string BESTfilename = "";
            String USERfilename = "";
            foreach (KeyValuePair<string, string> templateMetaData in _templatesMap)
            {
                String ckey = templateMetaData.Key;
                USERfilename = templateMetaData.Value;
                BESTfilename = "";
                String templateXml = _TemplateDataMapper.CreateOneTemplateByCkey(ckey);

                System.Console.WriteLine("Ckey: " + ckey);
                System.Console.WriteLine("Xml File Name: " + BESTfilename + ".xml");
                //System.Console.WriteLine("templateXml: " + templateXml);  //this takes a few seconds to output, so keep it commented out

                if (templateXml != string.Empty)
                {
                    String filePath = String.Format(@"{0}\{1}", _templateGeneratorPath, BESTfilename + ".xml");
                    File.WriteAllText(filePath, templateXml, Encoding.UTF8);
                    Debug.Assert(MyVB.FileIO.FileSystem.FileExists(filePath));
                }
                else
                {
                    throw new Exception("No template Xml available.");
                }
            }
            return BESTfilename;  //TODO:  this is a kludge to return the best filename to the caller.  It only works when generating a single template.
        }
    }
}
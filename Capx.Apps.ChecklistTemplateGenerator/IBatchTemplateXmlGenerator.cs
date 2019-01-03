using System;
using System.Collections.Generic;
using Capx.Apps.ChecklistTemplateGenerator;
namespace Capx.Apps.ChecklistTemplateGenerator
{
    public interface IBatchTemplateXmlGenerator
    {
        IDictionary<string, string> TemplatesMap { get; set; }
        String TemplateGeneratorPath { get; set; }
        //ITemplateBuilder TemplateBuilder { get; set; }
        SrTemplateBuilder TemplateBuilder { get; set; }
        void Generate();
    }
}

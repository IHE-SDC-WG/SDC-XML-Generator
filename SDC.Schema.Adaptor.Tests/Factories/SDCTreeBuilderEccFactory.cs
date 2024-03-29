using System.Data;
using SDC.DAL.DataSets;
using SDC;
using System;
using Microsoft.Pex.Framework;

namespace SDC
{
    /// <summary>A factory for SDC.SDCTreeBuilderEcc instances</summary>
    public static partial class SDCTreeBuilderEccFactory
    {
        /// <summary>A factory for SDC.SDCTreeBuilderEcc instances</summary>
        [PexFactoryMethod(typeof(SDCTreeBuilderEcc))]
        public static SDCTreeBuilderEcc Create(
            string TemplateVersionkey_s,
            IFormDesignDataSets dataSets_iFormDesignDataSets,
            string xsltPath_s1,
            DataRow value_dataRow
        )
        {
            SDCTreeBuilderEcc sDCTreeBuilderEcc = new SDCTreeBuilderEcc
                                                      (TemplateVersionkey_s, dataSets_iFormDesignDataSets, xsltPath_s1);
            ((SDCTreeBuilder)sDCTreeBuilderEcc).drFormDesign = value_dataRow;
            return sDCTreeBuilderEcc;

            // TODO: Edit factory method of SDCTreeBuilderEcc
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}

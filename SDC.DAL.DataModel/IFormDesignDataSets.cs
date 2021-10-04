using System;
using System.Data;
using SDC.DAL.DataSets;
namespace SDC.DAL.DataSets
{
    public interface IFormDesignDataSets
    {
        DataTable dtGetFormDesign(string versionCkey);
        DataTable dtGetFormDesignMetadata(string versionCkey);
        DataTable dtGetTemplateList(string versionCkey);
        //DataTable dtGetHeader(decimal versionCkey);
        //DataTable dtGetBody(decimal versionCkey);
        //DataTable dtGetFooter(decimal versionCkey);

    }
}

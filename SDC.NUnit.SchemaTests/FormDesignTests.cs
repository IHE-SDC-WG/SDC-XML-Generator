using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::SDC;
using System.Data;
using SDC.DAL.DataSets;
using System.Xml.Serialization;
using System.Diagnostics;



[TestFixture]
public class SDCTests
{
    [Test]
    public void FormDesign()
    {
        var FD = new FormDesignType(null);
        //var dr = new DataRow();
        //FD.Body.ID="123";//should fail
        FD.Body = new SectionItemType();
        FD.Body.ID = "123";//should pass

        var b = new SectionItemType();

        FD.Body = b;
        b.baseURI = "www.cap.org/sdc/a1234/123";
        b.AddSection();

        //b.A

        Assert.Pass("Form Design Passed1");
        Assert.Pass("Form Design Passed2");
    }

    [Test]
    public void Section()
    {
        var s = new SectionItemType();
        var d = (SDC.DisplayedType)s;
        s = new SectionItemType();


        var b = new SectionItemType();

        //s.Body = b;
        b.baseURI = "www.cap.org/sdc/a1234/123";
        //b.A

        Assert.Pass("Form Design Passed1");
        Assert.Pass("Form Design Passed2");
    }

    [Test]
    public void DataTableBuilder()
    {
        var fdd = new FormDesignDataSets();
        var s =new SDCTreeBuilderEcc("129.1000043", fdd, "srtemplate.xslt");

        var str = s.FormDesign.TreeBuilder.SerializeFormDesignTree();
        System.Diagnostics.Debug.WriteLine(str);
        //fd.SaveToFile(@"c:tmp\eCC\SDCTestFile" + DateTime.Now.Second.ToString());
        
        //tb.SerializeFormDesignTree;
       //var str = fd.Serialize();

    }
}

[TestFixture]
public class BuildSDCTree
{
    protected FormDesignDataSets fdd;
    [SetUp]
    public void SetUp()
    {  }


    [Test]
    public void SerializationTest()
    {
        var ser = new XmlSerializer(typeof(FormDesignType));

        var fdd = new FormDesignDataSets();
        var stb = new SDCTreeBuilderEcc("189.1000043", fdd, "srtemplate.xslt");

        var ns = new XmlSerializerNamespaces();
        ns.Add("",@"http://healthIT.gov/sdc");

        
        var writer = new System.IO.StringWriter();
        ser.Serialize(writer, stb.FormDesign, ns);
        String formDesignXml = writer.ToString();
        Debug.Print(formDesignXml);

        Debug.Print(stb.FormDesign.Serialize());
        

    }
}
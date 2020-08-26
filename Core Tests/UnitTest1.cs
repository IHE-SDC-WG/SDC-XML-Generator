using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
using global::SDC;
using SDC.DAL.DataSets;
using System.Collections.Generic;
using System.Data;
using SDC.Schema2;
using System.Linq;
using System.Security.Claims;
//using SDC.Schema;

namespace MSTestsCore
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var serializer = new XmlSerializer(typeof(SDC.Schema2.BaseType));
        }

        [TestMethod]
        public void SerializeAll()
        {    //rlm 2017/08/31
            Dictionary<String, String> templatesMap = new Dictionary<String, String>();
            string key, val;

            var dt = new SDC.DAL.DataSets.FormDesignDataSets();
            DataTable templateDT = dt.dtGetTemplateList(0);  //the decimal parameter is not used

            foreach (DataRow dr in templateDT.Rows)
            {

                key = (string)dr.ItemArray[0];
                val = (string)dr.ItemArray[1] + "_SDC.xml";

                templatesMap.Add(key, val);
                Debug.Print(templatesMap[key].ToString());

                var ser = new XmlSerializer(typeof(FormDesignType));
                var fdd = new FormDesignDataSets();
                SDCTreeBuilderEcc stb;
                stb = new SDCTreeBuilderEcc(key, fdd, "srtemplate.xslt");
                var filename = stb.FormDesign.filename;

                //var fd = stb.FormDesign;
                //var n = fd.IdentifiedTypes["11.1000043"];
                //move static classes to FormDesign instance



                String formDesignXml = stb.FormDesign.Serialize();
                string orig = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                string fix = orig + "\r\n" + "<?xml-stylesheet type=\"text/xsl\" href=\"sdctemplate.xslt\"?>";

                formDesignXml = formDesignXml.Replace(orig, fix);
                //formDesignXml = formDesignXml.Replace("&amp;", "&");
                //Debug.WriteLine(formDesignXml);
                System.IO.File.WriteAllText("C:\\SDC\\release\\" + filename, formDesignXml, System.Text.Encoding.UTF8);


            }


        }


        [TestMethod]
        public void TestSerializer()
        {

            var ser = new XmlSerializer(typeof(FormDesignType));

            var fdd = new FormDesignDataSets();
            SDCTreeBuilderEcc stb;

            //stb = new SDCTreeBuilderEcc("204.1000043", fdd, "srtemplate.xslt");   //urethra Bx
            //stb = new SDCTreeBuilderEcc("189.1000043", fdd, "srtemplate.xslt");   //Breast Inv Bx
            //stb = new SDCTreeBuilderEcc("349.1000043", fdd, "srtemplate.xslt");  //vendor testing template
            //stb = new SDCTreeBuilderEcc("117.1000043", fdd, "srtemplate.xslt");   //Endometrium Inv Bx
            //stb = new SDCTreeBuilderEcc("359.1000043", fdd, "srtemplate.xslt");   //Staging
            //stb = new SDCTreeBuilderEcc("129.1000043", fdd, "srtemplate.xslt");   //Adrenal

            var keys = new string[] {
                "129.100004300"
                //"360.100004300", "211.100004300", "362.100004300",
                //"189.100004300", "129.100004300", "175.100004300",
                //"128.100004300", "190.100004300", "171.100004300",
                //"172.100004300", "202.100004300","180.100004300",
                //"373.100004300","354.100004300", "372.100004300",
                //"209.100004300", "161.100004300", "369.100004300",
                //"212.100004300","363.100004300","147.100004300",
                //"364.100004300", "153.100004300","365.100004300",
                //"164.100004300","188.100004300","366.100004300"
            };

            String formDesignXml;
            foreach (string key in keys)
            {

                stb = new SDCTreeBuilderEcc(key, fdd, "sdctemplate.xslt");   //Adrenal


                var filename = stb.FormDesign.filename;


                formDesignXml = stb.FormDesign.Serialize();
                string orig = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                string fix = orig + "\r\n" + "<?xml-stylesheet type=\"text/xsl\" href=\"sdctemplate.xslt\"?>";
                formDesignXml = formDesignXml.Replace(orig, fix);
                System.IO.File.WriteAllText("C:\\SDC\\" + filename, formDesignXml, System.Text.Encoding.UTF8);

                Debug.WriteLine(System.DateTime.Now);
                Debug.WriteLine(formDesignXml);

                //Console.Write(stb.FormDesign.Serialize());
            }
            return;

            //alternate approach
            //var ns = new XmlSerializerNamespaces();
            //ns.Add("", @"http://healthIT.gov/sdc");


            //var writer = new System.IO.StringWriter();
            //ser.Serialize(writer, stb.FormDesign, ns);
            //formDesignXml = writer.ToString();
            //Debug.Print(formDesignXml);


        }
        [TestMethod]
        public void DeserializeDemogFormDesignFromPath()
        {
            string path = "C:\\SDC\\Demog CCO Lung Surgery.xml";
            //string sdcFile = File.ReadAllText(path, System.Text.Encoding.UTF8);
            DemogFormDesignType FD = DemogFormDesignType.DeserializeFromXmlPath(path);
            var myXML = FD.GetXml(); 
            Debug.Print(myXML);

        }
        [TestMethod]
        public void DeserializePkgFromPath()
        {
            string path = "C:\\SDC\\..Sample SDCPackage.xml";
            //string sdcFile = File.ReadAllText(path, System.Text.Encoding.UTF8);
            var Pkg = RetrieveFormPackageType.DeserializeFromXmlPath(path);
            FormDesignType FD = (FormDesignType)Pkg.Nodes.Values.Where(n => n.GetType() == typeof(FormDesignType)).FirstOrDefault();


            var Q = (QuestionItemType)Pkg.Nodes.Values.Where(
                t => t.GetType() == typeof(QuestionItemType)).Where(
                q => ((QuestionItemType)q).ID == "37387.100004300").FirstOrDefault();
            var DI = Q.AddChildDisplayedItem("DDDDD");//should add to end of the <List>
            DI.name = "my added DI";

            DisplayedType DI1 = (DisplayedType)Pkg.Nodes.Values.Where(n => n.name == "my added DI").First();
            DisplayedType DI2 = (DisplayedType)Q.ChildItemsNode.Items[0];
            QuestionItemType Q1 = (QuestionItemType)DI2.ParentNode.ParentNode;
            string diName = Q.Item1.Items[0].name;
            string diName2 = Q.ChildItemsNode.ChildItemsList[0].ID;
            int i = Q.ChildItemsNode.ChildItemsList.Count();
            bool b1 = Q.ChildItemsNode.ShouldSerializeItems();

            var myXML = Pkg.GetXml();


            Debug.Print(myXML);

        }
        [TestMethod]
        public void DeserializeDEFromPath()
        {
            string path = "C:\\Users\\rmoldwi\\OneDrive\\One Drive Documents\\SDC\\SDC Git Repo\\sdc-schema-package\\DE sample.xml";
            //string sdcFile = File.ReadAllText(path, System.Text.Encoding.UTF8);
            DataElementType DE = DataElementType.DeserializeFromXmlPath(path);
            var myXML = DE.GetXml();
            Debug.Print(myXML);

        }
        [TestMethod]
        public void DeserializeFormDesignFromPath()
        {
            //string path = "C:\\SDC\\CCO Lung Surgery.xml";
            string path = "C:\\Users\\rmoldwi\\OneDrive\\Desktop\\SDCLocal\\Breast.Invasive.Staging.359_.CTP9_sdcFDF.xml";
            //string path = "C:\\SDC\\Adrenal.Bx.Res.129_3.004.001.REL_sdcFDF_test.xml";
            string sdcFile = File.ReadAllText(path, System.Text.Encoding.UTF8);
            
            var FD = FormDesignType.DeserializeFromXmlPath(path);
            //SDC.Schema.FormDesignType FD = SDC.Schema.FormDesignType.DeserializeSdcFromFile(sdcFile);
            string myXML;
            //myXML =  SdcSerializer<FormDesignType>.Serialize(FD);

            //Test adding and reading FD object model
                var Q = (QuestionItemType)FD.Nodes.Values.Where(
                    t => t.GetType() == typeof(QuestionItemType)).Where(
                    q => ((QuestionItemType)q).ID == "58218.100004300").FirstOrDefault();

                var DI = Q.AddChildDisplayedItem("DDDDD");//should add to end of the <List>
                DI.name = DI.ID; DI.title = DI.ID;

                var P = Q.AddProperty(); P.name = "PPPPP"; P.propName = "PPPPP";
                var S = Q.AddChildSection("SSSSS", 0); S.name = "SSSSS";
            //Q.Move(new SectionItemType(), -1); Q.AddComment(); Q.Remove();
            //var li = new ListItemType(Q.ListField_Item.List,"abc" ); var b = li.SelectIf.returnVal; var rv = li.OnSelect[0].returnVal;
            
                DisplayedType DI1 = (DisplayedType)FD.Nodes.Values.Where(n => n.name == DI.ID).First();
                DisplayedType DI2 = (DisplayedType)Q.ChildItemsNode.Items[0];
                QuestionItemType Q1 = (QuestionItemType)DI2.ParentNode.ParentNode;
            myXML = SDCHelpers.XmlReorder(FD.GetXml());
            myXML = SDCHelpers.XmlFormat(myXML);
            
            Debug.Print(myXML);
            FD.Clear();
            //var myMP = FD.GetMsgPack();
            //FD.SaveMsgPackToFile("C:\\MPfile");  //also support REST transactions, like sending packages to SDC endpoints; consider FHIR support
            //var myJson = FD.GetJson();
            //Debug.Print(myJson);
        }
        
    }

    public class AddChildDisplayedItem
    {
        
        public AddChildDisplayedItem()
        {
            
        }
    }

    public class MyClass
    {
        
        public MyClass()
        {
            
        }

        [TestMethod]
        public void Test()
        {
            
        }


    }
    [TestClass]
    public class Tests
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }



    }
    }
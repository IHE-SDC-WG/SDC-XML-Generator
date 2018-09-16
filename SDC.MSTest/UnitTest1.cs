using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using global::SDC;
using SDC.DAL.DataSets;
using System.Collections.Generic;
using System.Data;

namespace MSTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var serializer = new XmlSerializer(typeof(SDC.Schema.BaseType));
        }

        [TestMethod]
        public void SerializeAll()
        {    //rlm 2017/08/31
            Dictionary<String, String> templatesMap = new Dictionary<String, String>();
            string key = "", val = "";

            var dt = new SDC.DAL.DataSets.FormDesignDataSets();
            DataTable templateDT = dt.dtGetTemplateList(0);  //the decimal parameter is not used

            foreach (DataRow dr in templateDT.Rows)
            {

                key = (string)dr.ItemArray[0];
                val = (string)dr.ItemArray[1] + "_SDC.xml";
                
                templatesMap.Add(key, val);
                Debug.Print(templatesMap[key].ToString());

                var ser = new XmlSerializer(typeof(SDC.Schema.FormDesignType));
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

            var ser = new XmlSerializer(typeof(SDC.Schema.FormDesignType));

            var fdd = new FormDesignDataSets();
            SDCTreeBuilderEcc stb;

            //stb = new SDCTreeBuilderEcc("204.1000043", fdd, "srtemplate.xslt");   //urethra Bx
            stb = new SDCTreeBuilderEcc("189.1000043", fdd, "srtemplate.xslt");   //Breast Inv Bx
            //stb = new SDCTreeBuilderEcc("349.1000043", fdd, "srtemplate.xslt");  //vendor testing template
            //stb = new SDCTreeBuilderEcc("117.1000043", fdd, "srtemplate.xslt");   //Endometrium Inv Bx
            //stb = new SDCTreeBuilderEcc("359.1000043", fdd, "srtemplate.xslt");   //Staging


            var filename = stb.FormDesign.filename;  
            

            String formDesignXml = stb.FormDesign.Serialize();
            string orig = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            string fix = orig + "\r\n" + "<?xml-stylesheet type=\"text/xsl\" href=\"sdctemplate.xslt\"?>";
            formDesignXml = formDesignXml.Replace(orig, fix);
            System.IO.File.WriteAllText("C:\\SDC\\" + filename, formDesignXml, System.Text.Encoding.UTF8);

            Debug.WriteLine(System.DateTime.Now);
            Debug.WriteLine(formDesignXml);

            //Console.Write(stb.FormDesign.Serialize());
            return;
            //alternate approach
            var ns = new XmlSerializerNamespaces();
            ns.Add("", @"http://healthIT.gov/sdc");


            var writer = new System.IO.StringWriter();
            ser.Serialize(writer, stb.FormDesign, ns);
            formDesignXml = writer.ToString();
            Debug.Print(formDesignXml);

        }


    }
}
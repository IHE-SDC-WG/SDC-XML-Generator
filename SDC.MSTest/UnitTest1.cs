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
            DataTable templateDT = dt.dtGetTemplateList(key);  //the parameter is not used

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
                var ns = new XmlSerializerNamespaces();
                ns.Add("", @"http://healthIT.gov/sdc");


                var writer = new System.IO.StringWriter();
                ser.Serialize(writer, stb.FormDesign, ns);
                formDesignXml = writer.ToString();
                Debug.Print(formDesignXml);
            

        }

        [TestMethod]
        public void DeserializeSdcFromPath()
        {
            //string sdcOutput;
            string path = "C:\\SDC\\Adrenal.Bx.Res.129_3.003.001.REL_sdcFDF.xml";
            string sdcFile = File.ReadAllText(path, System.Text.Encoding.UTF8);
            SDC.Schema.FormDesignType FD = SDC.Schema.FormDesignType.DeserializeSdcFromPath(path);
            //SDC.Schema.FormDesignType FD = SDC.Schema.FormDesignType.DeserializeSdcFromFile(sdcFile);
        }
        [TestMethod]
        public void ReadSDCxml()
        {

            string sdcOutput;
            string path= "C:\\SDC\\Adrenal.Bx.Res.129_3.003.001.REL_sdcFDF.xml";
            string sdcFile = File.ReadAllText(path, System.Text.Encoding.UTF8);


            SDC.Schema.FormDesignType FD = SDC.Schema.FormDesignType.Deserialize(sdcFile);
            sdcOutput=FD.Serialize();
            Debug.Print(sdcOutput);

            //read as XMLDocument to walk tree
            var x = new System.Xml.XmlDocument();
            x.LoadXml(sdcFile);

            XmlNodeList nodeList = x.SelectNodes("//*");        
            int j=0;
            foreach (XmlNode n in nodeList)
            {                
                string order = n.Attributes?["order"]?.Value.ToString()??"(null)";
                string ID = n.Attributes?["ID"]?.Value.ToString()??"(null)";
                Debug.WriteLine(n.Name + ", order:" + order + ", ID:" + ID);
                
                XmlNode par = n.ParentNode;
                order = par.Attributes?["order"]?.Value.ToString() ?? "(null)";
                ID = par.Attributes?["ID"]?.Value.ToString() ?? "(null)";
                Debug.WriteLine("-->Parent:" + par?.Name + ", order:" + order + ", ID:" + ID);

                var m=nodeList[j];
                Debug.Print(nodeList[j].Name.ToString());
                j++;
            }

            int i;
            int nodeListCount;
            for (i = 0; i < nodeList.Count; i++)//make sure the first node is "FormDesign", and not an xml or stylesheet etc. node
            {
                XmlNode n = nodeList[i];
                if (n.Name?.ToString() == "FormDesign") break;
            }
            nodeListCount = nodeList.Count - i;

            //var dNodes = new Dictionary<System.Guid, XmlNode>(); //create Dict so we can look up a node by its position (order) in the node list.
            //var dNodesRev = new Dictionary<XmlNode, int>();
            //var dKtoGuidMap = new Dictionary<int, System.Guid>();
            //var dGuidToKmap = new Dictionary<System.Guid, int>();


            var dPars = new Dictionary<System.Guid, XmlNode>(); //create Dict so we can look up a parent node by the position (order) of its child.
            var dParsRev = new Dictionary<XmlNode, int>();

            int i2 = i;  //i is the first node index for the XML tree.  It is normally 0.  Not sure if non-element lines ever count as nodes in a nodeList.
            for (int k = 0; k < nodeListCount; k++)
            {
                SDC.Schema.BaseType bt = FD.Nodes[k];


                //dNodes[bt.ObjectGUID] = nodeList[i2];
                //dNodesRev[nodeList[i2]] = k;
                //dKtoGuidMap[k] = bt.ObjectGUID;
                //dGuidToKmap[bt.ObjectGUID] = k;

                //As we interate through the nodes, we will need code to skip over any non-element node (using i2), 
                //and still stay in sync with FD (using k). For now, we assume that every nodeList node is an element.
                //https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnodetype?view=netframework-4.8
                //https://docs.microsoft.com/en-us/dotnet/standard/data/xml/types-of-xml-nodes

                dPars[bt.ObjectGUID] = nodeList[i2].ParentNode;
                dParsRev[nodeList[i2].ParentNode] = k;
                i2++;
            }

            
            //foreach (SDC.Schema.BaseType bt in FD.Nodes.Values) 
            for (int i3=0; i3 < FD.Nodes.Count;i3++)
            {
                SDC.Schema.BaseType bt =FD.Nodes[i3];
                Debug.Print(bt.order.ToString());
                //XmlNode n = dNodes[bt.ObjectGUID];
               // XmlNode par = dPars[bt.ObjectGUID];


                //string testOrder = n.Attributes?["order"]?.Value.ToString() ?? null;
                //string testID = n.Attributes?["ID"]?.Value.ToString() ?? null;
                //string testName = n.Attributes?["name"]?.Value?.ToString() ?? null;
                //decimal.TryParse(testOrder, out decimal order);


                int parIndex = dParsRev[dPars[bt.ObjectGUID]];
                var btpar = FD.Nodes[parIndex];  //Find the parent node in FD
                
                //bt.ParentNode = btpar;  //Set btPar metadata here. This needs to be done inside the FD class or the base class; or else, ParentNode "set" must be public
                //Set bt.ParentIETypeObject by walking up the FormDesignType parent object tree until we find an IET type 

            }




        }
    }
    

}
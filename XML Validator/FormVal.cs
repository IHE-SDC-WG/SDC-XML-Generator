using System;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;


// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace XML_Validator
{
    public partial class FormVal : Form
    {
        public FormVal()
        {
            InitializeComponent();

            try
            {   //for Rich only
                var schema = ConfigurationManager.AppSettings["SchemaFile"];//Properties.Settings.Default.SchemaFile;
                var xml = ConfigurationManager.AppSettings["XML_File"];//Properties.Settings.Default.XML_file;

                txtSchema.Text = schema.Replace("\"", "");
                txtFile.Text = xml.Replace("\"", "");
            }
            catch (Exception ex) { txtValMsg.Text = ex.Message.ToString(); }//nothing to do here 
            
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {            
            //ref: https://stackoverflow.com/questions/751511/validating-an-xml-against-referenced-xsd-in-c-sharp 
            XmlSchemaSet schemas = new XmlSchemaSet();
            var doc = new XDocument();
            var err = false;

            try {
                schemas.Add("urn:ihe:qrph:sdc:2016", txtSchema.Text.Replace("\"",""));
                doc = XDocument.Load(txtFile.Text.Replace("\"", ""));
            } catch (Exception ex) {
                err = true;
                txtValMsg.Text = ex.Message.ToString();
            };

            string msg = "";

            if (!err) try {
                    doc.Validate(schemas, (o, args) => { msg += args.Message + Environment.NewLine; });
                    txtValMsg.Text = (msg == "" ? "Document is valid" : "Document invalid: \r\n\r\n" + msg);
            }
            catch (Exception ex)
            { txtValMsg.Text = ex.Message.ToString(); }



        }

        private void txtValMsg_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

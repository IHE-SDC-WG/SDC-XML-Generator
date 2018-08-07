using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDC;
using System.Data;




namespace SDC.Helper
{
    public partial class FormDesign
    {
        DataRow _dr;
        public DataRow Dr
        {
            get { return _dr; }
            set { _dr = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDesign"/> class.
        /// </summary>
        public FormDesign()
        {
            //initialize Header
            //Boilerplate
            //Body
            //OtherText List
            //Links List
            //Images List
            //Codes (Version metadata) List
            //Init Body
            //OtherText List
            //Links List
            //Images List
            //Codes List
            //ChildItems List
            //Footer
            FD = new FormDesignType();

        }

        FormDesignType FD;



        private void AddHeader()
        {
            if (FD.Header == null)
            {
                FD.Header = new SDC.SectionItemType();
            }
        }
        private void AddBody()
        {
            if (FD.Body == null)
            {
                FD.Body = new SDC.SectionItemType();
            }
        }
        private void AddFooter()
        {
            if (FD.Footer == null)
            {
                FD.Footer = new SDC.SectionItemType();
            }
        }

        private void AddRules()
        {
            if (FD.Rules == null)
            {
                FD.Rules = new SDC.RulesType();
            }
        }
        private void AddChildItems()
        {

        }

        private void AddDisplayedType(DisplayedType NewDT, DisplayedType TargetDT)
        {
            if (FD.Footer == null)
            {
                FD.Footer = new SDC.SectionItemType();
            }
        }



        //OtherText List
        //Links List
        //Images List
        //Codes List
        //ChildItems List


    }
}

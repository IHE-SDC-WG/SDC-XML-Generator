using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDC;
using System.Data;
using System.Diagnostics;
//using global::SDC.DAL.DataModel;

namespace NUnit.SDC.SchemaTests
{
    [TestFixture]
    class DataTypeTests
    {




        [Test]
        public void Integer_DEtype()
        {
            var i = new integer_DEtype(null);

            //Assert.Pass("Should Serialize, val= not set: " + i.ShouldSerializeval());
            i.val = "1";
            //bool b = posInt.ShouldSerializeval();

            Assert.Pass("Should Serialize, val=1: " + i.ShouldSerializeval());
        }
                        
        [Test]
        public void positiveInteger_DEtype()
        {
            var posInt = new positiveInteger_DEtype(null);
            posInt.val = "1";
            bool b = posInt.ShouldSerializeval();

            Assert.Pass("Should Serialize: " + b.ToString());
        }
        [Test]
        public void yearMonthDuration_DEtype()
        {
            var ym = new yearMonthDuration_DEtype(null);
            ym.val = TimeSpan.Parse("12:12:1.1").ToString();
            bool b = ym.ShouldSerializeval();
            System.Diagnostics.Debug.WriteLine(ym.ToString());
            Assert.Pass("Should Serialize: " + b.ToString());
        }
        [Test]
        public void duration_DEtype()
        {
            var dur = new duration_DEtype();
            dur.val = TimeSpan.Parse("12:12:1.1").ToString();
            bool b = dur.ShouldSerializeval();
            Debug.WriteLine(dur.ToString());//writes to immediate window
            Trace.Write(dur.val.ToString());//writes to immediate window
            Assert.Pass("Should Serialize: " + b.ToString());
        }

    }
}

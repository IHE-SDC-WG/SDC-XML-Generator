using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDC.Schema.Adaptor
{
    class Class1
    {
        public readonly Class2 c2 = new Class2();


        public class Class2
        {
            internal Class2()
            { }


        }
    }
}

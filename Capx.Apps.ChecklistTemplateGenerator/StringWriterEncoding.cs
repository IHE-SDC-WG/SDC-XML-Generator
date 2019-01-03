using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Capx.Apps.ChecklistTemplateGenerator
{
    /// <summary>
    /// StringWriter class with specified encoding
    /// </summary>
    public class StringWriterEncoding : StringWriter
    {
        private Encoding _encoding;

        public StringWriterEncoding (Encoding encoding)
        {
            _encoding = encoding;
        }

        public override Encoding Encoding
        { 
            get
            {
                return _encoding;
            } 
        }
    }
}

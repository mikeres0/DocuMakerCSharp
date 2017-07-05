using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuMakerCSharp.Entities
{
    class DocuFunc
    {
        public string functionName { get; set; }
        public string summary { get; set; }
        public List<string> @params { get; set; }
        public string returns { get; set; }
    }
}

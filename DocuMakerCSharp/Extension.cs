using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuMakerCSharp
{
    static class Extension
    {
        public static string CleanString(this string original)
        {
            return original = original.Replace("/// ", "").Trim();
        }

        public static string CleanFunctionName(this string original)
        {
            List<string> excludes = new List<string>();

            return original = original.Replace("/// ", "").Trim();
        }
    }
}

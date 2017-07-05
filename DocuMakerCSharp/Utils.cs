using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuMakerCSharp
{
    class Utils
    {
        public static string GetLocalPath(string folder = "")
        {
            string dataFolder = "";
            switch (System.Net.Dns.GetHostName().ToUpper())
            {
                case "DESKTOP-022ID8J":
                    dataFolder = @"C:\Users\MikeR\OneDrive\Documents\moss\" + folder;
                    //dataFolder = @"C:\Users\MikeR\OneDrive\Documents\moss\Cortana\Cortana.Core" + folder;
                    return dataFolder;
                default:
                    return null;
            }
        }
    }
}

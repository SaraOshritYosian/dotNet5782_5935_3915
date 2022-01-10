using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace DalApi
{
    class DalConfig
    {
        //C:\Users\chanacom\source\repos\SaraOshritYosian\dotNet5782_5935_3915\xml\dal-config.xml

        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"C:\Users\chanacom\source\repos\SaraOshritYosian\dotNet5782_5935_3915\xml\dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg
                          ).ToDictionary(p => "" + p.Name, p => p.Value);
        }
    }
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }


/// <summary>
/// Represents errors during DalApi initialization
/// </summary>
[Serializable]
    public class DLConfigException : Exception
    {
        public DLConfigException(string message) : base(message) { }
        public DLConfigException(string message, Exception inner) : base(message, inner) { }
    }
}
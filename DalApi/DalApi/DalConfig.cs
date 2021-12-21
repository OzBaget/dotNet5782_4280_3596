using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace DalApi
{
    class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        internal static Dictionary<string, string> DalClasses;
        internal static Dictionary<string, string> DalNamespaces;
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"xml\dal-config.xml");
            
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg
                          ).ToDictionary(p => "" + p.Name, p => p.Value);
            DalClasses= (from pkg in dalConfig.Element("dal-packages").Elements()
                         select pkg
                          ).ToDictionary(p => "" + p.Name, p => p.Attribute("class").Value);
            DalNamespaces= (from pkg in dalConfig.Element("dal-packages").Elements()
                         select pkg
                          ).ToDictionary(p => "" + p.Name, p => p.Attribute("namespace").Value);
        }
    }

    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
}

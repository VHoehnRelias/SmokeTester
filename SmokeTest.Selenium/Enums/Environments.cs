using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cmt.Online.Web.TestUi.Selenium.Enums
{
    public static class Environments
    {
        public static List<Environment> TheList()
        {
            var lst = new List<Environment>();//Login.aspx
            lst.Add(new Environment(type: EnvironmentType.Local, server: "MorVmSqlQa01\\Qa01", database: "Missouri_OLTP", clientCode: "Missouri", userId: "vhoehn@cmthealthcare.com", password: "VHoehn#1", webSite: "http://localhost:32917/"));
            //lst.Add(new Environment(type: EnvironmentType.Dev01, server: "morimp01\\imp01", database: "Missouri_OLTP", clientCode: "Missouri", userId: "VHoehn@cmthealthcare.com", password: "VHoehn", webSite: "https://morvmdev01/"));
            lst.Add(new Environment(type: EnvironmentType.Dev01, server: "MorVmSqlQa01\\Qa02", database:"Missouri_OLTP", clientCode: "Missouri", userId: "VHoehn@cmthealthcare.com", password: "VHoehn#1", webSite: "http://localhost:52677/"));//, webSite: "http://localhost:52677/"));
            lst.Add(new Environment(type: EnvironmentType.Qa01, server: "MorVmSqlQa01\\Qa01", database: "CMTDemo_OLTP", clientCode: "CMTDemo", userId: "VHoehn@cmthealthcare.com", password: "VHoehn#1", webSite: "https://morvmwebqa01:445/" ));//, webSite: "http://localhost:52677/"));//
            lst.Add(new Environment(type: EnvironmentType.Qa02, server: "morvmsqlqa01\\qa02", database:"Missouri_OLTP", clientCode: "Missouri", userId: "VHoehn@cmthealthcare.com", password: "VHoehn#1", webSite: "https://morvmwebqa01:445/" ));
            lst.Add(new Environment(type: EnvironmentType.UatCmt, server: "CMTSQLSVR03\\OL02", database: "CMTDemo_OLTP", clientCode: "CMTDemo", userId: "vhoehn@cmthealthcare.com", password: "VHoehn", webSite: "https://uat.cmtanalytics.com" ));
            lst.Add(new Environment(type: EnvironmentType.UatMissouri, server: "CMTSQLSVR03\\OL02", database: "Missouri_OLTP", clientCode: "Missouri", userId: "vhoehn@cmthealthcare.com", password: "VHoehn", webSite: "https://uat.cmtanalytics.com"));
            //lst.Add(new Environment(type: EnvironmentType.Production, server: "CMTSQLSVR03\\OL01", database: "Missouri_OLTP", clientCode: "Missouri", userId: "VHoehn@cmthealthcare.com", password: "CMT.VH03hn", webSite: "https://www.cmtanalytics.com/"));
            lst.Add(new Environment(type: EnvironmentType.Production, server: "CMTSQLSVR03\\OL01", database: "CMTDemo_OLTP", clientCode: "CMTDemo", userId: "VHoehn@cmthealthcare.com", password: "VHoehn#1", webSite: "https://www.cmtanalytics.com/"));
            lst.Add(new Environment(type: EnvironmentType.Implementations, server: "CMTSQLSVR02\\Imp02", database:"Missouri_OLTP", clientCode: "Missouri", userId: "VHoehn@cmthealthcare.com", password: "VHoehn", webSite: "" ));
            lst.Add(new Environment(type: EnvironmentType.AzureDev01, server: "cmtazsqlsvr01.cloudapp.net,57500\\dev01", database:"Missouri_OLTP", clientCode: "Missouri", userId: "VHoehn@cmthealthcare.com", password: "VHoehn", webSite: "" ));
            return lst;
        }

        public static Environment GetEnvironment(EnvironmentType et)
        {
            Environment env = TheList().Find(e => e.Type == et);
            return env;
        }
    }
}

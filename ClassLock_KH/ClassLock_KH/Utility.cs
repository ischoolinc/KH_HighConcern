using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ClassLock_KH
{
    public class Utility
    {
        /// <summary>
        /// 傳送資料到局端
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="Action"></param>
        /// <returns></returns>
        public static string SendData(string ClassName,string GradeYear,string Reason, string Action)
        {
            string errMsg = "";
            try
            {
                string DSNS = FISCA.Authentication.DSAServices.AccessPoint;
                string ServiceName = "_.InsertLog";
                XElement reqXML = new XElement("Request");
                XElement reqContent = new XElement("Content");
                reqContent.SetElementValue("ClassName", ClassName);
                reqContent.SetElementValue("GradeYear", GradeYear);
                reqContent.SetElementValue("Reason", Reason);
                reqXML.SetElementValue("DSNS", DSNS);
                reqXML.SetElementValue("Action", Action);
                reqXML.Add(reqContent);
                // call service
                string reqStr = reqXML.ToString();
            }
            catch (Exception ex) { errMsg = ex.Message; }
            return errMsg;
        }
    }
}

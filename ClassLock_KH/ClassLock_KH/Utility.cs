using FISCA.DSAClient;
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
        public static string SendData(string ClassName, string GradeYear, string Reason, string Action, string ScheduleClassDate)
        {
            string DSNS = FISCA.Authentication.DSAServices.AccessPoint;
            string AccessPoint = @"j.kh.edu.tw";

            if (FISCA.RTContext.IsDiagMode)
            {
                string accPoint = FISCA.RTContext.GetConstant("KH_AccessPoint");
                AccessPoint = accPoint;
            }

            string Contract = "log";
            string ServiceName = "_.InsertLog";

            string errMsg = "";
            try
            {                
                XElement xmlRoot = new XElement("Request");
                XElement s1 = new XElement("SchoolLog");
                XElement s2 = new XElement("Field");
                s2.SetElementValue("DSNS", DSNS);
                s2.SetElementValue("Action", Action);
                XElement s3 = new XElement("Content");
                s3.SetElementValue("ClassName", ClassName);
                s3.SetElementValue("GradeYear", GradeYear);
                s3.SetElementValue("Reason", Reason);
                s3.SetElementValue("ScheduleClassDate", ScheduleClassDate);
                s2.Add(s3);
                s1.Add(s2);
                xmlRoot.Add(s1);
                
                XmlHelper reqXML = new XmlHelper(xmlRoot.ToString());                
                FISCA.DSAClient.Connection cn = new FISCA.DSAClient.Connection();
                cn.Connect(AccessPoint, Contract, DSNS, DSNS);
                Envelope rsp = cn.SendRequest(ServiceName, new Envelope(reqXML));
                XElement rspXML = XElement.Parse(rsp.XmlString);

            }
            catch (Exception ex) { 
                
                errMsg = ex.Message; }
            return errMsg;
        }

        /// <summary>
        /// 查詢傳送記錄
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static XElement QuerySendData(DateTime beginDate, DateTime endDate)
        {
            XElement elmMsg = null;

            string DSNS = FISCA.Authentication.DSAServices.AccessPoint;

            string AccessPoint = @"j.kh.edu.tw";

            if (FISCA.RTContext.IsDiagMode)
            {
                string accPoint = FISCA.RTContext.GetConstant("KH_AccessPoint");
                AccessPoint = accPoint;
            }

            string Contract = "log";
            string ServiceName = "_.QueryLog";

            try
            {

                XElement xmlRoot = new XElement("Request");
                XElement s1 = new XElement("Field");
                s1.SetElementValue("All", "");
                XElement s2 = new XElement("Condition");
                s2.SetElementValue("Dsns", DSNS);
                s2.SetElementValue("StartDate", beginDate.ToShortDateString());
                s2.SetElementValue("EndDate", endDate.ToShortDateString());
                xmlRoot.Add(s1);
                xmlRoot.Add(s2);

                XmlHelper reqXML = new XmlHelper(xmlRoot.ToString());
                FISCA.DSAClient.Connection cn = new FISCA.DSAClient.Connection();
                cn.Connect(AccessPoint, Contract, DSNS, DSNS);
                Envelope rsp = cn.SendRequest(ServiceName, new Envelope(reqXML));
                elmMsg = XElement.Parse(rsp.XmlString);
            }
            catch (Exception ex)
            {
                elmMsg = new XElement("Error");
                elmMsg.SetElementValue("Message", ex.Message);
            }

            return elmMsg;
        }

    }
}

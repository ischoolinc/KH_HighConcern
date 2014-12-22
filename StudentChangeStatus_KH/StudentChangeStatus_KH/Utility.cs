using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FISCA.DSAClient;

namespace StudentChangeStatus_KH
{
    public class Utility
    {
        /// <summary>
        /// 傳送資料到局端
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="Action"></param>
        /// <returns></returns>
        public static string SendData(string Action,string ClassName,string StudentName,string StudentNumber,string IDNumber, string StudentStatus, string NewStudentStatus,string Msg)
        {
            string DSNS = FISCA.Authentication.DSAServices.AccessPoint;
            string AccessPoint = @"j.kh.edu.tw";

            if (FISCA.RTContext.IsDiagMode)
            {
                string accPoint = FISCA.RTContext.GetConstant("KH_AccessPoint");
                if (!string.IsNullOrEmpty(accPoint))
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
                s3.SetElementValue("StudentName", StudentName);
                s3.SetElementValue("StudentNumber", StudentNumber);
                s3.SetElementValue("IDNumber", IDNumber);
                s3.SetElementValue("StudentStatus", StudentStatus);
                s3.SetElementValue("NewStudentStatus", NewStudentStatus);
                s3.SetElementValue("Reason", Msg); // 備註
                s2.Add(s3);
                s1.Add(s2);
                xmlRoot.Add(s1);

                XmlHelper reqXML = new XmlHelper(xmlRoot.ToString());
                FISCA.DSAClient.Connection cn = new FISCA.DSAClient.Connection();
                cn.Connect(AccessPoint, Contract, DSNS, DSNS);
                Envelope rsp = cn.SendRequest(ServiceName, new Envelope(reqXML));
                XElement rspXML = XElement.Parse(rsp.XmlString);

            }
            catch (Exception ex)
            {

                errMsg = ex.Message;
            }
            return errMsg;
        }

    }
}

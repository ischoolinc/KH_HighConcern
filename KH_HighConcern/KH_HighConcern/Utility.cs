using FISCA.DSAClient;
using KH_HighConcern.ImportExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace KH_HighConcern
{
    public class Utility
    {
        public static string SendData(string action, string IDNumber, string StudentNumber, string StudentName, string ClassName, string SeatNo, string DocNo, string NumberReduce)
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
                s2.SetElementValue("Action", action);
                XElement Content = new XElement("Content");
                Content.SetElementValue("IDNumber", IDNumber);
                Content.SetElementValue("StudentNumber", StudentNumber);
                Content.SetElementValue("StudentName", StudentName);
                Content.SetElementValue("ClassName", ClassName);
                Content.SetElementValue("SeatNo", SeatNo);
                Content.SetElementValue("NumberReduce", NumberReduce);
                Content.SetElementValue("DocNo", DocNo);
                s2.Add(Content);
                s1.Add(s2);
                xmlRoot.Add(s1);
                XmlHelper reqXML = new XmlHelper(xmlRoot.ToString());
                FISCA.DSAClient.Connection cn = new FISCA.DSAClient.Connection();
                cn.Connect(AccessPoint, Contract, DSNS, DSNS);
                Envelope rsp = cn.SendRequest(ServiceName, new Envelope(reqXML));
                XElement rspXML = XElement.Parse(rsp.XmlString);
            }
            catch (Exception ex) { errMsg = ex.Message; }

            return errMsg;
        }

        public static string SendDataList(string action,List<logStud> logStudList)
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
                s2.SetElementValue("Action", action);
                XElement Content = new XElement("Content");
                string summaryTxt=action+" 共 "+logStudList.Count+" 筆";
                Content.SetElementValue("Summary", summaryTxt);

                XElement Detail = new XElement("Detail");
                foreach(logStud ls in logStudList)
                {
                    XElement StudentXML = new XElement("Student");
                    StudentXML.SetElementValue("IDNumber", ls.IDNumber);
                    StudentXML.SetElementValue("StudentNumber", ls.StudentNumber);
                    StudentXML.SetElementValue("StudentName", ls.StudentName);
                    StudentXML.SetElementValue("ClassName", ls.ClassName);
                    StudentXML.SetElementValue("SeatNo", ls.SeatNo);
                    StudentXML.SetElementValue("NumberReduce", ls.NumberReduce);
                    StudentXML.SetElementValue("DocNo", ls.DocNo);
                    Detail.Add(StudentXML);
                }
                               
                s2.Add(Content);
                s2.Add(Detail);
                s1.Add(s2);
                xmlRoot.Add(s1);
                XmlHelper reqXML = new XmlHelper(xmlRoot.ToString());
                FISCA.DSAClient.Connection cn = new FISCA.DSAClient.Connection();
                cn.Connect(AccessPoint, Contract, DSNS, DSNS);
                Envelope rsp = cn.SendRequest(ServiceName, new Envelope(reqXML));
                XElement rspXML = XElement.Parse(rsp.XmlString);
            }
            catch (Exception ex) { errMsg = ex.Message; }

            return errMsg;
        }
    }
}

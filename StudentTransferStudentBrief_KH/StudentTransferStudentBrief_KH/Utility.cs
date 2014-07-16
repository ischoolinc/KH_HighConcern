using FISCA.Data;
using FISCA.DSAClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StudentTransferStudentBrief_KH
{
    public class Utility
    {
        /// <summary>
        /// 取得符合班級名稱
        /// </summary>
        /// <param name="GradeYear"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetClassNameFirst(string GradeYear)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();

            QueryHelper qh = new QueryHelper();
            string query = @"select class.class_name,count(student.id) as studCot,class.id from class inner join student on class.id=student.ref_class_id  where student.status=1 and (class_name not in(select class_name from $kh.automatic.class.lock) and class_name not in(select distinct class_name from $kh.automatic.placement.high.concern)) and class.grade_year=" + GradeYear + " group by class.class_name,class.id order by count(student.id),class.class_name,class.id limit 1";
            DataTable dt = qh.Select(query);
            foreach (DataRow dr in dt.Rows)
            {
                retVal.Add(dr["class_name"].ToString(), dr["id"].ToString()); ;
                break;
            }
            return retVal;
        }

        /// <summary>
        /// 取得班級年級(有學生且狀態為一般)
        /// </summary>
        /// <returns></returns>
        public static List<string> GetGradeYearList()
        {
            List<string> retVal = new List<string>();
            QueryHelper qh = new QueryHelper();
            string query = @"select distinct class.grade_year from class inner join student on class.id=student.ref_class_id where student.status=1 order by class.grade_year";
            DataTable dt = qh.Select(query);
            foreach (DataRow dr in dt.Rows)
                retVal.Add(dr[0].ToString());

            return retVal;
        }

        /// <summary>
        /// 傳送至局端
        /// </summary>
        /// <param name="action"></param>
        /// <param name="IDNumber"></param>
        /// <param name="StudentNumber"></param>
        /// <param name="StudentName"></param>
        /// <param name="GradeYear"></param>
        /// <param name="ClassName"></param>
        /// <param name="SeatNo"></param>
        /// <param name="NewClassName"></param>
        /// <param name="ScheduleClassDate"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public static string SendData(string action, string IDNumber, string StudentNumber, string StudentName, string GradeYear, string ClassName, string SeatNo, string NewClassName, string ScheduleClassDate, string Reason)
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
                s2.SetElementValue("Action", action);
                XElement Content = new XElement("Content");
                Content.SetElementValue("IDNumber", IDNumber);
                Content.SetElementValue("StudentNumber", StudentNumber);
                Content.SetElementValue("ClassName", ClassName);
                Content.SetElementValue("NewClassName", NewClassName);
                Content.SetElementValue("SeatNo", SeatNo);
                Content.SetElementValue("ScheduleClassDate", ScheduleClassDate);
                Content.SetElementValue("StudentName", StudentName);
                Content.SetElementValue("Reason", Reason);
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
    }
}

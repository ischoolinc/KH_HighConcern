using FISCA.Data;
using FISCA.DSAClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StudentTransStudBase_KH
{
    public class Utility
    {
        /// <summary>
        /// 取得符合ID,班級名稱
        /// </summary>
        /// <param name="GradeYear"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetClassNameIDDict(string GradeYear)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();

                QueryHelper qh = new QueryHelper();
                string query = @"select distinct class.class_name,class.id as classID from class inner join student on class.id=student.ref_class_id  where class.grade_year="+GradeYear+" and student.status=1 and (class_name not in(select class_name from $kh.automatic.class.lock) and class_name not in(select distinct class_name from $kh.automatic.placement.high.concern)) order by class.class_name";
                DataTable dt = qh.Select(query);
                foreach (DataRow dr in dt.Rows)
                {
                    retVal.Add(dr["classID"].ToString(), dr["class_name"].ToString());
                }

            return retVal;
        }

        /// <summary>
        /// 取得符合班級名稱
        /// </summary>
        /// <param name="GradeYear"></param>
        /// <returns></returns>
        public static string GetClassNameFirst(string GradeYear)
        {
            string retVal = "";

            /*
1. 排除鎖定班級
2. 編班人數少的班級優先(編班人數=班級人數+優先關懷人數)
3. 若編班人數相同，則無特殊生（高關懷學生）班級優先
4-1. 若皆無特殊生，則班號小者優先
4-2. 若有特殊生，則以實際人數少優先
4-2-1. 若有特殊生且實際人數又相同，則以班號小者優先
             */

            List<KH_HighConcernCalc.ClassStudent> ClassStudentList = KH_HighConcernCalc.Calc.GetClassStudentList(GradeYear);

            if (ClassStudentList.Count > 0)
                retVal = ClassStudentList[0].ClassName;           


            //QueryHelper qh = new QueryHelper();
            //string query = @"select class.class_name,count(student.id) as studCot from class inner join student on class.id=student.ref_class_id  where student.status=1 and (class_name not in(select class_name from $kh.automatic.class.lock) and class_name not in(select distinct class_name from $kh.automatic.placement.high.concern)) and class.grade_year=" + GradeYear + " group by class.class_name order by count(student.id),class.class_name";
            //DataTable dt = qh.Select(query);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    retVal= dr["class_name"].ToString();
            //    break;
            //}
            return retVal;
        }

        /// <summary>
        /// 取得班級可使用座號
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public static List<int> GetClassSeatNoList(string ClassName)
        {
            List<int> retVal = new List<int>();
            if (!string.IsNullOrEmpty(ClassName))
            {
                QueryHelper qh = new QueryHelper();
                List<int> intVal = new List<int>();
                string query = @"select seat_no from student inner join class on student.ref_class_id=class.id where student.seat_no is not null and class.class_name='" + ClassName + "' order by seat_no";
                DataTable dt = qh.Select(query);
                foreach (DataRow dr in dt.Rows)
                    intVal.Add(int.Parse(dr["seat_no"].ToString()));

                // 取得最後一號+1
                if (intVal.Count == 0)
                {
                    retVal.Add(1);
                }
                else
                {
                    int lastInt = intVal[intVal.Count - 1] + 1;

                    for (int i = 1; i <= lastInt; i++)
                    {
                        if (!intVal.Contains(i))
                            retVal.Add(i);
                    }
                }
            }
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


        /// <summary>
        /// 取得班級年級(有學生且狀態為一般)
        /// </summary>
        /// <returns></returns>
        public static List<string> GetGradeYearList()
        {
            List<string> retVal = new List<string>();
            QueryHelper qh = new QueryHelper();
            string query = @"select distinct class.grade_year from class inner join student on class.id=student.ref_class_id where student.status=1 and class.grade_year is not null  order by class.grade_year";
            DataTable dt = qh.Select(query);
            foreach (DataRow dr in dt.Rows)
                retVal.Add(dr[0].ToString());

            return retVal;
        }


        /// <summary>
        /// 透過年級取得學號
        /// </summary>
        /// <param name="grYear"></param>
        /// <returns></returns>
        public  static string GetStudentNumber(string grYear)
        {
            string retVal = "學號有非數字無法取得";
            if (!string.IsNullOrWhiteSpace(grYear))
            {
                QueryHelper qh = new QueryHelper();
                string strSQL = "select student_number from student inner join class on student.ref_class_id=class.id where student.status=1 and class.grade_year=" + grYear + " and student_number<>'' order by student_number desc limit 1;";
                DataTable dt = qh.Select(strSQL);
                foreach (DataRow dr in dt.Rows)
                {
                    int no;
                    if (int.TryParse(dr[0].ToString(), out no))
                        retVal = (no + 1).ToString();
                }
            }
            return retVal;
        }
    }
}

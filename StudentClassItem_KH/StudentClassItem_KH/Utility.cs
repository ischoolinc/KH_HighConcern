using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Data;
using System.Xml.Linq;

namespace StudentClassItem_KH
{
    public class Utility
    {
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
        public static string SendData(string action,string IDNumber, string StudentNumber, string StudentName, string GradeYear, string ClassName, string SeatNo, string NewClassName, string ScheduleClassDate, string Reason)
        {
            string DSNS = FISCA.Authentication.DSAServices.AccessPoint;
            string errMsg = "";
            try {
                XElement XmlReq = new XElement("Request");
                XmlReq.SetElementValue("DSNS", DSNS);
                XmlReq.SetElementValue("Action", action);
                XElement Content = new XElement("Content");
                Content.SetElementValue("IDNumber", IDNumber);
                Content.SetElementValue("StudentNumber", StudentNumber);
                Content.SetElementValue("ClassName", ClassName);
                Content.SetElementValue("NewClassName", NewClassName);
                Content.SetElementValue("SeatNo", SeatNo);
                Content.SetElementValue("ScheduleClassDate", ScheduleClassDate);
                Content.SetElementValue("Reason", Reason);

                XmlReq.Add(Content);
                string strReq = XmlReq.ToString();
            }
            catch (Exception ex) { errMsg = ex.Message; }

            return errMsg;
        }

        /// <summary>
        /// 取得該年級符合班級名稱
        /// </summary>
        /// <param name="GradeYear"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetClassNameDictByGradeYear(string GradeYear)
        {
            Dictionary<string, int> retVal = new Dictionary<string, int>();

            if (!string.IsNullOrEmpty(GradeYear))
            {
                QueryHelper qh = new QueryHelper();
                string query = @"select class.class_name,count(student.id) as studCot from class inner join student on class.id=student.ref_class_id  where (class_name not in(select class_name from $kh.automatic.class.lock) and class_name not in(select distinct class_name from $kh.automatic.placement.high.concern)) and class.grade_year=" + GradeYear + " group by class.class_name order by count(student.id),class.class_name";
                DataTable dt = qh.Select(query);
                foreach (DataRow dr in dt.Rows)
                {
                    string className = dr["class_name"].ToString();
                    int cot = int.Parse(dr["studCot"].ToString());

                    retVal.Add(className, cot);
                }
            }
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
                string query = @"select seat_no from student inner join class on student.ref_class_id=class.id where class.class_name='" + ClassName + "' order by seat_no";
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
    }
}

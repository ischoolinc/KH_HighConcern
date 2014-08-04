using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Data;
using System.Xml.Linq;
using FISCA.DSAClient;
using Aspose.Cells;
using System.IO;
using System.Windows.Forms;
using FISCA.Presentation.Controls;

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
        public static string SendData(string action, string IDNumber, string StudentNumber, string StudentName, string GradeYear, string ClassName, string SeatNo, string NewClassName, string ScheduleClassDate, string Reason, string FirstPriorityClassName)
        {
            string DSNS = FISCA.Authentication.DSAServices.AccessPoint;

            string AccessPoint = @"j.kh.edu.tw";

            if (FISCA.RTContext.IsDiagMode)
            {
                string accPoint = FISCA.RTContext.GetConstant("KH_AccessPoint");
                if(!string.IsNullOrEmpty(accPoint))
                    AccessPoint = accPoint;            
            }
            
            string Contract = "log";
            string ServiceName = "_.InsertLog";

            string errMsg = "";
            try {

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
                Content.SetElementValue("NewClassName", NewClassName);
                Content.SetElementValue("SeatNo", SeatNo);
                Content.SetElementValue("ScheduleClassDate", ScheduleClassDate);
                Content.SetElementValue("Reason", Reason);
                Content.SetElementValue("FirstPriorityClassName", FirstPriorityClassName);
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
        /// 取得該年級符合班級名稱,人數
        /// </summary>
        /// <param name="GradeYear"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetClassNameDictByGradeYear(string GradeYear)
        {
            Dictionary<string, int> retVal = new Dictionary<string, int>();

            if (!string.IsNullOrEmpty(GradeYear))
            {
                //QueryHelper qh = new QueryHelper();
                //string query = @"select class.class_name,count(student.id) as studCot from class inner join student on class.id=student.ref_class_id  where student.status=1 and (class_name not in(select class_name from $kh.automatic.class.lock) and class_name not in(select distinct class_name from $kh.automatic.placement.high.concern)) and class.grade_year=" + GradeYear + " group by class.class_name order by count(student.id),class.class_name";
                //DataTable dt = qh.Select(query);
                //foreach (DataRow dr in dt.Rows)
                //{
                //    string className = dr["class_name"].ToString();
                //    int cot = int.Parse(dr["studCot"].ToString());

                //    retVal.Add(className, cot);
                //}

                List<KH_HighConcernCalc.ClassStudent> ClassStudentList = KH_HighConcernCalc.Calc.GetClassStudentList(GradeYear);

                foreach (KH_HighConcernCalc.ClassStudent cs in ClassStudentList)
                {
                    retVal.Add(cs.ClassName, cs.ClassStudentCount);
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
                string query = @"select seat_no from student inner join class on student.ref_class_id=class.id where student.status=1 and student.seat_no is not null and class.class_name='" + ClassName + "' order by seat_no";
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
        /// 取得該年級符合班級名稱,ID
        /// </summary>
        /// <param name="GradeYear"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetClassNameIDDictByGradeYear(string GradeYear)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(GradeYear))
            {
                //QueryHelper qh = new QueryHelper();
                //string query = @"select distinct class.class_name,class.id as classID from class inner join student on class.id=student.ref_class_id  where (class_name not in(select class_name from $kh.automatic.class.lock) and class_name not in(select distinct class_name from $kh.automatic.placement.high.concern)) and class.grade_year=" + GradeYear + " order by class.class_name";
                //DataTable dt = qh.Select(query);
                //foreach (DataRow dr in dt.Rows)
                //{
                //    retVal.Add(dr["class_name"].ToString(), dr["classID"].ToString());
                //}

                List<KH_HighConcernCalc.ClassStudent> ClassStudentList = KH_HighConcernCalc.Calc.GetClassStudentList(GradeYear);

                foreach (KH_HighConcernCalc.ClassStudent cs in ClassStudentList)
                {
                    retVal.Add(cs.ClassName, cs.ClassID);
                }
            }
            return retVal;
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
                if (!string.IsNullOrEmpty(accPoint))
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
                s2.SetElementValue("StartDate", string.Format("{0:yyyy-MM-dd}", beginDate));
                s2.SetElementValue("EndDate", string.Format("{0:yyyy-MM-dd}", endDate.AddDays(1)));
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

        /// <summary>
        /// 取得班級年級(有學生且狀態為一般)
        /// </summary>
        /// <returns></returns>
        public static List<string> GetGradeYearList()
        {
            List<string> retVal = new List<string>();
            QueryHelper qh = new QueryHelper();
            string query = @"select distinct class.grade_year from class inner join student on class.id=student.ref_class_id where student.status=1 and class.grade_year is not null order by class.grade_year";
            DataTable dt = qh.Select(query);
            foreach (DataRow dr in dt.Rows)
                retVal.Add(dr[0].ToString());

            return retVal;
        }

                /// <summary>
        /// 匯出 Excel
        /// </summary>
        /// <param name="inputReportName"></param>
        /// <param name="inputXls"></param>
        public static void CompletedXls(string inputReportName, Workbook inputXls)
        {
            string reportName = inputReportName;

            string path = Path.Combine(Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".xls");

            Workbook wb = inputXls;

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                wb.Save(path, Aspose.Cells.FileFormatType.Excel97To2003);
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".xls";
                sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, Aspose.Cells.FileFormatType.Excel97To2003);

                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
  
    }
}

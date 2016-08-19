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
        public static string SendData(string action, string IDNumber, string StudentNumber, string StudentName, string GradeYear, string ClassName, string SeatNo, string NewClassName, string ScheduleClassDate, string Reason, string FirstPriorityClassName, string EDoc, string SecondPriorityClassName)
        {
            string DSNS = FISCA.Authentication.DSAServices.AccessPoint;
            string AccessPoint = @"j.kh.edu.tw";                    
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
                Content.SetElementValue("SecondPriorityClassName", SecondPriorityClassName);
                Content.SetElementValue("EDoc", EDoc);
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

        public static string UpdateData(RspMsg updateRspMsg)
        {
            string DSNS = FISCA.Authentication.DSAServices.AccessPoint;
            string AccessPoint = @"j.kh.edu.tw";                    
            string Contract = "log";
            string ServiceName = "_.UpdateLog";
            string errMsg = "";
            try
            {

                XElement xmlRoot = new XElement("Request");
                XElement s1 = new XElement("SchoolLog");
                XElement s2 = new XElement("Field");
                XElement s3 = new XElement("Condition");
                XElement Content = new XElement("Content");
                s2.SetElementValue("isVerify", updateRspMsg.Verify);
                foreach(string key in updateRspMsg.Content.Keys)
                    Content.SetElementValue(key, updateRspMsg.Content[key]);

                s2.Add(Content);
                s3.SetElementValue("Uid", updateRspMsg.UID);
                s1.Add(s2);
                s1.Add(s3);
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
        public static XElement QuerySendData(DateTime? beginDate, DateTime? endDate,List<string> isVerifyList)
        {
            XElement elmMsg = null;

             string DSNS = FISCA.Authentication.DSAServices.AccessPoint;

             string AccessPoint = @"j.kh.edu.tw";
            
            string Contract = "log";
            string ServiceName = "_.QueryLog";
            
            try
            {
                XElement xmlRoot = new XElement("Request");
                XElement s1 = new XElement("Field");
                s1.SetElementValue("All", "");
                XElement s2 = new XElement("Condition");
                
                s2.SetElementValue("Dsns", DSNS);
                
                if(beginDate.HasValue)
                    s2.SetElementValue("StartDate", string.Format("{0:yyyy-MM-dd}", beginDate.Value));
                if(endDate.HasValue)
                    s2.SetElementValue("EndDate", string.Format("{0:yyyy-MM-dd}", endDate.Value.AddDays(1)));
                
                if(isVerifyList != null)
                {
                    foreach(string str in isVerifyList)
                    {
                        XElement elmV = new XElement("IsVerify");
                        elmV.Value = str;
                        s2.Add(elmV);
                    }
                        
                }

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



        public static List<RspMsg> GetRspMsgList(XElement _RspXML)
        {
            List<RspMsg> RspMsgList = new List<RspMsg>();          
            if (_RspXML != null)
            {
                if (_RspXML.Element("Body") != null)
                    if (_RspXML.Element("Body").Element("Response") != null)
                    {
                        foreach (XElement elm in _RspXML.Element("Body").Element("Response").Elements("SchoolLog"))
                        {
                            RspMsg rm = new RspMsg();

                            if (elm.Element("UID") != null)
                                rm.UID = elm.Element("UID").Value;

                            if (elm.Element("Action") != null)
                                rm.Action = elm.Element("Action").Value;

                            if (elm.Element("Comment") != null)
                                rm.Comment = elm.Element("Comment").Value;

                            if (elm.Element("isVerify") != null)
                            {
                                rm.Verify = elm.Element("isVerify").Value;

                                //if (rm.Verify.Trim() == "t")
                                //    rm.Verify = "通過";
                                //else if (rm.Verify.Trim() == "f")
                                //    rm.Verify = "未通過";
                                //else
                                //    rm.Verify = "";
                            }

                            if (elm.Element("Content") != null)
                            {
                                XElement xmlContent = elm.Element("Content");

                                if (xmlContent.Element("GradeYear") != null)
                                    rm.Content.Add("GradeYear", xmlContent.Element("GradeYear").Value);

                                if (xmlContent.Element("IDNumber") != null)
                                    rm.Content.Add("IDNumber", xmlContent.Element("IDNumber").Value);

                                if (xmlContent.Element("StudentNumber") != null)
                                    rm.Content.Add("StudentNumber", xmlContent.Element("StudentNumber").Value);

                                if (xmlContent.Element("StudentName") != null)
                                    rm.Content.Add("StudentName", xmlContent.Element("StudentName").Value);

                                if (xmlContent.Element("ClassName") != null)
                                    rm.Content.Add("ClassName", xmlContent.Element("ClassName").Value);

                                if (xmlContent.Element("NewClassName") != null)
                                    rm.Content.Add("NewClassName", xmlContent.Element("NewClassName").Value);

                                if (xmlContent.Element("SeatNo") != null)
                                    rm.Content.Add("SeatNo", xmlContent.Element("SeatNo").Value);

                                if (xmlContent.Element("ScheduleClassDate") != null)
                                    rm.Content.Add("ScheduleClassDate", xmlContent.Element("ScheduleClassDate").Value);

                                if (xmlContent.Element("Reason") != null)
                                    rm.Content.Add("Reason", xmlContent.Element("Reason").Value);

                                if (xmlContent.Element("FirstPriorityClassName") != null)
                                    rm.Content.Add("FirstPriorityClassName", xmlContent.Element("FirstPriorityClassName").Value);

                                if (xmlContent.Element("SecondPriorityClassName") != null)
                                    rm.Content.Add("SecondPriorityClassName", xmlContent.Element("SecondPriorityClassName").Value);


                                if (xmlContent.Element("Summary") != null)
                                    rm.Content.Add("Summary", xmlContent.Element("Summary").Value);

                                if (xmlContent.Element("Comment") != null)
                                    rm.Content.Add("Comment", xmlContent.Element("Comment").Value);

                                if (xmlContent.Element("DocNo") != null)
                                    rm.Content.Add("DocNo", xmlContent.Element("DocNo").Value);

                                if (xmlContent.Element("NumberReduce") != null)
                                    rm.Content.Add("NumberReduce", xmlContent.Element("NumberReduce").Value);

                                if (xmlContent.Element("StudentStatus") != null)
                                    rm.Content.Add("StudentStatus", xmlContent.Element("StudentStatus").Value);

                                if (xmlContent.Element("NewStudentStatus") != null)
                                    rm.Content.Add("NewStudentStatus", xmlContent.Element("NewStudentStatus").Value);

                                if (xmlContent.Element("EDoc") != null)
                                    rm.Content.Add("EDoc", xmlContent.Element("EDoc").Value);
                            }

                            // 詳細內容    
                            if (elm.Element("Detail") != null)
                            {
                                foreach (XElement elms1 in elm.Element("Detail").Elements("Student"))
                                {
                                    RspStud rs = new RspStud();

                                    if (elms1.Element("IDNumber") != null)
                                        rs.IDNumber = elms1.Element("IDNumber").Value;

                                    if (elms1.Element("ClassName") != null)
                                        rs.ClassName = elms1.Element("ClassName").Value;

                                    if (elms1.Element("StudentNumber") != null)
                                        rs.StudentNumber = elms1.Element("StudentNumber").Value;

                                    if (elms1.Element("NewClassName") != null)
                                        rs.NewClassName = elms1.Element("NewClassName").Value;

                                    if (elms1.Element("SeatNo") != null)
                                        rs.SeatNo = elms1.Element("SeatNo").Value;

                                    if (elms1.Element("GradeYear") != null)
                                        rs.GradeYear = elms1.Element("GradeYear").Value;

                                    if (elms1.Element("StudentName") != null)
                                        rs.Name = elms1.Element("StudentName").Value;


                                    if (elms1.Element("Reason") != null)
                                        rs.Reason = elms1.Element("Reason").Value;

                                    if (elms1.Element("StudentStatus") != null)
                                        rs.Status = elms1.Element("StudentStatus").Value;

                                    if (elms1.Element("NewStudentStatus") != null)
                                        rs.NewStatus = elms1.Element("NewStudentStatus").Value;

                                    if (elms1.Element("EDoc") != null)
                                        rs.EDoc = elms1.Element("EDoc").Value;

                                    rm.Detail.Add(rs);
                                }
                            }

                            if (elm.Element("Timestamp") != null)
                                rm.Date = DateTime.Parse(elm.Element("Timestamp").Value);

                            RspMsgList.Add(rm);                         
                        }
                    }
            }
            return RspMsgList;
        }
    }
}

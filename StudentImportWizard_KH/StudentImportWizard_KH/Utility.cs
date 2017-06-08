using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FISCA.Data;
using System.Xml.Linq;
using FISCA.DSAClient;
using JHSchool.StudentExtendControls.Ribbon.StudentImportWizardControls;
using System.Net;
using System.IO;


namespace StudentImportWizard_KH
{
    public class Utility
    {
        public static string SendDataList(string action, List<logStud> logStudList, ImportMode importMode)
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
            try
            {
                {
                    XElement xmlRoot = new XElement("Request");
                    XElement s1 = new XElement("SchoolLog");
                    XElement s2 = new XElement("Field");

                    s2.SetElementValue("DSNS", DSNS);
                    s2.SetElementValue("Action", action);
                    XElement Content = new XElement("Content");
                    string summaryTxt = action + " 共 " + logStudList.Count + " 筆";
                    Content.SetElementValue("Summary", summaryTxt);
                    XElement Detail = new XElement("Detail");

                    if (importMode == ImportMode.Insert)
                    {
                        foreach (logStud ls in logStudList)
                        {
                            XElement StudentXML = new XElement("Student");
                            StudentXML.SetElementValue("IDNumber", ls.IDNumber);
                            StudentXML.SetElementValue("StudentNumber", ls.StudentNumber);
                            StudentXML.SetElementValue("StudentName", ls.StudentName);
                            StudentXML.SetElementValue("ClassName", ls.ClassName);
                            StudentXML.SetElementValue("SeatNo", ls.SeatNo);
                            StudentXML.SetElementValue("GradeYear", ls.GradeYear);
                            StudentXML.SetElementValue("StudentStatus", ls.StudentStatus);
                            Detail.Add(StudentXML);
                        }
                    }
                    else
                    {
                        //  更新
                        foreach (logStud ls in logStudList)
                        {
                            XElement StudentXML = new XElement("Student");
                            StudentXML.SetElementValue("IDNumber", ls.IDNumber);
                            StudentXML.SetElementValue("StudentNumber", ls.StudentNumber);
                            StudentXML.SetElementValue("StudentName", ls.StudentName);
                            StudentXML.SetElementValue("ClassName", ls.oClassName);
                            StudentXML.SetElementValue("NewClassName", ls.ClassName);
                            StudentXML.SetElementValue("SeatNo", ls.SeatNo);
                            StudentXML.SetElementValue("GradeYear", ls.GradeYear);
                            StudentXML.SetElementValue("StudentStatus", ls.oStudentStatus);
                            StudentXML.SetElementValue("NewStudentStatus", ls.StudentStatus);
                            Detail.Add(StudentXML);
                        }
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
                

                //2017/6/7 穎驊新增 高雄項目 [03-01][03] 巨耀局端介接學生資料欄位 巨耀自動編班 更新Service                
                try
                {
                    string urlString = "http://163.32.129.9/khdc/ito";
                    // 準備 Http request
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(urlString);
                    req.Method = "POST";

                    // 呼叫並取得結果
                    HttpWebResponse rsp;
                    rsp = (HttpWebResponse)req.GetResponse();

                    Stream dataStream = rsp.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);

                    string response = reader.ReadToEnd(); //檢查使用，若成功回傳，response 值為 "00"

                    reader.Close();
                    dataStream.Close();
                    rsp.Close();
                }
                catch (Exception e)
                {

                }

            }
            catch (Exception ex) { errMsg = ex.Message; }

            return errMsg;
        }

        public static List<logStud> ConveroClassName(List<logStud> logStudList)
        {
            List<string> sidList = new List<string>();
            List<string> snoList = new List<string>();
            foreach (logStud ls in logStudList)
            {
                if (!string.IsNullOrEmpty(ls.StudentID))
                {
                    sidList.Add(ls.StudentID);
                }
                else {
                    snoList.Add(ls.StudentNumber);
                }
            }

            Dictionary<string, string> sStatusDict = new Dictionary<string, string>();
            sStatusDict.Add("1", "一般");
            sStatusDict.Add("2", "延修");
            sStatusDict.Add("4", "休學");
            sStatusDict.Add("8", "輟學");
            sStatusDict.Add("16", "畢業或離校");
            sStatusDict.Add("256", "刪除");

            Dictionary<string, string> ddDict = new Dictionary<string, string>();
            Dictionary<string, string> ddDsict = new Dictionary<string, string>();
            if (sidList.Count > 0)
            {
                ddDict.Clear();
                ddDsict.Clear();
                string querStr = "select student.id,student_number,class.class_name,student.status as stud_status from student left join class on student.ref_class_id=class.id where  student.id in(" + string.Join(",", sidList.ToArray()) + ")";
                QueryHelper qh1 = new QueryHelper();
                DataTable dt1 = qh1.Select(querStr);

                foreach (DataRow dr in dt1.Rows)
                {
                    string id=dr["id"].ToString();
                    string status = dr["stud_status"].ToString();
                    string className = "";
                    if (dr["class_name"]!=null)
                        className=dr["class_name"].ToString();

                    ddDict.Add(id, className);
                    // 學生狀態
                    if (sStatusDict.ContainsKey(status))
                        ddDsict.Add(id, sStatusDict[status]);

                }
                foreach (logStud ls in logStudList)
                {
                    if (ddDict.ContainsKey(ls.StudentID))
                        ls.oClassName = ddDict[ls.StudentID];
                    
                    if (ddDsict.ContainsKey(ls.StudentID)) 
                        ls.oStudentStatus = ddDsict[ls.StudentID];
                    
                }
            }

            if (snoList.Count > 0)
            {
                ddDict.Clear();
                ddDsict.Clear();
                // 學號只處理學生狀態為一般 1
                string querStr = "select student.id,student_number,class.class_name,student.status as stud_status from student left join class on student.ref_class_id=class.id where student.status=1 and student.student_number in('" + string.Join("','", snoList.ToArray()) + "')";
                QueryHelper qh1 = new QueryHelper();
                DataTable dt1 = qh1.Select(querStr);

                foreach (DataRow dr in dt1.Rows)
                {
                    string status = dr["stud_status"].ToString();
                    string snum = dr["student_number"].ToString();
                    string className = "";
                    if (dr["class_name"] != null)
                        className = dr["class_name"].ToString();

                    if(!ddDict.ContainsKey(snum))
                        ddDict.Add(snum, className);

                    // 學生狀態
                    if (sStatusDict.ContainsKey(status))
                        ddDsict.Add(snum, sStatusDict[status]);

                }
                foreach (logStud ls in logStudList)
                {
                    if (ddDict.ContainsKey(ls.StudentNumber))
                        ls.oClassName = ddDict[ls.StudentNumber];

                    if (ddDsict.ContainsKey(ls.StudentNumber))
                        ls.oStudentStatus = ddDsict[ls.StudentNumber];
                }
            }
            return logStudList;
        }
    }
  

}

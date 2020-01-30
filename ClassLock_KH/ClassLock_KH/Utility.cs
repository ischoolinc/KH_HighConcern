using Aspose.Cells;
using FISCA.DSAClient;
using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using FISCA.Data;
using System.Data;
using System.Net;
using System.IO;

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
        public static string SendData(string ClassName, string GradeYear, string Reason, string Action, string ScheduleClassDate, string Comment, string DocNo, string EDoc, string ClassID, string SecondPriorityClassName, string ThridPriorityClassName)
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
                    s3.SetElementValue("Comment", Comment);
                    s3.SetElementValue("DocNo", DocNo);
                    s3.SetElementValue("EDoc", EDoc);
                    s3.SetElementValue("ClassID", ClassID);
                    s3.SetElementValue("SecondPriorityClassName", SecondPriorityClassName);
                    s3.SetElementValue("ThridPriorityClassName", ThridPriorityClassName);
                    s2.Add(s3);
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
            catch (Exception ex)
            {

                errMsg = ex.Message;
            }
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

        /// <summary>
        /// 取得資料庫日期時間
        /// </summary>
        /// <returns></returns>
        public static DateTime? GetDBServerDateTime()
        {
            DateTime? retVal;
            retVal = null;
            QueryHelper qh = new QueryHelper();
            string query = "select now()";
            DataTable dt = qh.Select(query);
            if (dt.Rows.Count > 0)
                retVal = DateTime.Parse(dt.Rows[0][0].ToString());

            return retVal;
        }

        public static DateTime? GetLastUnlockDate()
        {
            DateTime? retVal;

            retVal = null;
            QueryHelper qh = new QueryHelper();
            string query = "select date_time from $kh.automatic.class.unlock.log order by last_update desc limit 1";
            DataTable dt = qh.Select(query);
            if (dt.Rows.Count > 0)
                retVal = DateTime.Parse(dt.Rows[0][0].ToString());

            return retVal;
        }

        /// <summary>
        /// 上傳檔案到局端
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Data"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string UploadFile(string ID, string Data, string FileName)
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
            string ServiceName = "_.Upload";

            string errMsg = "";
            try
            {
                XElement xmlRoot = new XElement("Request");
                xmlRoot.SetElementValue("ID", ID);
                xmlRoot.SetElementValue("Data", Data);
                xmlRoot.SetElementValue("FileName", FileName);
                xmlRoot.SetElementValue("DSNS", DSNS);
                xmlRoot.SetElementValue("Type", "class");
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




        /// <summary>
        ///確認是否有要通知局端解鎖
        /// </summary>
        /// <returns></returns>
        public static string CheckDistrictUnlockCount()
        {

            string result = "";
            QueryHelper qh = new QueryHelper();
            //先確認資料庫是否有這一個
            string checkHasDistrictNote = @"SELECT count(*) FROM  list  WHERE name = '高雄_局端解鎖_通知設定' ";

            DataTable  dtcheckCount  =qh.Select(checkHasDistrictNote);

            string count= dtcheckCount.Rows[0][0] + "";
            
            //如果沒有通知設定檔
            if (count=="0")
            {
                return "";
            }
            string sql = @"
SELECT 
		   (xpath('/District/IsShow/text()',xmlparse(content content)))[1] AS is_show   
		 , (xpath('/District/LogID/text()',xmlparse(content content)))[1] AS log_id   
FROM list 
WHERE name = '高雄_局端解鎖_通知設定' ";

            try
            {
                DataTable dt = qh.Select(sql);

                if (dt.Rows.Count != 0)
                {
                    string IsShow = dt.Rows[0][0] + "";
                    string LogID = dt.Rows[0][1] + "";
                    if (!string.IsNullOrEmpty(IsShow) && IsShow == "true")
                    {
                        result = LogID;

                    }
                }
            }
            catch (Exception ex)
            {
               

            }


            return result;
        }

    }
}

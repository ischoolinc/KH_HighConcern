using FISCA.DSAClient;
using KH_HighConcern.ImportExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.IO;

namespace KH_HighConcern
{
    public class Utility
    {
        /// <summary>
        /// *.變更特殊身分
        /// *.取消特殊身分
        /// </summary>
        /// <returns></returns>
        public static string SendData(string action, string IDNumber, string StudentNumber, string StudentName, string ClassName, string SeatNo, string DocNo, string NumberReduce, string EDoc)
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
                    s2.SetElementValue("Action", action);
                    XElement Content = new XElement("Content");
                    Content.SetElementValue("IDNumber", IDNumber);
                    Content.SetElementValue("StudentNumber", StudentNumber);
                    Content.SetElementValue("StudentName", StudentName);
                    Content.SetElementValue("ClassName", ClassName);
                    Content.SetElementValue("SeatNo", SeatNo);
                    Content.SetElementValue("NumberReduce", NumberReduce);
                    Content.SetElementValue("DocNo", DocNo);
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

        /// <summary>
        /// 匯入特殊身分使用
        /// </summary>
        /// <returns></returns>
        public static string SendDataList(string action, List<logStud> logStudList)
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
                    s2.SetElementValue("Action", action);
                    XElement Content = new XElement("Content");
                    string summaryTxt = action + " 共 " + logStudList.Count + " 筆";
                    Content.SetElementValue("Summary", summaryTxt);

                    XElement Detail = new XElement("Detail");
                    foreach (logStud ls in logStudList)
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

        /// <summary>
        /// 上傳檔案到局端 - 2020/7/30參考班級鎖定功能
        /// </summary>
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
                xmlRoot.SetElementValue("Type", "student");
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

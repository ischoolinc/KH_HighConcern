using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentClassItem_KH
{
    /// <summary>
    /// 回傳訊息
    /// </summary>
    public class RspMsg
    {


        /// <summary>
        /// 日期時間。
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 動作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Summary:摘要內容。
        /// </summary>
        public Dictionary<string, string> Content = new Dictionary<string, string>();

        /// <summary>
        /// 局端備註。
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 審核結果。(空白,t:通過,f:未通過)
        /// </summary>
        public string Verify { get; set; }

        /// <summary>
        /// 詳細內容
        /// </summary>
        public List<RspStud> Detail = new List<RspStud>();

        /// <summary>
        /// 解析取得校端傳送備註
        /// </summary>
        /// <returns></returns>
        public string GetComment()
        {
            string retVal = "";
            if (Content.ContainsKey("Comment"))
                retVal = Content["Comment"];

            return retVal;
        }

        /// <summary>
        ///  取得 Content 組合
        /// </summary>
        /// <returns></returns>
        public string GetContentString(bool newLine)
        {
            string retVal="";
            string selAction = Action.Trim();
            List<string> retList = new List<string>();

            switch (selAction)
            { 
                case "匯入新增學生":
                case "匯入更新班級":
                case "匯入特殊身分":
                    if (Content.ContainsKey("Summary"))
                        retList.Add(Content["Summary"]);
                    break;
                case "解除鎖定班級":
                    retList.Add(selAction);
                    if (Content.ContainsKey("ClassName"))
                        retList.Add("班級「" + Content["ClassName"] + "」");

                    if (Content.ContainsKey("GradeYear"))
                        retList.Add("年級「" +Content["GradeYear"]+ "」");
                    break;
                case "鎖定班級":
                    retList.Add(selAction);
                    if (Content.ContainsKey("ClassName"))
                        retList.Add("班級「" + Content["ClassName"] + "」");

                    if (Content.ContainsKey("GradeYear"))
                        retList.Add("年級「" +Content["GradeYear"]+ "」");

                    ////if (Content.ContainsKey("Reason"))
                    ////    retList.Add("理由「" + Content["Reason"] + "」");

                    if (Content.ContainsKey("Comment"))
                        retList.Add("備註「" + Content["Comment"] + "」");

                    if (Content.ContainsKey("DocNo"))
                        retList.Add("文號「" + Content["DocNo"] + "」");

                    break;
            
                case "變更特殊身分":
                    retList.Add(selAction);
                    if (Content.ContainsKey("StudentName"))
                        retList.Add("學生「" + Content["StudentName"] + "」");
                    if (Content.ContainsKey("IDNumber"))
                        retList.Add("身分證「" + Content["IDNumber"] + "」");
                    if (Content.ContainsKey("StudentNumber"))
                        retList.Add("學號「" + Content["StudentNumber"] + "」");
                    if (Content.ContainsKey("ClassName"))
                        retList.Add("班級「" + Content["ClassName"] + "」");
                    if (Content.ContainsKey("SeatNo"))
                        retList.Add("座號「" + Content["SeatNo"] + "」");
                    if (Content.ContainsKey("NumberReduce"))
                        retList.Add("減免人數「" + Content["NumberReduce"] + "」");
                    if (Content.ContainsKey("DocNo"))
                        retList.Add("文號「" + Content["DocNo"] + "」");
                    break;

                case "調整班級":
                       retList.Add(selAction);
                    if (Content.ContainsKey("StudentName"))
                        retList.Add("學生「" + Content["StudentName"] + "」");

                    if (Content.ContainsKey("ClassName") && Content.ContainsKey("NewClassName"))
                        retList.Add("從「" + Content["ClassName"] + "調整班級到" + Content["NewClassName"] + "」");

                            if (Content.ContainsKey("IDNumber"))
                        retList.Add("身分證「" + Content["IDNumber"] + "」");
                    if (Content.ContainsKey("StudentNumber"))
                        retList.Add("學號「" + Content["StudentNumber"] + "」");

                    //if (Content.ContainsKey("Reason"))
                    //    retList.Add("理由「" + Content["Reason"] + "」");

                    if (Content.ContainsKey("FirstPriorityClassName"))
                        retList.Add("第一優先順班級「" + Content["FirstPriorityClassName"] + "」");

                    break;

                case "自動轉入":
                         retList.Add(selAction);
                    if (Content.ContainsKey("StudentName"))
                        retList.Add("學生「" + Content["StudentName"] + "」");
                    if (Content.ContainsKey("IDNumber"))
                        retList.Add("身分證「" + Content["IDNumber"] + "」");
                    if (Content.ContainsKey("StudentNumber"))
                        retList.Add("學號「" + Content["StudentNumber"] + "」");
                    if (Content.ContainsKey("NewClassName"))
                        retList.Add("班級「" + Content["NewClassName"] + "」");
                    if (Content.ContainsKey("SeatNo"))
                        retList.Add("座號「" + Content["SeatNo"] + "」");
                    break;

                case "狀態變更":
                    retList.Add(selAction);
                    if (Content.ContainsKey("StudentName"))
                        retList.Add("學生「" + Content["StudentName"] + "」");
                    if (Content.ContainsKey("IDNumber"))
                        retList.Add("身分證「" + Content["IDNumber"] + "」");
                    if (Content.ContainsKey("StudentNumber"))
                        retList.Add("學號「" + Content["StudentNumber"] + "」");
                    if (Content.ContainsKey("ClassName"))
                        retList.Add("班級「" + Content["ClassName"] + "」");
                    if (Content.ContainsKey("StudentStatus"))
                        retList.Add("狀態變更前「" + Content["StudentStatus"] + "」");
                    if (Content.ContainsKey("NewStudentStatus"))
                        retList.Add("狀態變更後「" + Content["NewStudentStatus"] + "」");

                    break;     
            }

            if (newLine)
                retVal = string.Join("\r\n", retList.ToArray());
            else
                retVal = string.Join(" ", retList.ToArray());

            return retVal;
        
        }
    }
}

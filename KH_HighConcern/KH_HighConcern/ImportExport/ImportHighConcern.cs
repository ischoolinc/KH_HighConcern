using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.DocumentValidator;
using Campus.Import;
using KH_HighConcern.DAO;
using K12.Data;

namespace KH_HighConcern.ImportExport
{
    public class ImportHighConcern : ImportWizard
    {
        private ImportOption _Option;
        Dictionary<string, UDT_HighConcern> _HighConcernDict = new Dictionary<string, UDT_HighConcern>();
        Dictionary<string, string> _StudentNumIDDict = new Dictionary<string, string>();
        Dictionary<string, StudentRecord> _StudentRecDict = new Dictionary<string, StudentRecord>();

        EventHandler eh;
        string EventCode = "KH_HighConcern_HighConcernContent";

        public override ImportAction GetSupportActions()
        {
            return ImportAction.InsertOrUpdate;
        }

        public ImportHighConcern()
        {
            this.IsSplit = false;
            this.IsLog = false;
            //啟動更新事件
            eh = FISCA.InteractionService.PublishEvent(EventCode);
        }

        public override string GetValidateRule()
        {
            return Properties.Resources.HighConcernValDef;
        }

        public override string Import(List<IRowStream> Rows)
        {            
            if (_Option.Action == ImportAction.InsertOrUpdate)
            {
                List<UDT_HighConcern> HighConcernList = new List<UDT_HighConcern>();
                List<logStud> logStudList = new List<logStud>();
                foreach(IRowStream row in Rows)
                {
                    string IDNumber = "", StudentNumber = "", StudentName = "", ClassName = "", SeatNo = "", NumberReduce = "", DocNo = "";

                    StudentNumber = row.GetValue("學號");
                    int hCount = int.Parse(row.GetValue("減免人數"));
                    DocNo = row.GetValue("文號");
                    if (_StudentNumIDDict.ContainsKey(StudentNumber))
                    {
                        string sid = _StudentNumIDDict[StudentNumber];

                        StudentRecord rec = _StudentRecDict[sid];
                        IDNumber = rec.IDNumber;
                        StudentName = rec.Name;

                        if (rec.SeatNo.HasValue)
                            SeatNo = rec.SeatNo.Value.ToString();
                        if (rec.Class != null)
                            ClassName = rec.Class.Name;

                        NumberReduce = hCount.ToString();

                        if (_HighConcernDict.ContainsKey(sid))
                        {
                            // 更新
                            _HighConcernDict[sid].NumberReduce = hCount;
                            _HighConcernDict[sid].DocNo = DocNo;
                            HighConcernList.Add(_HighConcernDict[sid]);
                        }
                        else
                        {
                            // 新增
                            UDT_HighConcern newData = new UDT_HighConcern();
                            newData.ClassName = ClassName;
                            newData.SeatNo = SeatNo;
                            newData.StudentNumber = StudentNumber;
                            newData.RefStudentID = sid;
                            newData.HighConcern = true;
                            newData.NumberReduce = hCount;
                            newData.DocNo = DocNo;
                            HighConcernList.Add(newData);
                        }
                        
                        // 傳送至局端
                        logStud ls= new logStud ();
                        ls.IDNumber = IDNumber;
                        ls.StudentNumber = StudentNumber;
                        ls.StudentName = StudentName;
                        ls.ClassName = ClassName;
                        ls.SeatNo = SeatNo;
                        ls.DocNo = DocNo;
                        ls.NumberReduce = NumberReduce;
                        logStudList.Add(ls);

                        //Utility.SendData("匯入特殊身分", IDNumber, StudentNumber, StudentName, ClassName, SeatNo, DocNo, NumberReduce);
                    }                    
                }
                // save
                if (logStudList.Count > 0)
                {
                    Utility.SendDataList("匯入特殊身分", logStudList);
                }
                HighConcernList.SaveAll();
                eh(this, EventArgs.Empty);
            }       
            return "";
        }

        /// <summary>
        /// 匯入前準備
        /// </summary>
        /// <param name="Option"></param>
        public override void Prepare(ImportOption Option)
        {
            _Option = Option;
            _HighConcernDict = UDTTransfer.GetHighConcernDictAll();
            _StudentNumIDDict = UDTTransfer.GetStudentNumIDDictAll();
            // 取得學生資料
            List<string> studentIDList = _StudentNumIDDict.Values.ToList();
            List<StudentRecord> recList = Student.SelectByIDs(studentIDList);
            foreach (StudentRecord rec in recList)            
                _StudentRecDict.Add(rec.ID, rec);
            
        }
    }
}

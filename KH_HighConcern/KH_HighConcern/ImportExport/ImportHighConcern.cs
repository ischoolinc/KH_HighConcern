using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.DocumentValidator;
using Campus.Import;
using KH_HighConcern.DAO;

namespace KH_HighConcern.ImportExport
{
    public class ImportHighConcern : ImportWizard
    {
        private ImportOption _Option;
        Dictionary<string, UDT_HighConcern> _HighConcernDict = new Dictionary<string, UDT_HighConcern>();
        Dictionary<string, string> _StudentNumIDDict = new Dictionary<string, string>();

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
                foreach(IRowStream row in Rows)
                {
                    string StudentNumber = row.GetValue("學號");
                    int hCount = int.Parse(row.GetValue("減免人數"));
                    if (_StudentNumIDDict.ContainsKey(StudentNumber))
                    {
                        string sid = _StudentNumIDDict[StudentNumber];
                        if (_HighConcernDict.ContainsKey(sid))
                        {
                            // 更新
                            _HighConcernDict[sid].NumberReduce = hCount;
                            HighConcernList.Add(_HighConcernDict[sid]);
                        }
                        else
                        {
                            // 新增
                            UDT_HighConcern newData = new UDT_HighConcern();
                            newData.ClassName = row.GetValue("班級");
                            newData.SeatNo = row.GetValue("座號");
                            newData.StudentNumber = StudentNumber;
                            newData.RefStudentID = sid;
                            newData.HighConcern = true;
                            newData.NumberReduce = hCount;
                            HighConcernList.Add(newData);
                        }
                    }
                }
                // save
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
        }
    }
}

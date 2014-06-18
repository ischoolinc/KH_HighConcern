using FISCA.DSAUtil;
using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLock_KH.DAO;
using System.ComponentModel;
using FISCA;
using FISCA.Presentation;
using FISCA.Permission;

namespace ClassLock_KH
{
    public class Program
    {
        static BackgroundWorker _bgLLoadUDT = new BackgroundWorker();
        
        [MainMethod()]
        public static void Main()
        {
            _bgLLoadUDT.DoWork += _bgLLoadUDT_DoWork;
            _bgLLoadUDT.RunWorkerCompleted += _bgLLoadUDT_RunWorkerCompleted;
            _bgLLoadUDT.RunWorkerAsync();

            Dictionary<string, UDT_ClassLock> _UDT_ClassLockDict = UDTTransfer.GetClassLockNameIDDict();

            Catalog catalog01 = RoleAclSource.Instance["班級"]["功能按鈕"];
            catalog01.Add(new RibbonFeature("KH_HighConcern_ClassLock", "班級鎖定/解鎖"));

            ListPaneField ClassLockField = new ListPaneField("班級鎖定");
            ClassLockField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_UDT_ClassLockDict.ContainsKey(e.Key))
                {
                    e.Value = "鎖定";
                }
            };
            K12.Presentation.NLDPanels.Class.AddListPaneField(ClassLockField);


            K12.Presentation.NLDPanels.Class.SelectedSourceChanged += delegate {
                // 檢查當有權限並只選一個班才可以使用
                K12.Presentation.NLDPanels.Class.ListPaneContexMenu["班級鎖定/解鎖"].Enable = UserAcl.Current["KH_HighConcern_ClassLock"].Executable && (K12.Presentation.NLDPanels.Class.SelectedSource.Count == 1);            
            };

            K12.Presentation.NLDPanels.Class.ListPaneContexMenu["班級鎖定/解鎖"].Click += delegate
            {
                // 一次處理一筆
                    // 取得 ClassLock UDT
                    UDT_ClassLock data = null;
                    string cid = K12.Presentation.NLDPanels.Class.SelectedSource[0];
                    if (_UDT_ClassLockDict.ContainsKey(cid))
                        data = _UDT_ClassLockDict[cid];

                    K12.Data.ClassRecord classRec = K12.Data.Class.SelectByID(cid);
                    string grYear = "";
                    if (classRec.GradeYear.HasValue)
                        grYear = classRec.GradeYear.Value.ToString();

                    if (data == null)
                    {
                        if (FISCA.Presentation.Controls.MsgBox.Show("將自動將[班級鎖定]並傳送至局端", "班級鎖定", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        {
                            // 沒有鎖定
                            data = new UDT_ClassLock();
                            data.ClassID = cid;
                            data.ClassName = classRec.Name;
                            Utility.SendData(classRec.Name, grYear, "", "鎖定");                            
                        }
                    }
                    else
                    {
                        if (FISCA.Presentation.Controls.MsgBox.Show("將自動將[班級解鎖]並傳送至局端", "班級解鎖", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        {
                            // 已被鎖定解鎖
                            data.Deleted = true;
                            Utility.SendData(classRec.Name, grYear, "", "解除鎖定");
                        }
                    }
                    // 儲存 UDT
                    data.Save();

                    _UDT_ClassLockDict = UDTTransfer.GetClassLockNameIDDict();
                    ClassLockField.Reload();
            };
        }
                

        static void _bgLLoadUDT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        static void _bgLLoadUDT_DoWork(object sender, DoWorkEventArgs e)
        {
            UDTTransfer.CreateUDTTable();

        }


      
    }
}

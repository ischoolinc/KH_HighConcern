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


            ListPaneField ClassLockField = new ListPaneField("班級鎖定");
            ClassLockField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_UDT_ClassLockDict.ContainsKey(e.Key))
                {
                    e.Value = "鎖定";
                }
            };
            K12.Presentation.NLDPanels.Class.AddListPaneField(ClassLockField);


            K12.Presentation.NLDPanels.Class.ListPaneContexMenu["班級鎖定"].Enable = true;
            K12.Presentation.NLDPanels.Class.ListPaneContexMenu["班級鎖定"].Click += delegate {
                // 一次處理一筆
                if (K12.Presentation.NLDPanels.Class.SelectedSource.Count == 1)
                { 
                    // 取得 ClassLock UDT
                    UDT_ClassLock data = null;
                    string cid = K12.Presentation.NLDPanels.Class.SelectedSource[0];
                    if (_UDT_ClassLockDict.ContainsKey(cid))
                        data = _UDT_ClassLockDict[cid];


                    if (data == null)
                    {
                        if (FISCA.Presentation.Controls.MsgBox.Show("將自動將班級鎖定並傳送至局端", "班級鎖定", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        {
                            // 沒有鎖定
                            data = new UDT_ClassLock();
                            data.ClassID = cid;
                            data.ClassName = K12.Data.Class.SelectByID(cid).Name;
                        }
                    }
                    else
                    {
                        if (FISCA.Presentation.Controls.MsgBox.Show("將自動將班級解鎖並傳送至局端", "班級解鎖",System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Warning,System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        {
                            // 已被鎖定解鎖
                            data.Deleted = true;
                        }
                    }
                    // 儲存 UDT
                    data.Save();

                    _UDT_ClassLockDict = UDTTransfer.GetClassLockNameIDDict();
                    ClassLockField.Reload();    
                }            
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

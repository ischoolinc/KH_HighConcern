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
            catalog01.Add(new RibbonFeature("KH_HighConcern_AllClassUnLock", "全部班級解鎖"));
            catalog01.Add(new RibbonFeature("KH_HighConcern_SendClassDataView", "檢視傳送局端備查紀錄"));

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

            RibbonBarItem rbSendClassDataView = K12.Presentation.NLDPanels.Class.RibbonBarItems["其它"];
            rbSendClassDataView["檢視傳送局端備查紀錄"].Enable = UserAcl.Current["KH_HighConcern_SendClassDataView"].Executable;
            rbSendClassDataView["檢視傳送局端備查紀錄"].Click += delegate
            {
                SendDataView sdv = new SendDataView();
                sdv.ShowDialog();
            };

            RibbonBarItem rbiDelAll = K12.Presentation.NLDPanels.Class.RibbonBarItems["其它"];
            rbiDelAll["全部班級解鎖"].Enable = UserAcl.Current["KH_HighConcern_AllClassUnLock"].Executable;
            rbiDelAll["全部班級解鎖"].Click += delegate
            {

                if (FISCA.Presentation.Controls.MsgBox.Show("將全部班級解鎖，按下「是」確認後，需報局備查。", "全部班級解鎖", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {

                    // 全部解鎖
                    UDTTransfer.UnlockAllClass();

                    List<string> ClassNameList = (from data in _UDT_ClassLockDict.Values select data.ClassName).ToList();

                    string classNames = string.Join(",", ClassNameList.ToArray());


                    string errMsg = Utility.SendData(classNames, "", "", "解除鎖定班級", "");
                    if (errMsg != "")
                        FISCA.Presentation.Controls.MsgBox.Show(errMsg);
                    else
                        FISCA.Presentation.Controls.MsgBox.Show("全部班級解鎖");

                    // 重新整理
                    _UDT_ClassLockDict = UDTTransfer.GetClassLockNameIDDict();
                    ClassLockField.Reload();
                }
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
                        // 編班委員會會議日期
                        string strDate = "";

                        SendDataForm sdf = new SendDataForm();
                        if (sdf.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                        {
                            strDate = sdf.GetSendDate();
                            if (FISCA.Presentation.Controls.MsgBox.Show("「班級鎖定」，按下「是」確認後，需報局備查。", "班級鎖定", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                            {
                                // 沒有鎖定
                                data = new UDT_ClassLock();
                                data.ClassID = cid;
                                data.ClassName = classRec.Name;
                                string errMsg = Utility.SendData(classRec.Name, grYear, "", "鎖定班級", strDate);
                                if (errMsg != "")
                                    FISCA.Presentation.Controls.MsgBox.Show(errMsg);
                                else
                                    FISCA.Presentation.Controls.MsgBox.Show("已鎖定");
                            }
                        }
                    }
                    else
                    {
                        if (FISCA.Presentation.Controls.MsgBox.Show("「班級解鎖」，按下「是」確認後，需報局備查。", "班級解鎖", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        {
                            // 已被鎖定解鎖
                            data.Deleted = true;
                            string errMsg = Utility.SendData(classRec.Name, grYear, "", "解除鎖定班級", "");
                            if (errMsg != "")
                                FISCA.Presentation.Controls.MsgBox.Show(errMsg);
                            else
                                FISCA.Presentation.Controls.MsgBox.Show("已解鎖");
                        }
                    }
                    // 儲存 UDT
                    if (data != null)
                    {
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

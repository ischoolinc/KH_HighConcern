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
            Dictionary<string, KH_HighConcernCalc.ClassStudent> _ClassStudentDict = KH_HighConcernCalc.Calc.GetClassStudentAllIDDict();

            Catalog catalog01 = RoleAclSource.Instance["班級"]["功能按鈕"];
            catalog01.Add(new RibbonFeature("KH_HighConcern_ClassLock", "班級鎖定/解鎖"));
            catalog01.Add(new RibbonFeature("KH_HighConcern_AllClassUnLock", "全部班級解鎖"));
            catalog01.Add(new RibbonFeature("KH_HighConcern_SendClassDataView", "檢視傳送局端備查紀錄"));

            ListPaneField ClassLockStudentCountField = new ListPaneField("編班人數");
            ClassLockStudentCountField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_ClassStudentDict.ContainsKey(e.Key))
                {
                    if (_ClassStudentDict.ContainsKey(e.Key))
                    {
                        
                        e.Value = _ClassStudentDict[e.Key].ClassStudentCountStr;
                    }
                }
            };
            K12.Presentation.NLDPanels.Class.AddListPaneField(ClassLockStudentCountField);
                       

            ListPaneField ClassLockField = new ListPaneField("班級鎖定");
            ClassLockField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_UDT_ClassLockDict.ContainsKey(e.Key))
                {

                    if (_UDT_ClassLockDict[e.Key].UnAutoUnlock)
                        e.Value = "鎖定(不自動解鎖)";
                    else
                        e.Value = "鎖定";
                }
            };
            K12.Presentation.NLDPanels.Class.AddListPaneField(ClassLockField);

            ListPaneField ClassLockCommentField = new ListPaneField("鎖定備註");
            ClassLockCommentField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_UDT_ClassLockDict.ContainsKey(e.Key))
                {
                    e.Value = _UDT_ClassLockDict[e.Key].Comment;
                }
            };
            K12.Presentation.NLDPanels.Class.AddListPaneField(ClassLockCommentField);


            ListPaneField ClassLockSStudentCountField = new ListPaneField("特殊生人數");
            ClassLockSStudentCountField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_ClassStudentDict.ContainsKey(e.Key))
                {
                    if (_ClassStudentDict.ContainsKey(e.Key))
                        e.Value = _ClassStudentDict[e.Key].ClassHStudentCount;
                }
            };
            K12.Presentation.NLDPanels.Class.AddListPaneField(ClassLockSStudentCountField);


            K12.Presentation.NLDPanels.Class.SelectedSourceChanged += delegate {
                // 檢查當有權限並只選一個班才可以使用
                K12.Presentation.NLDPanels.Class.ListPaneContexMenu["班級鎖定/解鎖"].Enable = UserAcl.Current["KH_HighConcern_ClassLock"].Executable && (K12.Presentation.NLDPanels.Class.SelectedSource.Count == 1);            
            };

            //RibbonBarItem rbSendClassDataView = K12.Presentation.NLDPanels.Class.RibbonBarItems["其它"];
            //rbSendClassDataView["檢視傳送局端備查紀錄"].Enable = UserAcl.Current["KH_HighConcern_SendClassDataView"].Executable;
            //rbSendClassDataView["檢視傳送局端備查紀錄"].Click += delegate
            //{
            //    SendDataView sdv = new SendDataView();
            //    sdv.ShowDialog();
            //};

            RibbonBarItem rbiDelAll = K12.Presentation.NLDPanels.Class.RibbonBarItems["其它"];
            rbiDelAll["全部班級解鎖"].Enable = UserAcl.Current["KH_HighConcern_AllClassUnLock"].Executable;
            rbiDelAll["全部班級解鎖"].Click += delegate
            {
                MsgForm mf2 = new MsgForm();
                mf2.Text = "全部班級解鎖";
                mf2.SetMsg("將全部班級解鎖，按下「是」確認後，局端會留解鎖紀錄。");
                //if (FISCA.Presentation.Controls.MsgBox.Show("將全部班級解鎖，按下「是」確認後，局端會留解鎖紀錄。", "全部班級解鎖", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                if(mf2.ShowDialog()== System.Windows.Forms.DialogResult.Yes)
                {

                    // 全部解鎖
                    UDTTransfer.UnlockAllClass();

                    List<string> ClassNameList = (from data in _UDT_ClassLockDict.Values select data.ClassName).ToList();

                    string classNames = string.Join(",", ClassNameList.ToArray());


                    string errMsg = Utility.SendData(classNames, "", "", "解除鎖定班級", "","","","");
                    if (errMsg != "")
                        FISCA.Presentation.Controls.MsgBox.Show(errMsg);
                    else
                        FISCA.Presentation.Controls.MsgBox.Show("全部班級解鎖");

                    // 重新整理
                    _UDT_ClassLockDict = UDTTransfer.GetClassLockNameIDDict();
                    ClassLockField.Reload();
                    ClassLockCommentField.Reload();
                    ClassLockStudentCountField.Reload();
                    ClassLockSStudentCountField.Reload();
                }
            };

            #region 同步更新

            // 當高關懷特殊身分有更新
            FISCA.InteractionService.SubscribeEvent("KH_HighConcern_HighConcernContent", (sender, args) =>
            {
                _ClassStudentDict = KH_HighConcernCalc.Calc.GetClassStudentAllIDDict();
                ClassLockStudentCountField.Reload();
                ClassLockSStudentCountField.Reload();
            });

            // 當變更學生狀態
            FISCA.InteractionService.SubscribeEvent("KH_StudentChangeStatus", (sender, args) =>
            {
                _ClassStudentDict = KH_HighConcernCalc.Calc.GetClassStudentAllIDDict();
                ClassLockStudentCountField.Reload();
                ClassLockSStudentCountField.Reload();
            });

            // 當變更學生班級
            FISCA.InteractionService.SubscribeEvent("KH_StudentClassItemContent", (sender, args) =>
            {
                _ClassStudentDict = KH_HighConcernCalc.Calc.GetClassStudentAllIDDict();
                ClassLockStudentCountField.Reload();
                ClassLockSStudentCountField.Reload();
            });

            // 當變更學生轉入
            FISCA.InteractionService.SubscribeEvent("KH_StudentTransferStudentBriefItem", (sender, args) =>
            {
                _ClassStudentDict = KH_HighConcernCalc.Calc.GetClassStudentAllIDDict();
                ClassLockStudentCountField.Reload();
                ClassLockSStudentCountField.Reload();
            });

            // 當變更學生匯入
            FISCA.InteractionService.SubscribeEvent("KH_StudentImportWizard", (sender, args) =>
            {
                _ClassStudentDict = KH_HighConcernCalc.Calc.GetClassStudentAllIDDict();
                ClassLockStudentCountField.Reload();
                ClassLockSStudentCountField.Reload();
            });

            // 當變更學生-轉入
            FISCA.InteractionService.SubscribeEvent("KH_StudentTransStudBase", (sender, args) =>
            {
                _ClassStudentDict = KH_HighConcernCalc.Calc.GetClassStudentAllIDDict();
                ClassLockStudentCountField.Reload();
                ClassLockSStudentCountField.Reload();
            });

            #endregion
            

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
                        string strComment = "";
                        string strDocNo = "";
                        string strEDoc = "";

                        SendDataForm sdf = new SendDataForm();
                        if (sdf.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                        {
                            strDate = sdf.GetSendDate();
                            strComment = sdf.GetComment();
                            strDocNo = sdf.GetDocNo();
                            strEDoc = sdf.GetEDoc();
                            MsgForm mf = new MsgForm();
                            mf.Text = "班級鎖定";
                            mf.SetMsg("「班級鎖定」，按下「是」確認後，除集中式特殊班級，餘需函報教育局並由局端線上審核。");
                            //if (FISCA.Presentation.Controls.MsgBox.Show("「班級鎖定」，按下「是」確認後，除集中式特殊班級，餘需函報教育局並由局端線上審核。", "班級鎖定", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                            if(mf.ShowDialog()== System.Windows.Forms.DialogResult.Yes)
                            {
                                // 沒有鎖定
                                data = new UDT_ClassLock();
                                data.ClassID = cid;
                                data.ClassName = classRec.Name;
                                data.Comment = strComment;
                                data.DocNo = strDocNo;
                                data.DateStr = strDate;
                                data.EDoc = strEDoc;
                                data.UnAutoUnlock = sdf.GetNUnLock();

                                string errMsg = Utility.SendData(classRec.Name, grYear, "", "鎖定班級", strDate,strComment,strDocNo,strEDoc);
                                if (errMsg != "")
                                    FISCA.Presentation.Controls.MsgBox.Show(errMsg);
                                else
                                {
                                    if (data.UnAutoUnlock)
                                        FISCA.Presentation.Controls.MsgBox.Show("已鎖定(不自動解鎖)");
                                    else
                                        FISCA.Presentation.Controls.MsgBox.Show("已鎖定");
                                }
                            }
                        }
                    }
                    else
                    {
                        MsgForm mf1 = new MsgForm();
                        mf1.Text = "班級解鎖";
                        mf1.SetMsg("「班級解鎖」，按下「是」確認後，局端會留解鎖紀錄。");
                        //if (FISCA.Presentation.Controls.MsgBox.Show("「班級解鎖」，按下「是」確認後，局端會留解鎖紀錄。", "班級解鎖", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        if(mf1.ShowDialog()== System.Windows.Forms.DialogResult.Yes)
                        {
                            // 已被鎖定解鎖
                            data.Deleted = true;
                            string errMsg = Utility.SendData(classRec.Name, grYear, "", "解除鎖定班級", "","","","");
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
                        ClassLockCommentField.Reload();
                        ClassLockStudentCountField.Reload();
                        ClassLockSStudentCountField.Reload();
                    }
            };
        }
                

        static void _bgLLoadUDT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        static void _bgLLoadUDT_DoWork(object sender, DoWorkEventArgs e)
        {
            UDTTransfer.CreateUDTTable();

            //FISCA.ServerModule.AutoManaged("http://module.ischool.com.tw/module/137/KHCentralOffice/udm.xml");

            // 檢查是否需要全部班級解鎖
            // 取得Server時間
            DateTime? serDT = Utility.GetDBServerDateTime();

            // 2015 年開始
            if (serDT.HasValue && serDT.Value.Year >= 2015)
            {
                // 第一學期
                DateTime dt1b = new DateTime(DateTime.Now.Year, 8, 1);
                DateTime dt1e = new DateTime(DateTime.Now.Year + 1, 2, 1);
                // 第二學期
                DateTime dt2b = new DateTime(DateTime.Now.Year, 2, 1);
                DateTime dt2e = new DateTime(DateTime.Now.Year, 8, 1);

                //檢查是否符合解鎖規則
                bool chkUnLock = false;

                if (serDT.Value >= dt1b || serDT.Value >= dt2b)
                    chkUnLock = true;

                // 需要解鎖
                if (chkUnLock)
                {
                    bool runUnLock = true;

                    // 取得最後解鎖日期
                    DateTime? unLockDate = Utility.GetLastUnlockDate();

                    if (unLockDate.HasValue)
                    {
                        if (unLockDate.Value.Month > 7)
                        {
                            if (unLockDate.Value >= dt1b && unLockDate.Value < dt1e)
                                runUnLock = false;
                        }
                        else
                        {
                            if (serDT.Value >= dt1b && serDT.Value < dt2e)
                            { 
                            
                            }
                            else
                            {
                                if (unLockDate.Value >= dt2b && unLockDate.Value < dt2e)
                                    runUnLock = false;
                            }
                        }
                    }

                    // 執行全部解鎖
                    if (runUnLock)
                    {
                        // 寫入解鎖log
                        UDT_ClassLock_Log lo = new UDT_ClassLock_Log();
                        lo.Action = "班級解鎖";
                        lo.Date = serDT.Value;
                        lo.Save();

                        UDTTransfer.UnlockAllClass();
                    }
                }
            }        
        }

    }
}

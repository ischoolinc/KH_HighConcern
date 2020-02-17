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
using JHSchool.Data;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using KHJHLog.Model;

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
            catalog01.Add(new RibbonFeature("KH_HighConcern_ClassLockAppling", "取消鎖定申請(年級鎖班超過1/2)"));
            catalog01.Add(new RibbonFeature("KH_HighConcern_AllClassUnLock", "全部班級解鎖"));
            catalog01.Add(new RibbonFeature("KH_HighConcern_SendClassDataView", "檢視傳送局端備查紀錄"));



            //通知(局端解鎖)
            string LogID = Utility.CheckDistrictUnlockCount();
            Boolean IsShowDistNotification = !String.IsNullOrEmpty(LogID);
            if (IsShowDistNotification)//如果有需要通知之項目
            {
                (new FrmDistrictNotification(LogID)).Show();
            }


            ListPaneField ClassLockStudentCountField = new ListPaneField("編班人數");
            ClassLockStudentCountField.GetVariable += delegate (object sender, GetVariableEventArgs e)
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
            ClassLockField.GetVariable += delegate (object sender, GetVariableEventArgs e)
            {
                //if (_UDT_ClassLockDict.ContainsKey(e.Key))
                //{
                //    if (!(_UDT_ClassLockDict[e.Key].LockApplingStatus == "鎖班申請中_鎖班數超過二分之一"))
                //    // 當有鎖定再顯示
                //    {
                //        if (_UDT_ClassLockDict[e.Key].isLock)
                //        {
                //            if (_UDT_ClassLockDict[e.Key].UnAutoUnlock)
                //            {
                //                e.Value = "鎖定(不自動解鎖)";
                //            }
                //            else if (String.IsNullOrEmpty(_UDT_ClassLockDict[e.Key].LockApplingStatus))
                //            {
                //                e.Value = "鎖定";
                //            }
                //            else if (_UDT_ClassLockDict[e.Key].LockApplingStatus == ApplyStatus.局端同意鎖班.ToString())
                //            {
                //                e.Value = _UDT_ClassLockDict[e.Key].LockApplingStatus;
                //            }
                //        }
                //    }
                //    else// 申請中
                //    {
                //        if (_UDT_ClassLockDict[e.Key].LockApplingStatus == ApplyStatus.鎖班申請中_鎖班數超過二分之一.ToString())
                //        {
                //            e.Value = _UDT_ClassLockDict[e.Key].LockApplingStatus;
                //        }
                //        else if (_UDT_ClassLockDict[e.Key].LockApplingStatus == ApplyStatus.鎖班申請退回_鎖班數超過二分之一.ToString())
                //        {
                //            e.Value = _UDT_ClassLockDict[e.Key].LockApplingStatus;
                //        }
                //    }
                //}


                if (_UDT_ClassLockDict.ContainsKey(e.Key))
                {
                    if (_UDT_ClassLockDict[e.Key].isLock) //如果是鎖班狀態 顯示 ==> 鎖定 || 鎖定(不自動解鎖)
                    {
                        if (_UDT_ClassLockDict[e.Key].UnAutoUnlock)
                        {
                            e.Value = "鎖定(不自動解鎖)";
                        }
                        else if (String.IsNullOrEmpty(_UDT_ClassLockDict[e.Key].LockApplingStatus))
                        {
                            e.Value = "鎖定";
                        }
                    }
                    else // 如果是  (鎖班申請中) || (申請退回)
                    {
                            e.Value = _UDT_ClassLockDict[e.Key].LockApplingStatus;
                    }
                }
            };



            K12.Presentation.NLDPanels.Class.AddListPaneField(ClassLockField);

          ListPaneField ClassLockCommentField = new ListPaneField("鎖定備註");
            ClassLockCommentField.GetVariable += delegate (object sender, GetVariableEventArgs e)
            {
                // 只要有資料就顯示
                if (_UDT_ClassLockDict.ContainsKey(e.Key))
                {
                    e.Value = _UDT_ClassLockDict[e.Key].Comment;
                }
            };
            K12.Presentation.NLDPanels.Class.AddListPaneField(ClassLockCommentField);


            //Jean增加欄位=>20200121跟耀明討論後局端備註先拿掉。
            //ListPaneField DistrictComment = new ListPaneField("局端備註");
            //DistrictComment.GetVariable += delegate (object sender, GetVariableEventArgs e)
            //{
            //    if (_UDT_ClassLockDict.ContainsKey(e.Key))
            //    {
            //        // 當有鎖定再顯示
            //        if (_UDT_ClassLockDict[e.Key].isLock)
            //        {
            //            e.Value = _UDT_ClassLockDict[e.Key].DistrictComment;
            //        }
            //    }
            //};
            //K12.Presentation.NLDPanels.Class.AddListPaneField(DistrictComment);


            // 局端解鎖/鎖班日期
            //ListPaneField DistrictUpdateUnLockOrLockDate = new ListPaneField("局端鎖班/解鎖日期");
            //DistrictComment.GetVariable += delegate (object sender, GetVariableEventArgs e)
            //{
            //    if (_UDT_ClassLockDict.ContainsKey(e.Key))
            //    {
            //        // 當有鎖定再顯示
            //        if (_UDT_ClassLockDict[e.Key].isLock)
            //        {
            //            e.Value = _UDT_ClassLockDict[e.Key].DistrictComment;
            //        }
            //    }
            //};
            //K12.Presentation.NLDPanels.Class.AddListPaneField(DistrictUpdateUnLockOrLockDate);



            ListPaneField ClassLockSStudentCountField = new ListPaneField("特殊生人數");
            ClassLockSStudentCountField.GetVariable += delegate (object sender, GetVariableEventArgs e)
            {
                if (_ClassStudentDict.ContainsKey(e.Key))
                {
                    if (_ClassStudentDict.ContainsKey(e.Key))
                        e.Value = _ClassStudentDict[e.Key].ClassHStudentCount;
                }
            };
            K12.Presentation.NLDPanels.Class.AddListPaneField(ClassLockSStudentCountField);


            K12.Presentation.NLDPanels.Class.SelectedSourceChanged += delegate
            {
                // 檢查當有權限並只選一個班才可以使用
                K12.Presentation.NLDPanels.Class.ListPaneContexMenu["班級鎖定/解鎖"].Enable = UserAcl.Current["KH_HighConcern_ClassLock"].Executable && (K12.Presentation.NLDPanels.Class.SelectedSource.Count == 1);

                K12.Presentation.NLDPanels.Class.ListPaneContexMenu["取消鎖定申請(年級鎖班超過1/2)"].Enable = UserAcl.Current["KH_HighConcern_ClassLockAppling"].Executable && (K12.Presentation.NLDPanels.Class.SelectedSource.Count == 1);
            };

            K12.Presentation.NLDPanels.Class.SelectedSourceChanged += delegate
            {
                // 檢查當有權限並只選一個班才可以使用
                // K12.Presentation.NLDPanels.Class.ListPaneContexMenu["取消鎖定申請(年級鎖班超過1/2)"].Enable = UserAcl.Current["KH_HighConcern_ClassLockAppling"].Executable && (K12.Presentation.NLDPanels.Class.SelectedSource.Count == 1);
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
                mf2.SetMsg("將全部班級解鎖，按下「是」確認後，局端會留解鎖紀錄。局端和校端會留下解鎖記錄。\n 若學校班級已勾選「不自動解鎖」，則無法進行自動解鎖。");
                //if (FISCA.Presentation.Controls.MsgBox.Show("將全部班級解鎖，按下「是」確認後，局端會留解鎖紀錄。", "全部班級解鎖", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                if (mf2.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {

                    // 全部解鎖
                    UDTTransfer.UnlockAllClass();

                    // 紀錄班級名稱條件：不自動解鎖false，鎖定 true
                    List<string> ClassNameList = (from data in _UDT_ClassLockDict.Values where data.UnAutoUnlock == false && data.isLock == true select data.ClassName).ToList();

                    foreach (UDT_ClassLock data in _UDT_ClassLockDict.Values)
                    {
                        if (data.UnAutoUnlock == false && data.isLock == true)
                        {
                            Utility.SendData(data.ClassName, "", "", "解除鎖定班級", data.DateStr, data.Comment, data.DocNo, data.EDoc, data.ClassID, "", "");
                        }
                    }

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
                string cid = K12.Presentation.NLDPanels.Class.SelectedSource[0];
                // 檢查並取得班級鎖定
                UDT_ClassLock data = UDTTransfer.GetClassLockByClassID(cid);
                K12.Data.ClassRecord classRec = K12.Data.Class.SelectByID(cid);

                List<JHClassTagRecord> recList = JHClassTag.SelectByClassID(cid);

                bool Class_Has_Standard_Category = false;


                string grYear = "";
                string SecondPriorityClassName = "", ThridPriorityClassName = "";
                if (classRec.GradeYear.HasValue)
                    grYear = classRec.GradeYear.Value.ToString();

                Dictionary<string, int> classCot = new Dictionary<string, int>();

                List<KH_HighConcernCalc.ClassStudent> ClassStudentList = KH_HighConcernCalc.Calc.GetClassStudentList(grYear);
                int idx = 1;
                foreach (KH_HighConcernCalc.ClassStudent cs in ClassStudentList)
                {
                    classCot.Add(cs.ClassName, cs.ClassStudentCount);
                    if (idx == 2) // 第二順位
                        SecondPriorityClassName = cs.ClassName;

                    if (idx == 3)  // 第三順位
                        ThridPriorityClassName = cs.ClassName;
                    idx++;
                }

                //穎驊新增， 用來檢查，此班級 是否有高雄定義的標準班級分類， 其定義了十種標準子類別在 KH_HighConcern專案 Program 下面程式碼，
                // 定義了普通班、體育班、美術班、音樂班、舞蹈班、資優班、資源班、特教班、技藝專班、機構式非學校自學班，
                //2016/12 高雄局端，希望在所有班級鎖班之前，都必須要有標準"班級分類"，以利作業。

                foreach (var rec in recList)
                {
                    if (rec.Prefix == "班級分類")
                    {
                        Class_Has_Standard_Category = true;
                    }
                }



                // 當已被鎖定，問是否解鎖
                if (data.isLock)
                {
                    MsgForm mf1 = new MsgForm();
                    mf1.Text = "班級解鎖";
                    mf1.SetMsg("「班級解鎖」，按下「是」確認後，局端會留解鎖紀錄。");
                    //if (FISCA.Presentation.Controls.MsgBox.Show("「班級解鎖」，按下「是」確認後，局端會留解鎖紀錄。", "班級解鎖", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    if (mf1.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        // 已被鎖定解鎖
                        data.isLock = false;
                        data.UnAutoUnlock = false;
                        string errMsg = Utility.SendData(classRec.Name, grYear, "", "解除鎖定班級", data.DateStr, data.Comment, data.DocNo, data.EDoc, data.ClassID, "", "");
                        if (errMsg != "")
                            FISCA.Presentation.Controls.MsgBox.Show(errMsg);
                        else
                            FISCA.Presentation.Controls.MsgBox.Show("已解鎖");
                        // 解鎖清除鎖定備註
                        data.Comment = "";
                    }
                }
                else //班級鎖定
                {

                    if (Class_Has_Standard_Category)
                    {
                        string errMsg = "";
                        Boolean IsApplyLock = false;  //是否因年級鎖班數已經超過1/2數申請
                        // 未鎖定，問是否要鎖定                                         
                        // 編班委員會會議日期
                        string strDate = "";
                        string strComment = "";
                        string strDocNo = "";
                        string strEDoc = "";

                        // 申請鎖班視窗(未超過年級數1/2)
                        SendDataForm sendDataForm = new SendDataForm();

                        #region 蒐集畫面資料

                        //蒐集畫面資料
                        //strDate = sendDataForm.GetSendDate();
                        //strComment = sendDataForm.GetComment();
                        //strDocNo = sendDataForm.GetDocNo();
                        //strEDoc = sendDataForm.GetEDoc();

                        ////裝到Model
                        //data.ClassID = cid;
                        //data.ClassName = classRec.Name;
                        //data.Comment = strComment;
                        //data.DocNo = strDocNo;
                        //data.DateStr = strDate;
                        //data.EDoc = strEDoc;
                        //data.UnAutoUnlock = sendDataForm.GetNUnLock();
                        //data.isLock = true; ======>>依據後面判斷是否超過1/2鎖班
                        #endregion

                        if (sendDataForm.ShowDialog() == System.Windows.Forms.DialogResult.Yes) //當按下確定時
                        {
                            //todo
                            if (UDTTransfer.CheckIfOneHalf(cid)) //204班  當(鎖班數超過1/2)
                            {
                                FrmApplyLock frmApplyLock = new FrmApplyLock(); //1.詢問是否提出申請

                                IsApplyLock = frmApplyLock.ShowDialog() == DialogResult.Yes ? true : false;

                                if (IsApplyLock) //使用者確認提出申請
                                {

                                    #region 蒐集畫面資料

                                    //蒐集畫面資料
                                    strDate = sendDataForm.GetSendDate();
                                    strComment = sendDataForm.GetComment();
                                    strDocNo = sendDataForm.GetDocNo();
                                    strEDoc = sendDataForm.GetEDoc();

                                    //裝到Model
                                    data.ClassID = cid;
                                    data.ClassName = classRec.Name;
                                    data.Comment = strComment;
                                    data.DocNo = strDocNo;
                                    data.DateStr = strDate;
                                    data.EDoc = strEDoc;
                                    data.UnAutoUnlock = sendDataForm.GetNUnLock();
                                    //data.isLock = true; ======>>依據後面判斷是否超過1/2鎖班
                                    #endregion

                                    // data.LockAppling = true;   已經有status 識別 可以不用
                                    data.LockApplingStatus = ApplyStatus.鎖班申請中_鎖班數超過二分之一.ToString();
                                    data.isLock = false;

                                    //將資料送到局端 ( $school_log ) <申請也要> <直接鎖班也要>
                                    errMsg = Utility.SendData(classRec.Name, grYear, "", "鎖定班級", strDate, strComment, strDocNo, strEDoc, data.ClassID, SecondPriorityClassName, ThridPriorityClassName);

                                    //傳送檔案到局端 ( $upload_url ) <申請也要> <直接鎖班也要>
                                    Utility.UploadFile(data.ClassID, sendDataForm.GetBase64DataString(), sendDataForm.GetFileName());

                                    MsgBox.Show("鎖班申請已送出，仍需函報教育局並由局端線上審核!");
                                }
                                else //不要申請
                                {
                                    if (DialogResult.Yes == MsgBox.Show("確定取消申請?", MessageBoxButtons.YesNo))
                                    {
                                        frmApplyLock.Close();
                                        sendDataForm.Close();
                                        return;
                                    }
                                }
                            }
                            else//沒有超過1/2(正常流程)
                            {
                                //strDate = sdf.GetSendDate();
                                //strComment = sdf.GetComment();
                                //strDocNo = sdf.GetDocNo();
                                //strEDoc = sdf.GetEDoc();

                                MsgForm mf = new MsgForm(); //班級鎖定 跳出班級鎖定視窗
                                mf.Text = "班級鎖定";
                                mf.SetMsg("「班級鎖定」，按下「是」確認後，除集中式特殊班級，餘需函報教育局並由局端線上審核。");
                                //if (FISCA.Presentation.Controls.MsgBox.Show("「班級鎖定」，按下「是」確認後，除集中式特殊班級，餘需函報教育局並由局端線上審核。", "班級鎖定", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                                if (mf.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                                {
                                    // 寫入相對班級學生變動
                                    int icid = int.Parse(cid);
                                    // 班級資訊
                                    UDTTransfer.UpdateUDTClassSepcByClassID(icid, classRec.Name, data.Comment, strComment); //???  申請不做  之鎖班做
                                    #region 蒐集畫面資料

                                    //蒐集畫面資料
                                    strDate = sendDataForm.GetSendDate();
                                    strComment = sendDataForm.GetComment();
                                    strDocNo = sendDataForm.GetDocNo();
                                    strEDoc = sendDataForm.GetEDoc();

                                    //裝到Model
                                    data.ClassID = cid;
                                    data.ClassName = classRec.Name;
                                    data.Comment = strComment;
                                    data.DocNo = strDocNo;
                                    data.DateStr = strDate;
                                    data.EDoc = strEDoc;
                                    data.UnAutoUnlock = sendDataForm.GetNUnLock();
                                    //data.isLock = true; ======>>依據後面判斷是否超過1/2鎖班
                                    #endregion

                                    data.isLock = true;


                                    //將資料送到局端 ( $school_log ) <申請也要> <直接鎖班也要>
                                    errMsg = Utility.SendData(classRec.Name, grYear, "", "鎖定班級", strDate, strComment, strDocNo, strEDoc, data.ClassID, SecondPriorityClassName, ThridPriorityClassName);

                                    //傳送檔案到局端 ( $upload_url ) <申請也要> <直接鎖班也要>
                                    Utility.UploadFile(data.ClassID, sendDataForm.GetBase64DataString(), sendDataForm.GetFileName());

                                    //正常途徑鎖定
                                    if (errMsg != "")
                                        FISCA.Presentation.Controls.MsgBox.Show(errMsg);
                                    else
                                    {   // todo 
                                        if (IsApplyLock)
                                        {

                                        }
                                        else
                                        {
                                            if (data.UnAutoUnlock)
                                                MsgBox.Show("已鎖定(不自動解鎖)");
                                            else
                                                MsgBox.Show("已鎖定");
                                        }
                                    }

                                }
                                else 
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("鎖班前，請將本班級先加入'班級分類:'中任一類別", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                        return;
                    }
                }
                // 儲存 UDT //儲存置本地資料庫
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



            K12.Presentation.NLDPanels.Class.ListPaneContexMenu["取消鎖定申請(年級鎖班超過1/2)"].Click += delegate
            {
                string cid = K12.Presentation.NLDPanels.Class.SelectedSource[0];

                if (!UDTTransfer.CheckIsUnlockAppling(cid)) //非取消狀態
                {
                    MsgBox.Show("本班沒有目前無鎖定申請(超過年級鎖班數1/2)");
                    return;
                }

                if (DialogResult.Yes == MsgBox.Show("確定取消申請?", MessageBoxButtons.YesNo))
                {
                    try
                    {
                        UDTTransfer.CancelAppling(cid);
                        MsgBox.Show("已取消鎖班申請!");
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show($"取消申請發生錯誤! \n{ex.Message} \n {ex.StackTrace}");
                    }
                }
                _UDT_ClassLockDict = UDTTransfer.GetClassLockNameIDDict();
                ClassLockField.Reload();
                ClassLockCommentField.Reload();
                ClassLockStudentCountField.Reload();
                ClassLockSStudentCountField.Reload();
            };
        }

        /// <summary>
        /// 將資料打包送出給局端
        /// </summary>
        static void WrapDataAndSendToDistrict(UDT_ClassLock data, string classID, K12.Data.ClassRecord classRec, string strComment, string strDocNo)
        {


        }
        static void _bgLLoadUDT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }





        static void _bgLLoadUDT_DoWork(object sender, DoWorkEventArgs e)
        {
            UDTTransfer.CreateUDTTable();

            //FISCA.ServerModule.AutoManaged("http://module.ischool.com.tw/module/137/KHCentralOffice/udm.xml");

            //// 因為 UDT 調整，需要檢查不自動解鎖相對鎖定欄位是鎖定
            // List<UDT_ClassLock> classLockList = UDTTransfer.GetClassLocList();
            // if(classLockList.Count>0)
            // {
            //     foreach (UDT_ClassLock data in classLockList)
            //     {
            //         if(data.isLock == null)
            //         {
            //             if (data.UnAutoUnlock)
            //                 data.isLock = true;
            //         }                    
            //     }
            //     classLockList.SaveAll();
            // }


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

        /// <summary>
        /// 打包資料 for update 
        /// </summary>
        static void WrapClassLockData()
        {
            //data.ClassID = cid;
            //data.ClassName = classRec.Name;
            //data.Comment = strComment;
            //data.DocNo = strDocNo;
            //data.DateStr = strDate;
            //data.EDoc = strEDoc;
            //data.UnAutoUnlock = sdf.GetNUnLock();
            //data.isLock = true;

        }
    }
}

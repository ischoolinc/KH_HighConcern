using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using Campus.Message;
using System.ComponentModel;
using System.Xml.Linq;

namespace StudentClassItem_KH
{
    public class Program
    {
        //StartupPriority.FirstAsynchronized
        [MainMethod(StartupPriority.FirstAsynchronized)]
        public static void Main()
        {
            FISCA.InteractionService.RegisterAPI<IRewriteAPI_JH.IStudentClassDetailItemAPI>(new StudentClassItem());
                    
            ////註冊一個事件引發模組
            //EventHandler eh = FISCA.InteractionService.PublishEvent("KH_StudentClassItemContent");
            //eh(null, EventArgs.Empty);
        }



        [MainMethod()]
        public static void Start()
        {
            Catalog catalog01 = RoleAclSource.Instance["局端"]["功能按鈕"];
            catalog01.Add(new RibbonFeature("KH_HighConcern_SendStudentDataView", "檢視傳送局端備查紀錄"));
//            RibbonBarItem rbSendClassDataView = K12.Presentation.NLDPanels.Student.RibbonBarItems["其它"];
            FISCA.Presentation.RibbonBarItem rbSendClassDataView = FISCA.Presentation.MotherForm.RibbonBarItems["局端", "管理"];
            rbSendClassDataView["高雄市局端"]["檢視傳送局端備查紀錄"].Enable = UserAcl.Current["KH_HighConcern_SendStudentDataView"].Executable;
            rbSendClassDataView["高雄市局端"]["檢視傳送局端備查紀錄"].Click += delegate
            {
                SendDataView sdv = new SendDataView();
                sdv.ShowDialog();
            };

            List<string> CheckRData1 = new List<string>();
            // 載入自動編班審核狀態
            BackgroundWorker bgWorker = new BackgroundWorker();
            XElement _RspXML = null;
            bgWorker.RunWorkerAsync();
            bgWorker.DoWork += delegate {
                List<string> itemList = new List<string>();
                //itemList.Add("通過"); //  通過不顯示
                itemList.Add("不通過"); itemList.Add("待修正");
                _RspXML = Utility.QuerySendData(null, null, itemList);

                List<RspMsg> RspMsgList = new List<RspMsg>();

                RspMsgList = Utility.GetRspMsgList(_RspXML);
            
                // 填資料到畫面
                if (RspMsgList.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (RspMsg rm in RspMsgList)
                    {
                        sb.Clear();
                        sb.AppendLine("日期:" + rm.Date);
                        sb.AppendLine("動作:" + rm.Action);
                        sb.AppendLine("摘要:" + rm.GetContentString(false));
                        sb.AppendLine("審核結果:" + rm.Verify);
                        sb.AppendLine("局端備註:" + rm.Comment);
                                                
                        CheckRData1.Add(sb.ToString());
                    }
                }

            };

            bgWorker.RunWorkerCompleted += delegate {

                if (CheckRData1.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("局端備查紀錄已回傳：");
                    foreach (string str in CheckRData1)
                        sb.AppendLine(str);

                    CustomRecord cr = new CustomRecord();
                    cr.Title = "局端備查紀錄通知";
                    cr.Content = sb.ToString();
                    cr.Type = CrType.Type.Warning_Blue;

                    name n = new name();
                    n._messageTitle1 = "局端備查紀錄通知";
                    n._value1 = sb.ToString();
                    n.type = true;

                    IsViewForm_Open open = new IsViewForm_Open(n);
                    cr.OtherMore = open;

                    Campus.Message.MessageRobot.AddMessage(cr);
                }
            };
        }
    }

    public class name
    {
        public string _messageTitle1 { get; set; }

        public string _value1 { get; set; }

        public bool type { get; set; }
    }
}

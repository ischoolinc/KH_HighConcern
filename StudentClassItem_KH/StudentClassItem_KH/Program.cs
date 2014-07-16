using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using FISCA.Permission;
using FISCA.Presentation;

namespace StudentClassItem_KH
{
    public class Program
    {
        //StartupPriority.FirstAsynchronized
        [MainMethod()]
        public static void Main()
        {
            FISCA.InteractionService.RegisterAPI<IRewriteAPI_JH.IStudentClassDetailItemAPI>(new StudentClassItem());

            //註冊一個事件引發模組
            EventHandler eh = FISCA.InteractionService.PublishEvent("KH_StudentClassItemContent");
            eh(null, EventArgs.Empty);
            Catalog catalog01 = RoleAclSource.Instance["學生"]["功能按鈕"];
            catalog01.Add(new RibbonFeature("KH_HighConcern_SendStudentDataView", "檢視傳送局端備查紀錄"));
            RibbonBarItem rbSendClassDataView = K12.Presentation.NLDPanels.Student.RibbonBarItems["其它"];
            rbSendClassDataView["檢視傳送局端備查紀錄"].Enable = UserAcl.Current["KH_HighConcern_SendStudentDataView"].Executable;
            rbSendClassDataView["檢視傳送局端備查紀錄"].Click += delegate
            {
                SendDataView sdv = new SendDataView();
                sdv.ShowDialog();
            };
        }
    }
}

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
        [MainMethod(StartupPriority.FirstAsynchronized)]
        public static void Main()
        {
            FISCA.InteractionService.RegisterAPI<IRewriteAPI_JH.IStudentClassDetailItemAPI>(new StudentClassItem());

            //註冊一個事件引發模組
            EventHandler eh = FISCA.InteractionService.PublishEvent("KH_StudentClassItemContent");
            eh(null, EventArgs.Empty);

            //RibbonBarItem rbItem1 = K12.Presentation.NLDPanels.Student.RibbonBarItems["其它"];            
            //rbItem1["查詢傳送至局端紀錄"].Enable = true;
            //rbItem1["查詢傳送至局端紀錄"].Click += delegate {
            //    SendDataView sdv = new SendDataView();
            //    sdv.ShowDialog();
            
            //};
        }
    }
}

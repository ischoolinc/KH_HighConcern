using FISCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTransferStudentBrief_KH
{
    public class Program
    {
         [MainMethod(StartupPriority.FirstAsynchronized)]
        public static void Main()
        {
            FISCA.InteractionService.RegisterAPI<StudentTransferAPI.IStudentBriefBaseAPI>(new StudentTransferStudentBriefItem());

            ////註冊一個事件引發模組
            //EventHandler eh = FISCA.InteractionService.PublishEvent("KH_StudentTransferStudentBriefItem");
            //eh(null, EventArgs.Empty);
         }
    }
}

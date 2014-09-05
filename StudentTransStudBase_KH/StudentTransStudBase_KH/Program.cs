using FISCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Permission;
using FISCA.Presentation;

namespace StudentTransStudBase_KH
{   
    public class Program
    {
        [MainMethod(StartupPriority.FirstAsynchronized)]
        public static void Main()
        {
            FISCA.InteractionService.RegisterAPI<IRewriteAPI_JH.IStudentTransStudBaseAPI>(new StudentTransStudBaseItem());

            ////註冊一個事件引發模組
            //EventHandler eh = FISCA.InteractionService.PublishEvent("KH_StudentTransStudBase");
            //eh(null, EventArgs.Empty);

        }
    }
}

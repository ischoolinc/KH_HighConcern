using FISCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentChangeStatus_KH
{
    public class Program
    {
        [MainMethod()]
        public static void Main()
        {
            FISCA.InteractionService.RegisterAPI<Tagging.IStudentChangeStatusAPI>(new StudentChangeStatusBarBuilder());


            ////註冊一個事件引發模組
            //EventHandler eh = FISCA.InteractionService.PublishEvent("KH_StudentChangeStatus");
            //eh(null, EventArgs.Empty);
        }
    }
}

using FISCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentImportWizard_KH
{
    public class Program
    {
        [MainMethod(StartupPriority.FirstAsynchronized)]
        public static void Main()
        {
            FISCA.InteractionService.RegisterAPI<IRewriteAPI_JH.IStudentImportWizardAPI>(new StudentImportWizardItem());

            //註冊一個事件引發模組
            EventHandler eh = FISCA.InteractionService.PublishEvent("KH_StudentImportWizard");
            eh(null, EventArgs.Empty);
        }
    }
}

using FISCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddStudent_KH
{
    public class Program
    {
        [MainMethod(StartupPriority.FirstAsynchronized)]
        public static void Main()
        {
            FISCA.InteractionService.RegisterAPI<IRewriteAPI_JH.IStudentAddStudentAPI>(new StudentAddStudentItem());         
        }
    }
}

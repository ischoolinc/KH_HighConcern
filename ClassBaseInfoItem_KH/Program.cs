using FISCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassBaseInfoItem_KH
{
    public class Program
    {
        [MainMethod(StartupPriority.FirstAsynchronized)]
        public static void Main()
        {
            // 註冊
            FISCA.InteractionService.RegisterAPI<IRewriteAPI_JH.IClassBaseInfoItemAPI>(new ClassItem());
        }
    }
}

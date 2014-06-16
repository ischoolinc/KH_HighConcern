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

            List<string> _UDT_ClassLockList = new List<string>();


            ListPaneField ClassLockField = new ListPaneField("班級鎖定");
            ClassLockField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_UDT_ClassLockList.Contains(e.Key))
                {
                    e.Value = "鎖定";
                }
            };
            K12.Presentation.NLDPanels.Student.AddListPaneField(ClassLockField);

            // 當
            FISCA.InteractionService.SubscribeEvent("KH_HighConcern_ClassLock", (sender, args) =>
            {
                _UDT_ClassLockList = UDTTransfer.GetClassLockNameList();
                ClassLockField.Reload();            
            });
        }

        static void _bgLLoadUDT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        static void _bgLLoadUDT_DoWork(object sender, DoWorkEventArgs e)
        {
            UDTTransfer.CreateUDTTable();

        }


      
    }
}

﻿using Campus.DocumentValidator;
using FISCA;
using KH_HighConcern.DAO;
using KH_HighConcern.ImportExport.ValidationRule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FISCA.Permission;
using FISCA.Presentation;
using System.Windows.Forms;

namespace KH_HighConcern
{
    public class Program
    {
           static BackgroundWorker _bgLLoadUDT = new BackgroundWorker();
           [MainMethod()]
           public static void Main()
           {
               _bgLLoadUDT.DoWork+=_bgLLoadUDT_DoWork;
               _bgLLoadUDT.RunWorkerCompleted += _bgLLoadUDT_RunWorkerCompleted;
               _bgLLoadUDT.RunWorkerAsync();
               Dictionary<string, UDT_HighConcern> _HighConcernDict = new Dictionary<string, UDT_HighConcern>();

               _HighConcernDict = UDTTransfer.GetHighConcernDictAll();
               ListPaneField HighConcernField = new ListPaneField("高關懷特殊身份");
               HighConcernField.GetVariable += delegate(object sender, GetVariableEventArgs e)
               {
                   if (_HighConcernDict.ContainsKey(e.Key))
                   {
                       e.Value = "是";
                   }
               };
               K12.Presentation.NLDPanels.Student.AddListPaneField(HighConcernField);

               ListPaneField HighCountField = new ListPaneField("高關懷減免人數");
               HighCountField.GetVariable += delegate(object sender, GetVariableEventArgs e)
               {
                   if (_HighConcernDict.ContainsKey(e.Key))
                   {
                       e.Value = _HighConcernDict[e.Key].NumberReduce;
                   }
               };
               K12.Presentation.NLDPanels.Student.AddListPaneField(HighCountField);

               ListPaneField HighDocNoField = new ListPaneField("高關懷文號");
               HighDocNoField.GetVariable += delegate(object sender, GetVariableEventArgs e)
               {
                   if (_HighConcernDict.ContainsKey(e.Key))
                   {
                       e.Value = _HighConcernDict[e.Key].DocNo;
                   }
               };
               K12.Presentation.NLDPanels.Student.AddListPaneField(HighDocNoField);

               // 當高關懷特殊身份有更新
               FISCA.InteractionService.SubscribeEvent("KH_HighConcern_HighConcernContent", (sender, args) =>
               {
                   _HighConcernDict = UDTTransfer.GetHighConcernDictAll();
                   HighConcernField.Reload();
                   HighCountField.Reload();
                   HighDocNoField.Reload();
               });
           }

           static void _bgLLoadUDT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
           {

               // 匯入高關懷特殊身份
               Catalog catalog01 = RoleAclSource.Instance["學生"]["功能按鈕"];
               catalog01.Add(new RibbonFeature("KH_HighConcern_ImportHighConcern", "匯入高關懷特殊身份"));

               Catalog catalog02 = RoleAclSource.Instance["學生"]["資料項目"];
               catalog02.Add(new DetailItemFeature(typeof(DetailContent.HighConcernContent)));

               RibbonBarItem item01 = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"];
               item01["匯入"]["其它相關匯入"]["匯入高關懷特殊身份"].Enable = UserAcl.Current["KH_HighConcern_ImportHighConcern"].Executable;
               item01["匯入"]["其它相關匯入"]["匯入高關懷特殊身份"].Click += delegate {
                   if (FISCA.Presentation.Controls.MsgBox.Show("匯入高關懷學生，按下「是」確認後，需報局備查。", "匯入高關懷學生", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                   {
                       new ImportExport.ImportHighConcern().Execute();
                   }                   
               };


               // 資料項目-高關懷特殊身份
               FeatureAce UserPermission = FISCA.Permission.UserAcl.Current["KH_HighConcern_HighConcernContent"];
               if (UserPermission.Editable)
                   K12.Presentation.NLDPanels.Student.AddDetailBulider(new DetailBulider<DetailContent.HighConcernContent>());

           }

           static void _bgLLoadUDT_DoWork(object sender, DoWorkEventArgs e)
           {
               UDTTransfer.CreateUDTTable();

               #region 自訂驗證規則
               FactoryProvider.FieldFactory.Add(new FieldValidatorFactory());
               #endregion
           }
    }
}

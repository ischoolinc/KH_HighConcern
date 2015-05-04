using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using SmartSchool.API.PlugIn;

namespace KH_HighConcern.ImportExport
{
    /// <summary>
    /// 匯出高懷特殊身分
    /// </summary>
    public class ExportHighConcern : SmartSchool.API.PlugIn.Export.Exporter 
    {
        List<string> ExportItemList;

        public ExportHighConcern()
        {
            this.Image = null;
            this.Text = "匯出高關懷特殊身分";
            ExportItemList = new List<string>();
            ExportItemList.Add("高關懷特殊身分註記");
            ExportItemList.Add("減免人數");
            ExportItemList.Add("文號");
            ExportItemList.Add("相關證明文件網址");

        }

        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {
            wizard.ExportableFields.AddRange(ExportItemList);
            wizard.ExportPackage += delegate(object sender, SmartSchool.API.PlugIn.Export.ExportPackageEventArgs e)
           { 
           int RowCount = 0;

                Dictionary<string,DAO.UDT_HighConcern> HighConcernList = DAO.UDTTransfer.GetHighConcernDictByStudentIDList(e.List);

                foreach (DAO.UDT_HighConcern udd in HighConcernList.Values)
                {
                    RowData row = new RowData();
                    row.ID = udd.RefStudentID;

                    // 檢查是否匯出
                    bool chkExportData = false;
                    foreach (string field in e.ExportFields)
                    {
                        if (wizard.ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "高關懷特殊身分註記":
                                    if (udd.HighConcern)
                                    {
                                        chkExportData = true;
                                        row.Add(field, "是");
                                    }
                                    break;    
                                case "減免人數": row.Add(field, udd.NumberReduce.ToString()); break;
                                case "文號":
                                    row.Add(field, udd.DocNo);
                                    break;
                                case "相關證明文件網址":
                                    row.Add(field, udd.EDoc);
                                    break;
                            }
                        }

                    }
                    if(chkExportData)
                    {
                        RowCount++;
                        e.Items.Add(row);
                    }                    
                }
           };
           }
    }
}

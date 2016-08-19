using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using FISCA.Presentation.Controls;

namespace StudentClassItem_KH
{
    public partial class SendDataView : FISCA.Presentation.Controls.BaseForm
    {
        private BackgroundWorker _bgWork;
        private XElement _RspXML = null;
        private DateTime _BeginDate;
        private DateTime _EndDate;

        public SendDataView()
        {
            InitializeComponent();
            _bgWork = new BackgroundWorker();
            _bgWork.DoWork += _bgWork_DoWork;
            _bgWork.RunWorkerCompleted += _bgWork_RunWorkerCompleted;
        }

        void _bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnQuery.Enabled = true;
            LoadDataToDataGrid();
        }

        void _bgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            _RspXML = Utility.QuerySendData(_BeginDate, _EndDate, null);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (dtBeginDate.IsEmpty)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇開始日期");
                return;
            }

            if (dtEndDate.IsEmpty)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇結束日期");
                return;
            }

            _BeginDate = dtBeginDate.Value;
            _EndDate = dtEndDate.Value;
            ReloadSelectData();            

        }

        private void ReloadSelectData()
        {
            // 清空畫面資料
            dgData.Rows.Clear();
            btnQuery.Enabled = false;
            // 讀取資料
            _bgWork.RunWorkerAsync();
        }

        private void LoadDataToDataGrid()
        {
            /*

Response XML 解析代表：
Action:動作
Content:摘要(要判斷是否存在)
Content>Summary:摘要內容。(匯入更新班級 共 1 筆)
Detail:詳細內容(要判斷是否存在)
Detail>Student(是elements，foreach讀取資料)
Detail>Student>IDNumber:身分證
Detail>Student>ClassName:原班級
Detail>Student>StudentNumber:學號
Detail>Student>NewClassName:新班級
Detail>Student>SeatNo:座號
Detail>Student>GradeYear:年級
Comment:局端備註。
Verify:審核結果。(空白,t:通過,f:未通過)
DateTime:日期時間。
             * 
            */
            lblMsg.Text = "";

            List<RspMsg> RspMsgList = new List<RspMsg>();
            RspMsgList = Utility.GetRspMsgList(_RspXML);
           

            // 填資料到畫面
            if (RspMsgList.Count > 0)
            {
                foreach (RspMsg rm in RspMsgList)
                {
                    int rowIdx = dgData.Rows.Add();
                    dgData.Rows[rowIdx].Tag = rm;
                    dgData.Rows[rowIdx].Cells[colDate.Index].Value = rm.Date;
                    dgData.Rows[rowIdx].Cells[colAction.Index].Value = rm.Action;
                    // 摘要需解析
                    dgData.Rows[rowIdx].Cells[colContent.Index].Value = rm.GetContentString(false);

                    // 審查結果
                    dgData.Rows[rowIdx].Cells[colR1.Index].Value = rm.Verify;
                    // 局端備註
                    dgData.Rows[rowIdx].Cells[colR2.Index].Value = rm.Comment;

                    // 相關證明文件網址
                    dgData.Rows[rowIdx].Cells[colEDoc.Index].Value = rm.GetEDoc();
                }
                
            }

            lblMsg.Text = " 共 " + RspMsgList.Count + " 筆";
        }

    
        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        private void ExportData()
        {
            btnExport.Enabled = false;
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();
            int colIdx = 0;
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                wb.Worksheets[0].Cells[0, colIdx].PutValue(dgvc.HeaderText);
                colIdx++;
            }

            int rowIdx = 1;
            foreach (DataGridViewRow dgvr in dgData.Rows)
            {
                colIdx = 0;
                foreach (DataGridViewCell dgvc in dgvr.Cells)
                {
                    if (dgvc.Value != null)
                        wb.Worksheets[0].Cells[rowIdx, colIdx].PutValue(dgvc.Value.ToString());
                    colIdx++;
                }
                rowIdx++;
            }
            wb.Worksheets[0].AutoFitColumns();
            Utility.CompletedXls("匯出局端備查資料", wb);
            btnExport.Enabled = true;
        }

        private void SendDataView_Load(object sender, EventArgs e)
        {
            dtEndDate.Value = DateTime.Now;
            dtBeginDate.Value = DateTime.Now.AddDays(-7);
        }

        private void dgData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
      
        }

        private void dgData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgData.SelectedRows.Count > 0)
            {
                RspMsg data = dgData.SelectedRows[0].Tag as RspMsg;
                if (data != null)
                {

                    if (data.Action.Contains("匯入"))
                    {
                        SendDataView_s2 sdv2 = new SendDataView_s2(data);
                        sdv2.ShowDialog();
                    }
                    else
                    {
                        SendDataView_s1 sdvs1 = new SendDataView_s1(data);
                        sdvs1.ShowDialog();
                    }
                }
            }

        }

        private void dgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            // 處理相關文件連結
            string url="";
            if (e.RowIndex>=0 && e.ColumnIndex ==colEDoc.Index && dgData.Rows[e.RowIndex].Cells[colEDoc.Index].Value != null)
            {
                url = dgData.Rows[e.RowIndex].Cells[colEDoc.Index].Value.ToString();

                if(!string.IsNullOrEmpty(url))
                {
                    try
                    {
                        if (url.Contains("://"))
                        {
                            ProcessStartInfo info = new ProcessStartInfo(url);
                            Process.Start(info);
                        }
                        else
                        {
                            FISCA.Presentation.Controls.MsgBox.Show("網址不完整");
                        }
                    }
                    catch (Exception ex)
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("網址無法解析，" + ex.Message);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            List<string> actionStrList = new List<string>();
            actionStrList.Add("調整班級");
            actionStrList.Add("取消特殊身分");
            actionStrList.Add("鎖定班級");
            actionStrList.Add("解除鎖定班級");
            actionStrList.Add("變更特殊身分");

            // 只支援單筆修改
            if(dgData.SelectedRows.Count==1)
            {
                RspMsg rm = dgData.SelectedRows[0].Tag as RspMsg;

                if(rm !=null)
                {
                    bool chkReloadData = false;
                    
                    if(actionStrList.Contains(rm.Action))
                    {
                        // 只有審核是空白或是不通過才能修改
                        if (rm.Verify == "" || rm.Verify == "待修正")
                        {
                            EditSendData esd = new EditSendData();
                            esd.SetRspMessage(rm);
                            esd.ShowDialog();
                            chkReloadData = esd.GetChkReloadData();
                        }
                        else
                        {
                            MsgBox.Show("只能修改未審核或待修正。");
                        }
                    }else
                    {
                        MsgBox.Show("不需要填寫 相關證明文件網址。");
                    }
                    
                    // 重新整理
                    if(chkReloadData)
                        ReloadSelectData();
                }
            }
        }        
    }
}

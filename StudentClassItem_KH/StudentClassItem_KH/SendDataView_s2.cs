using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentClassItem_KH
{
    public partial class SendDataView_s2 : FISCA.Presentation.Controls.BaseForm
    {
        private RspMsg _RspMsg;

        public SendDataView_s2(RspMsg rspMsg)
        {
            InitializeComponent();
            _RspMsg = rspMsg;
        }

        private void SendDataView_s2_Load(object sender, EventArgs e)
        {
            LoadData();
            txtAction.ReadOnly = true;
            txtComment.ReadOnly = true;
            txtContent.ReadOnly = true;
            txtDate.ReadOnly = true;
            dgDetail.ReadOnly = true;
        }

        private void LoadData()
        {
            if (_RspMsg != null)
            {
                txtDate.Text = _RspMsg.Date.ToString();
                txtAction.Text = _RspMsg.Action;
                //txtComment.Text = _RspMsg.Comment;
                txtComment.Text = _RspMsg.GetComment();
                txtContent.Text = _RspMsg.GetContentString(true);

                if (_RspMsg.Detail.Count > 0)
                {
                    DataTable dt = new DataTable();
                    if (_RspMsg.Action.Contains("新生") || _RspMsg.Action.Contains("特殊"))
                    {
                        dt.Columns.Add("身分證");
                        dt.Columns.Add("學號");
                        dt.Columns.Add("姓名");
                        //dt.Columns.Add("年級");
                        dt.Columns.Add("班級");
                        dt.Columns.Add("座號");

                        foreach (RspStud rs in _RspMsg.Detail)
                        {
                            DataRow dr = dt.NewRow();
                            dr["身分證"] = rs.IDNumber;
                            dr["學號"] = rs.StudentNumber;
                            dr["姓名"] = rs.Name;
                            //dr["年級"] = rs.GradeYear;
                            dr["班級"] = rs.ClassName;
                            dr["座號"] = rs.SeatNo;
                            dt.Rows.Add(dr);
                        }

                    }

                    if (_RspMsg.Action.Contains("更新"))
                    {
                        dt.Columns.Add("身分證");
                        dt.Columns.Add("學號");
                        dt.Columns.Add("姓名");
                        //dt.Columns.Add("年級");
                        dt.Columns.Add("班級");
                        dt.Columns.Add("新班級");
                        //dt.Columns.Add("理由");

                        foreach (RspStud rs in _RspMsg.Detail)
                        {
                            DataRow dr = dt.NewRow();
                            dr["身分證"] = rs.IDNumber;
                            dr["學號"] = rs.StudentNumber;
                            dr["姓名"] = rs.Name;
                            //dr["年級"] = rs.GradeYear;
                            dr["班級"] = rs.ClassName;
                            dr["新班級"] = rs.NewClassName;
                            //dr["理由"] = rs.Reason;
                            dt.Rows.Add(dr);
                        }
                        
                    }

                    dgDetail.DataSource = dt;
                }            
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();

            wb.Worksheets[0].Cells[0, 0].PutValue("日期");
            wb.Worksheets[0].Cells[0, 1].PutValue("動作");
            wb.Worksheets[0].Cells[0, 2].PutValue("備註");
            wb.Worksheets[0].Cells[0, 3].PutValue("摘要內容");
            int colIdx = 4;
            foreach (DataGridViewColumn dgvc in dgDetail.Columns)
            {
                wb.Worksheets[0].Cells[0, colIdx].PutValue(dgvc.HeaderText);
                colIdx++;
            }

            int rowIdx = 1;
            foreach (DataGridViewRow dgvr in dgDetail.Rows)
            {
                wb.Worksheets[0].Cells[rowIdx, 0].PutValue(txtDate.Text);
                wb.Worksheets[0].Cells[rowIdx, 1].PutValue(txtAction.Text);
                wb.Worksheets[0].Cells[rowIdx, 2].PutValue(txtComment.Text);
                wb.Worksheets[0].Cells[rowIdx, 3].PutValue(txtContent.Text);

                colIdx = 4;
                foreach (DataGridViewCell dgvc in dgvr.Cells)
                {
                    if (dgvc.Value != null)
                        wb.Worksheets[0].Cells[rowIdx, colIdx].PutValue(dgvc.Value.ToString());
                    colIdx++;
                }
                rowIdx++;
            }
            wb.Worksheets[0].AutoFitColumns();
            Utility.CompletedXls("匯出局端備查資料-詳細內容", wb);
            btnExport.Enabled = true;
        }

        private void dgDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

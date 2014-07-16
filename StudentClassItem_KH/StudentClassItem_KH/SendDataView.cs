using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

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
            _RspXML = Utility.QuerySendData(_BeginDate, _EndDate);
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

            // 清空畫面資料
            dgData.Rows.Clear();
            btnQuery.Enabled = false;
            // 讀取資料
            _bgWork.RunWorkerAsync();

        }

        private void LoadDataToDataGrid()
        {
            lblMsg.Text = "";
            int rowCot = 0;
            if (_RspXML != null)
            {
                if (_RspXML.Element("Body") != null)
                    if (_RspXML.Element("Body").Element("Response") != null)
                    {
                        foreach (XElement elm in _RspXML.Element("Body").Element("Response").Elements("SchoolLog"))
                        {
                            if (elm.Element("Action").Value != "鎖定班級" && elm.Element("Action").Value != "解除鎖定班級")
                            {
                                int rowIdx = dgData.Rows.Add();

                                if (elm.Element("Action") != null)
                                    dgData.Rows[rowIdx].Cells[colReason.Index].Value = elm.Element("Action").Value;

                                if (elm.Element("Content") != null)
                                {
                                    if (elm.Element("Content").Element("GradeYear") != null)
                                        dgData.Rows[rowIdx].Cells[colGryear.Index].Value = elm.Element("Content").Element("GradeYear").Value;

                                    if (elm.Element("Content").Element("ClassName") != null)
                                        dgData.Rows[rowIdx].Cells[colClassName.Index].Value = elm.Element("Content").Element("ClassName").Value;

                                    if (elm.Element("Content").Element("ScheduleClassDate") != null)
                                    {
                                        DateTime dd;
                                        if (DateTime.TryParse(elm.Element("Content").Element("ScheduleClassDate").Value, out dd))
                                            dgData.Rows[rowIdx].Cells[colScDate.Index].Value = dd;
                                    }
                                }
                                if (elm.Element("Timestamp") != null)
                                    dgData.Rows[rowIdx].Cells[colSendDate.Index].Value = DateTime.Parse(elm.Element("Timestamp").Value);

                                rowCot++;
                            }
                        }

                    }
            }
            lblMsg.Text = " 共 " + rowCot + " 筆";
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
    }
}

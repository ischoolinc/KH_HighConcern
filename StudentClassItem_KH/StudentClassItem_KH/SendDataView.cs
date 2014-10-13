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

            List<RspMsg> RspMsgList = new List<RspMsg> ();
              
            int rowCot = 0;
            if (_RspXML != null)
            {
                if (_RspXML.Element("Body") != null)
                    if (_RspXML.Element("Body").Element("Response") != null)
                    {
                        foreach (XElement elm in _RspXML.Element("Body").Element("Response").Elements("SchoolLog"))
                        {
                            RspMsg rm = new RspMsg ();

                                if (elm.Element("Action") != null)
                                     rm.Action= elm.Element("Action").Value;

                                if (elm.Element("Comment") != null)
                                    rm.Comment = elm.Element("Comment").Value;

                                if (elm.Element("isVerify") != null)
                                {
                                    rm.Verify = elm.Element("isVerify").Value;

                                    //if (rm.Verify.Trim() == "t")
                                    //    rm.Verify = "通過";
                                    //else if (rm.Verify.Trim() == "f")
                                    //    rm.Verify = "未通過";
                                    //else
                                    //    rm.Verify = "";
                                }

                                if (elm.Element("Content") != null)
                                {
                                    XElement xmlContent = elm.Element("Content");

                                    if (xmlContent.Element("GradeYear") != null)
                                        rm.Content.Add("GradeYear", xmlContent.Element("GradeYear").Value);

                                    if (xmlContent.Element("IDNumber") != null)
                                        rm.Content.Add("IDNumber", xmlContent.Element("IDNumber").Value);

                                    if (xmlContent.Element("StudentNumber") != null)
                                        rm.Content.Add("StudentNumber", xmlContent.Element("StudentNumber").Value);

                                    if (xmlContent.Element("StudentName") != null)
                                        rm.Content.Add("StudentName", xmlContent.Element("StudentName").Value);

                                    if (xmlContent.Element("ClassName") != null)
                                        rm.Content.Add("ClassName", xmlContent.Element("ClassName").Value);

                                    if (xmlContent.Element("NewClassName") != null)
                                        rm.Content.Add("NewClassName", xmlContent.Element("NewClassName").Value);

                                    if (xmlContent.Element("SeatNo") != null)
                                        rm.Content.Add("SeatNo", xmlContent.Element("SeatNo").Value);

                                    if (xmlContent.Element("ScheduleClassDate") != null)
                                        rm.Content.Add("ScheduleClassDate", xmlContent.Element("ScheduleClassDate").Value);

                                    if (xmlContent.Element("Reason") != null)
                                        rm.Content.Add("Reason", xmlContent.Element("Reason").Value);

                                    if (xmlContent.Element("FirstPriorityClassName") != null)
                                        rm.Content.Add("FirstPriorityClassName", xmlContent.Element("FirstPriorityClassName").Value);

                                    if (xmlContent.Element("Summary") != null)
                                        rm.Content.Add("Summary", xmlContent.Element("Summary").Value);

                                    if (xmlContent.Element("Comment") != null)
                                        rm.Content.Add("Comment", xmlContent.Element("Comment").Value);

                                    if (xmlContent.Element("DocNo") != null)
                                        rm.Content.Add("DocNo", xmlContent.Element("DocNo").Value);

                                    if (xmlContent.Element("NumberReduce") != null)
                                        rm.Content.Add("NumberReduce", xmlContent.Element("NumberReduce").Value);

                                    if (xmlContent.Element("StudentStatus") != null)
                                        rm.Content.Add("StudentStatus", xmlContent.Element("StudentStatus").Value);

                                    if (xmlContent.Element("NewStudentStatus") != null)
                                        rm.Content.Add("NewStudentStatus", xmlContent.Element("NewStudentStatus").Value);

                                    if (xmlContent.Element("EDoc") != null)
                                        rm.Content.Add("EDoc", xmlContent.Element("EDoc").Value);
                                }

                                // 詳細內容    
                                if (elm.Element("Detail") != null)
                                {
                                    foreach (XElement elms1 in elm.Element("Detail").Elements("Student"))
                                    {
                                        RspStud rs = new RspStud();

                                        if (elms1.Element("IDNumber") != null)
                                            rs.IDNumber = elms1.Element("IDNumber").Value;

                                        if (elms1.Element("ClassName") != null)
                                            rs.ClassName = elms1.Element("ClassName").Value;

                                        if (elms1.Element("StudentNumber") != null)
                                            rs.StudentNumber = elms1.Element("StudentNumber").Value;

                                        if (elms1.Element("NewClassName") != null)
                                            rs.NewClassName = elms1.Element("NewClassName").Value;

                                        if (elms1.Element("SeatNo") != null)
                                            rs.SeatNo = elms1.Element("SeatNo").Value;

                                        if (elms1.Element("GradeYear") != null)
                                            rs.GradeYear = elms1.Element("GradeYear").Value;

                                        if (elms1.Element("StudentName") != null)
                                            rs.Name = elms1.Element("StudentName").Value;


                                        if (elms1.Element("Reason") != null)
                                            rs.Reason = elms1.Element("Reason").Value;

                                        if (elms1.Element("StudentStatus") != null)
                                            rs.Status = elms1.Element("StudentStatus").Value;

                                        if (elms1.Element("NewStudentStatus") != null)
                                            rs.NewStatus = elms1.Element("NewStudentStatus").Value;

                                        if (elms1.Element("EDoc") != null)
                                            rs.EDoc = elms1.Element("EDoc").Value;

                                        rm.Detail.Add(rs);
                                    }
                                }

                                if (elm.Element("Timestamp") != null)
                                    rm.Date = DateTime.Parse(elm.Element("Timestamp").Value);

                                RspMsgList.Add(rm);
                                rowCot++;                            
                        }

                    }
            }

            // 依日期排序 日期新到舊
            RspMsgList = (from data in RspMsgList orderby data.Date descending select data).ToList();

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
            if (e.ColumnIndex ==colEDoc.Index && dgData.Rows[e.RowIndex].Cells[colEDoc.Index].Value != null)
            {
                url = dgData.Rows[e.RowIndex].Cells[colEDoc.Index].Value.ToString();

                if(!string.IsNullOrEmpty(url))
                {
                    ProcessStartInfo info = new ProcessStartInfo (url);
                    Process.Start(info);
                }
            }
        }

        
    }
}

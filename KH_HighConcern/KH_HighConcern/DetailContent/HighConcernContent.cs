using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KH_HighConcern.DAO;
using Campus.Windows;

namespace KH_HighConcern.DetailContent
{
     [FISCA.Permission.FeatureCode("KH_HighConcern_HighConcernContent", "高關懷特殊身份")]
    public partial class HighConcernContent : FISCA.Presentation.DetailContent
    {
        EventHandler eh;
        string EventCode = "KH_HighConcern_HighConcernContent";

         Dictionary<string,UDT_HighConcern> _HighConcernDict;
         List<string> sidList;
         BackgroundWorker _bgWorker;
         private ChangeListener _ChangeListener;
         K12.Data.StudentRecord _StudRec;
         bool _isBusy = false;
         ErrorProvider _errorP;
        public HighConcernContent()
        {
            _HighConcernDict = new Dictionary<string, UDT_HighConcern>();
            _ChangeListener = new ChangeListener();
            sidList = new List<string>();
            _errorP = new ErrorProvider();
            InitializeComponent();            
            this.Group = "高關懷特殊身份";
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;

            // 加入控制項變動檢查            
            _ChangeListener.Add(new CheckBoxXSource(chkHighConcern));
            _ChangeListener.Add(new TextBoxSource(txtCount));
            _ChangeListener.Add(new TextBoxSource(txtDocNo));
            _ChangeListener.StatusChanged += _ChangeListener_StatusChanged;
            //啟動更新事件
            eh = FISCA.InteractionService.PublishEvent(EventCode);
        }

        void _ChangeListener_StatusChanged(object sender, ChangeEventArgs e)
        {
            this.CancelButtonVisible = (e.Status == ValueStatus.Dirty);
            this.SaveButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isBusy)
            {
                _isBusy = false;
                _bgWorker.RunWorkerAsync();
                return;              
            }
            LoadData();
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {            
            _HighConcernDict = UDTTransfer.GetHighConcernDictByStudentIDList(sidList);
            _StudRec = K12.Data.Student.SelectByID(PrimaryKey);
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;
            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;
            sidList.Clear();
            sidList.Add(PrimaryKey);
            _BGRun();
        }

        private void LoadData()
        {
            _ChangeListener.SuspendListen();

            chkHighConcern.Checked = false;
            txtCount.Text = "";
            txtDocNo.Text = "";
            if (_HighConcernDict.Count > 0)
            {
                chkHighConcern.Checked = _HighConcernDict[PrimaryKey].HighConcern;
                txtCount.Text = _HighConcernDict[PrimaryKey].NumberReduce.ToString();
                txtDocNo.Text = _HighConcernDict[PrimaryKey].DocNo;
            }

            _ChangeListener.Reset();
            _ChangeListener.ResumeListen();
            this.Loading = false;
        }

        private void _BGRun()
        {
            if (_bgWorker.IsBusy)
                _isBusy = true;
            else
                _bgWorker.RunWorkerAsync();
        }

        protected override void OnCancelButtonClick(EventArgs e)
        {
            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;
            LoadData();
        }

        private bool chkData()
        {
            bool retVal = false;
            int bb;
            if (int.TryParse(txtCount.Text, out bb))
            {
                if(bb>=0 && bb<10)
                    retVal = true;
                else
                    _errorP.SetError(txtCount, "減免人數必須介於0~9");
            }
            else
            {
                _errorP.SetError(txtCount, "減免人數必須整數");
            }

            if (txtDocNo.Text.Trim() == "")
                _errorP.SetError(txtDocNo, "文號必填");
            return retVal;
        }

        protected override void OnSaveButtonClick(EventArgs e)
        {
            if (chkData())
            {
                int bb;
                int.TryParse(txtCount.Text,out bb);

                // 再次確認畫面
                if (FISCA.Presentation.Controls.MsgBox.Show("設定是特殊生或高關懷學生，按下「是」確認後，需報局備查。", "高關懷學生", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    string IDNumber = "", StudentNumber = "", StudentName = "", ClassName = "", SeatNo = "", NumberReduce = "", DocNo = "";

                    IDNumber = _StudRec.IDNumber;
                    StudentNumber = _StudRec.StudentNumber;
                    StudentName = _StudRec.Name;
                    if (_StudRec.SeatNo.HasValue)
                        SeatNo = _StudRec.SeatNo.Value.ToString();
                    if (_StudRec.Class != null)
                        ClassName = _StudRec.Class.Name;
                    NumberReduce = bb.ToString();
                    DocNo = txtDocNo.Text;

                    // 檢查當已是高關懷
                    if (_HighConcernDict.ContainsKey(PrimaryKey))
                    {
                        // 有勾選更新人數，沒有勾選刪除，因為沒存在必要
                        if (chkHighConcern.Checked)
                        {
                            
                            _HighConcernDict[PrimaryKey].NumberReduce = bb;
                            _HighConcernDict[PrimaryKey].DocNo = txtDocNo.Text;
                        }
                        else
                        {
                            txtCount.Text = "";
                            txtDocNo.Text = "";
                            _HighConcernDict[PrimaryKey].Deleted = true;
                        }
                        _HighConcernDict[PrimaryKey].Save();

                    }
                    else
                    {
                        UDT_HighConcern newData = new UDT_HighConcern();
                        newData.StudentNumber = StudentNumber;
                        newData.SeatNo = SeatNo;
                        newData.ClassName = ClassName;
                        newData.DocNo = txtDocNo.Text;
                        newData.RefStudentID = PrimaryKey;
                        newData.HighConcern = true;
                        newData.NumberReduce = bb;
                        newData.DocNo = txtDocNo.Text;
                        newData.Save();
                    }
                    // 傳送至局端
                    string errMsg = Utility.SendData("高關懷學生", IDNumber, StudentNumber, StudentName, ClassName, SeatNo, DocNo, NumberReduce);
                    if (errMsg != "")
                    {
                        FISCA.Presentation.Controls.MsgBox.Show(errMsg);
                    }
                }
                this.CancelButtonVisible = false;
                this.SaveButtonVisible = false;                
                eh(this, EventArgs.Empty);
            }
        }

        private void txtCount_TextChanged(object sender, EventArgs e)
        {
            _errorP.SetError(txtCount, "");
            if (txtCount.Text != "")
                chkHighConcern.Checked = true;
        }

        private void txtDocNo_TextChanged(object sender, EventArgs e)
        {
            _errorP.SetError(txtDocNo, "");
            if (txtDocNo.Text != "")
                chkHighConcern.Checked = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JHSchool;
using Campus.Windows;
using FISCA.Presentation;
using JHSchool.Data;
using System.Xml.Linq;

namespace StudentClassItem_KH
{
    [FISCA.Permission.FeatureCode("JHSchool.Student.Detail0090", "班級資訊")]
    public partial class StudentClassItemContent : FISCA.Presentation.DetailContent
    {
        

        private ChangeListener DataListener { get; set; }        
        private string _DefaultClassName;
        private string _DefaultSeatNo;
        private string _DefaultStudNum;
        private ErrorProvider Errors { get; set; }
        private Dictionary<string, string> _ClassNameIDDic;
        private JHSchool.Data.JHStudentRecord objStudent;
        private List<JHSchool.Data.JHClassRecord> _AllClassRecs;
        private List<int> _ClassSeatNoList;
        private bool isBwBusy = false;
        private BackgroundWorker BGWork;
        
        private List<JHSchool.Data.JHStudentRecord> _studRecList;
        private string tmpClassName = "";
        PermRecLogProcess prlp;


        public StudentClassItemContent()
        {            
            InitializeComponent();
            Group = "班級資訊";
        }

        private void StudentClassItemContent_Load(object sender, EventArgs e)
        {
            Errors = new ErrorProvider();
            _ClassNameIDDic = new Dictionary<string, string>();
            _ClassSeatNoList = new List<int>();

            JHSchool.Data.JHStudent.AfterChange += new EventHandler<K12.Data.DataChangedEventArgs>(JHStudent_AfterChange);

            objStudent = JHSchool.Data.JHStudent.SelectByID(PrimaryKey);
            _AllClassRecs = JHSchool.Data.JHClass.SelectAll();
            
            _studRecList = new List<JHSchool.Data.JHStudentRecord>();
            BGWork = new BackgroundWorker();
            BGWork.DoWork += new DoWorkEventHandler(BGWork_DoWork);
            BGWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWork_RunWorkerCompleted);

            DataListener = new ChangeListener();
            DataListener.Add(new TextBoxSource(txtStudentNumber));
            DataListener.StatusChanged += new EventHandler<ChangeEventArgs>(ValueManager_StatusChanged);
            prlp = new PermRecLogProcess();

            if (!string.IsNullOrEmpty(PrimaryKey))
                BGWork.RunWorkerAsync();

            Disposed += new EventHandler(ClassItem_Disposed);
        }

        void ClassItem_Disposed(object sender, EventArgs e)
        {
            JHStudent.AfterChange -= new EventHandler<K12.Data.DataChangedEventArgs>(JHStudent_AfterChange);
        }

        void JHStudent_AfterChange(object sender, K12.Data.DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, K12.Data.DataChangedEventArgs>(JHStudent_AfterChange), sender, e);
            }
            else
            {
                if (this.PrimaryKey != "")
                {
                    if (!BGWork.IsBusy)
                        BGWork.RunWorkerAsync();
                }
            }
        }

        void BGWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isBwBusy)
            {
                isBwBusy = false;
                BGWork.RunWorkerAsync();
                return;
            }
            reloadChkdData();
        }

        void BGWork_DoWork(object sender, DoWorkEventArgs e)
        {
            objStudent = JHSchool.Data.JHStudent.SelectByID(PrimaryKey);
        }

        private void ValueManager_StatusChanged(object sender, ChangeEventArgs e)
        {

            if (FISCA.Permission.UserAcl.Current[GetType()].Editable)
                SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            else
                SaveButtonVisible = false;

            CancelButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        private void reloadChkdData()
        {
            DataListener.SuspendListen();

            if (objStudent.Class != null)
            {
                lblClassName.Text = objStudent.Class.Name;
                
                this._DefaultClassName = objStudent.Class.Name;
            }
            else
                lblClassName.Text = string.Empty;


            lblSeatNo.Text = string.Empty;
            this._DefaultSeatNo = string.Empty;

            // 當有座號
            if (objStudent.SeatNo.HasValue)
                if (objStudent.SeatNo.Value > 0)
                {
                    string strSeatNo = objStudent.SeatNo.Value.ToString();
                    lblSeatNo.Text = strSeatNo;
                    this._DefaultSeatNo = strSeatNo;
                }


            // 當有學號
            if (string.IsNullOrEmpty(objStudent.StudentNumber))
            {
                this._DefaultStudNum = string.Empty;
                txtStudentNumber.Text = string.Empty;
            }
            else
            {
                txtStudentNumber.Text = objStudent.StudentNumber;
                this._DefaultStudNum = objStudent.StudentNumber;
            }

            prlp.SetBeforeSaveText("班級", lblClassName.Text);
            prlp.SetBeforeSaveText("座號", lblSeatNo.Text);
            prlp.SetBeforeSaveText("學號", txtStudentNumber.Text);

            tmpClassName = lblClassName.Text;        

            DataListener.Reset();
            DataListener.ResumeListen();
            this.Loading = false;
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            lblClassName.Text = "";
            lblSeatNo.Text = "";
            txtStudentNumber.Text = "";
            this.Loading = true;
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            if (BGWork.IsBusy)
                isBwBusy = true;
            else
                BGWork.RunWorkerAsync();

        }
        #region IDetailBulider 成員

        public DetailContent GetContent()
        {
            return new StudentClassItemContent();
        }

        #endregion
                

        protected override void OnSaveButtonClick(EventArgs e)
        {
            Errors.Clear();

            objStudent = JHSchool.Data.JHStudent.SelectByID(PrimaryKey);
                  

            // 檢查學號是否重複
            if (txtStudentNumber.Text != this._DefaultStudNum)
            {
                // 判斷是否是空
                if (string.IsNullOrEmpty(txtStudentNumber.Text))
                    objStudent.StudentNumber = "";
                else
                {
                    // 取得目前學生狀態
                    JHStudentRecord.StudentStatus studtStatus = JHSchool.Data.JHStudent.SelectByID(PrimaryKey).Status;

                    List<string> checkData = new List<string>();
                    // 同狀態下學號不能重複
                    foreach (JHSchool.Data.JHStudentRecord studRec in JHSchool.Data.JHStudent.SelectAll())
                        if (studRec.Status == studtStatus)
                            checkData.Add(studRec.StudentNumber);

                    if (checkData.Contains(txtStudentNumber.Text))
                    {
                        Errors.SetError(txtStudentNumber, "學號重複!");
                        return;
                    }
                    objStudent.StudentNumber = txtStudentNumber.Text;
                }
            }

      
            JHStudent.Update(objStudent);

            SaveButtonVisible = false;
            CancelButtonVisible = false;

            prlp.SetAfterSaveText("班級", lblClassName.Text);
            prlp.SetAfterSaveText("座號", lblSeatNo.Text);
            prlp.SetAfterSaveText("學號", txtStudentNumber.Text);
            prlp.SetActionBy("學籍", "學生班級資訊");
            prlp.SetAction("修改學生班級資訊");
            prlp.SetDescTitle("學生姓名:" + objStudent.Name + ",學號:" + objStudent.StudentNumber + ",");

            prlp.SaveLog("", "", "student", PrimaryKey);


            this._DefaultClassName = lblClassName.Text;
            this._DefaultSeatNo = lblSeatNo.Text;
            this._DefaultStudNum = txtStudentNumber.Text;
      //      Student.Instance.SyncDataBackground(PrimaryKey);

            reloadChkdData();
        }

        protected override void OnCancelButtonClick(EventArgs e)
        {
            lblClassName.Text = this._DefaultClassName;
            lblSeatNo.Text = this._DefaultSeatNo;
            txtStudentNumber.Text = this._DefaultStudNum;
            SaveButtonVisible = false;
            CancelButtonVisible = false;

        }
           
        private void lnkSend_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetClassNameSeatNoForm scnsf = new SetClassNameSeatNoForm();
            string gradeYear="";
            string oldClassName = "";
            if (objStudent.Class != null)
            {
                oldClassName = objStudent.Class.Name;
                if (objStudent.Class.GradeYear.HasValue)
                    gradeYear = objStudent.Class.GradeYear.Value.ToString();
            }
            Dictionary<string, int> ClassNameDict = Utility.GetClassNameDictByGradeYear(gradeYear);
            _ClassNameIDDic = Utility.GetClassNameIDDictByGradeYear(gradeYear);
            scnsf.SetClassNameItems(ClassNameDict.Keys.ToList());
            scnsf.SetClassName(lblClassName.Text);
            scnsf.SetSeatNo(lblSeatNo.Text);
            scnsf.SetOldClassName(oldClassName);
            if (scnsf.ShowDialog() == DialogResult.OK)
            {
                string className = scnsf.GetClassName();
                int seatNo;


                // 修改學生班、座
                if (_ClassNameIDDic.ContainsKey(className))
                {
                    objStudent.RefClassID = _ClassNameIDDic[className];
                    if (int.TryParse(scnsf.GetSeatNo(), out seatNo))
                        objStudent.SeatNo = seatNo;
                    else
                        objStudent.SeatNo = null;

                    // 傳送至局端
                    string errMsg=Utility.SendData("編班", objStudent.IDNumber, objStudent.StudentNumber, objStudent.Name, gradeYear, oldClassName, scnsf.GetSeatNo(), scnsf.GetClassName(), scnsf.GetMettingDate(), scnsf.GetMemo());
                    if (errMsg != "")
                        FISCA.Presentation.Controls.MsgBox.Show(errMsg);

                    // 更新學生資料
                    K12.Data.Student.Update(objStudent);

                    prlp.SetAfterSaveText("班級", lblClassName.Text);
                    prlp.SetAfterSaveText("座號", lblSeatNo.Text);
                    prlp.SetAfterSaveText("學號", txtStudentNumber.Text);
                    prlp.SetActionBy("學籍", "學生班級資訊");
                    prlp.SetAction("修改學生班級資訊");
                    prlp.SetDescTitle("學生姓名:" + objStudent.Name + ",學號:" + objStudent.StudentNumber + ",");

                    prlp.SaveLog("", "", "student", PrimaryKey);

                    Student.Instance.SyncDataBackground(PrimaryKey);
                    
                }
            }
        }
    }
}

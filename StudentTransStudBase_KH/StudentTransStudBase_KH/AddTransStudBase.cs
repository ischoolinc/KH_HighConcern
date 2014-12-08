using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Framework;
using JHPermrec.UpdateRecord.Transfer;
using JHPermrec.UpdateRecord;

namespace StudentTransStudBase_KH
{
    public partial class AddTransStudBase : FISCA.Presentation.Controls.BaseForm,IRewriteAPI_JH.ITransStudBase
    {
        public enum AddTransStudStatus { Added, Modify }

        //private StudentRecord student;

        private JHSchool.Data.JHStudentRecord  _student;
        private AddTransStudStatus _status;
        private JHSchool.Data.JHPhoneRecord _StudentPhone;


        private EnhancedErrorProvider Errors { get; set; }

        public void setStudent_Status(JHSchool.Data.JHStudentRecord student,AddTransStudStatus status)
        {
            _student = student;
            _status = status;
            Errors = new EnhancedErrorProvider();
            //           Errors.Icon = Properties.Resources.warning;
            
            cboNewNationality.Items.AddRange(DALTransfer1.GetNationalities().ToArray());
            //cboClass.Items.Add(new KeyValuePair<string, string>("", "<空白>"));

            // 

            //foreach (KeyValuePair<string, string> classItem in Utility.GetClassNameIDDict())
            //{
            //    cboClass.Items.Add(classItem);
            //}


            //cboClass.DisplayMember = "Value";
            //cboClass.ValueMember = "Key";

            //cboClass.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cboClass.AutoCompleteSource = AutoCompleteSource.ListItems;

            cboNewGender.Items.AddRange(new string[] { "男", "女" });

            if (student != null)
            {

                //把資料填入各項控制項當中
                txtName.Text = txtNewName.Text = _student.Name;
                txtSSN.Text = txtNewSSN.Text = _student.IDNumber;
                if (_student.Birthday.HasValue)
                    dtBirthDate.Text = dtNewBirthday.Text = _student.Birthday.Value.ToString();
                cboGender.Text = cboNewGender.Text = _student.Gender;
                cboNationality.Text = cboNewNationality.Text = _student.Nationality;
                txtBirthPlace.Text = txtNewBirthPlace.Text = _student.BirthPlace;
                _StudentPhone = JHSchool.Data.JHPhone.SelectByStudentID(_student.ID);

                txtTel.Text = txtNewTel.Text = _StudentPhone.Contact;
                //txtEngName.Text = txtNewEngName.Text = _student.na
                if (_student.Class != null)
                    lblClassName.Text =  _student.Class.Name;
                if (_student.SeatNo.HasValue)
                    lblSeatNo.Text = cboSeatNo.Text = _student.SeatNo.Value.ToString();
                lblStudentNum.Text = cbotStudentNumber.Text = _student.StudentNumber;

                // 如果轉出又入，使用原班
                lblNewClassName.Text = lblClassName.Text;
            }

            //依照status不同調整畫面大小
            if (status == AddTransStudStatus.Added)
            {
                gpOld.Visible = false;
                this.Size = new Size(422, 438);
                //txtNewName.Text = "";
                txtNewSSN.Text = AddTransBackgroundManager.StudentIDNumber;

            }
            else
            {
                gpOld.Visible = true;
                this.Size = new Size(823, 379);
            }
           // setClassNo();
            reLoadStudNumItems();
            this.MaximumSize = this.MinimumSize = this.Size;
            //AddTransBackgroundManager.AddTransStudBaseObj = this;

        }

        public AddTransStudBase()
        {
            InitializeComponent();
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewName.Text.Trim() == "")
                {
                    MsgBox.Show("姓名必填");
                    return;
                }

                if (lblNewClassName.Text.Trim() == "")
                {
                    MsgBox.Show("班級必填");
                    return;
                }            

                if (string.IsNullOrEmpty(cbotStudentNumber.Text))
                {
                    Errors.SetError(cbotStudentNumber, "學號空白!");
                }



                string msg = "請問是否將班級由「" + lblClassName.Text + "」調整成「" + lblNewClassName.Text + "」，並傳送至局端備查?";

                bool chkSend = false;

                if (FISCA.Presentation.Controls.MsgBox.Show(msg, "調整確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    chkSend = true;                
                }

                if (chkSend)
                {
                    string sid = string.Empty;
           

                    //if (StudCheckTool.CheckStudIDNumberSame(txtNewSSN.Text, sid))
                    //{
                    //    FISCA.Presentation.Controls.MsgBox.Show("身分證號重複請檢查");
                    //    return;
                    //}
                    

                    Dictionary<string, int> chkSum = new Dictionary<string, int>();
                    foreach (JHSchool.Data.JHStudentRecord studRec in JHSchool.Data.JHStudent.SelectAll())
                    {
                        if (studRec.Status == K12.Data.StudentRecord.StudentStatus.一般)
                            if (!string.IsNullOrEmpty(studRec.StudentNumber))
                            {
                                if (chkSum.ContainsKey(studRec.StudentNumber))
                                    chkSum[studRec.StudentNumber]++;
                                else
                                    chkSum.Add(studRec.StudentNumber, 1);
                            }
                    }

                    bool chkDNumber = false;
                    if (chkSum.ContainsKey(cbotStudentNumber.Text))
                    {
                        if (chkSum[cbotStudentNumber.Text] > 0)
                        {
                            chkDNumber = true;
                        }
                    }

                    Errors.SetError(cbotStudentNumber, "");

                    if (chkDNumber)
                    {
                        Errors.SetError(cbotStudentNumber, "學號重複請檢查!");
                        return;
                    }  

                    if (_status == AddTransStudStatus.Added)
                    {

                        JHSchool.Data.JHStudentRecord NewStudRec = new JHSchool.Data.JHStudentRecord();
                        NewStudRec.Name = txtNewName.Text;
                        NewStudRec.Gender = cboNewGender.Text;
                        NewStudRec.IDNumber = txtNewSSN.Text;
                        sid = JHSchool.Data.JHStudent.Insert(NewStudRec);
                        _StudentPhone = JHSchool.Data.JHPhone.SelectByStudentID(sid);
                        _StudentPhone.Contact = txtNewTel.Text;
                    }

                    if (!string.IsNullOrEmpty(sid))
                        _student = JHSchool.Data.JHStudent.SelectByID(sid);

                    //_student.Name = txtNewName.Text;

                    _student.IDNumber = txtNewSSN.Text;
                    DateTime dt;
                    if (DateTime.TryParse(dtNewBirthday.Text, out dt))
                        _student.Birthday = dt;

                    //_student.Gender = cboNewGender.Text;
                    _student.Nationality = cboNewNationality.Text;
                    _student.BirthPlace = txtNewBirthPlace.Text;
                    //_StudentPhone.Contact = txtNewTel.Text;
                    //_student.EnglishName = txtNewEngName.Text;

                    foreach (JHSchool.Data.JHClassRecord cr in JHSchool.Data.JHClass.SelectAll())
                    {
                        if (lblNewClassName.Text == cr.Name)
                        {
                            _student.RefClassID = cr.ID;
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(cboSeatNo.Text))
                        _student.SeatNo = null;
                    else
                    {
                        int no;
                        int.TryParse(cboSeatNo.Text, out no);
                        _student.SeatNo = no;
                    }
                    _student.StudentNumber = cbotStudentNumber.Text;


                    string strGradeYear = "";

                    if (_student.Class != null)
                        if (_student.Class.GradeYear.HasValue)
                            strGradeYear = _student.Class.GradeYear.Value.ToString();

                    // 當學生狀態非一般調整學生狀態
                    if (_student.Status != K12.Data.StudentRecord.StudentStatus.一般)
                    {
                        _student.Status = K12.Data.StudentRecord.StudentStatus.一般;
                    }

                    try
                    {
                        JHSchool.Data.JHStudent.Update(_student);
                        JHSchool.Data.JHPhone.Update(_StudentPhone);
                    }
                    catch (Exception ex)
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("更新學生資料發生錯誤：" + ex.Message);
                    }

                    // 傳送至局端
                    string errMsg = Utility.SendData("自動轉入", _student.IDNumber, _student.StudentNumber, _student.Name, strGradeYear, lblClassName.Text, cboSeatNo.Text, lblNewClassName.Text, "", "");
                    if (errMsg != "")
                        FISCA.Presentation.Controls.MsgBox.Show(errMsg);

                    //log
                    JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
                    if (_status == AddTransStudStatus.Added)
                        prlp.SaveLog("學生.轉入異動", "新增班級資料", "修改轉入與班級資料.");
                    else
                        prlp.SaveLog("學生.轉入異動", "修改班級資料", "修改轉入與班級資料.");

                    AddTransBackgroundManager.SetStudent(_student);

                    AddTransManagerForm atmf = new AddTransManagerForm();
                    this.Visible = false;
                    atmf.StartPosition = FormStartPosition.CenterParent;
                    atmf.ShowDialog(FISCA.Presentation.MotherForm.Form);
                    this.Close();
                    JHSchool.Student.Instance.SyncAllBackground();
                    JHSchool.Data.JHStudent.RemoveAll();
                    JHSchool.Data.JHStudent.SelectAll();

                    //註冊一個事件引發模組
                    EventHandler eh = FISCA.InteractionService.PublishEvent("KH_StudentTransStudBase");
                    eh(this, EventArgs.Empty);
                }
            }
            catch(Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show(ex.Message);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboClassName_TextChanged(object sender, EventArgs e)
        {

            cboSeatNo.Text = "";
            setClassNo();
            reLoadStudNumItems();
        }

        private void reLoadStudNumItems()
        {
            cbotStudentNumber.Items.Clear();
            cbotStudentNumber.Items.Add(lblStudentNum.Text);
            cbotStudentNumber.Items.Add(JHPermrec.UpdateRecord.DAL.DALTransfer2.GetGradeYearLastStudentNumber(lblNewClassName.Text));
            if (lblStudentNum.Text != "")
                cbotStudentNumber.Items.Add("");
        }

        // 產生座號
        private void setClassNo()
        {
            cboSeatNo.Items.Clear();
            cboSeatNo.Items.Add("");
            foreach (int no in Utility.GetClassSeatNoList(lblNewClassName.Text))
                cboSeatNo.Items.Add(no);
            //cboSeatNo.Items.AddRange(JHPermrec.UpdateRecord.DAL.DALTransfer2.GetClassNullNoList(cboClass.Text).ToArray());
        }



        private void cboSeatNo_TextChanged(object sender, EventArgs e)
        {
            //Errors.Clear();
        }

        private void cbotStudentNumber_TextChanged(object sender, EventArgs e)
        {
            
            Errors.Clear();
        }

        private void btnBefore_Click(object sender, EventArgs e)
        {
            AddTransBackgroundManager.AddTransStudObj.Visible = true;
            this.Visible = false;
        }

        private void txtNewName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbotStudentNumber.Text))
                Errors.SetError(cbotStudentNumber, "學號空白!");
            else
                Errors.SetError(cbotStudentNumber, "");
        }

        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void SetData(object x, object y)
        {
            setStudent_Status((JHSchool.Data.JHStudentRecord)x, (AddTransStudStatus)y);
        }

        public void Display()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowDialog(FISCA.Presentation.MotherForm.Form);            
        }

        private void AddTransStudBase_Load(object sender, EventArgs e)
        {
            // 取的班級年級
            List<string> grList = Utility.GetGradeYearList();
            cboGradeYear.Items.AddRange(grList.ToArray());
        }

        private void cboGradeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lblClassName.Text))
                lblNewClassName.Text = lblClassName.Text;
            else
                lblNewClassName.Text = Utility.GetClassNameFirst(cboGradeYear.Text);
            cboSeatNo.Text = "";
            // 班級座號
            setClassNo();
            // 取得學號
            cbotStudentNumber.Text = Utility.GetStudentNumber(cboGradeYear.Text);
        }   
    }
}

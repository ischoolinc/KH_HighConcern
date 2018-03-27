using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Framework;
using JHPermrec.UpdateRecord.Transfer;
using JHPermrec.UpdateRecord;
using ClassLock_KH.DAO;
using System.Data;
using FISCA.Data;

namespace StudentTransStudBase_KH
{
    public partial class AddTransStudBase : FISCA.Presentation.Controls.BaseForm,IRewriteAPI_JH.ITransStudBase
    {
        public enum AddTransStudStatus { Added, Modify }

        //private StudentRecord student;

        private JHSchool.Data.JHStudentRecord  _student;
        private AddTransStudStatus _status;
        private JHSchool.Data.JHPhoneRecord _StudentPhone;
        private int seatno_Max;
        private List<string> sameClassStudentIDList = new List<string>();

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


                MsgForm mf = new MsgForm();

                string msg = "請問是否將班級由「" + lblClassName.Text + "」調整成「" + lblNewClassName.Text + "」，並傳送至局端備查?";

                bool chkSend = false;

                mf.Text = "調整確認";
                mf.SetMsg(msg);
                //if (FISCA.Presentation.Controls.MsgBox.Show(msg, "調整確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                if(mf.ShowDialog()== System.Windows.Forms.DialogResult.Yes)
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

                    // 寫入班級學生變動
                    UDT_ClassSpecial StudSpec = UDTTransfer.AddClassSpecStudent(_student.ID, "", _student.RefClassID, lblClassName.Text, lblNewClassName.Text, lblNewClassName.Text, "", "");


                    // 傳送至局端
                    string errMsg = Utility.SendData("自動轉入", _student.IDNumber, _student.StudentNumber, _student.Name, strGradeYear, lblClassName.Text, cboSeatNo.Text, lblNewClassName.Text, "", "", _student.ID, _student.RefClassID, StudSpec.ClassComment);
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

        // 2018/3/27 穎驊 註解，稍微尋找一下後，控制項根本沒有cboClassName 這項，
        // 應該是隨著法規的更改，不再讓使用者選擇班級，因此控制項改用Label
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

            if (JHPermrec.UpdateRecord.DAL.DALTransfer2.GetGradeYearLastStudentNumber(lblNewClassName.Text)!="")
            {
                cbotStudentNumber.Items.Add(JHPermrec.UpdateRecord.DAL.DALTransfer2.GetGradeYearLastStudentNumber(lblNewClassName.Text));
            }
            
            if (lblStudentNum.Text != "")
                cbotStudentNumber.Items.Add("");
        }

        // 產生座號
        private void setClassNo()
        {
            cboSeatNo.Items.Clear();
            cboSeatNo.Items.Add("");
            
            foreach (int no in Utility.GetClassSeatNoList(lblNewClassName.Text))
            {
                cboSeatNo.Items.Add(no);
            }
                        
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
            //setClassNo();
            // 取得學號
            //cbotStudentNumber.Text = Utility.GetStudentNumber(cboGradeYear.Text);

            //2018/3/27 穎驊註解 ，因應 高雄小組項目 [09-04][02] 轉入生學號怎麼了? 更正 產生學號的邏輯

            List<KH_HighConcernCalc.ClassStudent> ClassStudentList = KH_HighConcernCalc.Calc.GetClassStudentList(cboGradeYear.Text);

            string _ClassID ="";

            //取得建議班級的ID
            if (ClassStudentList.Count > 0)
            {
                _ClassID = ClassStudentList[0].ClassID;
            }

            string cmd;
                        
            //2018/3/15 穎驊註解，因應高雄小組 [09-04][02] 轉入生學號怎麼了? 項目， 日後 轉入學生的建議號碼，一律是該班用過的最大號碼加一(不管學生的狀態)，
            //如此一來，系統建議提供的建議學號，就不會有重覆的問題
            if (_ClassID != null)
            {
                cmd = string.Format("select id,seat_no from student where ref_class_id='{0}'order by seat_no", _ClassID);

                QueryHelper Query = new QueryHelper();

                DataTable seatNoList = Query.Select(cmd);

                int seatno = 0;
                seatno_Max = 0;
                foreach (DataRow row in seatNoList.Rows)
                {
                    if (int.TryParse(row["seat_no"].ToString(), out seatno))
                    {
                        //取最大號碼
                        if (seatno > seatno_Max)
                        {
                            seatno_Max = seatno;
                        }
                    }
                    sameClassStudentIDList.Add(row["id"].ToString());
                }

                List<K12.Data.UpdateRecordRecord> updateRecod_List = K12.Data.UpdateRecord.SelectByStudentIDs(sameClassStudentIDList);

                //最大號碼加一
                cboSeatNo.Items.Clear();
                cboSeatNo.Items.Add("");
                cboSeatNo.Items.Add(seatno_Max+1);
                cboSeatNo.Text = ""+(seatno_Max+1);

                //整理入學年度的字典
                Dictionary<int?, int> entrySchoolYearDict = new Dictionary<int?, int>();

                foreach (K12.Data.UpdateRecordRecord ur in updateRecod_List)
                {
                    //為新生異動，整理個學年度入學的人數
                    if (ur.UpdateCode == "1")
                    {
                        if (!entrySchoolYearDict.ContainsKey(ur.SchoolYear))
                        {
                            entrySchoolYearDict.Add(ur.SchoolYear, 1);
                        }
                        else
                        {
                            entrySchoolYearDict[ur.SchoolYear]++;
                        }
                    }
                }

                int? majorEntrySchoolYear = 0;
                int count = 0;

                //找尋最多人入學的那一年
                foreach (KeyValuePair<int?, int> p in entrySchoolYearDict)
                {
                    if (p.Value > count)
                    {
                        majorEntrySchoolYear = p.Key;
                        count = p.Value;
                    }
                }

                string majorEntrySchoolYear_string = "" + majorEntrySchoolYear;

                // 入學年超過三碼 只取後兩碼(如106 >> 06)
                if (majorEntrySchoolYear_string.Length > 2)
                {
                    majorEntrySchoolYear_string = majorEntrySchoolYear_string.Remove(0, 1);
                }

                // 產生學號的性別碼(男=1,女=2)
                int genderCode;
                if (cboNewGender.Text == "男")
                {
                    genderCode = 1;
                }
                else
                {
                    genderCode = 2;
                }

                cmd = string.Format("select * from class where id='{0}'", _ClassID);

                DataTable classList = Query.Select(cmd);

                //抓班級名稱
                string className = "" + classList.Rows[0]["class_name"];

                int className_int;
                //  抓取 學號的班級序號
                string classOrder = "";

                // 2018/3/27 穎驊註解，假如抓出來的班級名稱無法順利的轉成int 型別，代表其班級命名方式
                // 有異於目前95%以上的高雄國中班級編碼模式(101、202、303等等)
                // 可能為1A、國一忠、三年一班 這種格式
                //如果出現此格式，則去抓它的班級排列序號
                if (!int.TryParse(className, out className_int))
                {

                    classOrder = "" + classList.Rows[0]["display_order"];

                    // 假如班級序號僅有一碼，補零(1>>01)
                    if (classOrder.Length == 1)
                    {
                        classOrder = "0" + classOrder;
                    }

                }
                else
                {
                    classOrder = "" + className_int;

                    //取後兩碼
                    if (classOrder.Length > 2)
                    {
                        classOrder = classOrder.Remove(0, 1);
                    }
                }


                // 假如建議座號僅有一碼，補零(1>>01)
                string seatno_Max_string = "" + (int.Parse(cboSeatNo.Text));

                if (seatno_Max_string.Length == 1)
                {
                    seatno_Max_string = "0" + seatno_Max_string;
                }

                string suggestStudentNumber = majorEntrySchoolYear_string + genderCode + classOrder + seatno_Max_string;

                // 加入建議學號
                cbotStudentNumber.Items.Add(suggestStudentNumber);
                cbotStudentNumber.Items.Add(Utility.GetStudentNumber(cboGradeYear.Text));
                cbotStudentNumber.Text = suggestStudentNumber;
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace StudentClassItem_KH
{
    public partial class SetClassNameSeatNoForm : FISCA.Presentation.Controls.BaseForm
    {
        private string _oldClassName;
        private string _ClassName = "";
        private string _SeatNo = "";
        private DateTime _MeetingDate;
        private string _Memo = "";
        private string _EDoc = "";
        private string _GradeYear = "";

        // upload file name
        private string _FileName = "";
        private string _base64Data = "";

        // 是否傳送
        private bool _ChkSend = true;
        Dictionary<string, string> _ClassNameDict;
        Dictionary<string, string> _ClassNameMapDict;
        public SetClassNameSeatNoForm()
        {
            InitializeComponent();
            _ClassNameDict = new Dictionary<string, string>();
            _ClassNameMapDict = new Dictionary<string, string>();
        }

        public void setCboGradeYearEnable(bool Enable)
        {
            cboGradeYear.Enabled = Enable;
        }

        public void SetClassName(string ClassName)
        {
            _ClassName = ClassName;
        }

        public void SetOldClassName(string oClassName)
        {
            _oldClassName = oClassName;
        }

        public void SetSeatNo(string SeatNo)
        {
            _SeatNo = SeatNo;
        }

        public void SetClassNameItems(List<string> nameList)
        {
            foreach (string name in nameList)
                cboClassName.Items.Add(name);
        }

       

        public void SetSeatNoItems(List<int> seatList)
        {
            cboSeatNo.Items.Add("");
            foreach (int i in seatList)
                cboSeatNo.Items.Add(i);
        }

        public string GetClassName()
        {
            return _ClassName;
        }

        public string GetSeatNo()
        {
            return _SeatNo;
        }

        public string GetMettingDate()
        {
            return _MeetingDate.ToShortDateString();
        }

        public string GetMemo()
        {
            return _Memo;
        }

        /// <summary>
        /// 相關證明文件網址
        /// </summary>
        /// <returns></returns>
        public string GetEDoc()
        {
            return _EDoc;
        }

        /// <summary>
        /// 取得上傳檔案名稱
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            return _FileName;
        }

        /// <summary>
        /// 取得轉 Base64 字串
        /// </summary>
        /// <returns></returns>
        public string GetBase64DataString()
        {
            return _base64Data;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void SetClassNameSeatNoForm_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.MaximumSize = this.Size;
            txtEDoc.ReadOnly = true;

            cboGradeYear.Text = _GradeYear;

            if (cboClassName.Items.Count > 0)
            {
                cboClassName.Text = cboClassName.Items[0].ToString();
            }

            // 透過年級取得班級
            if (!string.IsNullOrEmpty(cboGradeYear.Text))
            {
                SetClassNameCotItems(cboGradeYear.Text);
            }

        }

        private void SetClassNameCotItems(string GradeYear)
        {
            Dictionary<string, int> classCot = new Dictionary<string, int>();

            List<KH_HighConcernCalc.ClassStudent> ClassStudentList = KH_HighConcernCalc.Calc.GetClassStudentList(GradeYear);
            _ClassNameDict.Clear();
            foreach (KH_HighConcernCalc.ClassStudent cs in ClassStudentList)
            {
                classCot.Add(cs.ClassName, cs.ClassStudentCount);
               _ClassNameDict.Add(cs.ClassName, cs.ClassID);
            }


           // Dictionary<string, int> classCot = Utility.GetClassNameDictByGradeYear(GradeYear);
            cboClassName.Text = "";
            cboClassName.Items.Clear();
            _ClassNameMapDict.Clear();
            foreach (string name in classCot.Keys)
            {
                string nName = name + "(" + classCot[name] + ")";
                cboClassName.Items.Add(nName);
                _ClassNameMapDict.Add(nName, name);
            }
           // _ClassNameDict = Utility.GetClassNameIDDictByGradeYear(GradeYear);            
        }


        /// <summary>
        /// 檢查班級名稱是否相同
        /// </summary>
        /// <returns></returns>
        private bool chkSameClassName()
        {
            bool retVal = false;
            if (_ClassNameMapDict.ContainsKey(cboClassName.Text))
                _ClassName = _ClassNameMapDict[cboClassName.Text];

            if (_oldClassName == _ClassName)
                retVal = true;
            return retVal;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (ChkData())
            {

                _ChkSend = true;

                _ClassName = "";
                if(_ClassNameMapDict.ContainsKey(cboClassName.Text))
                    _ClassName = _ClassNameMapDict[cboClassName.Text];

                _SeatNo = cboSeatNo.Text;
                _MeetingDate = dtMeetting.Value;
                _Memo = txtMemo.Text;
                _EDoc = txtEDoc.Text;

                // 班級相同只改座號不傳送
                if (chkSameClassName())
                    _ChkSend = false;

                if (_ChkSend)
                {
                    string msg = "請問是否將班級由「" + _oldClassName + "」調整成「" + _ClassName + "」，按下「是」確認後，需函報教育局並由局端線上審核。";

                    MsgForm mf = new MsgForm();
                    mf.Text = "調整確認";
                    mf.SetMsg(msg);
                    //if (FISCA.Presentation.Controls.MsgBox.Show(msg, "調整確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    if(mf.ShowDialog()== System.Windows.Forms.DialogResult.Yes)
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    else
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
                else
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else {
                FISCA.Presentation.Controls.MsgBox.Show("資料有誤無法儲存");
            }
        }

        /// <summary>
        /// 取得是否傳送
        /// </summary>
        /// <returns></returns>
        public bool GetChkSend()
        {
            return _ChkSend;
        }

        private bool ChkData()
        {
            bool pass = true;
            if (cboClassName.Text.Trim() == "")
            {
                pass = false;
                FISCA.Presentation.Controls.MsgBox.Show("班級必填");
            }

            if (dtMeetting.IsEmpty)
            {
                if (chkSameClassName() == false)
                {
                    pass = false;
                    FISCA.Presentation.Controls.MsgBox.Show("編班委員會會議日期必填");
                }
            }

            // 當沒有填且班級名稱不同
            if (string.IsNullOrWhiteSpace(txtEDoc.Text) && chkSameClassName()==false)
            {
                pass = false;
                FISCA.Presentation.Controls.MsgBox.Show("相關證明文件網址必填");
            }

            return pass;
        }

        private void cboClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSeatNo.Text = "";
            cboSeatNo.Items.Clear();
            string className = "";
            if (_ClassNameMapDict.ContainsKey(cboClassName.Text))
                className = _ClassNameMapDict[cboClassName.Text];
            SetSeatNoItems(Utility.GetClassSeatNoList(className));
        }

        /// <summary>
        /// 取得第1順位班級名稱
        /// </summary>
        /// <returns></returns>
        public string GetFirstClassName()
        {
            string retVal = "";
            if (cboClassName.Items.Count > 0)
            {
                string ccName=cboClassName.Items[0].ToString();
                if (_ClassNameMapDict.ContainsKey(ccName))
                    retVal = _ClassNameMapDict[ccName];
            }
            return retVal;
        }

        /// <summary>
        /// 取得第2順位班級名稱
        /// </summary>
        /// <returns></returns>
        public string GetSecondClassName ()
        {
            string retVal = "";
            if (cboClassName.Items.Count > 1)
            {
                string ccName = cboClassName.Items[1].ToString();
                if (_ClassNameMapDict.ContainsKey(ccName))
                    retVal = _ClassNameMapDict[ccName];
            }
            return retVal;
        }

        /// <summary>
        /// 取得第3順位班級名稱
        /// </summary>
        /// <returns></returns>
        public string GetThridClassName()
        {
            string retVal = "";
            if (cboClassName.Items.Count > 2)
            {
                string ccName = cboClassName.Items[2].ToString();
                if (_ClassNameMapDict.ContainsKey(ccName))
                    retVal = _ClassNameMapDict[ccName];
            }
            return retVal;
        }

        public void SetCboGradeYearItems(List<string> items)
        {
            cboGradeYear.Items.AddRange(items.ToArray());
        }

        public void SetCboGradeYearText(string str)
        {
            _GradeYear = str;
            cboGradeYear.Items.Add(str);
        }

        private void cboGradeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboGradeYear.Text))
            {
                SetClassNameCotItems(cboGradeYear.Text);
            }
        }

        public Dictionary<string, string> GetClassNameDict()
        {
            return _ClassNameDict;
        }

        private void UploadFile_Click(object sender, EventArgs e)
        {
            Guid g = Guid.NewGuid();

            string DSNS = FISCA.Authentication.DSAServices.AccessPoint;

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // 取得檔案
                _FileName = g.ToString() + ofd.SafeFileName;
                txtEDoc.Text = "https://storage.googleapis.com/1campus-photo/" + DSNS + "/upload_" + _FileName;
                // 轉 Base64
                try
                {
                    MemoryStream ms = new MemoryStream();
                    ofd.OpenFile().CopyTo(ms);
                    _base64Data = Convert.ToBase64String(ms.ToArray());

                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show("讀取上傳檔案失敗," + ex.Message);
                }
            }
        }
    }
}

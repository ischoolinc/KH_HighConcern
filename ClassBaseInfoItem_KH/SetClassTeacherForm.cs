using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JHSchool.Data;
using JHSchool;

namespace ClassBaseInfoItem_KH
{
    public partial class SetClassTeacherForm : FISCA.Presentation.Controls.BaseForm
    {
        private string _oldTeacherName;
        private string _oldTeacherID;
        private string _TeacherName = "";

        private string _ClassName;

        private string _Memo = "";
        private string _EDoc = "";
        

        // upload file name
        private string _FileName = "";
        private string _base64Data = "";

        // 是否傳送
        private bool _ChkSend = true;
        Dictionary<string, string> _TeacherNameDic;

        public SetClassTeacherForm()
        {
            InitializeComponent();


            _TeacherNameDic = new Dictionary<string, string>();

        }


        public void SetTeacherName(string TeacherName)
        {
            _TeacherName = TeacherName;
        }

        public void SetOldTeacherName(string oTeacherName)
        {
            _oldTeacherName = oTeacherName;
        }

        public void SetOldTeacherID(string oTeacherID)
        {
            _oldTeacherID = oTeacherID;
        }

        public void SetClassName(string ClassName)
        {
            _ClassName = ClassName;
        }


        public string GetTeacherName()
        {
            return _TeacherName;
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
        

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (ChkData())
            {
                _ChkSend = true;

                _TeacherName = cboTeacherName.Text;

                _Memo = txtMemo.Text;
                _EDoc = txtEDoc.Text;

                if (_ChkSend)
                {
                    string msg = "請問是否將班級「" + _ClassName + "」班導師由「" + _oldTeacherName + "」調整成「" + _TeacherName + "」，按下「是」確認後，需函報教育局並由局端線上審核。";

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

            return pass;
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
                txtEDoc.Text = "https://storage.googleapis.com/1campus-photo/j.kh.edu.tw/" + DSNS + "/upload_" + _FileName;
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

        private void SetClassTeacherForm_Load(object sender, EventArgs e)
        {
            // 教師名稱索引
            _TeacherNameDic.Clear();
            foreach (JHTeacherRecord TRec in JHTeacher.SelectAll())
            {
                // 狀態為刪除的老師 排除
                if (TRec.Status == K12.Data.TeacherRecord.TeacherStatus.刪除)
                    continue;

                // 已經是目前的班級班導師，則不加入名單
                if (TRec.ID == _oldTeacherID)
                    continue;

                if (string.IsNullOrEmpty(TRec.Nickname))
                    _TeacherNameDic.Add(TRec.ID, TRec.Name);
                else
                    _TeacherNameDic.Add(TRec.ID, TRec.Name + "(" + TRec.Nickname + ")");
            }

            cboTeacherName.Items.Clear();
            List<string> nameList = new List<string>();
            foreach (string name in _TeacherNameDic.Values)
                nameList.Add(name);
            nameList.Sort();

            cboTeacherName.Items.AddRange(nameList.ToArray());
        }
    }
}

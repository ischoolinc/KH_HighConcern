using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ClassLock_KH
{
    public partial class SendDataForm : FISCA.Presentation.Controls.BaseForm
    {
        private string _strDate = "";

        private string _strComment = "";

        private string _strDocNo = "";

        private string _strEDoc = "";

        private bool _NUnLock = false;

        private string _base64Data = "";

        private string _FileName = "";


        public SendDataForm()
        {
            InitializeComponent();
        }

        private void SendDataForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            this.dtDate.IsEmpty = true;
            txtEDoc.ReadOnly = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (dtDate.IsEmpty)
            {
                FISCA.Presentation.Controls.MsgBox.Show("編班委員會會議日期必填");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEDoc.Text))
            {
                FISCA.Presentation.Controls.MsgBox.Show("相關證明文件網址必填");
                return;
            }

            //調整因高雄小組會議：[3-2] 鎖班時，其「備註」欄位也要修正成”必填”。
            if (string.IsNullOrWhiteSpace(txtComment.Text))
            {
                FISCA.Presentation.Controls.MsgBox.Show("備註必填");
                return;
            }

            _strDate = dtDate.Value.ToShortDateString();

            if (!string.IsNullOrEmpty(txtComment.Text))
                _strComment = txtComment.Text;

            if (!string.IsNullOrEmpty(txtDocNo.Text))
                _strDocNo = txtDocNo.Text;

            if (!string.IsNullOrEmpty(txtEDoc.Text))
                _strEDoc = txtEDoc.Text;

            _NUnLock = chkNUnLock.Checked;

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        /// <summary>
        /// 取得編班日期時間
        /// </summary>
        /// <returns></returns>
        public string GetSendDate()
        {
            return _strDate;
        }

        /// <summary>
        /// 取得備註
        /// </summary>
        /// <returns></returns>
        public string GetComment()
        {
            return _strComment;
        }

        /// <summary>
        /// 取得文號
        /// </summary>
        /// <returns></returns>
        public string GetDocNo()
        {
            return _strDocNo;
        }

        /// <summary>
        /// 取得是否不自動解鎖
        /// </summary>
        /// <returns></returns>
        public bool GetNUnLock()
        {
            return _NUnLock;
        }

        /// <summary>
        /// 相關證明文件網址
        /// </summary>
        /// <returns></returns>
        public string GetEDoc()
        {
            return _strEDoc;
        }

        /// <summary>
        /// 取得檔案轉 Base64 字串
        /// </summary>
        /// <returns></returns>
        public string GetBase64DataString()
        {
            return _base64Data;
        }

        /// <summary>
        /// 取得檔案名稱
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            return _FileName;
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            Guid g = Guid.NewGuid();

            string DSNS = FISCA.Authentication.DSAServices.AccessPoint;

           OpenFileDialog ofd = new OpenFileDialog();            
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // 取得檔案
                    _FileName = g.ToString() + ofd.SafeFileName;
                    txtEDoc.Text = "https://storage.googleapis.com/1campus-photo/j.kh.edu.tw/"+DSNS+"/upload_" + _FileName;
                    // 轉 Base64
                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        ofd.OpenFile().CopyTo(ms);
                        _base64Data = Convert.ToBase64String(ms.ToArray());

                    }catch(Exception ex)
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("讀取上傳檔案失敗," + ex.Message);
                    }
                }
        }
    }
}

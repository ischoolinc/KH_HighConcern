using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClassLock_KH
{
    public partial class SendDataForm : FISCA.Presentation.Controls.BaseForm
    {
        private string _strDate = "";

        private string _strComment = "";

        private string _strDocNo = "";

        private string _strEDoc = "";

        private bool _NUnLock = false;

        public SendDataForm()
        {
            InitializeComponent();
        }

        private void SendDataForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            this.dtDate.IsEmpty = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (dtDate.IsEmpty)
            {
                FISCA.Presentation.Controls.MsgBox.Show("編班委員會會議日期必填");
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
    }
}

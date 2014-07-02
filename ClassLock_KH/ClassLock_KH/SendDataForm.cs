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
    }
}

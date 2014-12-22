using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentChangeStatus_KH
{
    public partial class sendMessage : FISCA.Presentation.Controls.BaseForm
    {                
        private string _studName="";
        private string _StudStatus="";
        private string _NewStudStatus="";
        private bool _chkSendSpec = false;
        private string _Message = "";

        public sendMessage(string StudName, string StudStatus, string NewStudStatus, bool chkSendSpec)
        {
            InitializeComponent();
            _studName = StudName;
            _StudStatus = StudStatus;
            _NewStudStatus = NewStudStatus;
            _chkSendSpec = chkSendSpec;
        }

        public string GetMessage()
        {
            return _Message;
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void btnY_Click(object sender, EventArgs e)
        {
            _Message = txtMsg.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void sendMessage_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("請問是否將 " + _studName + " 由" + _StudStatus + " 調整成 " + _NewStudStatus + "，");
            
            if(_chkSendSpec)
                sb.AppendLine("按下「是」確認後，需報局備查。");
            else
                sb.AppendLine("按下「是」確認後，局端會留狀態變更紀錄。");

            lblMsg.Text = sb.ToString();
        }
    }
}

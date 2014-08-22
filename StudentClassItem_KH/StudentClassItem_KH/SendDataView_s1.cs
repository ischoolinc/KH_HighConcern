using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentClassItem_KH
{
    public partial class SendDataView_s1 : FISCA.Presentation.Controls.BaseForm
    {
        private RspMsg _RspMsg;

        public SendDataView_s1(RspMsg rspMsg)
        {
            InitializeComponent();
            _RspMsg = rspMsg;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SendDataView_s1_Load(object sender, EventArgs e)
        {
            LoadData();
            txtAction.ReadOnly = true;
            txtContent.ReadOnly = true;
            txtDate.ReadOnly = true;
            txtMemo.ReadOnly = true;
        }

        private void LoadData()
        {
            if (_RspMsg != null)
            {
                txtDate.Text = _RspMsg.Date.ToString();
                txtAction.Text = _RspMsg.Action;
                txtMemo.Text = _RspMsg.Comment;
                //txtMemo.Text = _RspMsg.GetComment();
                txtContent.Text = _RspMsg.GetContentString(true);
            }        
        } 
    }
}

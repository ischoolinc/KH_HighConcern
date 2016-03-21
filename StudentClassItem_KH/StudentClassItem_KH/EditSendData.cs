using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;

namespace StudentClassItem_KH
{
    public partial class EditSendData : BaseForm
    {
        RspMsg _RspMsg = null;
        private bool _chkReloadData = false;

        public EditSendData()
        {
            InitializeComponent();            
        }

        public void SetRspMessage(RspMsg rm)
        {
            _RspMsg = rm;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_RspMsg !=null)
            {
                //_RspMsg.Verify = "";
                if(_RspMsg.Content.ContainsKey("EDoc"))
                {
                    _RspMsg.Content["EDoc"] = txtEDoc.Text;
                }

                string errMsg=Utility.UpdateData(_RspMsg);
                if (errMsg == "")
                {
                    MsgBox.Show("更新完成");
                    _chkReloadData = true;
                }
                else
                {
                    MsgBox.Show("更新發生錯誤," + errMsg);
                    _chkReloadData = false;
                }
                    
            }
            this.Close();
        }

        /// <summary>
        /// 取得是否重新整理
        /// </summary>
        /// <returns></returns>
        public bool GetChkReloadData()
        {
            return _chkReloadData;
        }

        private void EditSendData_Load(object sender, EventArgs e)
        {
            if(_RspMsg !=null)
            {
                if (_RspMsg.Content.ContainsKey("EDoc"))
                    txtEDoc.Text = _RspMsg.Content["EDoc"];
            }
        }
    }
}

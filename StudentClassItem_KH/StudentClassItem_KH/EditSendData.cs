using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using System.IO;

namespace StudentClassItem_KH
{
    public partial class EditSendData : BaseForm
    {
        RspMsg _RspMsg = null;
        private bool _chkReloadData = false;
        // upload file name
        private string _FileName = "";
        private string _base64Data = "";

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
                // 傳送檔案到局端
                Utility.UploadFile(_RspMsg.UID, _base64Data, _FileName, "edit");

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

        private void Upload_Click(object sender, EventArgs e)
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
    }
}

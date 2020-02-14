using FISCA.Data;
using FISCA.Presentation.Controls;
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
    public partial class FrmDistrictNotification : BaseForm
    {
        private QueryHelper _Qh = new QueryHelper();
        private string _LogIDD;


        public FrmDistrictNotification(string logID)
        {
            InitializeComponent();
            this._LogIDD = logID;

        }


        private void FrmDistrictNotification_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = $"SELECT description FROM LOG WHERE id = '{this._LogIDD}' ";

                DataTable dt = _Qh.Select(sql);

                string description = dt.Rows[0][0] + "";

                this.labelX2.Text = description;
                //this.textInfo.Text = description;
            }
            catch (Exception ex)
            {
                MsgBox.Show("發生錯誤:" + ex.Message);
            }

        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            if (chkNotShow.Checked) //如果不要在顯示
            {
                // update list 
                string contentStr = "<District> <LogID>" + this._LogIDD + "</LogID> <IsShow> false </IsShow> </District> ";

                string sql = $"UPDATE list SET content = '{contentStr}' WHERE  name  = '高雄_局端解鎖_通知設定' RETURNING * ";

                try
                {
                    _Qh.Select(sql);

                }
                catch (Exception ex)
                {
                    MsgBox.Show("發生錯誤 : "+ ex.Message);
                }
            }

            this.Close();
        }
    }
}

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
    public partial class SetClassNameSeatNoForm : FISCA.Presentation.Controls.BaseForm
    {
        private string _ClassName = "";
        private string _SeatNo = "";
        private DateTime _MeetingDate;
        private string _Memo = "";
        Dictionary<string, string> _ClassNameDict;

        public SetClassNameSeatNoForm()
        {
            InitializeComponent();
            _ClassNameDict = new Dictionary<string, string>();
        }

        public void SetClassName(string ClassName)
        {
            _ClassName = ClassName;
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

        public void SetClassNameDict(Dictionary<string, string> data)
        {
            _ClassNameDict = data;
        }

        public void SetSeatNoItems(List<int> seatList)
        {
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void SetClassNameSeatNoForm_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.MaximumSize = this.Size;            
            dtMeetting.Value = DateTime.Now;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (ChkData())
            {
                _ClassName = cboClassName.Text;
                _SeatNo = cboSeatNo.Text;
                _MeetingDate = dtMeetting.Value;
                _Memo = txtMemo.Text;
                
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else {
                FISCA.Presentation.Controls.MsgBox.Show("資料有誤無法儲存");
            }
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
                pass = false;
                FISCA.Presentation.Controls.MsgBox.Show("編班會議日期必填");
            }

            return pass;
        }

        private void cboClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSeatNo.Text = "";
            cboSeatNo.Items.Clear();
            SetSeatNoItems(Utility.GetClassSeatNoList(cboClassName.Text));
        }    
    }
}

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
        private string _oldClassName;
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

        public void SetOldClassName(string oClassName)
        {
            _oldClassName = oClassName;
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
            cboSeatNo.Items.Add("");
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
            //dtMeetting.Value = DateTime.Now;
            if (cboClassName.Items.Count > 0)
            {
                cboClassName.Text = cboClassName.Items[0].ToString();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (ChkData())
            {
                _ClassName = cboClassName.Text;
                _SeatNo = cboSeatNo.Text;
                _MeetingDate = dtMeetting.Value;
                _Memo = txtMemo.Text;

                if (_ClassNameDict.ContainsKey(cboClassName.Text))
                    _ClassName = _ClassNameDict[cboClassName.Text];


                string msg = "請問是否將班級由「" + _oldClassName + "」調整成「" + _ClassName + "」，按下「是」確認後，需報局備查。";
                
                if (FISCA.Presentation.Controls.MsgBox.Show(msg, "調整確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                else
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                
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
                FISCA.Presentation.Controls.MsgBox.Show("編班委員會會議日期必填");
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

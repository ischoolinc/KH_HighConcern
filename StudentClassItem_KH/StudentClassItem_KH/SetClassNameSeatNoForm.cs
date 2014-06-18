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

        public SetClassNameSeatNoForm()
        {
            InitializeComponent();

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
            return dtMeetting.Value.ToShortDateString();
        }

        public string GetMemo()
        {
            return txtMemo.Text;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void SetClassNameSeatNoForm_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.MaximumSize = this.Size;
            cboClassName.Text = _ClassName;
            cboSeatNo.Text = _SeatNo;
            dtMeetting.Value = DateTime.Now;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cboClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSeatNo.Items.Clear();
            SetSeatNoItems(Utility.GetClassSeatNoList(cboClassName.Text));
        }    
    }
}

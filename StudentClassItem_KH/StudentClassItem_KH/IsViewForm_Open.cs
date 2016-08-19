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
    public partial class IsViewForm_Open : FISCA.Presentation.Controls.BaseForm
    {
        public IsViewForm_Open(name m)
        {
            InitializeComponent();
            lblName.Text = m._messageTitle1;
            txtMsg.Text = m._value1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

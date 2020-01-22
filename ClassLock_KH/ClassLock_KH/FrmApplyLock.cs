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
    public partial class FrmApplyLock : BaseForm
    {
        public FrmApplyLock()
        {
            InitializeComponent();
        }

        private void FrmApplyLock_Load(object sender, EventArgs e)
        {

        }

        //申請
        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
        //取消
        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}

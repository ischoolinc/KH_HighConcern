using JHSchool;
using K12.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AddStudent_KH
{
    public partial class FormAddStudent : FISCA.Presentation.Controls.BaseForm
    {
        public FormAddStudent()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
                return;

            string Msg = "轉學生請利用「異動作業>轉入作業」或是「線上轉學」功能。若是以此「新增」功能新增學生，必須透過「調整班級」功能調整學生班級，且需要輸入編班委員會會議日期，並將傳送至局端備查。請確認是否新增學生?";
            if (FISCA.Presentation.Controls.MsgBox.Show(Msg, "新增學生", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                K12.Data.StudentRecord studRec = new K12.Data.StudentRecord();
                studRec.Name = txtName.Text;
                string StudentID = K12.Data.Student.Insert(studRec);
                PermRecLogProcess prlp = new PermRecLogProcess();
                if (chkInputData.Checked == true)
                {
                    if (StudentID != "")
                    {
                        JHSchool.Student.Instance.PopupDetailPane(StudentID);
                        JHSchool.Student.Instance.SyncDataBackground(StudentID);
                    }
                }
                JHSchool.Student.Instance.SyncDataBackground(StudentID);

                prlp.SaveLog("學籍.學生", "新增學生", "新增學生姓名:" + txtName.Text);
            }
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

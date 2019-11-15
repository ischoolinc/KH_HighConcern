using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentTransferStudentBrief_KH
{
    public partial class ClassMessage : BaseForm
    {
        string _message = "";
        public ClassMessage(string message1, int message2, string message3, int message4)
        {
            InitializeComponent();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("轉入生 已是本校學生,原班級為:{0}");
            sb.AppendLine("");
            sb.AppendLine("轉入原班級:恢復為原班級的學生");
            sb.AppendLine("(編班人數: {1})");
            sb.AppendLine("");
            sb.AppendLine("轉入新班級:依局端系統規則優先轉入班級:{2}");
            sb.AppendLine("(編班人數:{3})");

            _message = string.Format(sb.ToString(), message1, "" + message2, message3, "" + message4);

            lbHelpMessage.Text = _message;
        }
    }
}

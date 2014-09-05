using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentImportWizard_KH
{
    public class logStud
    {
        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string StudentID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 身分證號
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 原班級
        /// </summary>
        public string oClassName { get; set; }

        /// <summary>
        /// 目前班級
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public string SeatNo { get; set; }

        /// <summary>
        /// 年級
        /// </summary>
        public string GradeYear { get; set; }

        /// <summary>
        /// 原學生狀態
        /// </summary>
        public string oStudentStatus { get; set; }

        /// <summary>
        /// 學生狀態
        /// </summary>
        public string StudentStatus { get; set; }
    }
}

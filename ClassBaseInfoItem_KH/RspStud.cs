using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassBaseInfoItem_KH
{
    /// <summary>
    /// 學生
    /// </summary>
    public class RspStud
    {
        /// <summary>
        /// 身分證
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 班級名稱
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 新班級
        /// </summary>
        public string NewClassName { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public string SeatNo { get; set; }

        /// <summary>
        /// 年級
        /// </summary>
        public string GradeYear { get; set; }

        /// <summary>
        /// 理由
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 學生狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 新學生狀態
        /// </summary>
        public string NewStatus { get; set; }

        /// <summary>
        /// 相關證明文件網址
        /// </summary>
        public string EDoc { get; set; }
    }
}

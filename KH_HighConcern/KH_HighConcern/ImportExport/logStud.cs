using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KH_HighConcern.ImportExport
{
    /// <summary>
    /// 紀錄傳送局端資料
    /// </summary>
    public class logStud
    {
        public string IDNumber { get; set; }
        public string StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string SeatNo { get; set; }
        public string DocNo { get; set; }
        public string NumberReduce { get; set; }

        /// <summary>
        /// 相關證明文件網址
        /// </summary>
        public string EDoc { get; set; }
    }
}

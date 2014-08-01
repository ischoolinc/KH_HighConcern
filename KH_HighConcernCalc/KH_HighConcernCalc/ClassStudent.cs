using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KH_HighConcernCalc
{
    /// <summary>
    /// 編班人數
    /// </summary>
    public class ClassStudent
    {

        /// <summary>
        /// 班級編號
        /// </summary>
        public string ClassID { get; set; }
        /// <summary>
        /// 班級名稱
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 班級人數
        /// </summary>
        public int StudentCount { get; set; }

        /// <summary>
        /// 高關懷人數
        /// </summary>
        public int HStudentCount = 0;

        /// <summary>
        /// 編班人數=班級人數+高關懷人數
        /// </summary>
        public int ClassStudentCount { get; set; }

        /// <summary>
        /// 將班級名稱轉數字
        /// </summary>
        public int ClassNameInt { get; set; }
    }
}

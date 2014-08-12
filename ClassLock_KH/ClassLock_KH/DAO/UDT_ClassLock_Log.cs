using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace ClassLock_KH.DAO
{

    /// <summary>
    /// 高雄自動編班-全班解鎖
    /// </summary>
    [TableName("kh.automatic.class.unlock.log")]
    public class UDT_ClassLock_Log:ActiveRecord
    {
        ///<summary>
        /// 動作
        ///</summary>
        [Field(Field = "action", Indexed = true)]
        public string Action { get; set; }

        ///<summary>
        /// 日期時間
        ///</summary>
        [Field(Field = "date_time", Indexed = false)]
        public DateTime Date { get; set; }
    }
}

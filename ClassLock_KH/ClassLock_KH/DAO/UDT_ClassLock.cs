using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLock_KH.DAO
{
    /// <summary>
    /// 高雄自動編班-班級鎖定
    /// </summary>
    [TableName("kh.automatic.class.lock")]
    public class UDT_ClassLock : ActiveRecord
    {
        ///<summary>
        /// 班級編號
        ///</summary>
        [Field(Field = "class_id", Indexed = true)]
        public string ClassID { get; set; }
        
        ///<summary>
        /// 班級
        ///</summary>
        [Field(Field = "class_name", Indexed = true)]
        public string ClassName { get; set; }
    }
}

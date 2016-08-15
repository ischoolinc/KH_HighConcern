using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
namespace ClassLock_KH.DAO
{
    /// <summary>
    /// 高雄自動編班-特殊班級
    /// </summary>
    [TableName("kh.automatic.class.special")]
    public class UDT_ClassSpecial:ActiveRecord
    {
        ///<summary>
        /// 學生編號
        ///</summary>
        [Field(Field = "ref_student_id", Indexed = true)]
        public int StudentID { get; set; }

        ///<summary>
        /// 舊班級編號
        ///</summary>
        [Field(Field = "old_class_id", Indexed = false)]
        public int OldClassID { get; set; }

        ///<summary>
        /// 班級編號
        ///</summary>
        [Field(Field = "ref_class_id", Indexed = false)]
        public int ClassID { get; set; }

        ///<summary>
        /// 舊班級名稱
        ///</summary>
        [Field(Field = "old_class_name", Indexed = false)]
        public string OldClassName { get; set; }

        ///<summary>
        /// 班級名稱
        ///</summary>
        [Field(Field = "class_name", Indexed = false)]
        public string ClassName { get; set; }

        ///<summary>
        /// 舊班級備註
        ///</summary>
        [Field(Field = "old_class_comment", Indexed = false)]
        public string OldClassComment { get; set; }

        ///<summary>
        /// 班級備註
        ///</summary>
        [Field(Field = "class_comment", Indexed = false)]
        public string ClassComment { get; set; }

        ///<summary>
        /// 其他內容(XML)
        ///</summary>
        [Field(Field = "content", Indexed = false)]
        public string Content { get; set; }

    }
}

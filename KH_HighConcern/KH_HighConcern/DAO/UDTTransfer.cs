using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using FISCA.Data;
using FISCA.DSAUtil;
using System.Data;

namespace KH_HighConcern.DAO
{
    public class UDTTransfer
    {
        /// <summary>
        /// 透過學生ID取得高關懷註記
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,UDT_HighConcern> GetHighConcernDictByStudentIDList(List<string> StudentIDList)
        {
            Dictionary<string, UDT_HighConcern> retVal = new Dictionary<string, UDT_HighConcern>();
            if (StudentIDList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                string query = "ref_student_id in('"+string.Join("','",StudentIDList.ToArray())+"')";
                List<UDT_HighConcern> HighConcernList = accessHelper.Select<UDT_HighConcern>(query);
                foreach (UDT_HighConcern data in HighConcernList)
                {
                    if (!retVal.ContainsKey(data.RefStudentID))
                        retVal.Add(data.RefStudentID, data);
                }
            }
            return retVal;
        }


        /// <summary>
        /// 取得系統內所有高關懷學生
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, UDT_HighConcern> GetHighConcernDictAll()
        {
            Dictionary<string, UDT_HighConcern> retVal = new Dictionary<string, UDT_HighConcern>();
        
                AccessHelper accessHelper = new AccessHelper();                
                List<UDT_HighConcern> HighConcernList = accessHelper.Select<UDT_HighConcern>();
                foreach (UDT_HighConcern data in HighConcernList)
                {
                    if (!retVal.ContainsKey(data.RefStudentID))
                        retVal.Add(data.RefStudentID, data);
                }
        
            return retVal;
        }

        /// <summary>
        /// 取得系統內所有高關懷註記學生ID
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllHighConcernStudentIDList()
        {
            List<string> retVal = new List<string>();
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select("select ref_student_id as sid from $kh.automatic.placement.high.concern");
            foreach (DataRow dr in dt.Rows)
                retVal.Add(dr["sid"].ToString());
            return retVal;
        }

        /// <summary>
        /// 取得系統內所有一般狀態學生ID
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudentNumIDDictAll()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select("select student_number,id from student where status=1 and student_number is not null order by student_number");
            foreach (DataRow dr in dt.Rows)
            {
                string key = dr["student_number"].ToString();
                if (!retVal.ContainsKey(key))
                    retVal.Add(key, dr["id"].ToString());
            
            }
            return retVal;
        }

        /// <summary>
        /// 建立使用到的 UDT Table
        /// </summary>
        public static void CreateUDTTable()
        {
            FISCA.UDT.SchemaManager Manager = new SchemaManager(new DSConnection(FISCA.Authentication.DSAServices.DefaultDataSource));
            Manager.SyncSchema(new UDT_HighConcern());
        }
    }
}

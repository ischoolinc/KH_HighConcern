using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using FISCA.Data;
using FISCA.DSAUtil;

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
        /// 取得系統內所有高關懷註記學生ID
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllHighConcernStudentIDList()
        {
            List<string> retVal = new List<string>();

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

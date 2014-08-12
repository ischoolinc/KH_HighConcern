using FISCA.DSAUtil;
using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLock_KH.DAO
{
    public class UDTTransfer
    {
        /// <summary>
        /// 取的被鎖定班級名稱
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,UDT_ClassLock> GetClassLockNameIDDict()
        {
            Dictionary<string, UDT_ClassLock> retVal = new Dictionary<string, UDT_ClassLock>();
            AccessHelper _AccessHelper = new AccessHelper();
            foreach (UDT_ClassLock data in _AccessHelper.Select<UDT_ClassLock>())
                retVal.Add(data.ClassID,data);
            return retVal;
        }

        /// <summary>
        /// 建立使用到的 UDT Table
        /// </summary>
        public static void CreateUDTTable()
        {
            FISCA.UDT.SchemaManager Manager = new SchemaManager(new DSConnection(FISCA.Authentication.DSAServices.DefaultDataSource));
            Manager.SyncSchema(new UDT_ClassLock());
            Manager.SyncSchema(new UDT_ClassLock_Log());
        }

        /// <summary>
        /// 全部班級解鎖
        /// </summary>
        public static void UnlockAllClass()
        {
            AccessHelper _AccessHelper = new AccessHelper();
            List<UDT_ClassLock> dataList = _AccessHelper.Select<UDT_ClassLock>();
            foreach (UDT_ClassLock data in dataList)
                data.Deleted = true;

            dataList.SaveAll();
        }

        /// <summary>
        /// 取的被鎖定班級
        /// </summary>
        /// <returns></returns>
        public static List<UDT_ClassLock> GetClassLocList()
        {
            List<UDT_ClassLock> retVal = new List<UDT_ClassLock>();
            AccessHelper _AccessHelper = new AccessHelper();
            retVal = _AccessHelper.Select<UDT_ClassLock>();

            return retVal;
        }

        /// <summary>
        /// 取得解鎖
        /// </summary>
        /// <returns></returns>
        public static List<UDT_ClassLock_Log> GetClassLock_LogList()
        {
            List<UDT_ClassLock_Log> retVal = new List<UDT_ClassLock_Log>();
            AccessHelper _AccessHelper = new AccessHelper();
            retVal = _AccessHelper.Select<UDT_ClassLock_Log>();

            return retVal;
        }
    }
}

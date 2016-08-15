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
            Manager.SyncSchema(new UDT_ClassSpecial());
        }

        /// <summary>
        /// 全部班級解鎖(會過濾不自動解鎖)
        /// </summary>
        public static void UnlockAllClass()
        {
            AccessHelper _AccessHelper = new AccessHelper();
            List<UDT_ClassLock> dataList = _AccessHelper.Select<UDT_ClassLock>();
            foreach (UDT_ClassLock data in dataList)
            {
                // 當不自動解鎖跳過
                if (data.UnAutoUnlock)
                    continue;

                data.isLock = false;
            }
            dataList.SaveAll();
        }

        /// <summary>
        /// 取得所有鎖定/解鎖班級
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
        /// 取得所有鎖定解鎖紀錄
        /// </summary>
        /// <returns></returns>
        public static List<UDT_ClassLock_Log> GetClassLock_LogList()
        {
            List<UDT_ClassLock_Log> retVal = new List<UDT_ClassLock_Log>();
            AccessHelper _AccessHelper = new AccessHelper();
            retVal = _AccessHelper.Select<UDT_ClassLock_Log>();

            return retVal;
        }

        /// <summary>
        /// 取得班級學生變動
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public static UDT_ClassSpecial GetClassSpecStudentByStudID(string StudentID)
        {
            UDT_ClassSpecial value = new UDT_ClassSpecial();

            if(!string.IsNullOrWhiteSpace(StudentID))
            {
                value.StudentID = int.Parse(StudentID);

                AccessHelper accHelper = new AccessHelper();
                string query = "ref_student_id=" + StudentID;
                List<UDT_ClassSpecial> dataList = accHelper.Select<UDT_ClassSpecial>(query);
                if (dataList.Count > 0)
                    value = dataList[0];
            }
            return value;
        }

        /// <summary>
        /// 新增單筆學生變動資料
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="oldClassID"></param>
        /// <param name="ClassID"></param>
        /// <param name="OldClassName"></param>
        /// <param name="ClassName"></param>
        public static void AddClassSpecStudent(string StudentID,string OldClassID,string ClassID,string OldClassName,string ClassName)
        {
            // 儲存班級學生變動
            UDT_ClassSpecial ClassSpecStud = GetClassSpecStudentByStudID(StudentID);
            ClassSpecStud.OldClassComment = ClassSpecStud.ClassComment;
            if(!string.IsNullOrEmpty(OldClassID))
                ClassSpecStud.OldClassID = int.Parse(OldClassID);
            
            if(!string.IsNullOrEmpty(ClassID))
                ClassSpecStud.ClassID = int.Parse(ClassID);
            ClassSpecStud.OldClassName = OldClassName;
            ClassSpecStud.ClassName = ClassName;


            // 取得班級鎖定相關資料
            Dictionary<string, UDT_ClassLock> classLockDict = GetClassLockNameIDDict();
            ClassSpecStud.ClassComment = "";
            if (classLockDict.ContainsKey(ClassID))
            {
                ClassSpecStud.ClassComment = classLockDict[ClassID].Comment;
            }

            if(classLockDict.ContainsKey(OldClassID))
            {
                ClassSpecStud.OldClassComment = classLockDict[OldClassID].Comment;
            }

            ClassSpecStud.Save();
        }

    }
}

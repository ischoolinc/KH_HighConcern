using FISCA.DSAUtil;
using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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
                if(!retVal.ContainsKey(data.ClassID))
                retVal.Add(data.ClassID,data);
            return retVal;
        }


        public static UDT_ClassLock GetClassLockByClassID(string ClassID)
        {
            UDT_ClassLock value = new UDT_ClassLock();
            value.ClassID = ClassID;
            AccessHelper accHelper = new AccessHelper();
            string query = "class_id='" + ClassID + "'";
            List<UDT_ClassLock> dataList = accHelper.Select<UDT_ClassLock>(query);
            if (dataList.Count > 0)
                value = dataList[0];

            return value;
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
        /// 透過多筆學生ID取得班級學生變動
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string,UDT_ClassSpecial> GetClassSpecStudentByIDList(List<string> StudentIDList)
        {
            Dictionary<string, UDT_ClassSpecial> value = new Dictionary<string, UDT_ClassSpecial>();
            if(StudentIDList.Count>0)
            {
                AccessHelper accHelper = new AccessHelper();
                string query ="ref_student_id in("+string.Join(",",StudentIDList.ToArray())+")";
                List<UDT_ClassSpecial> dataList = accHelper.Select<UDT_ClassSpecial>(query);
                foreach(UDT_ClassSpecial data in dataList)
                {
                    string SID = data.StudentID.ToString();
                    if (!value.ContainsKey(SID))
                        value.Add(SID, data);
                }
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
        public static UDT_ClassSpecial AddClassSpecStudent(string StudentID, string OldClassID, string ClassID, string OldClassName, string ClassName, string FirstClassName, string SecondClassName, string ThridClassName)
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

            XElement elmRoot = null;
            // 儲存學生班級順位名稱
            if(string.IsNullOrEmpty(ClassSpecStud.Content))
            {
                elmRoot = new XElement("Content");
            }
            else
            {
                try
                {
                    elmRoot = XElement.Parse(ClassSpecStud.Content);
                }
                catch (Exception ex) { }
            }

            if(elmRoot !=null)
            {
                elmRoot.SetElementValue("FirstClassName", FirstClassName);
                elmRoot.SetElementValue("SecondClassName", SecondClassName);
                elmRoot.SetElementValue("ThridClassName", ThridClassName);
            }
            ClassSpecStud.Content = elmRoot.ToString();

            ClassSpecStud.Save();

            return ClassSpecStud;
        }

    }
}

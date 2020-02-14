using FISCA.DSAUtil;
using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FISCA.Data;
using System.Data;

namespace ClassLock_KH.DAO
{
    public class UDTTransfer
    {
        /// <summary>
        /// 取的被鎖定班級名稱
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, UDT_ClassLock> GetClassLockNameIDDict()
        {
            Dictionary<string, UDT_ClassLock> retVal = new Dictionary<string, UDT_ClassLock>();
            AccessHelper _AccessHelper = new AccessHelper();
            foreach (UDT_ClassLock data in _AccessHelper.Select<UDT_ClassLock>())
                if (!retVal.ContainsKey(data.ClassID))
                    retVal.Add(data.ClassID, data);
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

            if (!string.IsNullOrWhiteSpace(StudentID))
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
        public static Dictionary<string, UDT_ClassSpecial> GetClassSpecStudentByIDList(List<string> StudentIDList)
        {
            Dictionary<string, UDT_ClassSpecial> value = new Dictionary<string, UDT_ClassSpecial>();
            if (StudentIDList.Count > 0)
            {
                AccessHelper accHelper = new AccessHelper();
                string query = "ref_student_id in(" + string.Join(",", StudentIDList.ToArray()) + ")";
                List<UDT_ClassSpecial> dataList = accHelper.Select<UDT_ClassSpecial>(query);
                foreach (UDT_ClassSpecial data in dataList)
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
            if (!string.IsNullOrEmpty(OldClassID))
                ClassSpecStud.OldClassID = int.Parse(OldClassID);

            if (!string.IsNullOrEmpty(ClassID))
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

            if (classLockDict.ContainsKey(OldClassID))
            {
                ClassSpecStud.OldClassComment = classLockDict[OldClassID].Comment;
            }

            XElement elmRoot = null;
            // 儲存學生班級順位名稱
            if (string.IsNullOrEmpty(ClassSpecStud.Content))
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

            if (elmRoot != null)
            {
                elmRoot.SetElementValue("FirstClassName", FirstClassName);
                elmRoot.SetElementValue("SecondClassName", SecondClassName);
                elmRoot.SetElementValue("ThridClassName", ThridClassName);
            }
            ClassSpecStud.Content = elmRoot.ToString();

            ClassSpecStud.Save();

            return ClassSpecStud;
        }

        /// <summary>
        /// 是否自己鎖班之後會超過1/2
        /// </summary>
        /// <returns></returns>
        public static Boolean CheckIfOneHalf(string classID)
        {
            Boolean result = false;
            QueryHelper qh = new QueryHelper();
            List<string> list = new List<string>();
            string sql = @"
WITH   class_lock AS (
    SELECT 
		    class.id 
		    ,class.grade_year
		    ,class.class_name AS now_class_name 
		    ,$kh.automatic.class.lock.*
    FROM
	    class
    LEFT JOIN
	    $kh.automatic.class.lock 
    ON 	class.id = $kh.automatic.class.lock.class_id::INT  
),gradeYear_matrix   AS (
    SELECT count (*)  FROM class_lock WHERE 
	    grade_year =(SELECT grade_year  FROM class WHERE  id = {0}   ) 
	    AND ( class_lock.unauto_unlock =false  OR  unauto_unlock IS NULL)
),gradeYear_molecule  AS (
    SELECT count (*)  FROM class_lock WHERE 
	    grade_year =(SELECT grade_year  FROM class WHERE  id = {0} ) 
	    AND class_lock.unauto_unlock = false 
	    AND class_lock.is_lock= true 
)SELECT 
((gradeYear_molecule.count)+1) ::Decimal  /(gradeYear_matrix.count)
FROM 
		gradeYear_molecule 
	CROSS JOIN 
		gradeYear_matrix
";

            sql = string.Format(sql, classID);

            DataTable dt = qh.Select(sql);

            string count = dt.Rows[0][0] + "";

            double lockCountRate;
            Double.TryParse(count, out lockCountRate);
            if (lockCountRate > 0.5)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 取消鎖班申請
        /// </summary>
        /// <param name="classID"></param>
        public static void CancelAppling(string classID)
        {
            QueryHelper qh = new QueryHelper();
            string sql = $"UPDATE  $kh.automatic.class.lock  SET  lock_appling_status = NULL , comment = NULL WHERE class_id = '{classID}'  RETURNING *";

            qh.Select(sql);

        }



        /// <summary>
        /// 確認本班是否解鎖申請中 
        /// </summary>
        /// <param name="classID"></param>
        public static Boolean CheckIsUnlockAppling(string classID)
        {
            Boolean isAppling;
            QueryHelper qh = new QueryHelper();
            string sql = $"SELECT   lock_appling_status FROM $kh.automatic.class.lock WHERE  class_id = '{classID}'  ";
            DataTable dt = qh.Select(sql);
            if (dt.Rows.Count != 0)
            {
                isAppling = ((dt.Rows[0][0] + "" == "鎖班申請中_鎖班數超過二分之一" )|| (dt.Rows[0][0] + "" =="鎖班申請退回_鎖班數超過二分之一")) ? true : false;
            }
            else
            {
                isAppling = false;
            }
            return isAppling;
        }

        /// <summary>
        /// 透過班級ID調整所屬班級學生ID
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="OldClassComment"></param>
        /// <param name="ClassComment"></param>
        public static void UpdateUDTClassSepcByClassID(int ClassID, string ClassName, string OldClassComment, string ClassComment)
        {
            try
            {
                List<string> StudentIDList = new List<string>();
                // 取得班級學生ID,學生狀態：一般、休學、中輟
                QueryHelper qh = new QueryHelper();
                string query = "select id from student where student.status in(1,4,8) and ref_class_id=" + ClassID;
                DataTable dt = qh.Select(query);

                foreach (DataRow dr in dt.Rows)
                    StudentIDList.Add(dr[0].ToString());

                // 取得特殊班學生資料
                Dictionary<string, UDT_ClassSpecial> StudSpecDict = GetClassSpecStudentByIDList(StudentIDList);

                List<UDT_ClassSpecial> StudSpecDataList = new List<UDT_ClassSpecial>();

                foreach (string StudID in StudentIDList)
                {
                    if (StudSpecDict.ContainsKey(StudID))
                    {
                        StudSpecDict[StudID].OldClassID = StudSpecDict[StudID].ClassID;
                        StudSpecDict[StudID].OldClassComment = StudSpecDict[StudID].ClassComment;
                        StudSpecDict[StudID].OldClassName = StudSpecDict[StudID].ClassName;
                        StudSpecDict[StudID].ClassID = ClassID;
                        StudSpecDict[StudID].ClassName = ClassName;
                        StudSpecDict[StudID].ClassComment = ClassComment;
                        StudSpecDataList.Add(StudSpecDict[StudID]);
                    }
                    else
                    {
                        UDT_ClassSpecial cs = new UDT_ClassSpecial();
                        cs.StudentID = int.Parse(StudID);
                        cs.ClassID = ClassID;
                        cs.ClassName = ClassName;
                        cs.ClassComment = ClassComment;
                        StudSpecDataList.Add(cs);
                    }
                }
                if (StudSpecDataList.Count > 0)
                {
                    StudSpecDataList.SaveAll();
                }

            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("鎖定班級寫入資料發生錯誤," + ex.Message);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Data;

namespace KH_HighConcernCalc
{
    public class Calc
    {
        /// <summary>
        /// 透過年及取得編班人數
        /// </summary>
        /// <param name="GradeYear"></param>
        /// <returns></returns>
        public static List<ClassStudent> GetClassStudentList(string GradeYear)       
        {
            /*
自動編班順位規則 （判斷順序）
1. 排除鎖定班級
2. 編班人數少的班級優先
3. 若編班人數相同，則無特殊生（高關懷學生）班級優先
4-1. 若皆無特殊生，則班號小者優先
4-2. 若有特殊生，則以實際人數少優先
4-2-1. 若有特殊生且實際人數又相同，則以班號小者優先
             */

            List<ClassStudent> retValue = new List<ClassStudent>();

            // 是否有特殊生
            bool hasSStudent = false;

            if (!string.IsNullOrWhiteSpace(GradeYear))
            {
                // 取得鎖定班級名稱
                QueryHelper qh1 = new QueryHelper();
                List<string> lockClassID = new List<string>();
                // 加入班級被鎖定判斷
                string query1 = "select class_id from $kh.automatic.class.lock where is_lock = true";
                DataTable dt1 = qh1.Select(query1);
                foreach (DataRow dr in dt1.Rows)
                    lockClassID.Add(dr["class_id"].ToString());

                // 取得班級人數(一般生,休學,輟學 1,4,8) 2015/11/30 因小組會議討論後加入休學狀態
                // 2019/02/11 穎驊執行高雄小組 [09-01][03]技藝班班級 項目 ， 
                //調整後邏輯如下
                //1.班級內完全沒有學生：顯示
                //2.班級內有一般生、休學、輟學：顯示
                //3.班級內有學生但都非一般生、休學、輟學：不顯示
                QueryHelper qh2 = new QueryHelper();
                string query2 = "select class.class_name,class.id as classid,count(student.id) as stud_count from class left join student on student.ref_class_id=class.id and student.status in(1,4,8) where class.grade_year=" + GradeYear + "  group by class.class_name,classid order by stud_count;";
                DataTable dt2 = qh2.Select(query2);
                foreach (DataRow dr in dt2.Rows)
                {
                    int csi;
                    ClassStudent cs = new ClassStudent();
                    cs.ClassID = dr["classid"].ToString();
                    cs.ClassName = dr["class_name"].ToString();
                    if (int.TryParse(cs.ClassName, out csi))
                        cs.ClassNameInt = csi;
                    cs.StudentCount = int.Parse(dr["stud_count"].ToString());
                    cs.ClassStudentCount = cs.StudentCount;
                    cs.HasHStudentCount = 0;
                    // 排除班級鎖定
                    if (!lockClassID.Contains(cs.ClassID))
                        retValue.Add(cs);
                }

                //  取得班級高關懷人數, 因小組會議討論加入休學狀態
                QueryHelper qh3 = new QueryHelper();
                string query3 = "select class.class_name,sum(number_reduce) as class_hcount,count($kh.automatic.placement.high.concern.ref_student_id) as class_hscount from $kh.automatic.placement.high.concern inner join student on to_number($kh.automatic.placement.high.concern.ref_student_id,'999999999')=student.id inner join class on student.ref_class_id=class.id where class.grade_year=" + GradeYear + " and student.status in(1,4,8) group by class.class_name;";
                DataTable dt3 = qh3.Select(query3);
                foreach (DataRow dr in dt3.Rows)
                {
                    string className = dr["class_name"].ToString();

                    int number_reduce = int.Parse(dr["class_hcount"].ToString());

                    int hscount = int.Parse(dr["class_hscount"].ToString());

                    // 加入高關懷學生
                    foreach (ClassStudent cs in retValue)
                    {
                        if (cs.ClassName == className)
                        {
                            hasSStudent = true;
                            cs.HStudentCount = number_reduce;
                            cs.ClassHStudentCount = hscount;
                            cs.ClassStudentCount += cs.HStudentCount;
                            cs.HasHStudentCount = 1;
                            break;
                        }
                    }
                }

                bool classNameInt = true;

                // 檢查班級名稱是否有非數字
                foreach (ClassStudent cs in retValue)
                {
                    if (cs.ClassNameInt == 0)
                    {
                        classNameInt = false;
                        break;
                    }
                }


                if (classNameInt)
                {
                    // 班級名稱是數字處理方式
                    // 有特殊生
                    if (hasSStudent)
                    {
                        //// 編班人數排序 (先編班人數小>大,沒有高關懷人在前，高關懷人數小到大,班級名稱數字小在前)
                        //retValue = (from data in retValue orderby data.ClassStudentCount ascending, data.HasHStudentCount ascending, data.ClassHStudentCount ascending, data.ClassNameInt ascending select data).ToList();

                        // 2014/11/11,高雄小組會議討論後調整：編班人數排序 (先編班人數小>大,沒有高關懷人在前，班級名稱數字小在前)
                        retValue = (from data in retValue orderby data.ClassStudentCount ascending, data.HasHStudentCount ascending, data.ClassNameInt ascending select data).ToList();
                    }
                    else
                    {
                        // 編班人數排序 (先編班人數小>大,班級名稱數字小在前)
                        retValue = (from data in retValue orderby data.ClassStudentCount ascending, data.ClassNameInt ascending select data).ToList();
                    }
                }
                else
                { 
                    // 班級名稱非數字處理方式
                    Dictionary<string, int> classOrderDict = new Dictionary<string, int>();
                    QueryHelper qh4 = new QueryHelper();
                    string query4 = "select id,display_order from class where display_order is not null;";
                    DataTable dt4 =qh4.Select(query4);

                    foreach(DataRow dr in dt4.Rows)
                    {
                        int ii;
                        if(int.TryParse(dr["display_order"].ToString(),out ii))
                        {
                            classOrderDict.Add(dr["id"].ToString(),ii);
                        }
                    }
                    
                    // 加入班級排序
                    foreach(ClassStudent cs in retValue)
                    {
                        cs.ClassOrder=999;
                        if(classOrderDict.ContainsKey(cs.ClassID))
                            cs.ClassOrder=classOrderDict[cs.ClassID];
                    }
                // 有特殊生
                    if (hasSStudent)
                    {
                        // 2014/11/11,高雄小組會議討論修改：編班人數排序 (先編班人數小>大,沒有高關懷人在前，班級排序數字小在前)
                        retValue = (from data in retValue orderby data.ClassStudentCount ascending, data.HasHStudentCount ascending, data.ClassOrder ascending select data).ToList();
                    }
                    else
                    {
                        // 編班人數排序 (先編班人數小>大,班級排序數字小在前)
                        retValue = (from data in retValue orderby data.ClassStudentCount ascending, data.ClassOrder ascending select data).ToList();
                    }
                }
            }
            return retValue;
        }

        /// <summary>
        /// 取得所有編班人數
        /// </summary>
        /// <returns></returns>
        public static List<ClassStudent> GetClassStudentAllList()
        {
            List<ClassStudent> retValue = new List<ClassStudent>();

                // 取得鎖定班級名稱
                QueryHelper qh1 = new QueryHelper();
                List<string> lockClassId = new List<string>();
                // 加入班級被鎖定判斷
                string query1 = "select class_id from $kh.automatic.class.lock where is_lock = true";
                DataTable dt1 = qh1.Select(query1);
                foreach (DataRow dr in dt1.Rows)
                    lockClassId.Add(dr["class_id"].ToString());

                // 取得班級人數(一般生,休學,輟學 1,4,8) ,因小組會議討論加入休學狀態
                QueryHelper qh2 = new QueryHelper();
                string query2 = "select class.class_name,class.id as classid,count(student.id) as stud_count from student inner join class on student.ref_class_id=class.id where student.status in(1,4,8) group by class.class_name,classid order by stud_count;";
                DataTable dt2 = qh2.Select(query2);
                foreach (DataRow dr in dt2.Rows)
                {
                    int csi;
                    ClassStudent cs = new ClassStudent();
                    cs.ClassID = dr["classid"].ToString();
                    cs.ClassName = dr["class_name"].ToString();
                    if (int.TryParse(cs.ClassName, out csi))
                        cs.ClassNameInt = csi;
                    cs.StudentCount = int.Parse(dr["stud_count"].ToString());
                    cs.ClassStudentCount = cs.StudentCount;
                    cs.HasHStudentCount = 0;
                    

                    // 排除班級鎖定
                    if (!lockClassId.Contains(cs.ClassID))
                        retValue.Add(cs);
                }

                //  取得班級高關懷人數,因小組會議討論加入休學狀態,2015/11/30
                QueryHelper qh3 = new QueryHelper();
                string query3 = "select class.class_name,sum(number_reduce) as class_hcount,count($kh.automatic.placement.high.concern.ref_student_id) as class_hscount from $kh.automatic.placement.high.concern inner join student on to_number($kh.automatic.placement.high.concern.ref_student_id,'999999999')=student.id inner join class on student.ref_class_id=class.id where student.status in(1,4,8) group by class.class_name;";
                DataTable dt3 = qh3.Select(query3);
                foreach (DataRow dr in dt3.Rows)
                {
                    string className = dr["class_name"].ToString();

                    int number_reduce = int.Parse(dr["class_hcount"].ToString());

                    int hscount = int.Parse(dr["class_hscount"].ToString());

                    // 加入高關懷學生
                    foreach (ClassStudent cs in retValue)
                    {
                        if (cs.ClassName == className)
                        {                            
                            cs.HStudentCount = number_reduce;
                            cs.ClassHStudentCount = hscount;
                            cs.ClassStudentCount += cs.HStudentCount;
                            cs.HasHStudentCount = 1;
                            break;
                        }
                    }
                }        
            
            return retValue;
        }

        /// <summary>
        /// 取得所有編班人數(Classid,ClassStudent)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,ClassStudent> GetClassStudentAllIDDict()
        {
            Dictionary<string, ClassStudent> retValue = new Dictionary<string, ClassStudent>();

            //// 取得鎖定班級名稱
            //QueryHelper qh1 = new QueryHelper();
            //List<string> lockClassID = new List<string>();
            //string query1 = "select class_id from $kh.automatic.class.lock";
            //DataTable dt1 = qh1.Select(query1);
            //foreach (DataRow dr in dt1.Rows)
            //    lockClassID.Add(dr["class_id"].ToString());

            // 取得班級人數(一般生、休學,輟學 1,4,8),因小組會議討論加入休學狀態,2015/11/30
            QueryHelper qh2 = new QueryHelper();
            string query2 = "select class.class_name,class.id as classid,count(student.id) as stud_count from student inner join class on student.ref_class_id=class.id where student.status in(1,4,8) group by class.class_name,classid order by stud_count;";
            DataTable dt2 = qh2.Select(query2);
            foreach (DataRow dr in dt2.Rows)
            {
                int csi;
                ClassStudent cs = new ClassStudent();
                cs.ClassID = dr["classid"].ToString();
                cs.ClassName = dr["class_name"].ToString();
                if (int.TryParse(cs.ClassName, out csi))
                    cs.ClassNameInt = csi;
                cs.StudentCount = int.Parse(dr["stud_count"].ToString());
                cs.ClassStudentCount = cs.StudentCount;
                cs.ClassStudentCountStr = cs.StudentCount.ToString();
                cs.HasHStudentCount = 0;
                //// 排除班級鎖定
                //if (!lockClassID.Contains(cs.ClassID))
                    retValue.Add(cs.ClassID,cs);
            }

            //  取得班級高關懷人數,因小組會議討論加入休學狀態,2015/11/30
            QueryHelper qh3 = new QueryHelper();
            string query3 = "select class.id as class_id,sum(number_reduce) as class_hcount,count($kh.automatic.placement.high.concern.ref_student_id) as class_hscount from $kh.automatic.placement.high.concern inner join student on to_number($kh.automatic.placement.high.concern.ref_student_id,'999999999')=student.id inner join class on student.ref_class_id=class.id and student.status in(1,4,8) group by class_id;";
            DataTable dt3 = qh3.Select(query3);
            foreach (DataRow dr in dt3.Rows)
            {
                string classID = dr["class_id"].ToString();

                int number_reduce = int.Parse(dr["class_hcount"].ToString());
                int sCount = int.Parse(dr["class_hscount"].ToString());


                // 加入高關懷學生
                if(retValue.ContainsKey(classID))
                {
                    retValue[classID].HStudentCount = number_reduce;
                    retValue[classID].ClassHStudentCount = sCount;
                    string str1 = "(" + retValue[classID].ClassStudentCount + "+" + number_reduce + ")";
                    retValue[classID].ClassStudentCount += retValue[classID].HStudentCount;
                    retValue[classID].ClassStudentCountStr = retValue[classID].ClassStudentCount + str1;
                    // 有高關懷學生
                    retValue[classID].HasHStudentCount = 1;
                }
            }

            return retValue;
        }
    }
}

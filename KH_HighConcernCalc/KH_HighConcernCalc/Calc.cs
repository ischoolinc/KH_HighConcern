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
                List<string> lockClassName = new List<string>();
                string query1 = "select class_name from $kh.automatic.class.lock";
                DataTable dt1 = qh1.Select(query1);
                foreach (DataRow dr in dt1.Rows)
                    lockClassName.Add(dr["class_name"].ToString());

                // 取得班級人數(一般生)
                QueryHelper qh2 = new QueryHelper();
                string query2 = "select class.class_name,class.id as classid,count(student.id) as stud_count from student inner join class on student.ref_class_id=class.id where class.grade_year=" + GradeYear + " and student.status=1 group by class.class_name,classid order by stud_count;";
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

                    // 排除班級鎖定
                    if(!lockClassName.Contains(cs.ClassName))
                        retValue.Add(cs);
                }

                //  取得班級高關懷人數
                QueryHelper qh3 = new QueryHelper();
                string query3 = "select class.class_name,sum(number_reduce) as class_hcount from $kh.automatic.placement.high.concern inner join student on to_number($kh.automatic.placement.high.concern.ref_student_id,'999999999')=student.id inner join class on student.ref_class_id=class.id where class.grade_year="+GradeYear+"  group by class.class_name;";
                DataTable dt3 = qh3.Select(query3);
                foreach (DataRow dr in dt3.Rows)
                {
                    string className = dr["class_name"].ToString();

                    int number_reduce = int.Parse(dr["class_hcount"].ToString());

                    // 加入高關懷學生
                    foreach (ClassStudent cs in retValue)
                    {
                        if (cs.ClassName == className)
                        {
                            hasSStudent = true;
                            cs.HStudentCount = number_reduce;
                            cs.ClassStudentCount += cs.HStudentCount;                            
                            break;
                        }
                    }
                }

                // 有特殊生
                if (hasSStudent)
                {
                    // 編班人數排序 (先編班人數小>大,實際人數小(高關懷人數多>少),班級名稱數字小在前)
                    retValue = (from data in retValue orderby data.ClassStudentCount ascending, data.HStudentCount descending ,data.ClassNameInt ascending select data).ToList();
                }
                else
                {
                    // 編班人數排序 (先編班人數小>大,班級名稱數字小在前)
                    retValue = (from data in retValue orderby data.ClassStudentCount ascending,data.ClassNameInt ascending select data).ToList();
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
                List<string> lockClassName = new List<string>();
                string query1 = "select class_name from $kh.automatic.class.lock";
                DataTable dt1 = qh1.Select(query1);
                foreach (DataRow dr in dt1.Rows)
                    lockClassName.Add(dr["class_name"].ToString());

                // 取得班級人數(一般生)
                QueryHelper qh2 = new QueryHelper();
                string query2 = "select class.class_name,class.id as classid,count(student.id) as stud_count from student inner join class on student.ref_class_id=class.id where student.status=1 group by class.class_name,classid order by stud_count;";
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

                    // 排除班級鎖定
                    if (!lockClassName.Contains(cs.ClassName))
                        retValue.Add(cs);
                }

                //  取得班級高關懷人數
                QueryHelper qh3 = new QueryHelper();
                string query3 = "select class.class_name,sum(number_reduce) as class_hcount from $kh.automatic.placement.high.concern inner join student on to_number($kh.automatic.placement.high.concern.ref_student_id,'999999999')=student.id inner join class on student.ref_class_id=class.id  group by class.class_name;";
                DataTable dt3 = qh3.Select(query3);
                foreach (DataRow dr in dt3.Rows)
                {
                    string className = dr["class_name"].ToString();

                    int number_reduce = int.Parse(dr["class_hcount"].ToString());

                    // 加入高關懷學生
                    foreach (ClassStudent cs in retValue)
                    {
                        if (cs.ClassName == className)
                        {                            
                            cs.HStudentCount = number_reduce;
                            cs.ClassStudentCount += cs.HStudentCount;
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
            //List<string> lockClassName = new List<string>();
            //string query1 = "select class_name from $kh.automatic.class.lock";
            //DataTable dt1 = qh1.Select(query1);
            //foreach (DataRow dr in dt1.Rows)
            //    lockClassName.Add(dr["class_name"].ToString());

            // 取得班級人數(一般生)
            QueryHelper qh2 = new QueryHelper();
            string query2 = "select class.class_name,class.id as classid,count(student.id) as stud_count from student inner join class on student.ref_class_id=class.id where student.status=1 group by class.class_name,classid order by stud_count;";
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

                //// 排除班級鎖定
                //if (!lockClassName.Contains(cs.ClassName))
                    retValue.Add(cs.ClassID,cs);
            }

            //  取得班級高關懷人數
            QueryHelper qh3 = new QueryHelper();
            string query3 = "select class.id as class_id,sum(number_reduce) as class_hcount from $kh.automatic.placement.high.concern inner join student on to_number($kh.automatic.placement.high.concern.ref_student_id,'999999999')=student.id inner join class on student.ref_class_id=class.id  group by class_id;";
            DataTable dt3 = qh3.Select(query3);
            foreach (DataRow dr in dt3.Rows)
            {
                string classID = dr["class_id"].ToString();

                int number_reduce = int.Parse(dr["class_hcount"].ToString());

                // 加入高關懷學生
                if(retValue.ContainsKey(classID))
                {
                    retValue[classID].HStudentCount = number_reduce;
                    retValue[classID].ClassStudentCount += retValue[classID].HStudentCount;
                }
            }

            return retValue;
        }
    }
}

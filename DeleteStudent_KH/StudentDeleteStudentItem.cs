using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRewriteAPI_JH;
using JHSchool.Data;

namespace DeleteStudent_KH
{
    class StudentDeleteStudentItem : IStudentDeleteStudentAPI
    {
        public FISCA.Presentation.Controls.BaseForm CreateForm()
        {
            return new FormDeleteStudent();
        }
    }
}

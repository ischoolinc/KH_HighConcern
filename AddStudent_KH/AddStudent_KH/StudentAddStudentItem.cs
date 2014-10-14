using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRewriteAPI_JH;

namespace AddStudent_KH
{
    public class StudentAddStudentItem:IStudentAddStudentAPI
    {
        public FISCA.Presentation.Controls.BaseForm CreateForm()
        {
            return new FormAddStudent();
        }
    }
}

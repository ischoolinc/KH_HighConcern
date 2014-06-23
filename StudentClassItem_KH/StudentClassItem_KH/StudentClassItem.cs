using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRewriteAPI_JH;

namespace StudentClassItem_KH
{
    public class StudentClassItem:IStudentClassDetailItemAPI
    {
        public FISCA.Presentation.IDetailBulider CreateBasicInfo()
        {
            return new FISCA.Presentation.DetailBulider<StudentClassItemContent>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IKHAPI_JH;
namespace StudentClassItem_KH
{
    public class StudentClassItem:DetailItemAPI
    {
        public FISCA.Presentation.IDetailBulider CreateBasicInfo()
        {
            return new FISCA.Presentation.DetailBulider<StudentClassItemContent>();
        }
    }
}

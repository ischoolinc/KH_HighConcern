using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tagging;

namespace StudentChangeStatus_KH
{
    public class StudentChangeStatusBarBuilder : IStudentChangeStatusAPI
    {

        public IDescriptionPaneBulider CreateBasicInfo()
        {
            return new StudentChangeStatusStudentBar();
        }
    }
}

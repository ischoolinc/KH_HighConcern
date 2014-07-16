using FISCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTransferAPI;

namespace StudentTransferStudentBrief_KH
{
    public class StudentTransferStudentBriefItem:StudentTransferAPI.IStudentBriefBaseAPI
    {

        public StudentTransferAPI.WizardForm CreateForm(ArgDictionary ars)
        {
            return new StudentBrief(ars);
        }
    }
}

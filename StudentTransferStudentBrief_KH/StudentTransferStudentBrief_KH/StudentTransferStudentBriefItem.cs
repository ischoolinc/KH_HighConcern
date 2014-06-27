using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTransferAPI;

namespace StudentTransferStudentBrief_KH
{
    public class StudentTransferStudentBriefItem:IStudentTransferStudentBriefAPI
    {
        public WizardForm CreateWizardForm(FISCA.ArgDictionary args)
        {
            return new StudentBrief(args);
        }
    }
}

using IRewriteAPI_JH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentImportWizard_KH
{
    public class StudentImportWizardItem:IStudentImportWizardAPI
    {
        public FISCA.Presentation.Controls.BaseForm CreateForm()
        {
            return new StudentImportWizard();
        }
    }
}

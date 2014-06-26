using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRewriteAPI_JH;

namespace StudentTransStudBase_KH
{
    public class StudentTransStudBaseItem:IRewriteAPI_JH.IStudentTransStudBaseAPI{
        
        public ITransStudBase CreateForm()
        {
            return new AddTransStudBase();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.DocumentValidator;

namespace KH_HighConcern.ImportExport.ValidationRule
{
    public class RowValidatorFactory : IRowValidatorFactory
    {
        public IRowVaildator CreateRowValidator(string typeName, System.Xml.XmlElement validatorDescription)
        {
            switch (typeName.ToUpper())
            {
                //case "COURSENAMECHECK":
                //    return new CourseNameCheck();
                default:
                    return null;
            }
        }
    }
}

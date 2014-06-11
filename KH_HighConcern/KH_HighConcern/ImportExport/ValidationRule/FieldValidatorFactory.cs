using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.DocumentValidator;

namespace KH_HighConcern.ImportExport.ValidationRule
{
    public class FieldValidatorFactory : IFieldValidatorFactory
    {

        public IFieldValidator CreateFieldValidator(string typeName, System.Xml.XmlElement validatorDescription)
        {
            switch (typeName.ToUpper())
            {
                case "HIGHCONCERNSTUDENTNUMBERCHECK":
                    return new StudentNumberCheck();               
                default:
                    return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.DocumentValidator;
using Campus.Import;
using KH_HighConcern.DAO;

namespace KH_HighConcern.ImportExport
{
    public class ImportHighConcern : ImportWizard
    {
        private ImportOption _Option;

        public override ImportAction GetSupportActions()
        {
            return ImportAction.InsertOrUpdate;
        }

        public ImportHighConcern()
        {
            this.IsSplit = false;
            this.IsLog = false;
        }

        public override string GetValidateRule()
        {
            return Properties.Resources.HighConcernValDef;
        }

        public override string Import(List<IRowStream> Rows)
        {
       
            return "";
        }

        /// <summary>
        /// 匯入前準備
        /// </summary>
        /// <param name="Option"></param>
        public override void Prepare(ImportOption Option)
        {
            _Option = Option;          
        }
    }
}

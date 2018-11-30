using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRewriteAPI_JH;

namespace ClassBaseInfoItem_KH
{
    public class ClassItem:IClassBaseInfoItemAPI
    {
        public FISCA.Presentation.IDetailBulider CreateBasicInfo()
        {
            return new FISCA.Presentation.DetailBulider<ClassBaseInfoItem>();
        }
    }
}

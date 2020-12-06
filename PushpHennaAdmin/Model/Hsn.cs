using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class Hsn
    {
    }

    #region HSN Master

    public class HsnModel : CommanProperty
    {
        public int HSN_ID { get; set; }
        public int TAX_ID { get; set; }
        public string HSN_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        //public string HSNREMARK { get; set; }
        public bool ISEXEMPTED { get; set; }
    }

    #endregion
}

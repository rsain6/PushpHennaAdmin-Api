using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    #region Tax Master
    public class Tax : CommanProperty
    {
        public int TAXID { get; set; }
        public decimal TAXVAL { get; set; }
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    #region HSN Master
    public class Group
    {
        public int MKTGROUP_ID { get; set; }
        public string MKTGROUPTYPE { get; set; }
        public string ALIAS { get; set; }
        public string NAME { get; set; }
        public string MODE { get; set; }
        public string REMARKS { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
        public string RESULTMSG { get; set; }
    }
    #endregion
}

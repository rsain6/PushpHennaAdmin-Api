using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class BasicModel
    {
    }

    public class CommanProperty
    {
        public string NAME { get; set; }
        public string MODE { get; set; }
        public string REMARK { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
        public string RESULTMSG { get; set; }
    }

    public class GridListModel
    {
        public string GODOWNSTR { get; set; }
        public int PAGEINDEX { get; set; }
        public int PAGESIZE { get; set; }
        public int MSTCATID { get; set; }
        public int CATID { get; set; }
        public int ID { get; set; }
        public int CREDITBUYERTYPE { get; set; }
        public string MODE { get; set; }
        public int DISPENSARYID { get; set; }
    }
    public class GridListFilterModel
    {
        public int PAGEINDEX { get; set; }
        public int PAGESIZE { get; set; }
        public string NAME { get; set; }
    }
    public class MasterApprovalModel
    {
        public int OBJECTID { get; set; }
        public string OBJECTNAME { get; set; }
        public string REMARKS { get; set; }
        public int STATUSID { get; set; }
        public int ISACTIVE { get; set; }
        public int CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
    }

    public class FilterProductModel
    {
        public string CREATEDBY { get; set; }
        public string PRODNAME { get; set; }
        public string DISCOUNT_FOR { get; set; }
    }

    public class FilterDropDown
    {
        public string FilterStr { get; set; }
    }
}

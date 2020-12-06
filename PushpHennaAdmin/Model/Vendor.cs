using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class Vendor
    {
    }

    public class VendorModel
    {
        public int VENDOR_ID { get; set; }
        public string COMPANY_NAME { get; set; }
        public string PERSON_NAME { get; set; }
        public string MOBILE { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string PINCODE { get; set; }
        public int STATEID { get; set; }
        public int CITYID { get; set; }
        public int DISTRICT_ID { get; set; }
        public string REMARKS { get; set; }
        public string GSTIN { get; set; }
        public bool ISIGSTAPPLY { get; set; }
        public string MSTCATIDSTR { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
        public string RESULTMSG { get; set; }
    }

    public class VendorStatus
    {
        public int VENDOR_ID { get; set; }
        public int ISACTIVE { get; set; }
    }
    public class VendorGroupMap
    {
        public int? MKTGROUPID { get; set; }
        public string VENDORIDSTR { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
        public string RESULTMSG { get; set; }

    }
}

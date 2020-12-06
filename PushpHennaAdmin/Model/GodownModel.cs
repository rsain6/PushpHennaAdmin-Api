using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class GodownModel
    {
        public int godown_id { get; set; }
        public string godown_name { get; set; }
        public string person_name { get; set; }
        public string mobile { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string pincode { get; set; }
        public int district_id { get; set; }
        public int stateid { get; set; }
        public int cityid { get; set; }
        public string mstcatidstr { get; set; }
        public string remarks { get; set; }
        public decimal capacity_qty { get; set; }
        public int capacity_weightclassid { get; set; }
        public string GstNo { get; set; }
        public string DLNo { get; set; }
        public string TINNO { get; set; }
        public int createdby { get; set; }
        public string ipaddress { get; set; }
        public string resultmsg { get; set; }
        public bool chkisstore { get; set; }
        public int UpGodownID { get; set; }
        public int GodownTypeID { get; set; }

    }

    public class GodownStatus
    {
        public int godown_id { get; set; }
        public int GodownTypeId { get; set; }
        public int UserId { get; set; }
        public int isactive { get; set; }
    }
}

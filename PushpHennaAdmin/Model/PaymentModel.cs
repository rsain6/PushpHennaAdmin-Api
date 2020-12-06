using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class PaymentModel
    {
        public int VoucherId { get; set; }
        public int PurchaseOrder_ID { get; set; }
        public int DemandFrom_Id { get; set; }
        public string DemandFrom_By { get; set; }
        public int DemandTo_Id { get; set; }
        public string DemandTo_By { get; set; }
        public int PoStatusID { get; set; }  
        public string PO_Number { get; set; }
        public int Vendor_ID { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
    public class VoucherModel
    {
        public string poid_str { get; set; }
        public double? amount { get; set; }
        public double? discount { get; set; }
        public double? tax_amt { get; set; }
        public double? other_charg { get; set; }
        public double? netamount { get; set; }
        public int paymentmodeid { get; set; }
        public string cheque_no { get; set; }
        public string remark { get; set; }
        public DateTime? cheque_date { get; set; }
        public string createdby { get; set; }
        public string ipaddress { get; set; }
        public string resultmsg { get; set; }
    }

}

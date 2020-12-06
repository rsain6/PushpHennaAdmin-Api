using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class StockReturn
    {
        public int Prod_Id { get; set; }
        public string StockForm { get; set; }
        public int StockFormId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class PurchaseReturnModel
    {
        public int Vendor_Id { get; set; }
        public bool ISPOPAYMENTDONE { get; set; }
        public int CreatedBy { get; set; }
    }

    public class StockReturnModel
    {
        public List<ReturnJson> Product_Json { get; set; }
        public int ReturnToId { get; set; }
        public string ReturnFor { get; set; }
        public bool ISPOPAYMENTDONE { get; set; }
        public int Vendor_Id { get; set; }
        public int StockPOReturnID { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public string IpAddress { get; set; }
    }
    public class ReturnJson
    {
        public int STOCKDETAIL_ID { get; set; }
        public decimal RETURNQTY { get; set; }
    }

    public class AcceptStockModel
    {
        public string EXP_CHALLANNO { get; set; }
        public int STATUSID { get; set; }
        public string LISTFOR { get; set; }
        public int CREATEDBY { get; set; }
        public int PAGEINDEX { get; set; }
        public int PAGESIZE { get; set; }
    }

    public class AcceptStockReturn
    {
        public int STOCKRETURNDTLID { get; set; }
        public int STOCKTRANSFERDTLID { get; set; }
        public string MODE { get; set; }
        public string REMARK { get; set; }
        public int CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
    }
}


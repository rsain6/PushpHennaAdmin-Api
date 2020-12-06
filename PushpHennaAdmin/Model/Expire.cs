using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class ExpireModel
    {
        public string stockdetailid { get; set; }
        public int ExpiryId { get; set; }
        public List<stockdetailidlist> StockDetail_Ids_Str { get; set; }
        public string ExpiryIdStr { get; set; }
        public string Return_To { get; set; }
        public int Return_ToId { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public string IpAddress { get; set; }
        public string ResultMsg { get; set; }
    }

    public class stockdetailidlist
    {
        public int STOCKBATCHID { get; set; }
        public int EXPIRYID { get; set; }
    }

    public class ExpireSearchModel
    {
        public string Transfer_To { get; set; }
        public int Transfer_ToId { get; set; }
        public string Batch { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class ExpireListModel
    {
        public int VendorId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public string Challan_No { get; set; }
    }

    public class TransferToGodownModel
    {
        public List<ProductTranDetail> Product_DetailJson { get; set; }
        public int TransferTo_Id { get; set; }
        public string TransferFor { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public string IpAddress { get; set; }
        public string ResultMsg { get; set; }

        //public int DemandId { get; set; }
        //public int Demand_DId { get; set; }
        //public int StockHead_ID { get; set; }
        //public int DemandFrom_Id { get; set; }
        //public string DemandFrom_By { get; set; }
        //public int DemandTo_Id { get; set; }
        //public string DemandTo_By { get; set; }
        
        //public List<ProductTranDetail> Product_Json { get; set; }
        //public int DemandStatusID { get; set; }
     
        //public string Mode { get; set; }
        //public string IndentNo { get; set; }
        //public int pageindex { get; set; }
        //public int pagesize { get; set; }
    }
    public class ProductTranDetail
    {
        public int STOCKDETAIL_ID { get; set; }
        public decimal TRANS_QTY { get; set; }
    }
}

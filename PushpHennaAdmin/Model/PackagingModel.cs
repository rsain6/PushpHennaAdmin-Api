using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class PackagingModel
    {
        public int StockFor_ID { get; set; }
        public string StockFor { get; set; }
        public string Mode { get; set; }
        public int Prod_Id { get; set; }
    }

    public class QtyCalulation
    {
        public int Qty { get; set; }
        public int StockDetail_Id { get; set; }
        public int ToId { get; set; }
    }

    public class SavePackagingModel
    {
        public int StockForId { get; set; }
        public string StockFor { get; set; }
        public int StockDetail_Id { get; set; }
        public decimal Used_Qty { get; set; }
        public int ProdId { get; set; }
        //public int Prod_V_Id { get; set; }
        public decimal Qty { get; set; }
        public decimal? Cust_Dis { get; set; }
        public decimal? SaleRate { get; set; }
        public decimal? ActualMargin { get; set; }
        public DateTime ExpDate { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public string IpAddress { get; set; }
        public string ResultMsg { get; set; }
    }

    public class ProductStockList
    {
        public int StockForID { get; set; }
        public string StockFor { get; set; }
        public string Mode { get; set; }
        public int StockDetail_IdFrom { get; set; }
        public int StockDetail_IdTO { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
    }
}

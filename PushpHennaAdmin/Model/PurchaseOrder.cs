using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class PurchaseOrder
    {
        public int PurchaseOrder_Id { get; set; }
        public int StockHead_Id { get; set; }
        public int Demand_DID { get; set; }
        public int DemandFromId { get; set; }
        public string DemandFromBy { get; set; }
        public string PurchaseOrder_Remark { get; set; }
        public List<ProductDetail> product_detailjson { get; set; }
        public int Vendor_Id { get; set; }
        public int Postatusid { get; set; }
        public string Mode { get; set; }
        public int? MKTGROUPID { get; set; }
        public int? VENDOR_ID { get; set; }
        public string RefNo { get; set; }
        public string CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
    }



    public class ProductDetail
    {
        public int PROD_ID { get; set; }
        public decimal? QTY { get; set; }
        public decimal? PO_PRICE { get; set; }
        //public int? VENDOR_ID { get; set; }
    }

    #region Stock In - Inventory

    public class ReturnStockIn
    {
        public string Exp_ChallanNo { get; set; }
        public string InvoiceNO { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceFile { get; set; }
        public string ChallanNo { get; set; }
        public string Vendor_Remark1 { get; set; }
        public string Vendor_Remark2 { get; set; }
        public string Vendor_Remark3 { get; set; }
        public bool IsClosed { get; set; }
        public string ICRNO { get; set; }
        public List<StockInProductDetail> Product_DetailJSON { get; set; }
        public List<StockInProductDetail> freeProduct_DetailJSON { get; set; }
        public decimal? Other_Adjustment { get; set; }
        public decimal? Other_Charges { get; set; }
        public string CreatedBy { get; set; }
        public string IPAddress { get; set; }

    }


    public class StockIn
    {
        public int PurchaseOrder_Id { get; set; }
        public int DemandId { get; set; }
        public string InvoiceNO { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceFile { get; set; }
        public string ChallanNo { get; set; }
        public string Vendor_Remark1 { get; set; }
        public string Vendor_Remark2 { get; set; }
        public string Vendor_Remark3 { get; set; }
        public bool IsClosed { get; set; }
        public string ICRNO { get; set; }
        public List<StockInProductDetail> Product_DetailJSON { get; set; }
        public List<StockInProductDetail> freeProduct_DetailJSON { get; set; }
        public decimal? Other_Adjustment { get; set; }
        public decimal? Other_Charges { get; set; }
        public string CreatedBy { get; set; }
        public string IPAddress { get; set; }

    }
    public class StockInProductDetail
    {
        public int? PROD_ID { get; set; }
        public int? PROD_VID { get; set; }
        public int? PURCHASEORDER_D_ID { get; set; }
        public int? DEMAND_DID { get; set; }
        public float? PURCHASE_RATE { get; set; }
        public float? PURCHASE_MRP { get; set; }
        public float? SALE_TRADERATE { get; set; }
        public decimal? PURCHASE_CASSTAX { get; set; }
        public decimal? PURCHASE_CASSTAX_PERC { get; set; }
        public decimal? QTY { get; set; }
        public string BATCH { get; set; }
        public string COLOR { get; set; }
        public DateTime? MFGDATE { get; set; }
        public DateTime? EXPIRYDATE { get; set; }
        public string BARCODE { get; set; }
        public decimal? PURCHASE_TRADEDIS { get; set; }
        public decimal? PURCHASE_TRADEDIS_PERC { get; set; }
        public decimal? LOT_DIS { get; set; }
        public decimal? SCHEME_DIS { get; set; }
        public int SCHEME_DISCOUNTTYPEID { get; set; }
        public decimal SCHEME_DIS_AMT { get; set; }
        public decimal? CUST_DIS { get; set; }
        public decimal? SALERATE { get; set; }
        public decimal? COSTRATE { get; set; }
        public decimal? COSTRATE_WITHOUT_TAX { get; set; }
        public decimal? GST { get; set; }
        public decimal? GST_AMOUNT { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? GROSSAMOUNT { get; set; }
        public decimal? GROSSMARGIN { get; set; }
        public decimal? OTERCHARGES_PERQTY { get; set; }
        public decimal? ACTUALMARGIN { get; set; }
        public string HSN_ID { get; set; }
        public int? FREEQTY { get; set; }
        //public string HSN_CODE { get; set; }
        //public int? GST { get; set; }
    }

    public class StockDetail
    {
        public int ISEXPIRY { get; set; }
        public string PROD_NAME { get; set; }
        public string STOCKFORID { get; set; }
        public string STOCKFOR { get; set; }
        public string INDENTNO { get; set; }
        public int STOCKHEAD_ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CREATEDBY { get; set; }
        public int PAGEINDEX { get; set; }
        public int PAGESIZE { get; set; }
    }


    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class RptBillWiseSellReport
    {
        public string Prod_Name { get; set; }
        public string RptType { get; set; }
        public int? BuyerId { get; set; }
        public int? MktGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CreditType { get; set; }
        public string InvoiceNo { get; set; }
        public int UserTypeId { get; set; }
        public string NACType { get; set; }
        public string DrugType { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Mode { get; set; }
    }
    public class RptInvoiceReport
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string InvoiceNo { get; set; }
        public int WorkForID { get; set; }
        public string WorkFor { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Mode { get; set; }
    }
    public class RptInvoiceTrailReport
    {
        public int CounterSaleID { get; set; }
    }
    public class Header
    {
        public Header()
        {
            SCF_ListDetailjson = new List<SCFListDetail>();
            SCFFooter = new SCFFooter();
            SCFHeader = new SCFHeader();
        }
        public List<SCFListDetail> SCF_ListDetailjson { get; set; }
        public SCFFooter SCFFooter { get; set; }
        public SCFHeader SCFHeader { get; set; }
    }


    public class SCFHeader
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Vendor { get; set; }
        public string V_GSTN { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvioceDate { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime StockFor { get; set; }

    }

    public class SCFListDetail
    {
        public int SN { get; set; }
        public string PRODUCT { get; set; }
        public int HSN_CODE { get; set; }
        public float QTY_IN { get; set; }
        public decimal PURCHASE_RATE { get; set; }
        public decimal PUR_AMT { get; set; }
        public decimal SALERATE { get; set; }
        public decimal RESALE_AMT { get; set; }
        public decimal TAXVAL { get; set; }
        public decimal PURCHASE_MRP { get; set; }
        public decimal LOT_DIS { get; set; }
        public decimal COSTRATE { get; set; }
        public decimal ACTUALMARGIN { get; set; }
        public decimal CUST_DIS { get; set; }
    }

    public class SCFFooter
    {
        public decimal Lot_Dis { get; set; }
        public decimal SCHEME_Dis { get; set; }
        public decimal Cust_Dis { get; set; }
        public decimal GST { get; set; }
        public decimal SGST { get; set; }
    }

    public class InvoiceUpdateStatus
    {
        public int TBLCOUNTERSALE_ID { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
    }

    public class RptOpenCoseStockModel
    {
        public string Mode { get; set; }
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class StoreModel
    {
        public string STORE_ID { get; set; }
        public string STORE_NAME { get; set; }
        public string PERSON_NAME { get; set; }
        public string MOBILE { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string PINCODE { get; set; }
        public int CITYID { get; set; }
        public string MSTCATIDSTR { get; set; }
        public int GODOWN_ID { get; set; }
        public int STATEID { get; set; }
        public int DISTRICT_ID { get; set; }
        public string GSTNO { get; set; }
        public string DLNO { get; set; }
        public string TINNO { get; set; }
        public string REMARK { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
        public int ISACTIVE { get; set; }
    }

   
    public class CounterSaleModel
    {

        public int SALEFROM_ID { get; set; }
        public string SALEFROM_BY { get; set; }
        public List<CounterProduct> PRODUCT_DETAILJSON { get; set; }
        public List<NACProductList> NAC_PRODUCTDETAIL_JSON { get; set; }
        public int PAYMENTMODE_ID { get; set; }
        public int OTP { get; set; }
        public string CUST_NAME { get; set; }
        public int? DISPENSARYID { get; set; }
        public string DOCTORNAME { get; set; }
        public string CUST_MOBILE { get; set; }
        public int BUYER_ID { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
        public string NAME { get; set; }
        public string DISPENSARYNAME { get; set; }
        public string OPTTKT { get; set; }
        public string EMPLOYEENAME { get; set; }
        public string DEPTNAME { get; set; }
        public int ? TBLCOUNTERSALE_ID { get; set; }
        public string REMARK { get; set; }
    }

    public class NACProductList
    {
        public int PROD_ID { get; set; }
        public string PRODUCTNAME { get; set; }
        public decimal QTY { get; set; }
    }
    public class CounterProduct
    {
        public int PROD_ID { get; set; }
        public int PROD_V_ID { get; set; }
        public float QTY { get; set; }
        public int STOCKBATCHID { get; set; }
        public float LISTPRICE { get; set; }
        public float DISCOUNT { get; set; }
        public float IGST { get; set; }
        public float SGST { get; set; }
        public float GST { get; set; }
        public float SELLINGPRICE { get; set; }        
        public decimal OTHER_DISCOUNTPERC { get; set; }
        public float OTHER_DISCOUNTAMT { get; set; }
        public float EXTRA_MARGINPERC { get; set; }
        public float EXTRA_MARGINAMT { get; set; }

    }
    public class SellModel
    {
        public int buyer_id { get; set; }
        public string mobile { get; set; }
        public string alternate_mobile { get; set; }
        public string mode { get; set; }
        public string email_id { get; set; }
        public double wallet_limit { get; set; }
        public double balance { get; set; }
        public int otp { get; set; }
        public string createdby { get; set; }
        public string ipaddress { get; set; }
        public string resultmsg { get; set; }
    }
    public class Response
    {
        public int OTP { get; set; }
        public string MSG { get; set; }
    }
    public class InvoiceFile
    {
        public int Id { get; set; }
        public string INVOICE_FOR { get; set; }
        public int TBLCOUNTERSALE_ID { get; set; }
        public string HTML { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
        public string RESULTMSG { get; set; }
    }
    public class PensionersExtensionInfo
    {
        public int BuyerID { get; set; }
        public string ExtendLimit { get; set; }
        public string ReferenceNumber { get; set; }
        public string BookNumber { get; set; }
        public string Mode { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
        public string RESULTMSG { get; set; }
    }


    public class StockUpdateModel
    {

        public int STOCKFORID { get; set; }
        public string STOCKFOR { get; set; }
        public List<ProductdetailModel> PRODUCT_DETAILJSON { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
    }

    public class ProductdetailModel
    {
        public int PROD_ID { get; set; }
        public string HSN_ID { get; set; }
        public float? PURCHASE_RATE { get; set; }
        public float? PURCHASE_MRP { get; set; }
        public decimal? PURCHASE_CASSTAX { get; set; }
        public decimal? PURCHASE_TRADEDIS { get; set; }
        public decimal QTY { get; set; }
        public decimal? LOT_DIS { get; set; }
        public decimal? SCHEME_DIS { get; set; }
        public decimal? CUST_DIS { get; set; }
        public decimal? SALERATE { get; set; }
        public decimal? COSTRATE { get; set; }
        public decimal? ACTUALMARGIN { get; set; }
        public string BATCH { get; set; }
        public DateTime? MFGDATE { get; set; }
        public string EXPDATE { get; set; }
        public DateTime EXPIRYDATE { get; set; }
        public decimal? PURCHASE_TRADEDIS_PERC { get; set; }
        public int SCHEME_DISCOUNTTYPEID { get; set; }
        public decimal SCHEME_DIS_AMT { get; set; }
        public decimal? PURCHASE_CASSTAX_PERC { get; set; }
        public decimal? GST { get; set; }
        public decimal? GST_AMOUNT { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? GROSSAMOUNT { get; set; }
        public decimal? GROSSMARGIN { get; set; }
        public decimal? OTERCHARGES_PERQTY { get; set; }
        public float? SALE_TRADERATE { get; set; }
    }
}


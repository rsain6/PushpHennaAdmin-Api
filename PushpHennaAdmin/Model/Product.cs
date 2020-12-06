using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class Product
    {
        public int PROD_ID { get; set; }
        public int UID { get; set; }
        public int PROD_TYPE { get; set; }
        public string NAME { get; set; }
        public string MODEL { get; set; }
        public string DISPLAYNAME { get; set; }
        public string BRAND { get; set; }
        public string BARCODE { get; set; }
        public string DETAILDESCRIPTION { get; set; }
        //public int CURRENTVENDORID { get; set; } 
        public int MASTERCATEGORYID { get; set; }
        public int CATEGORYID { get; set; }
        public int SUBCATEGORYID { get; set; }
        public float? WEIGHT { get; set; }
        public int? WEIGHTCLASSID { get; set; }
        public bool ISAPPLYONLOT { get; set; }
        public string APPLYONLOTSTR { get; set; }
        public int CURRENTVERSION { get; set; }
        public int HSN_ID { get; set; }
        //public string GSTIN { get; set; }
        public double LISTPRICE { get; set; }
        public double SELLINGPRICE { get; set; }
        public double IGST { get; set; }
        public double SGST { get; set; }
        public double GST { get; set; }
        public double DISCOUNT { get; set; }
        public double MARGIN { get; set; }
        public string DISCOUNTCLASS { get; set; }
        public DateTime EXPIRYDATE { get; set; }

        public int STATUSID { get; set; }
        public DateTime STATUSCHANGEON { get; set; }
        public bool ISACTIVE { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATEDON { get; set; }
        public int CREATEDBY { get; set; }
        public DateTime MODIFYON { get; set; }
        public int MODIFYBY { get; set; }
        public string IPADDRESS { get; set; }

        public int GENERIC_ID { get; set; }
        public int SIZECLASSID { get; set; }
        public int WEIGHTCLASSID_PAK { get; set; }
        public int WEIGHT_PAK { get; set; }
        public int MAXSTOCK { get; set; }
        public int MINSTOCK { get; set; }
        public string ALIAS { get; set; }
        public string DRUGTYPE { get; set; }
        public bool ISFASTMOVING { get; set; }
        public int MKTGROUPID { get; set; }
    }

    public class ProductFilterParameter
    {
        public string ProdName { get; set; }
        public int ChkId { get; set; }
        public int MktGroupId { get; set; }
        public string SearchText { get; set; }
        public string ORderBy { get; set; }
        public string DiscountClass { get; set; }
        public string OrderByType { get; set; }
        public string Mode { get; set; }
        public string CreatedBY { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ProductList
    {
        public string Prod_Name { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public int? BUYER_TYPE { get; set; }
        public int SaleFrom_ID { get; set; }
        public int ? ISWALKIN { get; set; }
        public string SaleFrom_BY { get; set; }
        public string ProductId { get; set; }
        public int CreatedBy { get; set; }
        public string IsActive { get; set; }
    }
    public class ProductMaster
    {
        public string NAME { get; set; }
        public int ID { get; set; }
        public string DISPLAYNAME { get; set; }
        public string WEIGHT { get; set; }
        public int WEIGHTCLASSID { get; set; }
        public float QTY { get; set; }
    }

    public class ProductUpdateStatus
    {
        public string Prod_Id { get; set; }
    }

    public class ResaleFormulaSearchModel
    {
        public int MKTGROUPID { get; set; }
        public int CATEGORYID { get; set; }
        public string PRODNAME { get; set; }
    }


    public class SaveSaleRate
    {
        public List<SaleRateProductModel> product_detailjson { get; set; }
        public string CreatedBy { get; set; }
        public string IPAddress { get; set; }
    }

    public class SaleRateProductModel
    {
        public int PROD_ID { get; set; }
        public int FORMULA_TYPID { get; set; }
        public decimal? VALUE_PERC { get; set; }
        public bool ISCHECK { get; set; }
        public decimal? PURCHASERATE { get; set; }
        public decimal? MRP { get; set; }
        public decimal? SALERATE { get; set; }
    }
}

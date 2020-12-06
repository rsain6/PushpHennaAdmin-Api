using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class DemandModel
    {
        public int DemandId { get; set; }
        public int Demand_DId { get; set; }
        public int StockHead_ID { get; set; }
        public int DemandFrom_Id { get; set; } 
        public string DemandFrom_By { get; set; }
        public int DemandTo_Id { get; set; }
        public string DemandTo_By { get; set; }
        public string Remarks { get; set; }
        public List<ProductDemand> Product_Json { get; set; }
        public int DemandStatusID { get; set; }
        public string CreatedBy { get; set; }
        public string IpAddress { get; set; }
        public string Mode { get; set; }
        public string IndentNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
    }
    public class ProductDemand
    {
        public int PROD_ID { get; set; }
        public decimal REQUIREDQTY { get; set; }
    }

    //{"DEMANDFROM_ID":"4","DEMANDFROM_BY":"STORE","DEMANDTO_ID":null,"DEMANDTO_BY":"GODOWN","REMARKS":"Demand Testing","CREATEDBY":"6","IPADDRESS":"164.100.222.44",
    //"PRODUCT_JSON":[
    //{"PROD_ID":25401,"PROD_NAME":"LUX VELVET TOUCH 4X100 GM ","WEIGHTCLASSID":0,"WEIGHT":1,"WEIGHTCLASS":"Packet","AVAL_QTY":"","REQUIREDQTY":"50"},
    //{"PROD_ID":562,"PROD_NAME":"Atta L.D. Packing 45Kg.","WEIGHTCLASSID":0,"WEIGHT":1,"WEIGHTCLASS":"Packet","AVAL_QTY":"","REQUIREDQTY":"25"},
    //{"PROD_ID":24726,"PROD_NAME":"COLGATE STRONG 4X200 GM ","WEIGHTCLASSID":0,"WEIGHT":1,"WEIGHTCLASS":"Packet","AVAL_QTY":"","REQUIREDQTY":"15"},
    //{"PROD_ID":525,"PROD_NAME":"Suji 1Kg.","WEIGHTCLASSID":0,"WEIGHT":1,"WEIGHTCLASS":"Packet","AVAL_QTY":"","REQUIREDQTY":"20"}]}
    public class PurchaseReport
    {
        public int DemandToId { get; set; }
        public string DemandToBy { get; set; }
        public string DemandFrom_Id_STR { get; set; }
        public string DemandFrom_By { get; set; }
        public int DemandTo_Id { get; set; }
        public string DemandTo_By { get; set; }
        public string GodownSTR { get; set; }
        public string StoreSTR { get; set; }
        public int DemandStatusId { get; set; }
        public string IndentNo { get; set; }
        public int MKTGroupID { get; set; }
        public int StatusId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
      
    }

    public class StockTransfer
    {
        public int DemandID { get; set; }
        public List <StockBatchList> Stock_DetailJSON { get; set; }
        public string CreatedBy { get; set; }
        public string IPAddress { get; set; }

    }

    public class StockDetailList
    {
        public int DEMAND_DID { get; set; }
        public int STOCKDETAIL_ID { get; set; }
        public decimal USED_QTY { get; set; }
    }
    public class StockBatchList
    {
        public int DEMAND_DID { get; set; }
        public int STOCKBATCHID { get; set; }
        public decimal USED_QTY { get; set; }
    }
    public class StockModel
    {
        public int AcceptStatusID { get; set; }
        public int StoreId { get; set; }
        public string StockDetail_IDSTR { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public string INDENT_TRANSCHALLAN { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
    }
    public class StockAcceptModel
    {
        public int AcceptStatusID { get; set; }
        public int StoreId { get; set; }
        public string StockDetail_IDSTR { get; set; }
        public string Mode { get; set; }
        public string CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public string INDENT_TRANSCHALLAN { get; set; }
        public string Remark { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
    }
}


    
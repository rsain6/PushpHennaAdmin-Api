using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;

namespace PushpHennaAdmin.Controllers
{
    [Route("api/Report")]
    public class ReportController : Controller
    {
        DAL objDAL = DAL.GetObject(); //new DAL();
        private IConfiguration _config;
        string connection = string.Empty;
        public ReportController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Get Bill Wise sell report 
        /// </summary>
        /// <param name="objGodownStockModel">search by prod_name,,store_id,FROMDATE,TODATE,pageindex,pagesize</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("BillWiseSellReport")]
        public string BillWiseSellReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PROD_NAME", objRptBillWiseSellReport.Prod_Name),
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate),
                                            new SqlParameter("@PAGEINDEX", objRptBillWiseSellReport.PageIndex),
                                            new SqlParameter("@PAGESIZE", objRptBillWiseSellReport.PageSize),
                                            };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELL", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Daily sell report 
        /// </summary>
        /// <param name="objGodownStockModel">search by prod_name,,store_id,FROMDATE,TODATE,pageindex,pagesize</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DailySellReport")]
        public string DailySellReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            //new SqlParameter("@CREDITTYPE", objRptBillWiseSellReport.CreditType),
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate),
                                            new SqlParameter("@BUYERTYPEID", objRptBillWiseSellReport.UserTypeId),
                                            new SqlParameter("@NACTYPE", objRptBillWiseSellReport.NACType),
                                            new SqlParameter("@PAGEINDEX", objRptBillWiseSellReport.PageIndex),
                                            new SqlParameter("@PAGESIZE", objRptBillWiseSellReport.PageSize),
                                            };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELL_DAILY", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Monthly sell report 
        /// </summary>
        /// <param name="objGodownStockModel">search by prod_name,,store_id,FROMDATE,TODATE,pageindex,pagesize</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("MonthlySellReport")]
        public string MonthlySellReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@MODE", objRptBillWiseSellReport.Mode),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate),
                                            new SqlParameter("@PAGEINDEX", objRptBillWiseSellReport.PageIndex),
                                            new SqlParameter("@PAGESIZE", objRptBillWiseSellReport.PageSize),
                                            };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELL_MONTHLY", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Product Name and Date wise Stock Detail report 
        /// </summary>
        /// <param name="objGodownStockModel">search by prod_name,DATE,pageindex,pagesize</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("StockDeatilReport")]
        public string StockDeatilReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PROD_NAME", objRptBillWiseSellReport.Prod_Name),
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@PAGEINDEX", objRptBillWiseSellReport.PageIndex),
                                            new SqlParameter("@PAGESIZE", objRptBillWiseSellReport.PageSize),
                                            };
                outPut = objDAL.GetJson(connection, "RPT_STOCKDETAIL", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Bill Wise sell report 
        /// </summary>
        /// <param name="objGodownStockModel">search by prod_name,,store_id,FROMDATE,TODATE,pageindex,pagesize</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("CreditSellRegisterReport")]
        public string CreditSellRegisterReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate)
                                            };
                outPut = objDAL.GetJson(connection, "RPT_CREDITSALE_REGISTER", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get SCF Details by PO Id
        /// </summary>
        /// <param name="Id"> uniquie PO Id</param>
        /// <returns>All save detail return as Json string</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetSCFDetailByPOId")]
        public string GetSCFDetailByPOId([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            DataSet objds = new DataSet();
            Header model = new Header();
            string str = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@STOCKHEAD_ID", objPurchaseorder.StockHead_Id) };
                objds = objDAL.Get(connection, "STOCK_DETAIL_S_BYINVOICENO", spparams);
                if (objds != null && objds.Tables.Count > 0)
                {
                    objds.Tables[0].TableName = "SCFHEader";
                    objds.Tables[1].TableName = "SCFListDetail";
                    objds.Tables[2].TableName = "SCFHFooter";
                    str = Newtonsoft.Json.JsonConvert.SerializeObject(objds);
                }

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return str;
        }


        /// <summary>
        /// Get SCF Details by PO Id
        /// </summary>
        /// <param name="Id"> uniquie PO Id</param>
        /// <returns>All save detail return as Json string</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetSCFDetailByPOIdHTML")]
        public string GetSCFDetailByPOIdHTML([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@STOCKHEAD_ID", objPurchaseorder.StockHead_Id)
                                          };
                outPut = objDAL.GetJson(connection, "STOCK_DETAIL_S_BYINVOICENO_HTML", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;          
        }



        /// <summary>
        /// Get Order Invoice report 
        /// </summary>
        /// <param name="RptBillWiseSellReport">search by INVOICENO,FROMDATE,TODATE,pageindex,pagesize</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("OrderInvoiceReport")]
        public string OrderInvoiceReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate),
                                            new SqlParameter("@INVOICE_NO", objRptBillWiseSellReport.InvoiceNo),
                                            new SqlParameter("@PAGEINDEX",objRptBillWiseSellReport.PageIndex),
                                            new SqlParameter("@PAGESIZE",objRptBillWiseSellReport.PageSize),
                                            };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELL_BYINVOICENO", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Invoice Trail report 
        /// </summary>
        /// <param name="RPT_COUNTERSELL_BYINVOICENO_WITHTRAIL">search by INVOICENO,FROMDATE,TODATE,Workfor,WorkForID,pageindex,pagesize</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("InvoiceDetailReport")]
        public string InvoiceDetailReport([FromBody] RptInvoiceReport objRptInvoiceReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@INVOICE_NO", objRptInvoiceReport.InvoiceNo),
                                            new SqlParameter("@WORKFOR", objRptInvoiceReport.WorkFor),
                                            new SqlParameter("@WORKFORID", objRptInvoiceReport.WorkForID),
                                            new SqlParameter("@FROMDATE", objRptInvoiceReport.FromDate),
                                            new SqlParameter("@TODATE", objRptInvoiceReport.ToDate),
                                            new SqlParameter("@TRAILMODE", objRptInvoiceReport.Mode),
                                            new SqlParameter("@PAGEINDEX",objRptInvoiceReport.PageIndex),
                                            new SqlParameter("@PAGESIZE",objRptInvoiceReport.PageSize),
                                            };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELL_BYINVOICENO_WITHTRAIL", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Invoice Trail report 
        /// </summary>
        /// <param name="RPT_COUNTERSELLDETAILTRAIL_S_CSID">search by INVOICENO,FROMDATE,TODATE,Workfor,WorkForID,pageindex,pagesize</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("InvoiceDetailTrailReport")]
        public string InvoiceDetailTrailReport([FromBody] RptInvoiceTrailReport objRptInvoiceTrailReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@TBLCOUNTERSALE_ID", objRptInvoiceTrailReport.CounterSaleID),
                                            };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELLDETAILTRAIL_S_CSID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("InvoiceDetailByInvoiceNo/{Id}")]
        public string InvoiceDetailByInvoiceNo(string Id)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@INVOICE_NO", Id) };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELL_BYINVOICENO_PRINT", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Get Near To Expiry Report 
        /// </summary>
        /// <param name="objGodownStockModel">search by CREATEDBY</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("NearToExpiryReport")]
        public string NearToExpiryReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy)
                                            };
                outPut = objDAL.GetJson(connection, "RPT_NEARTOEXPIRY", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Near To Expiry Report 
        /// </summary>
        /// <param name="objGodownStockModel">search by CREATEDBY</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("ExpiryReport")]
        public string ExpiryReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@MKTGROUPID", objRptBillWiseSellReport.MktGroupId),
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate),
                                            new SqlParameter("@PAGEINDEX",objRptBillWiseSellReport.PageIndex),
                                            new SqlParameter("@PAGESIZE",objRptBillWiseSellReport.PageSize),
                                            };
                outPut = objDAL.GetJson(connection, "RPT_EXPIRY", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Near To sales Details Report 
        /// </summary>
        /// <param name="objGodownStockModel">search by CREATEDBY</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetSaleDetailReport")]
        public string GetSaleDetailReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate)
                                            //new SqlParameter("@PAGEINDEX",objRptBillWiseSellReport.PageIndex),
                                            //new SqlParameter("@PAGESIZE",objRptBillWiseSellReport.PageSize),
                                            };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELL_SELLTYPE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Near To Schedule wise sales Report 
        /// </summary>
        /// <param name="objGodownStockModel">search by CREATEDBY</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetScheduleWiseSalesReport")]
        public string GetScheduleWiseSalesReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DRUGTYPE", objRptBillWiseSellReport.DrugType),
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate)
                                            };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELL_H1", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Near To mkt group wise sales Report 
        /// </summary>
        /// <param name="objGodownStockModel">search by CREATEDBY</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetMktWiseSalesReport")]
        public string GetMktWiseSalesReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate),
                                             new SqlParameter("@MKTGROUPID", objRptBillWiseSellReport.MktGroupId)
                                            };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELL_DETAIL_MKTGROUPWISE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Near To mkt group wise sales Report 
        /// </summary>
        /// <param name="objGodownStockModel">search by CREATEDBY</param>
        /// <returns>product name , sell qty, unit price, gst, total price</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetBuyerWiseSalesReport")]
        public string GetBuyerWiseSalesReport([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate),
                                            new SqlParameter("@BUYERID", objRptBillWiseSellReport.BuyerId)
                                            };
                outPut = objDAL.GetJson(connection, "RPT_COUNTERSELL_BUYERWISE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateInvoiceStatus")]
        public string UpdateInvoiceStatus([FromBody] InvoiceUpdateStatus objInvoiceUpdateStatus)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@TBLCOUNTERSALE_ID", objInvoiceUpdateStatus.TBLCOUNTERSALE_ID),
                                            new SqlParameter("@CREATEDBY", objInvoiceUpdateStatus.CREATEDBY),
                                            new SqlParameter("@IPADDRESS", objInvoiceUpdateStatus.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLCOUNTER_SALE_DELETE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetOpenCloseStockReport")]
        public string GetOpenCloseStockReport([FromBody] RptOpenCoseStockModel objRptOpenCoseStockModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@MODE", objRptOpenCoseStockModel.Mode),
                                            new SqlParameter("@ID", objRptOpenCoseStockModel.Id),
                                            new SqlParameter("@FROMDATE", objRptOpenCoseStockModel.FromDate),
                                            new SqlParameter("@TODATE", objRptOpenCoseStockModel.ToDate)
                                            };
                outPut = objDAL.GetJson(connection, "RPT_OPEN_CLOSE_STOCK", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DailySummaryReportStoreWise")]
        public string DailySummaryReportStoreWise([FromBody] RptBillWiseSellReport objRptBillWiseSellReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@CREATEDBY", objRptBillWiseSellReport.CreatedBy),
                                            new SqlParameter("@FROMDATE", objRptBillWiseSellReport.FromDate),
                                            new SqlParameter("@TODATE", objRptBillWiseSellReport.ToDate)
                                            };
                outPut = objDAL.GetJson(connection, "RPT_DAILYSUMMARY", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetIndentReport")]
        public string GetIndentReport([FromBody] RptBillWiseSellReport objDemandModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@FROMDATE", objDemandModel.FromDate),
                                            new SqlParameter("@TODATE", objDemandModel.ToDate),
                                             new SqlParameter("@CREATEDBY", objDemandModel.CreatedBy),
                                            new SqlParameter("@PAGEINDEX",objDemandModel.PageIndex),
                                            new SqlParameter("@PAGESIZE",objDemandModel.PageSize)};
                outPut = objDAL.GetJson(connection, "RPT_TBLGODOWN_DEMAND_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPurchaseOrderReport")]
        public string GetPurchaseOrderReport([FromBody] RptBillWiseSellReport objDemandModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@FROMDATE", objDemandModel.FromDate),
                                            new SqlParameter("@TODATE", objDemandModel.ToDate),
                                             new SqlParameter("@CREATEDBY", objDemandModel.CreatedBy),
                                            new SqlParameter("@PAGEINDEX",objDemandModel.PageIndex),
                                            new SqlParameter("@PAGESIZE",objDemandModel.PageSize)};
                outPut = objDAL.GetJson(connection, "RPT_TBLPO_H_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
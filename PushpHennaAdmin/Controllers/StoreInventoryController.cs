using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;
using System;
using System.Data.SqlClient;
using System.IO;


namespace PushpHennaAdmin.Controllers
{
    [Route("api/StoreInventory")]
    public class StoreInventoryController : Controller
    {
        DAL objDAL = DAL.GetObject(); //new DAL();
        private IConfiguration _config;
        string connection = string.Empty;
        public StoreInventoryController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Transfer stock to store from godown
        /// </summary>
        /// <param name="objPurchaseReport"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GodownTransferStockToStore")] //{PageIndex}/{PageSize}
        public string GodownTransferStockToStore([FromBody] StockTransfer objStockTransfer)
        {
            string outPut = string.Empty;
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objStockTransfer.Stock_DetailJSON);
                SqlParameter[] spparams = {
                                           new SqlParameter("@DEMANDID", objStockTransfer.DemandID),
                                            new SqlParameter("@PRODUCT_DETAILJSON", ProductJson),
                                             new SqlParameter("@CREATEDBY", objStockTransfer.CreatedBy),
                                            new SqlParameter("@IPADDRESS", objStockTransfer.IPAddress),
                                            new SqlParameter("@RESULTMSG", "")
                                          };
                outPut = objDAL.PostWithResultCode(connection, "TBLSTOCKIN_TRANSFERBY_DEMANDDID", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        ///Get Product stock lost  in Godown
        /// </summary>
        /// <param name="objStockModel">AcceptStatusID,StoreId</param>
        /// <returns>Prodcut list Batch wise</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetProductStockListInGDN")] //{PageIndex}/{PageSize}
        public string GetProductStockListInGDN([FromBody] StockModel objStockModel)
        {
            string outPut = string.Empty;
            try
            {               
                SqlParameter[] spparams = {
                                            new SqlParameter("@ACCEPTSTATUSID", objStockModel.AcceptStatusID),
                                            new SqlParameter("@STOREID", objStockModel.StoreId) ,
                                            new SqlParameter("@INDENT_TRANSCHALLAN", objStockModel.INDENT_TRANSCHALLAN),
                                             new SqlParameter("@FROMDATE", objStockModel.FromDate),
                                            new SqlParameter("@TODATE", objStockModel.ToDate),
                                            new SqlParameter("@PAGEINDEX", objStockModel.pageindex),
                                            new SqlParameter("@PAGESIZE", objStockModel.pagesize)
                                          };
                outPut = objDAL.GetJson(connection, "TBLSTOCK_S_FORACCEPT", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Receive Stock Accept By Store
        /// </summary>
        /// <param name="objStockModel">STOCKDETAIL_IDSTR,CREATEDBY,IPADDRESS</param>
        /// <returns>Success MSG</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("ReceiveStockAcceptInStore")] //{PageIndex}/{PageSize}
        public string ReceiveStockAcceptInStore([FromBody]  StockAcceptModel objStockModel)
        {
            string outPut = string.Empty;
            try
            {               
                SqlParameter[] spparams = {
                                           new SqlParameter("@STOCKDETAIL_TRAILID", objStockModel.StockDetail_IDSTR),
                                           new SqlParameter("@CREATEDBY", objStockModel.CreatedBy),
                                           new SqlParameter("@IPADDRESS", objStockModel.IPAddress),
                                           new SqlParameter("@MODE", objStockModel.Mode),
                                           new SqlParameter("@REMARK", objStockModel.Remark),
                                           new SqlParameter("@RESULTMSG", "")
                                          };
                outPut = objDAL.PostWithResultCode(connection, "TBLSTOCK_U_FORACCEPT", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get product detail lot wise
        /// </summary>
        /// <param name="objStockModel">DemandId</param>
        /// <returns>lIST OF PRICE WITH EXPIRY DATE </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetProductDetailLotWise")] //{PageIndex}/{PageSize}
        public string GetProductDetailLotWise([FromBody]  DemandModel objDemandModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {new SqlParameter("@DEMAND_DID", objDemandModel.Demand_DId) };
                outPut = objDAL.GetJson(connection, "TBLSTOCK_S_PRODUCT_LOTWISE", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

    }
}
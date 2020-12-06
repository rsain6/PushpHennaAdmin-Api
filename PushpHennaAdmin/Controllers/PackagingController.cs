using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;
using System;
using System.Data.SqlClient;
namespace PushpHennaAdmin.Controllers
{
    [Route("api/Packaging")]
    public class PackagingController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;

        public PackagingController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Get Row material Detail Godown Wise on Purchase order
        /// </summary>
        /// <param name="objRowMaterial">Godown row material id and mode</param>
        /// <returns>show- price ,gst,qunatiry </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetGodownRowMaterialDetail")]
        public string GetGodownRowMaterialDetail([FromBody] PackagingModel objPackagingModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@STOCKFORID", objPackagingModel.StockFor_ID) ,
                                            new SqlParameter("@STOCKFOR", objPackagingModel.StockFor),
                                            new SqlParameter("@MODE", objPackagingModel.Mode)};
                outPut = objDAL.GetJson(connection, "TBLSTOCK_S_PACKAGING", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get packaging product Detail by product id and mode
        /// </summary>
        /// <param name="objRowMaterial">packaging product id and mode</param>
        /// <returns>show- price ,gst,qunatiry </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPackagingProductList")]
        public string GetPackagingProductList([FromBody] PackagingModel objPackagingModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PRODID", objPackagingModel.Prod_Id) ,
                                            new SqlParameter("@MODE", objPackagingModel.Mode)};
                outPut = objDAL.GetJson(connection, "TBLPRODUCT_List_SBYPRODID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("ProductQtyCalculation")]
        public string ProductQtyCalculation([FromBody] QtyCalulation objQtyCalulation)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@QTY", objQtyCalulation.Qty) ,
                                            new SqlParameter("@STOCKDETAIL_ID", objQtyCalulation.StockDetail_Id),
                                            new SqlParameter("@TOID", objQtyCalulation.ToId)};
                outPut = objDAL.GetJson(connection, "TBLSTOCKPACKAGING_CALCULATION", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveRowMaterialPackaing")]
        public string SaveRowMaterialPackaing([FromBody] SavePackagingModel objSavePackagingModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@STOCKFORID",objSavePackagingModel.StockForId),
                                            new SqlParameter("@STOCKFOR", objSavePackagingModel.StockFor),
                                            new SqlParameter("@STOCKDETAIL_ID",objSavePackagingModel.StockDetail_Id),
                                            new SqlParameter("@USED_QTY",objSavePackagingModel.Used_Qty),
                                            new SqlParameter("@PRODID",objSavePackagingModel.ProdId),                                           
                                            new SqlParameter("@QTY",objSavePackagingModel.Qty),
                                            new SqlParameter("@CUST_DIS",objSavePackagingModel.Cust_Dis),
                                            new SqlParameter("@SALERATE",objSavePackagingModel.SaleRate),
                                            new SqlParameter("@ACTUALMARGIN ",objSavePackagingModel.ActualMargin),
                                            new SqlParameter("@EXPDATE",objSavePackagingModel.ExpDate),
                                            new SqlParameter("@REMARK",objSavePackagingModel.Remark),
                                            new SqlParameter("@CREATEDBY",objSavePackagingModel.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objSavePackagingModel.IpAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLSTOCKIN_I_PACKAGING", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Row material Detail Godown/store Wise on Purchase order
        /// </summary>
        /// <param name="objRowMaterial">Godown/store row material id</param>
        /// <returns>show- price ,gst,qunatiry </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPackagingList")]
        public string GetPackagingList([FromBody] ProductStockList objProductStockList)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {   new SqlParameter("@STOCKFORID ",objProductStockList.StockForID),
                                              new SqlParameter("@STOCKFOR ",objProductStockList.StockFor),
                                              new SqlParameter("@PAGEINDEX",objProductStockList.pageindex),
                                              new SqlParameter("@PAGESIZE",objProductStockList.pagesize)};
                outPut = objDAL.GetJson(connection, "TBLSTOCKOUT_FOR_PACKAGING", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Row material Detail Godown/store Wise on Purchase order
        /// </summary>
        /// <param name="objRowMaterial">Godown/store row material id</param>
        /// <returns>show- price ,gst,qunatiry </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPackagingDetail")]
        public string GetPackagingDetail([FromBody] ProductStockList objProductStockList)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@STOCKDETAIL_IDTO", objProductStockList.StockDetail_IdTO) };
                outPut = objDAL.GetJson(connection, "TBLSTOCKOUT_S_PACKAGINGDETAIL", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
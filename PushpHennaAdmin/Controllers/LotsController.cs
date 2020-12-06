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
    [Route("api/Lots")]
    public class LotsController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;
        public LotsController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Show tax List using page index and page size
        /// </summary>
        /// <param name="objCategoryListModel"> page index and page size</param>
        /// <returns>List of MSTLOTS_PRODUCT </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("Lots_ProdList")]
        public string Lots_ProdList([FromBody]  GridListModel objTaxListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objTaxListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objTaxListModel.PAGESIZE)
                                          };
                outPut = objDAL.GetJson(connection, "MSTLOTS_PRODUCT_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Using mode in SP Save/Update/Deactive and active MSTLOTS_PRODUCT Master data 
        /// </summary>
        /// <param name="objMasterCategory">MasterMSTLOTS_PRODUCTModel object </param>
        /// <returns>Success with return Id</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveLots_Prod")]
        public string SaveLots_Prod([FromBody] Lot objLot)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@LOTID",objLot.LOTID),
                                            new SqlParameter("@NAME", objLot.NAME) ,
                                            new SqlParameter("@MODE",objLot.MODE),
                                            new SqlParameter("@REMARKS",objLot.REMARK),
                                            new SqlParameter("@CREATEDBY",objLot.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objLot.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTLOTS_PRODUCT_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update MSTLOTS_PRODUCT master data 
        /// </summary>
        /// <param name="objTax">lotid,name,mode,remark,createdby,ipaddress</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateLots_Prod")]
        public string UpdateLots_Prod([FromBody] Lot objMstLot)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@LOTID",objMstLot.LOTID),
                                            new SqlParameter("@NAME", objMstLot.NAME) ,
                                            new SqlParameter("@MODE",objMstLot.MODE),
                                            new SqlParameter("@REMARKS",objMstLot.REMARK),
                                            new SqlParameter("@CREATEDBY",objMstLot.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objMstLot.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTLOTS_PRODUCT_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetLots_ProdDetail")]
        public string GetLots_ProdDetail([FromBody] Lot objLot)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@LOTID",objLot.LOTID),
                                            new SqlParameter("@MODE",objLot.MODE),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTLOTS_PRODUCT_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// deactive MSTLOTS_PRODUCT master data 
        /// </summary>
        /// <param name="objTax">lotid, mode</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DeactiveLots_Prod")]
        public string DeactiveLots_Prod([FromBody] Lot objLot)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@LOTID",objLot.LOTID),
                                            new SqlParameter("@MODE",objLot.MODE),
                                            new SqlParameter("@CREATEDBY",objLot.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objLot.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTLOTS_PRODUCT_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
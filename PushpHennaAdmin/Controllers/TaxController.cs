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
    [Route("api/Tax")]
    public class TaxController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;
        public TaxController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Show tax List using page index and page size
        /// </summary>
        /// <param name="objCategoryListModel"> page index and page size</param>
        /// <returns>List of tax </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("TaxList")]
        public string TaxList([FromBody]  GridListModel objTaxListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objTaxListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objTaxListModel.PAGESIZE)
                                          };
                outPut = objDAL.GetJson(connection, "MSTTAX_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Using mode in SP Save/Update/Deactive and active Tax Master data 
        /// </summary>
        /// <param name="objMasterCategory">MasterTax object </param>
        /// <returns>Success with return Id</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveTax")]
        public string SaveTax([FromBody] Tax objTax)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@TAXID",objTax.TAXID),
                                            new SqlParameter("@NAME", objTax.NAME),
                                            new SqlParameter("@TAXVAL", objTax.TAXVAL),
                                            new SqlParameter("@MODE",objTax.MODE),
                                            new SqlParameter("@REMARKS",objTax.REMARK),
                                            new SqlParameter("@CREATEDBY",objTax.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objTax.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTTAX_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update tax master data 
        /// </summary>
        /// <param name="objTax">taxid,name,mode,remark,createdby,ipaddress</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateTax")]
        public string UpdateTax([FromBody] Tax objMstTax)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@TAXID",objMstTax.TAXID),
                                            new SqlParameter("@NAME", objMstTax.NAME),
                                            new SqlParameter("@TAXVAL", objMstTax.TAXVAL),
                                            new SqlParameter("@MODE",objMstTax.MODE),
                                            new SqlParameter("@REMARKS",objMstTax.REMARK),
                                            new SqlParameter("@CREATEDBY",objMstTax.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objMstTax.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTTAX_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetTaxDetail")]
        public string GetTaxDetail([FromBody] Tax objTax)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@TAXID",objTax.TAXID),
                                            new SqlParameter("@MODE",objTax.MODE),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTTAX_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// deactive tax master data 
        /// </summary>
        /// <param name="objTax">taxid, mode</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DeactiveTax")]
        public string DeactiveTax([FromBody] Tax objTax)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@TAXID",objTax.TAXID),
                                            new SqlParameter("@MODE",objTax.MODE),
                                            new SqlParameter("@CREATEDBY",objTax.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objTax.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTTAX_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
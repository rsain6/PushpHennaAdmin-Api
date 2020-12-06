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
    [Route("api/Hsn")]
    public class HsnController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;
        public HsnController(IConfiguration config)
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
        [Route("HsnList")]
        public string HsnList([FromBody]  GridListModel objCategoryListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objCategoryListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objCategoryListModel.PAGESIZE)
                                          };
                outPut = objDAL.GetJson(connection, "MSTHSN_S", spparams);
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
        /// <param name="objMasterCategory">MasterCategoryModel object </param>
        /// <returns>Success with return Id</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveHsn")]
        public string SaveHsn([FromBody] HsnModel objHsn)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@HSNID",objHsn.HSN_ID),
                                            new SqlParameter("@TAXID",objHsn.TAX_ID),
                                            new SqlParameter("@HSNCODE",objHsn.HSN_CODE),
                                            new SqlParameter("@DESCRIPTION",objHsn.DESCRIPTION),
                                            //new SqlParameter("@HSNREMARK",objHsn.HSNREMARK),
                                            new SqlParameter("@ISEXEMPTED",objHsn.ISEXEMPTED),
                                            new SqlParameter("@MODE",objHsn.MODE),
                                            new SqlParameter("@REMARKS",objHsn.REMARK),
                                            new SqlParameter("@CREATEDBY",objHsn.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objHsn.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTHSN_I", spparams);
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
        [Route("UpdateHsn")]
        public string UpdateHsn([FromBody] HsnModel objHsn)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@HSNID",objHsn.HSN_ID),
                                            new SqlParameter("@TAXID",objHsn.TAX_ID),
                                            new SqlParameter("@HSNCODE",objHsn.HSN_CODE),
                                            new SqlParameter("@DESCRIPTION",objHsn.DESCRIPTION),
                                            //new SqlParameter("@HSNREMARK",objHsn.HSNREMARK),
                                            new SqlParameter("@ISEXEMPTED",objHsn.ISEXEMPTED),
                                            new SqlParameter("@MODE",objHsn.MODE),
                                            new SqlParameter("@REMARKS",objHsn.REMARK),
                                            new SqlParameter("@CREATEDBY",objHsn.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objHsn.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTHSN_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetHsnDetail")]
        public string GetHsnDetail([FromBody] HsnModel objMasterCategory)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@HSNID",objMasterCategory.HSN_ID),
                                            new SqlParameter("@MODE",objMasterCategory.MODE),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTHSN_I", spparams);
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
        [Route("DeactiveHsn")]
        public string DeactiveHsn([FromBody] HsnModel objHsn)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@HSNID",objHsn.HSN_ID),
                                            new SqlParameter("@TAXID",objHsn.TAX_ID),
                                            new SqlParameter("@HSNCODE",objHsn.HSN_CODE),
                                            new SqlParameter("@DESCRIPTION",objHsn.DESCRIPTION),
                                            //new SqlParameter("@HSNREMARK",objHsn.HSNREMARK),
                                            new SqlParameter("@ISEXEMPTED",objHsn.ISEXEMPTED),
                                            new SqlParameter("@MODE",objHsn.MODE),
                                            new SqlParameter("@CREATEDBY",objHsn.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objHsn.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTHSN_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
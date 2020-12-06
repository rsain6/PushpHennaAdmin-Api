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
    [Route("api/MasterCategory")]
    public class MasterCategoryController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;

        string connection = string.Empty;
        public MasterCategoryController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Show Category List using page index and page size
        /// </summary>
        /// <param name="objCategoryListModel"> page index and page size</param>
        /// <returns>List of category </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("MasterCategoryList")]
        public string MasterCategoryList([FromBody]  GridListModel objCategoryListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objCategoryListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objCategoryListModel.PAGESIZE)
                                          };
                outPut = objDAL.GetJson(connection, "MSTMASTERCATEGORY_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetMasterCategoryDetail")]
        public string GetMasterCategoryDetail([FromBody] MasterCategoryModel objMasterCategory)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MSTCATID",objMasterCategory.MSTCATID),
                                            new SqlParameter("@MODE",objMasterCategory.MODE),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTMASTERCATEGORY_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Using mode in SP Save/Update/Deactive and active Category Master data 
        /// </summary>
        /// <param name="objMasterCategory">MasterCategoryModel object </param>
        /// <returns>Success with return Id</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveMasterCategory")]
        public string SaveMasterCategory([FromBody] MasterCategoryModel objMasterCategory)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MSTCATID",objMasterCategory.MSTCATID),
                                            new SqlParameter("@NAME", objMasterCategory.NAME) ,
                                            new SqlParameter("@MODE",objMasterCategory.MODE),
                                            new SqlParameter("@REMARKS",objMasterCategory.REMARK),
                                            new SqlParameter("@CREATEDBY",objMasterCategory.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objMasterCategory.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTMASTERCATEGORY_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Update master category data 
        /// </summary>
        /// <param name="objMasterCategory">mstcatid,name,mode,remark,createdby,ipaddress</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateMasterCategory")]
        public string UpdateMasterCategory([FromBody] MasterCategoryModel objMasterCategory)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MSTCATID",objMasterCategory.MSTCATID),
                                            new SqlParameter("@NAME", objMasterCategory.NAME) ,
                                            new SqlParameter("@MODE",objMasterCategory.MODE),
                                            new SqlParameter("@REMARKS",objMasterCategory.REMARK),
                                            new SqlParameter("@CREATEDBY",objMasterCategory.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objMasterCategory.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTMASTERCATEGORY_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// deactive master category data 
        /// </summary>
        /// <param name="objMasterCategory">mstcatid, mode</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DeactiveMasterCategory")]
        public string DeactiveMasterCategory([FromBody] MasterCategoryModel objMasterCategory)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MSTCATID",objMasterCategory.MSTCATID),
                                            new SqlParameter("@MODE",objMasterCategory.MODE),
                                            new SqlParameter("@CREATEDBY",objMasterCategory.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objMasterCategory.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTMASTERCATEGORY_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
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

namespace SIMS_MEDICAL.Controllers
{
    [Route("api/Group")]
    public class GroupController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;
        public GroupController(IConfiguration config)
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
        [Route("GroupList")]
        public string GroupList([FromBody]  GridListModel objCategoryListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objCategoryListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objCategoryListModel.PAGESIZE)
                                          };
                outPut = objDAL.GetJson(connection, "MSTMKTGROUP_S", spparams);
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
        [Route("SaveGroup")]
        public string SaveGroup([FromBody] Group objGroup)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MKTGROUP_ID",objGroup.MKTGROUP_ID),
                                            new SqlParameter("@NAME",objGroup.NAME),
                                            new SqlParameter("@ALIAS",objGroup.ALIAS),
                                            new SqlParameter("@MKTGROUPTYPE",objGroup.MKTGROUPTYPE),
                                            new SqlParameter("@MODE",objGroup.MODE),
                                            new SqlParameter("@REMARKS",objGroup.REMARKS),
                                            new SqlParameter("@CREATEDBY",objGroup.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objGroup.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTMKTGROUP_I", spparams);
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
        [Route("UpdateGroup")]
        public string UpdateGroup([FromBody] Group objGroup)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MKTGROUP_ID",objGroup.MKTGROUP_ID),
                                            new SqlParameter("@NAME",objGroup.NAME),
                                            new SqlParameter("@ALIAS",objGroup.ALIAS),
                                            new SqlParameter("@MKTGROUPTYPE",objGroup.MKTGROUPTYPE),
                                            new SqlParameter("@MODE",objGroup.MODE),
                                            new SqlParameter("@REMARKS",objGroup.REMARKS),
                                            new SqlParameter("@CREATEDBY",objGroup.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objGroup.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTMKTGROUP_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetGroupDetail")]
        public string GetGroupDetail([FromBody] Group objGroup)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MKTGROUP_ID",objGroup.MKTGROUP_ID),
                                            new SqlParameter("@MODE",objGroup.MODE),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTMKTGROUP_I", spparams);
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
        [Route("DeactiveGroup")]
        public string DeactiveGroup([FromBody] Group objGroup)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MKTGROUP_ID",objGroup.MKTGROUP_ID),
                                            new SqlParameter("@NAME",objGroup.NAME),
                                            new SqlParameter("@ALIAS",objGroup.ALIAS),
                                            new SqlParameter("@MKTGROUPTYPE",objGroup.MKTGROUPTYPE),
                                            new SqlParameter("@MODE",objGroup.MODE),
                                            new SqlParameter("@REMARKS",objGroup.REMARKS),
                                            new SqlParameter("@CREATEDBY",objGroup.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objGroup.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTMKTGROUP_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
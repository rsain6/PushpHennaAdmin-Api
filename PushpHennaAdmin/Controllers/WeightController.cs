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
    [Route("api/Weight")]
    public class WeightController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;
        public WeightController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;

        }
        #region Master  SaveWeightClass 
        /// <summary>
        /// Using mode in SP Save/Update/Deactive and active WeightClass  data 
        /// </summary>
        /// <param name="WeightModel">objWeightModel object </param>
        /// <returns>WEIGHTCLASSID with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveWeightClass")]
        public string SaveWeightClass([FromBody] Weight objWeightModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@NAME", objWeightModel.NAME) ,
                                            new SqlParameter("@MODE",objWeightModel.MODE),
                                            new SqlParameter("@REMARKS",objWeightModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objWeightModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objWeightModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTWEIGHTCLASS_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Update Weight Class data by Id
        /// </summary>
        /// <param name="objWeightModel">weightclassid,name,mode,remark,createdby,ipaddress</param>
        /// <returns>WEIGHTCLASSID with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateWeightClass")]
        public string UpdateWeightClass([FromBody] Weight objWeightModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@WEIGHTCLASSID",objWeightModel.weightclassid),
                                            new SqlParameter("@NAME", objWeightModel.NAME) ,
                                            new SqlParameter("@MODE",objWeightModel.MODE),
                                            new SqlParameter("@REMARKS",objWeightModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objWeightModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objWeightModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTWEIGHTCLASS_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        ///deactive Weight Class by Id
        /// </summary>
        /// <param name="objWeightModel">weightclassid</param>
        /// <returns>WEIGHTCLASSID with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DeactiveWeightClass")]
        public string DeactiveWeightClass([FromBody] Weight objWeightModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@WEIGHTCLASSID",objWeightModel.weightclassid),
                                           new SqlParameter("@MODE",objWeightModel.MODE),
                                            new SqlParameter("@CREATEDBY",objWeightModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objWeightModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTWEIGHTCLASS_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        ///Get Weight Class Detail  by Id
        /// </summary>
        /// <param name="objWeightModel">weightclassid</param>
        /// <returns>WEIGHTCLASSID with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetWeightClassDetail")]
        public string GetWeightClassDetail([FromBody] Weight objWeightModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@WEIGHTCLASSID",objWeightModel.weightclassid),
                                            new SqlParameter("@MODE",objWeightModel.MODE),
                                            new SqlParameter("@CREATEDBY",objWeightModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objWeightModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTWEIGHTCLASS_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Show WeightClass List using page index and page size
        /// </summary>
        /// <param name="objGridListModel"> page index and page size</param>
        /// <returns>List of WeightClass </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetWeightClassList")]
        public string GetWeightClassList([FromBody]  GridListModel objGridListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objGridListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objGridListModel.PAGESIZE),
                                          };
                outPut = objDAL.GetJson(connection, "MSTWEIGHTCLASS_S_LIST", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        #endregion
    }
}
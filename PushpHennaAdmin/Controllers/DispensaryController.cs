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
    [Route("api/Dispensary")]
    public class DispensaryController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;
        public DispensaryController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;

        }

        #region Master  SaveWeightClass 
        /// <summary>
        /// Using mode in SP Save/Update/Deactive and active Dispensary  data 
        /// </summary>
        /// <param name="WeightModel">objWeightModel object </param>
        /// <returns>DispensaryID with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveDispensary")]
        public string SaveDispensary([FromBody] Dispensary objWeightModel)
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
                outPut = objDAL.PostWithResultCode(connection, "MSTDISPENSARY_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Update Dispensary data by Id
        /// </summary>
        /// <param name="objWeightModel">Dispensaryid,name,mode,remark,createdby,ipaddress</param>
        /// <returns>DispensaryID with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateDispensary")]
        public string UpdateDispensary([FromBody] Dispensary objWeightModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@DISPENSARYID",objWeightModel.dispensaryid),
                                            new SqlParameter("@NAME", objWeightModel.NAME) ,
                                            new SqlParameter("@MODE",objWeightModel.MODE),
                                            new SqlParameter("@REMARKS",objWeightModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objWeightModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objWeightModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTDISPENSARY_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        ///deactive Dispensary by Id
        /// </summary>
        /// <param name="objWeightModel">Dispensaryid</param>
        /// <returns>DispensaryID with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DeactiveDispensary")]
        public string DeactiveDispensary([FromBody] Dispensary objWeightModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@DISPENSARYID",objWeightModel.dispensaryid),
                                           new SqlParameter("@MODE",objWeightModel.MODE),
                                            new SqlParameter("@CREATEDBY",objWeightModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objWeightModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTDISPENSARY_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        ///Get Dispensary Detail  by Id
        /// </summary>
        /// <param name="objWeightModel">Dispensaryid</param>
        /// <returns>DispensaryID with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetDispensaryDetail")]
        public string GetDispensaryDetail([FromBody] Dispensary objWeightModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@DISPENSARYID",objWeightModel.dispensaryid),
                                            new SqlParameter("@MODE",objWeightModel.MODE),
                                            new SqlParameter("@CREATEDBY",objWeightModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objWeightModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTDISPENSARY_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Show Dispensary List using page index and page size
        /// </summary>
        /// <param name="objGridListModel"> page index and page size</param>
        /// <returns>List of Dispensary </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetDispensaryList")]
        public string GetDispensaryList([FromBody]  GridListModel objGridListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objGridListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objGridListModel.PAGESIZE),
                                          };
                outPut = objDAL.GetJson(connection, "MSTDISPENSARY_S_LIST", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Show Dispensary List FOR DROPDOWN
        /// </summary>
        /// <param name="objGridListModel"> page index and page size</param>
        /// <returns>List of Dispensary </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetDispensaryselect")]
        public string GetDispensaryselect([FromBody]  GridListModel objGridListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@MODE",objGridListModel.MODE),
                                            new SqlParameter("@DISPENSARYID",objGridListModel.DISPENSARYID),
                                          };
                outPut = objDAL.GetJson(connection, "MSTDISPENSARY_S", spparams);
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
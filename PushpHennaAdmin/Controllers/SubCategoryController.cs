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
    [Route("api/SubCategory")]
    public class SubCategoryController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;
        public SubCategoryController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        #region Master Sub Category 
        /// <summary>
        /// Using mode in SP Save/Update/Deactive and active Sub Category Master data 
        /// </summary>
        /// <param name="objMasterCategory">SubCategoryModel object </param>
        /// <returns>Success with return Id</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveSubCategory")]
        public string SaveSubCategory([FromBody] SubCategoryModel objSubCategoryModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@SUBCATID",objSubCategoryModel.SUBCATID),
                                            new SqlParameter("@CATID",objSubCategoryModel.CATID),
                                            new SqlParameter("@NAME", objSubCategoryModel.NAME) ,
                                            new SqlParameter("@MODE",objSubCategoryModel.MODE),
                                            new SqlParameter("@REMARKS",objSubCategoryModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objSubCategoryModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objSubCategoryModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTSUBCATEGORY_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update SubCategory data 
        /// </summary>
        /// <param name="objMasterCategory">mstcatid,name,mode,remark,createdby,ipaddress</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateSubCategory")]
        public string UpdateSubCategory([FromBody] SubCategoryModel objSubCategoryModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@SUBCATID",objSubCategoryModel.SUBCATID),
                                            new SqlParameter("@CATID",objSubCategoryModel.CATID),
                                            new SqlParameter("@NAME", objSubCategoryModel.NAME) ,
                                            new SqlParameter("@MODE",objSubCategoryModel.MODE),
                                            new SqlParameter("@REMARKS",objSubCategoryModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objSubCategoryModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objSubCategoryModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTSUBCATEGORY_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// deactive SUB category data 
        /// </summary>
        /// <param name="objSubCategoryModel">subcatid,catid, mode</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DeactiveSubCategory")]
        public string DeactiveSubCategory([FromBody] SubCategoryModel objSubCategoryModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@SUBCATID",objSubCategoryModel.SUBCATID),
                                            new SqlParameter("@CATID",objSubCategoryModel.CATID),
                                            new SqlParameter("@NAME", objSubCategoryModel.NAME) ,
                                            new SqlParameter("@MODE",objSubCategoryModel.MODE),
                                            new SqlParameter("@REMARKS",objSubCategoryModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objSubCategoryModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objSubCategoryModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTSUBCATEGORY_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// select sub category by id
        /// </summary>
        /// <param name="objSubCategoryModel">SUBCATID,CATID, mode</param>
        /// <returns>sub category detail</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetSubCategoryDetail")]
        public string GetSubCategoryDetail([FromBody] SubCategoryModel objSubCategoryModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@SUBCATID",objSubCategoryModel.SUBCATID),
                                            new SqlParameter("@CATID",objSubCategoryModel.CATID),
                                            new SqlParameter("@NAME", objSubCategoryModel.NAME) ,
                                            new SqlParameter("@MODE",objSubCategoryModel.MODE),
                                            new SqlParameter("@REMARKS",objSubCategoryModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objSubCategoryModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objSubCategoryModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTSUBCATEGORY_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Show Sub Category List using page index and page size
        /// </summary>
        /// <param name="objGridListModel"> page index and page size</param>
        /// <returns>List of category </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetSubCategoryList")]
        public string GetSubCategoryList([FromBody]  GridListModel objCategoryListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objCategoryListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objCategoryListModel.PAGESIZE),
                                            new SqlParameter("@MSTCATID",objCategoryListModel.MSTCATID),
                                             new SqlParameter("@CATID ",objCategoryListModel.CATID),
                                          };
                outPut = objDAL.GetJson(connection, "MSTSUBCATEGORY_S", spparams);
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
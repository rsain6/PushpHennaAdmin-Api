using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;
using System;
using System.Data.SqlClient;

namespace PushpHennaAdmin.Controllers
{
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;
        public CategoryController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;

        }

        #region Master Category 
        /// <summary>
        /// Using mode in SP Save/Update/Deactive and active Category  data 
        /// </summary>
        /// <param name="objCategoryModel">CategoryModel object </param>
        /// <returns>Success with return Id</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveCategory")]
        public string SaveCategory([FromBody] CategoryModel objCategoryModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MSTCATID",objCategoryModel.MSTCATID),
                                            new SqlParameter("@CATID",objCategoryModel.CATID),
                                            new SqlParameter("@NAME", objCategoryModel.NAME) ,
                                            new SqlParameter("@MODE",objCategoryModel.MODE),
                                            new SqlParameter("@REMARKS",objCategoryModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objCategoryModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objCategoryModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTCATEGORY_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update Category data 
        /// </summary>
        /// <param name="objCategoryModel">mstcatid,catid,size_str,weight_str,material_str,name,mode,remark,createdby,ipaddress</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateCategory")]
        public string UpdateCategory([FromBody] CategoryModel objCategoryModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MSTCATID",objCategoryModel.MSTCATID),
                                            new SqlParameter("@CATID",objCategoryModel.CATID),
                                            new SqlParameter("@NAME", objCategoryModel.NAME) ,
                                            new SqlParameter("@MODE",objCategoryModel.MODE),
                                            new SqlParameter("@REMARKS",objCategoryModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objCategoryModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objCategoryModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTCATEGORY_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// deactive category data 
        /// </summary>
        /// <param name="objCategoryModel">catid, mode</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DeactiveCategory")]
        public string DeactiveCategory([FromBody] CategoryModel objCategoryModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {new SqlParameter("@MSTCATID",objCategoryModel.MSTCATID),
                                            new SqlParameter("@CATID",objCategoryModel.CATID),
                                            new SqlParameter("@MODE",objCategoryModel.MODE),
                                            new SqlParameter("@CREATEDBY",objCategoryModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objCategoryModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTCATEGORY_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// select category by id
        /// </summary>
        /// <param name="objCategoryModel">CATID, mode</param>
        /// <returns> category detail</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetCategoryDetail")]
        public string GetCategoryDetail([FromBody] CategoryModel objCategoryModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@CATID",objCategoryModel.CATID),
                                            new SqlParameter("@MSTCATID",objCategoryModel.MSTCATID),
                                            new SqlParameter("@MODE",objCategoryModel.MODE),
                                            new SqlParameter("@CREATEDBY",objCategoryModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objCategoryModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTCATEGORY_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Category List using page index and page size
        /// </summary>
        /// <param name="objGridListModel"> page index and page size</param>
        /// <returns>List of category </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetCategoryList")]
        public string GetCategoryList([FromBody]  GridListModel objCategoryListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objCategoryListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objCategoryListModel.PAGESIZE),
                                            new SqlParameter("@MSTCATID",objCategoryListModel.MSTCATID),
                                          };
                outPut = objDAL.GetJson(connection, "MSTCATEGORY_S", spparams);
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
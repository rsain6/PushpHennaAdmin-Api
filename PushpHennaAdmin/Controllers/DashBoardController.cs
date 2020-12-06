using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;

namespace PushpHennaAdmin.Controllers
{
   // [Produces("application/json")]
    [Route("api/DashBoard")]
    public class DashBoardController : Controller
    {
        DAL objDAL = DAL.GetObject(); //new DAL();
        private IConfiguration _config;
        string connection = string.Empty;
        public DashBoardController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Get User dashboard according to Access (store & godown)
        /// </summary>
        /// <param name="objRptBillWiseSellReport"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UserDashBoard")]
        public string UserDashBoard([FromBody] DashBoardModel objDashBoardModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DATEFROM", objDashBoardModel.DateFrom),                                         
                                            new SqlParameter("@DATETO", objDashBoardModel.DateTo),
                                            new SqlParameter("@CREATEDBY", objDashBoardModel.CreatedBy)                                            
                                            };
                outPut = objDAL.GetJson(connection, "DASHBOARD", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get admin dashboard according to Access (store & godown)
        /// </summary>
        /// <param name="objRptBillWiseSellReport"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("AdminDashBoardAdmin")]
        public string AdminDashBoardAdmin([FromBody] DashBoardModel objDashBoardModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DATEFROM", objDashBoardModel.DateFrom),
                                            new SqlParameter("@DATETO", objDashBoardModel.DateTo),
                                            new SqlParameter("@WORKFOR", objDashBoardModel.WorkFor),
                                            new SqlParameter("@WORKFORIDSTR", objDashBoardModel.WorkForIdStr)
                                            };
                outPut = objDAL.GetJson(connection, "DASHBOARD_ADMIN", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("AdminDashBoardStore")]
        public string AdminDashBoardStore([FromBody] DashBoardModel objDashBoardModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DATEFROM", objDashBoardModel.DateFrom),
                                            new SqlParameter("@DATETO", objDashBoardModel.DateTo),
                                            new SqlParameter("@WORKFOR", objDashBoardModel.WorkFor),
                                            new SqlParameter("@WORKFORIDSTR", objDashBoardModel.WorkForIdStr)
                                            };
                outPut = objDAL.GetJson(connection, "DASHBOARD_STORE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
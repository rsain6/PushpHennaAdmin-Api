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
    [Route("api/Godown")]
    public class GodownController : Controller
    {
        DAL objDAL = DAL.GetObject(); //new DAL();
        private IConfiguration _config;
        string connection = string.Empty;

        public GodownController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Save new Godown 
        /// </summary>
        /// <param name="objGodownModel">godown_name,person_name,mobile, phone, email,address ,pincode ,cityid ,district_id ,createdby ,ipaddress </param>
        /// <returns>Godown Id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveGodownEntry")]
        public string SaveGodownEntry([FromBody] GodownModel objGodownModel)
        {

            string outPut = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(objGodownModel.mstcatidstr))
                {
                    objGodownModel.mstcatidstr = objGodownModel.mstcatidstr.Replace(objGodownModel.mstcatidstr.Substring(0, 1), "");
                    objGodownModel.mstcatidstr = objGodownModel.mstcatidstr.Replace(objGodownModel.mstcatidstr.Substring(objGodownModel.mstcatidstr.Length - 1), "");
                }
                SqlParameter[] spparams = { new SqlParameter("@GODOWN_NAME", objGodownModel.godown_name) ,
                                            new SqlParameter("@PERSON_NAME",objGodownModel.person_name),
                                            new SqlParameter("@MOBILE",objGodownModel.mobile),
                                            new SqlParameter("@PHONE",objGodownModel.phone),
                                            new SqlParameter("@EMAIL",objGodownModel.email),
                                            new SqlParameter("@ADDRESS",objGodownModel.address),
                                            new SqlParameter("@PINCODE",objGodownModel.pincode),
                                            new SqlParameter("@STATEID",objGodownModel.stateid),
                                            new SqlParameter("@DISTRICT_ID",objGodownModel.district_id),
                                            new SqlParameter("@CITYID",objGodownModel.cityid),
                                            //new SqlParameter("@MSTCATIDSTR",objGodownModel.mstcatidstr),
                                            new SqlParameter("@GSTNO",objGodownModel.GstNo),
                                            new SqlParameter("@DLNO",objGodownModel.DLNo),
                                            new SqlParameter("@TINNO",objGodownModel.TINNO),
                                            new SqlParameter("@ISSTORE",objGodownModel.chkisstore),
                                            new SqlParameter("@CREATEDBY",objGodownModel.createdby),
                                            new SqlParameter("@IPADDRESS",objGodownModel.ipaddress),
                                            new SqlParameter("@UPGODOWNID",objGodownModel.UpGodownID),
                                            new SqlParameter("@GODOWNTYPEID",objGodownModel.GodownTypeID),
                                            new SqlParameter("@RESULTMSG","") };
                outPut = objDAL.PostWithResultCode(connection, "MSTGODOWN_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Get Godown drop down list
        /// </summary>
        /// <returns>Godown Id and Name</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetGodownList")]
        public string GetGodownList([FromBody] FilterDropDown objFilterDropDown)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@GODOWNIDSTR", objFilterDropDown.FilterStr) };
                outPut = objDAL.GetJson(connection, "MSTGODOWN_S_LIST", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get GoDown list in grid 
        /// </summary>
        /// <param name="objGridListModel">PAGEINDEX, PAGESIZE</param>
        /// <returns>Name,Email,Phone, etc...</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetRegisteredGODown")]
        public string GetRegisteredGODown([FromBody]  GridListFilterModel objGridListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PAGEINDEX",objGridListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objGridListModel.PAGESIZE),
                                            new SqlParameter("@NAME",objGridListModel.NAME)
                                          };
                outPut = objDAL.GetJson(connection, "MSTGODOWN_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetGodownById/{Id}")]
        public string GetGodownById(string Id)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@GODOWN_ID ", Id) };
                outPut = objDAL.GetJson(connection, "MSTGODOWN_S_BYID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update godown data by godown id
        /// </summary>
        /// <param name="objGodownModel">godown_id,godown_name,person_name,mobile, phone, email,address ,pincode ,cityid ,district_id ,createdby ,ipaddress</param>
        /// <returns>Godown Id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateGodownEntry")]
        public string UpdateGodownEntry([FromBody] GodownModel objGodownModel)
        {
            string outPut = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(objGodownModel.mstcatidstr))
                {
                    objGodownModel.mstcatidstr = objGodownModel.mstcatidstr.Replace(objGodownModel.mstcatidstr.Substring(0, 1), "");
                    objGodownModel.mstcatidstr = objGodownModel.mstcatidstr.Replace(objGodownModel.mstcatidstr.Substring(objGodownModel.mstcatidstr.Length - 1), "");
                }
                SqlParameter[] spparams = {
                                            new SqlParameter("@GODOWN_ID", objGodownModel.godown_id) ,
                                            new SqlParameter("@GODOWN_NAME", objGodownModel.godown_name) ,
                                            new SqlParameter("@PERSON_NAME",objGodownModel.person_name),
                                            new SqlParameter("@MOBILE",objGodownModel.mobile),
                                            new SqlParameter("@PHONE",objGodownModel.phone),
                                            new SqlParameter("@EMAIL",objGodownModel.email),
                                            new SqlParameter("@ADDRESS",objGodownModel.address),
                                            new SqlParameter("@PINCODE",objGodownModel.pincode),
                                            new SqlParameter("@STATEID",objGodownModel.stateid),
                                            new SqlParameter("@DISTRICT_ID",objGodownModel.district_id),
                                            new SqlParameter("@CITYID",objGodownModel.cityid),
                                            //new SqlParameter("@MSTCATIDSTR",objGodownModel.mstcatidstr),
                                            new SqlParameter("@GSTNO",objGodownModel.GstNo),
                                            new SqlParameter("@DLNO",objGodownModel.DLNo),
                                            new SqlParameter("@TINNO",objGodownModel.TINNO),
                                            new SqlParameter("@REMARKS",objGodownModel.remarks),
                                            new SqlParameter("@CREATEDBY",objGodownModel.createdby),
                                            new SqlParameter("@IPADDRESS",objGodownModel.ipaddress),
                                            new SqlParameter("@UPGODOWNID",objGodownModel.UpGodownID),
                                            new SqlParameter("@GODOWNTYPEID",objGodownModel.GodownTypeID),
                                            new SqlParameter("@RESULTMSG","") };
                outPut = objDAL.PostWithResultCode(connection, "MSTGODOWN_U", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Approval for Godown Class
        /// </summary>
        /// <param name="objMasterApprovalModel">OBJECTID,OBJECTNAME(tablename),REMARKS,STATUSID,ISACTIVE,CREATEDBY,IPADDRESS</param>
        /// <returns>Godown_ID  with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("ApprovalGodownClass")]
        public string ApprovalGodownClass([FromBody]  MasterApprovalModel objMasterApprovalModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("OBJECTID", objMasterApprovalModel.OBJECTID),
                                            new SqlParameter("OBJECTNAME", objMasterApprovalModel.OBJECTNAME),
                                            new SqlParameter("REMARKS", objMasterApprovalModel.REMARKS),
                                            new SqlParameter("STATUSID", objMasterApprovalModel.STATUSID),
                                            new SqlParameter("ISACTIVE", objMasterApprovalModel.ISACTIVE),
                                            new SqlParameter("CREATEDBY", objMasterApprovalModel.CREATEDBY),
                                            new SqlParameter("IPADDRESS", objMasterApprovalModel.IPADDRESS),
                                             new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MASTERDATATRAIL_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update godown active and deactive status by godown id
        /// </summary>
        /// <param name="objGodownStatus">godown id , active /deactive status</param>
        /// <returns>success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateGodownActiveStatus")]
        public string UpdateGodownActiveStatus([FromBody] GodownStatus objGodownStatus)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@GODOWN_ID", objGodownStatus.godown_id),
                                            new SqlParameter("@ISACTIVE",objGodownStatus.isactive),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTGODOWN_U_ISACTIVE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
                //log.Error(outPut);
            }
            return outPut;
        }


        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetGodownTypeList")]
        public string GetGodownTypeList()
        {

            string outPut = string.Empty;
            try
            {
               // SqlParameter[] spparams = { new SqlParameter("@GODOWN_ID", objGodownStatus.godown_id)};
                outPut = objDAL.GetJson(connection, "MSTGODOWNTYPE_S_LIST", null);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
                //log.Error(outPut);
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetGodownByType")]
        public string GetGodownByType([FromBody] GodownStatus objGodownStatus)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@GODOWNTYPEID", objGodownStatus.GodownTypeId),
                                            new SqlParameter("@USERID",objGodownStatus.UserId)} ;
                outPut = objDAL.GetJson(connection, "MSTGODOWN_S_LISTBYTYPE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
                //log.Error(outPut);
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetGodownByUser")]
        public string GetGodownByUser([FromBody] GodownStatus objGodownStatus)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@GODOWNTYPEID", objGodownStatus.GodownTypeId)};
                outPut = objDAL.GetJson(connection, "MSTGODOWN_S_LISTBYTYPEfORUSER", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
                //log.Error(outPut);
            }
            return outPut;
        }
    }
}
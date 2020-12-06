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
    //[Produces("application/json")]
    [Route("api/Vendor")]
    public class VendorController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;

        public VendorController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Save Vendor Information
        /// </summary>
        /// <param name="objVendorModel">COMPANY_NAME,PERSON_NAME,MOBILE,PHONE,EMAIL,ADDRESS,PINCODE,CITYID,DISTRICT_ID,CREATEDBY,IPADDRESS</param>
        /// <returns>Successful mesage with Vendor Id</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveVendor")]
        public string SaveVendor([FromBody] VendorModel objVendorModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@COMPANY_NAME",objVendorModel.COMPANY_NAME),
                                            new SqlParameter("@PERSON_NAME", objVendorModel.PERSON_NAME) ,
                                            new SqlParameter("@MOBILE",objVendorModel.MOBILE),
                                            new SqlParameter("@PHONE",objVendorModel.PHONE),
                                            new SqlParameter("@EMAIL",objVendorModel.EMAIL),
                                            new SqlParameter("@ADDRESS",objVendorModel.ADDRESS),
                                            new SqlParameter("@PINCODE",objVendorModel.PINCODE),
                                            new SqlParameter("@STATEID",objVendorModel.STATEID),
                                            new SqlParameter("@CITYID",objVendorModel.CITYID),
                                            new SqlParameter("@DISTRICT_ID",objVendorModel.DISTRICT_ID),
                                            new SqlParameter("@GSTNO",objVendorModel.GSTIN),
                                            new SqlParameter("@ISIGSTAPPLY",objVendorModel.ISIGSTAPPLY),
                                            new SqlParameter("@MSTCATIDSTR",objVendorModel.MSTCATIDSTR),
                                            new SqlParameter("@CREATEDBY",objVendorModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objVendorModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTVENDOR_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Get Vendor Information by Vendor ID
        /// </summary>
        /// <param name="Id">Vendor ID</param>
        /// <returns>Vendor Detail Name,COMPANY_NAME,MOBILE,PHONE,EMAIL,ADDRESS,PINCODE,CITYID,DISTRICT_ID For Edit</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetVendorById/{Id}")]
        public string GetVendorById(string Id)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@VENDOR_ID  ", Id) };
                outPut = objDAL.GetJson(connection, "MSTVENDOR_S_BYID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update Vendor Data by Vendor Id
        /// </summary>
        /// <param name="objVendorModel">COMPANY_NAME,PERSON_NAME,MOBILE,PHONE,EMAIL,ADDRESS,PINCODE,CITYID,DISTRICT_ID,CREATEDBY,IPADDRESS</param>
        /// <returns>Success Msg</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateVendor")]
        public string UpdateVendor([FromBody] VendorModel objVendorModel)
        {
            string outPut = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(objVendorModel.MSTCATIDSTR))
                {
                    objVendorModel.MSTCATIDSTR = objVendorModel.MSTCATIDSTR.Replace(objVendorModel.MSTCATIDSTR.Substring(0, 1), "");
                    objVendorModel.MSTCATIDSTR = objVendorModel.MSTCATIDSTR.Replace(objVendorModel.MSTCATIDSTR.Substring(objVendorModel.MSTCATIDSTR.Length - 1), "");
                }

                SqlParameter[] spparams =  {
                                            new SqlParameter("@VENDOR_ID ",objVendorModel.VENDOR_ID),
                                            new SqlParameter("@COMPANY_NAME",objVendorModel.COMPANY_NAME),
                                            new SqlParameter("@PERSON_NAME", objVendorModel.PERSON_NAME) ,
                                            new SqlParameter("@MOBILE",objVendorModel.MOBILE),
                                            new SqlParameter("@PHONE",objVendorModel.PHONE),
                                            new SqlParameter("@EMAIL",objVendorModel.EMAIL),
                                            new SqlParameter("@ADDRESS",objVendorModel.ADDRESS),
                                            new SqlParameter("@PINCODE",objVendorModel.PINCODE),
                                            new SqlParameter("@STATEID",objVendorModel.STATEID),
                                            new SqlParameter("@CITYID",objVendorModel.CITYID),
                                            new SqlParameter("@DISTRICT_ID",objVendorModel.DISTRICT_ID),
                                            new SqlParameter("@REMARKS",objVendorModel.REMARKS),
                                            new SqlParameter("@GSTNO",objVendorModel.GSTIN),
                                            new SqlParameter("@ISIGSTAPPLY",objVendorModel.ISIGSTAPPLY),
                                            new SqlParameter("@MSTCATIDSTR",objVendorModel.MSTCATIDSTR),
                                            new SqlParameter("@CREATEDBY",objVendorModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objVendorModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTVENDOR_U", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Update Vendor Active/ Deactive Status
        /// </summary>
        /// <param name="objVendorStatus">vendor id and True and false</param>
        /// <returns>Success Msg</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateVendorActiveStatus")]
        public string UpdateVendorActiveStatus([FromBody] VendorStatus objVendorStatus)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@VENDOR_ID ", objVendorStatus.VENDOR_ID),
                                            new SqlParameter("@ISACTIVE ",objVendorStatus.ISACTIVE),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTVENDOR_U_ISACTIVE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        
        /// <summary>
        /// Show Vendor list in Grid with paging 
        /// </summary>
        /// <param name="PageIndex">Numeric number</param>
        /// <param name="PageSize">Numeric number</param>
        /// <returns>lsit of vendor (NAME , Phone, Eail.. etc ) </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetVendorList")] //{PageIndex}/{PageSize}
        public string GetVendorList([FromBody]  GridListFilterModel objGridListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objGridListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objGridListModel.PAGESIZE),
                                            new SqlParameter("@NAME",objGridListModel.NAME)
                                          };
                outPut = objDAL.GetJson(connection, "MSTVENDOR_S", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Approval for Vendor
        /// </summary>
        /// <param name="objMasterApprovalModel">OBJECTID,OBJECTNAME(tablename),REMARKS,STATUSID,ISACTIVE,CREATEDBY,IPADDRESS</param>
        /// <returns>MASTERDATATRAIL_ID  with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("ApprovalVendor")]
        public string ApprovalVendor([FromBody]  MasterApprovalModel objMasterApprovalModel)
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

        #region Create Vendor Mapping with Compnay

        /// <summary>
        /// Get Vendor List by Group Id 
        /// </summary>
        /// <param name="objFilterProductModel">MKTGROUPID</param>
        /// <returns>VendorId, Name, Ismap</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetVendorListByGroupId")]
        public string GetVendorListByGroupId([FromBody]  VendorGroupMap objVendorGroupMap)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MKTGROUPID", objVendorGroupMap.MKTGROUPID) };
                outPut = objDAL.GetJson(connection, "MSTVENDOR_S_LIST_BYMKTGROUP", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Save Vendor Mapping with company
        /// </summary>
        /// <param name="objVendorGroupMap">MKTGROUPID,VendorSTR</param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveVendorGroupMapping")]
        public string SaveVendorGroupMapping([FromBody]  VendorGroupMap objVendorGroupMap)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MKTGROUPID", objVendorGroupMap.MKTGROUPID),
                                            new SqlParameter("@VENDORIDSTR", objVendorGroupMap.VENDORIDSTR),
                                            new SqlParameter("@CREATEDBY",objVendorGroupMap.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objVendorGroupMap.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTMKTGROUP_VENDOR_I", spparams);
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
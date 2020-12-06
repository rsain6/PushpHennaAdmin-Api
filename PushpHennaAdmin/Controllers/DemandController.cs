using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;
using System;
using System.Data.SqlClient;

namespace PushpHennaAdmin.Controllers
{
    [Route("api/Demand")]
    public class DemandController : Controller
    {
        DAL objDAL = DAL.GetObject(); //new DAL();
        private IConfiguration _config;
        string connection = string.Empty;

        public DemandController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Create Store Demand 
        /// </summary>
        /// <param name="objDemandModel">GODOWNID,FROMDATE,TODATE,PRODUCT_JSON Lsit of Product with Qty,CREATEDBY,IPADDRESS</param>
        /// <returns>Id with Success Message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveDemand")]
        public string SaveDemand([FromBody] DemandModel objDemandModel)
        {

            string outPut = string.Empty;
            string JsonString = JsonConvert.SerializeObject(objDemandModel);
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objDemandModel.Product_Json);
                SqlParameter[] spparams = { new SqlParameter("@DEMANDFROMID",objDemandModel.DemandFrom_Id),
                                            new SqlParameter("@DEMANDFROMBY", objDemandModel.DemandFrom_By),
                                            new SqlParameter("@DEMANDTOID",objDemandModel.DemandTo_Id),
                                            new SqlParameter("@DEMANDTOBY",objDemandModel.DemandTo_By),
                                            new SqlParameter("@REMARKS",objDemandModel.Remarks),
                                            new SqlParameter("@PRODUCT_JSON",ProductJson),
                                            new SqlParameter("@INDENTNO",objDemandModel.IndentNo),
                                            new SqlParameter("@CREATEDBY",objDemandModel.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objDemandModel.IpAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLGODOWN_DEMAND_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// show demand list in grid format
        /// </summary>
        /// <param >DEMANDFROMIDSTR,DEMANDFROMBY,PAGEINDEX ,PAGESIZE </param>
        /// <returns>list of godown name,personname,address,mobile, etc..</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetDemandList")] //{PageIndex}/{PageSize}
        public string GetDemandList([FromBody] PurchaseReport objPurchaseReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@FROMDATE", objPurchaseReport.FromDate),
                                            new SqlParameter("@TODATE", objPurchaseReport.ToDate),
                                            new SqlParameter("@DEMANDFROMIDSTR", objPurchaseReport.DemandFrom_Id_STR),
                                            new SqlParameter("@DEMANDFROMBY", objPurchaseReport.DemandFrom_By),
                                            new SqlParameter("@DEMANDTOID", objPurchaseReport.DemandTo_Id),
                                            new SqlParameter("@DEMANDTOBY", objPurchaseReport.DemandTo_By),
                                            new SqlParameter("@DEMANDSTATUSID", objPurchaseReport.DemandStatusId),
                                            new SqlParameter("@STATUSID", objPurchaseReport.StatusId),
                                            new SqlParameter("@PAGEINDEX", objPurchaseReport.pageindex),
                                            new SqlParameter("@PAGESIZE", objPurchaseReport.pagesize)
                                          };
                outPut = objDAL.GetJson(connection, "TBLGODOWN_DEMAND_S", spparams);
                //outPut = outPut.Replace(@"\", "");
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Deactive/Approve Godown Demand
        /// </summary>
        /// <param name="objDemandModel">DEMANDID,CREATEDBY,IPADDRESS,RESULTMSG</param>
        /// <returns>ID , success Message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DemandApproveOrDelete")]
        public string DemandApproveOrDelete([FromBody] DemandModel objDemandModel)
        {

            string outPut = string.Empty;
            try
            {
                //string ProductJson = JsonConvert.SerializeObject(objDemandModel.product_json);
                SqlParameter[] spparams = { new SqlParameter("@DEMANDID",objDemandModel.DemandId),
                                            new SqlParameter("@MODE",objDemandModel.Mode),
                                            new SqlParameter("@CREATEDBY",objDemandModel.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objDemandModel.IpAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLGODOWN_DEMAND_DELAPP", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Demand Detail by Id
        /// </summary>
        /// <param name="objDemandModel">DEMANDID</param>
        /// <returns>Demand Data for View</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetDemandDetail")] //{PageIndex}/{PageSize}
        public string GetDemandDetail([FromBody] DemandModel objDemandModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DEMANDID", objDemandModel.DemandId)
                                          };
                outPut = objDAL.GetJson(connection, "TBLGODOWN_DEMAND_DETAIL_S_DID", spparams);
                //outPut = outPut.Replace(@"\", "");
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Demand Detail by Id
        /// </summary>
        /// <param name="objDemandModel">DEMANDID</param>
        /// <returns>Demand Data for View</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetDemandDetailHTML")] //{PageIndex}/{PageSize}
        public string GetDemandDetailHTML([FromBody] DemandModel objDemandModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DEMANDID", objDemandModel.DemandId)
                                          };
                outPut = objDAL.GetJson(connection, "TBLGODOWN_DEMAND_DETAIL_S_DID_HTML", spparams);
                //outPut = outPut.Replace(@"\", "");
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Update Godown Demand by Id
        /// </summary>
        /// <param name="objDemandModel">DEMANDID,GODOWNID,FROMDATE,TODATE,PRODUCT_JSON Lsit of Product with Qty,CREATEDBY,IPADDRESS</param>
        /// <returns>DEMANDID , succes Message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateDemand")]
        public string UpdateDemand([FromBody] DemandModel objDemandModel)
        {

            string outPut = string.Empty;
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objDemandModel.Product_Json);
                SqlParameter[] spparams = { new SqlParameter("@DEMANDID",objDemandModel.DemandId),
                                            new SqlParameter("@PRODUCT_JSON",ProductJson),
                                            new SqlParameter("@REMARKS",objDemandModel.Remarks),
                                            new SqlParameter("@CREATEDBY",objDemandModel.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objDemandModel.IpAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLGODOWN_DEMAND_U", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }



        /// <summary>
        /// Received demand form id 
        /// </summary>
        /// <param name="objDemandModel"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetReceiveDemandById")]
        public string GetReceiveDemandById([FromBody] DemandModel objDemandModel)
        {

            string outPut = string.Empty;
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objDemandModel.Product_Json);
                SqlParameter[] spparams = {
                                            new SqlParameter("@FROMDATE", objDemandModel.FromDate),
                                            new SqlParameter("@TODATE", objDemandModel.ToDate),
                                            new SqlParameter("@DEMANDTOID",objDemandModel.DemandTo_Id),
                                            new SqlParameter("@DEMANDTOBY",objDemandModel.DemandTo_By),
                                            new SqlParameter("@DEMANDSTATUSID",objDemandModel.DemandStatusID),
                                            new SqlParameter("@PAGEINDEX",objDemandModel.pageindex),
                                            new SqlParameter("@PAGESIZE",objDemandModel.pagesize)};
                outPut = objDAL.GetJson(connection, "TBLGODOWN_DEMAND_S_BYTOID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }



        /// <summary>
        /// Get Store Demand Indent Wise List
        /// </summary>
        /// <param name="objPurchaseReport">objPurchaseReport</param>
        /// <returns>List of Demand </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetStoreDemandIndentWise")]
        public  string GetStoreDemandIndentWise([FromBody] PurchaseReport objPurchaseReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DEMANDFROMIDSTR", objPurchaseReport.DemandFrom_Id_STR),
                                            new SqlParameter("@DEMANDFROMBY", objPurchaseReport.DemandFrom_By),
                                            new SqlParameter("@DEMANDTOID", objPurchaseReport.DemandTo_Id),
                                            new SqlParameter("@DEMANDTOBY", objPurchaseReport.DemandTo_By),
                                            new SqlParameter("@DEMANDSTATUSID", objPurchaseReport.DemandStatusId),
                                            new SqlParameter("@INDENTNO", objPurchaseReport.IndentNo),
                                            new SqlParameter("@STATUSID", objPurchaseReport.StatusId)
                                          };
                outPut = objDAL.GetJson(connection, "TBLGODOWN_DEMAND_STOCK_INDENT_S", spparams);
                //outPut = outPut.Replace(@"\", "");
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get NAC Product list for Indent by UserId
        /// </summary>
        /// <param name="objDemandModel">USERID</param>
        /// <returns>Get NAC Product list for View</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetNACProductDetail")] //{PageIndex}/{PageSize}
        public string GetNACProductDetail([FromBody] DemandModel objDemandModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {new SqlParameter("@CREATEDBY", objDemandModel.CreatedBy)};
                outPut = objDAL.GetJson(connection, "TBLGODOWN_DEMAND_S_NAC", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
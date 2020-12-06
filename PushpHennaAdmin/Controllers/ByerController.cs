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
    [Route("api/Byer")]
    public class ByerController : Controller
    {
        DAL objDAL = DAL.GetObject(); // new DAL();
        private IConfiguration _config;
        string connection = string.Empty;

        public ByerController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }


        /// <summary>
        /// Save Buyers Detail
        /// </summary>
        /// <param name="objbuyersModel">NAME,MOBILE,ALTERNATE_MOBILE , EMAIL_ID,BUYER_TYPE_ID , WALLET_LIMIT, DISTRICT_ID, CITYID, ADDRESS,MODE ,CREATEDBY,IPADDRESS</param>
        /// <returns>Buyer_id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("Savebuyers")]
        public string Savebuyers([FromBody] Byer objbuyersModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@NAME",objbuyersModel.NAME),
                                            new SqlParameter("@MOBILE", objbuyersModel.MOBILE) ,
                                            new SqlParameter("@DEPENDANT_NAME",objbuyersModel.DEPENDANT_NAME),
                                            new SqlParameter("@DEPENDANT_RELATION",objbuyersModel.DEPENDANT_RELATION),
                                            new SqlParameter("@DEPENDANT_NAME_TWO",objbuyersModel.DEPENDANT_NAME_TWO),
                                            new SqlParameter("@DEPENDANT_RELATION_TWO",objbuyersModel.DEPENDANT_RELATION_TWO),
                                            new SqlParameter("@BUYER_DOB",objbuyersModel.BUYER_DOB),
                                            new SqlParameter("@PPO_NO",objbuyersModel.PPO_NO),
                                            new SqlParameter("@BOOK_NO",objbuyersModel.BOOK_NO),
                                            new SqlParameter("@VALID_UPTO",objbuyersModel.VALID_UPTO),
                                            new SqlParameter("@REF_NO",objbuyersModel.REF_NO ),
                                            new SqlParameter("@DISPENSARY",objbuyersModel.DISPENSARY),
                                            new SqlParameter("@WALLET_LIMIT",objbuyersModel.WALLET_LIMIT),
                                            new SqlParameter("@PREV_LIMITBALANCE",objbuyersModel.PREV_LIMITBALANCE),
                                            new SqlParameter("@BUYER_TYPE",objbuyersModel.BUYER_TYPE ),
                                            new SqlParameter("@ADDRESS",objbuyersModel.ADDRESS),
                                            new SqlParameter("@RETIRED_POST",objbuyersModel.RETIRED_POST ),
                                            new SqlParameter("@DEPARTMENT",objbuyersModel.DEPARTMENT),
                                            new SqlParameter("@MODE",objbuyersModel.MODE),
                                            new SqlParameter("@CREATEDBY",objbuyersModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objbuyersModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTBUYERS_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// select Buyer by id
        /// </summary>
        /// <param name="objMasterCategory">Buyer id , mode</param>
        /// <returns>Buyer detail</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetBuyerDetail")]
        public string GetBuyerDetail([FromBody] Byer objbuyersModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@BUYER_ID",objbuyersModel.BUYER_ID),
                                            new SqlParameter("@MODE",objbuyersModel.MODE),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTBUYERS_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update UpdateBuyers by Id
        /// </summary>
        /// <param name="objDimensionModel">DIMENSIONCLASSID,NAME,MODE,REMARKS,CREATEDBY,IPADDRESS,RESULTMSG</param>
        /// <returns>DimensionId with Success Message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateBuyers")]
        public string UpdateBuyers([FromBody] Byer objbuyersModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@BUYER_ID",objbuyersModel.BUYER_ID),
                                            new SqlParameter("@NAME",objbuyersModel.NAME),
                                            new SqlParameter("@MOBILE", objbuyersModel.MOBILE) ,
                                            new SqlParameter("@DEPENDANT_NAME",objbuyersModel.DEPENDANT_NAME),
                                            new SqlParameter("@DEPENDANT_RELATION",objbuyersModel.DEPENDANT_RELATION),
                                            new SqlParameter("@DEPENDANT_NAME_TWO",objbuyersModel.DEPENDANT_NAME_TWO),
                                            new SqlParameter("@DEPENDANT_RELATION_TWO",objbuyersModel.DEPENDANT_RELATION_TWO),
                                            new SqlParameter("@BUYER_DOB",objbuyersModel.BUYER_DOB),
                                            new SqlParameter("@PPO_NO",objbuyersModel.PPO_NO),
                                            new SqlParameter("@BOOK_NO",objbuyersModel.BOOK_NO),
                                            new SqlParameter("@VALID_UPTO",objbuyersModel.VALID_UPTO),
                                            new SqlParameter("@REF_NO",objbuyersModel.REF_NO ),
                                            new SqlParameter("@DISPENSARY",objbuyersModel.DISPENSARY),
                                            new SqlParameter("@WALLET_LIMIT",objbuyersModel.WALLET_LIMIT),
                                            new SqlParameter("@PREV_LIMITBALANCE",objbuyersModel.PREV_LIMITBALANCE),
                                            new SqlParameter("@BUYER_TYPE",objbuyersModel.BUYER_TYPE ),
                                            new SqlParameter("@ADDRESS",objbuyersModel.ADDRESS),
                                            new SqlParameter("@RETIRED_POST",objbuyersModel.RETIRED_POST ),
                                            new SqlParameter("@DEPARTMENT",objbuyersModel.DEPARTMENT),
                                            new SqlParameter("@MODE",objbuyersModel.MODE),
                                            //new SqlParameter("@REMARKS",objbuyersModel.REMARK),
                                            new SqlParameter("@CREATEDBY", objbuyersModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS", objbuyersModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTBUYERS_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Get Buyers List
        /// </summary>
        /// <param name="objCategoryListModel">Pagesize,pageindex</param>
        /// <returns>list of buyers</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("BuyersList")]
        public string BuyersList([FromBody]  GridListFilterModel objCategoryListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objCategoryListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objCategoryListModel.PAGESIZE),
                                            new SqlParameter("@NAME",objCategoryListModel.NAME),
                                          };
                outPut = objDAL.GetJson(connection, "MSTBUYERS_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Add Balance in buyer wallet 
        /// </summary>
        /// <param name="objbuyersModel">BUYER_ID,BALANCE,PAYMENT_MODE,PAYMENT_REF,REMARKS,CREATEDBY,IPADDRESS</param>
        /// <returns>Success Message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateBuyersBalance")]
        public string UpdateBuyersBalance([FromBody] Byer objbuyersModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@BUYER_ID",objbuyersModel.BUYER_ID),
                                            //new SqlParameter("@BALANCE",objbuyersModel.BALANCE),
                                            //new SqlParameter("@PAYMENT_MODE",objbuyersModel.PAYMENT_MODE),
                                            //new SqlParameter("@PAYMENT_REF",objbuyersModel.PAYMENT_REF),
                                            new SqlParameter("@REMARKS",objbuyersModel.REMARK),
                                            new SqlParameter("@CREATEDBY", objbuyersModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS", objbuyersModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTBUYERS_ADDBALANCE", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// deactive News
        /// </summary>
        /// <param name="objMasterCategory">News_ID, mode</param>
        /// <returns>id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DeactiveBuyers")]
        public string DeactiveBuyers([FromBody] Byer objbuyersModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@BUYER_ID",objbuyersModel.BUYER_ID),
                                            new SqlParameter("@MODE",objbuyersModel.MODE),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTBUYERS_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Buyer Top 5 Transaction list of wallet recharge
        /// </summary>
        /// <param name="objbuyersModel">BUYER_ID</param>
        /// <returns>date,mode,amount</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetBuyerTransactionList")]
        public string GetBuyerTransactionList([FromBody]  Byer objbuyersModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@BUYER_ID", objbuyersModel.BUYER_ID),
                                            new SqlParameter("@MODE", objbuyersModel.MODE)};
                outPut = objDAL.GetJson(connection, "MSTBUYERSTRANS_S_TOP5", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update Buyer Previews Balance from booklet
        /// </summary>
        /// <param name="objbuyersModel"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateBuyerPrevBalance")]
        public string UpdateBuyerPrevBalance([FromBody]  Byer objbuyersModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@BUYER_ID", objbuyersModel.BUYER_ID) ,
                                            new SqlParameter("@PREV_LIMITBALANCE", objbuyersModel.PREV_LIMITBALANCE) ,
                                            new SqlParameter("@WALLET_LIMIT", objbuyersModel.WALLET_LIMIT) ,
                                            new SqlParameter("@CREATEDBY",objbuyersModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objbuyersModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.GetJson(connection, "MSTBUYERS_U_PREVLIMITBALANCE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
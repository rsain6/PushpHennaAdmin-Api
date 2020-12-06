using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PushpHennaAdmin.Controllers
{
    [Route("api/CounterSale")]
    public class CounterSaleController : Controller
    {
        DAL objDAL = DAL.GetObject(); //new DAL();
        private IConfiguration _config;
        string connection = string.Empty;

        public CounterSaleController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Get Product List on Counter Sale with Qty 
        /// </summary>
        /// <param name="objProductList">PROD_NAME,STOREID serach</param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetProductListForCounterSale")] //{PageIndex}/{PageSize}
        public string GetProductListForCounterSale([FromBody] ProductList objProductList)
        {
            string outPut = string.Empty;
            try
            {

                SqlParameter[] spparams = {
                                            new SqlParameter("@SALEFROM_ID", objProductList.SaleFrom_ID),
                                            new SqlParameter("@SALEFROM_BY", objProductList.SaleFrom_BY),
                                            new SqlParameter("@ISWALKIN", objProductList.ISWALKIN),
                                            new SqlParameter("@PROD_NAME", objProductList.Prod_Name)
                                          };
                outPut = objDAL.GetJson(connection, "TBLSTOCKSALE_S_BYSTOREIDFORCART", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Save Counter sale data 
        /// </summary>
        /// <param name="objCounterSaleModel">Sale Product detail </param>
        /// <returns>Return Invoice number</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveCounterSaleData")]
        public string SaveCounterSaleData([FromBody] CounterSaleModel objCounterSaleModel)
        {

            string outPut = string.Empty;
            try
            {
                string ProductJson = string.Empty;
                string NAC_ProductDetail= string.Empty; 

                if (objCounterSaleModel.PRODUCT_DETAILJSON!=null)
                  ProductJson = JsonConvert.SerializeObject(objCounterSaleModel.PRODUCT_DETAILJSON);
                if(objCounterSaleModel.NAC_PRODUCTDETAIL_JSON!=null)
                    NAC_ProductDetail = JsonConvert.SerializeObject(objCounterSaleModel.NAC_PRODUCTDETAIL_JSON);

                //ProductJson = JsonConvert.SerializeObject(objCounterSaleModel.PRODUCT_DETAILJSON);
                SqlParameter[] spparams = { new SqlParameter("@SALEFROM_ID", objCounterSaleModel.SALEFROM_ID) ,
                                            new SqlParameter("@SALEFROM_BY", objCounterSaleModel.SALEFROM_BY) ,
                                            new SqlParameter("@PRODUCT_DETAILJSON", ProductJson),
                                            new SqlParameter("@NAC_PRODUCTDETAIL_JSON", NAC_ProductDetail) ,
                                            new SqlParameter("@PAYMENTMODE_ID", objCounterSaleModel.PAYMENTMODE_ID) ,
                                            new SqlParameter("@OTP", objCounterSaleModel.OTP) ,
                                            new SqlParameter("@CUST_NAME", objCounterSaleModel.CUST_NAME) ,
                                            //new SqlParameter("@DISPENSARYID", objCounterSaleModel.DISPENSARYID) ,
                                            new SqlParameter("@DOCTORNAME", objCounterSaleModel.DOCTORNAME) ,
                                            new SqlParameter("@BUYER_ID", objCounterSaleModel.BUYER_ID) ,
                                            new SqlParameter("@CUST_MOBILE", objCounterSaleModel.CUST_MOBILE) ,
                                            new SqlParameter("@DISPENSARYNAME", objCounterSaleModel.DISPENSARYNAME) ,
                                            new SqlParameter("@OPTTKT", objCounterSaleModel.OPTTKT) ,
                                            new SqlParameter("@EMPLOYEENAME", objCounterSaleModel.EMPLOYEENAME) ,
                                            new SqlParameter("@DEPTNAME", objCounterSaleModel.DEPTNAME) ,
                                            new SqlParameter("@CREATEDBY",objCounterSaleModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objCounterSaleModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLCOUNTER_SALE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
          
    }


        /// <summary>
        /// Generate OTP for verify user Transaction
        /// </summary>
        /// <param name="objSellModel">buyer_id,MODE='BUYERS' ,CREATEDBY ,IPADDRESS , RESULTMSG </param>
        /// <returns>Return OTP as Result</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GenerateOTP")]
        public string GenerateOTP([FromBody] SellModel objSellModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@ID",objSellModel.buyer_id),
                                            new SqlParameter("@MODE",objSellModel.mode),
                                            new SqlParameter("@CREATEDBY", objSellModel.createdby),
                                            new SqlParameter("@IPADDRESS", objSellModel.ipaddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLOTP_I", spparams);


                List<Response> obj = new List<Response>();
                obj = JsonConvert.DeserializeObject<List<Response>>(outPut);
                if (obj[0].MSG == "SUCCESS")
                {
                    //Send OTP to mobile
                    return "[]";   //{'Result':'SUCCESS'}
                }
                else
                {
                    return "['Result':'Invalid Request']";  //{'Result':'Invalid Request'}
                }

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetBuyerDetailByMobileNo")]
        public string GetBuyerDetailByMobileNo([FromBody] SellModel objSellModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MOBILE", objSellModel.mobile) };
                outPut = objDAL.GetJson(connection, "MSTBUYERS_S_BYMOBILE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Verify buyer by OTP
        /// </summary>
        /// <param name="objSellModel">buyer_id,MODE='BUYERS' ,OTP,CREATEDBY ,IPADDRESS , RESULTMSG </param>
        /// <returns>Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("OTPVerify")]
        public string OTPVerify([FromBody] SellModel objSellModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@ID",objSellModel.buyer_id),
                                            new SqlParameter("@MODE",objSellModel.mode),
                                            new SqlParameter("@OTP",objSellModel.otp),
                                            new SqlParameter("@CREATEDBY", objSellModel.createdby),
                                            new SqlParameter("@IPADDRESS", objSellModel.ipaddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLOTP_VERIFY", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Get Buyer List by PPO number
        /// </summary>
        /// <param name="objProductList">PPO Search</param>
        /// <returns>Buyer List</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetBuyerListByPPONumber")] //{PageIndex}/{PageSize}
        public string GetBuyerListByPPONumber([FromBody] ProductList objProductList)
        {
            string outPut = string.Empty;
            try
            {

                SqlParameter[] spparams = {
                                           new SqlParameter("@TEXT", objProductList.Text),
                                           new SqlParameter("@BUYER_TYPE", objProductList.BUYER_TYPE)
                                        };
                outPut = objDAL.GetJson(connection, "MSTBUYERS_S_BYPPONO", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Buyer List by Name
        /// </summary>
        /// <param name="objProductList">Name</param>
        /// <returns>Buyer list</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetBuyerListByName")] //{PageIndex}/{PageSize}
        public string GetBuyerListByName([FromBody] ProductList objProductList)
        {
            string outPut = string.Empty;
            try
            {

                SqlParameter[] spparams = { new SqlParameter("@NAME", objProductList.Name) };
                outPut = objDAL.GetJson(connection, "MSTBUYERS_S_BYNAME", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Save counter bill data in html format
        /// </summary>
        /// <param name="objInvoiceFile"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveCounterInvoiceFile")] //{PageIndex}/{PageSize}
        public string SaveCounterInvoiceFile([FromBody] InvoiceFile objInvoiceFile)
        {
            string outPut = string.Empty;
            try
            {

                SqlParameter[] spparams = {  new SqlParameter("@ID", objInvoiceFile.Id),
                                             new SqlParameter("@INVOICE_FOR", objInvoiceFile.INVOICE_FOR),
                                             new SqlParameter("@HTML", objInvoiceFile.HTML),
                                             new SqlParameter("@CREATEDBY", objInvoiceFile.CREATEDBY),
                                             new SqlParameter("@IPADDRESS", objInvoiceFile.IPADDRESS),
                                             new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLINVOICE_I", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Save Pensioners diary or credit extended limit
        /// </summary>
        /// <param name="objInvoiceFile"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SavePensionersExtendedInfo")]
        public string SavePensionersExtendedInfo([FromBody] PensionersExtensionInfo objPensionersExtInfo)
        {
            string outPut = string.Empty;
            try
            {

                SqlParameter[] spparams = {  new SqlParameter("@BUYER_ID", objPensionersExtInfo.BuyerID),
                                             new SqlParameter("@EXTENDLIMIT", objPensionersExtInfo.ExtendLimit),
                                             new SqlParameter("@REF_NO", objPensionersExtInfo.ReferenceNumber),
                                             new SqlParameter("@BOOK_NO", objPensionersExtInfo.BookNumber),
                                             new SqlParameter("@MODE", objPensionersExtInfo.Mode),
                                             new SqlParameter("@CREATEDBY", objPensionersExtInfo.CREATEDBY),
                                             new SqlParameter("@IPADDRESS", objPensionersExtInfo.IPADDRESS),
                                             new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTBUYERS_U_LIMIT", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Counter Sale Invoice
        /// </summary>
        /// <param name="objInvoiceFile"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetCounterSaleInvoice")] //{PageIndex}/{PageSize}
        public string GetCounterSaleInvoice([FromBody] InvoiceFile objInvoiceFile)
        {
            string outPut = string.Empty;
            try
            {

                SqlParameter[] spparams = {  new SqlParameter("@ID", objInvoiceFile.Id),
                                             new SqlParameter("@INVOICE_FOR", objInvoiceFile.INVOICE_FOR)};
                outPut = objDAL.GetJson(connection, "TBLINVOICE_S", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Get Counter Bill Detail By Id 
        /// </summary>
        /// <param name="objInvoiceFile"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetCounterBillbyId")] //{PageIndex}/{PageSize}
        public string GetCounterBillbyId([FromBody] InvoiceFile objInvoiceFile)
        {
            string outPut = string.Empty;
            try
            {

                SqlParameter[] spparams = {  new SqlParameter("@TBLCOUNTERSALE_ID", objInvoiceFile.TBLCOUNTERSALE_ID)};
                outPut = objDAL.GetJson(connection, "TBLCOUNTERSALE_S_BYID", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Edit Counter sale by Id 
        /// </summary>
        /// <param name="objCounterSaleModel"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateCounterSaleById")] //{PageIndex}/{PageSize}
        public string UpdateCounterSaleById([FromBody] CounterSaleModel objCounterSaleModel)
        {
            string outPut = string.Empty;
            try
            {

                string ProductJson = string.Empty;
                string NAC_ProductDetail = string.Empty;

                if (objCounterSaleModel.PRODUCT_DETAILJSON != null)
                    ProductJson = JsonConvert.SerializeObject(objCounterSaleModel.PRODUCT_DETAILJSON);
                if (objCounterSaleModel.NAC_PRODUCTDETAIL_JSON != null)
                    NAC_ProductDetail = JsonConvert.SerializeObject(objCounterSaleModel.NAC_PRODUCTDETAIL_JSON);

                SqlParameter[] spparams = { new SqlParameter("@TBLCOUNTERSALE_ID", objCounterSaleModel.TBLCOUNTERSALE_ID) ,
                                            new SqlParameter("@SALEFROM_ID", objCounterSaleModel.SALEFROM_ID) ,
                                            new SqlParameter("@SALEFROM_BY", objCounterSaleModel.SALEFROM_BY) ,
                                            new SqlParameter("@PRODUCT_DETAILJSON", ProductJson),
                                            new SqlParameter("@NAC_PRODUCTDETAIL_JSON", NAC_ProductDetail) ,
                                            new SqlParameter("@EMPLOYEENAME", objCounterSaleModel.EMPLOYEENAME) ,
                                            new SqlParameter("@DEPTNAME", objCounterSaleModel.DEPTNAME) ,
                                            new SqlParameter("@CUST_NAME", objCounterSaleModel.CUST_NAME) ,
                                            new SqlParameter("@DOCTORNAME", objCounterSaleModel.DOCTORNAME) ,
                                            new SqlParameter("@CUST_MOBILE", objCounterSaleModel.CUST_MOBILE) ,
                                            new SqlParameter("@DISPENSARYNAME", objCounterSaleModel.DISPENSARYNAME) ,
                                            new SqlParameter("@OPTTKT", objCounterSaleModel.OPTTKT),
                                            new SqlParameter("@REMARK", objCounterSaleModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objCounterSaleModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objCounterSaleModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLCOUNTER_SALE_EDIT", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Doctor list for filter
        /// </summary>
        /// <param name="objCounterSaleModel">DOCTORNAME</param>
        /// <returns>DOCTORNAME</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetDoctorList")]
        public string GetDoctorList()
        {

            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "TBLCOUNTERSALE_S_DOCTOR");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;

        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;
using System;
using System.Data.SqlClient;

namespace PushpHennaAdmin.Controllers
{
    [Route("api/POPayment")]
    public class POPaymentController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;

        public POPaymentController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Get PO Payment Vendor  List 
        /// </summary>
        /// <param name="objPOSerarchModel">DEMANDFROMID,DEMANDFROMBY,POSTATUSID</param>
        /// <returns>Vendor ID ,Name</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPOPendingVendorList")]
        public string GetPOPendingVendorList([FromBody] PaymentModel objPaymentModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DEMANDFROMID",objPaymentModel.DemandFrom_Id),
                                            new SqlParameter("@DEMANDFROMBY",objPaymentModel.DemandFrom_By),
                                            new SqlParameter("@POSTATUSID",objPaymentModel.PoStatusID)
                                          };
                outPut = objDAL.GetJson(connection, "TBLPO_S_LISTVENDOR", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get PO List vendor wise for Payment
        /// </summary>
        /// <param name="objPaymentModel">DEMANDFROMID,DEMANDFROMBY,PO_NUMBER,VENDOR_ID</param>
        /// <returns>Get PO LIST with Total Payable Amount</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPOListForPayment")]
        public string GetPOListForPayment([FromBody] PaymentModel objPaymentModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DEMANDFROMID",objPaymentModel.DemandFrom_Id),
                                            new SqlParameter("@DEMANDFROMBY",objPaymentModel.DemandFrom_By),
                                            new SqlParameter("@PO_NUMBER",objPaymentModel.PO_Number),
                                            new SqlParameter("@VENDOR_ID",objPaymentModel.Vendor_ID),
                                          };
                outPut = objDAL.GetJson(connection, "TBLPO_STOCK_S_FORVOUCHER", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Create Voucher Respect of PO
        /// </summary>
        /// <param name="objVoucherModel">POID_STR, AMOUNT, DISCOUNT , TAX_AMT,OTHER_CHARGE ,NETAMOUNT ,PAYMENTMODEID , CHEQUE_NO, CHEQUE_DATE,REMARK , CREATEDBY,IPADDRESS , RESULTMSG</param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveVoucher")]
        public string SaveVoucher([FromBody] VoucherModel objVoucherModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@POID_STR",objVoucherModel.poid_str),
                                            new SqlParameter("@AMOUNT",objVoucherModel.amount),
                                            new SqlParameter("@DISCOUNT",objVoucherModel.discount==null?0 : objVoucherModel.discount),
                                            new SqlParameter("@TAX_AMT",objVoucherModel.tax_amt==null?0 :objVoucherModel.tax_amt),
                                            new SqlParameter("@OTHER_CHARGE",objVoucherModel.other_charg==null?0 :objVoucherModel.other_charg ),
                                            new SqlParameter("@NETAMOUNT",objVoucherModel.netamount),
                                            new SqlParameter("@PAYMENTMODEID",objVoucherModel.paymentmodeid),
                                            new SqlParameter("@CHEQUE_NO",objVoucherModel.cheque_no),
                                            new SqlParameter("@CHEQUE_DATE",objVoucherModel.cheque_date),
                                            new SqlParameter("@REMARK",objVoucherModel.remark),
                                            new SqlParameter("@CREATEDBY",objVoucherModel.createdby),
                                            new SqlParameter("@IPADDRESS",objVoucherModel.ipaddress),
                                            new SqlParameter("@RESULTMSG","")
                                          };
                outPut = objDAL.PostWithResultCode(connection, "TBLPO_VOUCHER_I", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get PO - Product Receving Detail
        /// </summary>
        /// <param name="objPaymentModel">PURCHASEORDER_ID</param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPODetail")]
        public string GetPODetail([FromBody] PaymentModel objPaymentModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PURCHASEORDER_ID",objPaymentModel.PurchaseOrder_ID),                                         
                                          };
                outPut = objDAL.GetJson(connection, "TBLPO_STOCK_S_BYPOID", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Voucher List 
        /// </summary>
        /// <param name="objPOSerarchModel">VENDERID,PAGEINDEX,PAGESIZE</param>
        /// <returns>voucher list</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetAllVoucherList")]
        public string GetAllVoucherList([FromBody] PaymentModel objPaymentModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DEMANDFROMID",objPaymentModel.DemandFrom_Id),
                                            new SqlParameter("@DEMANDFROMBY",objPaymentModel.DemandFrom_By),
                                            new SqlParameter("@VENDORID",objPaymentModel.Vendor_ID) ,
                                            new SqlParameter("@PAGEINDEX", objPaymentModel.PageIndex),
                                            new SqlParameter("@PAGESIZE", objPaymentModel.PageSize)
                                          };
                outPut = objDAL.GetJson(connection, "TBLPO_VOUCHER_S", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Get Voucher Detail by voucher ID
        /// </summary>
        /// <param name="VoucherId">Voucher Id</param>
        /// <returns>Voucher Detail With PO list </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetVoucherDetailById")]
        public string GetVoucherDetailById([FromBody] PaymentModel objPaymentModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PO_VOUCHERID ", objPaymentModel.VoucherId) };
                outPut = objDAL.GetJson(connection, "TBLPO_VOUCHER_S_DETAIL", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


    }
}
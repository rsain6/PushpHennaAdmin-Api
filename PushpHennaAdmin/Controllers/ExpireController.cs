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
    [Route("api/Expire")]
    public class ExpireController : Controller
    {
        DAL objDAL = DAL.GetObject(); //new DAL();
        private IConfiguration _config;
        string connection = string.Empty;
        public ExpireController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }


        #region Godown Dropdown List For Expire
        /// <summary>
        /// Get Godown drop down list
        /// </summary>
        /// <returns>Godown Id and Name</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetGodownListForExpire")]
        public string GetGodownListForExpire([FromBody] FilterDropDown objFilterDropDown)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@GODOWNIDSTR", objFilterDropDown.FilterStr) };
                outPut = objDAL.GetJson(connection, "MSTGODOWN_S_LIST_FOREXPIRY", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        #endregion

        /// <summary>
        /// Get ExpireProductVendorList Vendor ID and Name (master category wise)
        /// </summary>
        /// <param name="ID">VENDORID</param>
        /// <returns>List of ExpireProductVendor(ID ,Name)</returns>     
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetExpireProductVendorList/{Id}")]
        public string GetExpireProductVendorList(string Id)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@CREATEDBY", Id) };
                outPut = objDAL.GetJson(connection, "MSTVENDOR_S_VENDOR_EXPIRY", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Expire product List
        /// </summary>
        /// <param name="objPaymentModel">fromDate,Todate,godown,group</param>
        /// <returns>Get Expire product List with name, Exp. date, sale rate,companyname, genric name, qty</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetExpireProductList")]
        public string GetExpireProductList([FromBody] ExpireSearchModel objPaymentModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@TRANSFER_TO",objPaymentModel.Transfer_To),
                                            new SqlParameter("@TRANSFER_TOID",objPaymentModel.Transfer_ToId),
                                            new SqlParameter("@BATCH",objPaymentModel.Batch),
                                            new SqlParameter("@DT1", objPaymentModel.FromDate),
                                            new SqlParameter("@DT2", objPaymentModel.ToDate),
                                            new SqlParameter("@CREATEDBY",objPaymentModel.CreatedBy)
                                          };
                outPut = objDAL.GetJson(connection, "TBLSTOCK_DETAIL_S_PRODUCTEXPIRY", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Submit Expire Product
        /// </summary>
        /// <param name="objVoucherModel">POID_STR, AMOUNT, DISCOUNT , TAX_AMT,OTHER_CHARGE ,NETAMOUNT ,PAYMENTMODEID , CHEQUE_NO, CHEQUE_DATE,REMARK , CREATEDBY,IPADDRESS , RESULTMSG</param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveExpireProduct")]
        public string SaveExpireProduct([FromBody] ExpireModel objExpireModel)
        {
            string outPut = string.Empty;
            try
            {
                string expirestockprodtllst = JsonConvert.SerializeObject(objExpireModel.StockDetail_Ids_Str);
                SqlParameter[] spparams = {
                                            new SqlParameter("@STOCKDETAIL_JSON",expirestockprodtllst),
                                            new SqlParameter("@RETURN_TO",objExpireModel.Return_To),
                                            new SqlParameter("@RETURN_TOID",objExpireModel.Return_ToId),
                                            new SqlParameter("@REMARKS",objExpireModel.Remarks),
                                            new SqlParameter("@CREATEDBY",objExpireModel.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objExpireModel.IpAddress),
                                            new SqlParameter("@RESULTMSG","")
                                          };
                outPut = objDAL.PostWithResultCode(connection, "TBLEXPIRY_I", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Expire return List
        /// </summary>
        /// <param name="objPaymentModel">Userid</param>
        /// <returns>Get Expire return List with challanno, date</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetExpireReturnList")]
        public string GetExpireReturnList([FromBody] ExpireListModel objExpireListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objExpireListModel.PageIndex),
                                            new SqlParameter("@PAGESIZE",objExpireListModel.PageSize),
                                            new SqlParameter("@CREATEDBY",objExpireListModel.CreatedBy)
                                          };
                outPut = objDAL.GetJson(connection, "TBLEXPIRY_S", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Expire return Print Detail
        /// </summary>
        /// <param name="objPaymentModel">Userid</param>
        /// <returns>Get Expire return List with challanno, date</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetExpireReturnPrintDetail")]
        public string GetExpireReturnPrintDetail([FromBody] ExpireListModel objExpireListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@CHALLAN_NO", objExpireListModel.Challan_No) };
                outPut = objDAL.GetJson(connection, "TBLEXPIRY_S_BYCHALLAN_NO", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Expire product Accept List
        /// </summary>
        /// <param name="objPaymentModel">userid, pageindex, pagesize</param>
        /// <returns>Get Expire product List with name, Exp. date, sale rate,companyname, genric name, qty</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetExpireProductAcceptList")]
        public string GetExpireProductAcceptList([FromBody] ExpireListModel objExpireListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@STATUS", objExpireListModel.Status),
                                            new SqlParameter("@CHALLAN_NO", objExpireListModel.Challan_No),
                                            new SqlParameter("@PAGEINDEX",objExpireListModel.PageIndex),
                                            new SqlParameter("@PAGESIZE",objExpireListModel.PageSize),
                                            new SqlParameter("@CREATEDBY",objExpireListModel.CreatedBy)
                                          };
                outPut = objDAL.GetJson(connection, "TBLEXPIRY_S_FORACCEPT", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Expire Product reject By ExpireId
        /// </summary>
        /// <param name="objGodownStatus">ExpireId</param>
        /// <returns>success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("RejectExpiredProductById")]
        public string RejectExpiredProductById([FromBody] ExpireModel objExpireModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@EXPIRYID", objExpireModel.ExpiryId),
                                            new SqlParameter("@REMARKS",objExpireModel.Remarks),
                                            new SqlParameter("@CREATEDBY", objExpireModel.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objExpireModel.IpAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLEXPIRY_REJECT_BYEXID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
                //log.Error(outPut);
            }
            return outPut;
        }

        /// <summary>
        /// Submit Expire Product
        /// </summary>
        /// <param name="objVoucherModel">POID_STR, AMOUNT, DISCOUNT , TAX_AMT,OTHER_CHARGE ,NETAMOUNT ,PAYMENTMODEID , CHEQUE_NO, CHEQUE_DATE,REMARK , CREATEDBY,IPADDRESS , RESULTMSG</param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveAcceptExpireProduct")]
        public string SaveAcceptExpireProduct([FromBody] ExpireModel objExpireModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@EXPIRYIDSTR",objExpireModel.ExpiryIdStr),
                                            new SqlParameter("@CREATEDBY",objExpireModel.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objExpireModel.IpAddress),
                                            new SqlParameter("@RESULTMSG","")
                                          };
                outPut = objDAL.PostWithResultCode(connection, "TBLEXPIRY_U_ACCEPTBYEXID", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Received Against Expire List
        /// </summary>
        /// <param name="objPaymentModel">Vendorid, userid, pageindex, pagesize</param>
        /// <returns>Get Expire product List with name, Exp. date, sale rate,companyname, genric name, qty</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetReceivedAgainstExpireList")]
        public string GetReceivedAgainstExpireList([FromBody] ExpireListModel objExpireListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PAGEINDEX",objExpireListModel.PageIndex),
                                            new SqlParameter("@PAGESIZE",objExpireListModel.PageSize),
                                            new SqlParameter("@VENDORID",objExpireListModel.VendorId),
                                            new SqlParameter("@CREATEDBY",objExpireListModel.CreatedBy)
                                          };
                outPut = objDAL.GetJson(connection, "TBLEXPIRY_S_RECEIVE_CHALLANWISE", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Receive Expire Stock in Inventory
        /// </summary>
        /// <param name="file">Invoice File</param>
        /// <param name="Model">PO Detail</param>
        /// <returns>Success Msg</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveReceiveExpireStockIn")]
        public string SaveReceiveExpireStockIn(IFormFile file, string Model)
        {
            string outPut = string.Empty;
            string FileName = string.Empty;
            Guid obj;
            ReturnStockIn objStockIn = new ReturnStockIn();
            if (Model != null)
                objStockIn = JsonConvert.DeserializeObject<ReturnStockIn>(Model);
            else
                return outPut;

            if (file != null)
            {
                obj = Guid.NewGuid();
                FileName = obj.ToString() + "_EXP_Invoice_" + file.FileName;
                objStockIn.InvoiceFile = FileName;
            }
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objStockIn.Product_DetailJSON);
                string FreeProductJson = JsonConvert.SerializeObject(objStockIn.freeProduct_DetailJSON);
                SqlParameter[] spparams = {
                                            new SqlParameter("@EXP_CHALLANNO", objStockIn.Exp_ChallanNo),
                                            new SqlParameter("@INVOICENO", objStockIn.InvoiceNO),
                                            new SqlParameter("@INVOICEDATE", objStockIn.InvoiceDate),
                                            new SqlParameter("@INVOICEFILE", objStockIn.InvoiceFile),
                                            new SqlParameter("@CHALLANNO", objStockIn.ChallanNo),
                                            new SqlParameter("@VENDOR_REMARK1", objStockIn.Vendor_Remark1),
                                            new SqlParameter("@VENDOR_REMARK2", objStockIn.Vendor_Remark2),
                                            new SqlParameter("@VENDOR_REMARK3", objStockIn.Vendor_Remark3),
                                            new SqlParameter("@ISCLOSED", objStockIn.IsClosed),
                                            new SqlParameter("@PRODUCT_DETAILJSON", ProductJson),
                                            new SqlParameter("@FREEPRODUCT_JSON", FreeProductJson),
                                            new SqlParameter("@OTHER_ADJUSTMENT", objStockIn.Other_Adjustment),
                                            new SqlParameter("@OTHER_CHARGES", objStockIn.Other_Charges),
                                            new SqlParameter("@ICRNO", objStockIn.ICRNO),
                                            new SqlParameter("@CREATEDBY", objStockIn.CreatedBy),
                                            new SqlParameter("@IPADDRESS", objStockIn.IPAddress),
                                            new SqlParameter("@RESULTMSG", "")
                                           };
                outPut = objDAL.PostWithResultCode(connection, "TBLSTOCKIN_I_EXPIRY", spparams);
                var path = Path.Combine(Directory.GetCurrentDirectory(), _config.GetSection("AppSettings").GetSection("InvoicePath").Value, FileName);
                if (file != null)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Get product stock in Godown
        /// </summary>
        /// <param name="CreatedBy">CreatedBy</param>
        /// <param name="ProdName">ProdName</param>
        /// <returns>avail Qty</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetProductStockGodown")]
        public string GetProductStockGodown([FromBody]  FilterProductModel objFilterProductModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@CREATEDBY", objFilterProductModel.CREATEDBY),
                                            new SqlParameter("@PRODNAME", objFilterProductModel.PRODNAME) };
                outPut = objDAL.GetJson(connection, "TBLPRODUCT_STOCK_EXPIRY", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Create Store Demand 
        /// </summary>
        /// <param name="objDemandModel">GODOWNID,FROMDATE,TODATE,PRODUCT_JSON Lsit of Product with Qty,CREATEDBY,IPADDRESS</param>
        /// <returns>Id with Success Message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveTransferToGodown")]
        public string SaveTransferToGodown([FromBody] TransferToGodownModel objDemandModel)
        {
            string outPut = string.Empty;
            string JsonString = JsonConvert.SerializeObject(objDemandModel);
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objDemandModel.Product_DetailJson);
                SqlParameter[] spparams = { new SqlParameter("@PRODUCT_DETAILJSON",ProductJson),
                                            new SqlParameter("@TRANSFERTO_ID",objDemandModel.TransferTo_Id),
                                            new SqlParameter("@TRANSFERFOR", objDemandModel.TransferFor),
                                            new SqlParameter("@REMARK",objDemandModel.Remark),
                                            new SqlParameter("@CREATEDBY",objDemandModel.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objDemandModel.IpAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLSTOCKIN_TRANSFER", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        ///Get Expire Accept Return Stock List
        /// </summary>
        /// <param name="objStockModel">AcceptStatusID,StoreId</param>
        /// <returns>Prodcut list Batch wise</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetExpireAcceptReturnStockList")] //{PageIndex}/{PageSize}
        public string GetExpireAcceptReturnStockList([FromBody] AcceptStockModel objAcceptStockModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                             new SqlParameter("@STATUSID",objAcceptStockModel.STATUSID),
                                            new SqlParameter("@LISTFOR",objAcceptStockModel.LISTFOR),
                                            new SqlParameter("@CREATEDBY",objAcceptStockModel.CREATEDBY),
                                            new SqlParameter("@PAGEINDEX", objAcceptStockModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE", objAcceptStockModel.PAGESIZE)};
                outPut = objDAL.GetJson(connection, "TBLSTOCK_TRANSFER_S_FORU", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("ExpireReceiveReturnStockAccept")] //{PageIndex}/{PageSize}
        public string ExpireReceiveReturnStockAccept([FromBody]  AcceptStockReturn objAcceptStockReturn)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                           new SqlParameter("@STOCKTRANSFERDTLID", objAcceptStockReturn.STOCKTRANSFERDTLID),
                                           new SqlParameter("@MODE", objAcceptStockReturn.MODE),
                                           new SqlParameter("@REMARK", objAcceptStockReturn.REMARK),
                                           new SqlParameter("@IPADDRESS", objAcceptStockReturn.IPADDRESS),
                                           new SqlParameter("@CREATEDBY", objAcceptStockReturn.CREATEDBY),
                                           new SqlParameter("@RESULTMSG", "")
                                          };
                outPut = objDAL.PostWithResultCode(connection, "TBLSTOCK_TRANSFER_U", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Get Recevied Product List Against of Expiry Product 
        /// </summary>
        /// <param name="objAcceptStockModel"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetReceivedProdListAgainstExpire")] //{PageIndex}/{PageSize}
        public string GetReceivedProdListAgainstExpire([FromBody] AcceptStockModel objAcceptStockModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@EXP_CHALLANNO",objAcceptStockModel.EXP_CHALLANNO) };
                outPut = objDAL.GetJson(connection, "TBLSTOCKIN_INVC_S_EXPIRY", spparams);
            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

    }
}
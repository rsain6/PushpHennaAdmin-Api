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
    [Route("api/PurchaseOrder")]
    public class PurchaseOrderController : Controller
    {
        DAL objDAL = DAL.GetObject(); //new DAL();
        private IConfiguration _config;
        string connection = string.Empty;
        public PurchaseOrderController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }


        /// <summary>
        /// Get all consolidate Demand for PO
        /// </summary>
        /// <param > Godown STR and Store STR</param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetDemandForPO")] //{PageIndex}/{PageSize}
        public string GetDemandForPO([FromBody] PurchaseReport objPurchaseReport)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                           new SqlParameter("@DEMANDTOID", objPurchaseReport.DemandToId),
                                            new SqlParameter("@DEMANDTOBY", objPurchaseReport.DemandToBy),
                                            new SqlParameter("@MKTGROUPID", objPurchaseReport.MKTGroupID)
                                          };
                outPut = objDAL.GetJson(connection, "TBLGODOWN_DEMAND_S_PRODQTYFORPO", spparams);

            }

            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// User for save Purchase Request and create purchase order respact to PR
        /// </summary>
        /// <param name="objPurchaseorder">Pass- Godown,Vendor,if any Quatation,DeliveryDate,Remark,createdby, list of products with ProductId,Weightclass,quantity,Price with GST</param>
        /// <returns>Success with Purchase order</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SavePurchaseOrder")]
        public string SavePurchaseOrder([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objPurchaseorder.product_detailjson);
                SqlParameter[] spparams = { new SqlParameter("@DEMANDFROMID",objPurchaseorder.DemandFromId),
                                            new SqlParameter("@DEMANDFROMBY", objPurchaseorder.DemandFromBy),
                                            new SqlParameter("@VENDORID", objPurchaseorder.Vendor_Id),
                                            new SqlParameter("@REFNO", objPurchaseorder.RefNo),
                                            new SqlParameter("@PURCHASEORDER_REMARK",objPurchaseorder.PurchaseOrder_Remark),
                                            new SqlParameter("@PRODUCT_DETAILJSON",ProductJson),
                                            new SqlParameter("@CREATEDBY",objPurchaseorder.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objPurchaseorder.IPAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLPO_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        #region Update Edit & Rejact PO
        /// <summary>
        /// UPdate PO
        /// </summary>
        /// <param name="objPurchaseorder">Pass- Godown,Vendor,if any Quatation,DeliveryDate,Remark,createdby, list of products with ProductId,Weightclass,quantity,Price with GST</param>
        /// <returns>Success with Purchase order</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdatePurchaseOrder")]
        public string UpdatePurchaseOrder([FromBody] PurchaseOrder objPurchaseorder)
        {

            string outPut = string.Empty;
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objPurchaseorder.product_detailjson);
                SqlParameter[] spparams = { new SqlParameter("@PURCHASEORDER_ID",objPurchaseorder.PurchaseOrder_Id),
                                            new SqlParameter("@DEMANDFROMID",objPurchaseorder.DemandFromId),
                                            new SqlParameter("@DEMANDFROMBY", objPurchaseorder.DemandFromBy),
                                            new SqlParameter("@VENDORID", objPurchaseorder.Vendor_Id),
                                            new SqlParameter("@REFNO", objPurchaseorder.RefNo),
                                            new SqlParameter("@PURCHASEORDER_REMARK",objPurchaseorder.PurchaseOrder_Remark),
                                            new SqlParameter("@PRODUCT_DETAILJSON",ProductJson),
                                            new SqlParameter("@CREATEDBY",objPurchaseorder.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objPurchaseorder.IPAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLPO_U", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update PO Status Rejact or Approve
        /// </summary>
        /// <param name="objPurchaseorder"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdatePOStatus")]
        public string UpdatePOStatus([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams ={  new SqlParameter("PURCHASEORDER_ID",objPurchaseorder.PurchaseOrder_Id),
                                            new SqlParameter("MODE",objPurchaseorder.Mode),
                                            new SqlParameter("@CREATEDBY",objPurchaseorder.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objPurchaseorder.IPAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLPO_U_STATUS", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        #endregion
        /// <summary>
        /// get PO list 
        /// </summary>
        /// <param>DemandFromId ,DemandFromBy,Vendor_Id , pageindex, pagesize </param>
        /// <returns>PO Number with Id</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPurchaseOrderList")]
        public IActionResult GetPurchaseOrderList([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DEMANDFROMID", objPurchaseorder.DemandFromId),
                                            new SqlParameter("@DEMANDFROMBY", objPurchaseorder.DemandFromBy),
                                            new SqlParameter("@VENDOR_ID", objPurchaseorder.Vendor_Id),
                                            new SqlParameter("@FROMDATE", objPurchaseorder.FromDate),
                                            new SqlParameter("@TODATE", objPurchaseorder.ToDate),
                                            new SqlParameter("@POSTATUSID", objPurchaseorder.Postatusid),
                                            new SqlParameter("@PAGEINDEX", objPurchaseorder.pageindex),
                                            new SqlParameter("@PAGESIZE", objPurchaseorder.pagesize)
                                          };
                outPut = objDAL.GetJson(connection, "TBLPO_H_S", spparams);
                
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return Ok(outPut);
        }
        /// <summary>
        /// Get Purchase order by PO Id (PO Detail)
        /// </summary>
        /// <param name="Id"> uniquie PO Id</param>
        /// <returns>All save detail return as Json string</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPurchaseOrderDetailByPOId")]
        public string GetPurchaseOrderDetailByPOId([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PURCHASEORDER_ID", objPurchaseorder.PurchaseOrder_Id) };
                outPut = objDAL.GetJson(connection, "TBLPO_D_S_BY_POID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Purchase order by PO Id (PO Detail)
        /// </summary>
        /// <param name="Id"> uniquie PO Id</param>
        /// <returns>All save detail return as Json string</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPurchaseOrderDetailByPOIdHTML")]
        public string GetPurchaseOrderDetailByPOIdHTML([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PURCHASEORDER_ID", objPurchaseorder.PurchaseOrder_Id) };
                outPut = objDAL.GetJson(connection, "TBLPO_D_S_BY_POID_HTML", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Receive Stock Details by PO Id
        /// </summary>
        /// <param name="Id"> uniquie PO Id</param>
        /// <returns>All save detail return as Json string</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetReceiveStockDetailByPOId")]
        public string GetReceiveStockDetailByPOId([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PURCHASEORDER_ID", objPurchaseorder.PurchaseOrder_Id) };
                outPut = objDAL.GetJson(connection, "TBLSTOCKIN_INVC_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Complete PO
        /// </summary>
        /// <param name="objPurchaseorder">PurchaseOrder_Id,Vendor_Remark1,Vendor_Remark2 ,Vendor_Remark3 ,CreatedBy ,IPAddress </param>
        /// <returns>success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("CompletePurchaseOrder")]
        public string CompletePurchaseOrder([FromBody] StockIn objStockIn)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PURCHASEORDERID",objStockIn.PurchaseOrder_Id),
                                            new SqlParameter("@VENDOR_REMARK1",objStockIn.Vendor_Remark1),
                                            new SqlParameter("@VENDOR_REMARK2",objStockIn.Vendor_Remark2),
                                            new SqlParameter("@VENDOR_REMARK3",objStockIn.Vendor_Remark3),
                                            new SqlParameter("@CREATEDBY",objStockIn.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objStockIn.IPAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLPURCHASEORDER_COMPLETE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        #region Stock Recevie  Inventory
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetPOList")]
        public string GetPOList([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PO_ID", objPurchaseorder.PurchaseOrder_Id),
                                            new SqlParameter("@CREATEDBY", objPurchaseorder.CreatedBy)
                                           };
                outPut = objDAL.GetJson(connection, "TBLPO_DTL_S_PENDINGBYPOID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        #region get all po list
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetALLPOList")]
        public string GetALLPOList([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@FROMDATE", objPurchaseorder.FromDate),
                                            new SqlParameter("@TODATE", objPurchaseorder.ToDate),
                                            new SqlParameter("@VENDOR_ID", objPurchaseorder.Vendor_Id),
                                            new SqlParameter("@CREATEDBY", objPurchaseorder.CreatedBy),
                                            new SqlParameter("@PAGEINDEX", objPurchaseorder.pageindex),
                                            new SqlParameter("@PAGESIZE", objPurchaseorder.pagesize)
                                           };
                outPut = objDAL.GetJson(connection, "TBLPO_H_S_ALL", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        #endregion

        /// <summary>
        /// Receive Stock in Inventory
        /// </summary>
        /// <param name="file">Invoice File</param>
        /// <param name="Model">PO Detail</param>
        /// <returns>Success Msg</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("ReceivePOStockIn")]
        public string ReceivePOStockIn(IFormFile file, string Model)
        {
            string outPut = string.Empty;
            string FileName = string.Empty;
            Guid obj;

            StockIn objStockIn = new StockIn();
            if (Model != null)
                objStockIn = JsonConvert.DeserializeObject<StockIn>(Model);
            else
                return outPut;

            if (file != null)
            {
                obj = Guid.NewGuid();
                FileName = obj.ToString() + "_PO_Invoice_" + file.FileName;
                objStockIn.InvoiceFile = FileName;
            }
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objStockIn.Product_DetailJSON);
                string FreeProductJson = JsonConvert.SerializeObject(objStockIn.freeProduct_DetailJSON);
                SqlParameter[] spparams = {
                                            new SqlParameter("@PURCHASEORDERID", objStockIn.PurchaseOrder_Id),
                                            new SqlParameter("@DEMANDID", objStockIn.DemandId),
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
                outPut = objDAL.PostWithResultCode(connection, "TBLSTOCKIN_I", spparams);
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
        /// Challan Invoice Entry
        /// </summary>
        /// <param name="file">Invoice File</param>
        /// <param name="Model">PO Detail</param>
        /// <returns>Success Msg</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveChallanDetail")]
        public string SaveChallanDetail(IFormFile file, string Model)
        {
            string outPut = string.Empty;
            string FileName = string.Empty;
            Guid obj;

            StockIn objStockIn = new StockIn();
            if (Model != null)
                objStockIn = JsonConvert.DeserializeObject<StockIn>(Model);
            else
                return outPut;

            if (file != null)
            {
                obj = Guid.NewGuid();
                FileName = obj.ToString() + "_PO_Invoice_" + file.FileName;
                objStockIn.InvoiceFile = FileName;
            }
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objStockIn.Product_DetailJSON);
                SqlParameter[] spparams = {
                                            new SqlParameter("@INVOICENO", objStockIn.InvoiceNO),
                                            new SqlParameter("@INVOICEFILE", objStockIn.InvoiceFile),
                                            new SqlParameter("@STOCKHEAD_ID", objStockIn.ChallanNo),
                                            new SqlParameter("@INVOICEDATE", objStockIn.InvoiceDate),
                                            new SqlParameter("@CREATEDBY", objStockIn.CreatedBy),
                                            new SqlParameter("@IPADDRESS", objStockIn.IPAddress),
                                            new SqlParameter("@RESULTMSG", "")
                                           };
                outPut = objDAL.PostWithResultCode(connection, "TBLSTOCK_I_INVOICE", spparams);
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
        /// Get Product Inventory in Godown/Stock for Demand 
        /// </summary>
        /// <param name="objPurchaseorder">Demand_DID</param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetDemandPoStockList")]
        public string GetDemandPoStockList([FromBody] PurchaseOrder objPurchaseorder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@DEMAND_DID", objPurchaseorder.Demand_DID)
                                           };
                outPut = objDAL.GetJson(connection, "TBLDEMAND_PO_STOCK_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        #endregion

        /// <summary>
        /// Deactive/Approve Purchase Order
        /// </summary>
        /// <param name="objDemandModel">PURCHASEORDERID,CREATEDBY,IPADDRESS,RESULTMSG</param>
        /// <returns>ID , success Message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("PurchaseorderApproveOrDelete")]
        public string PurchaseorderApproveOrDelete([FromBody] PurchaseOrder objPurchaseOrder)
        {

            string outPut = string.Empty;
            try
            {
                //string ProductJson = JsonConvert.SerializeObject(objDemandModel.product_json);
                SqlParameter[] spparams = { new SqlParameter("@PURCHASEORDER_ID",objPurchaseOrder.PurchaseOrder_Id),
                                            new SqlParameter("@MODE",objPurchaseOrder.Mode),
                                            new SqlParameter("@CREATEDBY",objPurchaseOrder.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objPurchaseOrder.IPAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLPO_U_STATUS", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Challan by user id and purchaseorderid
        /// </summary>
        /// <param name="ID">MSTMCATID</param>
        /// <returns>List of Category(ID ,Name)</returns>     
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetChallanList")]
        public string GetChallanList([FromBody] PurchaseOrder objPurchaseOrder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@CREATEDBY", objPurchaseOrder.CreatedBy),
                                            new SqlParameter("@PURCHASEORDERID", objPurchaseOrder.PurchaseOrder_Id)};
                outPut = objDAL.GetJson(connection, "TBLSTOCK_S_CHALLANNO", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Get PRoduct & vendor by Group Id
        /// </summary>
        /// <param name="objPurchaseOrder"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetProductorVendorByMktGroup")]
        public string GetProductorVendorByMktGroup([FromBody] PurchaseOrder objPurchaseOrder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MKTGROUPID", objPurchaseOrder.MKTGROUPID),
                                            new SqlParameter("@CREATEDBY", objPurchaseOrder.CreatedBy)};
                outPut = objDAL.GetJson(connection, "TBLPRODUCT_S_FORTBLPO_BYMKTGROUPID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get vendor by Group Id
        /// </summary>
        /// <param name="objPurchaseOrder"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetVendorByGroupId")]
        public string GetVendorByGroupId([FromBody] PurchaseOrder objPurchaseOrder)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MKTGROUPID", objPurchaseOrder.MKTGROUPID)};
                outPut = objDAL.GetJson(connection, "MSTMKTGROUP_VENDOR_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
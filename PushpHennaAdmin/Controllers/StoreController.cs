using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PushpHennaAdmin.Controllers
{
    [Route("api/Store")]
    public class StoreController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;

        public StoreController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// show store list in grid
        /// </summary>
        /// <param name="PageIndex">1</param>
        /// <param name="PageSize">10</param>
        /// <returns>store list</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetStoreListData")]
        public string GetStoreListData([FromBody]  GridListFilterModel objGridListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PAGEINDEX",objGridListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE",objGridListModel.PAGESIZE),
                                            new SqlParameter("@NAME",objGridListModel.NAME)
                                          };
                outPut = objDAL.GetJson(connection, "MSTSTORE_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Store Data by Id
        /// </summary>
        /// <param name="Id">store Id</param>
        /// <returns>store data </returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetStoreById/{Id}")]
        public string GetStoreById(string Id)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@STORE_ID  ", Id) };
                outPut = objDAL.GetJson(connection, "MSTSTORE_S_BYID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// save store
        /// </summary>
        /// <param name="objStoreModel">store related information</param>
        /// <returns>sucess msg</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveStoreMaster")]
        public string SaveStoreMaster([FromBody] StoreModel objStoreModel)
        {
            string outPut = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(objStoreModel.MSTCATIDSTR))
                {
                    objStoreModel.MSTCATIDSTR = objStoreModel.MSTCATIDSTR.Replace(objStoreModel.MSTCATIDSTR.Substring(0, 1), "");
                    objStoreModel.MSTCATIDSTR = objStoreModel.MSTCATIDSTR.Replace(objStoreModel.MSTCATIDSTR.Substring(objStoreModel.MSTCATIDSTR.Length - 1), "");
                }
                SqlParameter[] spparams = { new SqlParameter("@STORE_NAME",objStoreModel.STORE_NAME),
                                            new SqlParameter("@PERSON_NAME", objStoreModel.PERSON_NAME) ,
                                            new SqlParameter("@MOBILE",objStoreModel.MOBILE),
                                            new SqlParameter("@PHONE",objStoreModel.PHONE),
                                            new SqlParameter("@EMAIL",objStoreModel.EMAIL),
                                            new SqlParameter("@ADDRESS",objStoreModel.ADDRESS),
                                            new SqlParameter("@PINCODE",objStoreModel.PINCODE),
                                            new SqlParameter("@STATEID",objStoreModel.STATEID),
                                            new SqlParameter("@DISTRICT_ID",objStoreModel.DISTRICT_ID),
                                            new SqlParameter("@CITYID",objStoreModel.CITYID),
                                            //new SqlParameter("@MSTCATIDSTR",objStoreModel.MSTCATIDSTR),
                                            new SqlParameter("@GSTNO",objStoreModel.GSTNO),
                                            new SqlParameter("@DLNO",objStoreModel.DLNO),
                                            new SqlParameter("@TINNO",objStoreModel.TINNO),
                                            new SqlParameter("@CREATEDBY",objStoreModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objStoreModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTSTORE_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update Store data Store Id Wise
        /// </summary>
        /// <param name="objStoreModel">STORE_NAME,PERSON_NAME, MOBILE, PHONE, EMAIL, ADDRESS, PINCODE, CITYID, DISTRICT_ID, CREATEDBY, IPADDRESS</param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateStoreData")]
        public string UpdateStoreData([FromBody] StoreModel objStoreModel)
        {
            string outPut = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(objStoreModel.MSTCATIDSTR))
                {
                    objStoreModel.MSTCATIDSTR = objStoreModel.MSTCATIDSTR.Replace(objStoreModel.MSTCATIDSTR.Substring(0, 1), "");
                    objStoreModel.MSTCATIDSTR = objStoreModel.MSTCATIDSTR.Replace(objStoreModel.MSTCATIDSTR.Substring(objStoreModel.MSTCATIDSTR.Length - 1), "");
                }
                SqlParameter[] spparams = { new SqlParameter("@STORE_ID",objStoreModel.STORE_ID),
                                            new SqlParameter("@STORE_NAME",objStoreModel.STORE_NAME),
                                            new SqlParameter("@PERSON_NAME", objStoreModel.PERSON_NAME) ,
                                            new SqlParameter("@MOBILE",objStoreModel.MOBILE),
                                            new SqlParameter("@PHONE",objStoreModel.PHONE),
                                            new SqlParameter("@EMAIL",objStoreModel.EMAIL),
                                            new SqlParameter("@ADDRESS",objStoreModel.ADDRESS),
                                            new SqlParameter("@PINCODE",objStoreModel.PINCODE),
                                            new SqlParameter("@STATEID",objStoreModel.STATEID),
                                            new SqlParameter("@CITYID",objStoreModel.CITYID),
                                           // new SqlParameter("@MSTCATIDSTR",objStoreModel.MSTCATIDSTR),
                                            new SqlParameter("@GSTNO",objStoreModel.GSTNO),
                                            new SqlParameter("@DLNO",objStoreModel.DLNO),
                                            new SqlParameter("@TINNO",objStoreModel.TINNO),
                                            new SqlParameter("@DISTRICT_ID",objStoreModel.DISTRICT_ID),
                                            new SqlParameter("@REMARKS",objStoreModel.REMARK),
                                            new SqlParameter("@CREATEDBY",objStoreModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objStoreModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTSTORE_U", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Show store id and store name list for dropDown list
        /// </summary>
        /// <returns>list</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetStore")]
        public string GetStore([FromBody] FilterDropDown objFilterDropDown)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@STOREIDSTR", objFilterDropDown.FilterStr) };
                outPut = objDAL.GetJson(connection, "MSTSTORE_S_LIST", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update store status (active =1 and deactive =0)
        /// </summary>
        /// <param name="objStoreModel">active/deactive and store id</param>
        /// <returns>status </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateStoreActiveStatus")]
        public string UpdateStoreActiveStatus([FromBody] StoreModel objStoreModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@STORE_ID ", objStoreModel.STORE_ID),
                                            new SqlParameter("@ISACTIVE",objStoreModel.ISACTIVE),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTSTORE_U_ISACTIVE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
                //log.Error(outPut);
            }
            return outPut;
        }

        /// <summary>
        /// Approval for Store
        /// </summary>
        /// <param name="objMasterApprovalModel">OBJECTID,OBJECTNAME(tablename),REMARKS,STATUSID,ISACTIVE,CREATEDBY,IPADDRESS</param>
        /// <returns>MASTERDATATRAIL_ID  with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("ApprovalStore")]
        public string ApprovalStore([FromBody]  MasterApprovalModel objMasterApprovalModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@OBJECTID", objMasterApprovalModel.OBJECTID),
                                            new SqlParameter("@OBJECTNAME", objMasterApprovalModel.OBJECTNAME),
                                            new SqlParameter("@REMARKS", objMasterApprovalModel.REMARKS),
                                            new SqlParameter("@STATUSID", objMasterApprovalModel.STATUSID),
                                            new SqlParameter("@ISACTIVE", objMasterApprovalModel.ISACTIVE),
                                            new SqlParameter("@CREATEDBY", objMasterApprovalModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS", objMasterApprovalModel.IPADDRESS),
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
        /// Get Godown & Store Inventory Detail
        /// </summary>
        /// <param name="objMasterApprovalModel"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetStockDetail")]
        public string GetStockDetail([FromBody]  StockDetail objStockDetail)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@ISEXPIRY", objStockDetail.ISEXPIRY),
                                            new SqlParameter("@PROD_NAME", objStockDetail.PROD_NAME),
                                            new SqlParameter("@STOCKFORID", objStockDetail.STOCKFORID),
                                            new SqlParameter("@STOCKFOR", objStockDetail.STOCKFOR),
                                            new SqlParameter("@PAGEINDEX", objStockDetail.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE", objStockDetail.PAGESIZE)
                                          };
                outPut = objDAL.GetJson(connection, "TBLSTOCK_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        #region stock transfer Challan Detail

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objStockDetail"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetChallanListByIndentNo")]
        public string GetChallanListByIndentNo([FromBody]  StockDetail objStockDetail)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@INDENTNO", objStockDetail.INDENTNO) ,
                                            new SqlParameter("@FROMDATE", objStockDetail.FromDate),
                                            new SqlParameter("@TODATE", objStockDetail.ToDate),
                                            new SqlParameter("@CREATEDBY", objStockDetail.CREATEDBY) };
                outPut = objDAL.GetJson(connection, "TBLSTOCKIN_TRANSFER_SCHALLAN_BYINDENTNO", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Challan Number detail 
        /// </summary>
        /// <param name="objStockDetail">CHALLANNO_TRANSFER</param>
        /// <returns>list of Stock transfer in challan number</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetChallanDetail")]
        public string GetChallanDetail([FromBody]  StockDetail objStockDetail)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@STOCKHEAD_ID", objStockDetail.STOCKHEAD_ID) };
                outPut = objDAL.GetJson(connection, "TBLSTOCKIN_TRANSFER_S_BYCHALLAN_PRINT", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        #endregion

        #region Menual Stock Update By Store......

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("StockUpdateByStore")]
        public string StockUpdateByStore([FromBody] StockUpdateModel objStockUpdateModel)
        {
            string outPut = string.Empty;
            try
            {
                string ProductJson = string.Empty;

                if (objStockUpdateModel.PRODUCT_DETAILJSON != null)
                    ProductJson = JsonConvert.SerializeObject(objStockUpdateModel.PRODUCT_DETAILJSON);
                #region code for attribute
                DataTable TTblAttribute = new DataTable("TTblAttribute");
                TTblAttribute.Columns.Add("PROD_ID", typeof(int));
                TTblAttribute.Columns.Add("EXPIRYDATE", typeof(DateTime));
                TTblAttribute.Columns.Add("IN_QTY", typeof(decimal));
                TTblAttribute.Columns.Add("BATCH", typeof(string));
                TTblAttribute.Columns.Add("SALERATE", typeof(decimal));
                TTblAttribute.Columns.Add("HSN_ID", typeof(int));
                //TTblAttribute.Columns.Add("PURCHASE_RATE", typeof(float));


                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "PURCHASE_RATE", DataType = typeof(float), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "PURCHASE_MRP", DataType = typeof(float), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "PURCHASE_CASSTAX", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "PURCHASE_TRADEDIS", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "LOT_DIS", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "SCHEME_DIS", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "CUST_DIS", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "COSTRATE", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "ACTUALMARGIN", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "MFGDATE", DataType = typeof(DateTime), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "PURCHASE_TRADEDIS_PERC", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "SCHEME_DISCOUNTTYPEID", DataType = typeof(int), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "SCHEME_DIS_AMT", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "PURCHASE_CASSTAX_PERC", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "GST", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "GST_AMOUNT", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "AMOUNT", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "GROSSAMOUNT", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "GROSSMARGIN", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "OTERCHARGES_PERQTY", DataType = typeof(decimal), AllowDBNull = true });
                TTblAttribute.Columns.Add(new DataColumn { ColumnName = "SALE_TRADERATE", DataType = typeof(float), AllowDBNull = true });

                if (objStockUpdateModel.PRODUCT_DETAILJSON != null)
                {
                    foreach (var item in objStockUpdateModel.PRODUCT_DETAILJSON)
                    {
                        DataRow row = TTblAttribute.NewRow();

                        row["PROD_ID"] = item.PROD_ID;
                        row["HSN_ID"] = item.HSN_ID;
                        if(item.PURCHASE_RATE != null) row["PURCHASE_RATE"] = item.PURCHASE_RATE;
                        if (item.PURCHASE_MRP != null) row["PURCHASE_MRP"] = item.PURCHASE_MRP;
                        if (item.PURCHASE_CASSTAX != null) row["PURCHASE_CASSTAX"] = item.PURCHASE_CASSTAX;
                        if(item.PURCHASE_TRADEDIS != null) row["PURCHASE_TRADEDIS"] = item.PURCHASE_TRADEDIS;
                        row["IN_QTY"] = item.QTY;
                        if(item.LOT_DIS != null) row["LOT_DIS"] = item.LOT_DIS;
                        if(item.SCHEME_DIS != null) row["SCHEME_DIS"] = item.SCHEME_DIS;
                        if(item.CUST_DIS != null) row["CUST_DIS"] = item.CUST_DIS;
                        if(item.SALERATE != null) row["SALERATE"] = item.SALERATE;
                        if(item.COSTRATE != null) row["COSTRATE"] = item.COSTRATE;
                        if(item.ACTUALMARGIN != null) row["ACTUALMARGIN"] = item.ACTUALMARGIN;
                        if(item.BATCH != null) row["BATCH"] = item.BATCH;
                        if(item.MFGDATE != null) row["MFGDATE"] = item.MFGDATE;
                        if(item.EXPIRYDATE != null) row["EXPIRYDATE"] = item.EXPIRYDATE;
                        if(item.PURCHASE_TRADEDIS_PERC != null) row["PURCHASE_TRADEDIS_PERC"] = item.PURCHASE_TRADEDIS_PERC;
                        if(item.SCHEME_DISCOUNTTYPEID != null) row["SCHEME_DISCOUNTTYPEID"] = item.SCHEME_DISCOUNTTYPEID;
                        if(item.SCHEME_DIS_AMT != null) row["SCHEME_DIS_AMT"] = item.SCHEME_DIS_AMT;
                        if(item.PURCHASE_CASSTAX_PERC != null) row["PURCHASE_CASSTAX_PERC"] = item.PURCHASE_CASSTAX_PERC;
                        if(item.GST != null) row["GST"] = item.GST;
                        if(item.GST_AMOUNT != null) row["GST_AMOUNT"] = item.GST_AMOUNT;
                        if(item.AMOUNT != null) row["AMOUNT"] = item.AMOUNT;
                        if(item.GROSSAMOUNT != null) row["GROSSAMOUNT"] = item.GROSSAMOUNT;
                        if(item.GROSSMARGIN != null) row["GROSSMARGIN"] = item.GROSSMARGIN;
                        if(item.OTERCHARGES_PERQTY != null) row["OTERCHARGES_PERQTY"] = item.OTERCHARGES_PERQTY;
                        if (item.SALE_TRADERATE != null) row["SALE_TRADERATE"] = item.SALE_TRADERATE;
                        TTblAttribute.Rows.Add(row);
                    }
                }
                #endregion
                SqlParameter[] spparams = { new SqlParameter("@STOCKFORID", objStockUpdateModel.STOCKFORID),
                                            new SqlParameter("@STOCKFOR", objStockUpdateModel.STOCKFOR),
                                            new SqlParameter("@TTBLSTOCKBATCHMANUAL", TTblAttribute),
                                            new SqlParameter("@CREATEDBY",objStockUpdateModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objStockUpdateModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TBLSTOCK_DETAIL_BATCH_I_MANUAL", spparams);
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
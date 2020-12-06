using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace PushpHennaAdmin.Controllers
{
    [Route("api/Comman")]
    public class CommanController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;
        public CommanController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Get Master Category List 
        /// </summary>
        /// <returns>List of Master Category with ID and Name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("MasterCategory")]
        public string MSTMASTERCATEGORY()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTMASTERCATEGORY_S_LIST");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Category List Category ID and Name (master category wise)
        /// </summary>
        /// <param name="ID">MSTMCATID</param>
        /// <returns>List of Category(ID ,Name)</returns>     
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("Category/{Id}")]
        public string GetCategory(string Id)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MSTCATID", Id) };
                outPut = objDAL.GetJson(connection, "MSTCATEGORY_S_BY_MSTCATID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Sub Category List  ID and Name ( category wise)
        /// </summary>
        /// <param name="ID">CATID</param>
        /// <returns>List of Category(ID ,Name)</returns>
        [HttpGet]
        [Authorize(Policy = "JwtAuth")]
        [Route("SubCategory/{Id}")]
        public string GetSubCategory(string Id)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@CATID", Id) };
                outPut = objDAL.GetJson(connection, "MSTSUBCATEGORY_S_BY_CATID", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

         /// <summary>
        /// Get State drop down list
        /// </summary>
        /// <returns>State Id and Name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("MasterStateList")]
        public string MasterStateList()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTSTATE_S");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get District drop down list by State ID
        /// </summary>
        /// <returns>District Id and Name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("MasterDistrictList/{StateID}")]
        public string MasterDistrictList(string StateID)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@STATEID", StateID) };
                outPut = objDAL.GetJson(connection, "MSTDISTRICTS_S_BY_STATEID", spparams);
                //outPut = objDAL.GetJson(connection, "MSTDISTRICTS_S");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get City Drop Down list by District Id 
        /// </summary>
        /// <param name="Id">District Id </param>
        /// <returns>City  Id and Name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("MasterCityList/{Id}")]
        public string MasterCityList(string Id)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@DISTRICT_ID", Id) };
                outPut = objDAL.GetJson(connection, "MSTCITY_S_BYDISTRICT", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get tax Master List 
        /// </summary>
        /// <returns>List of Tax Master with ID and Name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("TaxMaster")]
        public string TAXMASTER()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTTAX_S_LIST");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

       

        /// <summary>
        /// Show HSN List in dropdown 
        /// </summary>
        /// <returns>VendorId and Vendor Name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("HSNMaster")]
        public string HSNMaster()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTHSN_S_LIST");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Show Weight List in dropdown 
        /// </summary>
        /// <returns>WEIGHTCLASSID and NAME</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("WeightMaster")]
        public string WeightMaster()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTWEIGHTCLASS_S");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get product list dropdown Material wise
        /// </summary>
        /// <returns>Product Id and Name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("ProductList/{Mode}")]
        public string GetProductList(string Mode)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MODE", Mode.Trim()) };
                outPut = objDAL.GetJson(connection, "TBLPRODUCT_List_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetProductVersionById/{Id}")]
        public string GetProductVersionById(string Id)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PROD_ID", Id) };
                outPut = objDAL.GetJson(connection, "TBLPRODUCTVERSION_S_BY_PRODID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Show Weight List in dropdown 
        /// </summary>
        /// <returns>WEIGHTCLASSID and NAME</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("Lot_ProductMaster")]
        public string Lot_ProductMaster()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTLOTS_PRODUCT_S_LIST");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Buyer Type list drop down list
        /// </summary>
        /// <returns>buyer id and name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetBuyerListDrp")]
        public string GetBuyerListDrp()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTBUYER_S_LIST");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetBuyerListType/{Id}")]
        public string GetBuyerListType(int Id)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@BUYERSTYPEID", Id) };
                outPut = objDAL.GetJson(connection, "MSTBUYERS_TYPE_LIST_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Payment mode lis 
        /// </summary>
        /// <param name="payment_requesttype">payment_requesttype = 1 for get payment list for counter sale if 2 for invoce payment mode list</param>
        /// <returns>payment type Text and Id list</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetPaymentMode/{payment_requesttype}")]
        public string GetPaymentMode(int payment_requesttype)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PAYMENT_REQUESTTYPE", payment_requesttype) };
                outPut = objDAL.GetJson(connection, "MSTPAYMENTMODE_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Approval function for All Masters
        /// </summary>
        /// <param name="objMasterApprovalModel">OBJECTID,OBJECTNAME(tablename),REMARKS,STATUSID,ISACTIVE,CREATEDBY,IPADDRESS</param>
        /// <returns>MASTERDATATRAIL_ID  with Success message</returns>

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("ApprovalMasters")]
        public string ApprovalMasters([FromBody]  MasterApprovalModel objMasterApprovalModel)
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
        /// Get objection,approval and rejection list on specific master data row
        /// </summary>
        /// <param name="objMasterApprovalModel">objectid(specific row),objectname table name</param>
        /// <returns>list of  remark,createdby,statustype,date</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("MastersApprovalList")]
        public string MastersApprovalList([FromBody]  MasterApprovalModel objMasterApprovalModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {new SqlParameter("@OBJECTID", objMasterApprovalModel.OBJECTID),
                                            new SqlParameter("@OBJECTNAME", objMasterApprovalModel.OBJECTNAME)};
                outPut = objDAL.GetJson(connection, "MASTERDATATRAIL_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        #region Master Dropdown List
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
        /// Get Store drop down list
        /// </summary>
        /// <returns>Godown Id and Name</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetStoreList")]
        public string GetStoreList([FromBody] FilterDropDown objFilterDropDown)
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
        /// Get Request for Weight List
        /// </summary>
        /// <returns>Weight List (ID ,Name)</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("ProductWeight")]
        public string GetProductWeight()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTWEIGHTCLASS_S");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Show Vendor List in dropdown 
        /// </summary>
        /// <returns>VendorId and Vendor Name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("VendorMaster")]
        public string VendorMaster()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTVENDOR_S_LIST");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }



        /// <summary>
        /// Get Available product stock in Godown
        /// </summary>
        /// <param name="CreatedBy">CreatedBy</param>
        /// <param name="ProdName">ProdName</param>
        /// <returns>avail Qty</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetAvilableGodownProductStockById")]
        public string GetAvilableGodownProductStockById([FromBody]  FilterProductModel objFilterProductModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@CREATEDBY", objFilterProductModel.CREATEDBY),
                                            new SqlParameter("@PRODNAME", objFilterProductModel.PRODNAME) };
                outPut = objDAL.GetJson(connection, "TBLGODOWN_AVAILABLESTOCK", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Available product stock in Godown
        /// </summary>
        /// <param name="Id">Product Id</param>
        /// <returns>avail Qty</returns>
        //[HttpGet, Authorize(Policy = "JwtAuth")]
        //[Route("GetAvilableGodownProductStockById")]
        //public string GetAvilableGodownProductStockById_temp()
        //{
        //    string outPut = string.Empty;
        //    try
        //    {
        //        List<ProductMaster> objProductMaster = new List<ProductMaster>();
        //        DataSet ds = objDAL.Get(connection, "TBLGODOWN_AVAILABLESTOCK", );
        //        if (ds.Tables.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                objProductMaster = dt.AsEnumerable()
        //                                 .Select(row => new Cards
        //                                 {
        //                                    // assuming column 0's type is Nullable<long>
        //                                    CardID = row.Field<long?>(0).GetValueOrDefault(),
        //                                     CardName = String.IsNullOrEmpty(row.Field<string>(1))
        //                                         ? "not found"
        //                                         : row.Field<string>(1),
        //                                 }).ToList();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        outPut = ex.Message;
        //    }
        //    return outPut;
        //}
        #endregion


        #region Discount master
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetDiscountTypeList")]
        public string GetDiscountTypeList([FromBody]  FilterProductModel objFilterProductModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@DISCOUNT_FOR", objFilterProductModel.DISCOUNT_FOR) };
                outPut = objDAL.GetJson(connection, "MSTDISCOUNTTYPE_S_LIST", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        #endregion

        /// <summary>
        /// Get ResaleFormula Type List 
        /// </summary>
        /// <returns>List of ResaleFormula with ID and Name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("ResaleFormulaTypeList")]
        public string ResaleFormulaTypeList()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTSALERATE_FORMULATYP_S_LIST");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Users List 
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetAllUsers")]
        public string GetAllUsers([FromBody]  GridListModel objGridListModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@ID", objGridListModel.ID),
                                            new SqlParameter("@PAGEINDEX", objGridListModel.PAGEINDEX),
                                            new SqlParameter("@PAGESIZE", objGridListModel.PAGESIZE) };
                outPut = objDAL.GetJson(connection, "USERS_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }                        
            return outPut;
        }
    }
}
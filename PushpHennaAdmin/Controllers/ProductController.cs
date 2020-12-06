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
    [Route("api/Product")]
    public class ProductController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;

        public ProductController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Get product list by filter 
        /// </summary>
        /// <param name="objProductFilterParameter"> SEARCHTEXT,ORDERBY,ORDERBYTYPE,mode,PAGEINDEX,PAGESIZE</param>
        /// <returns>product name,id,hsn code, type etc</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetProductListByFilter")] //{PageIndex}/{PageSize}
        public string GetProductListByFilter([FromBody]ProductFilterParameter objProductFilterParameter)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@SEARCHTEXT",objProductFilterParameter.SearchText),
                                            new SqlParameter("@ORDERBY",objProductFilterParameter.ORderBy),
                                            new SqlParameter("@ORDERBYTYPE",objProductFilterParameter.OrderByType),
                                            new SqlParameter("@MODE",objProductFilterParameter.Mode),
                                            new SqlParameter("@PAGEINDEX",objProductFilterParameter.PageIndex),
                                            new SqlParameter("@PAGESIZE",objProductFilterParameter.PageSize),
                                          };
                outPut = objDAL.GetJson(connection, "TBLPRODUCT_S_BYFILTER", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        ///  Get Request for Product by Id
        /// </summary>
        /// <param name="Id"> product id</param>
        /// <returns>product detail</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetProductListById")]
        public string GetProductListById([FromBody] ProductList objproductUpdate)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PROD_ID", objproductUpdate.ProductId) };
                outPut = objDAL.GetJson(connection, "TBLPRODUCT_S_BYID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Post request for save product entery
        /// </summary>
        /// <param name="objProductModel">list of product model</param>
        /// <returns>success / error</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveProduct")]
        public string SaveProduct([FromBody] Product objProductModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MCATEGORYID",objProductModel.MASTERCATEGORYID),
                                            new SqlParameter("@CATEGORYID",objProductModel.CATEGORYID),
                                            new SqlParameter("@SUBCATEGORYID",objProductModel.SUBCATEGORYID),
                                            new SqlParameter("@GENERIC_ID",objProductModel.GENERIC_ID),
                                            new SqlParameter("@NAME",objProductModel.NAME),
                                            new SqlParameter("@ALIAS",objProductModel.ALIAS),
                                            new SqlParameter("@MODEL",objProductModel.MODEL),
                                            new SqlParameter("@DISPLAYNAME",objProductModel.DISPLAYNAME),
                                            new SqlParameter("@DETAILDESCRIPTION",objProductModel.DETAILDESCRIPTION),
                                            new SqlParameter("@HSN_ID",objProductModel.HSN_ID),
                                            new SqlParameter("@SIZECLASSID",objProductModel.SIZECLASSID),
                                            new SqlParameter("@WEIGHT",objProductModel.WEIGHT),
                                            new SqlParameter("@WEIGHTCLASSID",objProductModel.WEIGHTCLASSID),
                                            new SqlParameter("@ISFASTMOVING",objProductModel.ISFASTMOVING),
                                            new SqlParameter("@MKTGROUPID",objProductModel.MKTGROUPID),
                                            new SqlParameter("@MAXSTOCK",objProductModel.MAXSTOCK),
                                            new SqlParameter("@MINSTOCK",objProductModel.MINSTOCK),
                                            new SqlParameter("@ISAPPLYONLOT",objProductModel.ISAPPLYONLOT),
                                            new SqlParameter("@APPLYONLOTSTR",objProductModel.APPLYONLOTSTR),
                                            new SqlParameter("@DRUGTYPE",objProductModel.DRUGTYPE),
                                            new SqlParameter("@CREATEDBY",objProductModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objProductModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")
                                        };
                outPut = objDAL.PostWithResultCode(connection, "TBLPRODUCT_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update product data by product Id
        /// </summary>
        /// <param name="objProductModel">product Id</param>
        /// <returns>Sucess</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateProductData")]
        public string UpdateProductData([FromBody]Product objProductModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PROD_ID",objProductModel.PROD_ID),
                                            new SqlParameter("@NAME",objProductModel.NAME),
                                            new SqlParameter("@MODEL",objProductModel.MODEL),
                                            new SqlParameter("@DISPLAYNAME",objProductModel.DISPLAYNAME),
                                            new SqlParameter("@DETAILDESCRIPTION",objProductModel.DETAILDESCRIPTION),
                                            //new SqlParameter("@VENDORID",objProductModel.CURRENTVENDORID),
                                            new SqlParameter("@MCATEGORYID",objProductModel.MASTERCATEGORYID),
                                            new SqlParameter("@CATEGORYID",objProductModel.CATEGORYID),
                                            new SqlParameter("@SUBCATEGORYID",objProductModel.SUBCATEGORYID),
                                            new SqlParameter("@BARCODE",objProductModel.BARCODE),
                                            new SqlParameter("@BRAND",objProductModel.BRAND),
                                            new SqlParameter("@HSN_ID",objProductModel.HSN_ID),
                                            new SqlParameter("@ISAPPLYONLOT",objProductModel.ISAPPLYONLOT),
                                            new SqlParameter("@APPLYONLOTSTR",objProductModel.APPLYONLOTSTR),
                                            //new SqlParameter("@GSTIN",objProductModel.GSTIN),
                                            new SqlParameter("@WEIGHT",objProductModel.WEIGHT),
                                            new SqlParameter("@WEIGHTCLASSID",objProductModel.WEIGHTCLASSID),
                                            new SqlParameter("@CREATEDBY",objProductModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objProductModel.IPADDRESS),
                                            new SqlParameter("@REMARKS",objProductModel.REMARKS),
                                            new SqlParameter("@RESULTMSG","")
                                        };
                outPut = objDAL.PostWithResultCode(connection, "TBLPRODUCT_U", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Save product price detail
        /// </summary>
        /// <param name="objProductModel"> price detail</param>
        /// <returns>success with product id</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveProductVersionData")]
        public string SaveProductVersionData([FromBody] Product objProductModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PRODID",objProductModel.PROD_ID),
                                            new SqlParameter("@CREATEDBY",objProductModel.CREATEDBY),
                                            new SqlParameter("@LISTPRICE",objProductModel.LISTPRICE),
                                            new SqlParameter("@SELLINGPRICE",objProductModel.SELLINGPRICE),
                                            new SqlParameter("@IGST",objProductModel.IGST),
                                            new SqlParameter("@SGST",objProductModel.SGST),
                                            new SqlParameter("@GST",objProductModel.GST),
                                            new SqlParameter("@DISCOUNT",objProductModel.DISCOUNT),
                                            new SqlParameter("@MARGIN",objProductModel.MARGIN),
                                            new SqlParameter("@DISCOUNTCLASS",objProductModel.DISCOUNTCLASS),
                                            new SqlParameter("@EXPIRYDATE",objProductModel.EXPIRYDATE),
                                            new SqlParameter("@IPADDRESS",objProductModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")
                                        };
                outPut = objDAL.PostWithResultCode(connection, "TBLPRODUCTVESRION_I", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        #region Api For SIMS-Medical Product

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GroupMasterList")]
        public string GroupMasterList([FromBody] ProductList objProductList)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {new SqlParameter("@PRODIDSTR", objProductList.ProductId)};
                outPut = objDAL.GetJson(connection, "MSTMKTGROUP_S_LIST",spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GenericMasterList")]
        public string GenericMasterList()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTGENERIC_S_LIST");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetSizeClassList")]
        public string GetSizeClassList()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTSIZECLASS_S");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get product list by filter 
        /// </summary>
        /// <param name="objProductFilterParameter"> SEARCHTEXT,ORDERBY,ORDERBYTYPE,mode,PAGEINDEX,PAGESIZE</param>
        /// <returns>product name,id,hsn code, type etc</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetProductListByFilters")] //{PageIndex}/{PageSize}
        public string GetProductListByFilters([FromBody] ProductFilterParameter objProductFilterParameter)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@PROD_NAME",objProductFilterParameter.SearchText),
                                            new SqlParameter("@MKTGROUPID",objProductFilterParameter.MktGroupId),
                                            new SqlParameter("@CHKID",objProductFilterParameter.ChkId),
                                            new SqlParameter("@PAGEINDEX",objProductFilterParameter.PageIndex),
                                            new SqlParameter("@PAGESIZE",objProductFilterParameter.PageSize),
                                          };
                outPut = objDAL.GetJson(connection, "TEST_TBLPRODUCT_S_BYFILTER", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateProductDetail")]
        public string UpdateProductDetail([FromBody]Product objProductModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PROD_ID",objProductModel.PROD_ID),
                                            new SqlParameter("@MCATEGORYID",objProductModel.MASTERCATEGORYID),
                                            new SqlParameter("@CATEGORYID",objProductModel.CATEGORYID),
                                            new SqlParameter("@SUBCATEGORYID",objProductModel.SUBCATEGORYID),
                                            new SqlParameter("@GENERIC_ID",objProductModel.GENERIC_ID),
                                            new SqlParameter("@ISFASTMOVING",objProductModel.ISFASTMOVING),
                                            new SqlParameter("@MKTGROUPID",objProductModel.MKTGROUPID),
                                            new SqlParameter("@NAME",objProductModel.NAME),
                                            new SqlParameter("@ALIAS",objProductModel.ALIAS),
                                            new SqlParameter("@MODEL",objProductModel.MODEL),
                                            new SqlParameter("@DISPLAYNAME",objProductModel.DISPLAYNAME),
                                            new SqlParameter("@DETAILDESCRIPTION",objProductModel.DETAILDESCRIPTION),
                                            new SqlParameter("@HSN_ID",objProductModel.HSN_ID),
                                            new SqlParameter("@SIZECLASSID",objProductModel.SIZECLASSID),
                                            new SqlParameter("@WEIGHT",objProductModel.WEIGHT),
                                            new SqlParameter("@WEIGHTCLASSID",objProductModel.WEIGHTCLASSID),
                                            new SqlParameter("@WEIGHTCLASSID_PAK",objProductModel.WEIGHTCLASSID_PAK),
                                            new SqlParameter("@WEIGHT_PAK",objProductModel.WEIGHT_PAK),
                                            new SqlParameter("@MAXSTOCK",objProductModel.MAXSTOCK),
                                            new SqlParameter("@MINSTOCK",objProductModel.MINSTOCK),
                                            new SqlParameter("@ISAPPLYONLOT",objProductModel.ISAPPLYONLOT),
                                            new SqlParameter("@APPLYONLOTSTR",objProductModel.APPLYONLOTSTR),
                                            new SqlParameter("@DRUGTYPE",objProductModel.DRUGTYPE),
                                            new SqlParameter("@CREATEDBY",objProductModel.CREATEDBY),
                                            new SqlParameter("@IPADDRESS",objProductModel.IPADDRESS),
                                            new SqlParameter("@RESULTMSG","")
                                        };
                outPut = objDAL.PostWithResultCode(connection, "TBLPRODUCT_U", spparams);

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateProductStatus")]
        public string UpdateProductStatus([FromBody] ProductUpdateStatus objproductUpdate)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@PROD_ID", objproductUpdate.Prod_Id),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "TEST_TBLPRODUCT_DEACTIVE", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        #endregion
        /// <summary>
        /// Get Product list by mkt group 
        /// </summary>
        /// <param name="objProductFilterParameter">MKTGROUPIDSTR,mode</param>
        /// <returns>productname,ID</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetProductByMktGroup")]
        public string GetProductByMktGroup([FromBody] ProductFilterParameter objProductFilterParameter)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MKTGROUPID", objProductFilterParameter.MktGroupId),
                                            new SqlParameter("@PRODNAME",objProductFilterParameter.ProdName),
                                            new SqlParameter("@CREATEDBY",objProductFilterParameter.CreatedBY)};
                outPut = objDAL.GetJson(connection, "TBLPRODUCT_List_SBYMKTGROUPID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        ///  Get Request for Product by Id
        /// </summary>
        /// <param name="Id"> groupid, categoryid, product name</param>
        /// <returns>ResaleFormula List</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetProductResaleFormulaList")]
        public string GetProductResaleFormulaList([FromBody] ResaleFormulaSearchModel objResaleFormulaSearchModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MKTGROUPID", objResaleFormulaSearchModel.MKTGROUPID),
                                            new SqlParameter("@CATEGORYID", objResaleFormulaSearchModel.CATEGORYID),
                                            new SqlParameter("@PRODNAME", objResaleFormulaSearchModel.PRODNAME)
                };
                outPut = objDAL.GetJson(connection, "TBLPRODUCT_S_SALEFORMULA", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveSaleRateFormula")]
        public string SaveSaleRateFormula([FromBody] SaveSaleRate objSaveSaleRate)
        {
            string outPut = string.Empty;
            try
            {
                string ProductJson = JsonConvert.SerializeObject(objSaveSaleRate.product_detailjson);
                SqlParameter[] spparams = { new SqlParameter("@PRODUCT_DETAILJSON",ProductJson),
                                            new SqlParameter("@CREATEDBY",objSaveSaleRate.CreatedBy),
                                            new SqlParameter("@IPADDRESS",objSaveSaleRate.IPAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "MSTSALERATE_FORMULA_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
    }
}
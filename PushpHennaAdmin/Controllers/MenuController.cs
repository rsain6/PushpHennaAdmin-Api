using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Controllers
{
    [Route("api/Menu")]
    public class MenuController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;

        public MenuController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        #region Create Menu and Sub menu / Mange Create Update Delete and View (Menu & Sub Menu)        

        /// <summary>
        /// Get Menu List 
        /// </summary>
        /// <returns>Menu Name And Status</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetMenuList")]
        public string GetMenuList()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTMENU_S");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Save New Menu 
        /// </summary>
        /// <param name="objMenuModel">NAME, PAGEURL,ACTIONSTR ,ICONCLASS,ARROWSUBMENUCLASS ,SUBMENUCLASS ,SRNO ,CREATEDBY ,IPADDRESS</param>
        /// <returns>Menu Id with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveMenu")]
        public string SaveMenu([FromBody]MenuModel objMenuModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@NAME",objMenuModel.name),
                                            new SqlParameter("@PAGEURL",objMenuModel.pageurl),
                                            new SqlParameter("@ACTIONSTR",objMenuModel.actionstr),
                                            new SqlParameter("@ARROWSUBMENUCLASS",objMenuModel.arrowsubmenuclass),
                                            new SqlParameter("@ICONCLASS",objMenuModel.iconclass),
                                            new SqlParameter("@SUBMENUCLASS",objMenuModel.submenuclass),
                                            new SqlParameter("@SUBPAGEURLS",objMenuModel.subpageurls),
                                            new SqlParameter("@CREATEDBY",objMenuModel.createdby),
                                            new SqlParameter("@IPADDRESS",objMenuModel.ipaddress),
                                            new SqlParameter("@RESULTMSG","")
                                        };
                outPut = objDAL.PostWithResultCode(connection, "MSTMENU_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// update Menu By Id
        /// </summary>
        /// <param name="objMenuModel">MENUID,NAME, PAGEURL,ACTIONSTR ,ICONCLASS,ARROWSUBMENUCLASS ,SUBMENUCLASS ,SRNO ,CREATEDBY ,IPADDRESS</param>
        /// <returns>Menu Id with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateMenu")]
        public string UpdateMenu([FromBody]MenuModel objMenuModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@MENUID",objMenuModel.menuid),
                                            new SqlParameter("@NAME",objMenuModel.name),
                                            new SqlParameter("@PAGEURL",objMenuModel.pageurl),
                                            new SqlParameter("@ACTIONSTR",objMenuModel.actionstr),
                                            new SqlParameter("@ARROWSUBMENUCLASS",objMenuModel.arrowsubmenuclass),
                                            new SqlParameter("@ICONCLASS",objMenuModel.iconclass),
                                            new SqlParameter("@SUBMENUCLASS",objMenuModel.submenuclass),
                                            new SqlParameter("@SUBPAGEURLS",objMenuModel.subpageurls),
                                            new SqlParameter("@SRNO",objMenuModel.srno),
                                            new SqlParameter("@CREATEDBY",objMenuModel.createdby),
                                            new SqlParameter("@IPADDRESS",objMenuModel.ipaddress),
                                            new SqlParameter("@RESULTMSG","")
                                        };
                outPut = objDAL.PostWithResultCode(connection, "MSTMENU_U", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Active and deactive Menu
        /// </summary>
        /// <param name="objMenuModel">Menuid,Active/deactive Value </param>
        /// <returns>Id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DeactiveMenu")]
        public string DeactiveMenu([FromBody]MenuModel objMenuModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@MENUID",objMenuModel.menuid),
                                            new SqlParameter("@ISACTIVE ",objMenuModel.isactive),
                                            new SqlParameter("@CREATEDBY",objMenuModel.createdby),
                                            new SqlParameter("@IPADDRESS",objMenuModel.ipaddress),
                                            new SqlParameter("@RESULTMSG","")
                                        };
                outPut = objDAL.PostWithResultCode(connection, "MSTMENU_D", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        /// <summary>
        /// Get Menu Detail By Id 
        /// </summary>
        /// </summary>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetMenuDetailById")]
        public string GetMenuDetailById([FromBody]MenuModel objMenuModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MENUID", objMenuModel.menuid) };
                outPut = objDAL.GetJson(connection, "MSTMENU_S_BYMENUID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }
        #endregion

        #region Manage Sub menu
        /// <summary>
        /// Get Menu List By Menu ID
        /// </summary>
        /// <returns>Menu Name And Status</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetSubMenuList")]
        public string GetSubMenuList([FromBody]MenuModel objMenuModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@MENUID", objMenuModel.menuid) };
                outPut = objDAL.GetJson(connection, "MSTSUBMENU_S_BYMENUID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Sub Menu Detail By Id 
        /// </summary>
        /// <returns></returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetSubMenuDetailById")]
        public string GetSubMenuDetailById([FromBody]MenuModel objMenuModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@SUBMENUID", objMenuModel.submenuid) };
                outPut = objDAL.GetJson(connection, "MSTSUBMENU_S_BYSUBMENUID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Save New Sub Menu 
        /// </summary>
        /// <param name="objMenuModel">MENUID,NAME, PAGEURL,ACTIONSTR ,ICONCLASS ,SUBPAGEURL ,SRNO ,CREATEDBY ,IPADDRESS</param>
        /// <returns>Menu Id with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveSubMenu")]
        public string SaveSubMenu([FromBody]MenuModel objMenuModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@MENUID",objMenuModel.menuid),
                                            new SqlParameter("@NAME",objMenuModel.name),
                                            new SqlParameter("@PAGEURL",objMenuModel.pageurl),
                                            new SqlParameter("@SUBPAGEURL",objMenuModel.subpageurl),
                                            new SqlParameter("@ACTIONSTR",objMenuModel.actionstr),
                                            new SqlParameter("@ICONCLASS",objMenuModel.iconclass),
                                            new SqlParameter("@SRNO",objMenuModel.srno),
                                            new SqlParameter("@CREATEDBY",objMenuModel.createdby),
                                            new SqlParameter("@IPADDRESS",objMenuModel.ipaddress),
                                            new SqlParameter("@RESULTMSG","")
                                        };
                outPut = objDAL.PostWithResultCode(connection, "MSTSUBMENU_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// update Sub Menu By Id
        /// </summary>
        /// <param name="objMenuModel">SUBMENUID,MENUID,NAME, PAGEURL,ACTIONSTR ,ICONCLASS ,SUBPAGEURL ,SRNO ,CREATEDBY ,IPADDRESS</param>
        /// <returns>Menu Id with Success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateSubMenu")]
        public string UpdateSubMenu([FromBody]MenuModel objMenuModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@SUBMENUID",objMenuModel.submenuid),
                                            new SqlParameter("@MENUID",objMenuModel.menuid),
                                            new SqlParameter("@NAME",objMenuModel.name),
                                            new SqlParameter("@PAGEURL",objMenuModel.pageurl),
                                            new SqlParameter("@SUBPAGEURL",objMenuModel.subpageurl),
                                            new SqlParameter("@ACTIONSTR",objMenuModel.actionstr),
                                            new SqlParameter("@ICONCLASS",objMenuModel.iconclass),
                                            new SqlParameter("@SRNO",objMenuModel.srno),
                                            new SqlParameter("@CREATEDBY",objMenuModel.createdby),
                                            new SqlParameter("@IPADDRESS",objMenuModel.ipaddress),
                                            new SqlParameter("@RESULTMSG","")
                                        };
                outPut = objDAL.PostWithResultCode(connection, "MSTSUBMENU_U", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Active and deactive Sub Menu
        /// </summary>
        /// <param name="objMenuModel">SUBMENUID,Active/deactive Value </param>
        /// <returns>Id with success message</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("DeactiveSubMenu")]
        public string DeactiveSubMenu([FromBody]MenuModel objMenuModel)
        {

            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = {
                                            new SqlParameter("@SUBMENUID",objMenuModel.submenuid),
                                            new SqlParameter("@ISACTIVE",objMenuModel.isactive),
                                            new SqlParameter("@CREATEDBY",objMenuModel.createdby),
                                            new SqlParameter("@IPADDRESS",objMenuModel.ipaddress),
                                            new SqlParameter("@RESULTMSG","")
                                        };
                outPut = objDAL.PostWithResultCode(connection, "MSTSUBMENU_D", spparams);
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
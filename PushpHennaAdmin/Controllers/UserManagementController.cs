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
    [Route("api/User")]
    public class UserManagementController : Controller
    {
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;

        public UserManagementController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }

        /// <summary>
        /// Get Menu list with relaed action
        /// </summary>
        /// <param name="objUserMenuModel">UserId,Mode</param>
        /// <returns>List of menu and action parent and child format</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UserMenuPermission")]
        public string UserMenuPermission([FromBody] UserMenuModel objUserMenuModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@USERID", objUserMenuModel.userid),
                                            new SqlParameter("@MODE", objUserMenuModel.mode) };
                outPut = objDAL.GetJson(connection, "USER_MENUACTION_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }


        /// <summary>
        /// Get Menu list with relaed Role action
        /// </summary>
        /// <param name="objUserMenuModel">RoleId</param>
        /// <returns>List of menu and action parent and child format</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UserMenuPermissionWithRole")]
        public string UserMenuPermissionWithRole([FromBody] UserMenuModel objUserMenuModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@ROLEID", objUserMenuModel.roleid),
                                            new SqlParameter("@MODE", objUserMenuModel.mode)
                };
                outPut = objDAL.GetJson(connection, "ROLE_MENUACTION_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UserActionPermission")]
        public string UserActionPermission([FromBody] UserActionModel objUserActionModel)
        {
            string outPut = string.Empty;
            string userActions = JsonConvert.SerializeObject(objUserActionModel.action_detailjson);
            try
            {
                if (!string.IsNullOrEmpty(objUserActionModel.godownidstr))
                {
                    objUserActionModel.godownidstr = objUserActionModel.godownidstr.Replace(objUserActionModel.godownidstr.Substring(0, 1), "");
                    objUserActionModel.godownidstr = objUserActionModel.godownidstr.Replace(objUserActionModel.godownidstr.Substring(objUserActionModel.godownidstr.Length - 1), "");
                }
                if (!string.IsNullOrEmpty(objUserActionModel.storeidstr))
                {
                    objUserActionModel.storeidstr = objUserActionModel.storeidstr.Replace(objUserActionModel.storeidstr.Substring(0, 1), "");
                    objUserActionModel.storeidstr = objUserActionModel.storeidstr.Replace(objUserActionModel.storeidstr.Substring(objUserActionModel.storeidstr.Length - 1), "");
                }
                SqlParameter[] spparams = { new SqlParameter("@USERID", objUserActionModel.userid),
                                            new SqlParameter("@ROLEID", objUserActionModel.roleid),
                                            new SqlParameter("@ACTION_DETAILJSON", userActions),
                                            new SqlParameter("@GODOWNIDSTR", objUserActionModel.godownidstr),
                                            new SqlParameter("@STOREIDSTR", objUserActionModel.storeidstr),
                                            new SqlParameter("@WORKFOR", objUserActionModel.WorkFor),
                                            new SqlParameter("@WORKFORID", objUserActionModel.WorkForID),
                                            new SqlParameter("@ISBIOMATRIC", objUserActionModel.isbiomatric),
                                            new SqlParameter("@ISESIGN", objUserActionModel.isesign),
                                            new SqlParameter("@AADHAAR", objUserActionModel.aadhaar),
                                            new SqlParameter("@CREATEDBY", objUserActionModel.createdby),
                                            new SqlParameter("@IPADDRESS", objUserActionModel.ipaddress),
                                            new SqlParameter("@RESULTMSG", "") };
                outPut = objDAL.PostWithResultCode(connection, "USER_ACTION_I", spparams);
                // update isbiometric and aadhar number on sims app
                RegisterUser objRegisterUser = new RegisterUser();
                objRegisterUser.aadhaar = objUserActionModel.aadhaar;
                objRegisterUser.isbiomatric = objUserActionModel.isbiomatric;
                objRegisterUser.CREATEDBY = objUserActionModel.createdby;
                objRegisterUser.IPADDRESS = objUserActionModel.ipaddress;
                objRegisterUser.ssoid = objUserActionModel.ssoid;
                objRegisterUser.EMAIL = "";
                objRegisterUser.PASSWORD = "";
                UpdateUser(objRegisterUser);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        #region Consume SSO web api and get user detail , Register with us
        /// <summary>
        /// Consume SSO api for get user detail by SSOID and Register with us.
        /// </summary>
        /// <param name="SSOID">SSOID</param>
        /// <returns>UserId with success message</returns>          
        [HttpGet]
        [Route("UserDetails")]
        public async Task<SSOUser> UserDetails(string SSOID)
        {
            SSOUser ssoInfo = new SSOUser();
            SSOUserResponse objServiceResponse = new SSOUserResponse();
            string SSOId = string.Empty;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(_config["SSOUserDetail"].ToString()); //("http://ssotest.rajasthan.gov.in:8888/");
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("/SSOREST/GetUserDetailJSON/" + SSOID + "/" + _config["SSOUserName"] + "/" + _config["SSOPassword"]);
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    objServiceResponse = JsonConvert.DeserializeObject<SSOUserResponse>(EmpResponse);
                    if (objServiceResponse != null)
                    {
                        ssoInfo.SSOID = objServiceResponse.SSOID;
                        ssoInfo.DISPLAYNAME = objServiceResponse.displayName;
                        ssoInfo.EMAIL = objServiceResponse.mailPersonal;
                        ssoInfo.MOBILE = objServiceResponse.mobile;
                        ssoInfo.AADHAAR = string.IsNullOrEmpty(objServiceResponse.aadhaarId) ? "": objServiceResponse.aadhaarId;
                    }
                } 
                //returning the employee list to view  
                return ssoInfo;
            }
        }
        #endregion

        #region Register user with email 
        /// <summary>
        /// Register user  without email id 
        /// </summary>
        /// <param name="objRegisterUser"></param>
        /// <returns> userid with success msg</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveUserData")]
        public JsonResult SaveUserData([FromBody] RegisterUser objRegisterUser)
        {
            ReponseModel objReponseModel = new ReponseModel();
            string outPut = string.Empty;
            string resultAPI = string.Empty;
            try
            {
             
                        SqlParameter[] spparams = {
                                             
                                                    new SqlParameter("@NAME",objRegisterUser.NAME),
                                                    new SqlParameter("@PASSWORD",objRegisterUser.PASSWORD),
                                                    new SqlParameter("@MOBILE",objRegisterUser.MOBILE),
                                                    new SqlParameter("@PHONE",objRegisterUser.PHONE),
                                                    new SqlParameter("@EMAIL",objRegisterUser.EMAIL),
                                                    new SqlParameter("@ADDRESS",objRegisterUser.ADDRESS),
                                                    new SqlParameter("@PINCODE",objRegisterUser.PINCODE),
                                                    new SqlParameter("@STATEID",objRegisterUser.STATEID),
                                                    new SqlParameter("@CITYID",objRegisterUser.CITYID),
                                                    new SqlParameter("@DISTRICT_ID",objRegisterUser.DISTRICT_ID),
                                                    new SqlParameter("@ROLEID",objRegisterUser.ROLEID),
                                                    new SqlParameter("@WORKFOR",objRegisterUser.WORKFOR),
                                                    new SqlParameter("@WORKFORID",objRegisterUser.WORKFORID),
                                                    new SqlParameter("@USERTYPEID",objRegisterUser.USERTYPEID),
                                                    new SqlParameter("@CREATEDBY",objRegisterUser.CREATEDBY),
                                                    new SqlParameter("@IPADDRESS",objRegisterUser.IPADDRESS),
                                                    new SqlParameter("@RESULTMSG","")
                                                };
                        outPut = objDAL.PostWithResultCode(connection, "USER_I", spparams);
                        return Json(outPut);
   
            }
            catch (Exception ex)
            {
                objReponseModel.USERID = 0;
                objReponseModel.MSG = ex.Message;
            }
            return Json(objReponseModel);
        }
        #endregion
        /// <summary>
        /// Register user  without email id 
        /// </summary>
        /// <param name="objRegisterUser"></param>
        /// <returns> userid with success msg</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UpdateUserData")]
        public JsonResult UpdateUserData([FromBody] RegisterUser objRegisterUser)
        {
            ReponseModel objReponseModel = new ReponseModel();
            string outPut = string.Empty;
            string resultAPI = string.Empty;
            try
            {

                SqlParameter[] spparams = {
                                                    new SqlParameter("@USERID",objRegisterUser.USERID),
                                                    new SqlParameter("@NAME",objRegisterUser.NAME),
                                                    new SqlParameter("@PASSWORD",objRegisterUser.PASSWORD),
                                                    new SqlParameter("@MOBILE",objRegisterUser.MOBILE),
                                                    new SqlParameter("@PHONE",objRegisterUser.PHONE),
                                                    new SqlParameter("@EMAIL",objRegisterUser.EMAIL),
                                                    new SqlParameter("@ADDRESS",objRegisterUser.ADDRESS),
                                                    new SqlParameter("@PINCODE",objRegisterUser.PINCODE),
                                                    new SqlParameter("@STATEID",objRegisterUser.STATEID),
                                                    new SqlParameter("@CITYID",objRegisterUser.CITYID),
                                                    new SqlParameter("@DISTRICT_ID",objRegisterUser.DISTRICT_ID),
                                                    new SqlParameter("@ROLEID",objRegisterUser.ROLEID),
                                                    new SqlParameter("@WORKFOR",objRegisterUser.WORKFOR),
                                                    new SqlParameter("@WORKFORID",objRegisterUser.WORKFORID),
                                                    new SqlParameter("@USERTYPEID",objRegisterUser.USERTYPEID),
                                                    new SqlParameter("@CREATEDBY",objRegisterUser.CREATEDBY),
                                                    new SqlParameter("@IPADDRESS",objRegisterUser.IPADDRESS),
                                                    new SqlParameter("@RESULTMSG","")
                                                };
                outPut = objDAL.PostWithResultCode(connection, "USER_I", spparams);
                return Json(outPut);

            }
            catch (Exception ex)
            {
                objReponseModel.USERID = 0;
                objReponseModel.MSG = ex.Message;
            }
            return Json(objReponseModel);
        }

        public string CheckUser(RegisterUser objRegisterUser)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_config["SIMSAPI"].ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string objVM = JsonConvert.SerializeObject(new
                    {
                        email = objRegisterUser.EMAIL,
                        password = objRegisterUser.PASSWORD,
                        ssoid = objRegisterUser.ssoid,                       
                        applicationid = 1003, // 3 For MEDICAL on Live And 4 For Medical on Staging and Local
                        IsBiomatric = objRegisterUser.isbiomatric,
                        createdby = objRegisterUser.CREATEDBY,
                        ipaddress = objRegisterUser.IPADDRESS,
                        aadhaar = objRegisterUser.aadhaar
                    });

                    streamWriter.Write(objVM);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    //
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {

                        var result = streamReader.ReadToEnd();
                        var response = JsonConvert.DeserializeObject<SSOModel>(result);
                        if (!string.IsNullOrEmpty(response.result))
                            return response.result;
                        else
                            return response.result;
                    }
                }
            }
            catch (Exception ex)
            {
                var logPath = System.IO.Path.GetTempFileName();
                using (var writer = System.IO.File.CreateText(Directory.GetCurrentDirectory() + "/log.txt"))
                {
                    writer.WriteLine(ex.InnerException); //or .Write(), if you wish
                }
                return "0";
            }
        }

        public string UpdateUser(RegisterUser objRegisterUser)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_config["SIMSAPIUPDATE"].ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string objVM = JsonConvert.SerializeObject(new
                    {
                        email = objRegisterUser.EMAIL,
                        password = objRegisterUser.PASSWORD,
                        ssoid = objRegisterUser.ssoid,
                        applicationid = 3, // 3 For MEDICAL on Live And 4 For Medical on Staging and Local
                        IsBiomatric = objRegisterUser.isbiomatric,
                        createdby = objRegisterUser.CREATEDBY,
                        ipaddress = objRegisterUser.IPADDRESS,
                        aadhaar = objRegisterUser.aadhaar
                    });

                    streamWriter.Write(objVM);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {

                        var result = streamReader.ReadToEnd();
                        var response = JsonConvert.DeserializeObject<SSOModel>(result);
                        if (!string.IsNullOrEmpty(response.result))
                            return response.result;
                        else
                            return response.result;
                    }
                }
            }
            catch (Exception ex)
            {
                var logPath = System.IO.Path.GetTempFileName();
                using (var writer = System.IO.File.CreateText(Directory.GetCurrentDirectory() + "/log.txt"))
                {
                    writer.WriteLine(ex.InnerException); //or .Write(), if you wish
                }
                return "0";
            }
        }

        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetUserDetailById/{Id}")]
        public string GetUserDetailById(string Id)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@CREATEDBY ", Id) };
                outPut = objDAL.GetJson(connection, "USERS_S_BYID", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Role List 
        /// </summary>
        /// <returns>List of Master Role with ID and Name</returns>
        [HttpGet, Authorize(Policy = "JwtAuth")]
        [Route("GetRoleMaster")]
        public string GetRoleMaster()
        {
            string outPut = string.Empty;
            try
            {
                outPut = objDAL.GetJson(connection, "MSTROLE_S_LIST");
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Get Menu list with Role
        /// </summary>
        /// <param name="objUserMenuModel">RoleId</param>
        /// <returns>List of menu and action parent and child format</returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("GetRoleWiseMenuPermissionWithRole")]
        public string GetRoleWiseMenuPermissionWithRole([FromBody] UserMenuModel objUserMenuModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@ROLEID", objUserMenuModel.roleid),
                                            new SqlParameter("@MODE", objUserMenuModel.mode)
                };
                outPut = objDAL.GetJson(connection, "ROLE_MENUACTION_S", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("SaveRoleActionPermission")]
        public string SaveRoleActionPermission([FromBody] UserActionModel objUserActionModel)
        {
            string outPut = string.Empty;
            string userActions = JsonConvert.SerializeObject(objUserActionModel.action_detailjson);
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@ROLEID", objUserActionModel.roleid),
                                            new SqlParameter("@OFFICETIME_FROM", objUserActionModel.workfromtime),
                                            new SqlParameter("@OFFICETIME_TO", objUserActionModel.worktotime),
                                            new SqlParameter("@ACTION_DETAILJSON", userActions),
                                            new SqlParameter("@CREATEDBY", objUserActionModel.createdby),
                                            new SqlParameter("@IPADDRESS", objUserActionModel.ipaddress),
                                            new SqlParameter("@RESULTMSG", "") };
                outPut = objDAL.PostWithResultCode(connection, "ROLE_ACTION_I", spparams);
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return outPut;
        }

        /// <summary>
        /// Update user status (active =1 and deactive =0)
        /// </summary>
        /// <param name="objStoreModel">active/deactive and store id</param>
        /// <returns>status </returns>
        [HttpPost, Authorize(Policy = "JwtAuth")]
        [Route("UserActiveDeactiveById")]
        public string UserActiveDeactiveById([FromBody] UserActiveModel objUserActiveModel)
        {
            string outPut = string.Empty;
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@ID ", objUserActiveModel.Id),
                                            new SqlParameter("@CREATEDBY",objUserActiveModel.CreatedBy),
                                            new SqlParameter("@ISACTIVE",objUserActiveModel.IsActive),
                                            new SqlParameter("@IPADDRESS",objUserActiveModel.IpAddress),
                                            new SqlParameter("@RESULTMSG","")};
                outPut = objDAL.PostWithResultCode(connection, "USERS_U_ISACTIVE", spparams);
               
            }
            catch (Exception ex)
            {
                outPut = ex.Message;
                //log.Error(outPut);
            }
            return outPut;
        }
                 
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PushpHennaAdmin.Model;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Filter;
using PushpHennaAdmin.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;

namespace PushpHennaAdmin.Controllers
{
    [Route("api/Login")]
    public class UserLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        DAL objDAL = DAL.GetObject();
        private IConfiguration _config;
        string connection = string.Empty;

        public UserLoginController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "API is working", "Done" };
        }

        #region Check Login and create JWT TOKEN
        /// <summary>
        /// Authentication of User by username and password 
        /// </summary>
        /// <param name="login">Username and password</param>
        /// <returns>Userdetails-UserId,UserName,Roles,Mobile,EmailId with JWT Token Number</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("UserLogin")]
        public LoginResponseMessage CreateToken([FromBody]Login login)
        {
            LoginResponseMessage objLoginResponseMessage = new LoginResponseMessage();
            try
            {
                var user = Authenticate(login);
                if (user.EMAIL != null)
                {
                    var tokenString = JwtManager.GenerateToken(user, _config["Jwt:JwtKey"], _config["Jwt:Issuer"]); //BuildToken(user);
                    if (!string.IsNullOrEmpty(tokenString))
                    {
                        objLoginResponseMessage.TokenNumber = tokenString;
                        objLoginResponseMessage.UserId = user.ID;
                        objLoginResponseMessage.UserName = user.NAME;
                        objLoginResponseMessage.Roles = user.ROLES;
                        objLoginResponseMessage.Mobile = user.MOBILE;
                        objLoginResponseMessage.EmailId = user.EMAIL;
                        objLoginResponseMessage.GodownIdStr = user.GODOWNIDSTR;
                        objLoginResponseMessage.StoreIdStr = user.STOREIDSTR;
                        objLoginResponseMessage.WorkFor = user.WORKFOR;
                        objLoginResponseMessage.WorkForID = user.WORKFORID;
                        objLoginResponseMessage.Menu = user.MENU;
                        objLoginResponseMessage.Notification = user.NOTIFICATION;
                        objLoginResponseMessage.MaintenanceMsg = user.MAINTENANCEMSG;
                        objLoginResponseMessage.WORKFORNAME = user.WORKFORNAME;
                        objLoginResponseMessage.ISESIGN = user.ISESIGN;
                        objLoginResponseMessage.AADHAAR = user.AADHAAR;

                        objLoginResponseMessage.Message = new TransactionMessage() { message = "Success", status = TransactionMessage.MessageState.Success };
                    }
                    else
                        objLoginResponseMessage.Message = new TransactionMessage() { message = "NotFound", status = TransactionMessage.MessageState.NotFound };

                }
                else
                    objLoginResponseMessage.Message = new TransactionMessage() { message = "NotFound", status = TransactionMessage.MessageState.NotFound };
            }
            catch (Exception ex)
            {
                objLoginResponseMessage.Message = new TransactionMessage() { message = ex.Message.ToString(), status = TransactionMessage.MessageState.Error };
            }
            return objLoginResponseMessage;
        }
        /// <summary>
        /// Method use for handle user login check user exists and not / check password for authentication
        /// </summary>
        /// <param name="login">user name and password  </param>
        /// <returns> return- Id,UserName,Roles,Mobile,EmailId</returns>
        private UserModel Authenticate(Login login)
        {
            UserModel user = new UserModel();
            if (!string.IsNullOrEmpty(login.Username) && !string.IsNullOrEmpty(login.Password))
            {
                SqlParameter[] spparams = { new SqlParameter("@EMAIL", login.Username), new SqlParameter("@PASSWORD", login.Password) };
                DataSet ds = objDAL.Get(connection, "USER_LOGIN", spparams);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        user.ID = dt.Rows[0]["ID"].ToString();
                        user.NAME = dt.Rows[0]["NAME"].ToString();
                        user.MOBILE = dt.Rows[0]["MOBILE"].ToString();
                        user.EMAIL = dt.Rows[0]["EMAIL"].ToString();
                        user.ROLES = dt.Rows[0]["ROLES"].ToString();
                        user.GODOWNIDSTR = dt.Rows[0]["GODOWNIDSTR"].ToString();
                        user.STOREIDSTR = dt.Rows[0]["STOREIDSTR"].ToString();
                        user.MENU = dt.Rows[0]["MENU"].ToString();
                        user.NOTIFICATION = dt.Rows[0]["NOTIFICATION"].ToString();
                        user.MAINTENANCEMSG = dt.Rows[0]["MAINTENANCEMSG"].ToString();
                        user.WORKFOR = dt.Rows[0]["WORKFOR"].ToString();
                        user.WORKFORID = dt.Rows[0]["WORKFORID"].ToString();
                        user.WORKFORNAME = dt.Rows[0]["WORKFORNAME"].ToString();
                        user.ISESIGN = Convert.ToBoolean(dt.Rows[0]["ISESIGN"]);
                        user.AADHAAR = dt.Rows[0]["AADHAAR"].ToString();
                    }
                }
                else
                    return user;
            }
            return user;
        }
        #endregion

        [HttpGet]
        [Route("UnauthorizedError")]
        public string UnauthorizedError()
        {
            return "{\"Status\":401,\"Message\":\"Unauthorized Request is not found.\",\"Error\":\"Unauthorized Request is not found.\"}";
        }

       
    }
}
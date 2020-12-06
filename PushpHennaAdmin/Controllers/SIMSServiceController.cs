using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Filter;
using PushpHennaAdmin.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Controllers
{
    [Route("api/SIMSService")]
    public class SIMSServiceController : Controller
    {
        DAL objDAL = DAL.GetObject(); //new DAL();
        private IConfiguration _config;
        string connection = string.Empty;
        //  ILoggerFactory _loggerFactory;

        public SIMSServiceController(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
            // _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Verify Token Number With SSO API & Login on Application
        /// </summary>
        /// <param name="objToken">Token Numebr</param>
        /// <returns>JWT Token, UserId, UserName,Roles ,Mobile , EmailId, ssoid, GodownIdStr,StoreIdStr , Menu,Notification</returns>
        [HttpPost("[action]")]
        public async Task<LoginResponseMessage> getSSOid([FromBody] SSOModel objToken)
        {
            ServiceResponse ssoInfo = new ServiceResponse();
            LoginResponseMessage objLoginResponseMessage = new LoginResponseMessage();
            string SSOId = string.Empty;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(_config["SSOurl"].ToString()); //("http://ssotest.rajasthan.gov.in:8888/");

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("SSOREST/GetTokenDetailJSON/" + objToken.Token);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    ssoInfo = JsonConvert.DeserializeObject<ServiceResponse>(EmpResponse);
                    if (!string.IsNullOrEmpty(ssoInfo.sAMAccountName))
                    {
                        UserModel objUserModel = new UserModel();
                        try
                        {
                            var user = SsoAuthenticate(ssoInfo.sAMAccountName);
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
                                    objLoginResponseMessage.ssoid = ssoInfo.sAMAccountName;
                                    objLoginResponseMessage.GodownIdStr = user.GODOWNIDSTR;
                                    objLoginResponseMessage.StoreIdStr = user.STOREIDSTR;
                                    objLoginResponseMessage.WorkFor = user.WORKFOR;
                                    objLoginResponseMessage.WorkForID = user.WORKFORID;
                                    objLoginResponseMessage.Menu = user.MENU;
                                    objLoginResponseMessage.Notification = user.NOTIFICATION;
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
                    }
                    else
                        objLoginResponseMessage.Message = new TransactionMessage() { message = "Invalid Token", status = TransactionMessage.MessageState.NotFound };
                }
                //returning the employee list to view  
                return objLoginResponseMessage;

            }
        }
        /// <summary>
        /// Validate SSOID Exists and not
        /// </summary>
        /// <param name="SsoId">SsoId</param>
        /// <returns> UserId, UserName,Roles ,Mobile , EmailId, ssoid, GodownIdStr,StoreIdStr , Menu,Notification</returns>
        private UserModel SsoAuthenticate(string SsoId)
        {
            UserModel user = new UserModel();

            if (!string.IsNullOrEmpty(SsoId))
            {
                SqlParameter[] spparams = { new SqlParameter("@SSOID", SsoId) };
                DataSet ds = objDAL.Get(connection, "USER_SSO_LOGIN", spparams);
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



        #region  Execute sql script file in dataBase
        /// <summary>
        /// Use for execute data base query and script 
        /// </summary>
        /// <param name="objSqlScriptModel">user name,password for authentication and script , connection string</param>
        [HttpPost]
        [Route("ExecuteScript")]
        public string ExecuteScript([FromBody] SqlScriptModel objSqlScriptModel)
        {
            string Response = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(objSqlScriptModel.username) && !string.IsNullOrEmpty(objSqlScriptModel.password) && !string.IsNullOrEmpty(objSqlScriptModel.script) && !string.IsNullOrEmpty(objSqlScriptModel.connection))
                {
                    SqlParameter[] spparams = { new SqlParameter("@USERNAME", objSqlScriptModel.username), new SqlParameter("@PASSWORD", objSqlScriptModel.password) };
                    DataSet ds = objDAL.Get(connection, "TBL_APIUSER_S", spparams);
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            objSqlScriptModel.script = objSqlScriptModel.script.Replace("$$$", "\"");
                            Response = objDAL.ExecuteDbScript(objSqlScriptModel.connection, objSqlScriptModel.script, objSqlScriptModel.scriptType, objSqlScriptModel.ReturnType);
                        }
                    }
                }
                else
                {
                    Response = "Invalid username or password";
                }

            }
            catch (SqlException ex)
            {
                Response = ex.Message;
            }
            return Response;
        }
        #endregion
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PushpHennaAdmin.DataBase;
using PushpHennaAdmin.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Filter
{
    public class ValidateTokenFilter
    {
    }
    #region JWT Implementation
    public class JwtManager
    {
        public static string GenerateToken(UserModel user, string Secretkey, string Issuer)
        {
            var symmetricKey = Convert.FromBase64String(Secretkey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, user.ID),new Claim(ClaimTypes.Actor,Issuer)    //new Claim(ClaimTypes.Role,user.MENU),
                        }),
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(stoken);
        }
    }
    public class JWTAuthorizationHandler : AuthorizationHandler<JWTREQUIREMENT>
    {
        private IConfiguration _config;
        static string connection = string.Empty;
        public JWTAuthorizationHandler(IConfiguration config)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
            // _loggerFactory = loggerFactory;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JWTREQUIREMENT requirement)
        {
            var mvcContext = context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            if (requirement.JWTAUTHALLOW == "YES")
            {
                mvcContext.HttpContext.Response.StatusCode = 404;
                if (context.Resource is Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext Rsource)
                {
                    string authHeader = Rsource.HttpContext.Request.Headers["Authorization"];
                    if (authHeader != null && authHeader.StartsWith("Bearer"))
                    {
                        string JwtToken = authHeader.Substring("Bearer ".Length).Trim();
                        string ActionUrl = Rsource.HttpContext.Request.Path.Value;
                        var principal = AuthenticateJwtToken(JwtToken, _config["Jwt:JwtKey"], ActionUrl);
                        if (principal.Result != null)
                        {
                            mvcContext.HttpContext.Response.StatusCode = 200;
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }
                        else
                        {
                            mvcContext.HttpContext.Response.StatusCode = 200;
                            mvcContext.HttpContext.Response.Redirect("/API/Login/UnauthorizedError");
                            //   RedirectResult redirectResult = new RedirectResult(_config["SSOlogin"].ToString());


                        }

                    }
                    mvcContext.HttpContext.Response.StatusCode = 404;
                    return Task.CompletedTask;
                }
                else
                {

                    mvcContext.HttpContext.Response.StatusCode = 200;
                    mvcContext.HttpContext.Response.Redirect("/API/Login/UnauthorizedError");
                    //context.Fail();
                    return Task.CompletedTask;
                }
            }
            else
            {
                //context.Fail();
                mvcContext.HttpContext.Response.Redirect("/API/Login/UnauthorizedError");
                return Task.FromResult(0);
            }
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token, string Secretkey, string ActionUrl)
        {
            string username, userrole, rFrom;

            if (ValidateToken(token, Secretkey, ActionUrl, out username, out userrole, out rFrom))
            {
                // based on username to get more information from database in order to build local identity
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Actor, rFrom),
                    //new Claim(ClaimTypes.Role, userrole)
                    // Add more claims if needed: Roles, ...
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }
        private static bool ValidateToken(string token, string Secretkey, string ActionUrl, out string username, out string userrole, out string rFrom)
        {
            rFrom = userrole = username = null;

            var simplePrinciple = GetPrincipal(token, Secretkey);
            var identity = (simplePrinciple == null) ? null : simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;
            var userRoleClaim = identity.FindFirst(ClaimTypes.Role);
            userrole = userRoleClaim?.Value;
            var userActorClaim = identity.FindFirst(ClaimTypes.Actor);
            rFrom = userActorClaim?.Value;



            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(rFrom))//|| string.IsNullOrEmpty(userrole)
                return false;


            //if (ActionUrl.Split('/').Count() > 0)
            //{
            //    string baseURL = ActionUrl.Split('/').GetValue(2).ToString() + "/" + ActionUrl.Split('/').GetValue(3).ToString();
            //    DAL objDAL = DAL.GetObject(); //new DAL();
            //    SqlParameter[] spparams = { new SqlParameter("@USERID", username), new SqlParameter("@API_URL", baseURL) };
            //    string outPut = objDAL.GetJson(connection, "MSTAPI_ACTION_AUTH", spparams);
            //    return outPut == "True" ? true : false;
            //}

            return true;
        }
        /// <summary>
        /// check in Data base 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ClaimsPrincipal GetPrincipal(string token, string Secretkey)
        {
            try
            {
                // eBazaarAPI.Repository.HomeRepository hmrep = new Repository.HomeRepository();
                // Token tkn = hmrep.GetSecretKeyByToken(token);
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)// || IMEINO != tkn.IMEINUMBER)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secretkey);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class JWTREQUIREMENT : IAuthorizationRequirement
    {
        public JWTREQUIREMENT(string JWTALLOW)
        {
            JWTAUTHALLOW = JWTALLOW;
        }

        public string JWTAUTHALLOW { get; private set; }
    }

    #endregion
}

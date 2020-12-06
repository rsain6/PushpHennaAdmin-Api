using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class TransactionMessage
    {
        public enum MessageState
        {
            Success = 200,
            Created = 201,
            Accepted = 202,
            NoContent = 204,
            BadRequest = 400,
            Unauthorized = 401,
            Forbidden = 403,
            NotFound = 404,
            Conflict = 409,
            Error = 500,
            ServiceUnavailable = 503,
        }
        public MessageState status { get; set; }
        public string message { get; set; }
    }

    public class ResponseMessage1
    {
        public TransactionMessage Message { get; set; }
        public JObject ResultData { get; set; }
    }
    public class ResponseMessage
    {
        public string ResultData { get; set; }
    }
    #region Model use For manage SSO Authentication & Provide Application SSO
    public class LoginResponseMessage
    {
        public TransactionMessage Message { get; set; }
        public string TokenNumber { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Roles { get; set; }
        public string GodownIdStr { get; set; }
        public string StoreIdStr { get; set; }
        public string WorkFor { get; set; }
        public string WorkForID { get; set; }
        public string Menu { get; set; }
        public string ssoid { get; set; }
        public string WORKFORNAME { get; set; }
        public string Notification { get; set; }
        public string MaintenanceMsg { get; set; }
        public bool ISESIGN { get; set; }
        public string AADHAAR { get; set; }
    }
    #endregion
}

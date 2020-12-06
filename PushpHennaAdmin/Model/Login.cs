using System.Collections.Generic;

namespace PushpHennaAdmin.Model
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserModel
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string ROLES { get; set; }
        public string GODOWNIDSTR { get; set; }
        public string STOREIDSTR { get; set; }
        public string MENU { get; set; }
        public string NOTIFICATION { get; set; }
        public string MAINTENANCEMSG { get; set; }
        public string WORKFOR { get; set; }
        public string WORKFORID { get; set; }
        public string WORKFORNAME { get; set; }
        public bool ISESIGN { get; set; }
        public string AADHAAR { get; set; }
    }

    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public bool AgeRestriction { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string AGE { get; set; }
    }

    public class SSOModel
    {
        public string Token { get; set; }
        public string SSOID { get; set; }
        public string result { get; set; }
        public string ipaddress { get; set; }
    }
    public class ServiceResponse
    {
        public string sAMAccountName { get; set; }
        public List<string> Roles { get; set; }
    }
    public class SqlScriptModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string script { get; set; }
        public string connection { get; set; }
        public int scriptType { get; set; }
        public int ReturnType { get; set; }
    }
    public class SSOUser
    {
        public string SSOID { get; set; }
        public string DISPLAYNAME { get; set; }
        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public string AADHAAR { get; set; }

    }
    public class SSOUserResponse
    {
        public string SSOID { get; set; }
        public string aadhaarId { get; set; }
        public string bhamashahId { get; set; }
        public string bhamashahMemberId { get; set; }
        public string displayName { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string mobile { get; set; }
        public string telephoneNumber { get; set; }
        public string mailPersonal { get; set; }
        public string postalAddress { get; set; }
        public string postalCode { get; set; }
    }
    public class RegisterUser
    {
        public int USERID { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
        public string MOBILE { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string PINCODE { get; set; }
        public string ADDRESS { get; set; }
        public string STATEID { get; set; }
        public string CITYID { get; set; }
        public string DISTRICT_ID { get; set; }
        public string ROLEID { get; set; }
        public string WORKFOR { get; set; }
        public string WORKFORID { get; set; }
        public string USERTYPEID { get; set; }
        public string mode { get; set; }
        public string ssoid { get; set; }
        public int applicationid { get; set; }
        public string CREATEDBY { get; set; }
        public string IPADDRESS { get; set; }
        public string username { get; set; }
        public bool isbiomatric { get; set; }
        public string aadhaar { get; set; }
        public bool isactive { get; set; }
        public int godownid { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
    }
    public class UserMenuModel
    {
        public string userid { get; set; }
        public int roleid { get; set; }
        public string mode { get; set; }

    }
    public class UserActionModel
    {
        public int userid { get; set; }
        public int roleid { get; set; }
        public List<ActionModel> action_detailjson { get; set; }
        public string godownidstr { get; set; }
        public string storeidstr { get; set; }
        public string WorkFor { get; set; }
        public string WorkForID { get; set; }
        public string createdby { get; set; }
        public string ipaddress { get; set; }
        public string resultmsg { get; set; }
        public bool isbiomatric { get; set; }
        public bool isesign { get; set; }
        public string aadhaar { get; set; }
        public string ssoid { get; set; }
        public string workfromtime { get; set; }
        public string worktotime { get; set; }
    }
    public class ActionModel
    {
        public int MENUID { get; set; }
        public int SUBMENUID { get; set; }
        public string ACTIONIDSTR { get; set; }
        public string TYPE { get; set; }
    }
    public class TokenClassModel
    {
        public string tokennumber { get; set; }
    }

    public class DashBoard
    {
        public int userid { get; set; }
        public string mode { get; set; }
        public string godownidstr { get; set; }
        public int? notification_id { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
    public class ReponseModel
    {
        public int USERID { get; set; }
        public string MSG { get; set; }
    }
    public class SIMSUserUpdate
    {
        public int? SSOResult { get; set; }
    }



    public class UserActiveModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string IpAddress { get; set; }
        public string ResultMsg { get; set; }
    }
}

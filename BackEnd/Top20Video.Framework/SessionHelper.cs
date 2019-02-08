using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Web.Security;
//using Top20Video.Data;
using System.Threading;
using System.Globalization;
using Top20Video.Data;

namespace Top20Video.Framework
{
    public static class SessionHelper
    {
        /// <summary>
        /// to get User id from session
        /// </summary>
        public static long UserId
        {
            get
            {
                return HttpContext.Current.Session["UserId"] != null ? Convert.ToInt64(HttpContext.Current.Session["UserId"]) : 0;
            }
            set { HttpContext.Current.Session["UserId"] = value; }
        }

        /// <summary>
        /// to get User id from session
        /// </summary>
        public static int RoleID
        {
            get
            {
                return HttpContext.Current.Session["RoleID"] != null ? Convert.ToInt32(HttpContext.Current.Session["RoleID"]) : 0;
            }
            set { HttpContext.Current.Session["RoleID"] = value; }
        }


        /// <summary>
        /// to get number of companies
        /// </summary>
        public static int NumberOfCompanies
        {
            get
            {
                return HttpContext.Current.Session["NumberOfCompanies"] != null ? Convert.ToInt32(HttpContext.Current.Session["NumberOfCompanies"]) : 0;
            }
            set { HttpContext.Current.Session["NumberOfCompanies"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string UserRole
        {
            get
            {
                return HttpContext.Current.Session["UserRole"] != null ? Convert.ToString(HttpContext.Current.Session["UserRole"]) : "";
            }
            set { HttpContext.Current.Session["UserRole"] = value; }
        }


        /// <summary>
        /// to get Company id from session
        /// </summary>
        public static int CompanyId
        {
            get
            {
                return HttpContext.Current.Session["CompanyId"] != null ? Convert.ToInt32(HttpContext.Current.Session["CompanyId"]) : 0;
            }
            set { HttpContext.Current.Session["CompanyId"] = value; }
        }
        /// <summary>
        /// to get Company id from session
        /// </summary>
        public static string PaypalCompanyId
        {
            get
            {
                return HttpContext.Current.Session["CompanyId"] != null ? Convert.ToString(HttpContext.Current.Session["CompanyId"]) : "";
            }
            set { HttpContext.Current.Session["CompanyId"] = value; }
        }
        /// <summary>
        /// to check is user login or not
        /// </summary>
        public static bool IsUserLogin
        {
            get
            {
                Initialize();
                return UserId > 0;
            }
        }

        /// <summary>
        /// to get User name from session
        /// </summary>
        public static string UserName
        {
            get
            {
                return HttpContext.Current.Session["UserName"] != null ? Convert.ToString(HttpContext.Current.Session["UserName"]) : "";
            }
            set { HttpContext.Current.Session["UserName"] = value; }
        }

        /// <summary>
        /// to get Company id from session
        /// </summary>
        public static int AssignUserId
        {
            get
            {
                return HttpContext.Current.Session["AssignUserId"] != null ? Convert.ToInt32(HttpContext.Current.Session["AssignUserId"]) : 0;
            }
            set { HttpContext.Current.Session["AssignUserId"] = value; }
        }


        /// <summary>
        /// to get user profile picture 
        /// </summary>
        public static string UserProfilePic
        {
            get
            {
                return HttpContext.Current.Session["UserProfilePic"] != null ? Convert.ToString(HttpContext.Current.Session["UserProfilePic"]) : "";
            }
            set { HttpContext.Current.Session["UserProfilePic"] = value; }
        }

        /// <summary>
        /// to get User default language id from session
        /// </summary>
        public static int UserLanguageID
        {
            get
            {
                return HttpContext.Current.Session["UserLanguageID"] != null ? Convert.ToInt32(HttpContext.Current.Session["UserLanguageID"]) : Convert.ToInt32(ConfigurationSettings.AppSettings["DefaultLanguageID"]);
            }
            set { HttpContext.Current.Session["UserLanguageID"] = value; }
        }

        /// <summary>
        /// to get User group id from session
        /// </summary>
        public static int UserGroupID
        {
            get
            {
                return HttpContext.Current.Session["UserGroupID"] != null ? Convert.ToInt32(HttpContext.Current.Session["UserGroupID"]) : 0;
            }
            set { HttpContext.Current.Session["UserGroupID"] = value; }
        }

        /// <summary>
        /// to get User group name from session
        /// </summary>
        public static string UserGroupName
        {
            get
            {
                return HttpContext.Current.Session["GroupName"] != null ? Convert.ToString(HttpContext.Current.Session["GroupName"]) : "";
            }
            set { HttpContext.Current.Session["GroupName"] = value; }
        }

        /// <summary>
        /// to get message
        /// </summary>
        public static int MessageCount
        {
            get
            {
                return HttpContext.Current.Session["MessageCount"] != null ? Convert.ToInt32(HttpContext.Current.Session["MessageCount"]) : 0;
            }
            set { HttpContext.Current.Session["MessageCount"] = value; }
        }

        /// <summary>
        /// to check is super admin
        /// </summary>
        public static bool IsSuperAdmin
        {
            get
            {
                return HttpContext.Current.Session["IsSuperAdmin"] != null ? Convert.ToBoolean(HttpContext.Current.Session["IsSuperAdmin"]) : false;
            }
            set { HttpContext.Current.Session["IsSuperAdmin"] = value; }
        }


        /// <summary>
        /// to check is super admin
        /// </summary>
        public static bool IsValidLicense
        {
            get
            {
                return HttpContext.Current.Session["IsValidLicense"] != null ? Convert.ToBoolean(HttpContext.Current.Session["IsValidLicense"]) : false;
            }
            set { HttpContext.Current.Session["IsValidLicense"] = value; }
        }

        /// <summary>
        /// to check is super admin
        /// </summary>
        public static bool IsAdminUser
        {
            get
            {
                return HttpContext.Current.Session["IsAdminUser"] != null ? Convert.ToBoolean(HttpContext.Current.Session["IsAdminUser"]) : false;
            }
            set { HttpContext.Current.Session["IsAdminUser"] = value; }
        }

        /// <summary>
        /// to check is admin user
        /// </summary>
        public static bool IsAdmin
        {
            get
            {
                return HttpContext.Current.Session["IsAdmin"] != null ? Convert.ToBoolean(HttpContext.Current.Session["IsAdmin"]) : false;
            }
            set { HttpContext.Current.Session["IsAdmin"] = value; }
        }

        /// <summary>
        /// to check is admin user
        /// </summary>
        public static bool IsCompanyAdmin
        {
            get
            {
                return HttpContext.Current.Session["IsCompanyAdmin"] != null ? Convert.ToBoolean(HttpContext.Current.Session["IsCompanyAdmin"]) : false;
            }
            set { HttpContext.Current.Session["IsCompanyAdmin"] = value; }
        }

        /// <summary>
        /// to check is admin user
        /// </summary>
        public static bool ReportRecord
        {
            get
            {
                return HttpContext.Current.Session["ReportRecord"] != null ? Convert.ToBoolean(HttpContext.Current.Session["ReportRecord"]) : false;
            }
            set { HttpContext.Current.Session["ReportRecord"] = value; }
        }

        /// <summary>
        /// to Get or Set user login detail from/into cookie
        /// </summary>
        public static string UserCookie
        {
            get
            {
                return HttpContext.Current.Request.Cookies["fuid"] != null ? HttpContext.Current.Request.Cookies["fuid"].Value.ToString() : "0".ToEnctypt();
            }
            set
            {
                HttpCookie uc = new HttpCookie("fuid", value);
                uc.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(uc);
            }
        }

        /// <summary>
        /// to get Admin User id from session
        /// </summary>
        public static long AdminUserId
        {
            get
            {
                return HttpContext.Current.Session["AdminUserId"] != null ? Convert.ToInt64(HttpContext.Current.Session["AdminUserId"]) : 0;
            }
            set { HttpContext.Current.Session["AdminUserId"] = value; }
        }

        /// <summary>
        /// to check Admin user logged in or not
        /// </summary>
        public static bool IsAdminLogin
        {
            get
            {
                Initialize();
                return AdminUserId > 0;
            }
        }

        ///// <summary>
        ///// to get Admin User name from session
        ///// </summary>
        public static string AdminUserName
        {
            get
            {
                return HttpContext.Current.Session["AdminUserName"] != null ? Convert.ToString(HttpContext.Current.Session["AdminUserName"]) : "";
            }
            set { HttpContext.Current.Session["AdminUserName"] = value; }
        }


        ///// <summary>
        ///// to get Admin User name from session
        ///// </summary>
        public static string CCEmail
        {
            get
            {
                return HttpContext.Current != null && HttpContext.Current.Session["CCEmail"] != null ? Convert.ToString(HttpContext.Current.Session["CCEmail"]) : "";
            }
            set { HttpContext.Current.Session["CCEmail"] = value; }
        }

        ///// <summary>
        ///// to get Start Date
        ///// </summary>
        public static string StartDate
        {
            get
            {
                return HttpContext.Current.Session["StartDate"] != null ? Convert.ToString(HttpContext.Current.Session["StartDate"]) : "";
            }
            set { HttpContext.Current.Session["StartDate"] = value; }
        }
        ///// <summary>
        ///// to get End Date
        ///// </summary>
        public static string EndDate
        {
            get
            {
                return HttpContext.Current.Session["EndDate"] != null ? Convert.ToString(HttpContext.Current.Session["EndDate"]) : "";
            }
            set { HttpContext.Current.Session["EndDate"] = value; }
        }
        
        public static void Dispose()
        {
            HttpContext.Current.Session.Abandon();
            var fuid = HttpContext.Current.Request.Cookies["fuid"];
            if (fuid != null)
            {
                fuid.Expires = DateTime.Now.AddDays(-2);
                HttpContext.Current.Response.Cookies.Add(fuid);
            }
            var fauid = HttpContext.Current.Request.Cookies["fauid"];
            if (fauid != null)
            {
                fauid.Expires = DateTime.Now.AddDays(-2);
                HttpContext.Current.Response.Cookies.Add(fauid);
            }
        }

        
        private static void Initialize()
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    // check user in cookie
                    //#region UserDetails
                    //if (UserId == 0 || string.IsNullOrEmpty(UserName))
                    //{
                    //    UserId = UserCookie.IsValidEncryptedID() ? Convert.ToInt64(UserCookie.ToDecrypt()) : 0;
                    //    // intialize user value
                    //    dbLalitLoyalty DB = new dbLalitLoyalty();
                    //    User user = DB.Users.Where(x => x.ID == UserId && x.Status == (byte)Status.Active).FirstOrDefault();
                    //    if (user != null)
                    //    {
                    //        UserId = user.ID;
                    //        UserName = (user.FirstName ?? "") + " " + (user.LastName ?? "");
                    //        UserProfilePic = user.PhotoFileName ?? "";

                    //        var companyUser = user.CompanyUsers.FirstOrDefault();
                    //        if (companyUser != null)
                    //        {
                    //            CompanyId = companyUser.CompanyID ?? 0;

                    //        }

                    //        // get user role
                    //        var role = user.UserRoles.FirstOrDefault();
                    //        if (role != null)
                    //        {
                    //            RoleID = role.RoleID ?? 0;
                    //            UserRole = role.Role.Name ?? "";

                    //            // to check is super admin
                    //            if (role.RoleID == ApplicationSettings.SuperAdminRoleId)
                    //            {
                    //                IsSuperAdmin = true;
                    //            }
                    //            var companyLicense = companyUser.Company.CompanyLicenses
                    //              .Where(CL => CL.CompanyID == companyUser.CompanyID && CL.Status == (byte)Status.Active && CL.StartDate <= utilityHelper.CurrentDateTime && CL.EndDate >= utilityHelper.CurrentDateTime).FirstOrDefault();

                    //            SessionHelper.IsValidLicense = IsSuperAdmin ? true : (companyLicense != null) ? true : false;
                    //        }
                    //        SessionHelper.CCEmail = DB.CompanyUsers.Where(x => x.CompanyID == companyUser.CompanyID && x.IsAdmin == true).Select(x => x.User.Email).FirstOrDefault();
                    //        //// to check is super admin
                    //        //if (DB.AdminUsers.Where(x => x.Email == user.Email).Count() > 0)
                    //        //{
                    //        //    IsSuperAdmin = true;
                    //        //}

                    //        //// to check is ValidLicense


                    //    }
                    //}
                    //#endregion UserDetails

                    //// check admin user in cookie
                    //if (AdminUserId == 0 || string.IsNullOrEmpty(AdminUserName))
                    //{
                    //    AdminUserId = AdminUserCookie.IsValidEncryptedID() ? Convert.ToInt32(AdminUserCookie.ToDecrypt()) : 0;
                    //    // intialize user value
                    //    dbLalitLoyalty DB = new dbLalitLoyalty();
                    //    AdminUser adminUser = DB.AdminUsers.Where(x => x.ID == AdminUserId).FirstOrDefault();
                    //    if (adminUser != null)
                    //    {
                    //        AdminUserId = adminUser.ID;
                    //        AdminUserName = adminUser.Name ?? "";
                    //    }
                    //}



                }
            }
            catch { }
        }
    }
}

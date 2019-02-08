using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Xml.Linq;
using System.Xml;
using Top20Video.Framework;
using System.Web.Mvc;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Threading.Tasks;

namespace Top20Video.Framework
{
    public class utilityHelper
    {

        public static double UploadProcess
        {
            get;
            set;
        }
        /// <summary>
        /// to get current date time 
        /// </summary>
        /// <returns>datetime value</returns>
        public static DateTime CurrentDateTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// to get app default region
        /// </summary>
        public static string DefaultRegion
        {
            get
            {
                return GetAppSettings("DefaultRegion");
            }
        }


        /// <summary>
        /// to get current ip address
        /// </summary>
        /// <returns>ip address string</returns>
        public static string IpAddress()
        {
            return (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null) ? HttpContext.Current.Request.UserHostAddress : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }

        public static string ApiSecurityKey()
        {
            return GetAppSettings("ApiSecurityKey");
        }

        /// <summary>
        /// to get physical path from application
        /// </summary>
        /// <returns>application path string</returns>
        public static string ApplicationPath { get; set; }
        //{
        //    return HttpContext.Current.Server.MapPath("~/");
        //}

        /// <summary>
        /// to get site url from application setting file
        /// </summary>
        /// <returns>site root url in string</returns>
        public static string SiteUrl()
        {
            return GetAppSettings("SiteUrl");
        }

        /// <summary>
        /// to read application global message from Message.xml file
        /// </summary>
        /// <param name="root">root node name</param>
        /// <param name="node">child node name</param>
        /// <returns>node value</returns>
        public static string ReadGlobalMessage(string root, string node)
        {
            XDocument doc = XDocument.Load(ApplicationPath + "DataContainer\\XML\\Message.xml");
            return (doc.Descendants(root).Elements().Where(x => x.Name == node).FirstOrDefault().Value ?? "").Replace("[NewLine]", "</br>");
        }

        /// <summary>
        /// to read application global language content from LanguageContent.xml file
        /// </summary>
        /// <param name="root">root node name</param>
        /// <param name="node">child node name</param>
        /// /// <param name="languageId">languageId</param>
        /// <returns>node value</returns>
        public static string ReadLanguageLabel(string root, string node, int languageId)
        {
            XDocument doc = XDocument.Load(ApplicationPath + "DataContainer\\XML\\LanguageContent.xml");
            var lblNode = doc.Descendants(root).Elements().Where(x => x.Name == node);
            var label = doc.Descendants(root).Elements().Where(x => x.Name == node && x.LastAttribute.Value == languageId.ToString()).FirstOrDefault();
            return (label != null ? label.Value.Trim() : "");

        }

        /// <summary>
        /// to read html or text content from file to be saved in Mailer directory
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>file content in string</returns>
        public static string ReadFromFile(string fileName)
        {
            string path = ApplicationPath + "DataContainer\\Mailer\\" + fileName;
            return File.ReadAllText(path);
        }

        /// <summary>
        /// used to get application settings value on besis of setting key
        /// </summary>
        /// <param name="key">application setting key</param>
        /// <returns>application setting value in string</returns>
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        /// <summary>
        /// to send mail from default email
        /// </summary>
        /// <param name="toEmail">receiver email id</param>
        /// <param name="subject">email subject</param>
        /// <param name="bodyHtml">email body in html</param>
        /// <returns>bool is mail send or not</returns>
        public static void SendMail(string toEmail, string subject, string bodyHtml, string AdminEmail, string cc = null, string AttachmentFullPath = null)
        {
            currentContext = HttpContext.Current;
            System.Threading.Tasks.Task.Run(() =>
            {
                HttpContext.Current = currentContext;
                string fromEmail = GetAppSettings("EmailFrom");
                SendMail(toEmail, fromEmail, subject, bodyHtml, AdminEmail, AttachmentFullPath, cc);
            });
        }
        private static HttpContext currentContext;

        /// <summary>
        /// used to send mail from any email id
        /// </summary>
        /// <param name="toEmail">receiver email id</param>
        /// <param name="fromEmail">sender email id</param>
        /// <param name="subject">email subject</param>
        /// <param name="bodyHtml">email body in html</param>
        /// <param name="AttachmentFullPath">attachment full path</param>
        /// <returns>bool is mail send or not</returns>
        public static bool SendMail(string toEmail, string fromEmail, string subject, string bodyHtml, string AdminEmail, string AttachmentFullPath, string cc)
        {
            bool status = false;
            try
            {
                if (GetAppSettings("IsMailSetup") != "1")
                {
                    string s = "------------------------------------------" + Environment.NewLine;
                    s += "To  :" + toEmail + Environment.NewLine;
                    s += "CC  :" + cc + Environment.NewLine;
                    s += "Subject :" + subject + Environment.NewLine;
                    s += "Message :" + bodyHtml + Environment.NewLine;

                    EventLogHandler.WriteTextLog(s);

                    return false;
                }

                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient()
                {
                    DeliveryFormat = System.Net.Mail.SmtpDeliveryFormat.International,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    EnableSsl = (GetAppSettings("SmtpSslTrue") == "1"),
                    Host = GetAppSettings("SmtpIP"),
                    Credentials = new System.Net.NetworkCredential(GetAppSettings("SmtpUserName"), GetAppSettings("SmtpPassword")),
                    Port = Convert.ToInt32(GetAppSettings("SmtpPort"))
                };

                System.Net.Mail.MailMessage sendMail = new System.Net.Mail.MailMessage();

                sendMail.To.Add(toEmail);

                if (!string.IsNullOrEmpty(cc))
                {
                    sendMail.CC.Add(cc);
                }
                sendMail.From = new System.Net.Mail.MailAddress(fromEmail, GetAppSettings("EmailFromName"));
                sendMail.Body = bodyHtml;
                sendMail.IsBodyHtml = true;
                sendMail.Subject = subject;
                //if (GetAppSettings("IsCCEnabled") == "1" && type == CCType.SuperAdmin)
                //{
                //  sendMail.Bcc.Add(GetAppSettings("AdminEmail"));
                //}

                //else if (GetAppSettings("IsCCEnabled") == "1" && type == CCType.CompanyAdmin)
                //{


                //    //sendMail.Bcc.Add(SessionHelper.CCEmail);
                //}
                if (!string.IsNullOrEmpty(AdminEmail))
                {
                    sendMail.Bcc.Add(AdminEmail);
                }
                if (!string.IsNullOrEmpty(AttachmentFullPath))
                {
                    sendMail.Attachments.Add(new System.Net.Mail.Attachment(AttachmentFullPath));
                }
                smtpClient.Send(sendMail);

            }
            catch (Exception ee)
            {
                status = false;
                //throw new Exception(ee.Message);
            }
            return status;
        }

        /// <summary>
        /// to upload file 
        /// </summary>
        /// <param name="file">posted file base</param>
        /// <param name="fileTuye">fileType like Image, video, document</param>
        /// <returns>saved file name</returns>
        public static string UploadFile(HttpPostedFileBase file, FileType fileType)
        {
            string fileName = "";
            try
            {
                fileName = CurrentDateTime.ToString("ddMMyyhhmmss") + "." + file.FileName.Split('.')[1];
                string path = ApplicationPath + "DataContainer\\";
                switch (fileType)
                {
                    case FileType.ProfilePic:
                        path += "UserProfile\\";
                        break;
                    case FileType.Image:
                        path += "Images\\";

                        break;
                    case FileType.Video:
                        path += "Videos\\";
                        break;
                    case FileType.Document:
                        path += "Documents\\";
                        break;
                    case FileType.Logo:
                        path += "Logo\\";
                        break;
                }

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                file.SaveAs(path + fileName);
                //fileName = path + fileName;
            }
            catch { }
            return fileName;
        }


        /// <summary>
        /// Upload any file in given path
        /// </summary>
        /// <param name="file">file to be saved</param>
        /// <param name="path">path in which the file is to be saved</param>
        /// <returns>complete path in which the file is stored</returns>
        public static string UploadItem(HttpPostedFileBase file, string path)
        {
            string fileName = string.Empty;
            string extension = Path.GetExtension(file.FileName).ToLower();
            fileName = file.FileName.ToLower().Replace(extension, "") + CurrentDateTime.ToString("_yyyyMMddhhmmss") + extension;
            //itemUrl = Guid.NewGuid() + Path.GetExtension(file.FileName);
            //if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(path)))
            //    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
            var final_URL = String.Format("{0}{1}", path, fileName);
            //if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(final_URL)))
            //    System.IO.File.Delete(HttpContext.Current.Server.MapPath(final_URL));
            file.SaveAs(final_URL);

            return fileName;
        }

        /// <summary>
        /// to convert enum FileType to IEnumerable list
        /// </summary>
        /// <returns>IEnumerable list</returns>
        public static IEnumerable<SelectListItem> GetFileTypeEnumList()
        {
            var list = Enum.GetValues(typeof(FileType)).Cast<FileType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });
            return list;
        }

        /// <summary>
        /// Retrieve the value of the string resource
        /// The resource manager will retrieve the value of the  
        /// localized resource using the caller's current culture setting.
        /// </summary>
        /// <param name="key">resource name key</param>
        /// <returns></returns>
        public static string GetResourceValue(string key)
        {
            var value = HttpContext.GetGlobalResourceObject("Language", key);

            // ResourceManager rm = new ResourceManager("Language-" + SessionHelper.Culture, Assembly.GetExecutingAssembly());

            // var entry =
            //rm.GetResourceSet(System.Threading.Thread.CurrentThread.CurrentCulture, true, true)
            //  .OfType<DictionaryEntry>()
            //  .FirstOrDefault(e => e.Key.ToString() == key);



            // return entry.Value.ToString();
            return value.ToString();
        }


        /// <summary>
        /// Get default Password
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultPassword()
        {
            Random rnd = new Random();
            int RandomPassword = rnd.Next(1, 999999);
            //return ConfigurationManager.AppSettings["DefaultPassword"].ToString();
            return RandomPassword.ToString();
        }

        /// <summary>
        /// Get Image Path
        /// </summary>
        /// <returns></returns>
        public static string GetImageUrl()
        {
            return ConfigurationManager.AppSettings["ImageUrl"].ToString();
        }

    }


    public class ApplicationSettings
    {
        public static int CompanyDepartmentLimit
        {
            get { return Convert.ToInt32(utilityHelper.GetAppSettings("CompanyDepartmentLimit")); }

        }

        public static int SuperAdminRoleId
        {
            get { return Convert.ToInt32(utilityHelper.GetAppSettings("SuperAdminRoleId")); }
        }
    }


}
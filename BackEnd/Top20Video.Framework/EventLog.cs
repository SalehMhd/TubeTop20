using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Top20Video.Data;

namespace Top20Video.Framework
{
    public static class EventLogHandler
    {
        public static void WriteLog(Exception ex)
        {
            try
            {
                using (dbTop20Video_9359Entities DB = new dbTop20Video_9359Entities())
                {
                    Data.EventLog log = new Data.EventLog();
                    log.Message = ex.Message.ToString();
                    log.Source = ex.Source.ToString();
                    log.StackTrace = ex.StackTrace.ToString();
                    log.IpAddress = "";// IpAddress();
                    log.Datetime = DateTime.UtcNow;
                    log.Url = HttpContext.Current!=null ? HttpContext.Current.Request.Url.ToString() : "Service";

                    DB.EventLogs.Add(log);
                    DB.SaveChanges();
                }
            }
            catch
            {
            }
        }

        public static void WriteTextLog(string message)
        {
            try
            {
                string Folderpath = utilityHelper.ApplicationPath + "Log";
                if (!Directory.Exists(Folderpath))
                    Directory.CreateDirectory(Folderpath);
                string path = utilityHelper.ApplicationPath + "Log\\" + "log_" + DateTime.Now.ToString("dd MMM yyyy");
                string fileName = utilityHelper.ApplicationPath + "log_" + DateTime.Now.ToString("dd MMM yyyy");

                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(message);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


       

        public static string IpAddress()
        {
            return (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null) ? HttpContext.Current.Request.UserHostAddress : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
    }
}
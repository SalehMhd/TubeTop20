using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top20Video.Repository;
using Top20Video.Models;
using Top20Video.Framework;
using Top20Video.Data;

namespace Top20Video.Web.Controllers
{
 
    [ErrorHandler]
    public class ForgotPasswordController : BaseController 
    {
        /// <summary>
        /// controller to list activities
        /// </summary>
        /// <param name="_unitofwork"></param>
        public ForgotPasswordController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        public ActionResult Index()
        {                           
            return View();           
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult ResetPassword(ForgotPasswordModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            utilityHelper _utilityHelper = new utilityHelper();
           
                if (ModelState.IsValid) 
                {
                // check user Email
                var user = UnitofWork.RepoUser.Where(x => x.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    Guid gid = Guid.NewGuid();
                    string password = gid.ToString().Substring(0, 8);
                    // Update Password 
                    user.Password = Extenctions.ToEnctyptedPassword(password);
                    UnitofWork.Commit();
                    // todo:
                    // Send Mail
                    //string subject = "Reset Password";
                    //string template = _utilityHelper.ReadFromFile("ForgotPassword.html");
                    //template = template.Replace("[Name]", user.Name);
                    //template = template.Replace("[Email]", user.Email);
                    //template = template.Replace("[Password]", password);
                    //template = template.Replace("[SiteUrl]", _utilityHelper.SiteUrl());
                    //utilityHelper.SendMail(user.Email, subject, template);

                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ForgotPassword", "SuccessMessage");
                    model.TransMessage.Status = MessageStatus.Success;
                }
                else
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ForgotPassword", "ErrorMessage");
                }
            }
                else
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ForgotPassword", "ErrorMessage");
                }
           
            return Json(model.TransMessage, JsonRequestBehavior.DenyGet);
        }



    }
}

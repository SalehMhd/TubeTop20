using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top20Video.Repository;
using Top20Video.Models;
using Top20Video.Framework;

namespace Top20Video.Web.Controllers
{
    [AuthorizeAdmin]//[NoCache]
    [ErrorHandler]
    public class ChangePasswordController : BaseController 
    {
        /// <summary>
        /// controller to list activities
        /// </summary>
        /// <param name="_unitofwork"></param>
        public ChangePasswordController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
           
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
          
                if (ModelState.IsValid)
                {
                    if (model.NewPassword == model.ConfirmPassword)
                    {
                    var user = UnitofWork.RepoUser.Where(x => x.ID == SessionHelper.AdminUserId).FirstOrDefault();
                    if (user != null && user.Password == Extenctions.ToEnctyptedPassword(model.CurrentPassword))
                    {

                        string newPassword = Extenctions.ToEnctyptedPassword(model.NewPassword);
                        if (user.Password != newPassword)
                        {
                            // Update Password 
                            user.Password = newPassword;
                            UnitofWork.Commit();

                            model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ChangePassword", "SuccessMessage");
                            model.TransMessage.Status = MessageStatus.Success;
                        }
                        else
                        {
                            // old n new password are same
                            model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ChangePassword", "OldAndNewSame");
                        }
                    }
                    else
                    {
                        // wrong current password
                        model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ChangePassword", "WrongPassword");
                    }
                }
                    else
                    {
                        // new n confirm not match
                        model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ChangePassword", "NotMatch");
                    }
                }
                else
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ChangePassword", "ErrorMessage");
                }           
            return Json(model.TransMessage, JsonRequestBehavior.DenyGet);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Top20Video.Framework;
using Top20Video.Models;
using Top20Video.Repository;

namespace Top20Video.Web.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
        }


        //
        // GET: /Home/

        [AllowAnonymous]
        public ActionResult Index(string id)
        {
            Top20Video.Framework.utilityHelper.ApplicationPath = Server.MapPath("~/");
            if (SessionHelper.IsAdminLogin)
            {
                return RedirectToAction("Index", "Settings");
            }
            LoginModel model = new LoginModel();
            model.ReturnUrl = id;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult DashBoard()
        {     
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult DoLogin(LoginModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            utilityHelper _utilityHelper = new utilityHelper();
            TransMessage.Status = MessageStatus.Error;

            if (ModelState.IsValid)
            {
                string password = model.Password.ToEnctyptedPassword();
                //check user credential
                //p@ssw0rd1111
                //Synapse1234
                var user = UnitofWork.RepoUser.Where(x => x.Email.ToLower() == model.Email.ToLower() && x.Password == password && x.Status == 1).FirstOrDefault();
                if (user != null)
                {
                    SessionHelper.AdminUserId = user.ID;
                    SessionHelper.AdminUserName = user.Name ?? "";

                    if (model.RememberMe)
                    {
                        SessionHelper.UserCookie = user.ID.ToString().ToEnctypt();
                    }

                    TransMessage.Status = MessageStatus.Success;
                    TransMessage.Message = string.IsNullOrEmpty(model.ReturnUrl) ? Url.Action("Index", "Settings") : model.ReturnUrl;
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("Login", "ErrorMessage");
                }
            }
            else
            {
                TransMessage.Message = utilityHelper.ReadGlobalMessage("Login", "ErrorMessage");
            }

            return Json(TransMessage, JsonRequestBehavior.DenyGet);
        }

        [AuthorizeAdmin]
        public ActionResult Logout()
        {
            SessionHelper.Dispose();
            return Redirect("Index");

        }
    }
}

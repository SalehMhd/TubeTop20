using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top20Video.Framework;
using Top20Video.Models;
using Top20Video.Repository;


namespace Top20Video.Web.Controllers
{
    [AuthorizeAdmin]
    public class SettingsController : BaseController
    {
        ISettingService _settingService;
        public SettingsController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _settingService = new SettingService();
        }

        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            SettingModel model = new SettingModel();
            model = _settingService.Get();
            return View(model);

        }

        [HttpPost]
        public JsonResult Save(SettingModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            if (ModelState.IsValid)
            {
                try
                {
                    _settingService.Set(model);
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("Settings", "Update");
                    model.TransMessage.Status = MessageStatus.Success;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                model.TransMessage.Message = utilityHelper.ReadGlobalMessage("Settings", "Save");
            }

            return Json(model.TransMessage, JsonRequestBehavior.DenyGet);

        }

        [HttpPost]
        public JsonResult SyncVideo()
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Info;
            TransMessage.Message = "Video Sync Process is Started.";
            //web task scheduler
            System.Threading.Tasks.Task.Run(async () =>
            {
                if (await Top20Video.AutoService.JobScheduler.SyncNow())
                {
                    TransMessage.Status = MessageStatus.Success;
                    TransMessage.Message = "Video Sync process has been completed";
                }
            });

            return Json(TransMessage, JsonRequestBehavior.DenyGet);
        }


    }
}

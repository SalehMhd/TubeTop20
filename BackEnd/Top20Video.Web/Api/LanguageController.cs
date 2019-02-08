using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Top20Video.Repository;

namespace Top20Video.Web.Api
{
    public class LanguageController:BaseApiController
    {
        ILanguageServices languageServices;
        public LanguageController()
        {
            languageServices = new LanguageServices();
        }
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            return SuccessResult(languageServices.GetLanguageList());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Top20Video.Models;
using Top20Video.Framework;
using Top20Video.Repository;

namespace Top20Video.Web.Api
{
    //[ApiAuth]
    public class RegionController : BaseApiController
    {

        IRegionService regionService;

        public RegionController()
        {
            regionService = new RegionService();
        }
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            return SuccessResult(regionService.GetList().Where(r => r.Code != "All"));
        }

    }
}
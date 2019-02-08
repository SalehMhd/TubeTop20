using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Top20Video.Models;
using Top20Video.Framework;
using Top20Video.Repository;
using System.Configuration;

namespace Top20Video.Web.Api
{
    //[ApiAuth]
    public class VideoController : BaseApiController
    {

        IVideoService videoService;

        public VideoController()
        {
            videoService = new VideoService();
        }
        // GET api/<controller>

        public HttpResponseMessage Get(string regionCode, int categoryId,string LanguageCode="")
        {

            if (ConfigurationManager.AppSettings["OnlyAll"] == "Yes")
            {
                regionCode = "All";
            }
                
            if (string.IsNullOrEmpty(regionCode))
            {
                return NotFoundResult();
            }
           
            if(string.IsNullOrEmpty(LanguageCode))
            {
                LanguageCode = "Gn";
            }

            return SuccessResult(videoService.GetList(categoryId, regionCode,LanguageCode));
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Top20Video.Repository;

namespace Top20Video.Web.Api
{
    public class BaseApiController : ApiController
    {
        //protected IUnitOfWork unitOfWork;

        //public BaseApiController()
        //{
        //    unitOfWork = new EfUnitOfWork();
        //}

        public virtual HttpResponseMessage SuccessResult<T>(HttpStatusCode statucCode, T model)
        {
            return Request.CreateResponse(statucCode, model);
        }

        public virtual HttpResponseMessage SuccessResult<T>(T model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        public virtual HttpResponseMessage ForbiddenResult(string message)
        {
            return Request.CreateErrorResponse(HttpStatusCode.Forbidden, new HttpError(""));
        }

        public virtual HttpResponseMessage ForbiddenResult()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "");
        }

        public virtual HttpResponseMessage SuccessResult()
        {
            return Request.CreateErrorResponse(HttpStatusCode.OK, "");
        }

        public virtual HttpResponseMessage ErrorResult()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ModelState);
        }

        public virtual HttpResponseMessage NotFoundResult()
        {
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Please enter the language parameter.");
        }
    }
}
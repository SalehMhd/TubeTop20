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
    public class CategoryController : BaseApiController
    {

        ICategoryService categoryService;

        public CategoryController()
        {
            categoryService = new CategoryService();
        }
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            return SuccessResult(categoryService.GetList());
        }

    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.AspNetCore.Services;
using Blog.Models;
using Piranha.Models;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        private readonly IApi _api;
        private readonly IModelLoader _loader;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="api">The current api</param>
        public CommentController(IApi api, IModelLoader loader)
        {
            _api = api;
            _loader = loader;

        }



    }
}
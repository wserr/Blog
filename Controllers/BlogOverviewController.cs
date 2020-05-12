using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.AspNetCore.Services;
using Piranha.Models;

namespace Blog.Controllers
{
    public class BlogOverviewController : Controller
    {
        private readonly IApi _api;
        private readonly IModelLoader _loader;
        private readonly IApplicationService _service;

        public BlogOverviewController(IApi api, IModelLoader loader, IApplicationService service)
        {
            _api = api;
            _loader = loader;
            _service = service;
        }

        [HttpGet("BlogOverview/{keyword}")]
        public async Task<IActionResult> BlogOverview(string keyword)
        {
            var siteId = _service.Site.Id;
            var pagesToDisplay = await _api.Pages.GetAllAsync(siteId);
            pagesToDisplay = pagesToDisplay.Where(x => !string.IsNullOrEmpty(x.MetaKeywords) && x.MetaKeywords.Contains(keyword) && x.IsPublished);

            return View(pagesToDisplay);
        }
    }
}
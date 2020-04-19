using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.AspNetCore.Services;
using Blog.Models;
using Piranha.Models;

namespace Blog.Controllers
{
    public class CmsController : Controller
    {
        private readonly IApi _api;
        private readonly IModelLoader _loader;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="api">The current api</param>
        public CmsController(IApi api, IModelLoader loader)
        {
            _api = api;
            _loader = loader;
        }

        /// <summary>
        /// Gets the blog archive with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <param name="page">The optional page</param>
        /// <param name="category">The optional category</param>
        /// <param name="tag">The optional tag</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("archive")]
        public async Task<IActionResult> Archive(Guid id, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {
            var model = await _loader.GetPageAsync<BlogArchive>(id, HttpContext.User, draft);
            model.Archive = await _api.Archives.GetByIdAsync(id, page, category, tag, year, month);

            return View(model);
        }

        /// <summary>
        /// Gets the page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("page")]
        public async Task<IActionResult> Page(Guid id, bool draft = false)
        {
            var model = await _loader.GetPageAsync<StandardPage>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("custompage")]
        public async Task<IActionResult> CustomPage(Guid id, bool draft = false)
        {
            var model = await _loader.GetPageAsync<CustomPage>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("startpage")]
        public async Task<IActionResult> StartPage(Guid id, bool draft = false)
        {
            var model = await _loader.GetPageAsync<StartPageViewModel>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("pagewide")]
        public async Task<IActionResult> PageWide(Guid id, bool draft = false)
        {
            var model = await _loader.GetPageAsync<StandardPage>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the post with the given id.
        /// </summary>
        /// <param name="id">The unique post id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("post")]
        public async Task<IActionResult> Post(Guid id, bool draft = false)
        {
            var model = await _loader.GetPostAsync<BlogPost>(id, HttpContext.User, draft);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid id, BlogPost model)
        {
            ModelState.Remove("Category");
            if(ModelState["g-recaptcha-response"].Errors.Count>0)
            {
                ModelState.AddModelError("GoogleReCaptchaResponse", ModelState["g-recaptcha-response"].Errors[0].ErrorMessage);
                ModelState.Remove("g-recaptcha-response");
            }
            if (ModelState.IsValid)
            {
                // Create the comment
                var comment = new Comment
                {
                    IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    UserAgent = Request.Headers.ContainsKey("User-Agent") ? Request.Headers["User-Agent"].ToString() : "",
                    Author = model.CommentAuthor,
                    Email = model.CommentEmail,
                    Url = model.CommentUrl,
                    Body = model.CommentBody
                };

                await _api.Posts.SaveCommentAndVerifyAsync(id, comment);
                var Data = await _loader.GetPostAsync<PostBase>(id, HttpContext.User);
                return Redirect(Data.Permalink + "#comments");

            }
            else
            {
                var m = await _loader.GetPostAsync<BlogPost>(id, HttpContext.User);
                m.CommentAuthor = model.CommentAuthor;
                m.CommentEmail = model.CommentEmail;
                m.CommentUrl = model.CommentUrl;
                m.CommentBody = model.CommentBody;
                return View("post", m);
            }

        }

    }
}

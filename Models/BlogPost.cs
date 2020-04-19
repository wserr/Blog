using Microsoft.AspNetCore.Mvc;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    [PostType(Title = "Blog post")]
    public class BlogPost : Post<BlogPost>
    {
        [Region]
        public MarkdownField Content {get; set;}

        /// <summary>
        /// Gets/sets the hero.
        /// </summary>
        [Region]
        public Regions.Hero Hero { get; set; }

        [BindProperty]
        [Required]
        public string CommentAuthor { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+",ErrorMessage = "Input is not a valid e-mail")]
        public string CommentEmail { get; set; }

        [BindProperty]
        public string CommentUrl { get; set; }

        [BindProperty]
        [Required]
        public string CommentBody { get; set; }
    }
}
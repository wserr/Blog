using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

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
    }
}
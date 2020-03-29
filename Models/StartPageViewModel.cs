using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;


namespace Blog.Models
{
    [PageType(Title = "Start Page", UseBlocks = false)]
    [PageTypeRoute(Title = "Default", Route = "/startpage")]
    public class StartPageViewModel : Page<StartPageViewModel>
    {
        [Region]
        public MarkdownField Intro {get; set;}

    }
}
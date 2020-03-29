using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;


namespace Blog.Models
{
    [PageType(Title = "Custom Page", UseBlocks = false)]
    [PageTypeRoute(Title="Default", Route = "/custompage")]
    public class CustomPage : Page<CustomPage>
    {

        public class CustomPageFields
        {
            [Field]
            public StringField Description { get; set; }

            [Field]
            public StringField Context { get; set; }
        }
        [Region]
        public CustomPageFields Fields {get; set;}

    }
}
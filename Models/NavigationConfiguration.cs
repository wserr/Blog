using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class NavigationConfiguration
    {
        public List<PageConfiguration> Pages { get; set; } = new List<PageConfiguration>();
        public List<OverviewConfiguration> Overviews { get; set; } = new List<OverviewConfiguration>();
    }

    public class OverviewConfiguration
    {
        public string Name { get; set; }
        public string MetaKeyword { get; set; }
    }

    public class PageConfiguration
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}

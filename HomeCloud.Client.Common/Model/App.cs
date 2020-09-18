using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeCloud.Client.Common.Model
{
    public class App
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string AppLink { get; set; }

        public IEnumerable<NavigationItem> NavigationItems { get; set; }

        public NavigationItem FirstNavigationItem => NavigationItems.First();

        public string NavigationLink(NavigationItem navItem)
        {
            return $"/app/{ AppLink }/{ navItem.Link }";
        }
    }
}

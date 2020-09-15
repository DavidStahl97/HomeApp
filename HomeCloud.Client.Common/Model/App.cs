using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCloud.Client.Common.Model
{
    public class App
    {
        public string Name { get; set; }

        public IEnumerable<NavigationItem> NavigationItems { get; set; }
    }
}

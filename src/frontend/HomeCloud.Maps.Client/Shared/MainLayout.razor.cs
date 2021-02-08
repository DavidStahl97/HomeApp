using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        private bool _drawerOpen = false;

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}

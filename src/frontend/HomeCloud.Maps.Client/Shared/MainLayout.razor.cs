using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HomeCloud.Maps.Client.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        private bool _drawerOpen = false;
        private bool _rightToLeft = false;
        private NavMenu _navMenuRef;


        [Inject] private NavigationManager NavigationManager { get; set; }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private void RightToLeftToggle()
        {
            _rightToLeft = !_rightToLeft;
        }

        protected override void OnInitialized()
        {
            //_currentTheme = _defaultTheme;
            _drawerOpen = true;
        }

        protected override void OnParametersSet()
        {
            //_previous = DocsService.Previous;
            //_next = DocsService.Next;
            //_section = DocsService.Section;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            //refresh nav menu because no parameters change in nav menu
            //but internal data does
            //_navMenuRef.Refresh();
        }

        #region Theme        

        private void DarkMode()
        {
            //if (_currentTheme == _defaultTheme)
            //{
            //    _currentTheme = _darkTheme;
            //}
            //else
            //{
            //    _currentTheme = _defaultTheme;
            //}
        }

        //private MudTheme _currentTheme = new MudTheme();
        //private readonly MudTheme _defaultTheme =
        //    new MudTheme()
        //    {
        //        Palette = new Palette()
        //        {
        //            Black = "#272c34"
        //        }
        //    };
        //private readonly MudTheme _darkTheme =
        //    new MudTheme()
        //    {
        //        Palette = new Palette()
        //        {
        //            Black = "#27272f",
        //            Background = "#32333d",
        //            BackgroundGrey = "#27272f",
        //            Surface = "#373740",
        //            DrawerBackground = "#27272f",
        //            DrawerText = "rgba(255,255,255, 0.50)",
        //            AppbarBackground = "#27272f",
        //            AppbarText = "rgba(255,255,255, 0.70)",
        //            TextPrimary = "rgba(255,255,255, 0.70)",
        //            TextSecondary = "rgba(255,255,255, 0.50)",
        //            ActionDefault = "#adadb1",
        //            ActionDisabled = "rgba(255,255,255, 0.26)",
        //            ActionDisabledBackground = "rgba(255,255,255, 0.12)",
        //            DrawerIcon = "rgba(255,255,255, 0.50)"
        //        }
        //    };

        #endregion
    }
}

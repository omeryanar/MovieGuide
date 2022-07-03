using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MovieGuide.WebApp.Shared
{
    public class ThemeProvider : MudThemeProvider
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        public ThemeProvider()
        {
            Theme = new MudTheme();
            Theme.Palette.Primary = Colors.Blue.Default;
            Theme.Palette.Secondary = Colors.Orange.Default;
            Theme.Palette.AppbarBackground = Colors.Blue.Darken2;
            Theme.Palette.Background = Colors.Grey.Lighten5;
            Theme.Palette.DrawerBackground = Colors.Grey.Lighten4;

            Theme.PaletteDark.Primary = Colors.Blue.Default;
            Theme.PaletteDark.Secondary = Colors.Orange.Default;
            Theme.PaletteDark.AppbarBackground = Colors.Blue.Darken2;
            Theme.PaletteDark.Surface = "#383838";
            Theme.PaletteDark.Background = "#303030";
            Theme.PaletteDark.DrawerBackground = "#282828";
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (await LocalStorage.ContainKeyAsync("DarkMode"))
                    DarkModeSetting = await LocalStorage.GetItemAsync<bool>("DarkMode");
                else
                    DarkModeSetting = await GetSystemPreference();

                if (IsDarkMode != DarkModeSetting)
                {
                    IsDarkMode = DarkModeSetting;
                    StateHasChanged();
                }
            }
        }

        protected async override Task OnParametersSetAsync()
        {
            if (DarkModeSetting != IsDarkMode)
            {
                DarkModeSetting = IsDarkMode;
                await LocalStorage.SetItemAsync<bool>("DarkMode", IsDarkMode);
            }
        }

        private bool DarkModeSetting;
    }
}

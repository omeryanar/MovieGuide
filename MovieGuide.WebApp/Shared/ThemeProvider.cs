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
            Theme = new MudTheme()
            {
                PaletteLight = new PaletteLight()
                {
                    Primary = Colors.Blue.Default,
                    Secondary = Colors.Orange.Default,
                    AppbarBackground = Colors.Blue.Darken2,
                    Background = Colors.Gray.Lighten5,
                    BackgroundGray = Colors.Gray.Lighten5,
                    DrawerBackground = Colors.Gray.Lighten4
                },
                PaletteDark = new PaletteDark()
                {
                    Primary = Colors.Blue.Default,
                    Secondary = Colors.Orange.Default,
                    AppbarBackground = Colors.Blue.Darken2,
                    Surface = "#383838",
                    Background = "#303030",
                    BackgroundGray = "#282828",
                    DrawerBackground = "#282828"
                }
            };
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
                    await IsDarkModeChanged.InvokeAsync(DarkModeSetting);
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

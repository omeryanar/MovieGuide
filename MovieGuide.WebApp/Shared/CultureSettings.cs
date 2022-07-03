using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MovieGuide.WebApp.Shared
{
    public static class CultureSettings
    {
        public static async Task InitializeCulture(this WebAssemblyHost host)
        {
            ILocalStorageService localStorage = host.Services.GetRequiredService<ILocalStorageService>();

            string language = await localStorage.GetItemAsStringAsync("Language");
            if (String.IsNullOrEmpty(language))
                language = "en";

            CultureInfo cultureInfo = new CultureInfo(language);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}

using System.Collections.ObjectModel;
using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Properties;

namespace MovieGuide.WebApp.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public Language Language { get; private set; }

        public ReadOnlyCollection<Language> SupportedLanguages { get; } = new ReadOnlyCollection<Language>(new[]
        {
            new Language { Iso_639_1 ="en", Name = Resources.English },
            new Language { Iso_639_1 ="tr", Name = Resources.Turkish }
        });

        public async Task ChangeLanguage(string languageCode)
        {
            SetLanguage(languageCode);
            await LocalStorage.SetItemAsStringAsync("Language", Language.Iso_639_1);
            
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
        }

        public async Task GoBackward()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }

        public async Task GoForward()
        {
            await JSRuntime.InvokeVoidAsync("history.forward");
        }

        protected override void OnInitialized()
        {
            SetLanguage(CultureInfo.DefaultThreadCurrentCulture?.Name);
        }

        private void SetLanguage(string languageCode = "en")
        {
            Language language = SupportedLanguages.FirstOrDefault(x => x.Iso_639_1 == languageCode);
            Language = language;

            CultureInfo cultureInfo = new CultureInfo(language.Iso_639_1);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}

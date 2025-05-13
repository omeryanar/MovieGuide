using System.Collections.ObjectModel;
using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
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

        #region Language

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

        protected override void OnInitialized()
        {
            SetLanguage(CultureInfo.DefaultThreadCurrentCulture?.Name);
            CurrentPage = History.AddFirst(NavigationManager.Uri);

            NavigationManager.LocationChanged += OnLocationChanged; 
        }        

        private void SetLanguage(string languageCode = "en")
        {
            Language language = SupportedLanguages.FirstOrDefault(x => x.Iso_639_1 == languageCode);
            Language = language;

            CultureInfo cultureInfo = new CultureInfo(language.Iso_639_1);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }

        #endregion

        #region Navigation

        private LinkedList<string> History = new LinkedList<string>();

        private LinkedListNode<string> CurrentPage;

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (e.Location == CurrentPage.Previous?.Value)
            {
                CurrentPage = CurrentPage.Previous;
            }
            else if (e.Location == CurrentPage.Next?.Value)
            {
                CurrentPage = CurrentPage.Next;
            }
            else
            {
                while (CurrentPage.Next != null)
                    History.Remove(CurrentPage.Next);

                CurrentPage = History.AddAfter(CurrentPage, e.Location);
            }

            StateHasChanged();
        }

        #endregion
    }
}

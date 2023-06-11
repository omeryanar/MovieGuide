using System.Collections.ObjectModel;
using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MovieGuide.Common.Model.General;
using MovieGuide.Common.Properties;

namespace MovieGuide.WebApp.Shared
{
    public partial class MainLayout : IDisposable
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

        public void Dispose()
        {
             locationChangingRegistration?.Dispose();
        }

        public void GoBackward()
        {
            if (BackwardStack.Count > 0)
                NavigationManager.NavigateTo(BackwardStack.Peek());
        }

        public void GoForward()
        {
            if (ForwardStack.Count > 0)
                NavigationManager.NavigateTo(ForwardStack.Pop());
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
                locationChangingRegistration = NavigationManager.RegisterLocationChangingHandler(OnLocationChanging);
        }

        private ValueTask OnLocationChanging(LocationChangingContext context)
        {
            if (NavigationManager.Uri.EndsWith(context.TargetLocation, StringComparison.Ordinal))
                return ValueTask.CompletedTask;

            if (BackwardStack.Count > 0 && BackwardStack.Peek() == context.TargetLocation)
            {
                BackwardStack.Pop();

                if (ForwardStack.Count == 0 || ForwardStack.Peek() != NavigationManager.Uri)
                    ForwardStack.Push(NavigationManager.Uri);
            }
            else
            {
                if (BackwardStack.Count == 0 || BackwardStack.Peek() != NavigationManager.Uri)
                    BackwardStack.Push(NavigationManager.Uri);
            }

            return ValueTask.CompletedTask;
        }

        private Stack<string> BackwardStack = new Stack<string>();

        private Stack<string> ForwardStack = new Stack<string>();

        private IDisposable locationChangingRegistration;

        #endregion
    }
}

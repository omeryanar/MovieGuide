using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Services;

namespace MovieGuide.WebApp.Pages
{
    public class BaseResponsivePage : BasePaginatedPage, IBrowserViewportObserver
    {
        [Inject]
        public IBrowserViewportService BrowserViewportService { get; set; }

        public async ValueTask DisposeAsync() => await BrowserViewportService.UnsubscribeAsync(this);

        public Breakpoint CurrentBreakpoint { get; set; }

        #region IBrowserViewportObserver

        public Guid Id { get; } = Guid.NewGuid();

        public ResizeOptions ResizeOptions { get; } = new()
        {
            ReportRate = 250,
            NotifyOnBreakpointOnly = true
        };

        public Task NotifyBrowserViewportChangeAsync(BrowserViewportEventArgs eventArgs)
        {
            CurrentBreakpoint = eventArgs.Breakpoint;

            return InvokeAsync(StateHasChanged);
        }

        #endregion

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await BrowserViewportService.SubscribeAsync(this);

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}

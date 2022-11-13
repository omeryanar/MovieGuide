using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Services;

namespace MovieGuide.WebApp.Pages
{
    public class BaseResponsivePage : BasePaginatedPage
    {
        [Inject]
        public IBreakpointService BreakpointService { get; set; }

        public async ValueTask DisposeAsync() => await BreakpointService.Unsubscribe(breakpointSubscriptionId);

        public Breakpoint CurrentBreakpoint { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var subscriptionResult = await BreakpointService.Subscribe((breakpoint) =>
                {
                    CurrentBreakpoint = breakpoint;
                }, new ResizeOptions
                {
                    NotifyOnBreakpointOnly = true,
                });

                breakpointSubscriptionId = subscriptionResult.SubscriptionId;
                CurrentBreakpoint = subscriptionResult.Breakpoint;
            }
        }

        private Guid breakpointSubscriptionId;
    }
}

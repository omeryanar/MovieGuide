using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using MudBlazor.Utilities;

namespace MovieGuide.WebApp.Controls
{
    public partial class RatingCircular : MudComponentBase
    {
        protected string DivClassname =>
            new CssBuilder("mud-progress-circular")
                .AddClass($"{valueColor}-text")
                .AddClass($"mud-progress-{Size.ToDescriptionString()}")
                .AddClass("mud-progress-static")
                .AddClass(Class)
                .Build();

        protected string SvgClassname =>
            new CssBuilder("mud-progress-circular-circle")
                .AddClass("mud-progress-static")                
                .Build();

        [Parameter]
        [Category(CategoryTypes.ProgressCircular.Appearance)]
        public Size Size { get; set; } = Size.Medium;

        [Parameter]
        [Category(CategoryTypes.ProgressCircular.Behavior)]
        public double Min { get; set; } = 0.0;

        [Parameter]
        [Category(CategoryTypes.ProgressCircular.Behavior)]
        public double Max { get; set; } = 10.0;        

        [Parameter]
        [Category(CategoryTypes.ProgressCircular.Behavior)]
        public double? Value
        {
            get => rating;
            set
            {
                if (rating != value && value != null)
                {
                    fraction = (value.Value - Min) / (Max - Min);
                    rating = (int)(fraction * 100);                    
                    valueColor = rating < 40 ? "red" : (rating < 70 ? "darken-2 yellow" : "darken-2 green");
                    StateHasChanged();
                }
            }
        }
        private double rating;

        private double fraction;       
        private string valueColor;        
    }
}

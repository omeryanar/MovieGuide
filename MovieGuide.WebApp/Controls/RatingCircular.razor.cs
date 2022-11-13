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
                .AddClass("mud-progress-static")
                .AddClass($"rating-{Size.ToDescriptionString()}")
                .AddClass(Class)
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
                    dashOffset = (int) (600 - (6 * rating));
                    valueColor = rating < 40 ? "#F44336" : (rating < 70 ? "#FFEB3B" : "#4CAF50");
                    StateHasChanged();
                }
            }
        }
        private double rating;

        private int dashOffset;
        private double fraction;        
        private string valueColor;        
    }
}

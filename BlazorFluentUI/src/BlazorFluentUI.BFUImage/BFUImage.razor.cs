using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorFluentUI
{
    public partial class BFUImage : BFUComponentBase
    {
        [Inject] private IJSRuntime JSRuntime { get; set; }

        [Parameter] public string Alt { get; set; }
        [Parameter] public ImageCoverStyle CoverStyle { get; set; } = ImageCoverStyle.None;
        [Parameter] public double Height { get; set; } = double.NaN;
        [Parameter] public ImageFit ImageFit { get; set; } = ImageFit.Unset;
        [Parameter] public bool MaximizeFrame { get; set; }
        [Parameter] public string Role { get; set; }
        [Parameter] public bool ShouldFadeIn { get; set; } = true;
        [Parameter] public bool ShouldStartVisible { get; set; } = false;
        [Parameter] public string Src { get; set; }
        [Parameter] public double Width { get; set; } = double.NaN;

        [Parameter] public EventCallback<ImageLoadState> OnLoadingStateChange { get; set; }

        protected const string KEY_PREFIX = "fabricImage";
        private static Regex _svgRegex = new Regex(@"\.svg$");

        protected ElementReference imageRef;

        private bool isLandscape = false;
        private ImageLoadState imageLoadState = ImageLoadState.NotLoaded;
        private bool hasRenderedOnce;

        private static Nullable<bool> SupportsObjectFit;

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            return base.SetParametersAsync(parameters);
        }
    }
}

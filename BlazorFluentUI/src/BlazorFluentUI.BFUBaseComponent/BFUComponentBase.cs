using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFluentUI
{
    public class BFUComponentBase : ComponentBase
    {
        [CascadingParameter(Name = "Theme")]
        public ITheme Theme { get; set; }

        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] private ThemeProvider ThemeProvider { get; set; }

        [Parameter] public string ClassName { get; set; }
        [Parameter] public string Style { get; set; }


        [Parameter] public string AriaDescribedBy { get; set; }

        [Parameter] public string AriaLabel { get; set; }
        [Parameter] public string AriaLabelledBy { get; set; }


        public ElementReference RootElementReference;

        private ITheme _theme;
        private bool reloadStyle;

        [Inject] ScopedStatics ScopedStatics { get; set; }

        protected override void OnInitialized()
        {
            ThemeProvider.ThemeChanged += OnThemeChangedPrivate;
            ThemeProvider.ThemeChanged += OnThemeChangedProtected;
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!ScopedStatics.FocusRectsInitialized)
            {
                ScopedStatics.FocusRectsInitialized = true;
                await JSRuntime.InvokeVoidAsync("BlazorFluentUiBaseComponent.initializeFocusRects");
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private void OnThemeChangedProtected(object sender, BFUThemeChangedArgs themeChangedArgs)
        {
            Theme = themeChangedArgs.Theme;
            OnThemeChanged();
        }

        protected virtual void OnThemeChanged() { }

        private void OnThemeChangedPrivate(object sender, BFUThemeChangedArgs themeChangedArgs)
        {
            reloadStyle = true;
        }
    }
}

using Microsoft.JSInterop;

using System;
using System.Threading.Tasks;

namespace BlazorFluentUI
{
    public class ResponsiveComponentBase : FluentUIComponentBase, IAsyncDisposable
    {
        //STATE
        private string? _resizeEventGuid;
        private DotNetObjectReference<ResponsiveComponentBase>? selfReference;

        protected ResponsiveMode CurrentMode { get; set; } = ResponsiveMode.Large;

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (baseModule == null)
                baseModule = await JSRuntime
            return base.OnAfterRenderAsync(firstRender);
        }
    }
}

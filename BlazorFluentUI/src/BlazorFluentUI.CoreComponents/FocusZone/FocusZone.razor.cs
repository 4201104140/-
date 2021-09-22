using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFluentUI
{
    public partial class FocusZone : FluentUIComponentBase, IAsyncDisposable
    {
        private const string ScriptPath = "./_content/BlazorFluentUI.CoreComponents/focusZone.js";
        private IJSObjectReference? scriptModule;

        private DotNetObjectReference<FocusZone>? selfReference;
        public override async ValueTask DisposeAsync()
        {
            try
            {
                if (scriptModule != null)
                {
                    await scriptModule!.InvokeVoidAsync("unregisterFocusZone", selfReference);
                    await scriptModule.DisposeAsync();
                }
                if (baseModule != null)
                    await baseModule.DisposeAsync();

                selfReference?.Dispose();

                await base.DisposeAsync();
            }
            catch (TaskCanceledException)
            {
            }

        }
    }
}

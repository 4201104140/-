using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorFluentUI
{
    public class FluentUIComponentBase : ComponentBase, IAsyncDisposable
    {

        [Inject] protected IJSRuntime? JSRuntime { get; set; }

        [Parameter] public string? ClassName { get; set; }
        [Parameter] public string? Style { get; set; }

        public ElementReference RootElementReference;

        public virtual async ValueTask DisposeAsync()
        {
            try
            {

            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}

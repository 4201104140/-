using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorFluentUI
{
    public class FluentUIComponentBase : ComponentBase, IAsyncDisposable
    {

        [Inject] protected IJSRuntime? JSRuntime { get; set; }

        [Parameter] public string? ClassName { get; set; }
        [Parameter] public string? Style { get; set; }

        public ElementReference RootElementReference;

        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }

        protected const string BasePath = "./_content/BlazorFluentUI.CoreComponents/baseComponent.js";
        protected IJSObjectReference? baseModule;

        protected CancellationTokenSource cancellationTokenSource = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (baseModule == null)
                    baseModule = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", BasePath);

                if (cancellationTokenSource.Token.IsCancellationRequested)
                    throw new TaskCanceledException();

                
            }
            catch (TaskCanceledException cancelled)
            {
                Debug.WriteLine($"Task cancelled: {cancelled.Message}");
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        public virtual async ValueTask DisposeAsync()
        {
            try
            {
                cancellationTokenSource.Cancel();
                if (baseModule != null && !cancellationTokenSource.IsCancellationRequested)
                {
                    await baseModule.DisposeAsync();
                    baseModule = null;
                }
            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}

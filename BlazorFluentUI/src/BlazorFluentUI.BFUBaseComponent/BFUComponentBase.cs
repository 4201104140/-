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

        [Inject] private IJSRuntime JSRuntime { get; set; }


        [Parameter] public string ClassName { get; set; }
        [Parameter] public string Style { get; set; }


        [Parameter] public string AriaDescribedBy { get; set; }

        [Parameter] public string AriaLabel { get; set; }
        [Parameter] public string AriaLabelledBy { get; set; }


        public ElementReference RootElementReference;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("BlazorFluentUiBaseComponent.initializeFocusRects");

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFluentUI
{
    public class BFUButtonParameters : BFUComponentBase
    {
        [Parameter] public RenderFragment ContentTemplate { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Href { get; set; }
        [Parameter] public bool Primary { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool AllowDisabledFocus { get; set; }
        [Parameter] public bool PrimaryDisabled { get; set; }
        [Parameter] public bool? Checked { get; set; }
        [Parameter] public string AriaDescripton { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public string SecondaryText { get; set; }
        [Parameter] public bool Toggle { get; set; }
        [Parameter] public bool Split { get; set; }
        [Parameter] public string IconName { get; set; }
        [Parameter] public bool HideChervon { get; set; }

    }
}

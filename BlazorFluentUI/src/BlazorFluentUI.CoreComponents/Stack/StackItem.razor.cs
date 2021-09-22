using Microsoft.AspNetCore.Components;

namespace BlazorFluentUI
{
    public partial class StackItem : FluentUIComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public CssValue? Grow { get; set; }
        [Parameter] public bool VerticalFill { get; set; } = true;

        protected string GetStyles()
        {
            string style = "";

            style += $"height:{(VerticalFill ? "100%" : "auto")};";
            style += "width:auto;";

            if (Grow != null)
                style += $"flex-grow:{(Grow.AsBooleanTrueExplicit == true ? "1" : Grow.AsString)};";

            return style;
        }
    }
}

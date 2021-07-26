using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorFluentUI
{
    public partial class Stack : FluentUIComponentBase
    {
        protected double rowGap;
        protected double columnGap;
        protected string? horizontalMargin;
        protected string? verticalMargin;

        private readonly string _id = $"id_{Guid.NewGuid().ToString().Replace("-", "")}";  //id selectors can't begin with a number
        protected string Id => _id;

        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public bool DisableShrink { get; set; } = false;
        [Parameter] public CssValue? Grow { get; set; }

        [Parameter] public bool Wrap { get; set; } = false;

        protected string GetStyles()
        {
            string style = "";

            if (Wrap)
            {
                style += "flex-wrap:wrap;";
                if ()
            }
        }
    }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorFluentUI
{
    public partial class Stack : FluentUIComponentBase
    {
        protected string? horizontalMargin;

        private readonly string _id = $"id_{Guid.NewGuid().ToString().Replace("-", "")}";  //id selectors can't begin with a number
        protected string Id => _id;

        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public bool Horizontal { get; set; } = false;
        [Parameter] public bool Reversed { get; set; } = false;
        [Parameter] public bool VerticalFill { get; set; } = false;

        protected string GetStyles()
        {
            string style = "";

            if (false)
            {

            }
            else
            {
                style += "display:flex;";
                style += $"flex-direction:{(Horizontal ? (Reversed ? "row-reverse" : "row") : (Reversed ? "column-reverse" : "column"))};";
                style += "flex-wrap:nowrap;";
                style += "width:auto";
                style += $"height:{(VerticalFill ? "100%" : "auto")};";

                style += "box-sizing:border-box;";
            }

            return style;
        }
    }
}

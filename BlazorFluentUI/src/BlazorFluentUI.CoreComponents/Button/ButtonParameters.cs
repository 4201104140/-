using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using System.Collections.Generic;
using System.Windows.Input;

namespace BlazorFluentUI
{
    public class ButtonParameters : FluentUIComponentBase
    {

        [Parameter] public bool Primary { get; set; }


        [Parameter] public bool Split { get; set; }
    }
}

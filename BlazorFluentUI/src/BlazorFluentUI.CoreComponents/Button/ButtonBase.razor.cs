using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlazorFluentUI
{
    public partial class ButtonBase : ButtonParameters, IAsyncDisposable
    {
        public ButtonBase()
        {

        }

        internal bool isSplitButton = false;
    }
}

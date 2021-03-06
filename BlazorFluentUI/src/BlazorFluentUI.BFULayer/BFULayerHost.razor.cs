using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFluentUI
{
    public partial class BFULayerHost : BFUComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public RenderFragment? HostedContent { get; set; }

        [Parameter] public bool IsFixed { get; set; } = true;

        protected BFULayerPortalGenerator? portalGeneratorReference;

        public void AddOrUpdateHostedContent(string layerId, RenderFragment? renderFragment)
        {
            portalGeneratorReference?.AddOrUpdateHostedContent(layerId, renderFragment);
        }

        public void RemoveHostedContent(string layerId)
        {
            portalGeneratorReference?.RemoveHostedContent(layerId);
        }
    }
}

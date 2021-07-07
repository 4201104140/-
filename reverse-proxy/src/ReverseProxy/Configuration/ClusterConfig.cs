using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarp.ReverseProxy.Configuration
{
    /// <summary>
    /// A cluster is a group of equivalent endpoints and associated policies.
    /// </summary>
    public sealed record ClusterConfig
    {
        /// <summary>
        /// The Id for this cluster. This needs to be globally unique.
        /// This field is required.
        /// </summary>
        public string ClusterId { get; init; } = default!;
    }
}

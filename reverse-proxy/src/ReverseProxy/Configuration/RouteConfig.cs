
using System;
using System.Collections.Generic;
using Yarp.ReverseProxy.Utilities;

namespace Yarp.ReverseProxy.Configuration
{
    /// <summary>
    /// Describes a route that matches incoming requests based on a the <see cref="Match"/> criteria
    /// and proxies matching requests to the cluster identified by its <see cref="ClusterId"/>.
    /// </summary>
    public sealed record RouteConfig
    {
        /// <summary>
        /// Globally unique identifier of the route.
        /// This field is required.
        /// </summary>
        public string RouteId { get; init; } = default!;

        /// <summary>
        /// Parameters used to match requests.
        /// This field is required.
        /// </summary>
        public RouteMatch Match { get; init; } = default!;

        /// <summary>
        /// Optionally, an order value for this route. Routes with lower numbers take precedence over higher numbers.
        /// </summary>
        public int? Order { get; init; }

        /// <summary>
        /// Gets or sets the cluster that requests matching this route
        /// should be proxied to.
        /// </summary>
        public string? ClusterId { get; init; }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Order,
                RouteId?.GetHashCode(StringComparison.OrdinalIgnoreCase),
                ClusterId?.GetHashCode(StringComparison.OrdinalIgnoreCase),

                Match
                );
        }
    }
}

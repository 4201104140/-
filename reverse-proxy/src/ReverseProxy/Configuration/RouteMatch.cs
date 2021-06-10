
using System;
using System.Collections.Generic;
using Yarp.ReverseProxy.Utilities;

namespace Yarp.ReverseProxy.Configuration
{
    public sealed record RouteMatch
    {
        public IReadOnlyList<string>? Methods { get; init; }

        public IReadOnlyList<string>? Hosts { get; init; }

        public string? Path { get; init; }

        public IReadOnlyList<Rou>
    }
}

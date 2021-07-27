// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Model;

namespace Yarp.ReverseProxy.SessionAffinity
{
    /// <summary>
    /// Provides session affinity for load-balanced clusters.
    /// </summary>
    public interface ISessionAffinityPolicy
    {
        /// <summary>
        /// A unique identifier for this session affinity implementation. This will be referenced from config.
        /// </summary>
        string Name { get; }

        //AffinityResult FindAffinitizedDestinations(HttpContext context, Clus)
    }
}

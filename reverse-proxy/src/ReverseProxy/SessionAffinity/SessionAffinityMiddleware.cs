// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Model;
using Yarp.ReverseProxy.Utilities;

namespace Yarp.ReverseProxy.SessionAffinity
{
    /// <summary>
    /// Looks up addinitized <see cref="DestinationState"/> matching the request's affinity key if any is set
    /// </summary>
    internal sealed class SessionAffinityMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionAffinityMiddleware(
            RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task Invoke(HttpContext context)
        {
            var proxyFeature = context.GetRe
        }
    }
}

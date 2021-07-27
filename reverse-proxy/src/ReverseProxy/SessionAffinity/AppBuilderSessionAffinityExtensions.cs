// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Yarp.ReverseProxy.SessionAffinity;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extensions for adding proxy middleware to the pipeline.
    /// </summary>
    public static class AppBuilderSessionAffinityExtensions
    {

        public static IReverseProxyApplicationBuilder UseSessionAffinity(this IReverseProxyApplicationBuilder builder)
        {
            builder.UseMiddleware<SessionAffinityMiddleware>();
            return builder;
        }
    }
}

﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Microsoft.ReverseProxy.Service.Routing
{
    /// <summary>
    /// Represents request header metadata used during routing.
    /// </summary>
    internal class HeaderMetadata : IHeaderMetadata
    {
        public HeaderMetadata(IReadOnlyList<HeaderMatcher> matchers)
        {
            Matchers = matchers ?? throw new ArgumentNullException(nameof(matchers));
        }

        /// <inheritdoc/>
        public IReadOnlyList<HeaderMatcher> Matchers { get; }
    }
}

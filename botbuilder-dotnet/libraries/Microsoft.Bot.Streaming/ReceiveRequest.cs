// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Microsoft.Bot.Streaming.Payloads;

namespace Microsoft.Bot.Streaming
{
    /// <summary>
    /// An incoming request from a remote client.
    /// </summary>
    public class ReceiveRequest
    {
        /// <summary>
        /// Gets or sets the verb action this request wants to perform.
        /// </summary>
        /// <value>
        /// The string representation of an HTTP verb.
        /// </value>
        public string Verb { get; set; }
    }
}

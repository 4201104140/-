// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace Microsoft.Bot.Schema
{
    /// <summary>
    /// The value field of a <see cref="ICommandResultActivity"/> contains metadata related to a command result.
    /// An optional extensible data payload may be included if defined by the command result activity name. 
    /// The presence of an error field indicates that the original command failed to complete.
    /// </summary>
    /// <typeparam name="T">Type for data field.</typeparam>
    public class CommandResultValue<T>
    {
    }
}

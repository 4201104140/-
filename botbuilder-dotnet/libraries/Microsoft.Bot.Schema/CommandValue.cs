// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace Microsoft.Bot.Schema
{
    /// <summary>
    /// The value field of a <see cref="ICommandActivity"/> contains metadata related to a command.
    /// An optional extensible data payload may be included if defined by the command activity name.
    /// </summary>
    /// <typeparam name="T">Type for Data feild.</typeparam>
    public class CommandValue<T>
    {
    }
}

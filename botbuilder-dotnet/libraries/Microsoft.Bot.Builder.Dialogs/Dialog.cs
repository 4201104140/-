// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs.Debugging;
using Newtonsoft.Json;

namespace Microsoft.Bot.Builder.Dialogs
{
    /// <summary>
    /// Base class for all dialogs.
    /// </summary>
    [DebuggerDisplay("{Id}")]
    public abstract class Dialog
    {
        /// <summary>
        /// A <see cref="DialogTurnResult"/> that indicates that the current dialog is still
        /// active and waiting for input from the user next turn.
        /// </summary>
        public static readonly DialogTurnResult EndOfTurn = new DialogTurnResult(DialogTurnStatus.Waiting);

        [JsonProperty("id")]
        private string _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dialog"/> class.
        /// Called from constructors in derived classes to initialize the <see cref="Dialog"/> class.
        /// </summary>
        /// <param name="dialogId">The ID to assign to this dialog.</param>
        public Dialog(string dialogId = null)
        {
            Id = dialogId;
        }

        /// <summary>
        /// Gets or sets id for the dialog.
        /// </summary>
        /// <value>
        /// Id for the dialog.
        /// </value>
        [JsonIgnore]
        public string Id
        {
            get
            {
                _id = _id ?? OnComputeId();
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Builds the compute Id for the dialog.
        /// </summary>
        /// <returns>A string representing the compute Id.</returns>
        protected virtual string OnComputeId()
        {
            return GetType().Name;
        }
    }
}

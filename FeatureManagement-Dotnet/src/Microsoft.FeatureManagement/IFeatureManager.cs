﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.FeatureManagement
{
    /// <summary>
    /// Used to evaluate whether a feature is enabled or disabled.
    /// </summary>
    public interface IFeatureManager
    {
        /// <summary>
        /// Retrieves a list of feature names registered in the feature manager.
        /// </summary>
        /// <returns>An enumerator which provides asynchronous iteration over the feature names registered in the feature manager.</returns>
        IAsyncEnumerable<string> GetFeatureNamesAsync();

        
    }
}

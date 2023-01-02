﻿using IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Stores;

/// <summary>
/// The default validation key store
/// </summary>
/// <seealso cref="IValidationKeysStore" />
public class InMemoryValidationKeysStore : IValidationKeysStore
{
    private readonly IEnumerable<SecurityKeyInfo> _keys;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryValidationKeysStore"/> class.
    /// </summary>
    /// <param name="keys">The keys.</param>
    /// <exception cref="System.ArgumentNullException">keys</exception>
    public InMemoryValidationKeysStore(IEnumerable<SecurityKeyInfo> keys)
    {
        _keys = keys ?? throw new ArgumentNullException(nameof(keys));
    }

    /// <summary>
    /// Gets all validation keys.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync()
    {
        using var activity = Tracing.StoreActivitySource.StartActivity("InMemoryValidationKeysStore.GetValidationKeys");

        return Task.FromResult(_keys);
    }
}

using IdentityServer.EntityFramework.Interfaces;
using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.EntityFramework.Stores;

public class SigningKeyStore : ISigningKeyStore
{
    const string Use = "signing";

    /// <summary>
    /// The DbContext.
    /// </summary>
    protected readonly IPersistedGrantDbContext Context;

    /// <summary>
    /// The CancellationToken provider.
    /// </summary>
    protected readonly ICancellationTokenProvider CancellationTokenProvider;

    /// <summary>
    /// The logger.
    /// </summary>
    protected readonly ILogger<SigningKeyStore> Logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SigningKeyStore"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="cancellationTokenProvider"></param>
    /// <exception cref="ArgumentNullException">context</exception>
    public SigningKeyStore(IPersistedGrantDbContext context, ILogger<SigningKeyStore> logger, ICancellationTokenProvider cancellationTokenProvider)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Logger = logger;
        CancellationTokenProvider = cancellationTokenProvider;
    }

    /// <summary>
    /// Loads all keys from store.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<SerializedKey>> LoadKeysAsync()
    {
        using var activity = Tracing.StoreActivitySource.StartActivity("SigningKeyStore.LoadKeys");

        var entities = await Context.Keys.Where(x => x.Use == Use)
            .AsNoTracking()
            .ToArrayAsync(CancellationTokenProvider.CancellationToken);
        return entities.Select(key => new SerializedKey
        {
            Id = key.Id,
            Created = key.Created,
            Version = key.Version,
            Algorithm = key.Algorithm,
            Data = key.Data,
            DataProtected = key.DataProtected,
            IsX509Certificate = key.IsX509Certificate
        });
    }

    public Task DeleteKeyAsync(string id)
    {
        throw new NotImplementedException();
    }


    public Task StoreKeyAsync(SerializedKey key)
    {
        throw new NotImplementedException();
    }
}

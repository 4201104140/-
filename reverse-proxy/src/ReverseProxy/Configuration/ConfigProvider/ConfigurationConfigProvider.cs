using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yarp.ReverseProxy.Configuration.ConfigProvider
{
    public sealed class ConfigurationConfigProvider : IProxyConfigProvider
    {
        private readonly object _lockObject = new object();
        private readonly ILogger<ConfigurationConfigProvider> _logger;
        private readonly IConfiguration _configuration;
        private ConfigurationSnapshot? _snapshot;
        private CancellationTokenSource? _changeToken;
        private bool _disposed;
        private IDisposable? _subscription;

        public ConfigurationConfigProvider(
            ILogger<ConfigurationConfigProvider> logger,
            IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _subscription?.Dispose();
                _changeToken?.Dispose();
                _disposed = true;
            }
        }

        public IProxyConfig GetConfig()
        {
            if (_snapshot == null)
            {
                _subscription = ChangeToken.OnChange(_configuration)
            }
        }
    }
}

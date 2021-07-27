﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Forwarder;

namespace Yarp.ReverseProxy.Configuration.ConfigProvider
{
    /// <summary>
    /// Reacts to configuration changes and applies configurations to the Reverse Proxy core.
    /// When configs are loaded from appsettings.json, this takes care of hot updates
    /// when appsettings.json is modified on disk.
    /// </summary>
    internal sealed class ConfigurationConfigProvider : IProxyConfigProvider, IDisposable
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
            // First time load
            if (_snapshot == null)
            {
                _subscription = ChangeToken.OnChange(_configuration.GetReloadToken, UpdateSnapshot);
                UpdateSnapshot();
            }

            return _snapshot;
        }

        [MemberNotNull(nameof(_snapshot))]
        private void UpdateSnapshot()
        {
            // Prevent overlapping updates, especically on startup.
            lock (_lockObject)
            {
                Log.LoadData(_logger);
                ConfigurationSnapshot newSnapshot;

            }
        }

        private static class Log
        {
            private static readonly Action<ILogger, Exception> _errorSignalingChange = LoggerMessage.Define(
                LogLevel.Error,
                EventIds.ErrorSignalingChange,
                "An exception was thrown from the change notification.");

            private static readonly Action<ILogger, Exception?> _loadData = LoggerMessage.Define(
                LogLevel.Information,
                EventIds.LoadData,
                "Loading proxy data from config.");

            private static readonly Action<ILogger, Exception> _configurationDataConversionFailed = LoggerMessage.Define(
                LogLevel.Error,
                EventIds.ConfigurationDataConversionFailed,
                "Configuration data conversion failed.");

            public static void ErrorSignalingChange(ILogger logger, Exception exception)
            {
                _errorSignalingChange(logger, exception);
            }

            public static void LoadData(ILogger logger)
            {
                _loadData(logger, null);
            }

            public static void ConfigurationDataConversionFailed(ILogger logger, Exception exception)
            {
                _configurationDataConversionFailed(logger, exception);
            }
        }
    }
}

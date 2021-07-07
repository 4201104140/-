using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using static ChangeTokenSample.Utilities.Utilities;

namespace ChangeTokenSample.Extensions
{
    #region snippet1
    public interface IConfigurationMonitor
    {
        bool MonitoringEnabled { get; set; }
        string CurrentState { get; set; }
    }
    #endregion

    public class ConfigurationMonitor : IConfigurationMonitor
    {
        private byte[] _appsettingsHash = new byte[20];
        private byte[] _appsettingsEnvHash = new byte[20];
        private readonly IWebHostEnvironment _env;

        #region snippet2
        public ConfigurationMonitor(IConfiguration config, IWebHostEnvironment env)
        {
            _env = env;

            ChangeToken.OnChange<IConfigurationMonitor>(
                () => config.GetReloadToken(),
                InvokeChanged,
                this);
        }
        public bool MonitoringEnabled { get; set; } = false;
        public string CurrentState { get; set; } = "Not monitoring";
        #endregion

        #region snippet3
        private void InvokeChanged(IConfigurationMonitor state)
        {
            if (MonitoringEnabled)
            {
                
            }
        }
        #endregion
    }
}

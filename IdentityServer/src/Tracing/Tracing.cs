using System.Diagnostics;

namespace IdentityServer;

internal static class Tracing
{
    private static readonly Version AssemblyVersion = typeof(Tracing).Assembly.GetName().Version;

    public static ActivitySource BasicActivitySource { get; } = new(
        TraceNames.Basic,
        ServiceVersion);

    /// <summary>
    /// Store ActivitySource
    /// </summary>
    public static ActivitySource StoreActivitySource { get; } = new(
        TraceNames.Store,
        ServiceVersion);

    /// <summary>
    /// Cache ActivitySource
    /// </summary>
    public static ActivitySource ServiceActivitySource { get; } = new(
        TraceNames.Services,
        ServiceVersion);

    /// <summary>
    /// Service version
    /// </summary>
    public static string ServiceVersion => $"{AssemblyVersion.Major}.{AssemblyVersion.Minor}.{AssemblyVersion.Build}";

    public static class TraceNames
    {
        /// <summary>
        /// Service name for base traces
        /// </summary>
        public static string Basic => "IdentityServer";

        /// <summary>
        /// Service name for store traces
        /// </summary>
        public static string Store => Basic + ".Stores";

        /// <summary>
        /// Service name for caching traces
        /// </summary>
        public static string Services => Basic + ".Services";
    }
}
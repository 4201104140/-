using Host.Main7.Extensions;

namespace Host.Main7;

internal static class HostingExtensions
{
    internal static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.Services.AddSameSiteCookiePolicy();

        builder
    }
}

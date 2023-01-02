namespace IdentityServer;

internal static class Constants
{
    public static class KnownAcrValues
    {
        public const string HomeRealm = "idp:";
        public const string Tenant = "tenant:";

        public static readonly string[] All = { HomeRealm, Tenant };
    }

    public static class EndpointNames
    {
        public const string Authorize = "Authorize";
        public const string BackchannelAuthentication = "BackchannelAuthentication";
        public const string Token = "Token";
        public const string DeviceAuthorization = "DeviceAuthorization";
        public const string Discovery = "Discovery";
        public const string Introspection = "Introspection";
        public const string Revocation = "Revocation";
        public const string EndSession = "Endsession";
        public const string CheckSession = "Checksession";
        public const string UserInfo = "Userinfo";
    }
}

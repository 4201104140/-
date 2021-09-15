# Config based proxy apps

RE: https://github.com/microsoft/reverse-proxy/issues/9

Config based proxies are common and we'll need to support at least basic proxy scenarios from config. Here are some initial considerations:

- Config sources and systems
- Define routes based on host and/or path
- List multiple back-ends per route for load balancing
- A restart should not be needed to pick up config changes
- You should be able to augment a route's configuration in code. Kestrel does something similar using named endpoints.

## Config systems:

We have three relevant components that already have config systems: Kestrel, UrlRewrite, and ReverseProxy.

- [Kestrel](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-3.1#endpoint-configuration)
- [UrlRewrite](https://github.com/dotnet/aspnetcore/blob/f4d81e3af2b969744a57d76d4d622036ac514a6a/src/Middleware/Rewrite/sample/UrlRewrite.xml#L1-L11)
- [ReverseProxy](https://github.com/microsoft/reverse-proxy/blob/b2cf5bdddf7962a720672a75f2e93913d16dfee7/samples/IslandGateway.Sample/appsettings.json#L10-L34)

Proposals:
- The Kestrel config and the Proxy/Gadeway config should remain adjacent, not merged. Inbound and outbound are distinct concerns. As long as both are available in the same broader config system then that's close enough.
- UrlRewrite should also remain as is. It's not ideal that it's in a separate file and format from the rest of the config, but we'll wait and see if that is a long term blocker.

## Route config:


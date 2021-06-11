
using Xunit;

namespace Yarp.ReverseProxy.Configuration.Tests
{
    public class RouteMatchTests
    {
        [Fact]
        public void Equals_Positive()
        {
            var a = new RouteMatch()
            {
                Headers = new[]
                {
                    new RouteHeader()
                    {
                        Name = "Hi",
                        Values = new[] { "v1", "v2" },
                        IsCaseSensitive = true,
                        Mode = HeaderMatchMode.HeaderPrefix,
                    }
                },
                Hosts = new[] { "foo:90" },
                Methods = new[] { "GET", "POST" },
                Path = "/p",
            };
            var b = new RouteMatch()
            {
                Headers = new[]
                {
                    new RouteHeader()
                    {
                        Name = "Hi",
                        Values = new[] { "v1", "v2" },
                        IsCaseSensitive = true,
                        Mode = HeaderMatchMode.HeaderPrefix,
                    }
                },
                Hosts = new[] { "foo:90" },
                Methods = new[] { "GET", "POST" },
                Path = "/p",
            };
            var c = b with { }; // Clone

            Assert.True(a.Equals(b));
            Assert.True(a.Equals(c));
        }

        [Fact]
        public void Equals_Null_False()
        {
            Assert.False(new RouteMatch().Equals(null));
        }
    }
}

// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Microsoft.Bot.Schema.Tests
{
    internal class ActivityTestData
    {
        internal class TestChannelData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new JObject() };
                yield return new object[] { null };
                yield return new object[] { new Dictionary<string, string>() };
                yield return new object[] { new MyChannelData() };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        internal class MyChannelData
        {
            public string Ears { get; set; }

            public string Whiskers { get; set; }
        }
    }
}

// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using static Microsoft.Bot.Schema.Tests.ActivityTestData;

namespace Microsoft.Bot.Schema.Tests
{
    public class ActivityTest
    {
        [Fact]
        public void GetConversationReference()
        {
            var activity = CreateActivity("en-us");

            var conversationReference = activity.GetConversationReference();

            Assert.Equal(activity.Id, conversationReference.ActivityId);
            Assert.Equal(activity.From.Id, conversationReference.User.Id);
            Assert.Equal(activity.Recipient.Id, conversationReference.Bot.Id);
            Assert.Equal(activity.Conversation.Id, conversationReference.Conversation.Id);
            Assert.Equal(activity.ChannelId, conversationReference.ChannelId);
            Assert.Equal(activity.Locale, conversationReference.Locale);
            Assert.Equal(activity.ServiceUrl, conversationReference.ServiceUrl);
        }

        // Default locale intentionally oddly-cased to check that it isn't defaulted somewhere, but tests stay in English
        private static Activity CreateActivity(string locale, bool createRecipient = true, bool createFrom = true)
        {
            var account1 = createFrom ? new ChannelAccount
            {
                Id = "ChannelAccount_Id_1",
                Name = "ChannelAccount_Name_1",
                Properties = new JObject { { "Name", "Value" } },
                Role = "ChannelAccount_Role_1",
            }
            : null;

            var account2 = createRecipient ? new ChannelAccount
            {
                Id = "ChannelAccount_Id_2",
                Name = "ChannelAccount_Name_2",
                Properties = new JObject { { "Name", "Value" } },
                Role = "ChannelAccount_Role_2",
            }
            : null;

            var conversationAccount = new ConversationAccount
            {
                ConversationType = "a",
                Id = "123",
                IsGroup = true,
                Name = "Name",
                Properties = new JObject { { "Name", "Value" } },
                Role = "ConversationAccount_Role",
            };

            var activity = new Activity
            {
                Id = "123",
                From = account1,
                Recipient = account2,
                Conversation = conversationAccount,
                ChannelId = "ChannelId123",
                Locale = locale,
                ServiceUrl = "ServiceUrl123",
            };

            return activity;
        }
    }
}

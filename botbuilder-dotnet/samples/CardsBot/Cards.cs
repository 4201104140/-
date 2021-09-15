// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.IO;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace Microsoft.BotBuilderSamples;
public static class Cards
{
    public static Attachment CreateAdaptiveCardAttachment()
    {
        // combine path for cross platform support
        var paths = new[] { ".", "Resources", "adaptiveCard.json" };
        var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

        var adaptiveCardAttachment = new Attachment()
        {
            ContentType = "application/vnd.microsoft.card.adaptive",
            Content = JsonConvert.DeserializeObject(adaptiveCardJson),
        };

        return adaptiveCardAttachment;
    }

    public static HeroCard GetHeroCard()
    {
        var heroCard = new HeroCard
        {
            Title = "BotFramework Hero Card",
            Subtitle = "Microsoft Bot Framework",
            Text = "Build and connect intelligent bots to interact with your users naturally wherever they are," +
                   " from text/sms to Skype, Slack, Office 365 mail and other popular services.",
            Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
            Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Get Started", value: "https://docs.microsoft.com/bot-framework") },
        };

        return heroCard;
    }

    public static ThumbnailCard GetThumbnailCard()
    {
        var thumbnailCard = new ThumbnailCard
        {
            Title = "BotFramework Thumbnail Card",
            Subtitle = "Microsoft Bot Framework",
            Text = "Build and connect intelligent bots to interact with your users naturally wherever they are," +
                   " from text/sms to Skype, Slack, Office 365 mail and other popular services.",
            Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
            Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Get Started", value: "https://docs.microsoft.com/bot-framework") },
        };

        return thumbnailCard;
    }


}

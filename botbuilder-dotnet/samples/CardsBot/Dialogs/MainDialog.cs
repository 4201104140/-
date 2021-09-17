﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace Microsoft.BotBuilderSamples;

public class MainDialog : ComponentDialog
{
    protected readonly ILogger _logger;

    public MainDialog(ILogger<MainDialog> logger)
        : base(nameof(MainDialog))
    {
        _logger = logger;

        // Define the main dialog and its related components.
        AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
        AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
        {
            ChoiceCardStepAsync,
            ShowCardStepAsync,
        }));

        // The initial child Dialog to run.
        InitialDialogId = nameof(WaterfallDialog);
    }

    // 1. Prompts the user if the user is not in the middle of a dialog.
    // 2. Re-prompts the user when an invalid input is received.
    private async Task<DialogTurnResult> ChoiceCardStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("MainDialog.ChoiceCardStepAsync");

        // Create the PromptOptions which contain the prompt and re-prompt messages.
        // PromptOptions also contains the list of choices available to the user.
        var options = new PromptOptions()
        {
            Prompt = MessageFactory.Text("What card would you like to see? You can click or type the card name"),
            RetryPrompt = MessageFactory.Text("That was not a valid choice, please select a card or number from 1 to 9."),
            Choices = GetChoices(),
        };

        // Prompt the user with the configured PromptOptions.
        return await stepContext.PromptAsync(nameof(ChoicePrompt), options, cancellationToken);
    }

    private async Task<DialogTurnResult> ShowCardStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("MainDialog.ShowCardStepAsync");

        var attachments = new List<Attachment>();

        var reply = MessageFactory.Attachment(attachments);

        switch (((FoundChoice)stepContext.Result).Value)
        {
            case "Adaptive Card":
                // Display an Adaptive Card
                reply.Attachments.Add(Cards.CreateAdaptiveCardAttachment());
                break;
            default:
                // Displar a carousel of all the rich card types.
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                reply.Attachments.Add(Cards.CreateAdaptiveCardAttachment());
                break;
        }

        // Send the card(s) to the user as an attachment to the activity
        await stepContext.Context.SendActivityAsync(reply, cancellationToken);

        // Give the user instructions about what to do next
        await stepContext.Context.SendActivityAsync(MessageFactory.Text("Type anything to see another card."), cancellationToken);

        return await stepContext.EndDialogAsync();
    }

    private IList<Choice> GetChoices()
    {
        var cardOptions = new List<Choice>()
        {
            new Choice() { Value = "Adaptive Card", Synonyms = new List<string>() { "adaptive" } },
            new Choice() { Value = "Animation Card", Synonyms = new List<string>() { "animation" } },
            new Choice() { Value = "Audio Card", Synonyms = new List<string>() { "audio" } },
            new Choice() { Value = "Hero Card", Synonyms = new List<string>() { "hero" } },
            new Choice() { Value = "OAuth Card", Synonyms = new List<string>() { "oauth" } },
            new Choice() { Value = "Receipt Card", Synonyms = new List<string>() { "receipt" } },
            new Choice() { Value = "Signin Card", Synonyms = new List<string>() { "signin" } },
            new Choice() { Value = "Thumbnail Card", Synonyms = new List<string>() { "thumbnail", "thumb" } },
            new Choice() { Value = "Video Card", Synonyms = new List<string>() { "video" } },
            new Choice() { Value = "All cards", Synonyms = new List<string>() { "all" } },
        };

        return cardOptions;
    }
}
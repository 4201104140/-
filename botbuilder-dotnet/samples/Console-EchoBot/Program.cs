// See https://aka.ms/new-console-template for more information
using Console_EchoBot;

Console.WriteLine("Console EchoBot is online. I will repeat any message you send me!");
Console.WriteLine("Say \"quit\" to end.");

// Create the Console Adapter, and add Conversation State
// to the Bot. The Conversation State will be stored in memory.
var adapter = new ConsoleAdapter();

// Create the instance of our Bot.
var echoBot = new EchoBot();

// Connect the Console Adapter to the Bot.
adapter.ProcessActivityAsync(
    async (turnContext, cancellationToken) => await echoBot.OnTurnAsync(turnContext)).Wait();

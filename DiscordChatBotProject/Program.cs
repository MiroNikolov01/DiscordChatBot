using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;
using DiscordChatBotProject.config;
using DiscordChatBotProject.commands;
using DSharpPlus.Interactivity;
using System;
using DSharpPlus.Interactivity.Extensions;

namespace DiscordChatBotProject
{
    public sealed class Program
    {
        public static DiscordClient Client { get; set; }
        public static CommandsNextExtension Commands { get; set; }
        static async Task Main(string[] args)
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };
            Client = new DiscordClient(discordConfig);
            Client.Ready += Client_Ready;

            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
                CaseSensitive = false,
                
            };
            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<MyCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }
        private static Task Client_Ready(DiscordClient sender,ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}

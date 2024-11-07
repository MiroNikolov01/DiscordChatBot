using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static DSharpPlus.Entities.DiscordEmbedBuilder;

namespace DiscordChatBotProject.commands
{
    public class MyCommands : BaseCommandModule
    {
        [Command("hello")]
        public async Task HelloCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Hello {ctx.User.Mention}, how are you?");
        }

        [Command("online?")]
        public async Task Command(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("I am online when I fill my tank with rakia!");
        }

        [Command("serverinfo")]
        [Description("Server Information.")]
        public async Task ServerInfoCommand(CommandContext ctx)
        {
            var guild = ctx.Guild;

            var embed = new DiscordEmbedBuilder
            {
                Title = $"Server Information {guild.Name}",
                Color = DiscordColor.Blurple,
                Description = $"**Total members:** {guild.MemberCount}\n" +
                              $"**Server ID:** {guild.Id}\n" +
                              $"**Created on:** {guild.CreationTimestamp:dd/MM/yyyy}\n" +
                              $"**Owner:** Miro\n"
            };

            if (!string.IsNullOrEmpty(guild.IconUrl))
            {
                embed.ImageUrl = guild.IconUrl;
            }

            await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }

        [Command("roll")]
        [Description("Rolls a dice and returns a number between 1 and 6.")]
        public async Task RollCommand(CommandContext ctx)
        {
            var random = new Random();
            int roll = random.Next(1, 7);
            var embed = new DiscordEmbedBuilder()
            {
                Title = $"{ctx.User.Username} rolled {roll}! 🎲",
                Color = DiscordColor.Sienna,
                Timestamp = DateTimeOffset.Now
            };
            await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }

        [Command("joke")]
        [Description("Tells a random joke.")]
        public async Task JokeCommand(CommandContext ctx)
        {
            string[] jokes = {
            "What does a programmer say when asked why he can't find a girlfriend?\n- Error 404: girlfriend not found.",
            "What is the main reason programmers don't go outside?\n- Too many variables.",
            "How does a programmer express love?\n- if (feels == true) { love++; } else { keepCoding(); }",
            "How does a programmer wash dishes?\n- Washes the first one and then uses a while loop while(thereAreDishes)",
            "Why are some functions so shy?\n- Because they're private.",
            "Can you work in a team?\n– Well, I have experience working with arrays and lists, so I think yes."
        };
            var random = new Random();
            int index = random.Next(jokes.Length);
            await ctx.Channel.SendMessageAsync(jokes[index]).ConfigureAwait(false);
        }
        [Command("userinfo")]
        [Description("Provides information about the user.")]
        public async Task UserInfoCommand(CommandContext ctx)
        {
            DiscordMember member = ctx.Member;
            var roles = member.Roles;
            var user = ctx.User;
            var roleNames = string.Join(", ", roles.Select(role => role.Mention));
            var embed = new DiscordEmbedBuilder()
            {
                Title = $"Information about {ctx.User.Username}",
                Color = DiscordColor.Orange,
                Description = $"**Member name:** {ctx.User.Mention}\n" +
                              $"**Member ID:** {ctx.User.Id}\n" +
                              $"**Created on:** {user.CreationTimestamp:dd/MM/yyyy}\n" +
                              $"Roles: {roleNames}"
            };
            await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }

        [Command("meme")]
        [Description("Sends a random meme.")]
        public async Task MemeCommand(CommandContext ctx)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://meme-api.com/gimme";
                var response = await client.GetStringAsync(url);
                dynamic meme = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                string memeUrl = meme.url;

                await ctx.Channel.SendMessageAsync(memeUrl).ConfigureAwait(false);
            }
        }
        [Command("calculate")]
        [Description("Calculates numbers")]
        public async Task CalculateNumbers(CommandContext ctx, double numberOne, string decision, double numberTwo)
        {
            double result = 0;
            switch (decision)
            {
                case "+":
                    result = numberOne + numberTwo;
                    break;
                case "-":
                    result = numberOne - numberTwo;
                    break;
                case "*":
                    result = numberOne * numberTwo;
                    break;
                case "/":
                    if (numberTwo == 0)
                    {
                        var error = new DiscordEmbedBuilder()
                        {
                            Title = $"Error: You can't divide {numberOne} by 0.",
                            Color = DiscordColor.Red
                        };
                        await ctx.Channel.SendMessageAsync(embed: error).ConfigureAwait(false);
                        return;
                    }
                    result = numberOne / numberTwo;
                    break;
                case "%":
                    if (numberTwo == 0)
                    {
                        var error = new DiscordEmbedBuilder()
                        {
                            Title = $"Error: You can't divide {numberOne} by 0.",
                            Color = DiscordColor.Red
                        };
                        await ctx.Channel.SendMessageAsync(embed: error).ConfigureAwait(false);
                        return;
                    }
                    result = numberOne % numberTwo;
                    break;
            }
            var embedCalculator = new DiscordEmbedBuilder()
            {
                Title = $"Result of: {numberOne} {decision} {numberTwo} =  {result}",
                Color = DiscordColor.Green
            };
            await ctx.Channel.SendMessageAsync(embed: embedCalculator).ConfigureAwait(false);
        }
        [Command("books")]
        [Description("Template")]
        public async Task EmbededMessage(CommandContext ctx)
        {
            var message = new DiscordEmbedBuilder
            {
                Title = "🚀Top 15 Programming Books",
                Description = "We present to you the Programmer's Channel, where you will find a selection of 15 of the best books that anyone aiming to become a master in the world of technology should read! In this exciting list, you will discover foundational works that will help unlock your full potential as a programmer and dive deeper into the world of coding and software architecture!",
                Color = DiscordColor.PhthaloGreen
            }
            .AddField("💡The Passionate Programmer", "[Book 1](https://theswissbay.ch/pdf/Gentoomen%20Library/Programming/Pragmatic%20Programmers/The%20Passionate%20Programmer.pdf)", inline: false)
            .AddField("📊Algorithms Data Structures = Programs", "[Book 2](https://www.cl72.org/110dataAlgo/Algorithms%20%20%20Data%20Structures%20=%20Programs%20%5BWirth%201976-02%5D.pdf)", inline: false)
            .AddField("📈Inside the Machine", "[Book 3](https://nostarch.com/download/insidemachine_ch4.pdf)", inline: false)
            .AddField("💻Code: The Hidden Language of Computer Hardware and Software", "[Book 4](https://bobcarp.wordpress.com/wp-content/uploads/2014/07/code-charles-petzold.pdf)", inline: false)
            .AddField("🌍Concrete Mathematics", "[Book 5](https://seriouscomputerist.atariverse.com/media/pdf/book/Concrete%20Mathematics.pdf)", inline: false)
            .AddField("💡Structure and Interpretation of Computer Programs", "[Book 6](https://eldritchdata.neocities.org/PDF/U/SICP-TheWizardBook.pdf)", inline: false)
            .AddField("📊How to Design Programs", "[Book 7](https://edu.anarcho-copy.org/Programming%20Languages/Racket/How%20to%20design%20program%20se.pdf)", inline: false)
            .AddField("📈The C Programming Language", "[Book 8](https://seriouscomputerist.atariverse.com/media/pdf/book/C%20Programming%20Language%20-%202nd%20Edition%20(OCR).pdf)", inline: false)
            .AddField("💻William Strunk, Jr. The Elements of Style (Learn How to Write)", "[Book 9](https://daoyuan14.github.io/elos.pdf)", inline: false)
            .AddField("📅The Elements of Programming Style", "[Book 10](http://www2.ing.unipi.it/~a009435/issw/extra/kp_elems_of_pgmng_sty.pdf)", inline: false)
            .AddField("💡Clean Code", "[Book 11](https://github.com/jnguyen095/clean-code/blob/master/Clean.Code.A.Handbook.of.Agile.Software.Craftsmanship.pdf)", inline: false)
            .AddField("🌍Algorithm Design", "[Book 12](https://mimoza.marmara.edu.tr/~msakalli/cse706_12/SkienaTheAlgorithmDesignManual.pdf)", inline: false)
            .AddField("📈Modern Operating Systems", "[Book 13](https://csc-knu.github.io/sys-prog/books/Andrew%20S.%20Tanenbaum%20-%20Modern%20Operating%20Systems.pdf)", inline: false)
            .AddField("📊Predict the Unpredictable", "[Book 14](https://www.ece.uvic.ca/~bctill/papers/mocap/Zabell_1991.pdf)", inline: false)
            .AddField("💻Thinking Fast and Slow", "[Book 15](http://dspace.vnbrims.org:13000/jspui/bitstream/123456789/2224/1/Daniel-Kahneman-Thinking-Fast-and-Slow-.pdf)", inline: false);

            await ctx.Channel.SendMessageAsync(embed: message);
        }
        [Command("rules")]
        [Description("Server Rules")]
        public async Task RulesCommand(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = "Server Rules!",
                Description = "Please read the rules carefully!",
                Color = DiscordColor.Red,
                Footer = new EmbedFooter
                {
                    Text = "Thank you for being part of our community!",
                    IconUrl = ctx.Guild.IconUrl
                }
            };

            embed.AddField("❕1. Be Respectful", "– Maintain a good tone with all group members. Insults and disrespectful behavior will not be tolerated.", false);
            embed.AddField("❕2. No Spam", "– Avoid unnecessary messages, advertising, or spam content. Let's keep the server focused on the educational topics.", false);
            embed.AddField("❕3. Ask and Help", "– We encourage actively asking questions, but also mutual assistance. Sharing knowledge is the key to success.", false);
            embed.AddField("❕4. Share Solutions", "– Help with homework and tasks is welcome, but the goal is to learn together. Share explanations, not just answers.", false);
            embed.AddField("❕5. Follow Discord's Guidelines", "– Ensure you follow the general rules and guidelines of the Discord platform.", false);
            embed.AddField("❕6. Channels", "<#1292406919046369313> is intended only for solving tasks and discussing related topics. For other conversations, please use the ⁠<#1293632278018392234> channel.", false);
            embed.AddField("❕IMPORTANT❕", "This server is not official and is not affiliated with the educational institution. We are here to support each other and work together towards better results. Join, ask your questions, and let's succeed together!", false);

            await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }
        [Command("cardgame")]
        [Description("Card game")]
        public async Task GameCard(CommandContext ctx)
        {
            CardGame player = new CardGame();

            int playerCard = player.CardSet;
            string playerCardType = player.CardTypeSet;

            var playerInfo = new DiscordEmbedBuilder()
            {
                Title = $"Your card is {playerCard} {playerCardType}!",
                Color = DiscordColor.Violet,
            };
            await ctx.Channel.SendMessageAsync(embed: playerInfo).ConfigureAwait(false);

            CardGame bot = new CardGame();

            int botCard = bot.CardSet;
            string botCardType = bot.CardTypeSet;

            var botInfo = new DiscordEmbedBuilder()
            {
                Title = $"Bot Pesho the Robot drew {botCard} {botCardType}!",
                Color = DiscordColor.Gold,
            };

            await ctx.Channel.SendMessageAsync(embed: botInfo).ConfigureAwait(false);

            if (botCard > playerCard)
            {
                var botWinner = new DiscordEmbedBuilder()
                {
                    Title = "You lost to Bot Pesho the Robot :confused:",
                    Color = DiscordColor.Red,
                };
                await ctx.Channel.SendMessageAsync(embed: botWinner).ConfigureAwait(false);
            }
            else if (botCard < playerCard)
            {
                var playerWinner = new DiscordEmbedBuilder()
                {
                    Title = "Congratulations, you won! 🏆",
                    Color = DiscordColor.Green,
                };
                await ctx.Channel.SendMessageAsync(embed: playerWinner).ConfigureAwait(false);
            }
            else
            {
                var playerTie = new DiscordEmbedBuilder()
                {
                    Title = "It's a tie! A real battle between equals – deserved respect for both!",
                    Color = DiscordColor.Gold,
                };
                await ctx.Channel.SendMessageAsync(embed: playerTie).ConfigureAwait(false);
            }
        }
        [Command("rollgame")]
        [Description("Dice Game with numbers")]
        public async Task GameOfDice(CommandContext ctx)
        {
            // 🎲
            DiceGame player = new DiceGame();
            var playerNum = player.DiceNumbersSet;
            var embedPlayerMessage = new DiscordEmbedBuilder()
            {
                Title = $"You rolled the number {playerNum} 🎲",
                Color = DiscordColor.Lilac
            };

            await ctx.Channel.SendMessageAsync(embed: embedPlayerMessage).ConfigureAwait(false);

            DiceGame bot = new DiceGame();
            var botNum = bot.DiceNumbersSet;
            var emebededBotMessage = new DiscordEmbedBuilder()
            {
                Title = $"Bot Pesho the Robot rolled the number {botNum} 🎲",
                Color = DiscordColor.CornflowerBlue
            };

            await ctx.Channel.SendMessageAsync(embed: emebededBotMessage).ConfigureAwait(false);

            if (playerNum > botNum)
            {
                var embedWinner = new DiscordEmbedBuilder()
                {
                    Title = "Congratulations, you won! 🏆",
                    Color = DiscordColor.Green
                };
                await ctx.Channel.SendMessageAsync(embed: embedWinner).ConfigureAwait(false);
            }
            else if (playerNum < botNum)
            {
                var embedLoser = new DiscordEmbedBuilder()
                {
                    Title = "You lost to Bot Pesho the Robot :confused:",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: embedLoser).ConfigureAwait(false);
            }
            else
            {
                var embedTie = new DiscordEmbedBuilder()
                {
                    Title = "It's a tie! A real battle between equals – deserved respect for both!",
                    Color = DiscordColor.Gold
                };
                await ctx.Channel.SendMessageAsync(embed: embedTie).ConfigureAwait(false);
            }
        }
    }
}







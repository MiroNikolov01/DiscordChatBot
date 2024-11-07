using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using static DSharpPlus.Entities.DiscordEmbedBuilder;
namespace DiscordChatBotProject.commands
    

{
    public class MyCommands : BaseCommandModule
    {
        [Command("hello")]
        public async Task HelloCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Здравей {ctx.User.Mention} как си?");
        }
        [Command("online?")]
        public async Task Command(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Аз съм онлайн когато си напълня резервоара с ракия!");
        }
        [Command("serverinfo")]
        [Description("Server Information.")]
        public async Task ServerInfoCommand(CommandContext ctx)
        {
            var guild = ctx.Guild;


            var embed = new DiscordEmbedBuilder
            {
                Title = $"Информация на сървъра {guild.Name}",
                Color = DiscordColor.Blurple,
                Description = $"**Общо участници:** {guild.MemberCount}\n" +
                              $"**Сървър ID:** {guild.Id}\n" +
                              $"**Създанено на:** {guild.CreationTimestamp:dd/MM/yyyy}\n" +
                              $"**Собственик:** Miro\n"
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
                Title = $"{ctx.User.Username} хвърли {roll}! 🎲",
                Color = DiscordColor.Sienna,
                Timestamp = DateTimeOffset.Now
            };
            await ctx.Channel.SendMessageAsync(embed : embed).ConfigureAwait(false);
        }
        [Command("joke")]
        [Description("Tells a random joke.")]
        public async Task JokeCommand(CommandContext ctx)
        {
            string[] jokes = {
                "Какво казва програмист, когато го питат защо не може да си намери приятелка?\n- Error 404: girlfriend not found.",

                "Каква е основната причина програмистите да не излизат навън?\n- Твърде много променливи.",

                "Как програмист изразява любов?\n- if (feels == true) { love++; } else { keepCoding(); }",

                "Как програмист мие чинии?\n- Мие първата и после използва цикъл while(thereAreDishes)",

                "Защо някои функции са толкова срамежливи?\n- Защото са private“.",

                "Можете ли да работите в екип?\n– А, имам опит в работата с масиви и с листове, така че мисля, че да."
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
                Title = $"Информация за {ctx.User.Username}",
                Color = DiscordColor.Orange,
                Description = $"**Име на участник:** {ctx.User.Mention}\n" +
                              $"**ID на участник:** {ctx.User.Id}\n" +
                              $"**Създаден на:** {user.CreationTimestamp:dd/MM/yyyy}\n" +
                              $"Роли: {roleNames}"
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
                            Title = $"Error: Не можеш да делиш {numberOne} на 0.",
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
                            Title = $"Error: Не можеш да делиш {numberOne} на 0.",
                            Color = DiscordColor.Red
                        };
                        await ctx.Channel.SendMessageAsync(embed:error).ConfigureAwait(false);
                        return;
                    }
                    result = numberOne % numberTwo;
                    break;

            }
            var embedCalculator = new DiscordEmbedBuilder()
            {
               Title = $"Резултат от: {numberOne} {decision} {numberTwo} =  {result}",
               Color = DiscordColor.Green
            };
            await ctx.Channel.SendMessageAsync(embed : embedCalculator).ConfigureAwait(false);

        }
        [Command("books")]
        [Description("Template")]
        public async Task EmbededMessage(CommandContext ctx)
        {

            var message = new DiscordEmbedBuilder
            {
                Title = "🚀Топ 15 Книги за програмиране",
                Description = "Представяме ви Канала за програмисти, където ви очаква селекция от 15 от най-добрите книги, които всеки, стремящ се да стане майстор в света на технологиите, трябва да прочете! В този вълнуващ списък ще откриете основополагащи произведения, които ще ви помогнат да отключите пълния си потенциал като програмист и да навлезете по-дълбоко в света на кодирането и софтуерната архитектура!",
                Color = DiscordColor.PhthaloGreen
            }/*📊💻📅📈🌍*/
.AddField("💡The Passionate Programmer", "[Книга 1](https://theswissbay.ch/pdf/Gentoomen%20Library/Programming/Pragmatic%20Programmers/The%20Passionate%20Programmer.pdf)", inline: false)

.AddField("📊Algorithms Data Structures = Programs", "[💡Книга 2](https://www.cl72.org/110dataAlgo/Algorithms%20%20%20Data%20Structures%20=%20Programs%20%5BWirth%201976-02%5D.pdf)", inline: false)

.AddField("📈Inside the Machine", "[Книга 3](https://nostarch.com/download/insidemachine_ch4.pdf)", inline: false)

.AddField("💻Code:The Hidden Language of Computer Hardware and Software", "[Книга 4](https://bobcarp.wordpress.com/wp-content/uploads/2014/07/code-charles-petzold.pdf)", inline: false)

.AddField("🌍Concrete Mathematics", "[Книга 5](https://seriouscomputerist.atariverse.com/media/pdf/book/Concrete%20Mathematics.pdf)", inline: false)

.AddField("💡Structure and Interpretation of Computer Programs", "[Книга 6](https://eldritchdata.neocities.org/PDF/U/SICP-TheWizardBook.pdf)", inline: false)
.AddField("📊How to design programs", "[Книга 7](https://edu.anarcho-copy.org/Programming%20Languages/Racket/How%20to%20design%20program%20se.pdf)", inline: false)

.AddField("📈The C programming language", "[Книга 8](https://seriouscomputerist.atariverse.com/media/pdf/book/C%20Programming%20Language%20-%202nd%20Edition%20(OCR).pdf)", inline: false)

.AddField("💻William Strunk, Jr.The Elements of Style(Learn how to write)", "[Книга 9](https://daoyuan14.github.io/elos.pdf)", inline: false)

.AddField("📅The.Elements.Of.Programming.Style", "[Книга 10](http://www2.ing.unipi.it/~a009435/issw/extra/kp_elems_of_pgmng_sty.pdf)", inline: false)
.AddField("💡Clean Code", "[Книга 11](https://github.com/jnguyen095/clean-code/blob/master/Clean.Code.A.Handbook.of.Agile.Software.Craftsmanship.pdf)", inline: false)
.AddField("🌍Algorithm Design", "[Книга 12](https://mimoza.marmara.edu.tr/~msakalli/cse706_12/SkienaTheAlgorithmDesignManual.pdf)", inline: false)

.AddField("📈Modern Operation Systems", "[Книга 13](https://csc-knu.github.io/sys-prog/books/Andrew%20S.%20Tanenbaum%20-%20Modern%20Operating%20Systems.pdf)", inline: false)
.AddField("📊Predict the unpredictabl", "[Книга 14](https://www.ece.uvic.ca/~bctill/papers/mocap/Zabell_1991.pdf)", inline: false)
.AddField("💻Thinking fast and slow", "[Книга 15](http://dspace.vnbrims.org:13000/jspui/bitstream/123456789/2224/1/Daniel-Kahneman-Thinking-Fast-and-Slow-.pdf)", inline: false);
            await ctx.Channel.SendMessageAsync(embed: message);
        }

        [Command("rules")]
        [Description("Server Rules")]
        public async Task RulesCommand(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = "Правила на сървъра!",
                Description = "Моля прочетете правилата внимателно!",
                Color = DiscordColor.Red,
                Footer = new EmbedFooter
                {
                    Text = "Благодаря че участвате в нашето общество!",
                    IconUrl = ctx.Guild.IconUrl

                },

            };


            embed.AddField("❕1. Бъдете уважителни", "– Поддържайте добър тон към всички членове на групата. Обиди и неуважително поведение няма да бъдат толерирани.", false);
            embed.AddField("❕2. Без спам", "– Избягвайте ненужни съобщения, реклама или спам съдържание. Нека сървърът остане фокусиран върху учебните теми.", false);
            embed.AddField("❕3. Питайте и помагайте", "– Насърчаваме активното задаване на въпроси, но също така и взаимната помощ. Споделянето на знания е ключът към успеха.", false);
            embed.AddField("❕4. Споделянето на решения", "– Помощта с домашни и задачи е добре дошла, но целта е да се учим заедно. Споделяйте обяснения, а не просто готови отговори.", false);
            embed.AddField("❕5. Спазвайте правилата на Discord", "– Уверете се, че следвате общите насоки и правила на платформата Discord.", false);
            embed.AddField("❕6. Чатове", "<#1292406919046369313> е предназначена само за решаване на задачи и обсъждане на теми, свързани с тях. За всякакви разговори извън тези рамки, моля използвайте канала ⁠<#1293632278018392234>.", false);
            embed.AddField("❕ВАЖНО❕", "Този сървър не е официален и не е свързан с учебното заведение. Ние сме тук, за да се подкрепяме и да работим заедно към по-добри резултати.Присъединете се, задайте своите въпроси и нека заедно постигнем успеха", false);


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
                Title = $"Твоята карта е {playerCard} {playerCardType}!",
                Color = DiscordColor.Violet,
            };
            await ctx.Channel.SendMessageAsync(embed: playerInfo).ConfigureAwait(false);

            CardGame bot = new CardGame();

            int botCard = bot.CardSet;
            string botCardType = bot.CardTypeSet;

            var botInfo = new DiscordEmbedBuilder()
            {
                Title = $"Бат Пешо Робота извади {botCard} {botCardType}!",
                Color = DiscordColor.Gold,

            };

            await ctx.Channel.SendMessageAsync(embed: botInfo).ConfigureAwait(false);

            if (botCard > playerCard)
            {
                var botWinner = new DiscordEmbedBuilder()
                {
                    Title = "Ти загуби от Бат Пешо Робота :confused:",
                    Color = DiscordColor.Red,
                };
                await ctx.Channel.SendMessageAsync(embed: botWinner).ConfigureAwait(false);

            }
            else if (botCard < playerCard)
            {
                var playerWinner = new DiscordEmbedBuilder()
                {
                    Title = "Поздравления, ти спечели! 🏆",
                    Color = DiscordColor.Green,
                };
                await ctx.Channel.SendMessageAsync(embed: playerWinner).ConfigureAwait(false);

            }
            else
            {
                var playerTie = new DiscordEmbedBuilder()
                {
                    Title = "Равенство! Истинска битка между равни – заслужено уважение и за двамата!",
                    Color = DiscordColor.Gold,
                };
                await ctx.Channel.SendMessageAsync(embed: playerTie).ConfigureAwait(false);
            }
        }
        [Command("rollgame")]
        [Description("Dice Game with numbers")]
        public async Task GameOfDice(CommandContext ctx)
        {
            //🎲
            DiceGame player = new DiceGame();
            var playerNum = player.DiceNumbersSet; 
            var embedPlayerMessage = new DiscordEmbedBuilder()
            {
                Title = $"Ти хвърли числото {playerNum} 🎲",
                Color = DiscordColor.Lilac
            };

            await ctx.Channel.SendMessageAsync(embed : embedPlayerMessage).ConfigureAwait(false);

            DiceGame bot = new DiceGame();
            var botNum = bot.DiceNumbersSet;
            var emebededBotMessage = new DiscordEmbedBuilder()
            {
                Title = $"Бат Пешо Робота хвърли числото {botNum} 🎲",
                Color = DiscordColor.CornflowerBlue
            };

            await ctx.Channel.SendMessageAsync(embed: emebededBotMessage).ConfigureAwait(false);

            if (playerNum > botNum)
            {
                var embedWinner = new DiscordEmbedBuilder()
                {
                    Title = "Поздравления ти победи! 🏆",
                    Color = DiscordColor.Green
                };
                await ctx.Channel.SendMessageAsync(embed: embedWinner).ConfigureAwait(false);

            }
            else if (playerNum < botNum)
            {
                var embedLoser = new DiscordEmbedBuilder()
                {
                    Title = "Ти загуби от Бат Пешо Робота :confused: ",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: embedLoser).ConfigureAwait(false);
            }
            else
            {
                var embedTie = new DiscordEmbedBuilder()
                {
                    Title = "Равенство! Истинска битка между равни – заслужено уважение и за двамата!",
                    Color = DiscordColor.Gold
                };
                await ctx.Channel.SendMessageAsync(embed: embedTie).ConfigureAwait(false);
            }
        }
        // Interaction with a peson
        [Command("interaction")]
        public async Task Interaction(CommandContext ctx)
        {
            var interactivity = Program.Client.UseInteractivity();
            var messageToRetrieve = await interactivity.WaitForMessageAsync(message => message.Content == "Hello Pesho!");

            if (messageToRetrieve.Result.Content == "Hello Pesho!")
            {
                await ctx.Channel.SendMessageAsync($"Здрасти {ctx.User.Mention} върви ли кода?");
            }
        }

    }
}

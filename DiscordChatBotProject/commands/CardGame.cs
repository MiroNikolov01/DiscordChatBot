using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordChatBotProject.commands
{
    public class CardGame
    {
        public int[] CardNumber = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        public string[] CardType = new string[] { "Hearts :hearts:", "Clubs ♣️", "Diamonds ♦️", "Spades ♠️" };
        public int CardSet { get; set; }
        public string CardTypeSet { get; set; }
        public CardGame()
        {
            Random rnd = new Random();
            int numberCard = rnd.Next(0,CardNumber.Length - 1);
            int cardType = rnd.Next(0,CardType.Length - 1);


            this.CardSet = CardNumber[numberCard];
            this.CardTypeSet = CardType[cardType];
        }
    }
}

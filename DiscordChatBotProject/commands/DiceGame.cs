using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordChatBotProject.commands
{
    public class DiceGame
    {
        public int[] DiceNumbers = { 1, 2, 3, 4, 5, 6 };
        public int DiceNumbersSet { get; set; }
        public DiceGame()
        {
            Random rndDice = new Random();
            var randomIndex = rndDice.Next(0, DiceNumbers.Length - 1);
            this.DiceNumbersSet = DiceNumbers[randomIndex]; 
        }
    }
}

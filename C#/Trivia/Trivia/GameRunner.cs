using System;
using System.Linq;

using UglyTrivia;

namespace Trivia
{
    public class GameRunner
    {
        public static void Main(String[] args)
        {
            Game aGame = new Game();

            aGame.AddPlayer("Chet");
            aGame.AddPlayer("Pat");
            aGame.AddPlayer("Sue");

            Random rand = args.Any() ? new Random(args.First().GetHashCode()) : new Random();

            bool currentPlayerHasWon;

            do
            {
                aGame.OnDiceRolled(rand.Next(5) + 1);


                var wasAnswerCorrect = rand.Next(9) != 7;
                currentPlayerHasWon = wasAnswerCorrect ? aGame.CorrectlyAnswered() : aGame.IncorrectlyAnswered();

            } while (!currentPlayerHasWon);

        }


    }

}


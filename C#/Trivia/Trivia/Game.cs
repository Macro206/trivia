using System;
using System.Collections.Generic;
using System.Linq;
using Trivia;

namespace UglyTrivia
{
    public class Game
    {
        private enum QuestionCategory
        {
            Pop,
            Science,
            Sports,
            Rock
        }

        const int c_NumberOfPlayers = 6;
        const int c_QuestionsPerCategory = 50;
        const int c_BoardSize = 12;
        const int c_CoinsNeededToWin = 6;

        List<Player> players = new List<Player>();

        Dictionary<QuestionCategory, LinkedList<string>> m_AllQuestions = new Dictionary<QuestionCategory, LinkedList<string>>();

        int currentPlayerIndex = 0;
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
            CreateQuestions();
        }

        private void CreateQuestions()
        {
            foreach (QuestionCategory category in Enum.GetValues(typeof(QuestionCategory)))
            {
                var questions = new LinkedList<string>();

                for (var i = 0; i < c_QuestionsPerCategory; i++)
                {
                    questions.AddLast(category + " Question " + i);
                }

                m_AllQuestions.Add(category, questions);
            }
        }

        public bool IsPlayable()
        {
            return NumberOfPlayers() >= 2;
        }

        public bool AddPlayer(String playerName)
        {
            players.Add(new Player(playerName));

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int NumberOfPlayers()
        {
            return players.Count;
        }

        private void AdvancePlayerBy(int spaces)
        {
            var currentPlayer = GetCurrentPlayer();
            currentPlayer.MoveForwards(spaces, c_BoardSize);

            Console.WriteLine(currentPlayer.Name
                              + "'s new location is "
                              + currentPlayer.Position);
        }

        public void OnDiceRolled(int roll)
        {
            var currentPlayer = GetCurrentPlayer();
            Console.WriteLine(currentPlayer.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            var cannotLeavePenaltyBox = currentPlayer.IsInPenaltyBox && roll % 2 == 0;
            if (cannotLeavePenaltyBox)
            {
                Console.WriteLine(currentPlayer.Name + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox = false;
                return;
            }

            if (currentPlayer.IsInPenaltyBox)
            {
                isGettingOutOfPenaltyBox = true;
                Console.WriteLine(currentPlayer.Name + " is getting out of the penalty box");
            }

            AdvancePlayerBy(roll);
            AskQuestion();
        }

        private void AskQuestion()
        {
            Console.WriteLine("The category is " + CurrentCategory());

            var questions = m_AllQuestions[CurrentCategory()];
            Console.WriteLine(questions.First());
            questions.RemoveFirst();
        }


        private QuestionCategory CurrentCategory()
        {
            return (QuestionCategory) (GetCurrentPlayer().Position % 4);
        }

        private Player GetCurrentPlayer()
        {
            return players[currentPlayerIndex];
        }

        private void MoveToNextPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        private void GiveCoinToCurrentPlayer()
        {
            var currentPlayer = GetCurrentPlayer();
            currentPlayer.AddCoin();
            Console.WriteLine(GetCurrentPlayer().Name
                              + " now has "
                              + currentPlayer.Coins
                              + " Gold Coins.");
        }

        public bool CorrectlyAnswered()
        {
            var cannotGetCoin = GetCurrentPlayer().IsInPenaltyBox && !isGettingOutOfPenaltyBox;
            if (cannotGetCoin)
            {
                MoveToNextPlayer();
                return false;
            }

            Console.WriteLine("Answer was corrent!!!!");
            GiveCoinToCurrentPlayer();

            var winner = DidPlayerWin();
            MoveToNextPlayer();

            return winner;
        }

        public bool IncorrectlyAnswered()
        {
            Console.WriteLine("Question was incorrently answered");
            Console.WriteLine(GetCurrentPlayer().Name + " was sent to the penalty box");
            GetCurrentPlayer().GoToJail();

            MoveToNextPlayer();
            return false;
        }


        private bool DidPlayerWin()
        {
            return GetCurrentPlayer().Coins >= c_CoinsNeededToWin;
        }
    }

}


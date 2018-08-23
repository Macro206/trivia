namespace Trivia
{
    class Player
    {
        public int Coins { get; private set; }
        public int Position { get; private set; }
        public bool IsInPenaltyBox { get; private set; }

        public string Name { get; }

        public Player(string name)
        {
            Name = name;
        }

        public void MoveForwards(int spaces, int boardSize)
        {
            Position = (Position + spaces) % boardSize;
        }

        public void AddCoin()
        {
            Coins++;
        }

        public void GoToJail()
        {
            IsInPenaltyBox = true;
        }

        public void GetOutOfJail()
        {
            IsInPenaltyBox = false;
        }
    }
}

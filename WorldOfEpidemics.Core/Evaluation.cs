namespace WorldOfEpidemics.Core
{
    public class Evaluation
    {
        public int gameScore { get; private set; } // The game's score

        private int infectedCost = 43; // The score cost of an infected
        private int deathCost = 147; // The score cost of a death
        private int scoreStart = 100000000; // The start score

        public Evaluation()
        {
            //TODO: Implement evaluation at the end of the game
        }

        public void CalcScore(int deathToll, int numOfInfected)
        {
            gameScore = scoreStart - infectedCost * numOfInfected - deathCost * deathToll; // take the score start, and make a new game score, based on how many infected and death
        }
        public double CalculateScorePercentage()
        {
            return (double)gameScore / scoreStart * 100; // making the gamescore to a percentage, so it is easier to process
        }
    }
}

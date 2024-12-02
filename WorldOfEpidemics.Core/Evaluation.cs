namespace WorldOfEpidemics.Core
{
    public class Evaluation
    {
        private int infectedCost = 43; // The score cost of an infected
        private int deathCost = 147; // The score cost of a death
        private int scoreStart = 10000000; // The start score

        public Evaluation(Virus virus, IEnumerable<Event.ChoiceResult> choices)
        {
            CalcScore(virus.DeathToll, virus.NumOfInfected);

            // Put good and bad choices into their own lists.
            GoodChoiceList.AddRange(choices.Where(c => c.WasPositive));
            BadChoiceList.AddRange(choices.Where(c => !c.WasPositive));
        }
        
        public int GameScore { get; private set; } // The game's score
        public List<Event.ChoiceResult> GoodChoiceList { get; set; } = [];
        public List<Event.ChoiceResult> BadChoiceList { get; set; } = [];
        
        private void CalcScore(int deathToll, int numOfInfected)
        {
            GameScore = scoreStart - infectedCost * numOfInfected - deathCost * deathToll; // take the score start, and make a new game score, based on how many infected and death
        }

        private double CalculateScorePercentage()
        {
            return (double)GameScore / scoreStart * 100; // making the gamescore to a percentage, so it is easier to process
        }
    }
}

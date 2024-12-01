using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core
{
    public class Evaluation
    {
        private int infectedCost = 43; // The score cost of an infected
        private int deathCost = 147; // The score cost of a death
        private int scoreStart = 10000000; // The start score

        public Evaluation(Virus virus, Dictionary<Event.ChoiceResult, bool > choiceList)
        {
            CalcScore(virus.DeathToll, virus.NumOfInfected);

            // Sorts all good choices
            foreach(var choice in choiceList)
            {
                if(choice.Value)
                    GoodChoiceList.Add(choice.Key);
            }

            // Sorts all bad choices
            foreach(var choice in choiceList)
            {
                if(!choice.Value)
                    BadChoiceList.Add(choice.Key);
            }
        }
        
        public int GameScore { get; private set; } // The game's score
        public List<Event.ChoiceResult> GoodChoiceList { get; set; } = new List<Event.ChoiceResult>();
        public List<Event.ChoiceResult> BadChoiceList { get; set; } = new List<Event.ChoiceResult>();
        
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

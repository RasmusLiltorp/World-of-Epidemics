namespace WorldOfEpidemics.Core;

public class Evaluation
{
    public int gameScore { get; private set; } // The game's score

    private int baseCost = 43; // The score cost of an infected
    
    public Evaluation() { }

    public void CalcScore(int deathToll, int numOfInfected)
    {
        gameScore = (int)(8 * Math.Pow(10, 6) - baseCost * numOfInfected - 5 * baseCost * deathToll);
    }
}
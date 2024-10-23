namespace WorldOfEpidemics.Core;
using System.Timers;

public class Virus
{
    string virusName = "Gorm den Gamle";
    Random rnd = new Random();
    
    int numOfInfected = 1; // Start rate for infected
    int deathToll = 0; // Start rate for death toll
    private double infectionRate = 0.0; // Start rate infectionRate
    
    public void Infection()
    {
        // Random value that each infected infects themselves (between 1.01 and 1.25)
        double minValue = 1.01;
        double maxValue = 1.25;
        double infectionRate = minValue + (rnd.NextDouble() * (maxValue - minValue));
    }
    public void Deaths()
    {
        // Random value between 0.05% to 1%
        double minValue = 0.0005;
        double maxValue = 0.01;
        double deathRate = minValue + (rnd.NextDouble() * (maxValue - minValue));
    }
    public void TickRate()
    {
        Timer timer = new Timer(10000); // Every 10th second
        timer.Elapsed += (sender, e) => Service();
        timer.AutoReset = true;
        timer.Start();
    }
    private void Service()
    {
        numOfInfected = (int)(numOfInfected / (numOfInfected + Math.Exp(-infectionRate)));
        if (numOfInfected >= 100) // Don't want the player to lose instantly
        {
            
        }
    }
}
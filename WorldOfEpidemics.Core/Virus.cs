namespace WorldOfEpidemics.Core;
using System.Timers;

public class Virus
{
    string virusName = "Gorm den Gamle";
    Random rnd = new Random();
    public int numOfInfected = 1; // Start rate for infected
    public int deathToll; // Start rate for death toll
    
    private double infectionRate;
    private double deathRate;
    
    // parameters for Richards growth curve
    public int population = 5000000;   // Population (k)
    private double B = 0.2;       // Growth rate
    private double t0 = 50;       // Inflection point
    private double Q = 1.0;       // Scaling parameter
    private double Nu = 2.0;      // Shape parameter
    private int time;          // time

    public Virus() // initialize virus
    {
        setInfectionRate();
        SetDeathRate();
        TickRate();
    }
    private void setInfectionRate()
    {
        // Random value that each infected infects others (between 1.01 and 1.25)
        double minValue = 1.01;
        double maxValue = 1.25;
        infectionRate = minValue + (rnd.NextDouble() * (maxValue - minValue));
    }
    private void SetDeathRate()
    {
        // Random value between 0.05% to 1%
        double minValue = 0.0005;
        double maxValue = 0.01;
        double deathRate = minValue + (rnd.NextDouble() * (maxValue - minValue)); ;
    }
    private int CalculateRichardsGrowth(double time)
    {
        // Adjusting growth calculation based on continuous time
        return (int)(population * Math.Pow(1 + Q * Math.Exp(-B * infectionRate * (time - t0)), -1 / Nu));
    }
    private void TickRate()
    {
        Timer timer = new Timer(10000); // Every 10th second
        timer.Elapsed += (sender, e) => Service();
        timer.AutoReset = true;
        timer.Start();
    }
    private void Service()
    {
        // Calculate infected based on time
        numOfInfected = CalculateRichardsGrowth(time);
        
        if (numOfInfected > population) // shouldn't exceed population
        {
            numOfInfected = population; 
        }
        else if (numOfInfected > 100)
        {
            // Calculate new deaths based on deathrate
            deathToll += (int)(numOfInfected * deathRate);
        }
        
        if (deathToll > numOfInfected) // don't exceed numOfInfected
        {
            deathToll = numOfInfected;
        }
        
        Console.WriteLine($"Tick {time:F1}: Infected = {numOfInfected}, Death toll = {deathToll}");
        time += 1;
    }
}
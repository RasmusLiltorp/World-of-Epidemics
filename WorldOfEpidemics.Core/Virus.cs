using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core;
using System.Timers;

public class Virus
{
    string virusName = "Gork, Destroyer of Worlds";
    
    Random rnd = new Random();
    private int population = 8000000;   // Population
    private double infectionRate;
    private double deathRate;
    
    public Virus() // initialize virus
    {
        SetInitialInfectionRate();
        SetDeathRate(); 
    }
    public int NumOfInfected { get; private set; } = 500; 
    public int DeathToll { get; private set; }
    private void SetInitialInfectionRate()
    {
        // Random value that each infected infects others (between 1.005 and 1.1)
        double minValue = 1.005;
        double maxValue = 1.1;
        infectionRate = minValue + (rnd.NextDouble() * (maxValue - minValue));
    }
    public void ModifyInfectionRate(double newInfectionRate)
    {
        infectionRate = newInfectionRate;
    }
    private void SetDeathRate()
    {
        // Random value between 0.05% to 1%
        double minValue = 0.0005;
        double maxValue = 0.01;
        double deathRate = minValue + (rnd.NextDouble() * (maxValue - minValue)); ;
    }
    private int CalculateInfections()
    {
        NumOfInfected = (int)(NumOfInfected * infectionRate);
        return NumOfInfected;
    }

    private int CalculateDeaths()
    {
        DeathToll += (int)(NumOfInfected * deathRate);
        return DeathToll;
    }
   /* private void TickRate()
    {
        Timer timer = new Timer(10000); // Every 10th second
        timer.Elapsed += (sender, e) => Service();
        timer.AutoReset = true;
        timer.Start();
    }*/
    public void Service()
    {
        CalculateInfections();
        CalculateDeaths();
        if (NumOfInfected > population) // shouldn't exceed population
        {
            NumOfInfected = population; 
        }
        if (DeathToll > NumOfInfected) // don't exceed numOfInfected
        {
            DeathToll = NumOfInfected;
        }

        //PrintVirusInfo();
    }
    
}
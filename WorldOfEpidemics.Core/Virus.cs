using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core;
using System.Timers;

public class Virus
{
    string virusName = "Gork, Destroyer of Worlds";
    
    Random rnd = new Random();
    private int population;   // Population
    private double infectionRate;
    private double deathRate;
    private int prevDeaths;
    
    public Virus() // initialize virus
    {
        population = 800000;
        SetInitialInfectionRate();
        SetDeathRate(); 
    }
    public int NumOfInfected { get; private set; } = 250; 
    public int DeathToll { get; private set; }
    private void SetInitialInfectionRate()
    {
        // Random value that each infected infects others (between 1.005 and 1.1)
        double minValue = 1.005;
        double maxValue = 1.1;
        infectionRate = minValue + (rnd.NextDouble() * (maxValue - minValue));
    }
    public void ModifyInfectionRate(double addedInfectionRate)
    {
        infectionRate += infectionRate - addedInfectionRate == 0? addedInfectionRate-0.001 : addedInfectionRate;
    }
    private void SetDeathRate()
    {
        // Random value between 0.005% to 0.75%
        double minValue = 0.0001;
        double maxValue = 0.01;
        deathRate = minValue + (rnd.NextDouble() * (maxValue - minValue)); ;
    }
    private int CalculateInfections()
    {
        NumOfInfected = (int)(NumOfInfected * infectionRate);
        if (NumOfInfected > population) // shouldn't exceed population
        {
            NumOfInfected = population; 
        }
        return NumOfInfected;
    }
    private void CalculatePopulation()
    {
        population -= (DeathToll - prevDeaths);
    }
    private int CalculateDeaths()
    {
        int currentDeaths = (int)(NumOfInfected * deathRate);
        prevDeaths = DeathToll; 
        DeathToll += currentDeaths;
        // Reduce infected count by the number of new deaths
        NumOfInfected -= currentDeaths;
        return DeathToll;
    }
    public void Service()
    {
        CalculateDeaths();
        CalculateInfections();
        CalculatePopulation();
        //Console.WriteLine($"Population: {population}, Infected: {NumOfInfected}, Death Toll: {DeathToll}, Infectionrate: {infectionRate}, Deathrate: {deathRate}");
    }
}

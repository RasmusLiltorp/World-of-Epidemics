namespace WorldOfEpidemics.Core;
using System.Timers;

public class Virus
{
    string virusName = "Gork, Destroyer of Worlds";
    
    Random rnd = new Random();
    
    public int numOfInfected = 500; // Start rate for infected
    public int deathToll; // Start rate for death toll
    public int population = 8000000;   // Population
    
    private double infectionRate;
    private double deathRate;
    
    public Virus() // initialize virus
    {
        SetInitialInfectionRate();
        SetDeathRate(); 
    }
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
    private void CalculateInfections()
    {
        numOfInfected = (int)(numOfInfected * infectionRate);
    }
   /* private void TickRate()
    {
        Timer timer = new Timer(10000); // Every 10th second
        timer.Elapsed += (sender, e) => Service();
        timer.AutoReset = true;
        timer.Start();
    }*/
    private void Service()
    {
        CalculateInfections();
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
        
        Console.WriteLine($"Infected = {numOfInfected}, Death toll = {deathToll}");
    }
}
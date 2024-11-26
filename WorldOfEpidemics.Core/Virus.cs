namespace WorldOfEpidemics.Core;
using System;

public class Virus
{
    private static Random rnd = new Random();
    private Evaluation evaluation = new Evaluation(); // Instance of Evaluation
    public Virus() // initialize virus
    {
        SetInitialInfectionRate();
        SetDeathRate(); 
    }
    
    private int numOfInfected = 500; // Start rate for infected
    private int deathToll; // Start rate for death toll
    private int population = 8000000;   // Population
    private double infectionRate;
    private double deathRate;
    private int prevDeaths;
    private string virusName = VirusNameGenerator.GenerateVirusName();
    
    public bool gameOver { get; private set; } = false; //Game score
    public int NumOfInfected { get; private set; } = 250; 
    public int DeathToll { get; private set; }
    
    public class VirusNameGenerator
    {
        private static string[] firstName = {"Covid", "Alpha", "Mega", "Ultra", "Nano"}; // fill with 5 random first name a virus could have
        private static string[] lastName = {"Virus", "Bacterium", "Flu", "45", "Influenza"}; // fill with 5 random last name a virus could have

        public static string GenerateVirusName()
        {
            string first = firstName[rnd.Next(firstName.Length)]; //finds a random name from the firstName array
            string last = lastName[rnd.Next(lastName.Length)];  //finds a random name from the firstName array
            return first + last; // combine the first and last arrays to make a new virus name
        }    
    }
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
  public void GameIsOver()
    {
        if (gameOver)
        {
            evaluation.CalcScore(deathToll, numOfInfected); // Calculate the score based on death toll and infections
            double scorePercentage = evaluation.CalculateScorePercentage();
        }
    }
}

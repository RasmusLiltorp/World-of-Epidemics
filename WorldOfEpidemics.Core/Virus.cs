namespace WorldOfEpidemics.Core;
using System.Timers;
using System;

public class Virus
{


    private static Random rnd = new Random();
    private Evaluation evaluation = new Evaluation(); // Instance of Evaluation
    
    
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

    public string virusName = VirusNameGenerator.GenerateVirusName(); // sets the virusname to the new name we have just generatet
    
    
    public int numOfInfected = 500; // Start rate for infected
    public int deathToll; // Start rate for death toll
    public int population = 8000000;   // Population
    
    public bool gameOver = false;

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
        deathRate = minValue + (rnd.NextDouble() * (maxValue - minValue)); ;
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
    public void Service()
    {
        CalculateInfections();
        if (numOfInfected > population) // shouldn't exceed population
        {
            numOfInfected = population; 
            gameOver = true;
        }
        else if (numOfInfected > 100)
        {
            // Calculate new deaths based on deathrate
            deathToll += (int)(numOfInfected * deathRate);
        }
        
        if (deathToll > numOfInfected) // don't exceed numOfInfected
        {
            deathToll = numOfInfected;
            gameOver = true;
        }
        
        //Console.WriteLine($"Infected = {numOfInfected}, Death toll = {deathToll}");
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
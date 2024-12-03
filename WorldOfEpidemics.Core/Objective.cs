namespace WorldOfEpidemics.Core
{
    public class Objective
    {
        public Objective(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}

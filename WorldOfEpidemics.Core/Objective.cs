namespace WorldOfEpidemics.Core
{
    public class Objective
    {
        private Dictionary<string, bool> objectives;
        public Objective()
        {
            // Initialize new dictionary with each task - default value false
            objectives = new Dictionary<string, bool>()
            {
                {"wcTask", false},
                {"breakfastTask", false},
                {"schoolTask", false},
                {"dinnerTask", false}
            };
        }
        public bool CompleteObjective(string objectiveName)
        {
            if (objectives.ContainsKey(objectiveName))
            {
                objectives[objectiveName] = true;
                return true;
            }
            return false;
        }
        public bool isCompleted(string objectiveName)
        {
            return objectives[objectiveName];
        }
        public void ResetObjectives()
        {
            // sets each bool in "tasks" to false
            foreach (var key in objectives.Keys.ToList())
            {
                objectives[key] = false;
            }
        }
        public void PrintObjectives()
        {
            foreach (var objective in objectives)
            {
                string status = objective.Value ? "Completed" : "Not completed";
                Console.WriteLine($"{objective.Key}: {status}");
            }
        }
    }
}
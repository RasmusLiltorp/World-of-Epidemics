namespace WorldOfEpidemics.Core
{
    public class Task
    {
        private Dictionary<string, bool> tasks;
        public Task()
        {
            // Initialize new dictionary with each task - default value false
            tasks = new Dictionary<string, bool>()
            {
                {"wcTask", false},
                {"breakfastTask", false},
                {"schoolTask", false},
                {"dinnerTask", false},
                {"sleepTask", false}
            };
        }
        public bool CompleteTask(string taskName)
        {
            if (tasks.ContainsKey(taskName))
            {
                tasks[taskName] = true;
                return true;
            }
            return false;
        }

    }
}
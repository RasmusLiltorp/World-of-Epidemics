using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core
{
    public class Task
    {
        private IUser user;
        private Dictionary<string, bool> tasks;
        public Task()
        {
            // Initialize new dictionary with each task - default value false
            tasks = new Dictionary<string, bool>()
            {
                {"wcTask", false},
                {"breakfastTask", false},
                {"schoolTask", false},
                {"dinnerTask", false}
            };
        }

        public void SetUser(IUser user)
        {
            this.user = user;
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
        public bool isCompleted(string taskName)
        {
            return tasks[taskName];
        }
        public void ResetTasks()
        {
            // sets each bool in "tasks" to false
            foreach (var key in tasks.Keys.ToList())
            {
                tasks[key] = false;
            }
        }
        public void PrintTasks()
        {
            foreach (var task in tasks)
            {
                string status = task.Value ? "Completed" : "Not completed";
                user.WriteText($"{task.Key}: {status}");
            }
        }
    }
}
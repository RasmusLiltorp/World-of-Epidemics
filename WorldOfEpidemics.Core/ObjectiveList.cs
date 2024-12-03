using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core
{
    public class ObjectiveList
    {
        private IUser user;

        public ObjectiveList()
        {
            // Initialize new dictionary with each task - default value false
            Objectives.AddRange(
            [
                new("Use the toilet"),
                new("Eat breakfast"),
                new("Go to school"),
                new("Have dinner")
            ]);
        }

        public List<Objective> Objectives { get; init; } = [];

        public void SetUser(IUser user)
        {
            this.user = user;
        }

        public bool CompleteObjective(string objectiveName)
        {
            var objective = Objectives.FirstOrDefault(o => o.Name == objectiveName);
            if (objective != null)
            {
                objective.IsCompleted = true;

                return true;
            }

            return false;
        }

        public bool IsCompleted(string objectiveName)
        {
            return Objectives.FirstOrDefault(o => o.Name == objectiveName)?.IsCompleted ?? false;
        }

        public void ResetObjectives()
        {
            // sets each bool in "tasks" to false
            foreach (var objective in Objectives)
            {
                objective.IsCompleted = false;
            }
        }

        public void PrintObjectives()
        {
            foreach (var objective in Objectives)
            {
                string status = objective.IsCompleted ? "Completed" : "Not completed";
                user.WriteText($"{objective.Name}: {status}");
            }
        }
    }
}

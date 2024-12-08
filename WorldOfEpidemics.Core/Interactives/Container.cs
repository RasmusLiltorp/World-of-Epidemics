using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Interactives;
using WorldOfEpidemics.Core.Properties;

namespace WorldOfEpidemics.Core.Interactives
{
    public class Container : IInteractable
    {

        public List<IInteractable> interactables { get; set; } = new List<IInteractable>();
        
        public Container(string name, string description)
        {
            Name = name;
            Description = description;
            interactables = new List<IInteractable>();
        }
        public string Name { get; init; }
        public string Description { get; init; }
        public void AddInteractable(IInteractable interactable)
        {
            interactables.Add(interactable);
        }
        public void Interact(IContextModifier contextModifier, IUser user)
        {
            user.BeginBatchWrite();
            if (interactables.Count == 0)
            {
                user.WriteText($"{Name} is empty.");
                return;
            }
            user.WriteText($"Inside the {Name}, you find:");
            foreach (var interactable in interactables)
            {
                user.WriteText($" - {interactable.Name}");
            }
            user.EndBatchWrite();
        }
    }
}
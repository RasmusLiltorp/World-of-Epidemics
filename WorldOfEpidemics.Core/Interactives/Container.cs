using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Interactives;
using WorldOfEpidemics.Core.Properties;
using WorldOfEpidemics.Core.Abstractions;
namespace WorldOfEpidemics.Core.Interactives
{
    public class Container : IInteractable
    {
        public string Name { get; init; }
        public string Description { get; init; }

        private List<IInteractable> interactables;

        public Container(string name, string description)
        {
            Name = name;
            Description = description;
            interactables = new List<IInteractable>();
        }
        public void AddInteractable(IInteractable interactable)
        {
            interactables.Add(interactable);
        }
        public void Interact(IContextModifier contextModifier, IUser user)
        {
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
        }
    }
}
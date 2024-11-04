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
        public void Open(string containerName)
        {
            if (interactables.Count == 0)
            {
                Console.WriteLine($"{Name} is empty.");
                return;
            }

            Console.WriteLine($"Inside the {Name}, you find:");
            foreach (var interactable in interactables)
            {
                Console.WriteLine($" - {interactable.Name}");
            }
        }
        // TODO: Don't know how I should implement this
        public void Interact(IContextModifier contextModifyer)
        {
            throw new NotImplementedException();
        }
    }
}
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
        public void Open()
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
        public void Interact(IContextModifier contextModifier)
        {
            Open();
        }
    }
}
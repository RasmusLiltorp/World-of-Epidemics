namespace WorldOfEpidemics.Core.Interactives
{
    internal class Item : IInteractable
    {
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public required string Name { get; init; }
        public required string Description { get; init; }

        public void Interact(IContextModifier contextModifyer)
        {
            throw new NotImplementedException();
        }
    }
}

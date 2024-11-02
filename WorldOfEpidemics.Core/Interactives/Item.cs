namespace WorldOfEpidemics.Core.Interactives
{
    internal class Item : IInteractable
    {
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        //Name and description properties has had their "required" modifier removed as it messed with initialization of item instances.
        public string Name { get; init; }
        public string Description { get; init; }

        public void Interact(IContextModifier contextModifyer)
        {
            throw new NotImplementedException();
        }
    }
}

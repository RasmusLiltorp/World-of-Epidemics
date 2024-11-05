using System.Reflection.Metadata;
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Interactives
{
    internal class Item : IInteractable
    {
        public Item(string name, string description, bool isPickupable = false)
        {
            Name = name;
            Description = description;
            IsPickupable = isPickupable;
        }
        
        public bool IsPickupable { get; private set; }
        public string Name { get; init; }
        public string Description { get; init; }

        public void Interact(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You interacted with "+Name);
            contextModifier.Inventory.AddInteractable(this);
            throw new NotImplementedException();
        }
    }
}

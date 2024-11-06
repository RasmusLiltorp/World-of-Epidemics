using System.Reflection.Metadata;
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Interactives
{
    internal class Item : IInteractable
    {
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
        public string Name { get; init; }
        public string Description { get; init; }

        public void Interact(IContextModifier contextModifier, IUser user)
        {
            user.WriteText($"You put {Name} in your inventory.");
            contextModifier.Inventory.AddInteractable(this);
        }

        public void Use(IContextModifier contextModifier, IUser user)
        {
            user.WriteText($"You used {Name} from your inventory.");
        }
    }
}

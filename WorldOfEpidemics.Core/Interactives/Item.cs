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
            user.WriteText("You interacted with "+Name);
            throw new NotImplementedException();
        }
    }
}

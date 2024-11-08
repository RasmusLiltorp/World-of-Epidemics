using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Interactives
{
    internal class Item : IInteractable
    {
        public Item(string name, string description, double virusModifier)
        {
            Name = name;
            Description = description;
            VirusModifier = virusModifier;
        }
        
        public string Name { get; init; }
        public string Description { get; init; }
        public double VirusModifier { get; init; }

        public void Interact(IContextModifier contextModifier, IUser user)
        {
            user.WriteText($"You put {Name} in your inventory.");
            contextModifier.Inventory.AddInteractable(this);
        }

        public void Use(IContextModifier contextModifier, IUser user, Item item)
        {
            user.WriteText($"You used {Name} from your inventory.");
            switch (Name.ToLower())
            {
                case "handsanitizer":
                    contextModifier.Effects.Add(Effect.Disinfected, 3000);
                    break;
                case "gloves":
                    contextModifier.Effects.Add(Effect.Gloved, 3000);
                    break;
                case "mask":
                    contextModifier.Effects.Add(Effect.Masked, 3000);
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
            //TODO: use item.VirusModifier to manipulate virus infectionRate
        }
    }
}

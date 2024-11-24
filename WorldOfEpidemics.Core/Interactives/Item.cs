using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Interactives
{
    internal class Item : IInteractable
    {
        
        private Room originalRoom;
        private static readonly List<(Room Room, Item Item)> RemovedItems = new();
        
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
            
            originalRoom = contextModifier.CurrentRoom;
            RemovedItems.Add((originalRoom, this));
            contextModifier.Inventory.AddInteractable(this);
            contextModifier.CurrentRoom.Interactables.Remove(this);
        }

        public static void ReAddItems()
        {
            foreach (var (room, item) in RemovedItems)
            {
                room.Interactables.Add(item);
            }
            RemovedItems.Clear();
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

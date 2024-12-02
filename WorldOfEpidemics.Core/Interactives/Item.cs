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
            originalRoom = contextModifier.CurrentRoom; //Stores the original room of the item
            RemovedItems.Add((originalRoom, this)); //Adds the item to the list "RemovedItems"
            contextModifier.Inventory.AddInteractable(this);
            contextModifier.CurrentRoom.Interactables.Remove(this);
        }

        //method to re-add items after player has reset day
        public static void ReAddItems()
        {
            foreach (var (room, item) in RemovedItems)
            {
                room.Interactables.Add(item);
            }
            RemovedItems.Clear(); //clear RemovedItems list to avoid duplicates
        }
        
        public void Use(IContextModifier contextModifier, IUser user, Item item)
        {
            user.WriteText($"You used {Name} from your inventory.");
            
            // Apply or extend effect
            Dictionary<Effect, int> effects = contextModifier.Effects;
            
            switch (Name.ToLower())
            {
                case "handsanitizer":
                    if (effects.ContainsKey(Effect.Disinfected))
                    {
                        effects[Effect.Disinfected] = 300;
                        break;
                    }
                    
                    effects.Add(Effect.Disinfected, 300);
                    break;
                case "gloves":
                    if (effects.ContainsKey(Effect.Gloved))
                    {
                        effects[Effect.Gloved] = 300;
                        break;
                    }
                    
                    effects.Add(Effect.Gloved, 300);
                    break;
                case "mask":
                    if (effects.ContainsKey(Effect.Masked))
                    {
                        effects[Effect.Masked] = 300;
                        break;
                    }
                    
                    contextModifier.Effects.Add(Effect.Masked, 300);
                    break;
                default:
                    throw new NotImplementedException();
            }
            //TODO: use item.VirusModifier to manipulate virus infectionRate
            
        }
    }
}

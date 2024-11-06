/*Command for interacting with IInteractables*/
using System.Net.Http.Headers;
using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Interactives;
using WorldOfEpidemics.Core.Properties;

namespace WorldOfEpidemics.Core.Commands;

public class CommandInteract : BaseCommand
{
    public CommandInteract()
    {
        Description = Resources.CommandDescriptionInteract;
    }
    IInteractable? FindInteractable(Room room, Container? container, string itemName)
    {
        // Check room interactables
        var interactable = room.interactables.FirstOrDefault(i => i.Name.Equals(itemName));
        
        // If not in room check current container interactables
        if (interactable == null && container != null)
        {
            interactable = container.interactables.FirstOrDefault(i => i.Name.Equals(itemName));
        }
        return interactable;
        /*
        Returns the first element of a sequence, or a default value if the sequence contains no elements.
        Means we don't have to check if item exists with extra code
        */
    }

    public override void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters)
    {
        if (GuardEq(parameters, 1))
        {
            user.WriteText("Please specify an element you want to interact with");
            return;
        }
        Room room = contextModifier.CurrentRoom;
        string itemName = parameters[0].ToLower();
        
        // generic method of finding an interacble, also checks for CurrentContainer
        var interactable = FindInteractable(room, contextModifier.CurrentContainer, itemName);
        
        if (itemName.Equals("Inventory", StringComparison.OrdinalIgnoreCase))
        {
            contextModifier.CurrentContainer = null;
            user.WriteText("You open your inventory and find:");
            if (contextModifier.Inventory.interactables.Count == 0)
            {
                user.WriteText("Nothing.");
                return;
            }
            foreach (var invInteractable in contextModifier.Inventory.interactables)
            {
                Console.WriteLine($" - {invInteractable.Name}");
                return;
            }
        }
        
        if (interactable == null)
        {
            user.WriteText($"Could not find {itemName}.");
            return;
        }   
        if (interactable is Container container)
        {
            container.Interact(contextModifier, user);
            contextModifier.CurrentContainer = container;
        }
        else
        {
            interactable.Interact(contextModifier, user);
            // Reset after non-container item interaction
            contextModifier.CurrentContainer = null;
        }
    }
}
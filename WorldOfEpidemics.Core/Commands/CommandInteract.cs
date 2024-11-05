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
        Description = Resources.CommandInteract;
    }

    public override void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters)
    {
        Room room = contextModifier.CurrentRoom;
        string itemName = parameters[0]; //Item from user input

        if (itemName.ToLower() == "inventory")
        {
            contextModifier.Inventory.Interact(contextModifier, user);
            return;
        }
        foreach (var interactable in room.Interactables)
        {
            if(interactable.Name.Equals(itemName))
            {
                if (interactable is Container container)
                {
                    container.Interact(contextModifier, user);
                    user.WriteText("Do you want to take an item?");
                    return;
                }
                interactable.Interact(contextModifier, user);
                return;

            }
        }
        user.WriteText($"Could not find {itemName}.");
    }
}
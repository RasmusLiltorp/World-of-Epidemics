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
        
        foreach (var interactable in room.Interactables)
        {
            if(interactable.Name.Equals(itemName))
            {
                if(interactable is IInteractable item)
                {
                    item.Interact(itemName);
                    return;
                }
            }
        }
        user.WriteText("du dum");
    }
}
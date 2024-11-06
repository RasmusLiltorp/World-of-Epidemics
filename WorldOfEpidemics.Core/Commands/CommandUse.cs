using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Interactives;
using WorldOfEpidemics.Core.Properties;


namespace WorldOfEpidemics.Core.Commands;

public class CommandUse : BaseCommand
{
    public CommandUse()
    {
        Description = Resources.CommandDescriptionUse;
    }

    public override void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters)
    {
        if (GuardEq(parameters, 1))
        {
            user.WriteText("Please specify an element in your inventory that you want to use");
            return;
        }

        string itemName = parameters[0];
        Container inventory = contextModifier.Inventory;
    
        var interactable = inventory.interactables.FirstOrDefault(i => i.Name.Equals(itemName)); //Find the searched item in inventory list
        
        if(interactable == null) //If the user doesn't have the item in their inventory
        {
            user.WriteText($"You dont have {itemName} in your inventory.");
            return;
        }
        
        if(interactable is Item item) //Interactable can be either a container or item
        {
            item.Use(contextModifier, user, item);
            contextModifier.Inventory.interactables.Remove(item);
        }    
    }
}
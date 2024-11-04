/*Command for interacting with IInteractables*/

namespace WorldOfEpidemics.Core.Commands;

public class CommandInteract : BaseCommand, ICommand
{
    public CommandInteract()
    {
        description = "Interact with an item";
    }

    public void Execute(Context context, string command, string [] parameters)
    {
        return;
    }
}
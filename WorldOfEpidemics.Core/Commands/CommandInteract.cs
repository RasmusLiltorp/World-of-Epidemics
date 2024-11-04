/*Command for interacting with IInteractables*/
using WorldOfEpidemics.Core.Abstractions;
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
        throw new NotImplementedException();
    }
}
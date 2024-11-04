using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Properties;

namespace WorldOfEpidemics.Core.Commands;

public class CommandHelp : BaseCommand
{
    Registry registry;

    public CommandHelp(Registry registry)
    {
        this.registry = registry;
        Description = Resources.CommandDescriptionHelp;
    }

    public override void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters)
    {
        user.ShowHelp();
    }
}

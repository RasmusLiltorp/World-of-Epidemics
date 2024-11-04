using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Properties;

namespace WorldOfEpidemics.Core.Commands;

public class CommandUnknown : BaseCommand
{
    public override void Execute(IContextModifier context, IUser user, string command, string[] parameters)
    {
        user.WriteText(string.Format(Resources.MessageUnknownCommand, command));
    }
}

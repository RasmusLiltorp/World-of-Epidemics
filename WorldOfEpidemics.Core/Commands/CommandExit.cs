using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Commands;

/// <summary>
/// Exit the game.
/// </summary>
public class CommandExit : BaseCommand
{
    public override void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters)
    {
        contextModifier.Exit();
    }
}

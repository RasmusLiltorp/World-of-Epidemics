using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Properties;

namespace WorldOfEpidemics.Core.Commands;

/// <summary>
/// Transition between rooms.
/// </summary>
public class CommandGo : BaseCommand
{
    public CommandGo()
    {
        Description = Resources.CommandDescriptionGo;
    }

    public override void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters)
    {
        if (GuardEq(parameters, 1))
        {
            user.WriteText(Resources.MessageInvalidDoorway);

            return;
        }

        try
        {
        	contextModifier.ChangeRoom(contextModifier.CurrentRoom.FollowDoor(parameters[0]));
        }
        catch(KeyNotFoundException)
        {
            user.WriteText("The given exit does not exist");
        }
    }
}

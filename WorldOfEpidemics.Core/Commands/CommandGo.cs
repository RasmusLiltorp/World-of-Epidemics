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

        var room = contextModifier.CurrentRoom.FollowDoor(parameters[0]);
        if (room == null)
        {
            user.WriteText("That doorway doesn't exist.");

            return;
        }
        contextModifier.ChangeRoom(room);
    }
}

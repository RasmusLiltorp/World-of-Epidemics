/* Command for transitioning between spaces
 */

namespace WorldOfEpidemics.Core.Commands;

public class CommandGo : BaseCommand, ICommand
{
    public CommandGo()
    {
        description = "Follow an exit";
    }

    public void Execute(Context context, string command, string[] parameters)
    {
        if (GuardEq(parameters, 1))
        {
            Console.WriteLine("I don't seem to know where that is 🤔");
            return;
        }

        try
        {
            context.ChangeRoom(context.CurrentRoom.FollowDoor(parameters[0]));
        }
        catch(KeyNotFoundException)
        {
            context.UserController.WriteText("The given exit does not exist");
        }
    }
}

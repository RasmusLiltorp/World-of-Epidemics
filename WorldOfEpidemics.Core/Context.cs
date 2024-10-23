using Microsoft.Win32;
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core;

/// <summary>
/// Holds all data relavant to this session.
/// </summary>
public class Context : IContextModifier
{
    IUserController userController;
    //Room currentRoom;

    public Context(IUserController userController/*, Room startingRoom*/)
    {
        this.userController = userController;
        //currentRoom = startingRoom;
    }

    public bool IsDone { get; private set; }

    public void Service()
    {
        userController.RunCommand();
    }

    public void MakeDone()
    {
        IsDone = true;
    }

    #region IContextModifier
    public void ChangeRoom()
    {
        //if (currentRoom == room)
        //{
        //    userController.WriteText(Properties.Resources.InfoEnterSameRoom);
        //
        //    return;
        //}

        //currentRoom = room;
        // TODO: Use the UserController to write a message to the user.
        // TODO: Show options in the new room.
    }
    #endregion

    #region Original
    public void Transition(string direction)
    {
        //Space next = current.FollowEdge(direction);
        //if (next == null)
        //{
        //    Console.WriteLine("You are confused, and walk in a circle looking for '" + direction + "'. In the end you give up");
        //}
        //else
        //{
        //    current.Goodbye();
        //    current = next;
        //    current.Welcome();
        //}
    }
    #endregion
}

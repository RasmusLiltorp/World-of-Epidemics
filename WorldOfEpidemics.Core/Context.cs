using Microsoft.Win32;
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core;

/// <summary>
/// Holds all data relavant to this session.
/// </summary>
public class Context : IContextModifier
{
    IUserController userController;

    public Context(IUserController userController, Space startingRoom)
    {
        this.userController = userController;
        CurrentRoom = startingRoom;
        CurrentRoom.Welcome();
    }

    public bool IsDone { get; private set; }

    public void Service()
    {
        userController.RunCommand(this);
    }

    public void MakeDone()
    {
        IsDone = true;
    }

    #region IContextModifier
    public Space CurrentRoom { get; private set; }

    public void ChangeRoom(Space room)
    {
        if (CurrentRoom == room)
        {
            userController.WriteText(Properties.Resources.InfoEnterSameRoom);
        
            return;
        }
        
        // TODO: Use the UserController to write a message to the user.
        CurrentRoom.Goodbye();
        CurrentRoom = room;
        // TODO: Show options in the new room (with the UserController).
        CurrentRoom.Welcome();
    }
    #endregion
}

using Microsoft.Win32;
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core;

/// <summary>
/// Holds all data relavant to this session.
/// </summary>
public class Context : IContextModifier
{
    
    public Context(IUserController userController, Room startingRoom)
    {
        this.UserController = userController;
        CurrentRoom = startingRoom;
        CurrentRoom.Welcome();
    }

    public bool IsDone { get; private set; }
    public IUserController UserController { get; private set; }

    public void Service()
    {
        UserController.RunCommand(this);
    }

    public void MakeDone()
    {
        IsDone = true;
    }

    #region IContextModifier
    public Room CurrentRoom { get; private set; }

    public void ChangeRoom(Room room)
    {
        if (CurrentRoom == room)
        {
            UserController.WriteText(Properties.Resources.InfoEnterSameRoom);
        
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

using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core;

/// <summary>
/// Holds all data relavant to this session.
/// </summary>
public class Context : IContextModifier
{
    
    public Context(IUserController userController, Room startingRoom, Virus virus)
    {
        UserController = userController;
        CurrentRoom = startingRoom;
        CurrentRoom.Welcome();
        CurrentVirus = virus;
    }

    public bool IsDone { get; private set; }
    public IUserController UserController { get; private set; }

    public Virus CurrentVirus { get; private set; }
    
    public void Service()
    {
        UserController.RunCommand(this);
    }

    #region IContextModifier
    public Room CurrentRoom { get; private set; }

    public void ChangeRoom(Room room)
    {
        if (CurrentRoom == room)
        {
            userController.WriteText(Properties.Resources.MessageEnterSameRoom);
        
            return;
        }
        
        // TODO: Use the UserController to write a message to the user.
        CurrentRoom.Goodbye();
        CurrentRoom = room;
        // TODO: Show options in the new room (with the UserController).
        CurrentRoom.Welcome();
    }

    public void Exit()
    {
        IsDone = true;
    }
    #endregion
}

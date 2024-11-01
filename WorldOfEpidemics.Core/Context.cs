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
        
        // TODO: Remove the next three lines and uncomment the remaining code of this method.
        CurrentRoom.Goodbye();
        CurrentRoom = room;
        CurrentRoom.Welcome();

        //userController.WriteText(CurrentRoom.GetWelcome());
        //CurrentRoom = room;
        //userController.WriteText(CurrentRoom.GetGoodbye());
        //userController.AnnounceRoom();
    }

    public void Exit()
    {
        IsDone = true;
    }
    #endregion
}

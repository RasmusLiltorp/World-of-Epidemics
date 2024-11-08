using System.Diagnostics.CodeAnalysis;
using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Interactives;

namespace WorldOfEpidemics.Core;

/// <summary>
/// Holds all data relavant to this session.
/// </summary>
public class Context : IContextModifier
{
    private Room room;
    
    public Context(IUserController userController, Room startingRoom, Virus virus)
    {
        UserController = userController;
        CurrentRoom = startingRoom;
        CurrentVirus = virus;
    }
    
    public Container? CurrentContainer { get; set; }
    public Container Inventory {get;} = new("Inventory", "Contains inventory");
    public bool IsDone { get; private set; }
    public IUserController UserController { get; private set; }
    public Virus CurrentVirus { get; private set; }
    public Task[] TaskArray{ get; private set; }
    
    public void Service()
    {
        UserController.RunCommand(this);
    }
    
    #region IContextModifier
    public Room CurrentRoom
    {
        get => room;
        [MemberNotNull(nameof(room))]
        private set
        { 
            CurrentContainer = null;
            room = value;
            UserController.AnnounceRoom(room);
        }
    }
    
    public void ChangeRoom(Room room)
    {
        if (CurrentRoom == room)
        {
            UserController.WriteText(Properties.Resources.MessageEnterSameRoom);
            return;
        }
        UserController.WriteText(CurrentRoom.GetGoodbye());
        CurrentRoom = room;
    }

    public void Exit()
    {
        IsDone = true;
    }
    #endregion
}

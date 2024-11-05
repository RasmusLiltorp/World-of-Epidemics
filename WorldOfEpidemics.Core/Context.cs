using System.Diagnostics.CodeAnalysis;
using WorldOfEpidemics.Core.Abstractions;

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

    public bool IsDone { get; private set; }
    public IUserController UserController { get; private set; }

    public Virus CurrentVirus { get; private set; }
    
    public void Service()
    {
        // Update virus state by calling its Service method
        CurrentVirus.Service();

        // Run user commands or other session-specific logic
        UserController.RunCommand(this);

        // Optional: Display virus stats
        Console.WriteLine($"Virus: {CurrentVirus.virusName} | Infected: {CurrentVirus.numOfInfected} | Death Toll: {CurrentVirus.deathToll}");
    }

    #region IContextModifier
    public Room CurrentRoom
    {
        get => room;
        [MemberNotNull(nameof(room))]
        private set
        {
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

using System.Diagnostics.CodeAnalysis;
using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Interactives;

namespace WorldOfEpidemics.Core;

/// <summary>
/// Holds all data relavant to this session.
/// </summary>
public class Context : IContextModifier
{
    private Room currentRoom;
    private Event? currentEvent;
    
    public Context(IUserController userController, Room startingRoom, Virus virus)
    {
        UserController = userController;
        CurrentRoom = startingRoom;
        CurrentVirus = virus;
        Effects = [];
    }
    
    
    public bool IsDone { get; private set; }
    public IUserController UserController { get; private set; }
    public Virus CurrentVirus { get; private set; }
    
    public void Service()
    {
        if (!IsEventRaised)
            UserController.RunCommand(this);
        else if (UserController.TryFinishGetDecision(out string? decision))
        {
            if (currentEvent.Choices.TryGetValue(decision, out IEnumerable<Effect>? effects))
            {
                // TODO (SIA): Modify virus based on event effects vs active effects (in Effects).
            }

            currentEvent = null;
        }
    }

    #region IContextModifier
    public Room CurrentRoom
    {
        get => currentRoom;
        [MemberNotNull(nameof(currentRoom))]
        private set
        {
            currentRoom = value;
            UserController.AnnounceRoom(currentRoom);
            currentEvent = currentRoom.GetEvent();
            if (currentEvent != null)
            {
                UserController.StartGetDecision(currentEvent.DecisionMessage, currentEvent.Choices.Select(c => c.Key).ToList());
            }
        }
    }
    public Container CurrentContainer { get; set; }
    public Dictionary<Effect, int> Effects { get; private set; }
    [MemberNotNullWhen(true, nameof(currentEvent))]
    public bool IsEventRaised => currentEvent != null;

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

public enum Effect
{
    Masked,
    Disinfected
}

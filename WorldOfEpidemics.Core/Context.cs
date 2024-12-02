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
        ActionList.ObjectiveList.SetUser(userController);
    }
    
    public Container Inventory {get;} = new("Inventory", "Contains inventory");
    public bool IsDone { get; private set; }
    public IUserController UserController { get; private set; }
    public Virus CurrentVirus { get; private set; }
    public ObjectiveList[] TaskArray { get; private set; }
    
    public void Service()
    {
        if (!IsEventRaised)
            UserController.RunCommand(this);
        else if (UserController.TryFinishGetDecision(out string? decision))
        {
            // When we are here; the user has made their choice.
            if (currentEvent.Choices.TryGetValue(decision, out Event.ChoiceResult? choice))
            {
                ApplyChoice(choice);

                if (choice.HasMessage)
                    UserController.WriteText(choice.Message);
            }

            currentEvent = null;
        }
    }

    private void ApplyChoice(Event.ChoiceResult choice)
    {
        double modifier = 0;
        var choiceEffectsCount = choice.PositiveEffects.Count();

        try
        {
            // If no positive effects; apply choice modifier straight away.
            if (choiceEffectsCount == 0)
            {
                modifier = choice.VirusModifier;

                return;
            }

            // Get all effects that would be good to have for the choice, but the user doesn't have.
            var nonSatisfiedChoiceEffects = choice.PositiveEffects
                .Where(e => !Effects.ContainsKey(e))
                .ToList();

            // If the user has all effects; make the modifer negative.
            if (nonSatisfiedChoiceEffects.Count == 0)
            {
                modifier = -choice.VirusModifier;

                return;
            }

            // If the user has no positive effects; hit twice as hard if multiple options; otherwise use the given modifer.
            if (nonSatisfiedChoiceEffects.Count == choiceEffectsCount)
            {
                if (nonSatisfiedChoiceEffects.Count > 1)
                {
                    modifier = choice.VirusModifier * 2;

                    return;
                }

                modifier = choice.VirusModifier;
            }

            // The player have one or more positive effects, but not them all; apply reduced modifer.
            modifier = choice.VirusModifier / (choiceEffectsCount - nonSatisfiedChoiceEffects.Count + 1);
        }
        finally
        {
            CurrentVirus.ModifyInfectionRate(modifier);
        }
    }
    
    #region IContextModifier
    public Room CurrentRoom
    {
        get => currentRoom;
        [MemberNotNull(nameof(currentRoom))]
        private set
        {
            CurrentContainer = null;
            currentRoom = value;
            UserController.AnnounceRoom(currentRoom);
            currentEvent = currentRoom.GetEvent();
            if (currentEvent != null)
            {
                var availableChoices = currentEvent.Choices.Where(c => c.Value.HasRequiredEffects(Effects.Select(e => e.Key)));
                UserController.StartGetDecision(currentEvent.DecisionMessage, availableChoices.Select(c => c.Key).ToList());
            }
        }
    }
    public Container? CurrentContainer { get; set; }
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
    Disinfected,
    Gloved
}

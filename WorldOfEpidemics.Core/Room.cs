using WorldOfEpidemics.Core.Interactives;

namespace WorldOfEpidemics.Core;

public class Room
{
    private List<IInteractable> interactables = [];
    private List<Doorway> doorways = [];
    private Event? @event;

    public Room(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
    public IReadOnlyList<IInteractable> Interactables => interactables;
    public IReadOnlyList<Doorway> Doorways => doorways;

    public void AddDoor(string name, Room room)
    {
        doorways.Add(new Doorway() { Name = name, Room = room });
    }

    public void AddInteractable(IInteractable interactable)
    {
        interactables.Add(interactable);
    }

    public void AddEvent(Event @event)
    {
        this.@event = @event;
    }

    public Room? FollowDoor(string direction)
    {
        return Doorways.Where(d => d.Name == direction).FirstOrDefault()?.Room;
    }

    /// <summary>
    /// Get welcome message upon entering.
    /// </summary>
    /// <returns></returns>
    public string GetWelcome() =>
        "You are now at " + Name;

    /// <summary>
    /// Get goodbye message upon leaving.
    /// </summary>
    /// <returns></returns>
    public string GetGoodbye() =>
        string.Empty;

    /// <summary>
    /// Get an event for the room.
    /// If the room doesn't have an event or another condition makes it so that none should happen now, this method will return <see langword="null"/>.
    /// </summary>
    /// <returns>Event to run when in the room; otherwise <see langword="null"/>.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Event? GetEvent() =>
        @event;
}

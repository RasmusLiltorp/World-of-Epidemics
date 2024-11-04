using WorldOfEpidemics.Core.Interactives;

namespace WorldOfEpidemics.Core;

public class Room
{
    private List<IInteractable> interactables = [];
    private List<Doorway> doorways = [];

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

    public Room? FollowDoor(string direction)
    {
        return Doorways.Where(d => d.Name == direction).FirstOrDefault()?.Room;
    }

    public void AddInteractable(IInteractable interactable)
    {
        interactables.Add(interactable);
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
}

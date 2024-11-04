/* Room class for creating rooms
 */

using WorldOfEpidemics.Core.Interactives;

namespace WorldOfEpidemics.Core;

public class Room {
  public string name;
  private Dictionary<string, Room> doorways = new Dictionary<string, Room>();
  private List<IInteractable> interactables = new List<IInteractable>(); 
  
  public Room (string name) {
    this.name = name;
  }
  
  public String GetName () {
    return name;
  }
  
  public void AddDoor(string name, Room room) {
    doorways.Add(name, room);
  }
  
  public Room FollowDoor(string direction) {
    return doorways[direction];
  }

  public void AddInteractable(IInteractable interactable)
  {
    interactables.Add(interactable);
  }
  
  // provides text for which interactable is located in room
  public IInteractable GetInteractable(string name)
  {
      foreach (var interactable in interactables)
      {
          if (interactable.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
          {
              return interactable;
          }
      }
      return null; // if no interactable is found
  }

  

  public void Welcome () 
  {
    Console.WriteLine("You are now at "+name); //Print all doorways near current room
    HashSet<string> exits = doorways.Keys.ToHashSet();
    Console.WriteLine("Current exits are:");
    foreach (String exit in exits) 
    {
      Console.WriteLine(" - "+exit);
    }

    if(interactables.Count == 0) //Check if the current room holds no interactables
    {
      Console.WriteLine("\nThe room is empty");
      return; //If empty, skip the rest of Welcome()
    }

    Console.WriteLine("\nYou take a look at your surroundings. You see:"); //Print all interactables in current room upon entering
    for (int i = 0; i < interactables.Count; i++)
    {
        Console.Write(interactables[i].Name);
        if (i < interactables.Count - 1)
        {
            Console.Write(", ");
        }
    }
    Console.WriteLine();
  }

  public void Goodbye () {
  }
}
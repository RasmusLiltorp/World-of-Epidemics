/* Room class for creating rooms
 */

namespace WorldOfEpidemics.Core;

public class Room {
  public string name;
  private Dictionary<string, Room> edges = new Dictionary<string, Room>();
  private List<string> interactables = new List<string>();
  
  public Room (string name) {
    this.name = name;
  }
  
  public String GetName () {
    return name;
  }
  
  public void AddEdge (string name, Room room) {
    edges.Add(name, room);
  }
  
  public Room FollowEdge (string direction) {
    return edges[direction];
  }

  public void AddInteractable(string name)
  {
    interactables.Add(name);
  }

  public void Welcome () 
  {
    Console.WriteLine("You are now at "+name); //Print all edges near current room
    HashSet<string> exits = edges.Keys.ToHashSet();
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
    foreach (String interactable in interactables)
    {
      Console.WriteLine(" - "+interactable);
    }
  }

  public void Goodbye () {
  }
}
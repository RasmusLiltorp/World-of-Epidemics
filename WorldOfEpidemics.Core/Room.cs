/* Room class for creating rooms
 */

namespace WorldOfEpidemics.Core;

public class Room {
  protected string name;
  protected Dictionary<string, Room> edges = new Dictionary<string, Room>();
  
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

  public void Welcome () {
    Console.WriteLine("You are now at "+name);
    HashSet<string> exits = edges.Keys.ToHashSet();
    Console.WriteLine("Current exits are:");
    foreach (String exit in exits) {
      Console.WriteLine(" - "+exit);
    }
  }

  public void Goodbye () {
  }
}
/* World class for modeling the entire in-game world
 */

namespace WorldOfEpidemics.Core;

public class World {
  Room entry;
  
  public World () {
    Room entry    = new Room("Entry");
    Room corridor = new Room("Corridor");
    Room cave     = new Room("Cave");
    Room pit      = new Room("Darkest Pit");
    Room outside  = new Room("Outside");
    
    entry.AddEdge("door", corridor);
    corridor.AddEdge("door", cave);
    cave.AddEdge("north", pit);
    cave.AddEdge("south", outside);
    pit.AddEdge("door", cave);
    outside.AddEdge("door", cave);
    
    this.entry = entry;
  }
  
  public Room GetEntry () {
    return entry;
  }
}


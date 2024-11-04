/* World class for modeling the entire in-game world
 */

using WorldOfEpidemics.Core.Interactives;

namespace WorldOfEpidemics.Core;

public class World {
  Room entry;
  
  public World () {
    Room entry    = new Room("Entry");
    Room corridor = new Room("Corridor");
    Room cave     = new Room("Cave");
    Room pit      = new Room("Darkest Pit");
    Room outside  = new Room("Outside");
    
    entry.AddDoor("door", corridor);
    corridor.AddDoor("door", cave);
    cave.AddDoor("north", pit);
    cave.AddDoor("south", outside);
    pit.AddDoor("door", cave);
    outside.AddDoor("door", cave);

    Item handSanitizer = new Item("Hand Sanitizer", "Apply to sanitize your hands");
    corridor.AddInteractable(handSanitizer);
    
    this.entry = entry;
  }
  
  public Room GetEntry () {
    return entry;
  }
}


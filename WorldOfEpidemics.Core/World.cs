/* World class for modeling the entire in-game world
 */

using WorldOfEpidemics.Core.Interactives;

namespace WorldOfEpidemics.Core;

public class World {
  Room bedroom;
  
  public World () { 
    // Item list
    Item handSanitizer = new Item("hand sanitizer", "Apply to sanitize your hands");
    Item sink = new Item("sink", "Use to rinse your hands");
    Item soap = new Item("soap", "Apply to wash your hands");
    Item towel = new Item("towel", "Use to dry your hands");
    Item gloves = new Item("gloves", "Apply to protect your hands");
    Item fridge = new Item("fridge", "Open to get food");
    Item dishwasher = new Item("dishwasher", "Cleans dirty dishes");
    Item sofa = new Item("sofa", "Relax in your sofa");
    Item television = new Item("television", "Watch the news");
    Item window = new Item("window", "Admire the view");
    Item drawer = new Item("drawer", "Open your drawer");
    Item mask = new Item("mask", "Equip your mask");
    Item bed = new Item("bed", "Sleep in your bed");
    Item pc = new Item("pc", "Use your computer");
    Item book = new Item("book", "Read in your book");
    Item toilet = new Item("toilet", "Excuse yourself");
    Item toiletPaper = new Item("toilet paper", "Use toilet paper");
    Item bath = new Item("bath", "Have a nice bath");
    Item bench = new Item("bench", "Sit on the public bench");
    Item bus = new Item("bus", "Take the bus");
    Item tree = new Item("tree", "Admire the tree");
    Item person = new Item("person", "Interact with person");
    Item food = new Item("food", "Eat food");
      
    // Room list  
    Room bedroom    = new Room("Bedroom");
    Room livingRoom = new Room("Living room");
    Room kitchen     = new Room("Kitchen");
    Room wc      = new Room("WC");
    Room outside  = new Room("Outside");
    Room testCenter = new Room("Test Center");
    Room supermarket = new Room("Supermarket");
    Room restaurant = new Room("Restaurant");
    Room school    = new Room("School");
    
    // Bedroom
    bedroom.AddDoor("door", livingRoom);
    
    // Living room
    livingRoom.AddDoor("wc", wc);
    livingRoom.AddDoor("kitchen", kitchen);
    livingRoom.AddDoor("bedroom", bedroom);
    livingRoom.AddDoor("outside", outside);
    
    // WC
    wc.AddDoor("living room", livingRoom);
    
    // Kitchen
    kitchen.AddDoor("living room", livingRoom);
    kitchen.AddInteractable(handSanitizer);
    
    // Outside
    outside.AddDoor("house", livingRoom);
    outside.AddDoor("testcenter", testCenter);
    outside.AddDoor("supermarket", supermarket);
    outside.AddDoor("restaurant", restaurant);
    outside.AddDoor("school", school);
    
    // Testcenter
    testCenter.AddDoor("outside", outside);
    
    // School
    school.AddDoor("outside", outside);
    
    // Supermarket
    supermarket.AddDoor("outside", outside);
    
    // Restaurant
    restaurant.AddDoor("outside", outside);
    
    
    
    this.bedroom = bedroom;
  }
  
  public Room GetEntry () {
    return bedroom;
  }
}


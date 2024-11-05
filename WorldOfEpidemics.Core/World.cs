/* World class for modeling the entire in-game world
 */

using WorldOfEpidemics.Core.Interactives;
using Action = System.Action;

namespace WorldOfEpidemics.Core;

public class World {
  Room bedroom;
  
  public World () { 
    // Item list (Inventory items)
    Item handSanitizer = new("hand sanitizer", "Apply to sanitize your hands");
    Item gloves = new("gloves", "Apply to protect your hands");
    Item mask = new("mask", "Equip your mask");
    Item food = new("food", "Eat food");
    
    // Action item
    ActionItem dishwasher = new(name: "dishwasher", description: "Cleans dirty dishes", action: ActionList.UseDishwasher);
    ActionItem sink = new("sink", "Use to rinse your hands", action: ActionList.UseDishwasher);
    ActionItem sofa = new("sofa", "Relax in your sofa", action: ActionList.UseDishwasher);
    ActionItem  bath = new("bath", "Have a nice warm bath", action: ActionList.UseDishwasher);
    ActionItem person = new("person", "Interact with person", action: ActionList.UseDishwasher);
    ActionItem tree = new("tree", "Admire the tree", action:ActionList.UseDishwasher);
    ActionItem bench = new("bench", "Sit on the public bench", action: ActionList.UseDishwasher);
    ActionItem bus = new("bus", "Take the bus", action: ActionList.UseDishwasher);
    ActionItem toilet = new("toilet", "Excuse yourself", action: ActionList.UseDishwasher);
    ActionItem toiletPaper = new("toilet paper", "Use toilet paper", action: ActionList.UseDishwasher);
    ActionItem book = new("book", "Read in your book", action: ActionList.UseDishwasher);
    ActionItem window = new("window", "Admire the view", action: ActionList.UseDishwasher);
    ActionItem pc = new("pc", "Use your computer", action: ActionList.UseDishwasher);
    ActionItem television = new("television", "Watch the news", action: ActionList.UseDishwasher);
    ActionItem table = new("table", "Sit at the table", action: ActionList.UseDishwasher);
    ActionItem bed = new("bed", "Sleep in your bed", action: ActionList.UseDishwasher);
    ActionItem towel = new("towel", "Use to dry your hands", action: ActionList.UseDishwasher);
    ActionItem soap = new("soap", "Apply to wash your hands", action: ActionList.UseDishwasher);
    
    // Containers
    Container bedroomDrawer = new Container("drawer", "Open to see items inside");
    bedroomDrawer.AddInteractable(book);
    
    Container livingRoomShelf = new Container("shelf", "Look at to see items inside");
    livingRoomShelf.AddInteractable(mask);
    
    Container wcDrawer = new Container("drawer", "Open to see items inside");
    wcDrawer.AddInteractable(gloves);
    
    Container fridge = new Container("fridge", "Open to see items inside");
    fridge.AddInteractable(food);
    
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
    bedroom.AddInteractable(bed);
    bedroom.AddInteractable(pc);
    bedroom.AddInteractable(window);
    bedroom.AddInteractable(bedroomDrawer);
    
    // Living room
    livingRoom.AddDoor("wc", wc);
    livingRoom.AddDoor("kitchen", kitchen);
    livingRoom.AddDoor("bedroom", bedroom);
    livingRoom.AddDoor("outside", outside);
    livingRoom.AddInteractable(television);
    livingRoom.AddInteractable(sofa);
    livingRoom.AddInteractable(window);
    livingRoom.AddInteractable(livingRoomShelf);
    
    // WC
    wc.AddDoor("door", livingRoom);
    wc.AddInteractable(toilet);
    wc.AddInteractable(soap);
    wc.AddInteractable(handSanitizer);
    wc.AddInteractable(sink);
    wc.AddInteractable(wcDrawer);
    wc.AddInteractable(toiletPaper);
    wc.AddInteractable(bath);
    
    // Kitchen
    kitchen.AddDoor("door", livingRoom);
    kitchen.AddInteractable(handSanitizer);
    kitchen.AddInteractable(gloves);
    kitchen.AddInteractable(towel);
    kitchen.AddInteractable(sink);
    kitchen.AddInteractable(fridge);
    kitchen.AddInteractable(table);
    kitchen.AddInteractable(soap);
    kitchen.AddInteractable(dishwasher);
    kitchen.AddInteractable(window);
    
    // Outside
    outside.AddDoor("house", livingRoom);
    outside.AddDoor("testcenter", testCenter);
    outside.AddDoor("supermarket", supermarket);
    outside.AddDoor("restaurant", restaurant);
    outside.AddDoor("school", school);
    outside.AddInteractable(bench);
    outside.AddInteractable(tree);
    outside.AddInteractable(person);
    outside.AddInteractable(bus);
    
    // Testcenter
    testCenter.AddDoor("outside", outside);
    testCenter.AddInteractable(handSanitizer);
    
    // School
    school.AddDoor("outside", outside);
    school.AddInteractable(handSanitizer);
    
    // Supermarket
    supermarket.AddDoor("outside", outside);
    supermarket.AddInteractable(handSanitizer);
    supermarket.AddInteractable(food);
    
    // Restaurant
    restaurant.AddDoor("outside", outside);
    restaurant.AddInteractable(handSanitizer);
    supermarket.AddInteractable(food);
    this.bedroom = bedroom;
  }
  
  public Room GetEntry () {
    return bedroom;
  }
}


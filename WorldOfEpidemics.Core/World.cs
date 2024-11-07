/* World class for modeling the entire in-game world
 */

using WorldOfEpidemics.Core.Interactives;
using Action = System.Action;

namespace WorldOfEpidemics.Core;

public class World {
  Room bedroom;
  
  public World () { 
    // Item list (Inventory items)
    Item handSanitizer = new("handsanitizer", "Apply to sanitize your hands", 0.98);
    Item gloves = new("gloves", "Apply to protect your hands", 0.99);
    Item mask = new("mask", "Equip your mask", 0.99);
    
    // Action item
    ActionItem dishwasher = new(name: "dishwasher", description: "Cleans dirty dishes", action: ActionList.UseDishwasher);
    ActionItem sink = new("sink", "Use to rinse your hands", action: ActionList.UseSink);
    ActionItem sofa = new("sofa", "Relax in your sofa", action: ActionList.UseSofa);
    ActionItem  bath = new("bath", "Have a nice warm bath", action: ActionList.UseBath);
    ActionItem person = new("person", "Interact with person", action: ActionList.InteractPerson);
    ActionItem tree = new("tree", "Admire the tree", action:ActionList.InteractTree);
    ActionItem bench = new("bench", "Sit on the public bench", action: ActionList.UseBench);
    ActionItem bus = new("bus", "Take the bus", action: ActionList.UseBus);
    ActionItem toilet = new("toilet", "Excuse yourself", action: ActionList.UseToilet);
    ActionItem toiletPaper = new("toiletpaper", "Use toilet paper", action: ActionList.UseTP);
    ActionItem book = new("book", "Read in your book", action: ActionList.ReadBook);
    ActionItem window = new("window", "Admire the view", action: ActionList.WatchWindow);
    ActionItem pc = new("pc", "Use your computer", action: ActionList.UsePC);
    ActionItem television = new("television", "Watch the news", action: ActionList.UseTV);
    ActionItem table = new("table", "Sit at the table", action: ActionList.UseTable);
    ActionItem bed = new("bed", "Sleep in your bed", action: ActionList.UseBed);
    ActionItem towel = new("towel", "Use to dry your hands", action: ActionList.UseTowel);
    ActionItem soap = new("soap", "Apply to wash your hands", action: ActionList.UseSoap);
    ActionItem food = new("food", "Eat some food", action: ActionList.EatFood);
    
    // Containers
    Container bedroomDrawer = new Container("drawer", "Open to see items inside");
    bedroomDrawer.AddInteractable(book);
    
    Container livingRoomShelf = new Container("shelf", "Look at to see items inside");
    livingRoomShelf.AddInteractable(mask);
    
    Container wcDrawer = new Container("drawer", "Open to see items inside");
    wcDrawer.AddInteractable(gloves);
    
    Container fridge = new Container("fridge", "Open to see items inside");
    fridge.AddInteractable(food);

    // Events
    Event randomVirusFactEvent = new("Do you want to here a fact about viruses?", new()
    {
        { "Yes", new([], () => "This is a random fact.") }, // TODO (SIA): Pass method that returns a random fact.
        { "No, maybe later.", new([]) }
    }, 0);

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
    school.AddEvent(randomVirusFactEvent);
    
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


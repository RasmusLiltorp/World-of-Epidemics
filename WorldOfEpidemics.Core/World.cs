/* World class for modeling the entire in-game world
 */

using WorldOfEpidemics.Core.Interactives;
using Action = System.Action;

namespace WorldOfEpidemics.Core;

public class World {
  Room bedroom;

  public World()
  {
      // Item list (Inventory items)
      Item handSanitizer = new("handsanitizer", "Apply to sanitize your hands", 0.98);
      Item gloves = new("gloves", "Apply to protect your hands", 0.99);
      Item mask = new("mask", "Equip your mask", 0.99);

      // Action item
      ActionItem dishwasher = new(name: "dishwasher", description: "Cleans dirty dishes", action: ActionList.UseDishwasher);
      ActionItem sink = new("sink", "Use to rinse your hands", action: ActionList.UseSink);
      ActionItem sofa = new("sofa", "Relax in your sofa", action: ActionList.UseSofa);
      ActionItem bath = new("bath", "Have a nice warm bath", action: ActionList.UseBath);
      //ActionItem person = new("person", "Interact with person", action: ActionList.InteractPerson);
      ActionItem tree = new("tree", "Admire the tree", action: ActionList.InteractTree);
      ActionItem bench = new("bench", "Sit on the public bench", action: ActionList.UseBench);
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
      ActionItem bus = new("bus", "Take the bus", action: ActionList.UseBus);
      ActionItem classRoom = new("classroom", "Go to class", action: ActionList.GoClass);

      // Containers
      Container bedroomDrawer = new Container("drawer", "Open to see items inside");
      bedroomDrawer.AddInteractable(book);

      Container livingRoomShelf = new Container("shelf", "Look at to see items inside");
      livingRoomShelf.AddInteractable(mask);

      Container wcDrawer = new Container("drawer", "Open to see items inside");
      wcDrawer.AddInteractable(gloves);

      // Room list  
      Room bedroom = new Room("Bedroom");
      Room livingRoom = new Room("Living room");
      Room kitchen = new Room("Kitchen");
      Room wc = new Room("WC");
      Room outside = new Room("Outside");
      Room testCenter = new Room("Test Center");
      Room supermarket = new Room("Supermarket");
      Room restaurant = new Room("Restaurant");
      Room school = new Room("School");

      ActionItem food = new("food", "Eat some food", (contextModifier, user) =>
      {
          if (ActionList.task.isCompleted("schoolTask"))
          {
              user.WriteText("You eat dinner");
              user.WriteText("Suddenly you feel really sleepy. You should go to bed to start a new day.");
              ActionList.task.CompleteTask("dinnerTask");

          }
          else if (ActionList.task.isCompleted("wcTask"))
          {
              user.WriteText("You eat your breakfast.");
              ActionList.task.CompleteTask("breakfastTask");
              Event schoolEvent = new("Do you want to hear a random virus fact?", new()
              {
                  {
                      "Yes", new([], () =>
                      {
                          ActionList.task.CompleteTask("schoolTask");
                          return
                              "Did you know that viruses aren't technically \"alive\"? They can't reproduce or carry out metabolic processes on their own and need to hijack a host cell's machinery to make copies of themselves.";
                      })
                  },
                  { "No", new([]) }
              }, 0);
              school.AddEvent(schoolEvent);
          }
          else
          {
              user.WriteText("You really have to use the toilet first.");
          }
      });
      Container fridge = new Container("fridge", "Open to see items inside");
      fridge.AddInteractable(food);

      //TODO: Add check for if masked when getting tested, as you should wear a mask inside. Also modify infection rate based on answer. Unlock answers by having necessary effect
      Event testcenterEvent = new("You are walking into the test center. Do you want to get tested?", new()
      {
          {
              "Yes", new([], () =>
              {
                  return "You tested negative. You can proceed your day as usual.";
              })
          },
          {
              "Yes, and I'm wearing my mask while waiting", new([Effect.Masked], () =>
              {
                  //TODO: (IDEA) If you test positive, maybe a chance based on infectionRate, you have to go home again and sleep to reset day.
                  //TODO: Should have benefit if you go home and sleep, and big consequences if you don't. Could generate infected effect on the start of each day.
                  return "You tested negative. You can proceed your day as usual.";
              })
          },
          {
              "No", new([], () =>
              {
                  return "You decide to not get tested";
              })
          }
      }, 0);
      
      //TODO: modify infection rate based on answer, unlock answers by having necessary effect
      Event interactPerson = new("You see a classmate waiting for the bus outside. Do you want to speak to them?", new()
      {
          {
              "Yes", new([], () =>
              {
                  return "Your classmate tells you that you should wear a mask. They do not want to risk getting infected";
              })
          },
          {
              "Yes, and I'm wearing my mask", new([Effect.Masked], () =>
              {
                  return "You have a pleasant conversation about disinfection. They tell you how hand sanitizer is important to minimize infection.";
              })
          },
          {
              "No", new([], () =>
              {
                  return "Your classmate looks disappointed.";
              })
          }}, 0);
      
      //TODO: Modify infectionrate based on answer, unlock answers by having necessary effect
      Event eatBuffet = new("You go to the local buffet. There is a big group of people. Do you want to eat here?", new()
      {
          {
              "Yes", new([], () =>
              {
                  restaurant.AddInteractable(food);
                  return "You get a table at the buffet. The other people eating at the buffet, are not taking any precautions. You can now interact with food";
              })
          },
          {
              "Yes, and I'm wearing my mask, gloves and using my hand sanitizer", new([Effect.Masked, Effect.Disinfected, Effect.Gloved], () =>
              {
                  restaurant.AddInteractable(food);
                  return "You feel well prepared to eat at the buffet. The rest of the people are also taking precautions. You can now interact with food";
              })
          },
          {
              "Yes, and I'm wearing my mask and using my hand sanitizer", new([Effect.Masked, Effect.Disinfected], () =>
              {
                  restaurant.AddInteractable(food);
                  return "You feel prepared to eat at the buffet. The rest of the people are also taking some precautions. You can now interact with food";
              })
          },
          {
              "No", new([], () =>
              {
                  return "You decide to eat at home.";
              })
          }}, 0);
      
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
        outside.AddDoor("testcenter", testCenter);
        outside.AddInteractable(bench);
        outside.AddInteractable(tree);
        outside.AddInteractable(bus);
        outside.AddEvent(interactPerson);
        
        // Test center
        testCenter.AddDoor("outside", outside);
        testCenter.AddInteractable(handSanitizer);
        testCenter.AddEvent(testcenterEvent);
        
        // School
        school.AddDoor("outside", outside);
        school.AddInteractable(handSanitizer);
        school.AddInteractable(classRoom);
        
        // Supermarket
        supermarket.AddDoor("outside", outside);
        supermarket.AddInteractable(handSanitizer);
        supermarket.AddInteractable(food);
        
        // Restaurant
        restaurant.AddDoor("outside", outside);
        restaurant.AddInteractable(handSanitizer);
        restaurant.AddEvent(eatBuffet);
        this.bedroom = bedroom;
  }
  
  public Room GetEntry () {
    return bedroom;
  }
}


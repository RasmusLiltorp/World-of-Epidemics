/* World class for modeling the entire in-game world
 */

using WorldOfEpidemics.Core.Definitions;
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
    
    // Events
    ActionItem food = new("food", "Eat some food", (contextModifier, user) =>
    {
        if (ActionList.ObjectiveList.IsCompleted(ObjectiveDefinitions.GoToSchool))
        {
            user.WriteText("You eat dinner");
            user.WriteText("Suddenly you feel really sleepy. You should go to bed to start a new day.");
            ActionList.ObjectiveList.CompleteObjective(ObjectiveDefinitions.HaveDinner);
        }
        else if (ActionList.ObjectiveList.IsCompleted(ObjectiveDefinitions.UseTheToilet))
        {
            user.WriteText("You eat your breakfast.");
            ActionList.ObjectiveList.CompleteObjective(ObjectiveDefinitions.EatBreakFast);
            Event schoolEvent = new("You attend school. What precautions do you take?", new()
            {
                {
                    "None", new([], "You went to class without taking any precautions. This led to an increase in the virusses infection rate.",
                    () =>
                    {
                        ActionList.ObjectiveList.CompleteObjective(ObjectiveDefinitions.GoToSchool);
                        return "You attend class without taking any precautions. Your classmates avoid you all day.";
                    }, 0.05)
                },
                {
                    "I'm wearing my mask", new([Effect.Masked], "You went to class wearing a mask which helped contain the virus, but you could've done more.", 
                    () =>
                    {
                        ActionList.ObjectiveList.CompleteObjective(ObjectiveDefinitions.GoToSchool);
                        return "You attend class while wearing a mask. Your classmates are wearing masks too.";
                    })
                },
                {
                    "I'm wearing my mask and using my hand sanitizer", new([Effect.Masked, Effect.Disinfected], "You went to class wearing a mask and having sanitized your hands. This is a sure way to help contain the virus", 
                    () =>
                    {
                        ActionList.ObjectiveList.CompleteObjective(ObjectiveDefinitions.GoToSchool);
                        return "You attend class while wearing a mask. Your classmates are wearing masks too. Before entering the classroom, you apply your hand sanitizer.";
                })
            }
            }, 0.03);
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
      Event testcenterEvent = new("You are walking into the test center. Currently getting tested isn't mandatory, but you feel like getting tested would be a good idea. The people waiting in line are all wearing masks. Do you want to get tested?", new()
      {
          {
              "Yes", new([], "You went to get tested, but you didn't wear a mask. This could have resulted in being infected",
              () =>
              {
                  return "You tested negative. You can proceed your day as usual.";
              })
          },
          {
              "Yes, and I'm wearing my mask while waiting", new([Effect.Masked], "You went to get tested and you wore a mask while waiting. The risk of being infected was insignificantly small",
              () =>
              {
                  return "You tested negative. You can proceed your day as usual.";
              })
          },
          {
              "No", new([], "You chose to not get tested. As a result you may have been carrying the virus without knowing",
              () =>
              {
                  return "You decide to not get tested";
              }, 0.06)
          }
      }, 0.01);
      
      //TODO: modify infection rate based on answer, unlock answers by having necessary effect
      Event interactPerson = new("You see a classmate waiting for the bus outside. He is wearing a mask, and it looks like he wants to speak with you. Do you want to speak to them?", new()
      {
          {
              "Yes", new([], "You approached your classmate without a mask. This could have resulted in either one of you being infected.",
              () =>
              {
                  return "Your classmate tells you that you should wear a mask. They do not want to risk getting infected";
              })
          },
          {
              "Yes, and I'm wearing my mask", new([Effect.Masked], "You approached you classmate while wearing a mask. This minimized the risk of infection.",
              () =>
              {
                  return "You have a pleasant conversation about disinfection. They tell you how hand sanitizer is important to minimize infection.";
              })
          },
          {
              "No", new([], "You chose to not approach your classmate. Keeping your distance is a sure way of avoiding infection.",
              () =>
              {
                  return "Your classmate looks disappointed.";
              }, 0)
          }}, 0.01);
      
      //TODO: Modify infectionrate based on answer, unlock answers by having necessary effect
      Event eatBuffet = new("You go to the local buffet. There is a big group of people. You feel as it would be a good idea to take as many precautions as possible. Do you want to eat here?", new()
      {
          {
              "Yes", new([], "You ate at the buffet without taking any precautions. This is a sure way of getting infected.",
              () =>
              {
                  restaurant.AddInteractable(food);
                  return "You get a table at the buffet. The other people eating at the buffet, are not taking any precautions. Food has now appeared as an available item";
              }, 0.05)
          },
          {
              "Yes, and I'm wearing my mask, gloves and using my hand sanitizer", new([Effect.Masked, Effect.Disinfected, Effect.Gloved], "You ate at the buffet while taking all the recommended precautions. The risk of being infected was insignificantly small.",
              () =>
              {
                  restaurant.AddInteractable(food);
                  return "You feel well prepared to eat at the buffet. The rest of the people are also taking precautions. Food has now appeared as an available item";
              })
          },
          {
              "Yes, and I'm wearing my mask and using my hand sanitizer", new([Effect.Masked, Effect.Disinfected], "You ate at the buffet wearing a mask and with sanitized hands. This minimized the risk of infection, but you could've done more.",
              () =>
              {
                  restaurant.AddInteractable(food);
                  return "You feel prepared to eat at the buffet. The rest of the people are also taking some precautions. Food has now appeared as an available item";
              })
          },
          {
              "Yes, and I'm wearing using my hand sanitizer", new([Effect.Masked, Effect.Disinfected], "You ate at the buffet sanitized hands. This minimized the risk of infection, but you could've done more.",
                  () =>
                  {
                      restaurant.AddInteractable(food);
                      return "You feel prepared to eat at the buffet. Some of the people are using hand sanitizer before picking up their food. Food has now appeared as an available item";
                  }, 0.01)
          },
          {
              "No", new([], "You chose to eat at home. This way you fully avoided being infected or infecting others.",
              () =>
              {
                  return "You decide to eat at home.";
              }, 0)
          }}, 0.01);
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


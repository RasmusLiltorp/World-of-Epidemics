using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Definitions;

namespace WorldOfEpidemics.Core.Interactives
{
    public static class ActionList
    {
        public static ObjectiveList ObjectiveList { get; } = new();
        
        public static void UseDishwasher(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You wash the dishes. Your dishes are sparkly clean");
        } 
        public static void UseSink(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You wash your hands. Your hands are now very clean!");
            contextModifier.AddEffect(Effect.Disinfected, 150);
        }
        public static void UseSofa(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You sit in your sofa. You think about the consequences of not wearing a mask in social situations.");
        }
        public static void UseBath(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You take a nice warm bath. You feel really clean.");
            contextModifier.AddEffect(Effect.Disinfected, 150);

        }
        /*public static void InteractPerson(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You talk to a person.");
        }*/
        public static void InteractTree(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You look at a tree. You see a squirrel hiding behind a branch. The squirrel threw an acorn at you.");
            if (!contextModifier.Inventory.interactables.Any(i => i.Name == "acorn"))
            {
                user.WriteText("You pick up the acorn");
                contextModifier.Inventory.AddInteractable(new Item("acorn", "It's useless", 1));
            }
        }
        public static void UseBench(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You relax at a nearby bench.");
            if (!contextModifier.Inventory.interactables.Any(i => i.Name == "handsanitizer"))
            {
                user.WriteText("Your classmate came by you and gave you his spare hand sanitizer. He says his mom accidentally bought too many online.");
                contextModifier.Inventory.AddInteractable(new Item("handsanitizer", "A hand sanitizer your classmate gave you while you sat on the bench", 0.99));
            }
        }
        public static void UseBus(IContextModifier contextModifier, IUser user, Room school)
        {
            if (ObjectiveList.IsCompleted(ObjectiveDefinitions.EatBreakFast))
            {
                user.WriteText("You take the bus.");
                contextModifier.ChangeRoom(school);
            }
            else
            {
                user.WriteText("You should probably get ready for school first.");
            }
        }
        public static void UseTP(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You use toilet paper.");
        }
        public static void ReadBook(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("*You read in your history book about the COVID-19 pandemic.*");
            user.WriteText("The people during the COVID-19 pandemic were forced to quarantine, as the virus had gotten out of control.");
            user.WriteText("Many places required masks, and schools and workplaces required you to get tested daily.");
            user.WriteText("*You think about COVID-19 and how it relates to the current epidemic.");
            user.WriteText("*You think that you could use this information to your advantage*");
        }
        public static void WatchWindow(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You look outside your window. The streets are empty, probably because of the current epidemic.");
            user.WriteText("You should definitely take precautions necessary before going outside.");
        }
        public static void UsePC(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("*You look for information about the epidemic on your pc.*");
            user.WriteText("It tells you that by keeping up with virus status you can check the current amount infections.");
            user.WriteText("By using necessary precautions such as hand sanitizer, masks and gloves, you can help minimize infections.");
            user.WriteText("By having any active effects you will unlock new ways of interacting with the world around you.");
        }
        public static void UseTV(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("*You watch the news*. The news explain that your actions impact the average person.");
            user.WriteText("The virus transmits itself in air, liquid and surfaces.");
            user.WriteText("You should at minimum wear a mask. Other precautions could also help protect you and others.");
        }
        public static void UseTable(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You sit at the table. You should probably prepare yourself for school instead.");
        }
        public static void UseTowel(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You dry your hands using the towel.");
        }
        public static void UseSoap(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You apply soap to your hands.");
            user.WriteText("This helps kill potential virus that may have made it onto your hand");
            contextModifier.AddEffect(Effect.Disinfected, 150);
        }
        public static void UseToilet(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You use the toilet.");
            user.WriteText("You have completed a task. Next task: Eat breakfast");
            ObjectiveList.CompleteObjective(ObjectiveDefinitions.UseTheToilet);
        }
        //public static void EatFood(IContextModifier contextModifier, IUser user)
        //{
        //    if (objective.isCompleted(ObjectiveDefinitions.GoToSchool))
        //    {
        //        user.WriteText("You eat dinner");
        //        user.WriteText("Suddenly you feel really sleepy. You should go to bed to start a new day.");
        //        objective.CompleteObjective(ObjectiveDefinitions.HaveDinner);
                
        //    }
        //    else if (objective.isCompleted(ObjectiveDefinitions.UseTheToilet))
        //    {
        //        user.WriteText("You eat your breakfast.");
        //        objective.CompleteObjective(ObjectiveDefinitions.EatBreakFast);
        //    }
        //    else
        //    {
        //        user.WriteText("You really have to use the toilet first.");
        //    }
        //}
        public static void GoClass(IContextModifier contextModifier, IUser user)
        {
            if (ObjectiveList.IsCompleted(ObjectiveDefinitions.GoToSchool))
            {
                user.WriteText("You go to class.");
                ObjectiveList.CompleteObjective(ObjectiveDefinitions.GoToSchool);
            }
            else
            {
                user.WriteText("You have other things you should do before going to class.");
            }
        }
        public static void UseBed(IContextModifier contextModifier, IUser user)
        {
            if (ObjectiveList.IsCompleted(ObjectiveDefinitions.HaveDinner))
            {
                user.WriteText("You lie in your bed. Slowly you fall asleep...");
                user.WriteText("A new day begins...");
                ObjectiveList.ResetObjectives();
                Item.ReAddItems();
                user.WriteText("You wake up in your bedroom. It seems your parents have stocked up all your supplies.");
            }
        }
    }
}
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Interactives
{
    public static class ActionList
    {
        public static ObjectiveList ObjectiveList { get; } = new();
        
        public static void UseDishwasher(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You wash the dishes.");
        } 
        public static void UseSink(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You wash your hands.");
            //TODO: Apply effect for rinsed hands mby
        }
        public static void UseSofa(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You sit in your sofa.");
        }
        public static void UseBath(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You take a nice warm bath.");
            //TODO: Add effect for clean - max 1 time pr. day
        }
        /*public static void InteractPerson(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You talk to a person.");
        }*/
        public static void InteractTree(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You look at a tree. You see a squirrel hiding behind a branch.");
        }
        public static void UseBench(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You relax at a nearby bench.");
        }
        public static void UseBus(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You take the bus.");
            //TODO: Event to go to school using bus?
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
            user.WriteText("It tells you, that by using 'status', that you can keep up with the current amount infections.");
            user.WriteText("By using necessary precautions such as hand sanitizer, masks and gloves, you can help minimize infections.");
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
            //TODO: Add soap effect
        }
        public static void UseToilet(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You use the toilet.");
            user.WriteText("You have completed a task. Next task: Eat breakfast");
            ObjectiveList.CompleteObjective("wcTask");
        }
       /* public static void EatFood(IContextModifier contextModifier, IUser user)
        {
            if (objective.isCompleted("schoolTask"))
            {
                user.WriteText("You eat dinner");
                user.WriteText("Suddenly you feel really sleepy. You should go to bed to start a new day.");
                objective.CompleteObjective("dinnerTask");
                
            }
            else if (objective.isCompleted("wcTask"))
            {
                user.WriteText("You eat your breakfast.");
                objective.CompleteObjective("breakfastTask");
            }
            else
            {
                user.WriteText("You really have to use the toilet first.");
            }
        }
        */
        public static void GoClass(IContextModifier contextModifier, IUser user)
        {
            if (ObjectiveList.IsCompleted("breakfastTask"))
            {
                user.WriteText("You go to class.");
                //TODO: Activate Event
                ObjectiveList.CompleteObjective("schoolTask");
            }
            else
            {
                user.WriteText("You have other things you should do before going to class.");
            }
        }
        public static void UseBed(IContextModifier contextModifier, IUser user)
        {
            if (ObjectiveList.IsCompleted("dinnerTask"))
            {
                user.WriteText("You lie in your bed. Slowly you fall asleep...");
                user.WriteText("A new day begins...");
                ObjectiveList.ResetObjectives();
                Item.ReAddItems();
            }
        }
    }
}
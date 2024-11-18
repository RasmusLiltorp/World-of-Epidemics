using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Interactives
{
    public static class ActionList
    {
        public static Task task = new Task();
        
        public static void UseDishwasher(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You wash the dishes.");
        } 
        public static void UseSink(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You wash your hands.");
        }
        public static void UseSofa(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You sit in your sofa.");
        }
        public static void UseBath(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You take a nice warm bath.");
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
        }
        public static void UseTP(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You use toilet paper.");
        }
        public static void ReadBook(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You read in your book.");
        }
        public static void WatchWindow(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You look outside your window and admire the sky.");
        }
        public static void UsePC(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You look at cat videos online for an entire hour.");
        }
        public static void UseTV(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You watch the news.");
        }
        public static void UseTable(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You sit at the table.");
        }
        public static void UseTowel(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You dry your hands using the towel.");
        }
        public static void UseSoap(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You apply soap to your hands.");
        }
        public static void UseToilet(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You use the toilet.");
            user.WriteText("You have completed a task. Next task: Eat breakfast");
            task.CompleteTask("wcTask");
        }
        public static void EatFood(IContextModifier contextModifier, IUser user)
        {
            if (task.isCompleted("schoolTask"))
            {
                user.WriteText("You eat dinner");
                user.WriteText("Suddenly you feel really sleepy. You should go to bed to start a new day.");
                task.CompleteTask("dinnerTask");
                
            }
            else if (task.isCompleted("wcTask"))
            {
                user.WriteText("You eat your breakfast.");
                task.CompleteTask("breakfastTask");
            }
            else
            {
                user.WriteText("You really have to use the toilet first.");
            }
        }
        public static void GoClass(IContextModifier contextModifier, IUser user)
        {
            if (task.isCompleted("breakfastTask"))
            {
                user.WriteText("You go to class.");
                //TODO: Activate Event
                task.CompleteTask("schoolTask");
            }
            else
            {
                user.WriteText("You have other things you should do before going to class.");
            }
        }
        public static void UseBed(IContextModifier contextModifier, IUser user)
        {
            if (task.isCompleted("dinnerTask"))
            {
                user.WriteText("You lie in your bed. Slowly you fall asleep...");
                user.WriteText("A new day begins...");
                task.ResetTasks();
            }
        }
    }
}
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Interactives
{
    public static class ActionList
    {
        public static void UseDishwasher(IContextModifier contextModifier, IUser user)
        {
            user.WriteText("You wash the dishes");
        }
    }
}
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Interactives
{
    public class ActionItem: IInteractable
    {
        private Action<IContextModifier, IUser> _action;
        public ActionItem(string name, string description, Action<IContextModifier, IUser> action)
        {
            Name = name;
            Description = description;
            _action = action;
        }
        public string Name { get; init; }
        public string Description { get; init; }

        public void Interact(IContextModifier contextModifier, IUser user)
        {
            _action(contextModifier, user);
            /*user.WriteText("You interacted with "+Name);
            contextModifier.Inventory.AddInteractable(this);
            throw new NotImplementedException();*/
        }
    }
}

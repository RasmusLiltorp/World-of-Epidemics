using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Interactives
{
    public class ActionItem: IInteractable
    {
        private Action<IContextModifier, IUser> _action;
        public ActionItem(string name, string description, Action<IContextModifier, IUser> action, bool isPickupable = false)
        {
            Name = name;
            Description = description;
            IsPickupable = isPickupable;
            _action = action;
        }
           
        public bool IsPickupable { get; private set; }
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
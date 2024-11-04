using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Interactives
{
    /// <summary>
    /// An interactable item that can modify the state of the game (i.e. the current context) when interacted with.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Name of the interactable item.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// A description that hints the player about the effects of the item.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Interact with the item. Its effects will be applied and come into effect through <paramref name="contextModifier"/> when this method is called.
        /// </summary>
        /// <param name="contextModifier">
        ///     Object implementing <see cref="IContextModifier"/>.
        ///     This object is respoonsible for applied the item effects to the <see cref="Context"/>.
        /// </param>
        public void Interact(string contextModifier);
    }
}

using WorldOfEpidemics.Core.Interactives;

namespace WorldOfEpidemics.Core.Abstractions
{
    /// <summary>
    /// Interface through which a <see cref="Context"/> can be modifend.
    /// </summary>
    public interface IContextModifier
    {
        public Room CurrentRoom { get; }
        public Container? CurrentContainer { get; set; }
        public Container Inventory { get; }
        public IReadOnlyDictionary<Effect, int> Effects { get; }
        public bool IsEventRaised { get; }

        /// <summary>
        /// Add an active effect to <see cref="Effects"/>.
        /// </summary>
        /// <param name="effect">The effect to apply.</param>
        /// <param name="duration">Duration of the effect in ticks.</param>
        public void AddEffect(Effect effect, int duration);

        /// <summary>
        /// Change the current rooom and announce this to the UI through <see cref="IUserController"/>.
        /// </summary>
        /// <param name="room">Room to set <see cref="CurrentRoom"/> to.</param>
        void ChangeRoom(Room room);

        /// <summary>
        /// Exit the game.
        /// </summary>
        void Exit();
    }
}

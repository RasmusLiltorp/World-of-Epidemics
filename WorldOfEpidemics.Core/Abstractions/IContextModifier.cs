using WorldOfEpidemics.Core.Interactives;

namespace WorldOfEpidemics.Core.Abstractions
{
    /// <summary>
    /// Interface through which a <see cref="Context"/> can be modifend.
    /// </summary>
    public interface IContextModifier
    {
        public Room CurrentRoom { get; }
        public Container CurrentContainer{ get; set; }
        public Dictionary<Effect, int> Effects { get; }
        public bool IsEventRaised { get; }
        
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

namespace WorldOfEpidemics.Core.Abstractions
{
    public interface IUserController : IUser
    {
        public void Initialize(Registry registry);

        /// <summary>
        /// Get the next command to be executed.
        /// </summary>
        public void RunCommand(IContextModifier context);

        /// <summary>
        /// Represent a change to a given <see cref="Room"/> in the UI.
        /// </summary>
        /// <param name="room"><see cref="Room"/> that the user/player have just entered.</param>
        public void AnnounceRoom(Room room);
    }
}

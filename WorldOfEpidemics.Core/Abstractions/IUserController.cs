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
        /// Represent a change to a given <see cref="Space"/> in the UI.
        /// </summary>
        /// <param name="room"><see cref="Space"/> that the user/player have just entered.</param>
        public void AnnounceRoom(Space room);
    }
}

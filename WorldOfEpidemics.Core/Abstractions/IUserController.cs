namespace WorldOfEpidemics.Core.Abstractions
{
    public interface IUserController : IUser
    {
        public void Initialize(Registry registry);

        /// <summary>
        /// Get the next command to be executed.
        /// </summary>
        public void RunCommand(IContextModifier context);
    }
}

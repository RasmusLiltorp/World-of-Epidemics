namespace WorldOfEpidemics.Core.Abstractions
{
    public interface IUserController
    {
        public void Initialize(Registry registry);

        /// <summary>
        /// Get the next command to be executed.
        /// </summary>
        public void RunCommand();

        /// <summary>
        /// Output text to the user.
        /// </summary>
        /// <param name="text">Text to write.</param>
        public void WriteText(string text);
    }
}

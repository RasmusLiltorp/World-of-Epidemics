namespace WorldOfEpidemics.Core.Abstractions
{
    public interface IUser
    {
        /// <summary>
        /// Output text to the user.
        /// </summary>
        /// <param name="text">Text to write.</param>
        public void WriteText(string text);

        /// <summary>
        /// Display a help message to the user.
        /// </summary>
        public void ShowHelp();
    }
}

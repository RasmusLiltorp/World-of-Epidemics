namespace WorldOfEpidemics.Core.Abstractions
{
    public interface IUser
    {
        /// <summary>
        /// Start chaching the input from <see cref="WriteText(string)"/> until <see cref="EndBatchWrite"/> or this is called again.
        /// </summary>
        public void BeginBatchWrite();

        /// <summary>
        /// Write chached text from <see cref="WriteText(string)"/> since last call to <see cref="BeginBatchWrite"/>.
        /// </summary>
        public void EndBatchWrite();

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

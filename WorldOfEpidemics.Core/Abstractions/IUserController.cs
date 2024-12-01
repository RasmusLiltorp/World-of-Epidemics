using System.Diagnostics.CodeAnalysis;

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

        /// <summary>
        /// Make the user make a decision.
        /// </summary>
        /// <param name="message">Message to the user asking them to make a decision.</param>
        /// <param name="choices">A list of choices that the user can make.</param>
        public void StartGetDecision(string message, IList<string> choices);

        /// <summary>
        /// Check if the user is done making a decision after a call to <see cref="StartGetDecision(string, IEnumerable{string})"/>.
        /// </summary>
        /// <param name="decision">The decision chosen by the user.</param>
        /// <returns><see langword="true"/> if the user is done making a decision; otherwise <see langword="false"/>.</returns>
        /// <remarks>This is always called repeatedly within the core assembly until <see langword="true"/> is returned.</remarks>
        public bool TryFinishGetDecision([MaybeNullWhen(false)] out string decision);

        /// <summary>
        /// Print lists of good and bad decisions
        /// </summary>
        /// <param name="GoodChoiceList">Message to the user asking them to make a decision.</param>
        /// <param name="BadChoiceList">A list of choices that the user can make.</param>
        public void ShowDecisionEvaluation(List<Event.ChoiceResult> GoodChoiceList, List<Event.ChoiceResult> BadChoiceList);
    }
}

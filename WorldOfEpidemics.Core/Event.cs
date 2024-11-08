using System.Diagnostics.CodeAnalysis;

namespace WorldOfEpidemics.Core;

/// <summary>
/// Something that the player should make a decision about.
/// </summary>
public class Event
{
    public Event(string decisionMessage, Dictionary<string, ChoiceResult> choices, double virusModifier)
    {
        DecisionMessage = decisionMessage;
        Choices = choices;
        VirusModifier = virusModifier;
    }

    /// <summary> {
    /// Message to set up the scenario and ask the player to make a choice.
    /// </summary>
    public string DecisionMessage { get; init; }
    /// <summary>
    /// Choices that the player can make and what <see cref="Effect"/>s will help minimize or improve the state of the <see cref="Virus"/>.
    /// </summary>
    public IDictionary<string, ChoiceResult> Choices { get; }
    /// <summary>
    /// How much this event will affect the <see cref="Virus"/>.
    /// </summary>
    public double VirusModifier { get; init; }

    /// <summary>
    /// The result of making a choice.
    /// </summary>
    public record ChoiceResult
    {
        private readonly Func<string>? messageFunc;

        public ChoiceResult(IEnumerable<Effect> effects, Func<string>? messageFunc = null)
        {
            Effects = effects;
            this.messageFunc = messageFunc;
        }

        /// <summary>
        /// Effects that are good to have in order to minimize spread of the virus.
        /// </summary>
        public IEnumerable<Effect> Effects { get; init; }
        /// <summary>
        /// Whether this choice should have a message shown.
        /// </summary>
        [MemberNotNullWhen(true, nameof(Message))]
        public bool HasMessage => messageFunc != null;
        /// <summary>
        /// Message displayed to he user when making the choice.
        /// </summary>
        public string? Message => messageFunc?.Invoke() ?? null;
    }
}

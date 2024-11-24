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

        // Associate choices with this event.
        foreach (var choice in Choices)
        {
            choice.Value.Event = this;
        }
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
    /// How much this event will affect the <see cref="Virus"/> (used if the <see cref="ChoiceResult"/> doesn't have its own specific modifier value).
    /// </summary>
    protected double VirusModifier { get; init; }

    /// <summary>
    /// The result of making a choice.
    /// </summary>
    public record ChoiceResult
    {
        private readonly Func<string>? messageFunc;

        private double? virusModifier;

        public ChoiceResult(IEnumerable<Effect> effects, Func<string>? messageFunc = null, double? virusModifier = null)
        {
            Effects = effects;
            this.messageFunc = messageFunc;
            this.virusModifier = virusModifier;
        }

        /// <summary>
        /// The <see cref="Core.Event"/> that owns this instance.
        /// </summary>
        public Event? Event { private get; set; }
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
        /// <summary>
        /// How much this choice will affect the <see cref="Virus"/>.
        /// </summary>
        public double VirusModifier => virusModifier ?? Event?.VirusModifier ?? 0;
    }
}

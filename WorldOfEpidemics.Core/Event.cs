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

        public ChoiceResult(IEnumerable<Effect> effects, string evaluationMessage, Func<string>? messageFunc = null, double? virusModifier = null) : this(effects, effects, messageFunc, virusModifier)
        { }
        public ChoiceResult(IEnumerable<Effect> positiveEffects, IEnumerable<Effect> requiredEffects, string evaluationMessage, Func<string>? messageFunc = null, double? virusModifier = null)
        {
            PositiveEffects = positiveEffects;
            RequiredEffects = requiredEffects;
            this.messageFunc = messageFunc;
            this.virusModifier = virusModifier;
            EvaluationMessage = evaluationMessage;
        }

        /// <summary>
        /// The <see cref="Core.Event"/> that owns this instance.
        /// </summary>
        public Event? Event { private get; set; }
        /// <summary>
        /// Effects that are good to have in order to minimize spread of the virus.
        /// </summary>
        public IEnumerable<Effect> PositiveEffects { get; init; }
        /// <summary>
        /// Effects that the user must have to be able to make this choice.
        /// </summary>
        public IEnumerable<Effect> RequiredEffects { get; init; }
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
        /// Message displayed when evaluating good and bad choices.
        /// </summary>
        public string EvaluationMessage { get; set; }
        /// <summary>
        /// How much this choice will affect the <see cref="Virus"/>.
        /// </summary>
        public double VirusModifier => virusModifier ?? Event?.VirusModifier ?? 0;
        /// <summary>
        /// <see langword="true"/> if the didn't have a negative effect on the <see cref="Virus"/>; otherwise <see langword="false"/>.
        /// </summary>
        public bool WasPositive { get; set; }

        /// <summary>
        /// Check if the player is affected by the required effects to make this choice.
        /// </summary>
        /// <param name="effects">Currently active player effects.</param>
        /// <returns><see langword="true"/> if <paramref name="effects"/> contains the required effects for the player to make this choice; otherwise <see langword="false"/>.</returns>
        public bool HasRequiredEffects(IEnumerable<Effect> effects)
        {
            foreach (Effect requiredEffect in RequiredEffects)
            {
                if (!effects.Contains(requiredEffect))
                    return false;
            }

            return true;
        }
    }
}

namespace WorldOfEpidemics.Core;

/// <summary>
/// Something that the player should make a decision about.
/// </summary>
public class Event
{
    public Event(string decisionMessage, IDictionary<string, IEnumerable<Effect>> choices, double virusModifier)
    {
        DecisionMessage = decisionMessage;
        Choices = choices;
        VirusModifier = virusModifier;
    }

    /// <summary>
    /// Message to set up the scenario and ask the player to make a choice.
    /// </summary>
    public string DecisionMessage { get; init; }
    /// <summary>
    /// Choices that the player can make and what <see cref="Effect"/>s will help minimize or improve the state of the <see cref="Virus"/>.
    /// </summary>
    public IDictionary<string, IEnumerable<Effect>> Choices { get; } = new Dictionary<string, IEnumerable<Effect>>();
    /// <summary>
    /// How much this event will affect the <see cref="Virus"/>.
    /// </summary>
    public double VirusModifier { get; init; }
}

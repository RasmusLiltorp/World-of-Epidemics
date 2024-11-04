using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Commands;

public abstract class BaseCommand : ICommand
{
    public string Description { get; init; } = string.Empty;

    public abstract void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters);

    /// <summary>
    /// Guard for verifying that an array contains a specified number of elements.
    /// </summary>
    /// <param name="elements">An array of any type.</param>
    /// <param name="numberOfElements">Number of elements to have in the array.</param>
    /// <returns><see langword="true"/> if the length of <paramref name="elements"/> doesn't equal <paramref name="numberOfElements"/>; otherwise <see langword="false"/>.</returns>
    protected static bool GuardEq(object[] elements, int numberOfElements)
    {
        return elements.Length != numberOfElements;
    }
}

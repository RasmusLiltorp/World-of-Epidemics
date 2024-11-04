using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.Core.Commands;

public interface ICommand
{
    /// <summary>
    /// Description of what the command does and how to use it.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Execute the command and make contextual changes to the game world.
    /// </summary>
    /// <param name="contextModifier">Game context.</param>
    /// <param name="user">Representation of the user.</param>
    /// <param name="command">Command name.</param>
    /// <param name="parameters">Collection of parameters (in typed order).</param>
    public void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters);
}

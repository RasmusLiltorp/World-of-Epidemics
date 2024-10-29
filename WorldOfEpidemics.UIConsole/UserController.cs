using WorldOfEpidemics.Core;
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.UIConsole;

internal class UserController : IUserController
{
    private Registry? registry;

    public void Initialize(Registry registry)
    {
        this.registry = registry;
    }

    public void RunCommand(IContextModifier context)
    {
        Console.Write("> ");
        string? line = Console.ReadLine();
        if (line != null && registry != null)
            registry.Dispatch(line, (Context)context); // TODO: Use IContextModifier.
    }

    public void WriteText(string text)
    {
        Console.WriteLine(text);
    }
}

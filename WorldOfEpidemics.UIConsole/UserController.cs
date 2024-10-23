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

    public void RunCommand()
    {
        Console.Write("> ");
        string? line = Console.ReadLine();
        if (line != null && registry != null)
            registry.Dispatch(line);
    }

    public void WriteText(string text)
    {
        Console.WriteLine(text);
    }
}

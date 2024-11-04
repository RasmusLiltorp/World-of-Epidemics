using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Commands;

namespace WorldOfEpidemics.Core;

public class Game
{
    World world;
    Context context;
    Registry registry;

    public Game(IUserController userController)
    {
        world = new World();
        registry = new(new CommandUnknown());
        userController.Initialize(registry);
        context = new Context(userController, world.GetEntry(), new Virus());

        InitializeRegistry();
    }

    public void Run()
    {
        TimeSpan tickDuration = TimeSpan.FromMilliseconds(200);
        DateTime nextTick = DateTime.Now + tickDuration;
        while (!context.IsDone)
        {
            // Run commands
            context.Service();
            
            // Run events like the virus
            if (DateTime.Now >= nextTick)
            {
                nextTick = DateTime.Now + tickDuration;
                context.CurrentVirus.Service();
            }
            
        }
    }

    private void InitializeRegistry()
    {
        ICommand cmdExit = new CommandExit();
        registry.Register("exit", cmdExit);
        registry.Register("quit", cmdExit);
        registry.Register("bye", cmdExit);
        registry.Register("go", new CommandGo());
        registry.Register("help", new CommandHelp(registry));
    }
}

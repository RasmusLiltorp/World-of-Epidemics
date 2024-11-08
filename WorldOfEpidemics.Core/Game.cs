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
        context.CurrentVirus.Service();
        double effectModifier = 0.05;
        
        TimeSpan tickDuration = TimeSpan.FromMilliseconds(200);
        DateTime nextTick = DateTime.Now + tickDuration;
        long curTick = 1;
        while (!context.IsDone)
        {
            // Run commands
            context.Service();
            
            // Run the virus every 3 seconds
            if (curTick % 15 == 0)
            {
                context.CurrentVirus.Service();
            }
            
            // Update infection rate every minute
            if (curTick % 150 == 0)
            {
                context.CurrentVirus.ModifyInfectionRate(context.Effects.Count == 0? -1 * effectModifier : effectModifier * context.Effects.Count);
            }
            
            // Decrement effect time
            DecrementEffects();
            
            // Increment the tick count
            if (DateTime.Now >= nextTick)
            {
                nextTick = DateTime.Now + tickDuration;
                curTick++;
            }
            
            // Performance
            Thread.Sleep(1);
        }
    }

    private void DecrementEffects()
    {
        Dictionary<Effect, int> effects = context.Effects;
        var effectKeys = context.Effects.Keys;
        foreach (Effect effect in effectKeys)
        {
            if (effects[effect] == 0)
            {
                effects.Remove(effect);
            }
            effects[effect]--;
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
        registry.Register("status", new CommandVirusStatus(context.CurrentVirus));
        registry.Register("interact", new CommandInteract());
        registry.Register("use", new CommandUse());
    }
}

using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Commands;

namespace WorldOfEpidemics.Core;

public class Game
{
    World world;
    Context context;
    Registry registry;
    IUserController userController;

    public Game(IUserController userController)
    {
        world = new World();
        registry = new(new CommandUnknown());
        userController.Initialize(registry);
        context = new Context(userController, world.GetEntry(), new Virus());
        this.userController = userController;

        InitializeRegistry();
    }

    public void Run()
    {
        TimeSpan tickDuration = TimeSpan.FromMilliseconds(200);
        DateTime nextTick = DateTime.Now + tickDuration;
        long curTick = 0;
        int gameTime = 1500; // 1500 ticks is five minutes

        while (!context.IsDone)
        {
            // Run commands
            context.Service();
            
            // Increment the tick count
            if (DateTime.Now >= nextTick)
            {
                nextTick = DateTime.Now + tickDuration;
                curTick++;
                RunTick(curTick);
            }
            
            // Break game loop when the set game time is reached
            if(curTick == gameTime)
                break;

            // Performance
            Thread.Sleep(1);
        }
        
        // Post game evalutaion
        Evaluation evaluation = new(context.CurrentVirus, context.ChoiceList);
        userController.ShowGameEvaluation(evaluation);
    }

    private void RunTick(long tick)
    {
        const double effectModifier = 0.005;
        
        // Run the virus every 3 seconds
        if (tick % 15 == 0)
        {
            context.CurrentVirus.Service();
        }
            
        // Update infection rate every minute
        if (tick % 150 == 0)
        {
            context.CurrentVirus.ModifyInfectionRate(context.Effects.Count == 0? effectModifier : -1* effectModifier * context.Effects.Count);
        }
        
        // Decrement effect time
        DecrementEffects();
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
                continue;
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

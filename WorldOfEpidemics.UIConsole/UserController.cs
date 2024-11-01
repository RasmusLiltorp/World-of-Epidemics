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

    private string _currentCommand = "";
    
    public void RunCommand(IContextModifier context)
    {
        // Make sure ">> " is visible when asking the user for a command
        if (_currentCommand == "")
        {
            _currentCommand = ">> ";
            Console.Write(_currentCommand);
        }
        
        // Check if a key has been pressed or if the registry is null
        if (!Console.KeyAvailable || registry == null)
        {
            return;
        }
        
        // Read key pressed
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        ConsoleKey keySignature = keyInfo.Key;
        string input = keyInfo.KeyChar.ToString();
        switch (keySignature)
        {
            case ConsoleKey.Enter:
                // Dispatch command if enter was pressed
                Console.WriteLine();
                registry.Dispatch(_currentCommand[3..], (Context)context);
                _currentCommand = "";
                break;
            case ConsoleKey.Backspace:
                // Emulate backspace visually
                _currentCommand = _currentCommand[..^1];
                Console.Write("\b \b");
                break;
            default:
                // Build command from key presses
                Console.Write(input);
                _currentCommand += input;
                break;
        }
    }

    public void AnnounceRoom(Space room)
    {
        // TODO: Show options in the room; including doorways and items.
        throw new NotImplementedException();
    }

    public void ShowHelp()
    {
        if (registry == null)
            return;

        string[] commandNames = registry.GetCommandNames();
        Array.Sort(commandNames);

        // Find longest command name.
        int maxCommandNameLength = 0;
        foreach (var commandName in commandNames)
        {
            int length = commandName.Length;
            if (length > maxCommandNameLength)
                maxCommandNameLength = length;
        }

        // Present list of commands.
        Console.WriteLine("Commands:");
        foreach (var commandName in commandNames)
        {
            string description = registry.GetCommand(commandName).Description;
            Console.WriteLine(" - {0,-" + maxCommandNameLength + "} " + description, commandName);
        }
    }
    
    public void ShowHelp()
    {
        if (registry == null)
            return;

        string[] commandNames = registry.GetCommandNames();
        Array.Sort(commandNames);

        // Find longest command name.
        int maxCommandNameLength = 0;
        foreach (var commandName in commandNames)
        {
            int length = commandName.Length;
            if (length > maxCommandNameLength)
                maxCommandNameLength = length;
        }

        // Present list of commands.
        Console.WriteLine("Commands:");
        foreach (var commandName in commandNames)
        {
            string description = registry.GetCommand(commandName).Description;
            Console.WriteLine(" - {0,-" + maxCommandNameLength + "} " + description, commandName);
        }
    }

    public void WriteText(string text)
    {
        Console.WriteLine(text);
    }
}

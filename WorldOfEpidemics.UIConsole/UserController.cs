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
    
    public void RunCommand(IContextModifier contextModifier)
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
                registry.Dispatch(_currentCommand[3..], contextModifier, this);
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

    public void AnnounceRoom(Room room)
    {
        WriteText(room.GetWelcome());

        // Show doorways.
        Console.WriteLine("Doorways" + ":");
        foreach (var doorway in room.Doorways)
        {
            Console.WriteLine($" - {doorway.Name}");
        }

        // Show items.
        Console.Write("\r\n" + "You take a look at your surroundings." + " ");
        if (room.Interactables.Count > 0)
        {
            Console.WriteLine("You see" + ":");
            foreach (var interactable in room.Interactables)
            {
                Console.WriteLine($" - {interactable.Name}");
            }
        }
        else
        {
            Console.WriteLine("You don't see anything interesting.");
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

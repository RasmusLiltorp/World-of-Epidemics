using System.Diagnostics.CodeAnalysis;
using WorldOfEpidemics.Core;
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.UIConsole;

internal class UserController : IUserController
{
    private const string commandMarker = ">> ";

    private IList<string>? currentChoices;
    private Registry? registry;
    private string currentCommand = string.Empty;
    private bool wasEnterPressed = false;

    public void Initialize(Registry registry)
    {
        this.registry = registry;
    }
    
    public void RunCommand(IContextModifier contextModifier)
    {
        if (registry == null)
            return;

        // Read next key pressed.
        if (!ReadKeyPress() || !wasEnterPressed)
            return;

        // Enter was pressed; dispatch command.
        registry.Dispatch(currentCommand[commandMarker.Length..], contextModifier, this);
        ResetConsole();
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

    public void StartGetDecision(string message, IList<string> choices)
    {
        currentChoices = choices;
        WriteText("\r\n" + message);
        for (int i = 0; i < currentChoices.Count; i++)
        {
            Console.WriteLine($" ({i + 1}) - {currentChoices[i]}");
        }
    }

    public bool TryFinishGetDecision([MaybeNullWhen(false)] out string decision)
    {
        decision = null;

        // If no chioces, we should continue and just indicate that the decision making is done.
        if (currentChoices == null)
        {
            decision = string.Empty;

            return true;
        }

        // Read next key pressed.
        if (!ReadKeyPress() || !wasEnterPressed)
            return false;

        // Enter was pressed; parse input as an integer and return true together with the decision.
        if (!Int32.TryParse(currentCommand[commandMarker.Length..], out int result) &&
            0 < result && result <= currentChoices.Count)
            return false;

        decision = currentChoices[result - 1];
        currentChoices = null;
        ResetConsole();

        return true;
    }

    /// <summary>
    /// Make sure command marker is visible when asking the user to enter a command.
    /// </summary>
    private void EnsureCommandMarkerIsVisible()
    {
        if (string.IsNullOrWhiteSpace(currentCommand))
        {
            currentCommand = commandMarker;
            Console.Write(currentCommand);
        }
    }

    /// <summary>
    /// Read key from the console and modify <see cref="currentCommand"/>.
    /// </summary>
    /// <returns><see langword="true"/> if a key was pressed; otherwise <see langword="false"/>.</returns>
    private bool ReadKeyPress()
    {
        EnsureCommandMarkerIsVisible();

        // Check if a key has been pressed.
        if (!Console.KeyAvailable)
            return false;

        // Read key pressed
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        ConsoleKey keySignature = keyInfo.Key;
        string input = keyInfo.KeyChar.ToString();
        switch (keySignature)
        {
            case ConsoleKey.Enter:
                wasEnterPressed = true;
                Console.WriteLine();
                return true;
            case ConsoleKey.Backspace:
                // Emulate backspace visually.
                if (currentCommand.Length <= commandMarker.Length)
                    break;
                currentCommand = currentCommand[..^1];
                Console.Write("\b \b");
                break;
            default:
                // Build command from key presses.
                Console.Write(input);
                currentCommand += input;
                break;
        }

        wasEnterPressed = false;

        return true;
    }

    private void ResetConsole()
    {
        currentCommand = string.Empty;
        wasEnterPressed = false;
    }
}

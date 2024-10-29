using WorldOfEpidemics.Core;

namespace WorldOfEpidemics.UIConsole;

internal class Program
{
    static void Main(string[] args)
    {
        // Greet the user.
        UserController userController = new();
        userController.WriteText(Core.Properties.Resources.MessageWelcome);

        // Initialize the game (done in the constructor).
        Game game = new(userController);

        // Run the game. UserController will handle both user input and output, thus guiding the user through the game session controlled by the Game instance.
        game.Run();
    }
}

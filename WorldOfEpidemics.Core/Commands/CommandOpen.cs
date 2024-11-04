using WorldOfEpidemics.Core.Interactives;
namespace WorldOfEpidemics.Core.Commands
{
    public class CommandOpen : BaseCommand, ICommand
    {
        public CommandOpen()
        {
            description = "Open a specified container in the current room.";
        }
        public void Execute(Context context, string command, string[] parameters)
        {
            // Check if no parameters are provided
            if (GuardEq(parameters, 1))
            {
                Console.WriteLine("Please specify the container you want to open.");
                return;
            }

            string containerName = parameters[0];
            Room currentRoom = context.CurrentRoom;

            IInteractable interactable = currentRoom.GetInteractable(containerName);

            if (interactable is Container container)
            {
                container.Interact(context); // Open the container
            }
            else
            {
                Console.WriteLine($"There is no container named '{containerName}' in this room.");
            }
        }
    }
}
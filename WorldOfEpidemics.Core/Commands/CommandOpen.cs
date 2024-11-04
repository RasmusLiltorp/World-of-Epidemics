using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Interactives;
using WorldOfEpidemics.Core.Properties;

namespace WorldOfEpidemics.Core.Commands
{
    public class CommandOpen : BaseCommand
    {
        public CommandOpen()
        {
            Description = Resources.CommandDescriptionHelp;
        }
        public override void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters)        {
            // Check if no parameters are provided
            if (GuardEq(parameters, 1))
            {
                Console.WriteLine("Please specify the container you want to open.");
                return;
            }

            string containerName = parameters[0];
            Room currentRoom = contextModifier.CurrentRoom;
            foreach (var interactable in currentRoom.Interactables)
            {
                if (interactable.Name.Equals(containerName))
                {
                    if (interactable is Container container)
                    {
                        container.Open(containerName);
                        return;
                    }
                }
            }
            user.WriteText($"There is no container named '{containerName}' in this room.");
           
        }
    }
}
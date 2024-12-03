using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Interactives;
using WorldOfEpidemics.Core.Properties;

namespace WorldOfEpidemics.Core.Commands
{
    public class CommandVirusStatus : BaseCommand
    {
        private Virus virus;
        private static ObjectiveList task = new ObjectiveList();
        
        public CommandVirusStatus(Virus virus) {
            this.virus = virus;
            Description = Resources.CommandVirusStatus;
        }
        public override void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters)
        {
            user.WriteText($"Infected = {virus.NumOfInfected}, Death toll = {virus.DeathToll}");
            ActionList.ObjectiveList.PrintObjectives();

        }
    }

}
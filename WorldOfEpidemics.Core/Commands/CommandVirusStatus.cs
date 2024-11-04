using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Properties;

namespace WorldOfEpidemics.Core.Commands
{
    public class CommandVirusStatus : BaseCommand
    {
        private Virus virus;
        
        public CommandVirusStatus(Virus virus) {
            this.virus = virus;
            Description = Resources.CommandVirusStatus;
        }
        public override void Execute(IContextModifier contextModifier, IUser user, string command, string[] parameters)
        {
            virus.PrintVirusInfo();
        }
    }

}
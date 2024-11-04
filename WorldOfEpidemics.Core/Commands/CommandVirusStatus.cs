namespace WorldOfEpidemics.Core.Commands
{
    public class CommandVirusStatus : BaseCommand, ICommand
    {
        private Virus virus;
        
        public CommandVirusStatus(Virus virus) {
            this.virus = virus;
            this.description = "Display virus status";
        }
        public void Execute (Context context, string command, string[] parameters) {
            virus.PrintVirusInfo();
        }
    }

}
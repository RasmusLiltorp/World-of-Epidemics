/* Command for exiting program
 */

namespace WorldOfEpidemics.Core.Commands;

public class CommandExit : BaseCommand, ICommand {
  public void Execute (Context context, string command, string[] parameters) {
    context.MakeDone();
  }
}

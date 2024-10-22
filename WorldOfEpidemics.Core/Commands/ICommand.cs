/* Command interface
 */

namespace WorldOfEpidemics.Core.Commands;

public interface ICommand {
  void Execute (Context context, string command, string[] parameters);
  string GetDescription ();
}


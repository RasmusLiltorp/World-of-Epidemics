/* Baseclass for commands
 */

namespace WorldOfEpidemics.Core.Commands;

public class BaseCommand {
  protected string description = "Undocumented";
  
  protected bool GuardEq (string[] parameters, int bound) {
    return parameters.Length!=bound;
  }
  
  public String GetDescription () {
    return description;
  }
}

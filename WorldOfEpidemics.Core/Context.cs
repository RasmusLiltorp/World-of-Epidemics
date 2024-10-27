/* Context class to hold all context relevant to a session.
 */

namespace WorldOfEpidemics.Core;

public class Context {
  Space current;
  Space next;
  bool done = false;
  
  public Context (Space node) {
    current = node;
  }
  
  public Space GetCurrent() {
    return current;
  }
  
  public void Transition (string direction) 
  {
    try //Try transitioning to a given direction
    {
      next = current.FollowEdge(direction);
    }
    catch(KeyNotFoundException) 
    {
      Console.WriteLine("Denne retning eksisterer ikke i den givne kontekst");
      next = current; 
    }

    if (next==null) {
      Console.WriteLine("You are confused, and walk in a circle looking for '"+direction+"'. In the end you give up 😩");
    } else {
      current.Goodbye();
      current = next;
      current.Welcome();
    }
  }
  
  public void MakeDone () {
    done = true;
  }
  
  public bool IsDone () {
    return done;
  }
}


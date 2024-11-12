using System.Diagnostics.CodeAnalysis;
using WorldOfEpidemics.Core;
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.UIBlazor
{
    public class UserController : IUserController
    {
        public void AnnounceRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public void Initialize(Registry registry)
        {
            throw new NotImplementedException();
        }

        public void RunCommand(IContextModifier context)
        {
            throw new NotImplementedException();
        }

        public void ShowHelp()
        {
            throw new NotImplementedException();
        }

        public void StartGetDecision(string message, IList<string> choices)
        {
            throw new NotImplementedException();
        }

        public bool TryFinishGetDecision([MaybeNullWhen(false)] out string decision)
        {
            throw new NotImplementedException();
        }

        public void WriteText(string text)
        {
            throw new NotImplementedException();
        }
    }
}

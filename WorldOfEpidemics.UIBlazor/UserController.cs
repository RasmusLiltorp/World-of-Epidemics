using System.Diagnostics.CodeAnalysis;
using WorldOfEpidemics.Core;
using WorldOfEpidemics.Core.Abstractions;
using static WorldOfEpidemics.UIBlazor.Events;

namespace WorldOfEpidemics.UIBlazor
{
    public class UserController : IUserController
    {
        private Registry? registry;
        private CancellationTokenSource decisionCancellationTokenSource = new();
        private Room? lastAnnouncedRoom;

        public event EventHandler<RoomAnnouncedEventArgs>? RoomAnnounced;
        public event EventHandler<string>? OnWriteText;
        public event EventHandler<GetDecisionStartedEventArgs>? GetDecisionStarted;

        public Func<string?>? GetNextCommandFunc { get; set; }
        public Func<string?>? GetDecisionFunc { get; set; }

        public void AnnounceRoom()
        {
            if (lastAnnouncedRoom != null)
                AnnounceRoom(lastAnnouncedRoom);
        }

        public void AnnounceRoom(Room room)
        {
            lastAnnouncedRoom = room;
            RoomAnnounced?.Invoke(this, new(room));
        }

        public void Initialize(Registry registry)
        {
            this.registry = registry;
        }

        public void RunCommand(IContextModifier contextModifier)
        {
            if (registry == null)
                return;

            // Get the next command.
            var command = GetNextCommandFunc?.Invoke();
            if (command == null)
                return;

            // Dispatch command from the registry.
            registry.Dispatch(command, contextModifier, this);
        }

        public void ShowHelp()
        {
            if (registry == null)
                return;

            string[] commandNames = registry.GetCommandNames();
            Array.Sort(commandNames);

            // Find longest command name.
            int maxCommandNameLength = 0;
            foreach (var commandName in commandNames)
            {
                int length = commandName.Length;
                if (length > maxCommandNameLength)
                    maxCommandNameLength = length;
            }

            // Present list of commands.
            Console.WriteLine("Commands:");
            foreach (var commandName in commandNames)
            {
                string description = registry.GetCommand(commandName).Description;
                Console.WriteLine(" - {0,-" + maxCommandNameLength + "} " + description, commandName);
            }
        }

        public void StartGetDecision(string message, IList<string> choices)
        {
            if (!decisionCancellationTokenSource.TryReset())
                decisionCancellationTokenSource = new CancellationTokenSource();
            GetDecisionStarted?.Invoke(this, new(message, choices, decisionCancellationTokenSource));
        }

        public bool TryFinishGetDecision([MaybeNullWhen(false)] out string decision)
        {
            if (decisionCancellationTokenSource.IsCancellationRequested ||
                GetDecisionFunc == null)
            {
                // The user did not make a decision, and we should stop waiting around for one.
                decision = string.Empty;

                return true;
            }

            // Get the next command.
            decision = GetDecisionFunc.Invoke();
            if (decision == null)
                return false;

            decisionCancellationTokenSource.Dispose();

            return true;
        }

        public void WriteText(string text) =>
            OnWriteText?.Invoke(this, text);
    }
}

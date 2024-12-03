using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using WorldOfEpidemics.Core;
using WorldOfEpidemics.Core.Abstractions;
using static System.Net.Mime.MediaTypeNames;
using static WorldOfEpidemics.UIBlazor.Events;

namespace WorldOfEpidemics.UIBlazor
{
    public class UserController : IUserController
    {
        private bool isReceivingWriteBatch = false;
        private StringBuilder textBatch = new();

        private Registry? registry;
        private CancellationTokenSource decisionCancellationTokenSource = new();
        private Room? lastAnnouncedRoom;
        private DateTime nextTick = DateTime.Now;

        public event EventHandler<RoomAnnouncedEventArgs>? RoomAnnounced;
        public event EventHandler<string>? OnWriteText;
        public event EventHandler<GetDecisionStartedEventArgs>? GetDecisionStarted;
        public event EventHandler<AfterCommandRunEventArgs>? AfterCommandRun;
        public event EventHandler<UITickEventArgs>? OnUITick;
        public event EventHandler<ShowEvaluationEventArgs>? OnShowEvaluation;

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

        private void RunTick(IContextModifier contextModifier)
        {
            nextTick = DateTime.Now + TimeSpan.FromMilliseconds(200);
            OnUITick?.Invoke(this, new (contextModifier));
        }
        
        public void RunCommand(IContextModifier contextModifier)
        {
            if (DateTime.Now > nextTick)
            {
                RunTick(contextModifier);
            }
            
            if (registry == null)
                return;

            // Get the next command.
            var command = GetNextCommandFunc?.Invoke();
            if (command == null)
                return;

            // Dispatch command from the registry.
            registry.Dispatch(command, contextModifier, this);
            
            string[] elements = command.Split(" ");
            AfterCommandRun?.Invoke(this, new AfterCommandRunEventArgs(elements[0], elements[1..], contextModifier));
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
            decisionCancellationTokenSource = new CancellationTokenSource();
            GetDecisionStarted?.Invoke(this, new(message, choices, decisionCancellationTokenSource));
        }

        public bool TryFinishGetDecision([MaybeNullWhen(false)] out string decision)
        {
            if (GetDecisionFunc == null)
                throw new InvalidOperationException($"{nameof(GetDecisionFunc)} wasn't set.");

            // Poll for decision.
            decision = GetDecisionFunc.Invoke();
            if (!string.IsNullOrWhiteSpace(decision))
                return true;

            // The user did not make a decision.
            // Check if the operation has been cancelled.
            if (decisionCancellationTokenSource.IsCancellationRequested)
            {
                // The task has been cancelled and no decision was made; return empty.
                decision = string.Empty;

                return true;
            }

            return false;
        }

        public void BeginBatchWrite()
        {
            if (isReceivingWriteBatch)
                EndBatchWrite();

            isReceivingWriteBatch = true;
        }

        public void EndBatchWrite()
        {
            if (isReceivingWriteBatch)
            {
                OnWriteText?.Invoke(this, textBatch.ToString());
                textBatch.Clear();
            }

            isReceivingWriteBatch = false;
        }

        public void WriteText(string text)
        {
            if (isReceivingWriteBatch)
                textBatch.AppendLine(text);
            else
                OnWriteText?.Invoke(this, text);
        }

        public void ShowGameEvaluation(Evaluation evaluation)
        {
            OnShowEvaluation?.Invoke(this, new(evaluation));
        }
    }
}

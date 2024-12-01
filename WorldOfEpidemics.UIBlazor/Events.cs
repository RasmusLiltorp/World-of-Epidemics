using BlazorBootstrap;
using WorldOfEpidemics.Core;
using WorldOfEpidemics.Core.Abstractions;

namespace WorldOfEpidemics.UIBlazor;

public static class Events
{
    public class RoomAnnouncedEventArgs(Room room) : EventArgs
    {
        public Room Room { get; } = room;
    }

    public class GetDecisionStartedEventArgs(string message, IList<string> choices, CancellationTokenSource tokenSource) : EventArgs
    {
        public string Message { get; } = message;
        public IList<string> Choices { get; } = choices;
        /// <summary>
        /// Cancel the decison making from the UI (user closing dialog etc.).
        /// </summary>
        public CancellationTokenSource TokenSource { get; } = tokenSource;
    }

    public class AfterCommandRunEventArgs(string command, string[] args, IContextModifier contextModifier)
    {
        public string Command { get; } = command;
        public string[] Args { get; } = args;
        public IContextModifier ContextModifier { get; } = contextModifier;
    }

    public class UITickEventArgs(IContextModifier contextModifier)
    {
        public IContextModifier ContextModifier { get; } = contextModifier;
    }
}
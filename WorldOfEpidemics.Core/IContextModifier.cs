namespace WorldOfEpidemics.Core
{
    /// <summary>
    /// Interface through which a <see cref="Context"/> can be modifend.
    /// </summary>
    public interface IContextModifier
    {
        public Room CurrentRoom { get; }

        void ChangeRoom(Room room);

        void Exit();
    }
}

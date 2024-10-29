namespace WorldOfEpidemics.Core
{
    /// <summary>
    /// Interface through which a <see cref="Context"/> can be modifend.
    /// </summary>
    public interface IContextModifier
    {
        public Space CurrentRoom { get; }

        void ChangeRoom(Space room);
    }
}

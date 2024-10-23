namespace WorldOfEpidemics.Core
{
    /// <summary>
    /// Interface through which a <see cref="Context"/> can be modifend.
    /// </summary>
    internal interface IContextModifier
    {
        void ChangeRoom(/*Room room*/);
    }
}

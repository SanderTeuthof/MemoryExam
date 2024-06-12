namespace Memory.Models
{
    public interface IBoardState
    {
        BoardStates State { get; }

        void AddPreview(TileModel tile);
        void TileAnimationEnded(TileModel tile);
    }
}



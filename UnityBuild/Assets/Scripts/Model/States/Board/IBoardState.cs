namespace MemoryGame.Model.States.Board
{
    public interface IBoardState
    {
        public BoardStates BoardStates { get; }
        public MemoryBoard Board { get; set; }

        public void AddPreview(Tile tile);
        public void TileAnimationEnd(Tile tile);
    }
}
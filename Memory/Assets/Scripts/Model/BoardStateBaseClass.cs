namespace Memory.Models
{
    public abstract class BoardStateBaseClass : IBoardState
    {
        public abstract BoardStates State { get; }

        protected BoardModel board;

        public BoardStateBaseClass(BoardModel board)
        {
            this.board = board;
        }

        public abstract void AddPreview(TileModel tile);
        public abstract void TileAnimationEnded(TileModel tile);
    }
}

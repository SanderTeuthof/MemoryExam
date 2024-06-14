namespace MemoryGame.Model.States.Board
{
    public abstract class BoardStateBaseClass : IBoardState
    {
        public abstract BoardStates BoardStates { get; }

        public MemoryBoard Board 
        {
            get => _board;
            set
            {
                if (_board == value) { return; }
                _board = value;
            }
        }

        private MemoryBoard _board;

        public BoardStateBaseClass(MemoryBoard board)
        {
            Board = board;
        }

        public abstract void AddPreview(Tile tile);
        public abstract void TileAnimationEnd(Tile tile);
    }
}
namespace Memory.Models
{
    public class BoardOneFlippingState : IBoardState
    {
        public BoardStates State => BoardStates.TwoHiding;
        public BoardModel Board;

        public BoardOneFlippingState(BoardModel board)
        {
            Board = board;
        }

        public void AddPreview(TileModel tile)
        {
            // Do nothing since we are currently hiding two tiles
        }

        public void TileAnimationEnded(TileModel tile)
        {
           
            Board.State = new BoardOnePreviewState(Board);

        }
    }
}

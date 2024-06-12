namespace Memory.Models
{
    public class BoardNoPreviewState : BoardStateBaseClass
    {
        public override BoardStates State => BoardStates.NoPreview;

        public BoardNoPreviewState(BoardModel board) : base(board)
        {
        }

        public override void AddPreview(TileModel tile)
        {
            if (tile.State.State == TileStates.Hidden)
            {
                tile.State = new TilePreviewingState(tile);
                board.PreviewingTiles.Add(tile);
                board.State = new BoardOneFlippingState(board);
            }
        }

        public override void TileAnimationEnded(TileModel tile)
        {
            // Not interested in tile animation ending in this state
        }
    }
}


namespace Memory.Models
{
    public class BoardOnePreviewState : BoardStateBaseClass
    {
        public override BoardStates State => BoardStates.OnePreview;

        public BoardOnePreviewState(BoardModel board) : base(board)
        {
        }

        public override void AddPreview(TileModel tile)
        {
            if (tile.State.State == TileStates.Hidden)
            {
                board.PreviewingTiles.Add(tile);

                board.State = new BoardTwoPreviewState(board);
                tile.State = new TilePreviewingState(tile);
                
            }
        }

        public override void TileAnimationEnded(TileModel tile)
        {
            // Not interested in tile animation ending in this state
        }
    }

}

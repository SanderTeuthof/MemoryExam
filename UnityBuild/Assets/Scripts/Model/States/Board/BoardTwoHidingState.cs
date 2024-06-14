namespace MemoryGame.Model.States.Board
{
    public class BoardTwoHidingState : BoardStateBaseClass
    {
        public override BoardStates BoardStates => BoardStates.TwoHiding;

        public BoardTwoHidingState(MemoryBoard board) : base(board) { }

        public override void AddPreview(Tile tile) { }

        public override void TileAnimationEnd(Tile tile)
        {
            // remove the tile from the PreviewingTiles. If the PreviewingTiles is empty, set the
            // state of the Board to a BoardNoPreviewing instance, so we can start looking for a
            // new combination.
            tile.Board.PreviewingTiles.Remove(tile);

            if (tile.Board.PreviewingTiles.Count == 0)
            {
                tile.Board.State = new BoardNoPreviewState(tile.Board);
            }
        }
    }
}
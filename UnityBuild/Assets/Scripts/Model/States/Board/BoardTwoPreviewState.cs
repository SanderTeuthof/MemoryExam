using MemoryGame.Model.States.Tiles;

namespace MemoryGame.Model.States.Board
{
    public class BoardTwoPreviewState : BoardStateBaseClass
    {
        public override BoardStates BoardStates => BoardStates.TwoPreviews;

        public BoardTwoPreviewState(MemoryBoard board) : base(board) { }

        public override void AddPreview(Tile tile) { }

        public override void TileAnimationEnd(Tile tile)
        {
            // if this is the tile at the second place in the PreviewingTiles list, then both tiles ended
            // the preview animation and they should both be hidden again: set the state of the
            // Board to a BoardTwoHidingState instance and the state of both tiles to an instance
            // of TileHiddenState.
            if (tile.Board.PreviewingTiles.Count != 2) { return; }

            if (tile == tile.Board.PreviewingTiles[1])
            {
                tile.Board.State = new BoardTwoHidingState(tile.Board);

                foreach (Tile t in tile.Board.PreviewingTiles)
                {
                    t.State = new TileHiddenState(t);
                }
            }
        }
    }
}
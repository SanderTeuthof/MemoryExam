using MemoryGame.Model.States.Tiles;

namespace MemoryGame.Model.States.Board
{
    public class BoardNoPreviewState : BoardStateBaseClass
    {
        public override BoardStates BoardStates => BoardStates.NoPreview;

        public BoardNoPreviewState(MemoryBoard board) : base(board) { }
        public override void AddPreview(Tile tile)
        {
            // only a Tile which is now hidden can be added to the Preview list.If the state of the
            // state of the tile is not equal to TileStates.Hidden, nothing should happen: return;
            // (eg: a Tile which is already found or is now previewed should not be processed again)
            if (tile.State.State != TileStates.Hidden) { return; }

            // change the State of this tile to a TilePreviewingState instance;
            tile.State = new TilePreviewState(tile);

            // add this tile to the PreviewingTiles property of tile.Board;
            tile.Board.PreviewingTiles.Add(tile);

            // change the State of the board of the tile to a BoardOnePreviewState instance
            tile.Board.State = new BoardOnePreviewState(tile.Board);
        }

        public override void TileAnimationEnd(Tile tile) { }
    }
}

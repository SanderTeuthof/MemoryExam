using MemoryGame.Model.States.Tiles;
namespace MemoryGame.Model.States.Board
{
    public class BoardOnePreviewState : BoardStateBaseClass
    {
        public override BoardStates BoardStates => BoardStates.OnePreview;

        public BoardOnePreviewState(MemoryBoard board) : base(board) { }

        public override void AddPreview(Tile tile)
        {
            // only a Tile which is now hidden can be added to the Preview list.If the state of the
            // state of the tile is not equal to TileStates.Hidden, nothing should happen: return;
            if (tile.State.State != TileStates.Hidden) { return; }

            // add this tile to the Previewing property of tile.Board;
            tile.Board.PreviewingTiles.Add(tile);

            // if the two tiles are a combination(we created a property on Board to check this),
            // set the state of the board to a BoardTwoFoundState instance and set the state of
            // both tiles to TileFoundState instances;
            if (tile.Board.IsCombinationFound)
            {
                tile.Board.State = new BoardTwoFoundState(tile.Board);

                foreach (Tile t in tile.Board.PreviewingTiles)
                {
                    t.State = new TileFoundState(t);
                }
            }
            // if the two tiles are not a combination change the state of the board of the tile to a
            // BoardTwoPreviewState instance, and set the state of this tile to a TilePreviewState
            // instance
            else
            {
                tile.Board.State = new BoardTwoPreviewState(tile.Board);
                tile.State = new TilePreviewState(tile);
            }
        }

        public override void TileAnimationEnd(Tile tile) { }
    }
}
using MemoryGame.Model.States.Tiles;
using System.Linq;

namespace MemoryGame.Model.States.Board
{
    public class BoardTwoFoundState : BoardStateBaseClass
    {
        public override BoardStates BoardStates => BoardStates.TwoFound;

        public BoardTwoFoundState(MemoryBoard board) : base(board) { }

        public override void AddPreview(Tile tile) { }

        public override void TileAnimationEnd(Tile tile)
        {
            // remove the tile from the PreviewingTiles. If the PreviewingTiles is empty and there
            // is at most 1 tile which is hidden, set the state of the Board to a BoardFinishedState
            // instance, because the game ended.Otherwise, with an empty PreviewingTiles list,
            // set the state of the Board to a BoardNoPreviewsState instance, so we can start
            // looking for another combination
            tile.Board.PreviewingTiles.Remove(tile);

            if (tile.Board.PreviewingTiles.Count <= 1)
            {
                tile.Board.PreviewingTiles.Clear();

                if (tile.Board.Tiles
                    .Where(x => x.State.State == TileStates.Hidden)
                    .ToArray()
                    .Length <= 1)
                {
                    tile.Board.State = new BoardFinishedState(tile.Board);
                }
                else
                {
                    tile.Board.State = new BoardNoPreviewState(tile.Board);
                }
            }
        }
    }
}
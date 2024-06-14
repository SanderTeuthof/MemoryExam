namespace MemoryGame.Model.States.Tiles
{
    public class TilePreviewState : TileStateBaseClass
    {
        public override TileStates State => TileStates.Preview;

        public TilePreviewState(Tile tile) : base(tile) { }
    }
}
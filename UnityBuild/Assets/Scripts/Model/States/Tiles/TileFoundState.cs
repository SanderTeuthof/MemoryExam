namespace MemoryGame.Model.States.Tiles
{
    public class TileFoundState : TileStateBaseClass
    {
        public override TileStates State => TileStates.Found;

        public TileFoundState(Tile tile) : base(tile) { }
    }
}
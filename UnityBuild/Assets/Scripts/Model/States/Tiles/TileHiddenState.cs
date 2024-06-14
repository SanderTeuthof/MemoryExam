namespace MemoryGame.Model.States.Tiles
{
    public class TileHiddenState : TileStateBaseClass
    {
        public override TileStates State => TileStates.Hidden;

        public TileHiddenState(Tile tile) : base(tile) { }
    }
}
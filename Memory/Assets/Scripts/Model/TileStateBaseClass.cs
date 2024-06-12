namespace Memory.Models
{
    public abstract class TileStateBaseClass : ITileState
    {
        public abstract TileStates State { get; }

        protected TileModel tile;

        public TileStateBaseClass(TileModel tile)
        {
            this.tile = tile;
        }
    }
}

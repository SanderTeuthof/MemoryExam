namespace MemoryGame.Model.States.Tiles
{
    public abstract class TileStateBaseClass : ITileState
    {
        public abstract TileStates State { get; }

        public Tile Tile 
        {
            get => _tile;
            set
            {
                if (_tile == value) { return; }
                _tile = value;
            }
        }

        private Tile _tile;

        public TileStateBaseClass(Tile tile)
        {
            Tile = tile;
        }
    }
}
namespace MemoryGame.Model.States.Tiles
{
    public interface ITileState
    {
        public TileStates State { get; }
        public Tile Tile { get; set; }
    }
}
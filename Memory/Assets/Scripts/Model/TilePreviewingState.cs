namespace Memory.Models
{
    public class TilePreviewingState : TileStateBaseClass
    {
        public override TileStates State => TileStates.Previewed;

        public TilePreviewingState(TileModel tile) : base(tile)
        {
        }
    }
}


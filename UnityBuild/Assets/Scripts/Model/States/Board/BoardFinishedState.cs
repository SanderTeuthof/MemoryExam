namespace MemoryGame.Model.States.Board
{
    public class BoardFinishedState : BoardStateBaseClass
    {
        public override BoardStates BoardStates => BoardStates.Finished;

        public BoardFinishedState(MemoryBoard board) : base(board) { }

        public override void AddPreview(Tile tile) { }
        public override void TileAnimationEnd(Tile tile) { }
    }
}
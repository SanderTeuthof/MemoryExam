using MemoryGame.Model.States.Board;
using MemoryGame.Model;
using MemoryGame.Model.States.Tiles;

public class BoardWaitStartState : BoardStateBaseClass
{
    public override BoardStates BoardStates => BoardStates.NoPreview;

    public BoardWaitStartState(MemoryBoard board) : base(board) { }
    public override void AddPreview(Tile tile) { }

    public override void TileAnimationEnd(Tile tile) { }
}
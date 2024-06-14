using MemoryGame.Data;
using System;

namespace MemoryGame.Model.States.Board
{
    public class BoardResettingState : BoardStateBaseClass
    {
        public override BoardStates BoardStates => throw new System.NotImplementedException();

        private int _amountOfNonHiddenTiles;

        public BoardResettingState(MemoryBoard board, int amountOfNonHiddenTiles) : base(board) 
        {
            _amountOfNonHiddenTiles = amountOfNonHiddenTiles;
        }

        public override void AddPreview(Tile tile) { }

        public override void TileAnimationEnd(Tile tile)
        {
            // Every time an animation ends, this number of reetting tiles is decreased by one
            _amountOfNonHiddenTiles--;

            // If this number reaches zero, the elapsed time for both players is set to 0,
            // the first player becomes the active player and the board state is set to NoPreview
            // and the board previewTiles colelction is emptied
            // a new entry is written in the ResetGame table using the webservice
            if (_amountOfNonHiddenTiles <= 0)
            {
                string activePlayer = Board.PlayerOne.IsActive ? nameof(Board.PlayerOne) : nameof(Board.PlayerTwo);
                
                Board.PlayerOne.Elapsed = 0f;
                Board.PlayerTwo.Elapsed = 0f;

                Board.PlayerOne.Score = 0;
                Board.PlayerTwo.Score = 0;

                Board.PlayerOne.IsActive = false;
                Board.PlayerTwo.IsActive = true;

                Board.PreviewingTiles.Clear();
                Board.State = new BoardNoPreviewState(Board);

                ResetGameRepository.Instance.ResetGame(DateTime.Now.ToString(), activePlayer);
            }

            
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory.Models
{
    public class BoardTwoHidingState : IBoardState
    {
        public BoardStates State => BoardStates.TwoHiding;
        public BoardModel Board { get; private set; }

        public BoardTwoHidingState(BoardModel board)
        {
            Board = board;
        }

        public void AddPreview(TileModel tile)
        {
            // Do nothing since we are currently hiding two tiles
        }

        public void TileAnimationEnded(TileModel tile)
        {
            // Remove the tile from the list of previewing tiles
            Board.PreviewingTiles.Clear();
            Board.FlipPlayer();

            // Check if there are no more previewing tiles
            if (Board.PreviewingTiles.Count == 0)
            {
                // Set the state of the board back to NoPreviewState
                Board.State = new BoardNoPreviewState(Board);
            }
        }
    }
}

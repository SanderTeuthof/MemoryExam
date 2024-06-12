using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory.Models
{
    public class BoardTwoFoundState : IBoardState
    {
        public BoardStates State => BoardStates.TwoFound;
        public BoardModel Board { get; private set; }

        public BoardTwoFoundState(BoardModel board)
        {
            Board = board;

            Board.PreviewingTiles.Clear();

            // Check if all tiles have been found
            if (Board.Tiles.FindAll(t => t.State.State == TileStates.Hidden).Count < 2)
            {
                // If less than 2 hidden tiles remain, set the state to Finished
                Board.State = new BoardFinishedState(Board);
            }
            else
            {
                // Otherwise, set the state back to NoPreview
                Board.State = new BoardNoPreviewState(Board);
            }
        }

        public void AddPreview(TileModel tile)
        {
            // Do nothing since no previews are allowed in this state
        }

        public void TileAnimationEnded(TileModel tile)
        {
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory.Models
{
    public class BoardTwoPreviewState : IBoardState
    {
        public BoardStates State => BoardStates.TwoPreview;
        public BoardModel Board { get; private set; }

        public BoardTwoPreviewState(BoardModel board)
        {
            Board = board;
        }

        public void AddPreview(TileModel tile)
        {
            // Do nothing since we are already in a two preview state
        }

        public void TileAnimationEnded(TileModel tile)
        {
            // Check if both tiles have finished their preview animations
            if (Board.PreviewingTiles.Count == 2 && Board.PreviewingTiles.Contains(tile))
            {
                // Check if the tiles form a match
                if (Board.CheckCombination())
                {
                    foreach (var previewingTile in Board.PreviewingTiles)
                    {
                        previewingTile.State = new TileFoundState(previewingTile);
                    }

                    Board.PreviewingTiles.Clear();

                    // Check if all tiles have been found
                    if (Board.Tiles.FindAll(t => t.State.State == TileStates.Hidden).Count < 2)
                    {
                        Board.AddScore();
                        // If less than 2 hidden tiles remain, set the state to Finished
                        Board.State = new BoardFinishedState(Board);
                    }
                    else
                    {
                        Board.AddScore();
                        // Otherwise, set the state back to NoPreview
                        Board.State = new BoardNoPreviewState(Board);
                    }


                }
                else
                {                    
                    foreach (var previewingTile in Board.PreviewingTiles)
                    {
                        previewingTile.State = new TileHiddenState(previewingTile);
                    }

                    Board.State = new BoardTwoHidingState(Board);
                }
            }
        }
    }
}

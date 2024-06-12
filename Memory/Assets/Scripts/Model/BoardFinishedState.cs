using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory.Models
{
    public class BoardFinishedState : IBoardState
    {
        public BoardStates State => BoardStates.Finished;
        public BoardModel Board { get; private set; }

        public BoardFinishedState(BoardModel board)
        {
            Board = board;
        }

        public void AddPreview(TileModel tile)
        {
            // Do nothing since the game has finished
        }

        public void TileAnimationEnded(TileModel tile)
        {
            // Do nothing since the game has finished
        }
    }
}


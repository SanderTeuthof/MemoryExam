using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory.Models
{
    public class TileFoundState : ITileState
    {
        public TileStates State => TileStates.Found;
        public TileModel Tile { get; private set; }

        public TileFoundState(TileModel tile)
        {
            Tile = tile;
        }
    }
}

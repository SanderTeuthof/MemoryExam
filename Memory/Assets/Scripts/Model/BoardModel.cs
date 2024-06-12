using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace Memory.Models
{
    public class BoardModel : BaseModel
    {
        // Properties
        private int rows;
        private int columns;
        private List<TileModel> tiles;
        private List<TileModel> previewingTiles;
        private IBoardState state;

        public int Rows
        {
            get => rows;
            private set
            {
                if (rows != value)
                {
                    rows = value;
                    
                }
            }
        }

        public int Columns
        {
            get => columns;
            private set
            {
                if (columns != value)
                {
                    columns = value;
                    
                }
            }
        }

        public int NumTiles;

        public List<TileModel> Tiles
        {
            get => tiles;
            private set
            {
                if (tiles != value)
                {
                    tiles = value;
                    
                }
            }
        }

        public List<TileModel> PreviewingTiles
        {
            get => previewingTiles;
            private set
            {
                if (previewingTiles != value)
                {
                    previewingTiles = value;
                    
                }
            }
        }

        public IBoardState State
        {
            get => state;
            set
            {
                if (state != value)
                {
                    state = value;
                    if(state.State == BoardStates.Finished)
                        OnPropertyChanged();
                }
            }
        }

        public PlayerModel Player1 { get; set; }
        public PlayerModel Player2 { get; set; }

        // Constructors
        public BoardModel(int rows, int columns, int numberOfTiles, PlayerModel player1, PlayerModel player2)
        {
            Rows = rows;
            Columns = columns;
            NumTiles = numberOfTiles;
            Tiles = new List<TileModel>();
            PreviewingTiles = new List<TileModel>();
            Player1 = player1;
            Player2 = player2;

            // Fill the Tiles property with tile objects
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    if (row * columns + col < NumTiles)
                        Tiles.Add(new TileModel(row, col, this));
                }
            }

            AssignMemoryCardIds();
            State = new BoardNoPreviewState(this);
            Player1 = player1;
            Player2 = player2;
        }

        // Methods
        private void AssignMemoryCardIds()
        {
            // Assign memory card IDs to tiles
            List<int> ids = new List<int>();
            for (int i = 0; i < NumTiles / 2; i++)
            {
                ids.Add(i);
                ids.Add(i);
            }
            if ((NumTiles) % 2 != 0)
                ids.Add(NumTiles - 1);

            Shuffle(ids);

            for (int i = 0; i < NumTiles; i++)
            {
                Tiles[i].MemoryCardId = ids[i];
            }
        }

        private void Shuffle<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Random ran = new Random();
                int randomIndex = ran.Next(i, list.Count);
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        public override string ToString()
        {
            return $"MemoryBoard(Rows: {Rows}, Columns: {Columns})";
        }

        public bool CheckCombination()
        {
            if (PreviewingTiles.Count == 2 && PreviewingTiles[0].MemoryCardId == PreviewingTiles[1].MemoryCardId)
            {

                return true;
            }
            else
            {

                return false;
            }
        }

        public void FlipPlayer()
        {
            Player1.IsActive = !Player1.IsActive;
            Player2.IsActive = !Player2.IsActive;
        }

        public void AddScore()
        {
            if (Player1.IsActive)
            {
                Player1.Score++;
            }
            else
            {
                Player2.Score++;
            }
        }
    }
}

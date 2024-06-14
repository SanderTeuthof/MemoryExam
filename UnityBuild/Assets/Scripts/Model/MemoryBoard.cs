using MemoryGame.Data;
using MemoryGame.Model.States.Board;
using MemoryGame.Model.States.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame.Model
{
    public class MemoryBoard : ModelBaseClass
    {
        #region Properties
        public int Rows
        {
            get { return _rows; }
            set
            {
                if (_rows == value) { return; }
                _rows = value;
                OnPropertyChanged();
            }
        }

        public int Columns
        {
            get { return _columns; }
            set
            {
                if (_columns == value) { return; }
                _columns = value;
                OnPropertyChanged();
            }
        }

        public List<Tile> Tiles
        {
            get { return _tiles; }
            set
            {
                if (_tiles == value) { return; }
                _tiles = value;
                OnPropertyChanged();
            }
        }

        public List<Tile> PreviewingTiles
        {
            get { return _previewingTiles; }
            set
            {
                if (_previewingTiles == value) { return; }
                _previewingTiles = value;
                OnPropertyChanged();
            }
        }

        public bool IsCombinationFound
        {
            get 
            {
                if (PreviewingTiles.Count != 2) { return false; }
                if (PreviewingTiles[0].MemoryCardId == PreviewingTiles[1].MemoryCardId) { return true; }
                return false;
            }
        }

        public IBoardState State
        {
            get { return _state; }
            set
            {
                if (_state == value) { return; }
                _state = value;
                OnPropertyChanged();
            }
        }

        public Player PlayerOne
        {
            get { return _playerOne; }
            set
            {
                if (_playerOne == value) { return; }
                _playerOne = value;
                OnPropertyChanged();
            }
        }

        public Player PlayerTwo
        {
            get { return _playerTwo; }
            set
            {
                if (_playerTwo == value) { return; }
                _playerTwo = value;
                OnPropertyChanged();
            }
        }

        public float TileSpacing
        {
            get { return _tileSpacing; }
            private set { }
        }

        private int _rows;
        private int _columns;
        private List<Tile> _tiles;
        private List<Tile> _previewingTiles;
        private IBoardState _state;
        private Player _playerOne;
        private Player _playerTwo;
        private float _tileSpacing;
        private Random _random;

        #endregion

        public MemoryBoard(int rows, int columns, float tileSpacing, Player playerOne, Player playerTwo)
        {
            Rows = rows;
            Columns = columns;

            _tileSpacing = tileSpacing;

            PlayerOne = playerOne;
            PlayerTwo = playerTwo;

            PlayerOne.IsActive = false;
            PlayerTwo.IsActive = false;
            _state = new BoardWaitStartState(this);
            
            _random = new Random();

            CreateTiles();
            AssignMemoryCardIds();
        }

        public void StartGame()
        {
            _state = new BoardNoPreviewState(this);
            PlayerOne.IsActive = true;
            PlayerTwo.IsActive = false;
        }

        public void ResetGame()
        {
            // All non hidden tiles are hidden (which starts an animation)
            List<Tile> nonHiddenTiles = Tiles.Where(x => x.State.State != TileStates.Hidden).ToList();

            foreach (Tile tile in nonHiddenTiles)
            {
                tile.State = new TileHiddenState(tile);
            }

            // The board changes to a new type of state: BoardResettingState which keeps track of the number of resetting tiles
            _state = new BoardResettingState(this, nonHiddenTiles.Count);
        }

        private void CreateTiles()
        {
            Tiles = new List<Tile>();
            PreviewingTiles = new List<Tile>();

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Tiles.Add(new Tile(r, c, this));
                }
            }
        }

        private void AssignMemoryCardIds()
        {
            ImageRepository repository = ImageRepository.Instance;
            repository.ProcessImageIds(AssignMemoryCardIds);
        }

        private void AssignMemoryCardIds(List<int> memoryCardIds)
        {
            memoryCardIds = Shuffle(memoryCardIds);
            List<Tile> shuffledTiles = Shuffle(Tiles);
            int memoryCardIndex = 0;
            bool first = true;
            foreach (Tile tile in shuffledTiles)
            {
                tile.MemoryCardId = memoryCardIds[memoryCardIndex];
                if (first)
                {
                    first = false;
                }
                else
                {
                    memoryCardIndex++;
                    first = true;
                }
            }
        }

        private List<T> Shuffle<T>(List<T> list)
        {
            List<T> shuffledList = new List<T>(list);
            int n = shuffledList.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(0, n + 1);
                T value = shuffledList[k];
                shuffledList[k] = shuffledList[n];
                shuffledList[n] = value;
            }
            return shuffledList;
        }

        public void ToggleActivePlayer()
        {
            _playerOne.IsActive = _playerTwo.IsActive;
            _playerTwo.IsActive = !_playerOne.IsActive;
        }

        public Player GetActivePlayer()
        {
            return _playerOne.IsActive == true ? _playerOne : _playerTwo;
        }

        public override string ToString()
        {
            return $"MemoryBoard({Rows}, {Columns})";
        }
    }
}
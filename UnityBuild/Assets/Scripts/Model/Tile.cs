using MemoryGame.Model.States.Tiles;

namespace MemoryGame.Model
{
    public class Tile : ModelBaseClass
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

        public MemoryBoard Board
        {
            get { return _board; }
            set
            {
                if (_board == value) { return; }
                _board = value;
                OnPropertyChanged();
            }
        }

        public int MemoryCardId
        {
            get { return _memoryCardId; }
            set
            {
                if (_memoryCardId == value) { return; }
                _memoryCardId = value;
                OnPropertyChanged();
            }
        }

        public ITileState State
        {
            get { return _state; }
            set 
            {
                if (_state == value) { return; }
                _state = value;
                OnPropertyChanged();
            }
        }

        private int _rows;
        private int _columns;
        private MemoryBoard _board;
        private int _memoryCardId;
        private ITileState _state;
        #endregion

        public Tile(int row, int column, MemoryBoard board)
        {
            Rows = row;
            Columns = column;
            Board = board;

            State = new TileHiddenState(this);
        }

        public override string ToString()
        {
            return $"Tile({Rows}, {Columns})";
        }
    }
}
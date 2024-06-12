
namespace Memory.Models
{
    public class TileModel : BaseModel
    {
        // Properties
        public int Row { get; private set; }
        public int Column { get; private set; }
        public BoardModel Board { get; private set; }
        private int _memoryCardId;
        public int MemoryCardId
        {
            get { return _memoryCardId; }
            set
            {
                if (_memoryCardId != value)
                {
                    _memoryCardId = value;
                }
            }
        }

        private ITileState _state;

        public ITileState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged();
                }
            }
        }

        // Constructors
        public TileModel(int row, int column, BoardModel board)
        {
            Row = row;
            Column = column;
            Board = board;
            State = new TileHiddenState(this);
        }

        // Override ToString method
        public override string ToString()
        {
            return $"Tile(Row: {Row}, Column: {Column})";
        }
    }

}


namespace Memory.Models
{
    public class PlayerModel : BaseModel
    {
        private bool _isActive;
        private int _score;

        public string Name { get; set; }
        public int Score 
        { 
            get { return _score; }
            set
            {
                if(_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }
        public float ElapsedTime { get; set; }
        public bool IsActive 
        { 
            get { return _isActive; }

            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged();
                }
            }
        }

        public PlayerModel(bool active)
        {
            IsActive = active;
        }
    }
}


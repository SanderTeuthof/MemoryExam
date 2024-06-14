using MemoryGame.Model;

public class Player : ModelBaseClass
{
    #region Properties
    public string Name
	{
		get { return _name; }
        set
        {
            if (_name == value) { return; }
            _name = value;
            OnPropertyChanged();
        }
    }

    public int Score
    {
        get { return _score; }
        set
        {
            if (_score == value) { return; }
            _score = value;
            OnPropertyChanged();
        }
    }

    public bool IsActive
    {
        get { return _isActive; }
        set
        {
            if (_isActive == value) { return; }
            _isActive = value;
            OnPropertyChanged();
        }
    }

    public float Elapsed
    {
        get { return _elapsed; }
        set
        {
            if (_elapsed == value) { return; }
            _elapsed = value;
            OnPropertyChanged();
        }
    }

    private string _name;
    private int _score;
    private bool _isActive;
    private float _elapsed;
    #endregion

    public Player(string name)
    {
        Name = name;
        Score = 0;
        IsActive = false;
        Elapsed = 0;
    }
}

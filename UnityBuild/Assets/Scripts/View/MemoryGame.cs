using MemoryGame.Data;
using MemoryGame.Model;
using MemoryGame.Model.States.Board;
using System;
using UnityEngine;

namespace MemoryGame.View
{
    public class MemoryGame : MonoBehaviour
    {
        [Header("Memory Board")]
        [SerializeField] private GameObject _memoryBoard;
        [Header("Memory Card Tiles")]
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private float _tileSize;
        [SerializeField] private float _tileSpacing;
        [Header("Players")]
        [SerializeField] private PlayerView _playerOne;
        [SerializeField] private PlayerView _playerTwo;
        [Header("Loading Screen")]
        [SerializeField] private GameObject _loadingScreen;
        

        private MemoryBoard _board;

        private bool _areTexturesLoaded;

        private bool _player1Set;
        private bool _player2Set;
        

        private void Start()
        {
            _loadingScreen.SetActive(true);

            _areTexturesLoaded = false;
            _player1Set = false;
            _player2Set = false;

            _playerOne.Model = new Player("Player One");
            _playerTwo.Model = new Player("Player Two");

            _board = new MemoryBoard(3, 3, 0.066f, _playerOne.Model, _playerTwo.Model);
            _board.Tiles[UnityEngine.Random.Range(0, _board.Tiles.Count)].PropertyChanged += OnTilePropertyChanged;
            _board.PropertyChanged += OnModelPropertyChanged;

            MemoryBoardView boardView = _memoryBoard.GetComponent<MemoryBoardView>();
            boardView.TexturesLoaded += BoardView_TexturesLoaded;
            boardView.SetUpMemoryBoardView(_board, _tilePrefab);

            float offsetX = -(_board.Columns / 2f - 0.5f + _tileSpacing) * _tileSize;
            float offsetZ = -(_board.Rows / 2f - 0.5f + _tileSpacing) * _tileSize;
            _memoryBoard.transform.position += new Vector3(offsetX, 0, offsetZ);

            
        }

        private void Update()
        {
            _board.GetActivePlayer().Elapsed += Time.deltaTime;
        }

        public void UpdatePlayerOne(string newName)
        {
            _playerOne.UpdateName(newName);
            _player1Set = true;
            TryStartGame();
        }

        public void UpdatePlayerTwo(string newName)
        {
            _playerTwo.UpdateName(newName);
            _player2Set = true;
            TryStartGame();
        }

        private void OnTilePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        private void OnModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_board.State))
            {
                if (_board.State.BoardStates == BoardStates.NoPreview)
                {
                    _board.ToggleActivePlayer();
                }

                if (_board.State.BoardStates == BoardStates.TwoFound)
                {
                    _board.GetActivePlayer().Score++;
                }

                if (_board.State.BoardStates == BoardStates.Finished)
                {
                    FinishGame();
                }
            }
        }

        private void BoardView_TexturesLoaded(object sender, System.EventArgs e)
        {
            _areTexturesLoaded = true;         

            TryStartGame() ;
        }

        private void TryStartGame()
        {
            if (_areTexturesLoaded && _player1Set && _player2Set) 
                StartGame();
        }

        private void StartGame()
        {
            _board.StartGame();
            _playerOne.ShowUserInterface();
            _playerTwo.ShowUserInterface();
            _loadingScreen.SetActive(false);

            NamesRepository.Instance.PostNames(_playerOne.GetName(), _playerTwo.GetName());
            Debug.Log($"Posted: {_playerOne.GetName()} and {_playerTwo.GetName()} to the names Database");

            _memoryBoard.GetComponent<MemoryBoardView>().TexturesLoaded -= BoardView_TexturesLoaded;
        }

        public void Reset()
        {
            _board.ResetGame();
        }

        private void FinishGame()
        {
            
        }
    }
}
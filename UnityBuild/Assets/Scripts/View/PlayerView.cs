using MemoryGame.View;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : ViewBaseClass<Player>
{
    [SerializeField] private GameObject _userInterface;
    [SerializeField] private Text _playerName;
    [SerializeField] private Text _playerScore;
    [SerializeField] private Text _playerElapsed;
    [SerializeField] private Image _activeBackground;
    [SerializeField] private Image _nonActiveBackground;

    public void UpdateName(string newName)
    {
        Model.Name = newName;
    }

    public string GetName()
    {
        return Model.Name;
    }

    protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Player.Name):
                _playerName.text = Model.Name.ToString();
                break;
            case nameof(Player.Score):
                _playerScore.text = Model.Score.ToString();
                break;
            case nameof(Player.IsActive):
                _activeBackground.gameObject.SetActive(Model.IsActive);
                _nonActiveBackground.gameObject.SetActive(!Model.IsActive);
                break;
            case nameof(Player.Elapsed):
                _playerElapsed.text = FormatSeconds(Model.Elapsed);
                break;
            default:
                break;
        }
    }

    private string FormatSeconds(float elapsed)
    {
        int d = (int)(elapsed * 100f);
        int minutes = d / (60 * 100);
        int seconds = (d % (60 * 100)) / 100;
        int hundredths = d % 100;
        return String.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
    }

    public void ShowUserInterface()
    {
        _userInterface.SetActive(true);

        _playerName.text = Model.Name.ToString();
        _playerScore.text = Model.Score.ToString();
        _playerElapsed.text = FormatSeconds(Model.Elapsed);

        _activeBackground.gameObject.SetActive(Model.IsActive);
        _nonActiveBackground.gameObject.SetActive(!Model.IsActive);
    }
}

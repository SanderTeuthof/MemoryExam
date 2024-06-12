using Memory.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private PlayerView _player1;
    [SerializeField]
    private PlayerView _player2;

    private PlayerView _activePlayer;

    private UIDocument _document;

    private VisualElement _root;
    private VisualElement _player1Element;
    private VisualElement _player2Element;

    private Label _player1Name;
    private Label _player1Score;
    private Label _player1Time;

    private Label _player2Name;
    private Label _player2Score;
    private Label _player2Time;

    private void Start()
    {
        _document = GetComponent<UIDocument>();

        _root = _document.rootVisualElement;

        _player1Element = _root.Q<VisualElement>("Player1UI");
        _player2Element = _root.Q<VisualElement>("Player2UI");

        _player1Name = _player1Element.Q<Label>("Player1Name");
        _player1Score = _player1Element.Q<Label>("Player1Score");
        _player1Time = _player1Element.Q<Label>("Player1Time");

        _player2Name = _player2Element.Q<Label>("Player2Name");
        _player2Score = _player2Element.Q<Label>("Player2Score");
        _player2Time = _player2Element.Q<Label>("Player2Time");

        _player1.ActiveChanged += Player1_ActiveChanged;
        _player2.ActiveChanged += Player2_ActiveChanged;
        _player1.ScoreChanged += Player_ScoreChanged;
        _player2.ScoreChanged += Player_ScoreChanged;

        FirstSetup();
    }

    private void Player_ScoreChanged(object sender, EventArgs e)
    {
        SetScore();
    }

    private void FirstSetup()
    {
        if (_player1.Model.IsActive)
        {
            _player1Name.text = _player1.Model.Name;
        }
        else
        {
            _player1Name.text = "";
        }

        if (_player2.Model.IsActive)
        {
            _player2Name.text = _player2.Model.Name;
        }
        else
        {
            _player2Name.text = "";
        }

        _player1Score.text = "0";
        _player2Score.text = "0";
    }

    private void Player2_ActiveChanged(object sender, EventArgs e)
    {
        
        if(_player2.Model.IsActive)
        {
            _player2Name.text = _player2.Model.Name;
        }
        else
        {
            _player2Name.text = "";
        }
    }



    private void Player1_ActiveChanged(object sender, EventArgs e)
    {
        if(_player1.Model.IsActive)
        {
            _player1Name.text = _player1.Model.Name;
        }
        else
        {
            _player1Name.text = "";
        }
    }

    private void SetScore()
    {
        _player1Score.text = _player1.Model.Score.ToString();
        _player2Score.text = _player2.Model.Score.ToString();
    }

    private void LateUpdate()
    {
        SetPlayer1Time();
        SetPlayer2Time();
    }

    private void SetPlayer1Time()
    {
        _player1Time.text = ((int)_player1.Model.ElapsedTime).ToString();
    }

    private void SetPlayer2Time()
    {
        _player2Time.text = ((int)_player2.Model.ElapsedTime).ToString();
    }
}

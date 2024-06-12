using UnityEngine;
using Memory.Models;
using Memory.View;
using System.Collections.Generic;
using System;

public class MemoryGame : MonoBehaviour
{
    [SerializeField]
    private GameObject _memoryBoardPrefab;
    [SerializeField]
    private GameObject _tilePrefab;
    [SerializeField]
    private Material _material;
    [SerializeField]
    private PlayerView _player1;
    [SerializeField]
    private PlayerView _player2;

    private BoardModel _memoryBoard;
    private BoardView _boardView;

    private void Awake()
    {

        _player1.Model = new PlayerModel(true);
        _player2.Model = new PlayerModel(false);
    }

    public void SetUpMemoryGame(List<Texture2D> textures)
    {
        int totalTiles = textures.Count * 2;
        int numRows = Mathf.CeilToInt(Mathf.Sqrt(totalTiles));
        int numColumns = Mathf.CeilToInt(Mathf.Sqrt(totalTiles));

        // Create a MemoryBoard with calculated rows and columns
        _memoryBoard = new BoardModel(numRows, numColumns, totalTiles, _player1.Model, _player2.Model);

        // Instantiate MemoryBoardView and set up the view
        GameObject memoryBoardGO = Instantiate(_memoryBoardPrefab);
        _boardView = memoryBoardGO.GetComponent<BoardView>();
        _boardView.SetUpMemoryBoardView(_memoryBoard, _tilePrefab, CreateMaterialVariants(textures));
    }

    private List<Material> CreateMaterialVariants(List<Texture2D> textures)
    {
        List<Material> materialVariants = new List<Material>();

        foreach (Texture2D tex in textures)
        {
            // Create a new material instance
            Material newMaterial = new Material(_material);

            // Assign the texture to the material
            newMaterial.mainTexture = tex;

            // Add the new material to the list
            materialVariants.Add(newMaterial);
        }

        return materialVariants;
    }
}

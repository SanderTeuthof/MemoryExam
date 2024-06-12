using UnityEngine;
using System.ComponentModel;
using Memory.Models;
using System.Collections.Generic;

namespace Memory.View
{
    public class BoardView : BaseView<BoardModel>
    {

        [SerializeField]
        private float _tileSpacing = 1f;
        
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void SetUpMemoryBoardView(BoardModel model, GameObject tilePrefab, List<Material> materials)
        {
            Model = model;

            int numRows = model.Rows;
            int numColumns = model.Columns;
            int numTiles = model.NumTiles;
            float tileSpacing = _tileSpacing; 

            // Calculate the total width and height of the board
            float boardWidth = numColumns * tileSpacing;
            float boardHeight = numRows * tileSpacing;

            // Calculate the starting position for the first tile
            float startX = -boardWidth / 2.0f + tileSpacing / 2.0f;
            float startZ = boardHeight / 2.0f - tileSpacing / 2.0f;

            // Instantiate tiles based on the MemoryBoard model
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numColumns; col++)
                {
                    if (row * numColumns + col < numTiles)
                    {
                        // Calculate the position for the current tile
                        float posX = startX + col * tileSpacing;
                        float posZ = startZ - row * tileSpacing;

                        // Instantiate the tile at the calculated position
                        Vector3 tilePosition = new Vector3(posX, 0f, posZ);
                        GameObject tileGO = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);

                        // Set the model for the TileView component
                        TileView tileView = tileGO.GetComponent<TileView>();
                        tileView.Model = model.Tiles[row * numColumns + col];

                        // Assign the material to the tile
                        int materialIndex = tileView.Model.MemoryCardId;

                        Renderer tileRenderer = tileGO.GetComponentInChildren<Renderer>();
                        Material[] newMaterials = tileRenderer.materials;
                        newMaterials[1] = materials[materialIndex];
                        tileRenderer.materials = newMaterials;
                    }
                }
            }

            _camera.orthographicSize = boardHeight / 2 * 1.2f;
        }

        public override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(Model.State.State == BoardStates.Finished)
            {
                if (Model.Player1.Score > Model.Player2.Score)
                    Debug.Log($"{Model.Player1.Name} won!");
                else if (Model.Player1.Score < Model.Player2.Score)
                    Debug.Log($"{Model.Player2.Name} won!");
                else
                    Debug.Log("It's a tie!");
            }
            
        }
    }
}

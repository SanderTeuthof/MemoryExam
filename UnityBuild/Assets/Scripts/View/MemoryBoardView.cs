using MemoryGame.Model;
using System;
using System.ComponentModel;
using UnityEngine;

namespace MemoryGame.View
{
    public class MemoryBoardView : ViewBaseClass<MemoryBoard>
    {
        public event EventHandler TexturesLoaded;

        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e) { }

        private int _loadedTextures;

        public void SetUpMemoryBoardView(MemoryBoard model, GameObject tilePrefab)
        {
            Model = model;
            _loadedTextures = 0;

            for (int i = 0; i < Model.Tiles.Count; i++)
            {
                Tile tile = Model.Tiles[i];

                GameObject tileViewObject = Instantiate(tilePrefab,
                    new Vector3(tile.Rows + (model.TileSpacing * tile.Rows), 0, tile.Columns + (model.TileSpacing * tile.Columns)),
                    Quaternion.Euler(-90, 0, 0),
                    transform);

                TileView tileView = tileViewObject.GetComponent<TileView>();
                tileView.Model = tile;
                tileView.TextureLoaded += TileView_TextureLoaded;
            }
        }

        private void TileView_TextureLoaded(object sender, EventArgs e)
        {
            _loadedTextures++;

            if (_loadedTextures == Model.Tiles.Count) { TexturesLoaded?.Invoke(this, EventArgs.Empty); }
        }
    }
}
using UnityEngine.EventSystems;
using Memory.Models;
using System.ComponentModel;
using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Memory.View
{
    public class TileView : BaseView<TileModel>
    {
        private FlipScript _flipper;

        private void Start()
        {
            _flipper = GetComponent<FlipScript>();
            _flipper.AnimationDone += FlipScript_AnimationDone;
        }

        private void FlipScript_AnimationDone(object sender, EventArgs e)
        {
            Model.Board.State.TileAnimationEnded(this.Model);
        }

        public void Clicked()
        {
            // Execute AddPreview on the State of the Board of the Model
            Model.Board.State.AddPreview(Model);
        }

        public override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Start animation if the changed property is the State property
            if (e.PropertyName == nameof(TileModel.State))
            {
                StartAnimation(Model.State.State);
            }
        }

        private void StartAnimation(TileStates state)
        {
            switch (state)
            {
                // Add cases for different tile states and start corresponding animations
                case TileStates.Hidden:
                    // Start animation to hide the tile
                    _flipper.FlipBack();
                    break;
                case TileStates.Previewed:
                    // Start animation to preview the tile
                    _flipper.FlipForward();
                    break;
                case TileStates.Found:
                    // No animation needed for found tiles
                    break;
                default:
                    break;
            }
        }
    }
}


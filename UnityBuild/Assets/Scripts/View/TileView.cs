using MemoryGame.Data;
using MemoryGame.Model;
using MemoryGame.Model.States.Tiles;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MemoryGame.View
{
    [RequireComponent(typeof(Animator))]
    public class TileView : ViewBaseClass<Tile>, IPointerDownHandler
    {
        public event EventHandler TextureLoaded;

        // TODO: Find better way
        public Renderer FrontRenderer;

        private Animator _animator;

        public void OnPointerDown(PointerEventData eventData)
        {
            Model.Board.State.AddPreview(Model);
        }

        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Model.State))) { StartAnimation(); }
            else if (e.PropertyName.Equals(nameof(Model.MemoryCardId))) { LoadFront(); }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            AddEndAnimationEvent();
        }

        private void LoadFront()
        {
            ImageRepository.Instance.GetProcessTexture(Model.MemoryCardId, LoadFront);
        }  
        
        private void LoadFront(Texture2D texture)
        {
            gameObject.transform.Find("Back").GetComponent<Renderer>().material.mainTexture = texture;
            TextureLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void AddEndAnimationEvent()
        {
            for (int i = 0; i < _animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                AnimationClip clip = _animator.runtimeAnimatorController.animationClips[i];

                AnimationEvent animationEndEvent = new AnimationEvent();
                animationEndEvent.time = clip.length;
                animationEndEvent.functionName = nameof(AnimationCompleteHandler);
                animationEndEvent.stringParameter = clip.name;

                clip.AddEvent(animationEndEvent);
            }
        }

        private void AnimationCompleteHandler()
        {
            Model.Board.State.TileAnimationEnd(Model);
        }

        private void StartAnimation()
        {
            if (Model.State.State == TileStates.Preview || Model.State.State == TileStates.Found)
            {
                _animator.Play("State_Show");
            }
            else if (Model.State.State == TileStates.Hidden)
            {
                _animator.Play("State_Hide");
            }
        }
    }
}
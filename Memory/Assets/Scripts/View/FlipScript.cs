using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Memory.View
{
    public class FlipScript : MonoBehaviour
    {
        [SerializeField]
        private float _flipTime = 1f;

        public EventHandler AnimationDone;

        private bool _flipping = false;

        private quaternion _rotation;

        private void Start()
        {
            _rotation = transform.rotation;
        }

        public void FlipForward()
        {
            if (_flipping)
                return;

            // Calculate the target rotation (180 degrees around the Y-axis)
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(180f, 0f, 0f);

            StartCoroutine(Flip(startRotation, targetRotation));
        }

        public void FlipBack()
        {
            if (_flipping)
                return;

            // Calculate the target rotation (180 degrees around the Y-axis)
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);

            StartCoroutine(Flip(startRotation, targetRotation));
        }

        private IEnumerator Flip(Quaternion startRotation, Quaternion targetRotation)
        {
            _flipping = true;

            float elapsedTime = 0f;

            while (elapsedTime < _flipTime)
            {
                // Interpolate between the start and target rotations
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / _flipTime);

                // Update the elapsed time
                elapsedTime += Time.deltaTime;

                // Wait for the next frame
                yield return null;
            }

            // Ensure the rotation ends at exactly 180 degrees
            transform.rotation = targetRotation;
            _flipping = false ;
            AnimationDone.Invoke(this, EventArgs.Empty);
        }
    }
}


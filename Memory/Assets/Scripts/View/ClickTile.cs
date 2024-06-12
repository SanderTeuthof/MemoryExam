using Memory.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTile : MonoBehaviour
{
    void Update()
    {
        // Check if the Fire1 button (left mouse button by default) is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // Shoot a ray from the camera to the cursor position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any object
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object has the TileView component
                TileView tileView = hit.collider.GetComponent<TileView>();
                if (tileView != null)
                {
                    tileView.Clicked();
                }
            }
        }
    }
}

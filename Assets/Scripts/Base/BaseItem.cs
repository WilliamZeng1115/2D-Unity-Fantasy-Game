using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour {

    private float xOffset, yOffset;

    public void OnMouseDown()
    {
        xOffset = transform.position.x - Input.mousePosition.x;
        yOffset = transform.position.y - Input.mousePosition.y;
    }
     
    public void OnMouseDrag()
    {
        transform.position = new Vector2(xOffset + Input.mousePosition.x, yOffset + Input.mousePosition.y);
    }
}

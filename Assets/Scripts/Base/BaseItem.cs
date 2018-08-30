using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour {

    private float xOffset, yOffset;

    protected float size;
    protected int cost, count;
    protected string name, description;

    protected float value;

    public abstract void use();

    public void drop()
    {
        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        xOffset = transform.position.x - Input.mousePosition.x;
        yOffset = transform.position.y - Input.mousePosition.y;
    }
     
    public void OnMouseDrag()
    {
        transform.position = new Vector2(xOffset + Input.mousePosition.x, yOffset + Input.mousePosition.y);
    }

    public float getSize()
    {
        return size;
    }

    public int getCost()
    {
        return cost;
    }

    public string getName()
    {
        return name;
    }

    public string getDescription()
    {
        return description;
    }
    
    public float getValue()
    {
        return value;
    }
}

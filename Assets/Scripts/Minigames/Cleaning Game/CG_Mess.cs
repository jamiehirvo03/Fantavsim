using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_Mess : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onCreateMessItem += OnCreateMessItem;
    }

    private void OnCreateMessItem()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            //Move object based on objects centre and where the mouse is on screen
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        //Check the distance between the centre of the selected object and the mouse location
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Start dragging
        dragging = true;
    }

    private void OnMouseUp()
    {
        //Stop dragging
        dragging = false;
    }
}

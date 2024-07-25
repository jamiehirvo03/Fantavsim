using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CG_Mess : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    private List<string> messType = new List<string>(4) {"Tankard", "Scraps", "Dust", "Rodents"};
    [SerializeField] private string thisMessType;

    [SerializeField] private Vector3 thisMessOrigin;

    public GameObject tankardBin;
    public GameObject scrapsBin;
    public GameObject dustBin;
    public GameObject rodentsBin;

    [SerializeField] private string hoveringOver;

    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onCreateMessItem += OnCreateMessItem;
    }

    private void OnCreateMessItem()
    {
        thisMessType = messType[Random.Range(0,messType.Count)];
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

        //Get objects origin point
        thisMessOrigin = transform.position;
    }

    private void OnMouseUp()
    {
        //Stop dragging
        dragging = false;

        //Check if the object is hovering over the correct bin
        if (hoveringOver == thisMessType)
        {
            //Trigger correct placement event
            CG_Events.current.MessPlacementCorrect();

            //Delete mess instance
            Destroy(this.gameObject);
        }
        else
        {
            //Trigger incorrect placement event
            CG_Events.current.MessPlacementIncorrect();

            //Reset mess back to its origin
            transform.position = thisMessOrigin;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CG_Mess : MonoBehaviour
{
    public bool placedCorrectly = false;
    private bool dragging = false;
    private Vector3 offset;

    private List<string> messType = new List<string>(4) {"Tankard", "Scraps", "Dust", "Rodents"};
    private string thisMessType;

    [SerializeField] private Vector3 thisMessOrigin;

    [SerializeField] private string hoveringOver;

    public Sprite EmptyTankard;
    public Sprite FoodScraps;
    public Sprite Dust;
    public Sprite Rodents;

    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onCreateMessItem += OnCreateMessItem;
    }

    private void OnCreateMessItem()
    {
        thisMessType = messType[Random.Range(0,messType.Count)];

        Sprite thisMessSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        if (thisMessType == "Tankard")
        {
            thisMessSprite = EmptyTankard;
        }
        if (thisMessType == "Scraps")
        {
            thisMessSprite = FoodScraps;
        }
        if (thisMessType == "Dust")
        {
            thisMessSprite = Dust;
        }
        if (thisMessType == "Rodents")
        {
            thisMessSprite = Rodents;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!placedCorrectly)
        {
            if (dragging)
            {
                //Move object based on objects centre and where the mouse is on screen
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            }
        } 
    }

    private void OnMouseDown()
    {
        if (!placedCorrectly)
        {
            //Check the distance between the centre of the selected object and the mouse location
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Start dragging
            dragging = true;

            //Get objects origin point
            thisMessOrigin = transform.position;
        }  
    }

    private void OnMouseUp()
    {
        if (!placedCorrectly)
        {
            //Stop dragging
            dragging = false;

            //Check if the object is hovering over the correct bin
            if (hoveringOver == thisMessType)
            {
                //Trigger correct placement event
                CG_Events.current.MessPlacementCorrect();

                placedCorrectly = true;
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
}

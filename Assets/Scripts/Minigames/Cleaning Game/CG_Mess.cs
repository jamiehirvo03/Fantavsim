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
    [SerializeField] private string thisMessType;

    [SerializeField] private Vector3 thisMessOrigin;

    [SerializeField] private string hoveringOver;

    public Sprite emptyTankardSprite;
    public Sprite foodScrapSprite;
    public Sprite dustSprite;
    public Sprite rodentSprite;

    public GameObject messItem;

    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onOverTankardBin += OnOverTankardBin;
        CG_Events.current.onOverScrapsBin += OnOverScrapsBin;
        CG_Events.current.onOverDustBin += OnOverDustBin;
        CG_Events.current.onOverRodentBin += OnOverRodentBin;
        CG_Events.current.onOverNoBin += OnOverNoBin;

        hoveringOver = "None";

        CreateMessItem();
    }

    private void CreateMessItem()
    {
        thisMessType = messType[Random.Range(0,3)];

        Debug.Log($"This mess is: {thisMessType}");

        if (thisMessType == "Tankard")
        {
            messItem.GetComponent<SpriteRenderer>().sprite = emptyTankardSprite;
            Debug.Log("Tankard Sprite has loaded");
        }
        if (thisMessType == "Scraps")
        {
            messItem.GetComponent<SpriteRenderer>().sprite = foodScrapSprite;
            Debug.Log("Food Scraps Sprite has loaded");
        }
        if (thisMessType == "Dust")
        {
            messItem.GetComponent<SpriteRenderer>().sprite = dustSprite;
            Debug.Log("Dust Sprite has loaded");
        }
        if (thisMessType == "Rodents")
        {
            messItem.GetComponent<SpriteRenderer>().sprite = rodentSprite;
            Debug.Log("Rodents Sprite has loaded");
        }
    }
    private void OnOverTankardBin()
    {
        hoveringOver = "Tankard";
    }
    private void OnOverDustBin()
    {
        hoveringOver = "Dust";
    }
    private void OnOverScrapsBin()
    {
        hoveringOver = "Scraps";
    }
    private void OnOverRodentBin()
    {
        hoveringOver = "Rodents";
    }
    private void OnOverNoBin()
    {
        hoveringOver = "None";
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
                Debug.Log($"{thisMessType} has been placed correctly!");
                
                //Trigger correct placement event
                CG_Events.current.MessPlacementCorrect();

                placedCorrectly = true;
            }
            else
            {
                if (hoveringOver != "None")
                {
                    Debug.Log($"{thisMessType} has been placed incorrectly");

                    //Trigger incorrect placement event
                    CG_Events.current.MessPlacementIncorrect();
                }

                //Reset mess back to its origin
                transform.position = thisMessOrigin;
            }
        }
    }
}

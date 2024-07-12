using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlace : MonoBehaviour
{
    //allows user to assign what object gets cloned on click
    public Transform cloneObj;
    //allows user to assign value to ingredients 
    public int foodValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //all the stuff for getting ingredients to appear on plates
        if (gameObject.name == "Ingredient 1")
            Instantiate(cloneObj, new Vector3(GameManagerFS.plateXpos, 2, 0), cloneObj.rotation);

        if (gameObject.name == "Ingredient 2")
            Instantiate(cloneObj, new Vector3(GameManagerFS.plateXpos, 3, 0), cloneObj.rotation);

        if (gameObject.name == "Ingredient 3")
            Instantiate(cloneObj, new Vector3(GameManagerFS.plateXpos, 4, 0), cloneObj.rotation);

        if (gameObject.name == "Ingredient 4")
            Instantiate(cloneObj, new Vector3(GameManagerFS.plateXpos, 5, 0), cloneObj.rotation);

        if (gameObject.name == "Ingredient 5")
            Instantiate(cloneObj, new Vector3(GameManagerFS.plateXpos, 6, 0), cloneObj.rotation);

        if (gameObject.name == "Ingredient 6")
            Instantiate(cloneObj, new Vector3(GameManagerFS.plateXpos, 7, 0), cloneObj.rotation);

        //the checker to see if the plate value matches the order value
        GameManagerFS.plateValue[GameManagerFS.plateNum] += foodValue;

        //remove later
        Debug.Log(GameManagerFS.plateValue+ "  " +GameManagerFS.orderValue);
    }










}

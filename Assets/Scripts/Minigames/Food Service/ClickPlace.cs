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
        if (gameObject.name == "Ingredient 1")
            Instantiate(cloneObj, new Vector3(-7f, 2, 0), cloneObj.rotation);

        if (gameObject.name == "Ingredient 2")
            Instantiate(cloneObj, new Vector3(-7f, 3, 0), cloneObj.rotation);

        if (gameObject.name == "Ingredient 3")
            Instantiate(cloneObj, new Vector3(-7f, 4, 0), cloneObj.rotation);

        if (gameObject.name == "Ingredient 4")
            Instantiate(cloneObj, new Vector3(-7f, 5, 0), cloneObj.rotation);

        if (gameObject.name == "Ingredient 5")
            Instantiate(cloneObj, new Vector3(-7f, 6, 0), cloneObj.rotation);

        if (gameObject.name == "Ingredient 6")
            Instantiate(cloneObj, new Vector3(-7f, 7, 0), cloneObj.rotation);

        GameManagerFS.plateValue += foodValue;
        Debug.Log(GameManagerFS.plateValue+ "  " +GameManagerFS.orderValue);
    }










}

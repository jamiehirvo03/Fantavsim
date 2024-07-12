using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
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
        //on plate click, checks to see if ingredient values are correct to order
        if (GameManagerFS.orderValue==GameManagerFS.plateValue)
        {
            Debug.Log("Order Correct");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerFS : MonoBehaviour
{

    public static int [] orderValue= { 111111, 100001, 120011 };
    public static int [] plateValue = { 0, 0, 0 };

    //tracks what order number goes with what plate
    public static int plateNum = 0;
    
    //will tell engine were to move ingredients too
    public static float plateXpos = 0;
    
    //tells unity what plate it is working with

    public Transform plateSelector; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Xpos = x position 
        //move plate selector
        if(Input.GetKeyDown("tab"))
        {
            plateNum += 1;
            plateXpos += 3;
        }
        if (plateNum>2)
        {
            plateNum = 0;
            plateXpos = 0;

        }

        plateSelector.transform.position = new Vector3(plateXpos, 2.5f, 0);




    }
}

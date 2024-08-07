using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerFS : MonoBehaviour
{
    public static string[] order = { "213000", "11100", "110100", "111111", "120012", "121002", "03000" };
    public static int [] orderValue = {213000, 11100, 110100, 111111, 120012, 121002, 3000};
    public static int [] plateValue = { 0, 0, 0 };

   

    
    

    //tracks what order number goes with what plate
    public static int plateNum = 0;
    
    //will tell engine were to move ingredients too
    public static float plateXpos = 0;
    
    //tells unity what plate it is working with

    public Transform plateSelector;

    //order model
    public SpriteRenderer[] currentPNG;

    //PNG of order
    public Texture[] orderPNG;

    public static bool emptyPlateNow = false;

    public static float totalPoints = 0;


    // Start is called before the first frame update

  public void Start()
    {
        AssignSprite();
    }
    public void AssignSprite()
    {
     // change to random.range []

        for (int rep = 0; rep < 3; rep += 1)
        {

            if (orderValue[rep] == 3000) 
            currentPNG[rep].GetComponent<MeshRenderer>().material.mainTexture = orderPNG[0];

            else if (orderValue[rep] == 11100) 
            currentPNG[rep].GetComponent<MeshRenderer>().material.mainTexture = orderPNG[1];

            else if (orderValue[rep] == 110100) 
            currentPNG[rep].GetComponent<MeshRenderer>().material.mainTexture = orderPNG[2];

            else if (orderValue[rep] == 110110) 
            currentPNG[rep].GetComponent<MeshRenderer>().material.mainTexture = orderPNG[3];

            else if (orderValue[rep] == 111111) 
            currentPNG[rep].GetComponent<MeshRenderer>().material.mainTexture = orderPNG[4];

            else if (orderValue[rep] == 120012) 
            currentPNG[rep].GetComponent<MeshRenderer>().material.mainTexture = orderPNG[5];

            else if (orderValue[rep] == 121002) 
            currentPNG[rep].GetComponent<MeshRenderer>().material.mainTexture = orderPNG[6];

            else if (orderValue[rep] == 213000) 
            currentPNG[rep].GetComponent<MeshRenderer>().material.mainTexture = orderPNG[7];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for loop here somewhere

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

       //moves the plate selector to the corret y axis
        plateSelector.transform.position = new Vector3(plateXpos, 2.5f, 0);




    }
}

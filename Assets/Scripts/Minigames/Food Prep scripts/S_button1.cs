using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_button1 : MonoBehaviour
{
    S_Ingredient ingredientHealth;
    int n;
    public int ingredientDamage = 1;


    //public int for linked ingredient
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        {

        }
        //check if linked ingredient is selected
        //if so enable button
        //if not disable button
        // on click take 1 health from linked ingredient 
    }
    public void OnButtonPress()
    {
        n++;
        Debug.Log("Button clicker" + n + "times");
        
    }
}

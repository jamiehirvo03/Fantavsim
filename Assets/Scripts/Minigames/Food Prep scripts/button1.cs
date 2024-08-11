using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button1 : MonoBehaviour
{
    IngredientScript ingredientHealth;
    IngredientScript ingredientCurrentHealth;
    FoodPrep_GM Ingredients;
    
   
    int n;
    public int amount;


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

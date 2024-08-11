using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    public int ingredientHealth;
    public int ingredientCurrentHealth;
    FoodPrep_GM ingredientCap;
    // Start is called before the first frame update
    void Start()
    {
        ingredientCurrentHealth = ingredientHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ingredientCurrentHealth <= 0)
        {
            //ingredientCap = 1;
            Destroy(gameObject);
        }
        //if ingredient 1 picked load ingredient 1 sprite and values
        //if ingredient 2 picked load ingredient 2 sprite and values
        //if ingredient 3 picked load ingredient 3 sprite and values

        
        //on ingredient health reaching 0 add 1 to ingredient cap
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2 : MonoBehaviour
{
    FoodPrep_GM createIngredient;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonPress()
    {

        string[] Ingredients = new string[] { "Ingredient1", "Ingredient2", "Ingredient3" };
        System.Random random = new System.Random();
        int useIngredients = random.Next(Ingredients.Length);
        string pickIngredients = Ingredients[useIngredients];

    }
}

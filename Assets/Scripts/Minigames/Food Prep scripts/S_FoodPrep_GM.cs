using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FoodPrep_GM : MonoBehaviour
{
    //ingredient cap
    public int ingredientCap = 0;

    //public string[] Ingredients = { "Ingredient1", "Ingredient2", "Ingredient3" };
    public GameObject Ingredient1;
    public GameObject Ingredient2;
    public GameObject Ingredient3;


    // Start is called before the first frame update
    void Start()
    {
        //create ui element for next ingredient
    }

    // Update is called once per frame
    void Update()
    {
        //if ui is clicked create ingredient and hide ui
        //randomly pick from ingredient type list
        if (Input.GetKeyDown(KeyCode.Space))
        {
         string[] Ingredients = new string [] { "Ingredient1", "Ingredient2", "Ingredient3" };
            System.Random random = new System.Random();
            int useIngredients = random.Next(Ingredients.Length);
            string pickIngredients = Ingredients[useIngredients];
            
            Debug.Log(pickIngredients);

}
        //if ingredient health reaches 0 check ingredient cap
        //if cap has reached max then end game
        //if cap has not reached max load ui 
    }

    void createIngredient()
    {
        if (gameObject.name == "Ingredient1")
        {
            Instantiate(Ingredient1, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (gameObject.name == "Ingredient2")
        {
            Instantiate(Ingredient2, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (gameObject.name == "Ingredient3")
        {
            Instantiate(Ingredient3, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}

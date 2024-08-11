using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPrep_GM : MonoBehaviour
{
    //ingredient cap
    public int ingredientCap;
    IngredientScript health;
    Button2 OnButtonPress;

    //public string[] Ingredients = { "Ingredient1", "Ingredient2", "Ingredient3" };
    public GameObject[] Ingredients;



    // Start is called before the first frame update
    void Start()
    {
        ingredientCap = 0;
        health = GameObject.FindGameObjectWithTag("Ingredient").GetComponent<IngredientScript>();

        //create ui element for next ingredient
    }

    // Update is called once per frame
    void Update()
    {
        //if ui is clicked create ingredient and hide ui
        //randomly pick from ingredient type list
        if (Input.GetKeyDown(KeyCode.E))
        {

            int randomNumber = Random.Range(0, Ingredients.Length);

            GameObject randomObject = Ingredients[randomNumber];

            Instantiate(randomObject, new Vector3(0, 0, 0), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health.ingredientCurrentHealth -= 1;
            if (health.ingredientCurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }




    }
    //if ingredient health reaches 0 check ingredient cap
    //if cap has reached max then end game
    //if cap has not reached max load ui 


   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private Scene TheTavern;
    [SerializeField] private Scene DrinkPouring;
    [SerializeField] private Scene FoodPrep;
    [SerializeField] private Scene FoodServe;
    [SerializeField] private Scene DrinkChallenge;
    [SerializeField] private Scene CleaningGame;

    Scene currentScene;
    Scene previousScene;

    private void Awake()
    {
        //Check if the starting scene is the Tavern, if it isn't load that scene
        if (currentScene != TheTavern)
        {
            SceneManager.LoadScene("TheTavern");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartTheTavern()
    {
        Debug.Log("'The Tavern' is LOADING...");
    }

    private void StartDrinkPouring()
    {
        Debug.Log("'Drink Pouring' is LOADING...");
    }

    private void StartFoodPrep()
    {
        Debug.Log("'Food Prep' is LOADING...");
    }

    private void StartFoodServe()
    {
        Debug.Log("'Food Serve' is LOADING...");
    }

    private void StartDrinkChallenge()
    {
        Debug.Log("'Drink Challenge' is LOADING...");
    }

    private void StartCleaningGame()
    {
        Debug.Log("'Cleaning Game' is LOADING...");
    }
}

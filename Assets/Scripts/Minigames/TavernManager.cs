using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TavernManager : MonoBehaviour
{
    public ScoreTrack ScoreTrack;
    public TextMeshProUGUI ScoreValues;

    public GameObject DrinkPouring;
    public GameObject FoodService;
    public GameObject FoodPreperation;
    public GameObject DrinkDrinking;
    public GameObject CleaningOne;
    public GameObject CleaningTwo;

    // Start is called before the first frame update
    void Start()
    {
        ScoreTrack = FindObjectOfType<ScoreTrack>();
        UpdateScoreDisplay();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreDisplay()
    {
        ScoreValues.text = "GOLD: " + ScoreTrack.goldAmount;
    }

}

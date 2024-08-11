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

    public GameObject PatronPrefab;
    public GameObject Table1Mess;
    public GameObject Table2Mess;

    public bool dirtyTable1;
    public bool dirtyTable2;

    // Start is called before the first frame update
    void Start()
    {
        ScoreTrack = FindObjectOfType<ScoreTrack>();
        UpdateScoreDisplay();





    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DrinkPouring.SetActive(true);
            FoodService.SetActive(true);
            FoodPreperation.SetActive(true);
            DrinkDrinking.SetActive(true);
            CleaningOne.SetActive(true);
            CleaningTwo.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GeneratePatron();
        }
    }

    public void UpdateScoreDisplay()
    {
        ScoreValues.text = "GOLD: " + ScoreTrack.goldAmount;
    }


    public void GeneratePatron()
    {
        Instantiate(PatronPrefab);
    }

    public void MessCheck()
    {
        if (dirtyTable1 == true)
        {
            CleaningOne.SetActive (true);
        }

        if (dirtyTable2 == true)
        {
            CleaningTwo.SetActive (true);
        }
    }



}

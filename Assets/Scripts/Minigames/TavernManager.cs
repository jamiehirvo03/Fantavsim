using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TavernManager : MonoBehaviour
{
    public ScoreTrack ScoreTrack;
    public TextMeshProUGUI ScoreValues;
    public TextMeshProUGUI PatronOrder;

    public GameObject DrinkPouring;
    public GameObject FoodService;
    public GameObject FoodPreperation;
    public GameObject DrinkDrinking;
    public GameObject CleaningOne;
    public GameObject CleaningTwo;

    public GameObject PatronPrefab;
    public GameObject Table1Mess;
    public GameObject Table2Mess;



    // Start is called before the first frame update
    void Start()
    {
        ScoreTrack = FindObjectOfType<ScoreTrack>();
        UpdateScoreDisplay();

        PatronOrder.text = "";

        if (ScoreTrack.dirtyTable1 == true)
        {
            MessTable1();
        }

        if (ScoreTrack.dirtyTable2 == true)
        {
            MessTable2();
        }





    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {


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
        ScoreValues.text = "GOLD: " + ScoreTrack.goldAmount + "<br>MOOD:" + ScoreTrack.moodLevel + "<br>MESS:" + ScoreTrack.messLevel;
    }


    public void GeneratePatron()
    {
        float patronTimer = (Time.deltaTime * 5) * ScoreTrack.messLevel;
        Instantiate(PatronPrefab);
        int orderRoll = Random.Range(0, 100);

        if (orderRoll < 34)
        {
            // Order drink only
            PatronOrder.text = "Get me a drink.";
            DrinkPouring.SetActive(true);
        }

        else if ((orderRoll > 33) && (orderRoll < 67)) 
        {
            // Order food only
            PatronOrder.text = "I'll have some food.";
            FoodService.SetActive(true);
        }

        else if (orderRoll > 66)
        {
            // Order food and drink.
            PatronOrder.text = "Grog and grub!";
            DrinkPouring.SetActive(true);
            FoodService.SetActive(true);
        }
    }

    public void MessCheck()
    {
        if (ScoreTrack.dirtyTable1 == false || ScoreTrack.dirtyTable2 == false)
        {
            int messRoll = Random.Range(0, 100);
            if (messRoll > 74) 
            {
                if (ScoreTrack.dirtyTable1 == false)
                {
                    MessTable1();
                }

                else if (ScoreTrack.dirtyTable2 == false)
                {
                    MessTable2();
                }
            }
        }
    }


    public void MessTable1()
    {
        Instantiate(Table1Mess);
        CleaningOne.SetActive(true);
    }

    public void MessTable2()
    {
        Instantiate(Table2Mess);
        CleaningTwo.SetActive(true);
    }    

}

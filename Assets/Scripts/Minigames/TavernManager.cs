using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.PackageManager;

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

    public float degradeCounter;

    string moodLevel;
    string messLevel;




    // Start is called before the first frame update
    void Start()
    {
        ScoreTrack = FindObjectOfType<ScoreTrack>();
        UpdateScoreDisplay();
        TaskAvailabilityCheck();
        PatronOrder.text = "";

        if (ScoreTrack.clientsSpawned == 0)
        {
            GeneratePatron();
        }

        if ((ScoreTrack.drinkOpen == true) || (ScoreTrack.foodServiceOpen == true))
        {
            Instantiate(PatronPrefab);
        }

        if ((ScoreTrack.drinkOpen == false) && (ScoreTrack.foodServiceOpen == false) && (ScoreTrack.clientActive == true))
        {
            ScoreTrack.clientActive = false;
            Debug.Log("Order completed");
            MessCheck();
            StartCoroutine(SpawnTimer());
        }



        // degradeCounter += (Time.deltaTime * 1) * ScoreTrack.messValue;
    }

    public void UpdateScoreDisplay()
    {
        if (ScoreTrack.messValue == 1)
        {
            messLevel = "Clean";
        }

        else if (ScoreTrack.messValue == 2) 
        {
            messLevel = "Low";
        }

        else if (ScoreTrack.messValue == 3)
        {
            messLevel = "High";
        }

        if (ScoreTrack.moodValue == 1)
        {
            moodLevel = "Low";
        }

        else if (ScoreTrack.moodValue == 2)
        {
            moodLevel = "Medium";
        }

        else if (ScoreTrack.moodValue == 3)
        {
            moodLevel = "High";
        }

        ScoreValues.text = "GOLD: " + ScoreTrack.goldAmount + "<br>MOOD: " + moodLevel + "<br>MESS: " + messLevel;
    }


    public void GeneratePatron()
    {
        // float patronTimer = 5 * ScoreTrack.messValue;
        Instantiate(PatronPrefab);
        ScoreTrack.clientActive = true;
        ScoreTrack.clientsSpawned += 1;
        int orderRoll = Random.Range(0, 100);

        if (orderRoll < 34)
        {
            // Order drink only
            PatronOrder.text = "Get me a drink.";
            ScoreTrack.drinkOpen = true;
            DrinkPouring.SetActive(true);
        }

        else if ((orderRoll > 33) && (orderRoll < 67)) 
        {
            // Order food only
            PatronOrder.text = "I'll have some food.";
            ScoreTrack.foodServiceOpen = true;
            FoodService.SetActive(true);
        }

        else if (orderRoll > 66)
        {
            // Order food and drink.
            PatronOrder.text = "Grog and grub!";
            ScoreTrack.foodServiceOpen = true;
            ScoreTrack.drinkOpen = true;
            DrinkPouring.SetActive(true);
            FoodService.SetActive(true);
        }
    }

    public void MessCheck()
    {
        if (ScoreTrack.dirtyTable1 == false || ScoreTrack.dirtyTable2 == false)
        {
            Debug.Log("Attemping mess roll");
            int messRoll = Random.Range(0, 100);
            if (messRoll > 1) 
            {
                Debug.Log("Mess roll passed.");
                if (ScoreTrack.dirtyTable1 == false)
                {
                    Debug.Log("Table 1 mess");
                    ScoreTrack.dirtyTable1 = true;
                    MessTable1();
                }

                else if (ScoreTrack.dirtyTable2 == false)
                {
                    Debug.Log("Table 2 mess");
                    ScoreTrack.dirtyTable2 = true;
                    MessTable2();
                }
            }
        }
    }


    void TaskAvailabilityCheck()
    {
        if (ScoreTrack.drinkOpen == true)
        {
            DrinkPouring.SetActive(true);
        }
        else 
        { 
            DrinkDrinking.SetActive(false);
        }

        if (ScoreTrack.foodServiceOpen == true)
        {
            FoodService.SetActive(true);
        }
        else 
        { 
            FoodService.SetActive(false);
        }

        if (ScoreTrack.moodValue < 3)
        {
            DrinkDrinking.SetActive(true);
        }
        else 
        {
            DrinkDrinking.SetActive(false);
        }
        if (ScoreTrack.dirtyTable1 == true)
        {
            MessTable1();
        }
        else
        {
            CleaningOne.SetActive(false);
        }
        if (ScoreTrack.dirtyTable2 == true)
        {
            MessTable2();
        }
        else
        {
            CleaningTwo.SetActive(false);
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
    
    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(15f / ScoreTrack.moodValue);
        GeneratePatron();
    }

}

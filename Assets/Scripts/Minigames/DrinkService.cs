using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class DrinkService : MonoBehaviour
{
    //Content Warning: Spaghetti code

    public float kegVolume;
    public int nozzleSetting;
    private float pourRate;
    public int vesselAngle;
    public int vesselSize;
    public int currentCapacity;
    public int totalCapacity;
    public float currentVolume;
    public bool vesselInHand;
    public bool drinkReady;
    private float inputTimer;
    private float grogInTransit;
    public float wastedGrog;
    public float liquidVolume;
    public float frothVolume;
    public float liquidPercent;
    public float frothPercent;
    public bool nucleation;
    public int grade;
    public GameObject MugPrefab;
    public DrinkServiceAngle AngleScript;

    public TextMeshProUGUI notifyUI;
    public TextMeshProUGUI nozzleDisplay;

    //Handles time limit

    //Generates customer orders

    //Handles drink selections
    
    // Start is called before the first frame update
    void Start()
    {
        nozzleSetting = 0;
        pourRate = 0;
        vesselInHand = false;
        kegVolume = 50000;
        currentCapacity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Controls the angle (and subsequently its current capacity) of a mug when in hand.
        if (vesselInHand == true)
        {

            //Increases angle and current capacity
            if ((Input.GetKey(KeyCode.D)) && (vesselAngle < 30))
                if (inputTimer <= 0)
            {
                vesselAngle += 1;
                inputTimer = 1;
                UpdateCurrentCapacity();

            }

            //Decreases angle and current capacity.
            if ((Input.GetKey(KeyCode.A)) && (vesselAngle > 0))
            {
                if (inputTimer <= 0)
                {
                    vesselAngle -= 1;
                    inputTimer = 1;
                    UpdateCurrentCapacity();
                }
            }
        }
        //Angle adjust support
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            inputTimer = 0;
        }
        // Speed of angle adjust
        if (inputTimer >= 0)
        {
            inputTimer -= Time.deltaTime * 15;
        }
        // Increases flow of booze
        if ((Input.GetKeyDown(KeyCode.S)) && (nozzleSetting < 3))
        {
            nozzleSetting +=1;
            ChangePour();
        }
        //Decreases flow of booze
        if ((Input.GetKeyDown(KeyCode.W)) && (nozzleSetting > 0))
        {
            nozzleSetting -= 1;
            ChangePour();
        }
        //Cuts flow of booze
        if ((Input.GetKeyDown(KeyCode.Space)) && (nozzleSetting > 0))
        {
            nozzleSetting = 0;
            ChangePour();
        }



        //Trigger flow of booze
        if (nozzleSetting > 0)
        {
            StartPour();
        }



        // This should either A) if you have no mug equipped, equip and a new one to hand, or B) if you have a mug equipped, serve it.
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (vesselInHand == true)
            {
                ServeDrink();
            }

            else if (vesselInHand == false)
            {
                NewVessel();
            }
        }

        if (liquidVolume > (totalCapacity - currentCapacity))
        {
            nucleation = true;
        }

        else
        {
            nucleation = false;
        }

    }

    public void NewVessel()
    {
        // Player picks up a new glass, generate new size, ensure its empty
        vesselInHand = true;
        vesselSize = 2;
        currentVolume = 0;
        totalCapacity = 150 * vesselSize;
        vesselAngle = 15;
        grade = 0;
        Instantiate(MugPrefab);
        AngleScript = FindObjectOfType<DrinkServiceAngle>();
        UpdateCurrentCapacity();
    }

    public void ServeDrink()
    {
        // Player serves drink to client. Grades performance and resets values.
        vesselAngle = 30;
        UpdateCurrentCapacity();
        GradeTask();
        liquidVolume = 0;
        frothVolume = 0;
        wastedGrog = 0;
        currentCapacity = 0;
        vesselInHand = false;
        AngleScript.ServeDrink();
    }

    public void ChangePour()
    {
        // Math for flow settings
        pourRate = nozzleSetting * 15;
        nozzleDisplay.text = "" + nozzleSetting;
    }


    public void StartPour()
    {
        if (kegVolume > 0)
        {
            grogInTransit += (Time.deltaTime * pourRate);
            kegVolume -= grogInTransit;
            if (vesselInHand == true)
            {
                if (currentVolume <= currentCapacity)
                {
                    if (nucleation == true)
                    {
                        liquidVolume += (grogInTransit * 0.9f);
                        frothVolume += (grogInTransit * 0.1f);
                        // currentVolume += grogInTransit;
                       /* if (currentVolume >= currentCapacity)
                        {
                            frothVolume -= grogInTransit;
                            liquidVolume += grogInTransit;
                        } */
                        grogInTransit = 0;
                    }

                    else if (nucleation == false)
                    {
                        liquidVolume += grogInTransit;
                        grogInTransit = 0;
                    }
                    currentVolume = liquidVolume + frothVolume;
                    liquidPercent = (liquidVolume / currentVolume) * 100;
                    frothPercent = (frothVolume / currentVolume) * 100;
                }
                else if (currentVolume >= currentCapacity)
                {
                    wastedGrog += grogInTransit;
                    grogInTransit = 0;
                }
            }
            else
            {
                wastedGrog += grogInTransit;
                grogInTransit = 0;
            }
        }

    }

    public void UpdateCurrentCapacity()
    {
        // Updates current capacity with angle adjustments
        currentCapacity = (totalCapacity / 30) * vesselAngle;
        AngleScript.UpdateAngle();
    }

    public void GradeTask()
    {
        // Review task, assign a grade and clear
        if (wastedGrog < 10)
        {
            grade += 0;
        }

        else if ((wastedGrog > 10) && (wastedGrog < 20))
        {
            grade += 1;
        } 

        else if (wastedGrog > 20)
        {
            grade += 2;
        }

        if ((frothPercent >= 4) && (frothPercent <= 6))
        {
            grade += 0;
        }

        else if (frothPercent < 4)
        {
            //Too little froth
            grade += 1;
        }

        else if ((frothPercent > 6) && (frothPercent < 10))
        {
            //Too much froth
            grade += 1;
        }

        else if (frothPercent >= 10)
        {
            //WAY TOO MUCH froth
            grade += 2;
        }

        if ((liquidPercent >= 94) && (liquidPercent <= 96))
        {
            grade += 0;
        }

        else if (liquidPercent > 96)
        {
            grade += 1;
        }

        else if ((liquidPercent < 94) && (liquidPercent >= 85))
        {
            grade += 2;
        }

        else if ((liquidPercent < 85) && (liquidPercent >= 60))
        {
            grade += 3;
        }

        else if (liquidPercent < 60)
        {
            grade += 4;
        }

        if (grade == 0)
        {
            notifyUI.text = "PERFECT!";
        }

        if (grade == 1)
        {
            notifyUI.text = "Great!";
        }

        if (grade == 2)
        {
            notifyUI.text = "Good!";
        }

        if (grade == 3)
        {
            notifyUI.text = "Alright...";
        }

        if (grade >= 4)
        {
            notifyUI.text = "Bad.";
        }

        grade = 0;
        StartCoroutine(ClearTimer());
    }

    IEnumerator ClearTimer()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Clearing log.");
        notifyUI.text = "";
    }
}

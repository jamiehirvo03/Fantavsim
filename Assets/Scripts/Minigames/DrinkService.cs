using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DrinkService : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        // Controls the angle (and subsequently its current capacity) of a mug when in hand.
        if (vesselInHand == true)
        {
          /*  if((Input.GetKeyDown(KeyCode.A)) && (vesselAngle < 30)) 
            {
                vesselAngle += 1;
                UpdateCurrentCapacity();
            } */

            if ((Input.GetKey(KeyCode.A)) && (vesselAngle < 30))
                if (inputTimer <= 0)
            {
                vesselAngle += 1;
                inputTimer = 1;
                UpdateCurrentCapacity();

            }

            /*  if ((Input.GetKeyDown(KeyCode.D)) && (vesselAngle > 0))
          {
              vesselAngle -= 1;
              UpdateCurrentCapacity();
          } */

            if ((Input.GetKey(KeyCode.D)) && (vesselAngle > 0))
            {
                if (inputTimer <= 0)
                {
                    vesselAngle -= 1;
                    inputTimer = 1;
                    UpdateCurrentCapacity();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            inputTimer = 0;
        }

        if (inputTimer >= 0)
        {
            inputTimer -= Time.deltaTime * 4;
        }

        if ((Input.GetKeyDown(KeyCode.S)) && (nozzleSetting < 3))
        {
            nozzleSetting +=1;
            ChangePour();
        }

        if ((Input.GetKeyDown(KeyCode.W)) && (nozzleSetting > 0))
        {
            nozzleSetting -= 1;
            ChangePour();
        }

        if ((Input.GetKeyDown(KeyCode.Space)) && (nozzleSetting > 0))
        {
            nozzleSetting = 0;
            ChangePour();
        }




        if (nozzleSetting > 0)
        {
            StartPour();
        }

       /* if (currentVolume > currentCapacity)
        {
            currentVolume = currentCapacity;
        }*/


        // This should either A) if you have no mug equipped, equip and a new one to hand, or B) if you have a mug equipped, put it aside.
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

        if (liquidVolume > (currentCapacity / 2))
        {
            nucleation = true;
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
        UpdateCurrentCapacity();
        grade = 0;
    }

    public void ServeDrink()
    {
        vesselAngle = 30;
        UpdateCurrentCapacity();
        GradeTask();
        currentVolume = 0;
        vesselInHand = false;
    }

    public void ChangePour()
    {
        pourRate = nozzleSetting * 15;
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
        currentCapacity = (totalCapacity / 30) * vesselAngle;
    }

    public void GradeTask()
    {
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


    }
}

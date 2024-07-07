using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DrinkService : MonoBehaviour
{
    public int nozzleSetting;
    public float pourRate;
    public float pourSpeed;
    public int vesselAngle;
    public int vesselSize;
    public int currentCapacity;
    public int totalCapacity;
    public float currentVolume;
    public bool vesselInHand;
    public bool drinkReady;

    //Handles time limit

    //Generates customer orders

    //Handles drink selections

    // Start is called before the first frame update
    void Start()
    {
        nozzleSetting = 0;
        pourRate = 0;
        pourSpeed = 0; 
        vesselInHand = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (vesselInHand == true)
        {
            if((Input.GetKeyDown(KeyCode.A)) && (vesselAngle < 30)) 
            {
                vesselAngle = vesselAngle + 1;
            }

            if ((Input.GetKeyDown(KeyCode.D)) && (vesselAngle > 0))
            {
                vesselAngle = vesselAngle - 1;
            }
        }

        if ((Input.GetKeyDown(KeyCode.S)) && (nozzleSetting < 3))
        {
            nozzleSetting = nozzleSetting + 1;
            ChangePour();
        }

        if ((Input.GetKeyDown(KeyCode.W)) && (nozzleSetting > 0))
        {
            nozzleSetting = nozzleSetting - 1;
            ChangePour();
        }

        if ((Input.GetKeyDown(KeyCode.Space)) && (nozzleSetting > 0))
        {
            nozzleSetting = 0;
        }
        if ((Input.GetKeyDown(KeyCode.E)) && (vesselInHand = false))
        {

            NewVessel();
        }
        else if ((Input.GetKeyDown(KeyCode.E)) && (vesselInHand = true))
        {
            currentVolume = 0;
            vesselInHand = false;
        }
        if ((nozzleSetting > 0) && (vesselInHand = true))
        {
            currentVolume += (Time.deltaTime * pourRate);
            while (pourSpeed > pourRate)
            {
                currentVolume += 1;
                pourSpeed = pourRate;
            }
        }


    }

    public void NewVessel()
    {
        vesselInHand = true;
        vesselSize = 2;
        currentVolume = 0;
        totalCapacity = 150 * vesselSize;
        vesselAngle = 30;
    }

    public void ChangePour()
    {
        pourRate = nozzleSetting * 15;
    }

    public void CutPour()
    {
        pourRate = 0;
    }
}

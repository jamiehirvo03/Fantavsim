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
                vesselAngle += 1;
                UpdateCurrentVolume();
            }

            if ((Input.GetKeyDown(KeyCode.D)) && (vesselAngle > 0))
            {
                vesselAngle -= 1;
                UpdateCurrentVolume();
            }
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
        }




        if ((nozzleSetting > 0) && (vesselInHand == true))
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
                currentVolume = 0;
                vesselInHand = false;
            }

            else if (vesselInHand == false)
            {
                NewVessel();
            }
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
    }

    public void ChangePour()
    {
        pourRate = nozzleSetting * 15;
    }

    public void CutPour()
    {
        pourRate = 0;
    }

    public void StartPour()
    {
        if (currentVolume >= currentCapacity) return;
            currentVolume += (Time.deltaTime * pourRate);
    }

    public void UpdateCurrentVolume()
    {
        currentCapacity = (totalCapacity / 30) * vesselAngle;
    }
}

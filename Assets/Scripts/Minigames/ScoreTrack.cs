using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrack : MonoBehaviour
{

    public int goldAmount;


    public int moodValue;
    public int messValue;
    public int clientsSpawned;


    public float lastPositionX;
    public float lastPositionY;

    public bool clientActive;
    public bool drinkOpen;
    public bool foodServiceOpen;
    public bool drinkingOpen;
    public bool dirtyTable1;
    public bool dirtyTable2;

    public bool atTable1;
    public bool atTable2;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        goldAmount = 0;
        moodValue = 2;
        messValue = 1;

        clientActive = false;

        drinkOpen = false;
        foodServiceOpen = false;
        // drinkingOpen = false;
        dirtyTable1 = false;
        dirtyTable2 = false;

        atTable1 = false;
        atTable2 = false;

        lastPositionX = 1.62f;
        lastPositionY = 3.5f;

        clientsSpawned = 0;

    }



}

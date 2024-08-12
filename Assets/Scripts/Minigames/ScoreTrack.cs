using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrack : MonoBehaviour
{

    public int goldAmount;
    public int moodLevel;
    public int messLevel;

    public bool dirtyTable1;
    public bool dirtyTable2;
    public Vector2 playerPosition;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        goldAmount = 0;
        moodLevel = 0;
        messLevel = 1;

        dirtyTable1 = false;
        dirtyTable2 = false;

    }



}

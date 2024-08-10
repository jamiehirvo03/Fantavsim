using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrack : MonoBehaviour
{

    public int goldAmount;
    public int messLevel;
    public int revelryLevel;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        goldAmount = 0;
        messLevel = 0;
        revelryLevel = 0;
    }


    public void ScoreTest()
    {
        Debug.Log("OH MY GAWD!");
    }
}

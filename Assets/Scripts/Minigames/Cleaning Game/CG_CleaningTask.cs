using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_CleaningTask : MonoBehaviour
{
    [SerializeField] private List<string> messList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onStartCleaningTask += OnStartCleaningTask;
        CG_Events.current.onCloseCleaningTask += OnCloseCleaningTask;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnStartCleaningTask()
    {
        //Initiate cleaning task
        //Generate mess items randomly
    }
    private void OnCloseCleaningTask()
    {
        //End cleaning task
    }
}

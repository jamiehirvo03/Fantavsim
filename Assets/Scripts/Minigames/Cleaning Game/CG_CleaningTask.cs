using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CG_CleaningTask : MonoBehaviour
{
    [SerializeField] private bool isTaskActive = false;

    public float range = 7;
    public int minMessCount = 5;
    public int maxMessCount = 15;
    [SerializeField] private int taskMessCount;

    public GameObject mess;
    [SerializeField] public List<GameObject> messList = new List<GameObject>();

    private float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onStartGame += OnStartGame;
        CG_Events.current.onMessPlacementCorrect += OnMessPlacementCorrect;
    }
    // Update is called once per frame
    void Update()
    {
        if (isTaskActive)
        {
            if (messList.Count < minMessCount)
            {
                x = Random.Range(-range, range);
                y = Random.Range(-range, range);
                z = -0.75f;
                GameObject newMess = (GameObject)Instantiate(mess, new Vector3(x, y, z), Quaternion.identity);

                messList.Add(newMess);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                CG_Events.current.CreateMessItem();
            }
        }
    }
    private void OnStartGame()
    {
        //Initiate cleaning task
        isTaskActive = true;

        //Randomly decide how many mess items will be generated
        taskMessCount = Random.Range(minMessCount,maxMessCount);

        //Generate mess items randomly
        for (int i = 0; i < taskMessCount; i++)
        {
            x = Random.Range(-range, range);
            y = Random.Range(-range, range);
            z = -0.75f;
            GameObject newMess = (GameObject)Instantiate(mess, new Vector3 (x, y, z), Quaternion.identity);

            messList.Add(newMess);
        }
    }

    private void OnMessPlacementCorrect()
    {
        //Remove mess item from list
        for (int i = 0; i < messList.Count; i++)
        {
            if (messList[i].GetComponent<CG_Mess>().placedCorrectly == true)
            {
                messList.RemoveAt(i);
            }
        }
    }
}

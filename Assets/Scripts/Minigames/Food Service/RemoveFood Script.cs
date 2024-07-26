using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFoodScript : MonoBehaviour
{
    GameManagerFS emptyPlateNow;
    public GameObject clone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameManagerFS.emptyPlateNow == true)
        {
            Destroy(gameObject);
        }
        
        
        
        //if ((GameManagerFS.emptyPlateNow>transform.position.x-.4f) && (GameManagerFS.emptyPlateNow <transform.position.x + .4f))     
        //{
            //Destroy(gameObject);
        //}
    }
}

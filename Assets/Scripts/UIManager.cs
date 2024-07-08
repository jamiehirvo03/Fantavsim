using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Handles what UI appears & when

    //Handles timer & score display

    //Handles pause menu


    // Start is called before the first frame update
    void Start()
    {
        //EventManager.current.onShowTimer += OnShowTimer;
        //EventManager.current.onHideTimer += OnHideTimer;

    }

    private void OnShowTimer()
    {
        //Debug log to test that event is working
        Debug.Log("Timer UI is showing");
    }

    private void OnHideTimer()
    {
        //Debug log to test that event is working
        Debug.Log("Timer UI is hidden");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

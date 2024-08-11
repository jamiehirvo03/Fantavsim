using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_TankardBin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Over Tankard Bin");

        CG_Events.current.OverTankardBin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Over No Bin");

        CG_Events.current.OverNoBin();
    }
}

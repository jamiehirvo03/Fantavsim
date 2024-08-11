using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_DustBin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Over Dust Bin");

        CG_Events.current.OverDustBin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Over No Bin");

        CG_Events.current.OverNoBin();
    }
}

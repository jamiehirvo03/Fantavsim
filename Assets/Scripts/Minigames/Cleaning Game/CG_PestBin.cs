using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_PestBin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Over Pest Bin");

        CG_Events.current.OverPestBin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Over No Bin");

        CG_Events.current.OverNoBin();
    }
}

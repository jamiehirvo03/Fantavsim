using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_RodentBin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Over Rodent Bin");

        CG_Events.current.OverRodentBin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Over No Bin");

        CG_Events.current.OverNoBin();
    }
}

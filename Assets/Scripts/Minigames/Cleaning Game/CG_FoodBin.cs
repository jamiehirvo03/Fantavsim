using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_FoodBin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Over Scraps Bin");

        CG_Events.current.OverScrapsBin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Over No Bin");

        CG_Events.current.OverNoBin();
    }
}

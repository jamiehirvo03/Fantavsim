using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_Table : MonoBehaviour
{
    private void Start()
    {
        CG_Events.current.OverTable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CG_Mess>().dragging == true)
        {
            Debug.Log("Over Table");

            CG_Events.current.OverTable();
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Not over Table");

        CG_Events.current.OverNoBin();
    }
}

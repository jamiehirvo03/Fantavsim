using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG_Player : MonoBehaviour
{
    private bool isCurrentDrinkGolden;

    // Start is called before the first frame update
    void Start()
    {
        DG_Events.current.onCurrentDrinkRegular += OnCurrentDrinkRegular;
        DG_Events.current.onCurrentDrinkGolden += OnCurrentDrinkGolden;
        DG_Events.current.onIdle += OnIdle;
        DG_Events.current.onDrinking += OnDrinking;
        DG_Events.current.onSpilling1 += OnSpilling1;
        DG_Events.current.onChugging += OnChugging;
        DG_Events.current.onSpilling2 += OnSpilling2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCurrentDrinkRegular()
    {
        isCurrentDrinkGolden = false;
    }

    private void OnCurrentDrinkGolden()
    {
        isCurrentDrinkGolden = true;
    }

    private void OnIdle()
    {
        if (!isCurrentDrinkGolden)
        {
            //REGULAR IDLE SPRITE
        }
        else
        {
            //GOLDEN IDLE SPRITE
        }
    }

    private void OnDrinking()
    {
        if (!isCurrentDrinkGolden)
        {
            //REGULAR DRINKING SPRITE
        }
        else
        {
            //GOLDEN DRINKING SPRITE
        }
    }

    private void OnSpilling1()
    {
        if (!isCurrentDrinkGolden)
        {
            //REGULAR SPILLING 1 SPRITE
        }
        else
        {
            //GOLDEN SPILLING 1 SPRITE
        }
    }

    private void OnChugging()
    {
        if (!isCurrentDrinkGolden)
        {
            //REGULAR CHUGGING SPRITE
        }
        else
        {
            //GOLDEN CHUGGING SPRITE
        }
    }

    private void OnSpilling2()
    {
        if (!isCurrentDrinkGolden)
        {
            //REGULAR SPILLING 2 SPRITE
        }
        else
        {
            //GOLDEN SPILLING 2 SPRITE
        }
    }

}

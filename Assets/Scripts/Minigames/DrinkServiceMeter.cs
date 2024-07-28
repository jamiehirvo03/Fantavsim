using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkServiceMeter : MonoBehaviour
{
    public DrinkService DrinkService;

    public float maxValue;
    public float[] values;
    public RectTransform[] valueBars;

    public float lengthOfBackground;
    private float lengthPerValue;


    // Start is called before the first frame update
    void Start()
    {
        // lengthPerValue = lengthOfBackground / maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        //Translate liquid, froth and current capacity values in UI meter
        float liquid = DrinkService.liquidVolume * 2;
        float froth = DrinkService.frothVolume * 2;
        int capacity = DrinkService.currentCapacity * 2;
        valueBars[0].sizeDelta = new Vector2(valueBars[0].sizeDelta.x, liquid);
        valueBars[1].sizeDelta = new Vector2(valueBars[1].sizeDelta.x, froth + valueBars[0].sizeDelta.y);
        valueBars[2].sizeDelta = new Vector2(valueBars[2].sizeDelta.x, capacity);
    }

}

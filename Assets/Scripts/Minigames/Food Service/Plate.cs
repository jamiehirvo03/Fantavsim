using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public int thisPlate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //on plate click, checks to see if ingredient values are correct to order
        if (GameManagerFS.orderValue[GameManagerFS.plateNum]==GameManagerFS.plateValue[GameManagerFS.plateNum])
        {
            Debug.Log("Order Correct"+" "+GameManagerFS.plateNum);
            GameManagerFS.plateValue[GameManagerFS.plateNum] = GameManagerFS.orderValue[Random.Range(0, 7)];
        }

        GameManagerFS.emptyPlateNow = transform.position.x;
        StartCoroutine(platereset());
    }



    IEnumerator platereset()
    {
        yield return new WaitForSeconds(.2f);
        GameManagerFS.emptyPlateNow = -1;
        
    }


}

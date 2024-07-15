using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public GameObject GM;
    private GameManagerFS gameManagerFS;
    
    public int thisPlate;
    // Start is called before the first frame update
    void Start()
    {
        //get by type
        GM = GameObject.FindGameObjectWithTag ("GM");
        gameManagerFS = GM.GetComponent<GameManagerFS> ();
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
            //GameManagerFS.plateValue[GameManagerFS.plateNum] = GameManagerFS.orderValue[Random.Range(0, 7)];

            GameManagerFS.plateNum = Random.Range(0, 7);
            gameManagerFS.AssignSprite();
            //Debug.Log(GameManagerFS.plateValue[GameManagerFS.plateNum]);
            Debug.Log(GameManagerFS.orderValue[GameManagerFS.plateNum]);
        }

        GameManagerFS.emptyPlateNow = true;
        StartCoroutine(platereset());
    }



    IEnumerator platereset()
    {
        yield return new WaitForSeconds(.2f);
        GameManagerFS.emptyPlateNow = false;
        
    }


}

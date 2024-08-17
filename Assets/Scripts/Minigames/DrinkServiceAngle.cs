using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrinkServiceAngle : MonoBehaviour
{
    public DrinkService DrinkService;
    // Start is called before the first frame update

    private void Awake()
    {
        DrinkService = FindObjectOfType<DrinkService>();
        UpdateAngle();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAngle()
    {
        // Translate angle adjusts to visible mug (instantiated by E)
        float x = 0;
        float y = 0;
        float z = (DrinkService.vesselAngle * -3) + 90;
        //  Vector3 systemAngle = new Vector3(0, 0, z);
        // Rotate(x, y, z);
        transform.eulerAngles = new Vector3(x, y, z);
    }

    public void ServeDrink()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TavernPlayerController : MonoBehaviour
{
    private bool isMoving;
    public float moveSpeed;

    private Vector2 input;

    public LayerMask solidObjetsLayer;
    private string taskVicinity;

    private void Start()
    {
        taskVicinity = "";
    }


    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (taskVicinity == "")
            {
                Debug.Log("No task in vicinity");
            }

            else if (taskVicinity == "Drinks")
            {
                Debug.Log("Launch Drinks minigame");
            }

            else if (taskVicinity == "Drinking")
            {
                Debug.Log("Launch Drinking miningame");
            }

            else if (taskVicinity == "Food Service")
            {
                Debug.Log("Launch Food Service minigame");
            }

            else if (taskVicinity == "Food Preperation")
            {
                Debug.Log("Launch Food Preperation miningame");
            }

            else if (taskVicinity == "Cleaning")
            {
                Debug.Log("Launch Cleaning miningame");
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;

    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjetsLayer) != null)
        {
            return false;
        }
        return true;
    }

    // Detects when the player has entered the collision zone to start a minigame, prompts the Player to press E to launch.
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.transform.name == "KegTriggerZone")
        {
            Debug.Log("Offer Drinking Game");
            taskVicinity = "Drinks";
        }

        if (target.transform.name == "FoodServiceZone")
        {
            Debug.Log("Offer Food Service Gasme");
            taskVicinity = "Food Service";
        }

        if (target.transform.name == "FoodPreperationZone")
        {
            Debug.Log("Offer Food Preperation Game");
            taskVicinity = "Food Preperation";
        }

        if (target.transform.name == "DrinkingZone")
        {
            Debug.Log("Offer Drinking Miningame");
            taskVicinity = "Drinking";
        }

        if ((target.transform.name == "CleaningZone1") || (target.transform.name == "CleaningZone2"))
        {
            Debug.Log("Offer Cleaning Miningame");
            taskVicinity = "Cleaning";
        }
    }


    private void OnTriggerExit2D(Collider2D target)
    {
        taskVicinity = "";
    }




}

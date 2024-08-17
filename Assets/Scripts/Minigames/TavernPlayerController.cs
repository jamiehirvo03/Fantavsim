using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class TavernPlayerController : MonoBehaviour
{
    public ScoreTrack ScoreTrack;

    private bool isMoving;
    public float moveSpeed;
    public TextMeshProUGUI taskNotifiy;

    private Vector2 input;

    public LayerMask solidObjetsLayer;
    private string taskVicinity;

    private void Start()
    {
        ScoreTrack = FindObjectOfType<ScoreTrack>();
        taskVicinity = "";
        taskNotifiy.text = "";
        // transform.position = ScoreTrack.lastPositionX, 
    }


    private void Update()
    {
        //Add another bool here - Tavern minigame active Y/N
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
                // Below in dev. Update (E) notification to be above player character.
                // UpdateNotifyLocation();




                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // ScoreTrack.playerPosition = x, y
            if (taskVicinity == "")
            {
                Debug.Log("No task in vicinity");
            }
                                                                                    
            else if (taskVicinity == "Drinks")
            {
                Debug.Log("Launch Drinks minigame");
                SceneManager.LoadScene(2);
            }

            else if (taskVicinity == "Food Service")
            {
                Debug.Log("Launch Food Service minigame");
                SceneManager.LoadScene(3);
            }

            else if (taskVicinity == "Food Preperation")
            {
                Debug.Log("Launch Food Preperation miningame");
                SceneManager.LoadScene(4);
            }

            else if (taskVicinity == "Drinking")
            {
                Debug.Log("Launch Drinking miningame");
                SceneManager.LoadScene(5);
            }

            else if (taskVicinity == "Cleaning")
            {
                Debug.Log("Launch Cleaning miningame");
                SceneManager.LoadScene(6);  
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
            taskNotifiy.text = "(E) Pour a drink.";
            taskVicinity = "Drinks";
        }

        if (target.transform.name == "FoodServiceZone")
        {
            Debug.Log("Offer Food Service Game");
            taskNotifiy.text = "(E) Serve food.";
            taskVicinity = "Food Service";
        }

        if (target.transform.name == "FoodPreperationZone")
        {
            Debug.Log("Offer Food Preperation Game");
            taskNotifiy.text = "(E) Prepare Food.";
            taskVicinity = "Food Preperation";
        }

        if (target.transform.name == "DrinkingZone")
        {
            Debug.Log("Offer Drinking Miningame");
            taskNotifiy.text = "(E) Have a drink/s.";
            taskVicinity = "Drinking";
        }

        if ((target.transform.name == "CleaningZone1") || (target.transform.name == "CleaningZone2"))
        {
            Debug.Log("Offer Cleaning Miningame");
            ScoreTrack.atTable1 = true;
            taskNotifiy.text = "(E) Clean table.";
            taskVicinity = "Cleaning";
        }

        if (target.transform.name == "CleaningZone2")
        {
            Debug.Log("Offer Cleaning Miningame");
            ScoreTrack.atTable2 = true;
            taskNotifiy.text = "(E) Clean table.";
            taskVicinity = "Cleaning";
        }
    }


    private void OnTriggerExit2D(Collider2D target)
    {
        taskVicinity = "";
        taskNotifiy.text = "";
    }

    void UpdateNotifyLocation()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        taskNotifiy.transform.position = new Vector2(x, y + 2);
    }




}

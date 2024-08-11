using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG_Player : MonoBehaviour
{
    [SerializeField] private bool isCurrentDrinkGolden = false;

    public GameObject playerSprite;
    private SpriteRenderer spriteRenderer;

    public Sprite regularIdle;
    public Sprite regularDrinking;
    public Sprite regularSpilling1;
    public Sprite regularChugging;
    public Sprite regularSpilling2;

    public Sprite goldenIdle;
    public Sprite goldenDrinking;
    public Sprite goldenSpilling1;
    public Sprite goldenChugging;
    public Sprite goldenSpilling2;

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

        SpriteRenderer spriteRenderer = playerSprite.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = regularIdle;
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
            playerSprite.GetComponent<SpriteRenderer>().sprite = regularIdle;
        }
        else
        {
            //GOLDEN IDLE SPRITE
            playerSprite.GetComponent<SpriteRenderer>().sprite = goldenIdle;
        }
    }

    private void OnDrinking()
    {
        if (!isCurrentDrinkGolden)
        {
            //REGULAR DRINKING SPRITE
            playerSprite.GetComponent<SpriteRenderer>().sprite = regularDrinking;
        }
        else
        {
            //GOLDEN DRINKING SPRITE
            playerSprite.GetComponent<SpriteRenderer>().sprite = goldenDrinking;
        }
    }

    private void OnSpilling1()
    {
        if (!isCurrentDrinkGolden)
        {
            //REGULAR SPILLING 1 SPRITE
            playerSprite.GetComponent<SpriteRenderer>().sprite = regularSpilling1;
        }
        else
        {
            //GOLDEN SPILLING 1 SPRITE
            playerSprite.GetComponent<SpriteRenderer>().sprite = goldenSpilling1;
        }
    }

    private void OnChugging()
    {
        if (!isCurrentDrinkGolden)
        {
            //REGULAR CHUGGING SPRITE
            playerSprite.GetComponent<SpriteRenderer>().sprite = regularChugging;
        }
        else
        {
            //GOLDEN CHUGGING SPRITE
            playerSprite.GetComponent<SpriteRenderer>().sprite = goldenChugging;
        }
    }

    private void OnSpilling2()
    {
        if (!isCurrentDrinkGolden)
        {
            //REGULAR SPILLING 2 SPRITE
            playerSprite.GetComponent<SpriteRenderer>().sprite = regularSpilling2;
        }
        else
        {
            //GOLDEN SPILLING 2 SPRITE
            playerSprite.GetComponent<SpriteRenderer>().sprite = goldenSpilling2;
        }
    }

}

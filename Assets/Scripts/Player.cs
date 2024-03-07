// Copyright (c) 2024 Joel Shane. All rights reserved.

// This software is protected by copyright and international intellectual property laws. 
// You are hereby granted a non-exclusive, non-transferable license to use this software 
// for internal business purposes only. You may not modify, distribute, or 
// reverse engineer this software without the written permission of Joel Shane.

using UnityEngine;

public class Player : MonoBehaviour
{

    private Vector3 direction;

    public float gravity = -9.8f;
    
    public float strength = 5f;
    
    //Sprite rendering variable
    private SpriteRenderer spriteRenderer;

    // Array for the n number opf sprites
    public Sprite[] sprites;

    //Variable for find out current sprite index of the array
    private int spriteIndex;

   // Awake is called only once in the lifetime of the game.
    private void Awake()
    {
        //Sprite rendering called only once in the game
     spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Repeating the function to animate the flappy bird
        InvokeRepeating(nameof (SpriteAnimation),0.15f,0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    { 
        // 0 is the left mouse click and 1 is the right mouse click
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
         direction = Vector3.up * strength;
        }

        //Implementations for mobile devices
        if(Input.touchCount > 0)
        {
            Touch touch= Input.GetTouch(0);

            //TouchPhase.began means that just started touching the screen
            if(touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }
        
        /*  The player direction update in every frame of the game, 
         for this game only targeting the Y axis.*/
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    //Flappy bird animation functionality
    private void SpriteAnimation()
    {
        spriteIndex ++;

        if(spriteIndex >= sprites.Length)
        {
           spriteIndex =0;
        }
        //Updating the sprite index
        spriteRenderer.sprite=sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle") 
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if(other.gameObject.tag == "Score")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}

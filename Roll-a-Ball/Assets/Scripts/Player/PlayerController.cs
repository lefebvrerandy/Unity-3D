using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    //public ParticleSystem particleSystem;
    public float speed;
    private Rigidbody rb;
    public ParticleSystem particleSystem;   // This is used for the ending animation on game complete

    Scene currentScene;
    Scene nextScene;

    // Score keeper
    public int count;

    // Jump
    public bool Ability_Jump;
    public float jumpHeight;

    // Main Ground
    public GameObject MainGround;

    // Constants
    private const int MaxPickups = 12;
    private const int MaxPickupsLevel3MainFloor = 8;
    private const int MaxLevels = 3;


    /************************************************************/
    /*                      Event Methods                       */
    /************************************************************/
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        count = 0;
        if (count == 0)
            EndingPole.ep.endingSound = false;
        Ability_Jump = false;

        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        JumpStart();
        CheckOutOfBounds();
        CheckForKeyPress();
    }

    // Used for physics 
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }


    /************************************************************/
    /*                      Collision                           */
    /************************************************************/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            gameController.UpdatePickupCounter();
            CheckDeleteFloor();
            if (count == MaxPickups)
            {
                EndingPole.ep.endingSound = true;
            }
        }
        else if (other.gameObject.CompareTag("Tree"))
        {
            if (count >= MaxPickups)
            {
                if (currentScene.name == "level 3")
                {
                    // Make ball go poof
                    particleSystem.Play();
                    gameController.EndGame();
                    gameObject.SetActive(false);
                }
                else if (SceneManager.GetActiveScene().buildIndex != MaxLevels)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                else
                    SceneManager.LoadScene(0);
            }
        }

    }

    void OnCollisionEnter(Collision other)
    {
        JumpReset(other);
    }

    /************************************************************/
    /*                      Logic Methods                       */
    /************************************************************/
    /***********************************/
    /*              Jump               */
    /***********************************/
    void JumpStart()
    {
        gameController.UpdateJumpAbility();
        if (Ability_Jump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                // the cube is going to move upwards in 10 units per second
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
                Ability_Jump = false;
            }
        }
    }

    void JumpReset(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Ability_Jump = true;
        }
    }


    /***********************************/
    /*             Death               */
    /***********************************/
    void CheckOutOfBounds()
    {
        gameController.CheckOutOfBounds();

    }

    private void CheckForKeyPress()
    {
        gameController.HandleKeyPress();
    }

    /***********************************/
    /*             Level 3             */
    /***********************************/
    private void CheckDeleteFloor()
    {
        if ((count >= MaxPickupsLevel3MainFloor) && (currentScene.name == "level 3"))
        {
            MainGround.gameObject.SetActive(false);
        }
    }
}

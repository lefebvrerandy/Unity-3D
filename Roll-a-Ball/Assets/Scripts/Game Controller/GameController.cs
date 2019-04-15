using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Singleton gamecontroller object to run throughout the games lifetime
    public static GameController Instance { get; private set; }
    public EndingPole EndPole;

    public PlayerController player;
    GameObject northWall;
    public UserInterface UI;
    
    // local timer
    private Timer aTimer;
    private bool EndingGameTimerActive = false;
    private bool gameOver = false;

    // constants
    private const int EndGameWaitTime = 4000;
    private const int active = 1;
    private const int inactive = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            player.gameController = Instance;
            Instance.player = player;
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (gameOver)
        {
            SceneManager.LoadScene(0);
            gameOver = false;
        }
    }

    /************************************************************/
    /*                     User Interface                       */
    /************************************************************/
    public void UpdatePickupCounter()
    {
        UI.PickupCounter(player.count);
    }

    public void UpdateJumpAbility()
    {
        if (player.Ability_Jump)
        {
            UI.JumpAbilityUnlocked();
        }
        else
        {
            UI.JumpAbilityLocked();
        }
    }

    /************************************************************/
    /*                        Player Death                      */
    /************************************************************/
    public void CheckOutOfBounds()
    {
        if (player.transform.position.y <= -20)
        {
            Death();
        }
    }
    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /************************************************************/
    /*                        Key Press                         */
    /************************************************************/
    public void HandleKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == active)
            {
                Time.timeScale = inactive;
                SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
            }
            else if (Time.timeScale == inactive)
            {
                SceneManager.UnloadSceneAsync("Pause Menu");
                Time.timeScale = active;
            }
        }
    }

    // start a 2 second timer to end the change and load the menu
    public void EndGame()
    {
        // Check if the gameobject has been deleted. If so
        //lets start a timer for 2 seconds and then redirect to an event
        // that ends the game
        if (!EndingGameTimerActive)
        {
            aTimer = new System.Timers.Timer(EndGameWaitTime);
            aTimer.Elapsed += EndGame;
            aTimer.Enabled = true;
            EndingGameTimerActive = true;
        }
    }

    private void EndGame(System.Object source, ElapsedEventArgs e)
    {
        aTimer.Dispose();
        EndingGameTimerActive = false;
        gameOver = true;
    }
}

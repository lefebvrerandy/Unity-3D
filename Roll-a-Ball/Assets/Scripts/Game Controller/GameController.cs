using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    GameObject northWall;
    public UserInterface UI;

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
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
            }
            else if (Time.timeScale == 0)
            {
                SceneManager.UnloadSceneAsync("Pause Menu");
                Time.timeScale = 1;
            }
        }
    }
}

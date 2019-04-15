using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public static UserInterface Instance;
    public Text countText;
    public Text winText;
    public Text jumpText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance.winText.text = "";
            Instance.countText.text = "Count: 0";
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        countText.text = "Count: 0";
        winText.text = "";
        JumpAbilityLocked();
    }

    public void PickupCounter(int count)
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "Well Done! Find the exit.";
        }
    }

    public void PlayerWonGame()
    {
        winText.text = "Great Job!";
    }

    public void JumpAbilityUnlocked()
    {
        jumpText.text = "Jump Ability: UNLOCKED";
    }

    public void JumpAbilityLocked()
    {
        jumpText.text = "Jump Ability: LOCKED";
    }
}

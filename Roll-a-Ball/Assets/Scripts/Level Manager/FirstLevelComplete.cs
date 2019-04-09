using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelComplete : MonoBehaviour
{
    GameObject newGround;

    // Start is called before the first frame update
    void Start()
    {
        newGround = GameObject.Find("Ground2");
    }

    // Update is called once per frame
    void Update()
    {
        LowerWall();
    }

    void LowerWall()
    {
        if (transform.position.y >= -1.0f)
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);

        if (transform.position.y <= -1.0f)
        {
            if (newGround.transform.position.y < 0)
            {
                newGround.transform.position = new Vector3(
                    newGround.transform.position.x,
                    newGround.transform.position.y + 0.5f,
                    newGround.transform.position.z);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("ChangeScore", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeScore()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            score += 20;
        }
        else
        {
            score += 10;
        }
        if(!playerControllerScript.gameOver)
        {
            Debug.Log("Score =" + score);
        }
    }
}

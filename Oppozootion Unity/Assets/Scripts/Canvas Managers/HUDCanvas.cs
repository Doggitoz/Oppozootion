
/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Anupam Terkonda
 * Last Edited: April 22, 2022
 * 
 * Description: Updates HUD canvas referecing game manager
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvas : MonoBehaviour
{
    /*** VARIABLES ***/

    GameManager gm; //reference to game manager

    [Header("Canvas SETTINGS")]
    public Text textboxP1; //textbox for player 1's score
    public Text textboxP2; //textbox for player 2's score
    public Text textboxP3; //textbox for player 3's score
    public Text textboxP4; //textbox for player 4's score
    public Text textboxMaxScore; //textbox for max score
    public Text textboxTimer; //textbox for timer

    //GM Data
    private int scoreP1;
    private int scoreP2;
    private int scoreP3;
    private int scoreP4;
    private int maxScore;
    private float maxTimeAllowed;
    private float timer;




    private void Start()
    {
        gm = GameManager.GM; //find the game manager

        maxScore = GameManager.pointsToWinGame; //get the max score from the game manager
        maxTimeAllowed = GameManager.turnTimer; 
        timer = maxTimeAllowed; //set the timer to the max time allowed

        SetHUD();
    }//end Start

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; //reduce time by 1 second
            Debug.Log("Reduced Time");
        }
        else
        {
            //Automatically move to next player's turn
            timer = maxTimeAllowed; //set timer back to start
            Debug.Log("Timer ran out!");
        }
        GetGameStats();
        SetHUD();
    }//end Update()

    void GetTime()
    {
        
    }//end GetTime()

    void GetGameStats()
    {
        //get all the player's score from the game manager
        scoreP1 = GameManager.pOneScore;
        scoreP2 = GameManager.pTwoScore;
        scoreP3 = GameManager.pThreeScore;
        scoreP4 = GameManager.pFourScore;
        //timer = gm.turnTimer;

    }

    void SetHUD()
    {
        //if texbox exsists update value
        if (textboxP1) { textboxP1.text = "Player 1: " + scoreP1; }
        if (textboxP2) { textboxP2.text = "Player 2: " + scoreP2; }
        if (textboxP3) { textboxP3.text = "Player 3: " + scoreP3; }
        if (textboxP4) { textboxP4.text = "Player 4: " + scoreP4; }
        if (textboxMaxScore) { textboxMaxScore.text = "Max Score: " + maxScore; }
        if (textboxTimer) { textboxTimer.text = "Time Remaining: " + timer.ToString("0"); } //displays timer rounded to the nearest decimal

    }//end SetHUD()

}